using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MainProjectHos.Models.BusinessLayer;
using MainProjectHos.Models.DataLayer;
using MainProjectHos.Models.DataModels;
using System.Data;

namespace MainProjectHos
{
    public partial class frmAllocConsultDoctorToPatient : System.Web.UI.Page
    {
        AllocConsultDoctorToPatientBLL MobjClaim = new AllocConsultDoctorToPatientBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] != null)
            {
                EntityLogin entLogin = (EntityLogin)Session["user"];

                List<EntityFormMaster> entFormAcc = (List<EntityFormMaster>)Session["AccessibleForm"];
                int Access = (from tbl in entFormAcc
                              where tbl.FormName.Contains("frmAllocConsultDoctorToPatient")
                              select tbl).Count();

                if (Access > 0)
                {
                    if (!Page.IsPostBack)
                    {
                        //BindConsultDoctor();
                        BindPatientList();
                        BindCategory();
                        Session["Myflag"] = string.Empty;
                        BindAllocateDocToPatient();
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

        private void BindCategory()
        {
            try
            {
                DataTable tblCat = new OpeartionMasterBLL().GetAllCategoryName();
                DataRow dr = tblCat.NewRow();
                dr["CategoryId"] = 0;
                dr["CategoryName"] = "---Select---";
                tblCat.Rows.InsertAt(dr, 0);

                ddlCategoryName.DataSource = tblCat;
                ddlCategoryName.DataValueField = "CategoryId";
                ddlCategoryName.DataTextField = "CategoryName";
                ddlCategoryName.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void ddlCategoryName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                PatientMasterBLL mobjPatient = new PatientMasterBLL();
                int linReligionId = Convert.ToInt32(ddlCategoryName.SelectedValue);
                DataTable ldt = new DataTable();
                ldt = mobjPatient.GetDeptVisitDoctors(linReligionId);
                if (ldt.Rows.Count > 0 && ldt != null)
                {
                    ddlConsultDoctor.DataSource = ldt;
                    ddlConsultDoctor.DataValueField = "DocAllocId";
                    ddlConsultDoctor.DataTextField = "EmpName";
                    ddlConsultDoctor.DataBind();

                    FillDeptDoctorCast();
                    //ddlDeptDoctor.Enabled = true;
                }
                else
                {
                    //ddlDeptDoctor.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                //lblMsg.Text = ex.Message;
            }

        }

        private void FillDeptDoctorCast()
        {
            try
            {
                ListItem li = new ListItem();
                li.Text = "--Select Dept.Doctor--";
                li.Value = "0";
                ddlConsultDoctor.Items.Insert(0, li);
            }
            catch (Exception ex)
            {
                //lblMsg.Text = ex.Message;
            }
        }

        //public void BindConsultDoctor()
        //{
        //    try
        //    {
        //        DataTable tblDesig = new AllocConsultDoctorToPatientBLL().GetConsultDoctor();
        //        DataRow dr = tblDesig.NewRow();
        //        dr["PKId"] = 0;
        //        dr["AnaesthetistName"] = "---Select---";
        //        tblDesig.Rows.InsertAt(dr, 0);

        //        ddlConsultDoctor.DataSource = tblDesig;
        //        ddlConsultDoctor.DataValueField = "PKId";
        //        ddlConsultDoctor.DataTextField = "AnaesthetistName";
        //        ddlConsultDoctor.DataBind();
        //    }
        //    catch (Exception ex)
        //    {
        //        lblMessage.Text = ex.Message;
        //    }
        //}

        public void BindPatientList()
        {
            try
            {
                List<EntityPatientAdmit> lst = MobjClaim.GetPatientList();
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

        //protected void ddlPatient_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlPatient.SelectedIndex>0)
        //    {
        //        EntityPatientAlloc objTxt = new PatientAllocDocBLL().GetPatientType(Convert.ToInt32(ddlPatient.SelectedValue));
        //        CalDate.StartDate = objTxt.AdmitDate;
        //    }
        //}

        protected void ddlConsutDoctor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCategoryName.SelectedIndex > 0 && ddlConsultDoctor.SelectedIndex > 0)
            {
                txtConsultCharge.Text = Convert.ToString(MobjClaim.GetConsultCharge(Convert.ToInt32(ddlCategoryName.SelectedValue), Convert.ToInt32(ddlConsultDoctor.SelectedValue)));
            }
        }

        public void Clear()
        {
            ddlPatient.SelectedIndex = 0;
        }

        public void BindData()
        {
            List<EntityAllocaConDocDetails> lst = MobjClaim.GetDocForPatientAllocate(Convert.ToInt32(ddlPatient.SelectedValue));
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
                        ListItem itemProduct = ddlCategoryName.Items.FindByText(row.Cells[2].Text);
                        ddlCategoryName.SelectedValue = itemProduct.Value;
                        ListItem consultDoc = ddlConsultDoctor.Items.FindByText(row.Cells[3].Text);
                        ddlConsultDoctor.SelectedValue = consultDoc.Value;

                        txtConsultCharge.Text = row.Cells[4].Text;
                        txtConsultDate.Text = row.Cells[1].Text;
                    }
                    else
                    {
                        Session["TempId"] = Convert.ToInt32(dgvChargeDetails.DataKeys[row.RowIndex].Value);
                        ListItem itemProduct = ddlCategoryName.Items.FindByText(row.Cells[2].Text);
                        ddlCategoryName.SelectedValue = itemProduct.Value;
                        ListItem consultDoc = ddlConsultDoctor.Items.FindByText(row.Cells[3].Text);
                        ddlConsultDoctor.SelectedValue = consultDoc.Value;

                        txtConsultCharge.Text = row.Cells[4].Text;
                        txtConsultDate.Text = row.Cells[1].Text;
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
            List<EntityAllocaConDocDetails> lst = (List<EntityAllocaConDocDetails>)Session["Prescript"];
            List<EntityAllocaConDocDetails> lstFinal = new List<EntityAllocaConDocDetails>();
            if (BtnSave.Visible)
            {
                if (lst != null)
                {
                    foreach (EntityAllocaConDocDetails item in lst)
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
                foreach (EntityAllocaConDocDetails item in lst)
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
                List<EntityAllocaConDocDetails> lst = null;
                if (Convert.ToString(Session["MyFlag"]).Equals("Addnew", StringComparison.CurrentCultureIgnoreCase))
                {
                    lst = (List<EntityAllocaConDocDetails>)Session["Charges"];
                }
                else
                {
                    lst = (List<EntityAllocaConDocDetails>)Session["Prescript"];
                }
                lst.Add(new EntityAllocaConDocDetails
                {
                    PatientName = ddlPatient.SelectedItem.Text,
                    AdmitId = Convert.ToInt32(ddlPatient.SelectedValue),
                    /*TabletId = Convert.ToInt32(ddlTablet.SelectedValue),
                    MedicineName = ddlTablet.SelectedItem.Text,
                    Price = Convert.ToDecimal(txtPrice.Text),
                    Quantity=Convert.ToInt32(txtQuantity.Text),
                    Amount = Convert.ToDecimal(Convert.ToDecimal(txtPrice.Text) * Convert.ToInt32(txtQuantity.Text)),*/
                    TempId = lst.Count + 1
                });

                dgvChargeDetails.DataSource = lst;
                dgvChargeDetails.DataBind();
                /*txtTotal.Text = Convert.ToString(lst.Sum(p => p.Amount));*/
                Session["Unit"] = lst;
                Clear();
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ddlPatient.SelectedIndex = 0;
            /*txtPrice.Text = string.Empty;
            txtQuantity.Text = string.Empty;*/
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            tblAllocConsultDoctor tblins = new tblAllocConsultDoctor();
            tblins.CategoryId = Convert.ToInt32(ddlCategoryName.SelectedValue);
            tblins.ConsultDocId = Convert.ToInt32(ddlConsultDoctor.SelectedValue);
            tblins.ConsultCharges = Convert.ToDecimal(txtConsultCharge.Text);
            tblins.Consult_Date = Convert.ToDateTime(txtConsultDate.Text);
            ////tblins.TotalAmount = Convert.ToDecimal(txtTotal.Text);
            //tblPatientAdmitDetail objFac = MobjClaim.GetEmployee(Convert.ToInt32(ddlPatient.SelectedValue));
            //if (objFac != null)
            //{
            List<EntityAllocaConDocDetails> inslst = (List<EntityAllocaConDocDetails>)Session["Charges"];
            int ClaimId = Convert.ToInt32(MobjClaim.Save(tblins, inslst));
            lblMessage.Text = "Record Saved Successfully.....";
            Session["Charges"] = null;
            Clear();
            inslst = new List<EntityAllocaConDocDetails>();
            dgvChargeDetails.DataSource = inslst;
            dgvChargeDetails.DataBind();
            lblMsg.Text = string.Empty;
            //}
            //else
            //{
            //    lblMsg.Text = "Invalid Patient";
            //}
            Session["Charges"] = new List<EntityAllocaConDocDetails>();
            BindAllocateDocToPatient();
            MultiView1.SetActiveView(View1);
        }
        public void BindAllocateDocToPatient()
        {
            List<EntityAllocaConDoc> lst = MobjClaim.GetAllocatedPatientList();
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
                List<EntityAllocaConDocDetails> lst = MobjClaim.GetDocForPatientAllocate(Id);
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
                //ddlPatient.Enabled = false;
                tblAllocConsultDoctor tblins = new tblAllocConsultDoctor();
                tblins.SrNo = Convert.ToInt32(Session["PrescriptionId"]);
                tblins.CategoryId = Convert.ToInt32(ddlCategoryName.SelectedValue);
                tblins.ConsultDocId = Convert.ToInt32(ddlConsultDoctor.SelectedValue);
                tblins.ConsultCharges = Convert.ToDecimal(txtConsultCharge.Text);
                tblins.Consult_Date = Convert.ToDateTime(txtConsultDate.Text);
                //tblins.TotalAmount = Convert.ToDecimal(txtTotal.Text);
                List<EntityAllocaConDocDetails> inslst = (List<EntityAllocaConDocDetails>)Session["Prescript"];
                MobjClaim.Update(tblins, inslst);
                lblMessage.Text = "Record Updated Successfully.....";
                Clear();
                List<EntityAllocaConDocDetails> lst = new List<EntityAllocaConDocDetails>();
                dgvChargeDetails.DataSource = lst;
                dgvChargeDetails.DataBind();
                BindAllocateDocToPatient();
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
            if (Session["MyFlag"] == "Addnew")
            {
                List<EntityAllocaConDocDetails> lst = (List<EntityAllocaConDocDetails>)Session["Charges"];
                foreach (EntityAllocaConDocDetails item in lst)
                {
                    if (Convert.ToInt32(Session["TempId"]) == item.TempId)
                    {
                        item.AdmitId = Convert.ToInt32(ddlPatient.SelectedValue);
                        item.IsDelete = false;
                    }
                    else
                    {
                        if (item.IsDelete)
                        {
                            lst.Add(new EntityAllocaConDocDetails()
                            {
                                SrDetailId = item.SrDetailId,
                                SrNo = item.SrNo,
                                IsDelete = item.IsDelete,
                                AdmitId = item.AdmitId,
                            });
                        }
                    }
                }


                dgvChargeDetails.DataSource = lst;
                dgvChargeDetails.DataBind();
                //txtTotal.Text = Convert.ToString(lst.Where(p => p.IsDelete == false).ToList().Sum(p => p.Amount));
                Clear();
                btnUpdatecharge.Visible = false;
                btnAdd.Visible = true;
            }
            else
            {
                List<EntityAllocaConDocDetails> lst = (List<EntityAllocaConDocDetails>)Session["Prescript"];
                foreach (EntityAllocaConDocDetails item in lst)
                {
                    if (Convert.ToInt32(Session["TempId"]) == item.TempId)
                    {
                        item.AdmitId = Convert.ToInt32(ddlPatient.SelectedValue);
                        item.IsDelete = false;
                    }
                    else
                    {
                        if (item.IsDelete)
                        {
                            lst.Add(new EntityAllocaConDocDetails()
                            {
                                SrDetailId = item.SrDetailId,
                                SrNo = item.SrNo,
                                IsDelete = item.IsDelete,
                                AdmitId = item.AdmitId,
                            });
                        }
                    }
                }


                dgvChargeDetails.DataSource = lst;
                dgvChargeDetails.DataBind();
                //txtTotal.Text = Convert.ToString(lst.Where(p => p.IsDelete == false).ToList().Sum(p => p.Amount));
                Clear();
                btnUpdatecharge.Visible = false;
                btnAdd.Visible = true;
            }
        }

        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            btnAdd.Visible = true;
            btnUpdatecharge.Visible = false;
            BtnSave.Visible = false;
            btnUpdate.Visible = true;
            Session["MyFlag"] = "Edit";
            ImageButton imgEdit = (ImageButton)sender;
            GridViewRow row = (GridViewRow)imgEdit.NamingContainer;
            Session["PrescriptionId"] = Convert.ToInt32(dgvClaim.DataKeys[row.RowIndex].Value);
            ListItem item = ddlCategoryName.Items.FindByText(Convert.ToString(row.Cells[2].Text));
            ListItem itemDoc = ddlConsultDoctor.Items.FindByText(Convert.ToString(row.Cells[3].Text));
            if (item != null && itemDoc != null)
            {
                ddlCategoryName.SelectedValue = item.Value;
                ddlConsultDoctor.SelectedValue = itemDoc.Value;
                DateTime MDate = Convert.ToDateTime(row.Cells[1].Text);
                txtConsultDate.Text = string.Format("{0:MM/dd/yyyy}", MDate);
                txtConsultCharge.Text = Convert.ToString(row.Cells[4].Text);
                tblAllocConsultDoctor objPresc = MobjClaim.GetPrescriptionInfo(Convert.ToInt32(Session["PrescriptionId"]));
                BindPrescription(Convert.ToInt32(Session["PrescriptionId"]));
                //InjectionPara(false);
                MultiView1.SetActiveView(View2);
            }
            else
            {
                lblMessage.Text = "Category Name Not Found";
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
                    List<EntityAllocaConDoc> lst = MobjClaim.GetInsurance(txtSearch.Text);
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
                BindAllocateDocToPatient();
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
                ListConverter.ToDataTable((List<EntityAllocaConDoc>)Session["PrescriptDetails"]);
                Session["Details"] = ListConverter.ToDataTable((List<EntityAllocaConDoc>)Session["PrescriptDetails"]);
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
            //ddlPatient.Enabled = true;
            //ddlPatient.SelectedIndex = 0;
            ddlCategoryName.SelectedIndex = 0;
            //ddlConsultDoctor.SelectedIndex = 0;
            txtConsultCharge.Text = Convert.ToString(0);
            txtConsultDate.Enabled = false;
            txtConsultDate.Text = DateTime.UtcNow.Date.ToString("MM/dd/yyyy");
            BtnSave.Visible = true;
            btnUpdate.Visible = false;
            btnAdd.Visible = true;
            btnUpdatecharge.Visible = false;
            List<EntityAllocaConDocDetails> lst = new List<EntityAllocaConDocDetails>();
            dgvChargeDetails.DataSource = lst;
            dgvChargeDetails.DataBind();
            Session["Charges"] = lst;
            //MultiView1.SetActiveView(View2);
            MultiView1.SetActiveView(View2);
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
                dgvClaim.DataSource = (List<EntityOTMedicineBill>)Session["PrescriptDetails"];
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
    }
}