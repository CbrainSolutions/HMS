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
    public partial class frmBirthCertificate : System.Web.UI.Page
    {
        BirthCertificateBLL mobjBirthBLL = new BirthCertificateBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] != null)
            {
                EntityLogin entLogin = (EntityLogin)Session["user"];

                List<EntityFormMaster> entFormAcc = (List<EntityFormMaster>)Session["AccessibleForm"];
                int Access = (from tbl in entFormAcc
                              where tbl.FormName.Contains("frmBirthCertificate")
                              select tbl).Count();

                if (Access > 0)
                {
                    if (!Page.IsPostBack)
                    {
                        Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                        GetBirthDetails();
                        GetPatientList();
                        GetGenderDesc();
                        MultiView1.SetActiveView(View1);
                    }
                }
                else
                {
                    Response.Redirect("~/frmAdminMain.aspx", false);
                }
            }
            else
            {
                Response.Redirect("~/frmlogin.aspx", false);
            }
        }

        private void GetBirthDetails()
        {
            try
            {
                List<EntityBirthCertificate> ldtBirth = mobjBirthBLL.GetAllBirthDetails();
                if (ldtBirth.Count > 0)
                {
                    dgvShift.DataSource = ldtBirth;
                    dgvShift.DataBind();
                    Session["DepartmentDetail"] = ldtBirth;
                    Session["StartBirthDetails"] = ListConverter.ToDataTable(ldtBirth);
                    int lintRowcount = ldtBirth.Count;
                    lblRowCount.Text = "<b>Total Records:</b> " + lintRowcount.ToString();
                    pnlShow.Style.Add(HtmlTextWriterStyle.Display, "");
                    hdnPanel.Value = "";
                }
                else
                {
                    //pnlShow.Style.Add(HtmlTextWriterStyle.Display, "none");
                    hdnPanel.Value = "none";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private void GetGenderDesc()
        {
            try
            {
                List<EntityBirthCertificate> ldtRequisition = mobjBirthBLL.GetAllGender();
                ldtRequisition.Insert(0, new EntityBirthCertificate() { GenderID = 0, GenderDesc = "----Select-----" });
                ddlGender.DataSource = ldtRequisition;
                ddlGender.DataTextField = "GenderDesc";
                ddlGender.DataValueField = "GenderID";
                ddlGender.DataBind();
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }

        }

        protected void BtnClose_Click(object sender, EventArgs e)
        {
            try
            {
                GetBirthDetails();
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
                List<EntityBirthCertificate> ldtRequisition = mobjBirthBLL.GetAllPatients();
                ldtRequisition.Insert(0, new EntityBirthCertificate() { PatientAdmitID = 0, FullName = "----Select-----" });
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
                EntityBirthCertificate entDept = new EntityBirthCertificate();

                if (ddlPatientName.SelectedIndex == 0)
                {
                    lblMsg.Text = "Please Patient Name";
                    return;
                }
                else
                {
                    entDept.PatientAdmitID = Convert.ToInt32(ddlPatientName.SelectedValue);
                    entDept.GrandFatherName = txtGrandFather.Text;
                    entDept.ChildName = txtChildName.Text;
                    entDept.GenderID = Convert.ToInt32(ddlGender.SelectedValue);
                    entDept.BirthDate = Convert.ToDateTime(txtBirthDate.Text);
                    TimeSpan objTime = new TimeSpan(0, 0, 0);
                    DateTime dt = DateTime.Now.Date;
                    dt = dt.Add(objTime);
                    entDept.BirthTime = dt;

                    entDept.Height = Convert.ToDecimal(txtHeight.Text);
                    entDept.Weight = Convert.ToDecimal(txtWeight.Text);

                    lintcnt = mobjBirthBLL.InsertBirthRecord(entDept);
                    if (lintcnt > 0)
                    {
                        GetBirthDetails();
                        lblMessage.Text = "Record Inserted Successfully....";
                        Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                    }
                    else
                    {
                        lblMessage.Text = "Record Not Inserted...";
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
                    BirthDetails(txtSearch.Text);
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

        private void BirthDetails(string Prefix)
        {
            List<EntityBirthCertificate> lst = mobjBirthBLL.SelectBirthDetails(Prefix);
            try
            {
                if (lst != null)
                {
                    dgvShift.DataSource = lst;
                    dgvShift.DataBind();
                    Session["Birth_Details"] = ListConverter.ToDataTable(lst);
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
            GetBirthDetails();
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSearch.Text))
                {
                    Session["Details"] = Session["StartBirthDetails"];
                    Response.Redirect("~/ExcelReport/MonthwiseSalExcel.aspx", false);
                }
                else
                {
                    Session["Details"] = Session["Birth_Details"];
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
            Session["Birth_ID"] = Convert.ToInt32(dgvShift.DataKeys[row.RowIndex].Value);
            Session["ReportType"] = "Birth";
            Response.Redirect("~/PathalogyReport/PathologyReport.aspx", false);
        }
    }
}