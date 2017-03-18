using MainProjectHos.Models.BusinessLayer;
using MainProjectHos.Models.DataLayer;
using MainProjectHos.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MainProjectHos
{
    public partial class frmPrescription : System.Web.UI.Page
    {
        PrescriptionBLL MobjClaim = new PrescriptionBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] != null)
            {
                EntityLogin entLogin = (EntityLogin)Session["user"];

                List<EntityFormMaster> entFormAcc = (List<EntityFormMaster>)Session["AccessibleForm"];
                int Access = (from tbl in entFormAcc
                              where tbl.FormName.Contains("frmPrescription")
                              select tbl).Count();

                if (Access > 0)
                {
                    if (!Page.IsPostBack)
                    {
                        BindTablet();
                        //BindPatientList();
                        GetDeptCategory();
                        Session["Myflag"] = string.Empty;
                        BindPrescription();
                        MultiView1.SetActiveView(View1);
                    }
                }
                else
                {
                    Response.Redirect("frmAdminMain.aspx", false);
                }
            }
            else
            {
                Response.Redirect("frmlogin.aspx", false);
            }
        }

        public void BindTablet()
        {
            try
            {
                IssueMaterialBLL mobjDeptBLL = new IssueMaterialBLL();
                List<EntityProduct> lstPat = mobjDeptBLL.GetProductList();
                ddlTablet.DataSource = lstPat;
                lstPat.Insert(0, new EntityProduct() { ProductId = 0, ProductName = "--Select--" });
                ddlTablet.DataValueField = "ProductId";
                ddlTablet.DataTextField = "ProductName";
                ddlTablet.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        public void GetDeptCategory()
        {
            try
            {
                PatientMasterBLL mobjPatient = new PatientMasterBLL();
                DataTable ldt = new DataTable();
                ldt = mobjPatient.GetDeptCategory();
                ddlDeptCategory.DataSource = ldt;
                ddlDeptCategory.DataValueField = "CategoryId";
                ddlDeptCategory.DataTextField = "CategoryName";
                ddlDeptCategory.DataBind();

                ListItem li = new ListItem();
                li.Text = "--Select DeptCategory--";
                li.Value = "0";
                ddlDeptCategory.Items.Insert(0, li);
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void ddlDeptCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int linCategoryId = Convert.ToInt32(ddlDeptCategory.SelectedValue);
                EntityPatientMaster objTxt = new PatientMasterBLL().GetDeptDoctor(Convert.ToInt32(ddlDeptCategory.SelectedValue));
                txtDeptCat.Text = objTxt.FullName;

                BindPatientList(linCategoryId);
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        public void BindPatientList(int CategoryId)
        {
            try
            {
                List<EntityPatientAdmit> lst = MobjClaim.GetPatientList(CategoryId);
                ddlPatient.DataSource = lst;
                lst.Insert(0, new EntityPatientAdmit() { AdmitId = 0, PatientFirstName = "--Select--" });
                ddlPatient.DataValueField = "AdmitId";
                ddlPatient.DataTextField = "PatientFirstName";
                ddlPatient.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void ddlPatient_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPatient.SelectedIndex > 0)
            {
                EntityPatientAlloc objTxt = new PatientAllocDocBLL().GetPatientType(Convert.ToInt32(ddlPatient.SelectedValue));
                //CalDate.StartDate = objTxt.AdmitDate;
            }
        }

        public void Clear()
        {
            ddlTablet.SelectedIndex = 0;
            txtMorning.Text = string.Empty;
            txtafternoon.Text = string.Empty;
            txtNight.Text = string.Empty;
            txtNoOfDays.Text = string.Empty;
            txtQuantity.Text = string.Empty;
        }

        public void BindData()
        {
            List<EntityPrescriptionDetails> lst = MobjClaim.GetPrescription(Convert.ToInt32(ddlPatient.SelectedValue));
            dgvChargeDetails.DataSource = lst;
            Session["Charges"] = lst;
            dgvChargeDetails.DataBind();
        }

        protected void BtnEditCharge_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton imgEdit = (ImageButton)sender;
                GridViewRow row = (GridViewRow)imgEdit.NamingContainer;
                if (row != null)
                {
                    btnUpdatecharge.Visible = true;
                    btnAdd.Visible = false;
                    if (Session["MyFlag"] == "Addnew")
                    {
                        Session["TempId"] = Convert.ToInt32(dgvChargeDetails.DataKeys[row.RowIndex].Value);
                        ListItem itemProduct = ddlTablet.Items.FindByText(row.Cells[0].Text);
                        ddlTablet.SelectedValue = itemProduct.Value;
                        txtMorning.Text = row.Cells[1].Text;
                        txtafternoon.Text = row.Cells[2].Text;
                        txtNight.Text = row.Cells[3].Text;
                        txtNoOfDays.Text = row.Cells[4].Text;
                        txtQuantity.Text = row.Cells[5].Text;
                        if (row.Cells[6].Text == "True")
                        {
                            chkDress.Checked = true;
                        }
                        else
                        {
                            chkDress.Checked = false;
                        }
                        if (row.Cells[7].Text == "True")
                        {
                            chkInject.Checked = true;
                            txtInjection.Visible = true;
                            txtInjection.Text = row.Cells[8].Text;
                        }
                        else
                        {
                            chkInject.Checked = false;
                        }
                        //txtInjection.Text = row.Cells[11].Text;
                    }
                    else
                    {
                        Session["TempId"] = Convert.ToInt32(dgvChargeDetails.DataKeys[row.RowIndex].Value);
                        ListItem itemProduct = ddlTablet.Items.FindByText(row.Cells[0].Text);
                        ddlTablet.SelectedValue = itemProduct.Value;
                        txtMorning.Text = row.Cells[1].Text;
                        txtafternoon.Text = row.Cells[2].Text;
                        txtNight.Text = row.Cells[3].Text;
                        txtNoOfDays.Text = row.Cells[4].Text;
                        txtQuantity.Text = row.Cells[5].Text;
                        if (row.Cells[6].Text == "True")
                        {
                            chkDress.Checked = true;
                        }
                        else
                        {
                            chkDress.Checked = false;
                        }
                        if (row.Cells[7].Text == "True")
                        {
                            chkInject.Checked = true;
                            txtInjection.Visible = true;
                            txtInjection.Text = row.Cells[8].Text;
                        }
                        else
                        {
                            chkInject.Checked = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ImageButton imgDelete = (ImageButton)sender;
            GridViewRow row = (GridViewRow)imgDelete.NamingContainer;
            List<EntityPrescriptionDetails> lst = (List<EntityPrescriptionDetails>)Session["Prescript"];
            List<EntityPrescriptionDetails> lstFinal = new List<EntityPrescriptionDetails>();
            if (BtnSave.Visible)
            {
                if (lst != null)
                {
                    foreach (EntityPrescriptionDetails item in lst)
                    {
                        if (item.TempId != Convert.ToInt32(dgvChargeDetails.DataKeys[row.RowIndex].Value))
                        {
                            lstFinal.Add(item);
                        }
                    }
                    dgvChargeDetails.DataSource = lstFinal;
                    dgvChargeDetails.DataBind();
                    Session["Prescript"] = lst;
                }
            }
            else
            {
                foreach (EntityPrescriptionDetails item in lst)
                {
                    if (item.TempId == Convert.ToInt32(dgvChargeDetails.DataKeys[row.RowIndex].Value))
                    {
                        item.IsDelete = true;
                    }
                }
                dgvChargeDetails.DataSource = lst.Where(p => p.IsDelete == false).ToList();
                dgvChargeDetails.DataBind();
                Session["Prescript"] = lst;
            }
        }

        protected void btnAddCharge_Click(object sender, EventArgs e)
        {
            try
            {
                List<EntityPrescriptionDetails> lst = null;
                if (Convert.ToString(Session["MyFlag"]).Equals("Addnew", StringComparison.CurrentCultureIgnoreCase))
                {
                    lst = (List<EntityPrescriptionDetails>)Session["Charges"];
                }
                else
                {
                    lst = (List<EntityPrescriptionDetails>)Session["Prescript"];
                }
                lst.Add(new EntityPrescriptionDetails
                {
                    ProductId = Convert.ToInt32(ddlTablet.SelectedValue),
                    ProductName = ddlTablet.SelectedItem.Text,
                    Morning = txtMorning.Text,
                    Afternoon = txtafternoon.Text,
                    Night = txtNight.Text,
                    NoOfDays = txtNoOfDays.Text,
                    Quantity = txtQuantity.Text,
                    TempId = lst.Count + 1
                });

                dgvChargeDetails.DataSource = lst;
                dgvChargeDetails.DataBind();
                Session["Unit"] = lst;
                Clear();
                InjectionPara(chkInject.Checked);
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ddlTablet.SelectedIndex = 0;
            txtMorning.Text = string.Empty;
            txtafternoon.Text = string.Empty;
            txtNight.Text = string.Empty;
            txtNoOfDays.Text = string.Empty;
            txtQuantity.Text = string.Empty;
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            tblPrescription tblins = new tblPrescription();
            tblins.DeptCategory = Convert.ToInt32(ddlDeptCategory.SelectedValue);
            tblins.DeptDoctor = txtDeptCat.Text;
            tblins.AdmitId = Convert.ToInt32(ddlPatient.SelectedValue);
            tblins.Prescription_Date = Convert.ToDateTime(txtPrescriptionDate.Text);
            tblins.IsDressing = chkDress.Checked;
            tblins.IsInjection = chkInject.Checked;
            if (chkInject.Checked)
            {
                tblins.InjectionName = txtInjection.Text;
            }
            tblins.Investigation = Convert.ToString(txtInvestigation.Text);
            tblins.Impression = Convert.ToString(txtImpression.Text);
            tblins.AdviceNote = Convert.ToString(txtAdviceNote.Text);
            tblins.Remarks = Convert.ToString(txtRemarks.Text);

            tblPatientAdmitDetail objFac = MobjClaim.GetEmployee(Convert.ToInt32(ddlPatient.SelectedValue));
            if (objFac != null)
            {
                List<EntityPrescriptionDetails> inslst = (List<EntityPrescriptionDetails>)Session["Charges"];
                int ClaimId = Convert.ToInt32(MobjClaim.Save(tblins, inslst));
                lblMessage.Text = "Record Saved Successfully.....";
                Session["Charges"] = null;
                chkDress.Checked = false;
                chkInject.Checked = false;
                txtInjection.Text = string.Empty;
                txtInvestigation.Text = string.Empty;
                txtImpression.Text = string.Empty;
                txtAdviceNote.Text = string.Empty;
                txtRemarks.Text = string.Empty;
                InjectionPara(chkInject.Checked);
                Clear();
                inslst = new List<EntityPrescriptionDetails>();
                dgvChargeDetails.DataSource = inslst;
                dgvChargeDetails.DataBind();
                lblMsg.Text = string.Empty;
                InjectionPara(chkInject.Checked);
            }
            else
            {
                lblMsg.Text = "Invalid Patient";
            }
            Session["Charges"] = new List<EntityPrescriptionDetails>();
            BindPrescription();
            MultiView1.SetActiveView(View1);
        }
        public void BindPrescription()
        {
            List<EntityPrescription> lst = MobjClaim.GetInsurance();
            Session["PrescriptDetails"] = lst;
            dgvClaim.DataSource = lst;
            int lintRowcount = lst.Count();
            lblRowCount.Text = "<b>Total Records:</b> " + lintRowcount.ToString();
            dgvClaim.DataBind();
        }

        public void BindPrescription(int Id)
        {
            try
            {
                List<EntityPrescriptionDetails> lst = MobjClaim.GetPrescription(Id);
                //Session["DetailID"] = lst[0].TempId;
                dgvChargeDetails.DataSource = lst;
                dgvChargeDetails.DataBind();
                Session["Prescript"] = lst;
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }
        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                ddlPatient.Enabled = false;
                tblPrescription tblins = new tblPrescription();
                tblins.Prescription_Id = Convert.ToInt32(Session["PrescriptionId"]);
                tblins.AdmitId = Convert.ToInt32(ddlPatient.SelectedValue);
                tblins.DeptCategory = Convert.ToInt32(ddlDeptCategory.SelectedValue);
                tblins.DeptDoctor = txtDeptCat.Text;
                tblins.Prescription_Date = Convert.ToDateTime(txtPrescriptionDate.Text);
                tblins.InjectionName = txtInjection.Text;
                tblins.IsDressing = chkDress.Checked;
                tblins.IsInjection = chkInject.Checked;
                tblins.AdviceNote = txtAdviceNote.Text;
                tblins.Investigation = txtInvestigation.Text;
                tblins.Impression = Convert.ToString(txtImpression.Text);
                tblins.Remarks = Convert.ToString(txtRemarks.Text);
                List<EntityPrescriptionDetails> inslst = (List<EntityPrescriptionDetails>)Session["Prescript"];
                MobjClaim.Update(tblins, inslst);
                lblMessage.Text = "Record Updated Successfully.....";
                chkDress.Checked = false;
                chkInject.Checked = false;
                txtInjection.Text = string.Empty;
                txtInvestigation.Text = string.Empty;
                txtImpression.Text = string.Empty;
                txtAdviceNote.Text = string.Empty;
                txtRemarks.Text = string.Empty;
                InjectionPara(chkInject.Checked);
                Clear();
                List<EntityPrescriptionDetails> lst = new List<EntityPrescriptionDetails>();
                dgvChargeDetails.DataSource = lst;
                dgvChargeDetails.DataBind();
                BindPrescription();
                Session["Charges"] = null;
                MultiView1.SetActiveView(View1);
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void btnUpdatecharge_Click(object sender, EventArgs e)
        {
            List<EntityPrescriptionDetails> lst = (List<EntityPrescriptionDetails>)Session["Prescript"];
            foreach (EntityPrescriptionDetails item in lst)
            {
                if (Convert.ToInt32(Session["TempId"]) == item.TempId)
                {
                    item.ProductId = Convert.ToInt32(ddlTablet.SelectedValue);
                    item.Morning = Convert.ToString(txtMorning.Text);
                    item.Afternoon = Convert.ToString(txtafternoon.Text);
                    item.Night = Convert.ToString(txtNight.Text);
                    item.NoOfDays = Convert.ToString(txtNoOfDays.Text);
                    item.Quantity = Convert.ToString(txtQuantity.Text);
                    item.IsDelete = false;
                }
                else
                {
                    if (item.IsDelete)
                    {
                        lst.Add(new EntityPrescriptionDetails()
                        {
                            PrescriptionDetailId = item.PrescriptionDetailId,
                            Prescription_Id = item.Prescription_Id,
                            IsDelete = item.IsDelete,
                            ProductId = item.ProductId,
                            Quantity = item.Quantity,
                            Morning = item.Morning,
                            Afternoon = item.Afternoon,
                            Night = item.Night,
                            NoOfDays = item.NoOfDays
                        });
                    }
                }
            }
            //MobjClaim.Update(lst);
            dgvChargeDetails.DataSource = lst;
            dgvChargeDetails.DataBind();
            Clear();
            InjectionPara(false);
            btnUpdatecharge.Visible = false;
            btnAdd.Visible = true;
        }

        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            btnAdd.Visible = true;
            btnUpdatecharge.Visible = false;
            BtnSave.Visible = false;
            btnUpdate.Visible = true;
            Session["MyFlag"] = "Edit";
            GetDeptCategory();
            ImageButton imgEdit = (ImageButton)sender;
            GridViewRow row = (GridViewRow)imgEdit.NamingContainer;
            Session["PrescriptionId"] = Convert.ToInt32(dgvClaim.DataKeys[row.RowIndex].Value);
            ListItem item1 = ddlDeptCategory.Items.FindByText(Convert.ToString(row.Cells[3].Text));
            if (item1 != null)
            {
                ddlDeptCategory.SelectedValue = item1.Value;
                ListItem item = ddlPatient.Items.FindByText(Convert.ToString(row.Cells[1].Text));
                ddlPatient.SelectedValue = item.Value;
                DateTime MDate = Convert.ToDateTime(row.Cells[2].Text);
                txtPrescriptionDate.Text = string.Format("{0:MM/dd/yyyy}", MDate);
                txtDeptCat.Text = Convert.ToString(row.Cells[4].Text);
                tblPrescription objPresc = MobjClaim.GetPrescriptionInfo(Convert.ToInt32(Session["PrescriptionId"]));
                chkDress.Checked = Convert.ToBoolean(objPresc.IsDressing);
                chkInject.Checked = Convert.ToBoolean(objPresc.IsInjection);
                txtInvestigation.Text = Convert.ToString(objPresc.Investigation);
                txtImpression.Text = Convert.ToString(objPresc.Impression);
                txtAdviceNote.Text = Convert.ToString(objPresc.AdviceNote);
                txtRemarks.Text = Convert.ToString(objPresc.Remarks);
                InjectionPara(objPresc.IsInjection.Value);
                txtInjection.Text = objPresc.InjectionName;
                BindPrescription(Convert.ToInt32(Session["PrescriptionId"]));
                //InjectionPara(false);
                MultiView1.SetActiveView(View2);
            }
            else
            {
                lblMessage.Text = "Patient Already Discharged";
            }
        }

        protected void BtnClose_Click(object sender, EventArgs e)
        {
            Clear();
            MultiView1.SetActiveView(View1);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSearch.Text))
                {
                    List<EntityPrescription> lst = MobjClaim.GetInsurance(txtSearch.Text);
                    if (lst.Count > 0)
                    {
                        dgvClaim.DataSource = lst;
                        dgvClaim.DataBind();
                        Session["PrescriptDetails"] = lst;
                        int lintRowcount = lst.Count;
                        lblRowCount.Text = "<b>Total Records:</b> " + lintRowcount.ToString();
                    }
                    else
                    {
                        lblMessage.Text = "No Record Found";
                    }
                }
                else
                {
                    lblMessage.Text = "Please Enter Content To Search";
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                BindPrescription();
                txtSearch.Text = string.Empty;
                lblMessage.Text = string.Empty;
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                ListConverter.ToDataTable((List<EntityPrescription>)Session["PrescriptDetails"]);
                Session["Details"] = ListConverter.ToDataTable((List<EntityPrescription>)Session["PrescriptDetails"]);
                Response.Redirect("~/ExcelReport/MonthwiseSalExcel.aspx");
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void BtnAddNewPrescription_Click(object sender, EventArgs e)
        {
            Session["MyFlag"] = "Addnew";
            ddlPatient.Enabled = true;
            //ddlPatient.SelectedIndex = 0;
            txtPrescriptionDate.Text = string.Empty;
            BtnSave.Visible = true;
            lblBalQty.Text = Convert.ToString(0);
            btnUpdate.Visible = false;
            btnAdd.Visible = true;
            btnUpdatecharge.Visible = false;
            InjectionPara(false);
            txtInvestigation.Text = string.Empty;
            txtImpression.Text = string.Empty;
            txtAdviceNote.Text = string.Empty;
            txtRemarks.Text = string.Empty;
            List<EntityPrescriptionDetails> lst = new List<EntityPrescriptionDetails>();
            dgvChargeDetails.DataSource = lst;
            dgvChargeDetails.DataBind();
            Session["Charges"] = lst;
            MultiView1.SetActiveView(View2);
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton imgEdit = (ImageButton)sender;
                GridViewRow cnt = (GridViewRow)imgEdit.NamingContainer;
                Session["Prescription_Id"] = cnt.Cells[0].Text;
                int ID_Issue = Convert.ToInt32(Session["Prescription_Id"]);
                if (ID_Issue > 0)
                {
                    Session["ReportType"] = "Prescription";
                    Response.Redirect("~/PathalogyReport/PathologyReport.aspx", false);
                }
                else
                {
                    //lblMessage.Text = "This Patient Don't Have Invoice";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void dgvClaim_DataBound(object sender, EventArgs e)
        {
            int lintCurrentPage = dgvClaim.PageIndex + 1;
            lblPageCount.Text = "<b>Page</b> " + lintCurrentPage.ToString() + "<b> of </b>" + dgvClaim.PageCount.ToString();
        }
        protected void dgvClaim_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dgvClaim.DataSource = (List<EntityPrescription>)Session["PrescriptDetails"];
                dgvClaim.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
        protected void dgvClaim_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvClaim.PageIndex = e.NewPageIndex;
        }
        protected void dgvChargeDetails_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void chkInject_CheckedChanged(object sender, EventArgs e)
        {
            if (chkInject.Checked)
            {
                InjectionPara(true);
            }
            else
            {
                InjectionPara(false);
            }
        }

        private void InjectionPara(bool flag)
        {
            txtInjection.Visible = flag;
            lblInjection.Visible = flag;
        }

        protected void ddlTablet_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                List<EntityPrescriptionDetails> lst = null;
                if (ddlTablet.SelectedIndex > 0)
                {
                    IssueMaterialBLL mobjDeptBLL = new IssueMaterialBLL();
                    EntityProduct entProduct = mobjDeptBLL.GetProductPrice(Convert.ToInt32(ddlTablet.SelectedValue));

                    List<tblStockDetail> lst1 = new CustomerTransactionBLL().GetProductTransByProductId(Convert.ToInt32(ddlTablet.SelectedValue));
                    if (lst1 != null)
                    {
                        lblBalQty.Text = Convert.ToString(Convert.ToInt32(lst1.Sum(p => p.InwardQty)) - Convert.ToInt32(lst1.Sum(p => p.OutwardQty)));
                    }
                    else
                    {
                        lblBalQty.Text = string.Empty;
                    }

                }
                else
                {
                    lst = new List<EntityPrescriptionDetails>();
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }
    }
}