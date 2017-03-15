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
    public partial class frmDepartmentMaster : System.Web.UI.Page
    {
        DepartmentBLL mobjDeptBLL = new DepartmentBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] != null)
            {
                EntityLogin entLogin = (EntityLogin)Session["user"];

                List<EntityFormMaster> entFormAcc = (List<EntityFormMaster>)Session["AccessibleForm"];
                int Access = (from tbl in entFormAcc
                              where tbl.FormName.Contains("frmDepartmentMaster")
                              select tbl).Count();

                if (Access > 0)
                {
                    if (!Page.IsPostBack)
                    {
                        GetDepartments();
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

        protected void BtnClose_Click(object sender, EventArgs e)
        {
            try
            {
                txtDeptCode.Text = String.Empty;
                txtDeptDesc.Text = String.Empty;
                GetDepartments();
                MultiView1.SetActiveView(View1);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton imgEdit = (ImageButton)sender;
                GridViewRow row = (GridViewRow)imgEdit.NamingContainer;
                txtDeptCode.Text = Convert.ToString(row.Cells[0].Text);
                txtDeptDesc.Text = Convert.ToString(row.Cells[1].Text);
                txtDeptCode.ReadOnly = true;
                GetDepartments();
                btnUpdate.Visible = true;
                BtnSave.Visible = false;
                MultiView1.SetActiveView(View2);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            ViewState["update"] = Session["update"];
        }
        protected void BtnAddNewDept_Click(object sender, EventArgs e)
        {
            DataTable ldt = new DataTable();
            txtDeptDesc.Text = string.Empty;
            ldt = mobjDeptBLL.GetNewDeptCode();
            txtDeptCode.Text = ldt.Rows[0]["DeptCode"].ToString();
            btnUpdate.Visible = false;
            BtnSave.Visible = true;
            MultiView1.SetActiveView(View2);
        }

        private void GetDepartments()
        {
            DataTable ldtDept = new DataTable();
            ldtDept = mobjDeptBLL.GetAllDepartments();

            if (ldtDept.Rows.Count > 0 && ldtDept != null)
            {
                dgvDepartment.DataSource = ldtDept;
                dgvDepartment.DataBind();
                Session["DepartmentDetail"] = ldtDept;
                int lintRowcount = ldtDept.Rows.Count;
                lblRowCount.Text = "<b>Total Records:</b> " + lintRowcount.ToString();
                pnlShow.Style.Add(HtmlTextWriterStyle.Display, "");
                hdnPanel.Value = "";
            }
            else
            {
                hdnPanel.Value = "none";
            }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            int lintcnt = 0;
            EntityDepartment entDept = new EntityDepartment();
            if (string.IsNullOrEmpty(txtDeptCode.Text.Trim()))
            {
                lblMsg.Text = "Please Enter Department Code";
            }
            else
            {
                if (string.IsNullOrEmpty(txtDeptDesc.Text.Trim()))
                {
                    lblMsg.Text = "Please Enter Department Description";
                }
                else
                {
                    entDept.DeptCode = txtDeptCode.Text.Trim();
                    entDept.DeptDesc = txtDeptDesc.Text.Trim();
                    entDept.EntryBy = SessionWrapper.UserName;
                    if (!Commons.IsRecordExists("tblDepartment", "DeptDesc", entDept.DeptDesc))
                    {
                        lintcnt = mobjDeptBLL.InsertDepartment(entDept);

                        if (lintcnt > 0)
                        {
                            GetDepartments();
                            lblMessage.Text = "Record Inserted Successfully....";
                            Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                        }
                        else
                        {
                            lblMessage.Text = "Record Not Inserted...";
                        }
                    }
                    else
                    {
                        lblMessage.Text = "Record Already Exist....";
                    }
                }
            }
            MultiView1.SetActiveView(View1);
        }

        protected void dgvDepartment_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                DataTable ldt = new DataTable();
                if (e.CommandName == "EditDept")
                {
                    int linIndex = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                    LinkButton lnkDeptCode = (LinkButton)gvr.FindControl("lnkDeptCode");
                    string lstrDeptCode = lnkDeptCode.Text;
                    ldt = mobjDeptBLL.GetDepartmentForEdit(lstrDeptCode);
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
                EntityDepartment entDept = new EntityDepartment();
                entDept.DeptCode = txtDeptCode.Text;
                entDept.DeptDesc = txtDeptDesc.Text;
                entDept.ChangeBy = SessionWrapper.UserName;
                if (mobjDeptBLL.GetDepartMent(entDept) != null)
                {
                    lintCnt = mobjDeptBLL.UpdateDepartment(entDept);
                    if (lintCnt > 0)
                    {
                        GetDepartments();
                        lblMessage.Text = "Record Updated Successfully";
                    }
                    else
                    {
                        lblMessage.Text = "Record Not Updated";
                    }
                }
                else if (mobjDeptBLL.GetDepartMentByName(entDept) == null)
                {
                    lintCnt = mobjDeptBLL.UpdateDepartment(entDept);
                    if (lintCnt > 0)
                    {
                        GetDepartments();
                        lblMessage.Text = "Record Updated Successfully";
                    }
                    else
                    {
                        lblMessage.Text = "Record Not Updated";
                    }
                }
                else
                {
                    lblMessage.Text = "Record already exist";
                }


                MultiView1.SetActiveView(View1);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void chkDelete_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chk.NamingContainer;
            if (chk.Checked)
            {
                LinkButton DeptCode = (LinkButton)row.FindControl("lnkDeptCode");
                Session["DeptCode"] = DeptCode.Text;
            }
            else
            {
                Session["DeptCode"] = string.Empty;
            }
        }

        protected void BtnDeleteOk_Click(object sender, EventArgs e)
        {
            EntityDepartment entDept = new EntityDepartment();
            int cnt = 0;

            try
            {
                foreach (GridViewRow drv in dgvDepartment.Rows)
                {
                    CheckBox chkDelete = (CheckBox)drv.FindControl("chkDelete");
                    if (chkDelete.Checked)
                    {
                        LinkButton lnkDeptCode = (LinkButton)drv.FindControl("lnkDeptCode");
                        string lstrDeptCode = lnkDeptCode.Text;
                        entDept.DeptCode = lstrDeptCode;

                        cnt = mobjDeptBLL.DeleteDepartment(entDept);
                        if (cnt > 0)
                        {
                            lblMessage.Text = "Record Deleted Successfully....";

                            if (dgvDepartment.Rows.Count <= 0)
                            {
                                hdnPanel.Value = "none";
                            }
                        }
                        else
                        {
                            lblMessage.Text = "Record Not Deleted....";
                        }
                    }
                }
                GetDepartments();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
        protected void dgvDepartment_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dgvDepartment.DataSource = (DataTable)Session["DepartmentDetail"];
                dgvDepartment.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
        protected void dgvDepartment_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvDepartment.PageIndex = e.NewPageIndex;
        }
        protected void dgvDepartment_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap; text-align:left;");
                }

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Attributes.Add("onmouseover", "SetMouseOver(this)");
                    e.Row.Attributes.Add("onmouseout", "SetMouseOut(this)");
                    //LinkButton lnkDeptCode = (LinkButton)e.Row.FindControl("lnkDeptCode");
                    //CheckBox chkDelete = (CheckBox)e.Row.FindControl("chkDelete");
                    //if (lnkDeptCode.Text == "Admin")
                    //{
                    //    lnkDeptCode.Enabled = false;
                    //    chkDelete.Enabled = false;
                    //}
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
        protected void dgvDepartment_DataBound(object sender, EventArgs e)
        {
            int lintCurrentPage = dgvDepartment.PageIndex + 1;
            lblPageCount.Text = "<b>Page</b> " + lintCurrentPage.ToString() + "<b> of </b>" + dgvDepartment.PageCount.ToString();
        }
    }
}