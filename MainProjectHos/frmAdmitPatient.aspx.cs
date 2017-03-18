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
    public partial class frmAdmitPatient : System.Web.UI.Page
    {
        ProductBLL mobjProductBLL = new ProductBLL();
        OPDPatientMasterBLL mobjPatientMasterBLL = new OPDPatientMasterBLL();
        PatientMasterBLL mobjPatient = new PatientMasterBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] != null)
            {
                EntityLogin entLogin = (EntityLogin)Session["user"];

                List<EntityFormMaster> entFormAcc = (List<EntityFormMaster>)Session["AccessibleForm"];
                int Access = (from tbl in entFormAcc
                              where tbl.FormName.Contains("frmAdmitPatient")
                              select tbl).Count();

                if (Access > 0)
                {
                    if (!Page.IsPostBack)
                    {
                        GetPatientList();
                        GetDeptCategory();
                        ddlCompName.Enabled = false;
                        ddlInsurance.Enabled = false;
                        GetOPDPatientList();
                        GetInsuranceCompanies();
                        GetCompanies();
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

        protected void txtNew_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtNew.Text))
                {
                    GetPatientListsearch(txtNew.Text);
                }
                else
                {
                    GetPatientList();
                }

            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void ddlPatientName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPatientName.SelectedIndex > 0)
            {
                tblPatientMaster obj = new PatientMasterBLL().GetPatientbyId(Convert.ToInt32(ddlPatientName.SelectedValue));
                if (Convert.ToBoolean(obj.IsDeath))
                {
                    lblMsg.Text = "This patient can not be re-admitted. This patient is already passed away.";
                    ddlPatientName.SelectedIndex = 0;
                }
                else
                {
                    if (obj.BirthDate != null)
                    {
                        lblDOB.Text = obj.BirthDate.Value.ToShortDateString();
                        txtAge.Text = Convert.ToString(DateTime.Now.Date.Year - obj.BirthDate.Value.Year);
                        ddlAge.Text = obj.AgeIn;
                        txtWeight.Text = Convert.ToString(obj.Weight);
                    }
                    else
                    {
                        txtAge.Text = Convert.ToString(obj.Age);
                        ddlAge.Text = obj.AgeIn;
                        txtWeight.Text = Convert.ToString(obj.Weight);
                        //lblDOB.Text = obj.BirthDate.Value.ToShortDateString();
                    }
                }
            }
        }

        protected void OPD_CheckedChanged(object sender, EventArgs e)
        {
            lblDiagnosis.Visible = false;
            txtDignosys.Visible = false;
            lblIPDNo.Text = "OPD No :";
            DataTable ldt1 = new DataTable();
            ldt1 = mobjPatient.GetNewOPDNumber();
            if (ldt1.Rows.Count > 0 && ldt1 != null)
            {
                txtIPDNo.Text = ldt1.Rows[0]["PatientOPDNo"].ToString();
            }
        }

        protected void IPD_CheckedChanged(object sender, EventArgs e)
        {
            lblDiagnosis.Visible = true;
            txtDignosys.Visible = true;
            lblIPDNo.Text = "IPD No :";
            DataTable ldt1 = new DataTable();
            ldt1 = mobjPatient.GetNewIPDNumber();
            if (ldt1.Rows.Count > 0 && ldt1 != null)
            {
                txtIPDNo.Text = ldt1.Rows[0]["PatientIPDNo"].ToString();
            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton imgEdit = (ImageButton)sender;
                GridViewRow row = (GridViewRow)imgEdit.NamingContainer;
                Session["AdmitId"] = Convert.ToInt32(dgvPatientList.DataKeys[row.RowIndex].Value);
                Session["ReportType"] = "OPDPaper";
                Response.Redirect("~/PathalogyReport/PathologyReport.aspx", false);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                txtSearch.Text = string.Empty;
                GetOPDPatientList();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void ImageShift_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton imgEdit = (ImageButton)sender;
                GridViewRow cnt = (GridViewRow)imgEdit.NamingContainer;
                string PatientType = cnt.Cells[7].Text;
                if (PatientType.Equals("OPD", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (mobjPatient.ShiftToIPD(Convert.ToInt32(dgvPatientList.DataKeys[cnt.RowIndex].Value)) > 0)
                    {
                        GetOPDPatientList();
                    }
                }
                else
                {
                    lblMessage.Text = "Patient Type Already IPD";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                List<sp_GetAllPatientListForRegisteredResult> ldtRequisition = mobjPatientMasterBLL.GetPatientListForRegistered(txtSearch.Text);
                if (ldtRequisition.Count > 0 && ldtRequisition != null)
                {
                    dgvPatientList.DataSource = ldtRequisition;
                    dgvPatientList.DataBind();
                    Session["PatientList"] = ldtRequisition;
                    int lintRowcount = ldtRequisition.Count;
                    lblRowCount.Text = "<b>Total Records:</b> " + lintRowcount.ToString();
                }
                else
                {
                    dgvPatientList.DataSource = new List<sp_GetAllPatientListForRegisteredResult>();
                    dgvPatientList.DataBind();
                    Session["PatientList"] = new List<sp_GetAllPatientListForRegisteredResult>();
                    lblRowCount.Text = "<b>Total Records:</b> " + 0;
                }
                txtSearch.Text = string.Empty;
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private void GetPatientList()
        {
            try
            {
                List<EntityPatientMaster> ldtRequisition = mobjPatientMasterBLL.GetAllPatients();
                ldtRequisition.Insert(0, new EntityPatientMaster() { PatientId = 0, FullName = "----Select----" });
                ddlPatientName.DataSource = ldtRequisition;
                ddlPatientName.DataTextField = "FullName";
                ddlPatientName.DataValueField = "PatientId";
                ddlPatientName.DataBind();
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        private void GetPatientListsearch(string Prefix)
        {
            try
            {
                List<EntityPatientMaster> ldtRequisition = mobjPatientMasterBLL.GetAllPatientssearch(Prefix);
                ldtRequisition.Insert(0, new EntityPatientMaster() { PatientId = 0, FullName = "----Select----" });
                ddlPatientName.DataSource = ldtRequisition;
                ddlPatientName.DataTextField = "FullName";
                ddlPatientName.DataValueField = "PatientId";
                ddlPatientName.DataBind();
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
                int linReligionId = Convert.ToInt32(ddlDeptCategory.SelectedValue);
                DataTable ldt = new DataTable();
                ldt = mobjPatient.GetDeptDoctors(linReligionId);
                if (ldt.Rows.Count > 0 && ldt != null)
                {
                    ddlDeptDoctor.DataSource = ldt;
                    ddlDeptDoctor.DataValueField = "DocAllocId";
                    ddlDeptDoctor.DataTextField = "EmpName";
                    ddlDeptDoctor.DataBind();

                    FillDeptDoctorCast();
                    ddlDeptDoctor.Enabled = true;
                }
                else
                {
                    ddlDeptDoctor.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        private void FillDeptDoctorCast()
        {
            try
            {
                ListItem li = new ListItem();
                li.Text = "--Select Dept.Doctor--";
                li.Value = "0";
                ddlDeptDoctor.Items.Insert(0, li);
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        public void GetDeptCategory()
        {
            try
            {
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

        public void GetInsuranceCompanies()
        {
            try
            {
                DataTable ldt = new DataTable();
                ldt = mobjPatient.GetInsuranceCompanies();
                ddlInsurance.DataSource = ldt;
                ddlInsurance.DataValueField = "InsuranceCode";
                ddlInsurance.DataTextField = "InsuranceDesc";
                ddlInsurance.DataBind();
                ListItem li = new ListItem();
                li.Text = "--Select Company--";
                li.Value = "0";
                ddlInsurance.Items.Insert(0, li);
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }

        }

        protected void ChkInsurance_CheckedChanged(object sender, EventArgs e)
        {
            ddlInsurance.Enabled = true;
        }

        protected void chkCom_CheckedChanged(object sender, EventArgs e)
        {
            ddlCompName.Enabled = true;
        }

        public void GetCompanies()
        {
            try
            {
                DataTable ldt = new DataTable();
                ldt = mobjPatient.GetCompanies();
                ddlCompName.DataSource = ldt;
                ddlCompName.DataValueField = "CompanyCode";
                ddlCompName.DataTextField = "CompanyName";
                ddlCompName.DataBind();
                ListItem li = new ListItem();
                li.Text = "--Select Company--";
                li.Value = "0";
                ddlCompName.Items.Insert(0, li);
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }

        }

        protected void BtnAddNewProduct_Click(object sender, EventArgs e)
        {
            Clear();
            txtAdmitDate.Enabled = false;
            txtNew.Text = string.Empty;
            txtAge.Text = string.Empty;
            txtWeight.Text = string.Empty;
            txtAdmitDate.Text = DateTime.UtcNow.Date.ToString("MM/dd/yyyy");
            //AdmissionTimeSelector.Enabled = true;
            BtnSave.Visible = true;
            btnUpdate.Visible = false;
            MultiView1.SetActiveView(View2);
        }

        private void Clear()
        {
            ddlPatientName.SelectedIndex = 0;
            ddlDeptCategory.SelectedIndex = 0;
            ddlAge.SelectedIndex = 0;
            txtDignosys.Text = string.Empty;
            ddlCompName.SelectedIndex = 0;
            ddlInsurance.SelectedIndex = 0;
            txtAdmitDate.Text = string.Empty;
            txtIPDNo.Text = string.Empty;
            txtAge.Text = string.Empty;
            txtWeight.Text = string.Empty;
            txtAdmitDate.Enabled = true;
            //AdmissionTimeSelector.Enabled = true;
            lblMsg.Text = string.Empty;
        }

        public void GetOPDPatientList()
        {
            try
            {
                List<sp_GetAllPatientListForRegisteredResult> ldtRequisition = mobjPatientMasterBLL.GetPatientListForRegistered();
                if (ldtRequisition.Count > 0 && ldtRequisition != null)
                {
                    dgvPatientList.DataSource = ldtRequisition;
                    dgvPatientList.DataBind();
                    Session["PatientList"] = ldtRequisition;
                    int lintRowcount = ldtRequisition.Count;
                    lblRowCount.Text = "<b>Total Records:</b> " + lintRowcount.ToString();
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }

        }


        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            int lintCnt = 0;
            try
            {
                EntityPatientAdmit entAdmit = new EntityPatientAdmit();
                entAdmit.AdmitId = Convert.ToInt32(Session["AdmitId"]);
                entAdmit.PatientId = Convert.ToInt32(ddlPatientName.SelectedValue);
                entAdmit.DeptCategory = Convert.ToInt32(ddlDeptCategory.SelectedValue);
                entAdmit.DeptDoctorId = Convert.ToInt32(ddlDeptDoctor.SelectedValue);
                TimeSpan objTime = new TimeSpan(0, 0, 0);
                entAdmit.AdmitDate = Convert.ToDateTime(txtAdmitDate.Text).Add(objTime);
                entAdmit.PatientAdmitTime = Convert.ToString(objTime);

                if (rbtnIPD.Checked)
                {
                    entAdmit.IsIPD = true;
                    entAdmit.PatientType = "IPD";
                    entAdmit.IsOPD = false;
                    entAdmit.IPDNo = Convert.ToString(txtIPDNo.Text);
                    entAdmit.OPDNo = "";
                }
                if (rbtnOPD.Checked)
                {
                    entAdmit.IsIPD = false;
                    entAdmit.IsOPD = true;
                    entAdmit.PatientType = "OPD";
                    entAdmit.IPDNo = "";
                    entAdmit.OPDNo = Convert.ToString(txtIPDNo.Text);
                }
                if (!string.IsNullOrEmpty(txtAge.Text))
                {
                    entAdmit.Age = Convert.ToInt32(txtAge.Text);
                }
                entAdmit.AgeIn = ddlAge.SelectedItem.Text;
                entAdmit.Weight = Convert.ToString(txtWeight.Text);
                entAdmit.IsCompany = chkCom.Checked ? true : false;
                entAdmit.IsInsured = ChkInsurance.Checked ? true : false;
                if (chkCom.Checked)
                {
                    entAdmit.CompanyId = Convert.ToInt32(ddlCompName.SelectedValue);
                    entAdmit.CompanyName = Convert.ToString(ddlCompName.SelectedItem.Text);
                }
                else
                {
                    entAdmit.CompanyId = 0;
                    entAdmit.CompanyName = string.Empty;
                }
                if (ChkInsurance.Checked)
                {
                    entAdmit.InsuranceComId = Convert.ToInt32(ddlInsurance.SelectedValue);
                    entAdmit.InsuName = Convert.ToString(ddlCompName.SelectedItem.Text);
                }
                else
                {
                    entAdmit.InsuranceComId = 0;
                    entAdmit.InsuName = string.Empty;
                }
                entAdmit.Dignosys = txtDignosys.Text;

                lintCnt = mobjPatient.UpdatePatient(entAdmit);

                if (lintCnt > 0)
                {
                    lblMessage.Text = "Record Updated Successfully";
                }
                else
                {
                    lblMessage.Text = "Record Not Updated";
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
            GetOPDPatientList();
            MultiView1.SetActiveView(View1);
        }



        protected void dgvPatientList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvPatientList.PageIndex = e.NewPageIndex;
        }


        protected void dgvPatientList_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dgvPatientList.DataSource = (List<sp_GetAllPatientListForRegisteredResult>)Session["PatientList"];
                dgvPatientList.DataBind();
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                txtAdmitDate.Enabled = false;
                GetDeptCategory();
                //AdmissionTimeSelector.Enabled = false;
                ImageButton imgEdit = (ImageButton)sender;
                GridViewRow cnt = (GridViewRow)imgEdit.NamingContainer;
                Session["AdmitId"] = Convert.ToInt32(dgvPatientList.DataKeys[cnt.RowIndex].Value);
                ListItem item = ddlPatientName.Items.FindByText(Convert.ToString(cnt.Cells[1].Text));
                ddlPatientName.SelectedValue = item.Value;
                txtAdmitDate.Text = string.Format("{0:MM/dd/yyyy}", Convert.ToDateTime(cnt.Cells[2].Text));
                txtAge.Text = cnt.Cells[3].Text;
                ddlAge.Text = Convert.ToString(cnt.Cells[4].Text);
                string PatientType = cnt.Cells[7].Text;
                if (PatientType.ToUpper().Equals("OPD"))
                {
                    rbtnOPD.Checked = true;
                    rbtnIPD.Checked = false;
                    lblDiagnosis.Visible = false;
                    txtDignosys.Visible = false;
                    lblIPDNo.Text = "OPD No :";
                    txtIPDNo.Text = cnt.Cells[12].Text;
                }
                if (PatientType.ToUpper().Equals("IPD"))
                {
                    rbtnOPD.Checked = false;
                    rbtnIPD.Checked = true;
                    lblDiagnosis.Visible = true;
                    txtDignosys.Visible = true;
                    lblIPDNo.Text = "IPD No :";
                    txtIPDNo.Text = cnt.Cells[11].Text;
                }
                if (cnt.Cells[8].Text != "&nbsp;")
                {
                    txtDignosys.Text = cnt.Cells[8].Text;
                }
                else
                {
                    txtDignosys.Text = string.Empty;
                }
                ListItem item1 = ddlDeptCategory.Items.FindByText(Convert.ToString(cnt.Cells[9].Text));
                ddlDeptCategory.SelectedValue = item1.Value;

                DataTable ldt = new DataTable();
                ldt = mobjPatient.GetDeptDoctors(Convert.ToInt32(item1.Value));
                if (ldt.Rows.Count > 0 && ldt != null)
                {
                    ddlDeptDoctor.DataSource = ldt;
                    ddlDeptDoctor.DataValueField = "DocAllocId";
                    ddlDeptDoctor.DataTextField = "EmpName";
                    ddlDeptDoctor.DataBind();
                    //ListItem item2 = ddlDeptDoctor.Items.FindByText(Convert.ToString(cnt.Cells[10].Text));
                    //ddlDeptDoctor.SelectedValue = item2.Value;
                }

                if (!string.IsNullOrEmpty(cnt.Cells[5].Text))
                {
                    if (cnt.Cells[5].Text.ToUpper().Equals("&nbsp;".ToUpper()) == false)
                    {
                        chkCom.Checked = true;
                        ListItem itemCompany = ddlCompName.Items.FindByText(Convert.ToString(cnt.Cells[5].Text));
                        ddlCompName.SelectedValue = itemCompany.Value;
                    }
                    else
                    {
                        chkCom.Checked = false;
                    }
                }
                else
                {
                    chkCom.Checked = false;
                }
                if (!string.IsNullOrEmpty(cnt.Cells[6].Text))
                {
                    if (cnt.Cells[6].Text.ToUpper().Equals("&nbsp;".ToUpper()) == false)
                    {
                        ChkInsurance.Checked = true;
                        ListItem itemCompany = ddlInsurance.Items.FindByText(Convert.ToString(cnt.Cells[6].Text));
                        ddlInsurance.SelectedValue = itemCompany.Value;
                    }
                    else
                    {
                        ChkInsurance.Checked = false;
                    }
                }
                else
                {
                    ChkInsurance.Checked = false;
                }
                BtnSave.Visible = false;
                btnUpdate.Visible = true;
                MultiView1.SetActiveView(View2);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void BtnClose_Click(object sender, EventArgs e)
        {
            try
            {
                Clear();
                MultiView1.SetActiveView(View1);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                EntityPatientAdmit entAdmit = new EntityPatientAdmit();
                entAdmit.PatientId = Convert.ToInt32(ddlPatientName.SelectedValue);
                TimeSpan objTime = new TimeSpan(0, 0, 0);
                entAdmit.AdmitDate = Convert.ToDateTime(txtAdmitDate.Text).Add(objTime);
                entAdmit.PatientAdmitTime = Convert.ToString(objTime);
                entAdmit.Dignosys = txtDignosys.Text;

                entAdmit.DeptCategory = Convert.ToInt32(ddlDeptCategory.SelectedValue);
                entAdmit.DeptDoctorId = Convert.ToInt32(ddlDeptDoctor.SelectedValue);

                if (rbtnIPD.Checked)
                {
                    DataTable ldt1 = new DataTable();
                    ldt1 = mobjPatient.GetNewIPDNumber();
                    if (ldt1.Rows.Count > 0 && ldt1 != null && (ldt1.Rows[0]["PatientIPDNo"].ToString() != string.Empty))
                    {
                        txtIPDNo.Text = ldt1.Rows[0]["PatientIPDNo"].ToString();
                    }
                    entAdmit.IsIPD = true;
                    entAdmit.PatientType = "IPD";
                    entAdmit.IsOPD = false;
                    entAdmit.IPDNo = Convert.ToString(txtIPDNo.Text);
                    entAdmit.OPDNo = "";
                }
                if (rbtnOPD.Checked)
                {
                    DataTable ldt1 = new DataTable();
                    ldt1 = mobjPatient.GetNewOPDNumber();
                    if (ldt1.Rows.Count > 0 && ldt1 != null && (ldt1.Rows[0]["PatientOPDNo"].ToString() != string.Empty))
                    {
                        txtIPDNo.Text = ldt1.Rows[0]["PatientOPDNo"].ToString();
                    }
                    entAdmit.IsIPD = false;
                    entAdmit.IsOPD = true;
                    entAdmit.PatientType = "OPD";
                    entAdmit.OPDNo = Convert.ToString(txtIPDNo.Text);
                    entAdmit.IPDNo = "";
                }
                if (!string.IsNullOrEmpty(txtAge.Text))
                {
                    entAdmit.Age = Convert.ToInt32(txtAge.Text);
                }
                entAdmit.AgeIn = ddlAge.SelectedItem.Text;
                entAdmit.Weight = Convert.ToString(txtWeight.Text);
                entAdmit.IsCompany = chkCom.Checked ? true : false;
                entAdmit.IsInsured = ChkInsurance.Checked ? true : false;
                if (chkCom.Checked)
                {
                    entAdmit.CompanyId = Convert.ToInt32(ddlCompName.SelectedValue);
                    entAdmit.CompanyName = Convert.ToString(ddlCompName.SelectedItem.Text);
                }
                else
                {
                    entAdmit.CompanyId = 0;
                    entAdmit.CompanyName = string.Empty;
                }
                if (ChkInsurance.Checked)
                {
                    entAdmit.InsuranceComId = Convert.ToInt32(ddlInsurance.SelectedValue);
                    entAdmit.InsuName = Convert.ToString(ddlInsurance.SelectedItem.Text);
                }
                else
                {
                    entAdmit.InsuranceComId = 0;
                    entAdmit.InsuName = string.Empty;
                }
                bool Status = mobjPatient.CheckPatientExistforSameDate(entAdmit);
                if (!Status)
                {
                    int i = mobjPatient.Save(entAdmit);
                    if (i > 0)
                    {
                        Clear();
                        lblMessage.Text = "Record Save Successfully.";
                    }
                    else
                    {
                        lblMessage.Text = "Record Save Successfully.";
                    }
                }
                else
                {
                    lblMessage.Text = "This Patient Was Not Discharged.";
                }
                GetOPDPatientList();
                MultiView1.SetActiveView(View1);
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }
    }
}