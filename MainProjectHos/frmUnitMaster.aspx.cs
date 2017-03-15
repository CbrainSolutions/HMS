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
    public partial class frmUnitMaster : System.Web.UI.Page
    {
        UnitBLL mobjUnitBLL = new UnitBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] != null)
            {
                EntityLogin entLogin = (EntityLogin)Session["user"];

                if (entLogin != null)
                {
                    if (entLogin.UserType.Equals("Lab", StringComparison.CurrentCultureIgnoreCase) || entLogin.UserType.Equals("Admin", StringComparison.CurrentCultureIgnoreCase))
                    {
                        if (!Page.IsPostBack)
                        {
                            if (!Page.IsPostBack)
                            {
                                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                                GetUnits();
                                MultiView1.SetActiveView(View1);
                            }
                        }
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
                txtUnitCode.Text = String.Empty;
                txtUnitDesc.Text = String.Empty;
                GetUnits();
                MultiView1.SetActiveView(View1);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
        private void GetUnits()
        {
            DataTable ldtUnit = new DataTable();
            ldtUnit = mobjUnitBLL.GetAllUnit();

            if (ldtUnit.Rows.Count > 0 && ldtUnit != null)
            {
                dgvUnit.DataSource = ldtUnit;
                dgvUnit.DataBind();
                Session["UnitDetail"] = ldtUnit;
                int lintRowcount = ldtUnit.Rows.Count;
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

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton imgEdit = (ImageButton)sender;
                GridViewRow row = (GridViewRow)imgEdit.NamingContainer;
                txtUnitCode.Text = Convert.ToString(row.Cells[0].Text);
                txtUnitDesc.Text = Convert.ToString(row.Cells[1].Text);
                txtUnitCode.ReadOnly = true;
                GetUnits();
                btnUpdate.Visible = true;
                BtnSave.Visible = false;
                MultiView1.SetActiveView(View2);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void BtnAddNewUnit_Click(object sender, EventArgs e)
        {
            DataTable ldt = new DataTable();
            txtUnitDesc.Text = string.Empty;
            ldt = mobjUnitBLL.GetNewUnitCode();
            txtUnitCode.Text = ldt.Rows[0]["UnitCode"].ToString();

            btnUpdate.Visible = false;
            BtnSave.Visible = true;
            MultiView1.SetActiveView(View2);
            //this.programmaticModalPopup.Show();
        }


        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow drv in dgvUnit.Rows)
            {
                CheckBox chkDelete = (CheckBox)drv.FindControl("chkDelete");
                if (chkDelete.Checked)
                {
                    //this.modalpopupDelete.Show();
                }
            }
        }

        protected void chkDelete_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chk.NamingContainer;
            if (chk.Checked)
            {
                LinkButton UnitCode = (LinkButton)row.FindControl("lnkUnitCode");
                Session["UnitCode"] = UnitCode.Text;
                lblMessage.Text = string.Empty;
            }
            else
            {
                Session["UnitCode"] = string.Empty;
            }
        }

        private void FillControls(DataTable ldt)
        {
            //txtUnitDesc.Text = ldt.Rows[0]["UnitDesc"].ToString();
        }

        protected void dgvUnit_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dgvUnit.DataSource = (DataTable)Session["UnitDetail"];
                dgvUnit.DataBind();
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmUnitMaster - dgvUnit_PageIndexChanged(object sender, EventArgs e)", ex);
            }
        }
        protected void dgvUnit_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvUnit.PageIndex = e.NewPageIndex;
        }
        protected void dgvUnit_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                DataTable ldt = new DataTable();
                if (e.CommandName == "EditUnit")
                {
                    //this.programmaticModalPopupEdit.Show();
                    int linIndex = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                    LinkButton lnkUnitCode = (LinkButton)gvr.FindControl("lnkUnitCode");
                    string lstrUnitCode = lnkUnitCode.Text;
                    txtUnitCode.Text = lstrUnitCode;
                    ldt = mobjUnitBLL.GetUnitForEdit(lstrUnitCode);
                    FillControls(ldt);
                }
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmUnitMaster - dgvUnit_RowCommand(object sender, GridViewCommandEventArgs e)", ex);
            }
        }
        protected void dgvUnit_DataBound(object sender, EventArgs e)
        {
            int lintCurrentPage = dgvUnit.PageIndex + 1;
            lblPageCount.Text = "<b>Page</b> " + lintCurrentPage.ToString() + "<b> of </b>" + dgvUnit.PageCount.ToString();
        }
        protected void dgvUnit_RowDataBound(object sender, GridViewRowEventArgs e)
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
                Commons.FileLog("frmUnitMaster -  dgvUnit_RowDataBound(object sender, GridViewRowEventArgs e)", ex);
            }
        }
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            int lintcnt = 0;
            EntityUnit entUnit = new EntityUnit();
            if (string.IsNullOrEmpty(txtUnitCode.Text.Trim()))
            {
                lblMsg.Text = "Please Enter Unit Code";
            }
            else
            {
                if (string.IsNullOrEmpty(txtUnitDesc.Text.Trim()))
                {
                    lblMsg.Text = " Enter Unit Description";
                    txtUnitDesc.Focus();
                    return;
                }
                else
                {
                    entUnit.UnitCode = txtUnitCode.Text.Trim();
                    entUnit.UnitDesc = txtUnitDesc.Text.Trim();

                    if (!Commons.IsRecordExists("tblUnitMaster", "UnitDesc", Convert.ToString(entUnit.UnitDesc)))
                    {
                        lintcnt = mobjUnitBLL.InsertUnit(entUnit);

                        if (lintcnt > 0)
                        {
                            GetUnits();
                            lblMessage.Text = "Record Inserted Successfully";
                            // this.programmaticModalPopup.Hide();
                        }
                        else
                        {
                            lblMessage.Text = "Record Not Inserted";
                        }
                    }
                    else
                    {
                        lblMessage.Text = "Record Already Exist";
                    }
                }

            }
            MultiView1.SetActiveView(View1);
        }
        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            int lintcnt = 0;
            EntityUnit entUnit = new EntityUnit();
            if (string.IsNullOrEmpty(txtUnitCode.Text.Trim()))
            {
                lblMsg.Text = "Please Enter Unit Code";
            }
            else
            {
                if (string.IsNullOrEmpty(txtUnitDesc.Text.Trim()))
                {
                    lblMsg.Text = "Enter Unit Description";
                    txtUnitDesc.Focus();
                    return;
                }
                else
                {
                    entUnit.UnitCode = txtUnitCode.Text.Trim();
                    entUnit.UnitDesc = txtUnitDesc.Text.Trim();
                    if (!Commons.IsRecordExists("tblUnitMaster", "UnitDesc", Convert.ToString(entUnit.UnitDesc)))
                    {
                        lintcnt = mobjUnitBLL.UpdateUnit(entUnit);

                        if (lintcnt > 0)
                        {
                            GetUnits();
                            lblMessage.Text = "Record Updated Successfully";
                        }
                        else
                        {
                            lblMessage.Text = "Record Not Updated";
                        }
                    }
                    else
                    {
                        lblMessage.Text = "Record Already Exist";
                    }
                }
            }
            MultiView1.SetActiveView(View1);
        }
        protected void BtnDeleteOk_Click(object sender, EventArgs e)
        {
            EntityUnit entUnit = new EntityUnit();
            int cnt = 0;

            try
            {
                foreach (GridViewRow drv in dgvUnit.Rows)
                {
                    CheckBox chkDelete = (CheckBox)drv.FindControl("chkDelete");
                    if (chkDelete.Checked)
                    {
                        LinkButton lnkUnitCode = (LinkButton)drv.FindControl("lnkUnitCode");
                        string lstrUnitCode = lnkUnitCode.Text;
                        entUnit.UnitCode = lstrUnitCode;
                        cnt = mobjUnitBLL.DeleteUnit(entUnit);
                        if (cnt > 0)
                        {
                            //this.modalpopupDelete.Hide();
                            lblMessage.Text = "Record Deleted Successfully....";
                            if (dgvUnit.Rows.Count <= 0)
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
                GetUnits();
            }
            catch (System.Threading.ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                Commons.FileLog("frmUnitMaster -   BtnDeleteOk_Click(object sender, EventArgs e)", ex);
            }
        }
    }
}