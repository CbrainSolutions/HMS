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

namespace MainProjectHos.PatientStatus
{
    public partial class frmDeathCertificate : System.Web.UI.Page
    {
        DeathCertificateBLL mobjDeptBLL = new DeathCertificateBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] != null)
            {
                EntityLogin entLogin = (EntityLogin)Session["user"];

                List<EntityFormMaster> entFormAcc = (List<EntityFormMaster>)Session["AccessibleForm"];
                int Access = (from tbl in entFormAcc
                              where tbl.FormName.Contains("frmDeathCertificate")
                              select tbl).Count();

                if (Access > 0)
                {
                    if (!Page.IsPostBack)
                    {
                        Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                        GetDeathDetails();
                        GetPatientList();
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

        private void GetDeathDetails()
        {
            try
            {
                List<EntityDeathCertificate> ldtDeath = mobjDeptBLL.GetAllDeathDetails();
                if (ldtDeath.Count > 0)
                {
                    dgvShift.DataSource = ldtDeath;
                    dgvShift.DataBind();
                    Session["DepartmentDetail"] = ldtDeath;
                    Session["StartDeathDetails"] = ListConverter.ToDataTable(ldtDeath);
                    int lintRowcount = ldtDeath.Count;
                    lblRowCount.Text = "<b>Total Records:</b> " + lintRowcount.ToString();
                }
                else
                {
                    dgvShift.DataSource = ldtDeath;
                    dgvShift.DataBind();
                    Session["DepartmentDetail"] = ldtDeath;
                    Session["StartDeathDetails"] = ListConverter.ToDataTable(ldtDeath);
                    int lintRowcount = ldtDeath.Count;
                    lblRowCount.Text = "<b>Total Records:</b> " + lintRowcount.ToString();
                }
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
                GetDeathDetails();
                MultiView1.SetActiveView(View1);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
        }

        private void GetPatientList()
        {
            try
            {
                List<EntityDeathCertificate> ldtRequisition = mobjDeptBLL.GetAllPatients();
                ldtRequisition.Insert(0, new EntityDeathCertificate() { PatientAdmitId = 0, FullName = "----Select-----" });
                ddlPatientName.DataSource = ldtRequisition;
                ddlPatientName.DataTextField = "FullName";
                ddlPatientName.DataValueField = "PatientAdmitId";
                ddlPatientName.DataBind();
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }

        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            ViewState["update"] = Session["update"];
        }

        protected void BtnAddNewDept_Click(object sender, EventArgs e)
        {
            Clear();
            btnUpdate.Visible = false;
            BtnSave.Visible = true;
            MultiView1.SetActiveView(View2);
            //this.programmaticModalPopup.Show();
        }

        private void Clear()
        {
            //StartTimeSelector.ReadOnly = false;
            //StartTimeSelector.Hour = DateTime.Now.Hour;
            //StartTimeSelector.Minute = DateTime.Now.Minute;
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int lintcnt = 0;
                EntityDeathCertificate entDept = new EntityDeathCertificate();

                if (ddlPatientName.SelectedIndex == 0)
                {
                    lblMsg.Text = "Please Patient Name";
                    return;
                }
                else
                {
                    if (string.IsNullOrEmpty(txtDate.Text))
                    {
                        lblMsg.Text = "Please Select Date";
                        return;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(txtReason.Text))
                        {
                            lblMsg.Text = "Please Enter The Reason Of Death";
                            return;
                        }
                        else
                        {
                            entDept.PatientAdmitId = Convert.ToInt32(ddlPatientName.SelectedValue);
                            entDept.Death_Date = Convert.ToDateTime(txtDate.Text);
                            TimeSpan objTime = new TimeSpan(0, 0, 0);
                            DateTime dt = DateTime.Now.Date;
                            dt = dt.Add(objTime);
                            entDept.Death_Time = dt;

                            entDept.Death_Reason = txtReason.Text;
                            lintcnt = mobjDeptBLL.InsertDeathRecord(entDept);
                            if (lintcnt > 0)
                            {
                                GetDeathDetails();
                                lblMessage.Text = "Record Inserted Successfully....";
                                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                            }
                            else
                            {
                                lblMessage.Text = "Record Not Inserted...";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
            MultiView1.SetActiveView(View1);
        }


        protected void dgvDepartment_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private void FillControls(DataTable ldt)
        {
            //txtEditDeptDesc.Text = ldt.Rows[0]["DeptDesc"].ToString();
        }

        protected void BtnEdit_Click(object sender, EventArgs e)
        {

        }

        protected void chkDelete_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chk.NamingContainer;
            if (chk.Checked)
            {
                LinkButton DeptCode = (LinkButton)row.FindControl("lnkDeptCode");
                Session["DeptCode"] = DeptCode.Text;
                //lblMessage.Text = string.Empty;
            }
            else
            {
                Session["DeptCode"] = string.Empty;
            }
        }
        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow drv in dgvShift.Rows)
            {
                CheckBox chkDelete = (CheckBox)drv.FindControl("chkDelete");
                if (chkDelete.Checked)
                {
                    //this.modalpopupDelete.Show();
                }
            }
        }

        protected void dgvDepartment_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dgvShift.DataSource = (List<sp_GetAllBedAllocResult>)Session["DepartmentDetail"];
                dgvShift.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
        protected void dgvDepartment_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvShift.PageIndex = e.NewPageIndex;
        }
        protected void dgvDepartment_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        protected void dgvDepartment_DataBound(object sender, EventArgs e)
        {
            int lintCurrentPage = dgvShift.PageIndex + 1;
            lblPageCount.Text = "<b>Page</b> " + lintCurrentPage.ToString() + "<b> of </b>" + dgvShift.PageCount.ToString();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSearch.Text))
                {
                    DeathDetails(txtSearch.Text);
                }
                else
                {
                    lblMessage.Text = "Please Fill Search Text.";
                    txtSearch.Focus();
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private void DeathDetails(string Prefix)
        {
            try
            {
                List<EntityDeathCertificate> lst = mobjDeptBLL.SelectDeathDetails(Prefix);
                if (lst != null)
                {
                    dgvShift.DataSource = lst;
                    dgvShift.DataBind();
                    Session["Death_Details"] = ListConverter.ToDataTable(lst);
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtSearch.Text = string.Empty;
            GetDeathDetails();
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSearch.Text))
                {
                    Session["Details"] = Session["StartDeathDetails"];
                    Response.Redirect("~/ExcelReport/MonthwiseSalExcel.aspx", false);
                }
                else
                {
                    Session["Details"] = Session["Death_Details"];
                    Response.Redirect("~/ExcelReport/MonthwiseSalExcel.aspx", false);
                }

            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            ImageButton imgEdit = (ImageButton)sender;
            GridViewRow row = (GridViewRow)imgEdit.NamingContainer;
            Session["Death_Id"] = Convert.ToInt32(dgvShift.DataKeys[row.RowIndex].Value);
            Session["ReportType"] = "Death";
            Response.Redirect("~/PathalogyReport/PathologyReport.aspx", false);
        }
    }
}