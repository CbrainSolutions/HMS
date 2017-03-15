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
    public partial class frmGenderMaster : System.Web.UI.Page
    {
        GenderBLL mobjGenderBLL = new GenderBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] != null)
            {
                EntityLogin entLogin = (EntityLogin)Session["user"];

                List<EntityFormMaster> entFormAcc = (List<EntityFormMaster>)Session["AccessibleForm"];
                int Access = (from tbl in entFormAcc
                              where tbl.FormName.Contains("frmGender")
                              select tbl).Count();

                if (Access > 0)
                {
                    if (!Page.IsPostBack)
                    {
                        GetGender();
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

        protected void BtnAddNewGender_Click(object sender, EventArgs e)
        {
            DataTable ldt = new DataTable();
            ldt = mobjGenderBLL.GetNewGenderCode();
            txtGenderCode.Text = ldt.Rows[0]["GenderCode"].ToString();
            txtGenderDesc.Text = string.Empty;
            //this.programmaticModalPopup.Show();
        }

        public void GetGender()
        {
            DataTable ldtGender = new DataTable();
            ldtGender = mobjGenderBLL.GetAllGender();
            if (ldtGender.Rows.Count > 0 && ldtGender != null)
            {
                dgvGender.DataSource = ldtGender;
                dgvGender.DataBind();
                Session["GenderDetails"] = ldtGender;
                int lintRowcount = ldtGender.Rows.Count;
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
            EntityGender entGender = new EntityGender();
            if (string.IsNullOrEmpty(txtGenderCode.Text.Trim()))
            {
                lblMsg.Text = "Please Enter Country Code";
            }
            else
            {
                if (string.IsNullOrEmpty(txtGenderDesc.Text.Trim()))
                {
                    lblMsg.Text = "Please Enter Country Description";
                }
                else
                {
                    entGender.GenderCode = txtGenderCode.Text.Trim();
                    entGender.GenderDesc = txtGenderDesc.Text.Trim();
                    entGender.EntryBy = SessionWrapper.UserName;
                    lintcnt = mobjGenderBLL.InsertGender(entGender);

                    if (lintcnt > 0)
                    {
                        GetGender();
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

        protected void dgvGender_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                DataTable ldt = new DataTable();
                if (e.CommandName == "EditGender")
                {
                    //this.programmaticModalPopupEdit.Show();
                    int linIndex = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                    LinkButton lnkGenderCode = (LinkButton)gvr.FindControl("lnkGenderCode");
                    string lstrGenderCode = lnkGenderCode.Text;
                    txtEditGenderCode.Text = lstrGenderCode;
                    ldt = mobjGenderBLL.GetGenderForEdit(lstrGenderCode);
                    FillControls(ldt);
                }
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmGenderMaster -  dgvGender_RowCommand(object sender, GridViewCommandEventArgs e)", ex);
            }
        }

        private void FillControls(DataTable ldt)
        {
            txtEditGenderDesc.Text = ldt.Rows[0]["GenderDesc"].ToString();
        }

        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            int lintCnt = 0;
            try
            {
                EntityGender entGender = new EntityGender();

                entGender.GenderCode = txtEditGenderCode.Text;
                entGender.GenderDesc = txtEditGenderDesc.Text;
                entGender.ChangeBy = SessionWrapper.UserName;
                lintCnt = mobjGenderBLL.UpdateGender(entGender);

                if (lintCnt > 0)
                {
                    GetGender();
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
                Commons.FileLog("frmGenderMaster -  BtnEdit_Click(object sender, EventArgs e)", ex);
            }
        }

        protected void chkDelete_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chk.NamingContainer;
            if (chk.Checked)
            {
                LinkButton GenderCode = (LinkButton)row.FindControl("lnkGenderCode");
                Session["GenderCode"] = GenderCode.Text;
                lblMessage.Text = string.Empty;
            }
            else
            {
                Session["GenderCode"] = string.Empty;
            }
        }

        protected void BtnDeleteOk_Click(object sender, EventArgs e)
        {
            EntityGender entGender = new EntityGender();
            int cnt = 0;

            try
            {
                foreach (GridViewRow drv in dgvGender.Rows)
                {
                    CheckBox chkDelete = (CheckBox)drv.FindControl("chkDelete");
                    if (chkDelete.Checked)
                    {
                        LinkButton lnkGenderCode = (LinkButton)drv.FindControl("lnkGenderCode");
                        string lstrGenderCode = lnkGenderCode.Text;
                        entGender.GenderCode = lstrGenderCode;

                        cnt = mobjGenderBLL.DeleteGender(entGender);
                        if (cnt > 0)
                        {
                            //this.modalpopupDelete.Hide();

                            lblMessage.Text = "Record Deleted Successfully....";

                            if (dgvGender.Rows.Count <= 0)
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
                GetGender();
            }
            catch (System.Threading.ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                Commons.FileLog("frmGenderMaster -   BtnDeleteOk_Click(object sender, EventArgs e)", ex);
            }

        }

        protected void BtnDelete_Click(object sender, EventArgs e)
        {

            foreach (GridViewRow drv in dgvGender.Rows)
            {
                CheckBox chkDelete = (CheckBox)drv.FindControl("chkDelete");
                if (chkDelete.Checked)
                {
                    //this.modalpopupDelete.Show();
                }
            }
        }

        protected void dgvGender_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dgvGender.DataSource = (DataTable)Session["GenderDetail"];
                dgvGender.DataBind();
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmGenderMaster - dgvGender_PageIndexChanged(object sender, EventArgs e)", ex);
            }
        }

        protected void dgvGender_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvGender.PageIndex = e.NewPageIndex;
        }

        protected void dgvGender_RowDataBound(object sender, GridViewRowEventArgs e)
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

                }
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmGenderMaster -  dgvGender_RowDataBound(object sender, GridViewRowEventArgs e)", ex);
            }
        }

        protected void dgvGender_DataBound(object sender, EventArgs e)
        {
            int lintCurrentPage = dgvGender.PageIndex + 1;
            lblPageCount.Text = "<b>Page</b> " + lintCurrentPage.ToString() + "<b> of </b>" + dgvGender.PageCount.ToString();
        }

    }
}