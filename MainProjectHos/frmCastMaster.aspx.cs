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
    public partial class frmCastMaster : System.Web.UI.Page
    {
        CasteBLL mobjCastBLL = new CasteBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] != null)
            {
                EntityLogin entLogin = (EntityLogin)Session["user"];

                List<EntityFormMaster> entFormAcc = (List<EntityFormMaster>)Session["AccessibleForm"];
                int Access = (from tbl in entFormAcc
                              where tbl.FormName.Contains("frmCast")
                              select tbl).Count();

                if (Access > 0)
                {
                    if (!Page.IsPostBack)
                    {
                        GetCaste();
                        GetReligionForSave();
                        GetReligionForEdit();
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
        protected void BtnAddNewCast_Click(object sender, EventArgs e)
        {
            DataTable ldt = new DataTable();
            ldt = mobjCastBLL.GetNewCasteCode();
            txtCastCode.Text = ldt.Rows[0]["CastCode"].ToString();
            txtCastDesc.Text = string.Empty;
            GetReligionForSave();
            //this.programmaticModalPopup.Show();
        }

        public void GetCaste()
        {
            List<EntityCast> ldtCast = mobjCastBLL.GetAllCaste();
            if (ldtCast.Count > 0 && ldtCast != null)
            {
                dgvCast.DataSource = ldtCast;
                dgvCast.DataBind();
                Session["CastDetails"] = ldtCast;
                int lintRowcount = ldtCast.Count;
                lblRowCount.Text = "<b>Total Records:</b> " + lintRowcount.ToString();
            }
        }

        public void GetReligionForSave()
        {
            DataTable ldtState = new DataTable();
            ldtState = mobjCastBLL.GetAllReligion();
            if (ldtState.Rows.Count > 0 && ldtState != null)
            {
                ddlReligion.DataSource = ldtState;
                ddlReligion.DataValueField = "PKId";
                ddlReligion.DataTextField = "ReligionDesc";
                ddlReligion.DataBind();
                ListItem li = new ListItem();
                li.Text = "--Select Religion--";
                li.Value = "0";
                ddlReligion.Items.Insert(0, li);
            }
        }

        public void GetReligionForEdit()
        {
            DataTable ldtState = new DataTable();
            ldtState = mobjCastBLL.GetAllReligion();
            if (ldtState.Rows.Count > 0 && ldtState != null)
            {
                ddlEditReligion.DataSource = ldtState;
                ddlEditReligion.DataValueField = "PKId";
                ddlEditReligion.DataTextField = "ReligionDesc";
                ddlEditReligion.DataBind();
                ListItem li = new ListItem();
                li.Text = "--Select Religion--";
                li.Value = "0";
                ddlEditReligion.Items.Insert(0, li);
            }

        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            int lintcnt = 0;
            EntityCast entCast = new EntityCast();

            if (ddlReligion.SelectedIndex == 0)
            {
                lblMsg.Text = "Please Select Religion";
            }
            else if (string.IsNullOrEmpty(txtCastDesc.Text.Trim()))
            {
                lblMsg.Text = "Enter Cast Description";
            }
            else
            {
                entCast.CastCode = txtCastCode.Text.Trim();
                entCast.CastDesc = txtCastDesc.Text.Trim();
                entCast.Religion = Convert.ToInt32(ddlReligion.SelectedValue);
                entCast.EntryBy = SessionWrapper.UserName;
                lintcnt = mobjCastBLL.InsertCaste(entCast);

                if (lintcnt > 0)
                {
                    GetCaste();
                    lblMessage.Text = "Record Inserted Successfully";
                    //this.programmaticModalPopup.Hide();
                }
                else
                {
                    lblMessage.Text = "Record Not Inserted";
                }
            }
        }

        protected void dgvCast_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                DataTable ldt = new DataTable();
                if (e.CommandName == "EditCast")
                {
                    //this.programmaticModalPopupEdit.Show();
                    int linIndex = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                    LinkButton lnkCastCode = (LinkButton)gvr.FindControl("lnkCastCode");
                    string lstrCastCode = lnkCastCode.Text;
                    txtEditCastCode.Text = lstrCastCode;
                    Session["CastCode"] = lnkCastCode.Text;
                    ldt = mobjCastBLL.GetCasteForEdit(lstrCastCode);
                    GetReligionForEdit();
                    FillControls(ldt);

                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private void FillControls(DataTable ldt)
        {
            ddlEditReligion.SelectedValue = ldt.Rows[0]["ReligionId"].ToString();
            txtEditCastDesc.Text = ldt.Rows[0]["CastDesc"].ToString();
        }

        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            int lintCnt = 0;
            try
            {
                EntityCast entCast = new EntityCast();

                string lstrCastCode = Session["CastCode"].ToString();

                entCast.CastCode = lstrCastCode;
                entCast.CastDesc = txtEditCastDesc.Text;
                entCast.Religion = Convert.ToInt32(ddlEditReligion.SelectedValue);
                entCast.ChangeBy = SessionWrapper.UserName;
                lintCnt = mobjCastBLL.UpdateCaste(entCast);

                if (lintCnt > 0)
                {
                    GetCaste();
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
                lblMessage.Text = ex.Message;
            }
        }

        protected void chkDelete_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chk.NamingContainer;
            if (chk.Checked)
            {
                LinkButton CastCode = (LinkButton)row.FindControl("lnkCastCode");
                Session["CastCode"] = CastCode.Text;
                lblMessage.Text = string.Empty;
            }
            else
            {
                Session["CastCode"] = string.Empty;
            }
        }
        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow drv in dgvCast.Rows)
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
            EntityCast entCast = new EntityCast();
            int cnt = 0;

            try
            {
                foreach (GridViewRow drv in dgvCast.Rows)
                {
                    CheckBox chkDelete = (CheckBox)drv.FindControl("chkDelete");
                    if (chkDelete.Checked)
                    {
                        LinkButton lnkCastCode = (LinkButton)drv.FindControl("lnkCastCode");
                        string lstrCastCode = lnkCastCode.Text;
                        entCast.CastCode = lstrCastCode;

                        cnt = mobjCastBLL.DeleteCaste(entCast);
                        if (cnt > 0)
                        {
                            //this.modalpopupDelete.Hide();
                            lblMessage1.Text = "Record Deleted Successfully....";

                            if (dgvCast.Rows.Count <= 0)
                            {
                                pnlShow.Style.Add(HtmlTextWriterStyle.Display, "none");
                                hdnPanel.Value = "none";
                            }

                        }
                        else
                        {
                            lblMessage1.Text = "Record Not Deleted....";
                        }
                    }
                }
                GetCaste();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void dgvCast_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dgvCast.DataSource = (List<EntityCast>)Session["CastDetails"];
                dgvCast.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void dgvCast_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvCast.PageIndex = e.NewPageIndex;
        }

        protected void dgvCast_RowDataBound(object sender, GridViewRowEventArgs e)
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
                lblMessage.Text = ex.Message;
            }
        }

        protected void dgvCast_DataBound(object sender, EventArgs e)
        {
            int lintCurrentPage = dgvCast.PageIndex + 1;
            lblPageCount.Text = "<b>Page</b> " + lintCurrentPage.ToString() + "<b> of </b>" + dgvCast.PageCount.ToString();
        }
    }
}