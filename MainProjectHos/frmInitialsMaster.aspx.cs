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
    public partial class frmInitialsMaster : System.Web.UI.Page
    {
        InitialsBLL mobjInitialBLL = new InitialsBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] != null)
            {
                EntityLogin entLogin = (EntityLogin)Session["user"];

                List<EntityFormMaster> entFormAcc = (List<EntityFormMaster>)Session["AccessibleForm"];
                int Access = (from tbl in entFormAcc
                              where tbl.FormName.Contains("frmInitials")
                              select tbl).Count();

                if (Access > 0)
                {
                    if (!Page.IsPostBack)
                    {
                        GetInitials();
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
        protected void BtnAddNewInitial_Click(object sender, EventArgs e)
        {
            DataTable ldt = new DataTable();
            ldt = mobjInitialBLL.GetNewInitialCode();
            txtInitialCode.Text = ldt.Rows[0]["InitialCode"].ToString();
            txtInitialDesc.Text = string.Empty;
            //this.programmaticModalPopup.Show();
        }
        private void GetInitials()
        {
            DataTable ldtInitial = new DataTable();
            ldtInitial = mobjInitialBLL.GetAllInitials();

            if (ldtInitial.Rows.Count > 0 && ldtInitial != null)
            {
                dgvInitials.DataSource = ldtInitial;
                dgvInitials.DataBind();
                Session["InitialsDetail"] = ldtInitial;
                int lintRowcount = ldtInitial.Rows.Count;
                lblRowCount.Text = "<b>Total Records:</b> " + lintRowcount.ToString();
                pnlShow.Style.Add(HtmlTextWriterStyle.Display, "");
                hdnPanel.Value = "";
            }
            else
            {
                pnlShow.Style.Add(HtmlTextWriterStyle.Display, "none");
                hdnPanel.Value = "none";
            }

        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            int lintcnt = 0;
            EntityInitials entInitial = new EntityInitials();
            if (string.IsNullOrEmpty(txtInitialCode.Text.Trim()))
            {
                lblMsg.Text = "Please Enter Initial Code";
            }
            else
            {
                if (string.IsNullOrEmpty(txtInitialDesc.Text.Trim()))
                {
                    lblMsg.Text = "Please Enter Initial Description";
                }
                else
                {
                    entInitial.InitialCode = txtInitialCode.Text.Trim();
                    entInitial.InitialDesc = txtInitialDesc.Text.Trim();
                    entInitial.EntryBy = SessionWrapper.UserName;
                    lintcnt = mobjInitialBLL.InsertInitial(entInitial);

                    if (lintcnt > 0)
                    {
                        GetInitials();
                        lblMessage.Text = "Record Inserted Successfully";
                        //this.programmaticModalPopup.Hide();
                    }
                    else
                    {
                        lblMessage.Text = "Record Not Inserted";
                    }
                }
            }
        }
        protected void dgvInitials_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                DataTable ldt = new DataTable();
                if (e.CommandName == "EditInitial")
                {
                    //this.programmaticModalPopupEdit.Show();
                    int linIndex = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                    LinkButton lnkDeptCode = (LinkButton)gvr.FindControl("lnkInitialCode");
                    string lstrInitialCode = lnkDeptCode.Text;
                    txtEditInitialCode.Text = lstrInitialCode;
                    ldt = mobjInitialBLL.GetInitialForEdit(lstrInitialCode);
                    FillControls(ldt);
                }
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmInitialstMaster -  dgvInitial_RowCommand(object sender, GridViewCommandEventArgs e)", ex);
            }
        }

        private void FillControls(DataTable ldt)
        {
            txtEditInitialDesc.Text = ldt.Rows[0]["InitialDesc"].ToString();
        }

        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            int lintCnt = 0;
            try
            {
                EntityInitials entInitial = new EntityInitials();

                entInitial.InitialCode = txtEditInitialCode.Text;
                entInitial.InitialDesc = txtEditInitialDesc.Text;
                entInitial.ChangeBy = SessionWrapper.UserName;
                lintCnt = mobjInitialBLL.UpdateInitial(entInitial);

                if (lintCnt > 0)
                {
                    GetInitials();
                    lblMessage.Text = "Record Updated Successfully";
                    //this.programmaticModalPopup.Hide();
                }
                else
                {
                    lblMessage.Text = "Record Not Updated";
                }
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmInitialstMaster -  BtnEdit_Click(object sender, EventArgs e)", ex);
            }
        }

        protected void chkDelete_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chk.NamingContainer;
            if (chk.Checked)
            {
                LinkButton InitialCode = (LinkButton)row.FindControl("lnkInitialCode");
                Session["InitialCode"] = InitialCode.Text;
                lblMessage.Text = string.Empty;
            }
            else
            {
                Session["InitialCode"] = string.Empty;
            }
        }

        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow drv in dgvInitials.Rows)
            {
                CheckBox chkDelete = (CheckBox)drv.FindControl("chkDelete");
                if (chkDelete.Checked)
                {
                    //this.modalpopupDelete.Show();
                }
            }
        }

        protected void BtnDeleteOk_Click(object sender, EventArgs e)
        {
            EntityInitials entInitial = new EntityInitials();
            int cnt = 0;

            try
            {
                foreach (GridViewRow drv in dgvInitials.Rows)
                {
                    CheckBox chkDelete = (CheckBox)drv.FindControl("chkDelete");
                    if (chkDelete.Checked)
                    {
                        LinkButton lnkInitialCode = (LinkButton)drv.FindControl("lnkInitialCode");
                        string lstrInitialCode = lnkInitialCode.Text;
                        entInitial.InitialCode = lstrInitialCode;

                        cnt = mobjInitialBLL.DeleteInitial(entInitial);
                        if (cnt > 0)
                        {
                            //this.modalpopupDelete.Hide();

                            lblMessage.Text = "Record Deleted Successfully....";

                            if (dgvInitials.Rows.Count <= 0)
                            {
                                pnlShow.Style.Add(HtmlTextWriterStyle.Display, "none");
                                hdnPanel.Value = "none";
                            }

                        }
                        else
                        {
                            lblMessage.Text = "Record Not Deleted....";
                        }
                    }
                }
                GetInitials();

            }
            catch (System.Threading.ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                Commons.FileLog("frmInitialsMaster -   BtnDeleteOk_Click(object sender, EventArgs e)", ex);
            }
        }

        protected void dgvInitials_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dgvInitials.DataSource = (DataTable)Session["InitialDetail"];
                dgvInitials.DataBind();
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmInitialsMaster - dgvInitialPageIndexChanged(object sender, EventArgs e)", ex);
            }
        }

        protected void dgvInitials_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvInitials.PageIndex = e.NewPageIndex;
        }

        protected void dgvInitials_RowDataBound(object sender, GridViewRowEventArgs e)
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
                    LinkButton lnkInitialCode = (LinkButton)e.Row.FindControl("lnkInitialCode");
                    CheckBox chkDelete = (CheckBox)e.Row.FindControl("chkDelete");
                    if (lnkInitialCode.Text == "Admin")
                    {
                        lnkInitialCode.Enabled = false;
                        chkDelete.Enabled = false;
                    }
                }

            }
            catch (Exception ex)
            {
                Commons.FileLog("frmInitialsMaster -  dgvInitials_RowDataBound(object sender, GridViewRowEventArgs e)", ex);
            }
        }

        protected void dgvInitials_DataBound(object sender, EventArgs e)
        {
            int lintCurrentPage = dgvInitials.PageIndex + 1;
            lblPageCount.Text = "<b>Page</b> " + lintCurrentPage.ToString() + "<b> of </b>" + dgvInitials.PageCount.ToString();
        }
    }
}