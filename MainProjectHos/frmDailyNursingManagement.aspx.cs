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
    public partial class frmDailyNursingManagement : System.Web.UI.Page
    {
        NursingManagementBLL MobjClaim = new NursingManagementBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] != null)
            {
                EntityLogin entLogin = (EntityLogin)Session["user"];

                List<EntityFormMaster> entFormAcc = (List<EntityFormMaster>)Session["AccessibleForm"];
                int Access = (from tbl in entFormAcc
                              where tbl.FormName.Contains("frmDailyNursingManagement")
                              select tbl).Count();

                if (Access > 0)
                {
                    if (!Page.IsPostBack)
                    {
                        BindNurse();
                        //BindPatientList();
                        BindCategory();
                        Session["Myflag"] = string.Empty;
                        BindDailyNursingManagement();
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

        private void BindNurse()
        {
            try
            {
                DataTable tblCat = new NurseBLL().GetAllReligion();
                DataRow dr = tblCat.NewRow();
                dr["PKId"] = 0;
                dr["NurseName"] = "---Select---";
                tblCat.Rows.InsertAt(dr, 0);
                ddlNurseName.DataSource = tblCat;
                ddlNurseName.DataValueField = "PKId";
                ddlNurseName.DataTextField = "NurseName";
                ddlNurseName.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private void BindCategory()
        {
            try
            {
                DataTable tblCat = new RoomMasterBLL().GetAllCategory();
                DataRow dr = tblCat.NewRow();
                dr["PKId"] = 0;
                dr["CategoryDesc"] = "---Select---";
                tblCat.Rows.InsertAt(dr, 0);

                ddlWardCategory.DataSource = tblCat;
                ddlWardCategory.DataValueField = "PKId";
                ddlWardCategory.DataTextField = "CategoryDesc";
                ddlWardCategory.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        //public void BindPatientList()
        //{
        //    try
        //    {
        //        List<EntityPatientAdmit> lst = MobjClaim.GetPatientList();
        //        ddlPatient.DataSource = lst;
        //        lst.Insert(0, new EntityPatientAdmit() { AdmitId = 0, PatientFirstName = "--Select--" });
        //        ddlPatient.DataValueField = "AdmitId";
        //        ddlPatient.DataTextField = "PatientFirstName";
        //        ddlPatient.DataBind();
        //    }
        //    catch (Exception ex)
        //    {
        //        lblMessage.Text = ex.Message;
        //    }
        //}

        //protected void ddlPatient_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlPatient.SelectedIndex>0)
        //    {
        //        EntityPatientAlloc objTxt = new PatientAllocDocBLL().GetPatientType(Convert.ToInt32(ddlPatient.SelectedValue));
        //        CalDate.StartDate = objTxt.AdmitDate;
        //    }
        //}

        protected void ddlNurseName_SelectedIndexChanged(object sender, EventArgs e)
        {
            PatientMasterBLL mobjPatient = new PatientMasterBLL();
            string dept = string.Empty;
            if (ddlNurseName.SelectedIndex > 0)
            {
                txtDepartment.Text = Convert.ToString(MobjClaim.GetConsultCharge(Convert.ToInt32(ddlNurseName.SelectedValue)));

                dept = txtDepartment.Text;
                List<EntityPatientAdmit> lst = MobjClaim.GetPatientList(dept);
                ddlPatient.DataSource = lst;
                lst.Insert(0, new EntityPatientAdmit() { AdmitId = 0, PatientFirstName = "--Select--" });
                ddlPatient.DataValueField = "AdmitId";
                ddlPatient.DataTextField = "PatientFirstName";
                ddlPatient.DataBind();
            }
        }

        public void Clear()
        {
            // ddlPatient.SelectedIndex = 0;
            txtInjectableMedi.Text = string.Empty;
            txtInfusions.Text = string.Empty;
            txtOral.Text = string.Empty;
            txtNursingCare.Text = string.Empty;

        }

        public void BindData()
        {
            List<EntityNursingManagementDetails> lst = MobjClaim.GetDocForPatientAllocate(Convert.ToInt32(ddlPatient.SelectedValue));
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
                    if (Session["MyFlag"].ToString() == "Addnew")
                    {
                        Session["TempId"] = Convert.ToInt32(dgvChargeDetails.DataKeys[row.RowIndex].Value);
                        ListItem itemPatient = ddlPatient.Items.FindByText(row.Cells[0].Text);
                        ddlPatient.SelectedValue = itemPatient.Value;
                        txtInjectableMedi.Text = row.Cells[2].Text;
                        txtInfusions.Text = row.Cells[3].Text;
                        txtOral.Text = row.Cells[4].Text;
                        txtNursingCare.Text = row.Cells[5].Text;
                    }
                    else
                    {
                        Session["TempId"] = Convert.ToInt32(dgvChargeDetails.DataKeys[row.RowIndex].Value);
                        ListItem itemPatient = ddlPatient.Items.FindByText(row.Cells[0].Text);
                        ddlPatient.SelectedValue = itemPatient.Value;
                        txtInjectableMedi.Text = row.Cells[2].Text;
                        txtInfusions.Text = row.Cells[3].Text;
                        txtOral.Text = row.Cells[4].Text;
                        txtNursingCare.Text = row.Cells[5].Text;
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
            List<EntityNursingManagementDetails> lst = (List<EntityNursingManagementDetails>)Session["Prescript"];
            List<EntityNursingManagementDetails> lstFinal = new List<EntityNursingManagementDetails>();
            if (BtnSave.Visible)
            {
                if (lst != null)
                {
                    foreach (EntityNursingManagementDetails item in lst)
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
                foreach (EntityNursingManagementDetails item in lst)
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
                TimeSpan objTime = new TimeSpan(0,0, 0);
                List<EntityNursingManagementDetails> lst = null;
                if (Convert.ToString(Session["MyFlag"]).Equals("Addnew", StringComparison.CurrentCultureIgnoreCase))
                {
                    lst = (List<EntityNursingManagementDetails>)Session["Charges"];
                }
                else
                {
                    lst = (List<EntityNursingManagementDetails>)Session["Prescript"];
                }
                lst.Add(new EntityNursingManagementDetails
                {

                    PatientName = ddlPatient.SelectedItem.Text,
                    PatientId = Convert.ToInt32(ddlPatient.SelectedValue),
                    InjectableMedications = Convert.ToString(txtInjectableMedi.Text),
                    Infusions = Convert.ToString(txtInfusions.Text),
                    Oral = Convert.ToString(txtOral.Text),
                    NursingCare = Convert.ToString(txtNursingCare.Text),
                    TreatmentTime = Convert.ToString(objTime),
                    NurseId = Convert.ToInt32(ddlNurseName.SelectedValue),
                    NurseName = Convert.ToString(ddlNurseName.SelectedItem.Text),
                    CategoryId = Convert.ToInt32(ddlWardCategory.SelectedValue),
                    CategoryDesc = Convert.ToString(ddlWardCategory.SelectedItem.Text),
                    Department = Convert.ToString(txtDepartment.Text),
                    TreatmentDate = Convert.ToDateTime(txtTreatmentDate.Text),
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
            txtInjectableMedi.Text = string.Empty;
            txtInfusions.Text = string.Empty;
            txtOral.Text = string.Empty;
            txtNursingCare.Text = string.Empty;
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            tblNursingManagement tblins = new tblNursingManagement();
            tblins.CategoryId = Convert.ToInt32(ddlWardCategory.SelectedValue);
            tblins.NurseId = Convert.ToInt32(ddlNurseName.SelectedValue);
            tblins.Department = Convert.ToString(txtDepartment.Text);
            tblins.TreatmentDate = Convert.ToDateTime(txtTreatmentDate.Text);

            List<EntityNursingManagementDetails> inslst = (List<EntityNursingManagementDetails>)Session["Charges"];
            int ClaimId = Convert.ToInt32(MobjClaim.Save(tblins, inslst));
            lblMessage.Text = "Record Saved Successfully.....";
            Session["Charges"] = null;
            Clear();
            inslst = new List<EntityNursingManagementDetails>();
            dgvChargeDetails.DataSource = inslst;
            dgvChargeDetails.DataBind();
            lblMsg.Text = string.Empty;

            Session["Charges"] = new List<EntityNursingManagementDetails>();
            BindDailyNursingManagement();
            MultiView1.SetActiveView(View1);
        }
        public void BindDailyNursingManagement()
        {
            List<EntityNursingManagement> lst = MobjClaim.GetAllocatedPatientList();
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
                List<EntityNursingManagementDetails> lst = MobjClaim.GetDocForPatientAllocate(Id);
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
                tblNursingManagement tblins = new tblNursingManagement();
                tblins.SrNo = Convert.ToInt32(Session["PrescriptionId"]);
                tblins.CategoryId = Convert.ToInt32(ddlWardCategory.SelectedValue);
                tblins.NurseId = Convert.ToInt32(ddlNurseName.SelectedValue);
                tblins.Department = Convert.ToString(txtDepartment.Text);
                tblins.TreatmentDate = Convert.ToDateTime(txtTreatmentDate.Text);
                List<EntityNursingManagementDetails> inslst = (List<EntityNursingManagementDetails>)Session["Prescript"];
                MobjClaim.Update(tblins, inslst);
                lblMessage.Text = "Record Updated Successfully.....";
                Clear();
                List<EntityNursingManagementDetails> lst = new List<EntityNursingManagementDetails>();
                dgvChargeDetails.DataSource = lst;
                dgvChargeDetails.DataBind();
                BindDailyNursingManagement();
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
            if (Session["MyFlag"].ToString() == "Addnew")
            {
                List<EntityNursingManagementDetails> lst = (List<EntityNursingManagementDetails>)Session["Charges"];
                foreach (EntityNursingManagementDetails item in lst)
                {
                    if (Convert.ToInt32(Session["TempId"]) == item.TempId)
                    {
                        TimeSpan objTime = new TimeSpan(0, 0, 0);
                        item.PatientId = Convert.ToInt32(ddlPatient.SelectedValue);
                        item.InjectableMedications = Convert.ToString(txtInjectableMedi.Text);
                        item.Infusions = Convert.ToString(txtInfusions.Text);
                        item.Oral = Convert.ToString(txtOral.Text);
                        item.NursingCare = Convert.ToString(txtNursingCare.Text);
                        item.TreatmentTime = Convert.ToString(objTime);
                        item.IsDelete = false;
                    }
                    else
                    {
                        if (item.IsDelete)
                        {
                            lst.Add(new EntityNursingManagementDetails()
                            {
                                SrDetailId = item.SrDetailId,
                                SrNo = item.SrNo,
                                IsDelete = item.IsDelete,
                                PatientId = item.PatientId,
                                InjectableMedications = item.InjectableMedications,
                                Infusions = item.Infusions,
                                Oral = item.Oral,
                                NursingCare = item.NursingCare,
                                TreatmentTime = item.TreatmentTime,
                                NurseId = item.NurseId,
                                CategoryId = item.CategoryId,
                                Department = item.Department,
                                TreatmentDate = item.TreatmentDate
                            });
                        }
                    }
                }


                dgvChargeDetails.DataSource = lst;
                dgvChargeDetails.DataBind();
                Clear();
                btnUpdatecharge.Visible = false;
                btnAdd.Visible = true;
            }
            else
            {
                List<EntityNursingManagementDetails> lst = (List<EntityNursingManagementDetails>)Session["Prescript"];
                foreach (EntityNursingManagementDetails item in lst)
                {
                    if (Convert.ToInt32(Session["TempId"]) == item.TempId)
                    {
                        TimeSpan objTime = new TimeSpan(0, 0, 0);
                        item.PatientId = Convert.ToInt32(ddlPatient.SelectedValue);
                        item.InjectableMedications = Convert.ToString(txtInjectableMedi.Text);
                        item.Infusions = Convert.ToString(txtInfusions.Text);
                        item.Oral = Convert.ToString(txtOral.Text);
                        item.NursingCare = Convert.ToString(txtNursingCare.Text);
                        item.TreatmentTime = Convert.ToString(objTime);
                        item.IsDelete = false;
                    }
                    else
                    {
                        if (item.IsDelete)
                        {
                            lst.Add(new EntityNursingManagementDetails()
                            {
                                SrDetailId = item.SrDetailId,
                                SrNo = item.SrNo,
                                IsDelete = item.IsDelete,
                                PatientId = item.PatientId,
                                InjectableMedications = item.InjectableMedications,
                                Infusions = item.Infusions,
                                Oral = item.Oral,
                                NursingCare = item.NursingCare,
                                TreatmentTime = item.TreatmentTime,
                                NurseId = item.NurseId,
                                CategoryId = item.CategoryId,
                                Department = item.Department,
                                TreatmentDate = item.TreatmentDate
                            });
                        }
                    }
                }


                dgvChargeDetails.DataSource = lst;
                dgvChargeDetails.DataBind();
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
            ListItem item = ddlNurseName.Items.FindByText(Convert.ToString(row.Cells[2].Text));
            ListItem itemDoc = ddlWardCategory.Items.FindByText(Convert.ToString(row.Cells[4].Text));
            if (item != null && itemDoc != null)
            {
                ddlNurseName.SelectedValue = item.Value;
                ddlWardCategory.SelectedValue = itemDoc.Value;
                DateTime MDate = Convert.ToDateTime(row.Cells[1].Text);
                txtTreatmentDate.Text = string.Format("{0:MM/dd/yyyy}", MDate);
                txtDepartment.Text = Convert.ToString(row.Cells[3].Text);
                string dept = txtDepartment.Text;
                List<EntityPatientAdmit> lst = MobjClaim.GetPatientList(dept);
                ddlPatient.DataSource = lst;
                lst.Insert(0, new EntityPatientAdmit() { AdmitId = 0, PatientFirstName = "--Select--" });
                ddlPatient.DataValueField = "AdmitId";
                ddlPatient.DataTextField = "PatientFirstName";
                ddlPatient.DataBind();
                tblNursingManagement objPresc = MobjClaim.GetPrescriptionInfo(Convert.ToInt32(Session["PrescriptionId"]));
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
                    List<EntityNursingManagement> lst = MobjClaim.GetInsurance(txtSearch.Text);
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
                BindDailyNursingManagement();
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
                ListConverter.ToDataTable((List<EntityNursingManagement>)Session["PrescriptDetails"]);
                Session["Details"] = ListConverter.ToDataTable((List<EntityNursingManagement>)Session["PrescriptDetails"]);
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
            ddlNurseName.SelectedIndex = 0;
            ddlWardCategory.SelectedIndex = 0;
            txtDepartment.Text = string.Empty;
            txtTreatmentDate.Enabled = false;
            txtTreatmentDate.Text = DateTime.UtcNow.Date.ToString("MM/dd/yyyy");
            BtnSave.Visible = true;
            btnUpdate.Visible = false;
            btnAdd.Visible = true;
            btnUpdatecharge.Visible = false;
            List<EntityNursingManagementDetails> lst = new List<EntityNursingManagementDetails>();
            dgvChargeDetails.DataSource = lst;
            dgvChargeDetails.DataBind();
            Session["Charges"] = lst;
            //MultiView1.SetActiveView(View2);
            MultiView1.SetActiveView(View2);
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton imgEdit = (ImageButton)sender;
                GridViewRow cnt = (GridViewRow)imgEdit.NamingContainer;
                Session["Sr_No"] = cnt.Cells[0].Text;
                int ID_Issue = Convert.ToInt32(Session["Sr_No"]);
                if (ID_Issue > 0)
                {
                    Session["ReportType"] = "NursingFollowupSheet";
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
                dgvClaim.DataSource = (List<EntityNursingManagement>)Session["PrescriptDetails"];
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