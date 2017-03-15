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
    public partial class frmRoomCategoryMaster : System.Web.UI.Page
    {
        RoomCategoryBLL mobjCateBLL = new RoomCategoryBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] != null)
            {
                EntityLogin entLogin = (EntityLogin)Session["user"];

                List<EntityFormMaster> entFormAcc = (List<EntityFormMaster>)Session["AccessibleForm"];
                int Access = (from tbl in entFormAcc
                              where tbl.FormName.Contains("frmRoomCategoryMaster")
                              select tbl).Count();

                if (Access > 0)
                {
                    if (!Page.IsPostBack)
                    {
                        GetCategories();
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
        protected void BtnAddNewCat_Click(object sender, EventArgs e)
        {
            DataTable ldt = new DataTable();
            txtCateDesc.Text = string.Empty;
            txtRate.Text = string.Empty;
            rdoICU.Checked = false;
            rdoOT.Checked = false;
            ldt = mobjCateBLL.GetNewCategoryCode();
            if (ldt.Rows.Count > 0 && ldt != null)
            {
                txtCateCode.Text = ldt.Rows[0]["CateCode"].ToString();
                //this.programmaticModalPopup.Show();
            }
            BtnSave.Visible = true;
            btnUpdate.Visible = false;
            MultiView1.SetActiveView(View2);
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton imgEdit = (ImageButton)sender;
                GridViewRow cnt = (GridViewRow)imgEdit.NamingContainer;
                txtCateCode.Text = Convert.ToString(cnt.Cells[0].Text);
                DataTable ldt = new DataTable();
                ldt = mobjCateBLL.GetCategoriesForEdit(txtCateCode.Text);
                if (ldt.Rows.Count > 0)
                {
                    if (Convert.ToBoolean(ldt.Rows[0]["IsICU"]))
                    {
                        rdoICU.Checked = true;
                    }
                    else
                    {
                        rdoICU.Checked = false;
                    }
                    if (Convert.ToBoolean(ldt.Rows[0]["IsOT"]))
                    {
                        rdoOT.Checked = true;
                    }
                    else
                    {
                        rdoOT.Checked = false;
                    }
                }
                txtCateDesc.Text = Convert.ToString(cnt.Cells[1].Text);
                txtRate.Text = Convert.ToString(cnt.Cells[2].Text);
                btnUpdate.Visible = true;
                BtnSave.Visible = false;
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
                txtCateCode.Text = string.Empty;
                txtCateDesc.Text = string.Empty;
                txtRate.Text = string.Empty;
                btnUpdate.Visible = true;
                BtnSave.Visible = false;
                MultiView1.SetActiveView(View1);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private void GetCategories()
        {
            DataTable ldtRoomCate = new DataTable();
            ldtRoomCate = mobjCateBLL.GetAllCategories();

            if (ldtRoomCate.Rows.Count > 0 && ldtRoomCate != null)
            {
                dgvRoomCategory.DataSource = ldtRoomCate;
                dgvRoomCategory.DataBind();
                Session["RoomCategoryDetail"] = ldtRoomCate;
                int lintRowcount = ldtRoomCate.Rows.Count;
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
            EntityRoomCategory entRoomCate = new EntityRoomCategory();

            if (string.IsNullOrEmpty(txtCateCode.Text.Trim()))
            {
                lblMsg.Text = "Please Enter Category Code";
            }
            else
            {
                if (string.IsNullOrEmpty(txtCateDesc.Text.Trim()))
                {
                    lblMsg.Text = "Please Enter Category Description";
                }
                else
                {
                    entRoomCate.CategoryCode = txtCateCode.Text.Trim();
                    entRoomCate.CategoryDesc = txtCateDesc.Text.Trim();

                    if (rdoICU.Checked)
                    {
                        entRoomCate.IsICU = true;
                    }
                    if (rdoOT.Checked)
                    {
                        entRoomCate.IsOT = true;
                        entRoomCate.Rate = 0;
                    }
                    else
                    {
                        entRoomCate.Rate = Convert.ToDecimal(txtRate.Text);
                    }
                    entRoomCate.EntryBy = SessionWrapper.UserName;
                    if (!Commons.IsRecordExists("tblCategoryMaster", "CategoryCode", entRoomCate.CategoryCode))
                    {
                        lintcnt = mobjCateBLL.InsertCategory(entRoomCate);

                        if (lintcnt > 0)
                        {
                            GetCategories();
                            lblMessage.Text = "Record Inserted Successfully....";
                            //this.programmaticModalPopup.Hide();                                                
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
        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            int lintCnt = 0;
            try
            {
                EntityRoomCategory entCate = new EntityRoomCategory();
                entCate.CategoryCode = txtCateCode.Text;
                entCate.CategoryDesc = txtCateDesc.Text;
                entCate.Rate = Convert.ToDecimal(txtRate.Text.Trim());
                entCate.ChangeBy = SessionWrapper.UserName;
                if (rdoICU.Checked)
                {
                    entCate.IsICU = true;
                }
                if (rdoOT.Checked)
                {
                    entCate.IsOT = true;
                }
                lintCnt = mobjCateBLL.UpdateCategory(entCate);

                if (lintCnt > 0)
                {
                    GetCategories();
                    lblMessage.Text = "Record Updated Successfully";
                    //this.programmaticModalPopup.Hide();
                }
                else
                {
                    lblMessage.Text = "Record Not Updated";
                }
                MultiView1.SetActiveView(View1);
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmCategoryMaster -  BtnEdit_Click(object sender, EventArgs e)", ex);
            }
        }
        protected void dgvRoomCategory_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                DataTable ldt = new DataTable();
                if (e.CommandName == "EditCategory")
                {
                    //this.programmaticModalPopupEdit.Show();
                    int linIndex = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                    LinkButton lnkCatCode = (LinkButton)gvr.FindControl("lnkCatCode");
                    string lstrCatCode = lnkCatCode.Text;
                    txtCateCode.Text = lstrCatCode;
                    ldt = mobjCateBLL.GetCategoriesForEdit(lstrCatCode);
                    if (ldt.Rows.Count > 0 && ldt != null)
                    {
                        FillControls(ldt);
                    }
                }
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmCategoryMaster -  dgvRoomCategory_RowCommand(object sender, GridViewCommandEventArgs e)", ex);
            }
        }

        private void FillControls(DataTable ldt)
        {
            txtCateDesc.Text = ldt.Rows[0]["CategoryDesc"].ToString();
            txtRate.Text = ldt.Rows[0]["Rate"].ToString();
        }

        protected void chkDelete_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chk.NamingContainer;
            if (chk.Checked)
            {
                LinkButton CateCode = (LinkButton)row.FindControl("lnkCatCode");
                Session["CategoryCode"] = CateCode.Text;
                //lblMessage.Text = string.Empty;
            }
            else
            {
                Session["CategoryCode"] = string.Empty;
            }
        }
        protected void BtnDeleteOk_Click(object sender, EventArgs e)
        {
            EntityRoomCategory entCate = new EntityRoomCategory();
            int cnt = 0;
            try
            {
                foreach (GridViewRow drv in dgvRoomCategory.Rows)
                {
                    CheckBox chkDelete = (CheckBox)drv.FindControl("chkDelete");
                    if (chkDelete.Checked)
                    {
                        LinkButton lnkCateCode = (LinkButton)drv.FindControl("lnkCatCode");
                        string lstrCateCode = lnkCateCode.Text;
                        entCate.CategoryCode = lstrCateCode;

                        cnt = mobjCateBLL.DeleteCategory(entCate);
                        if (cnt > 0)
                        {
                            //this.modalpopupDelete.Hide();
                            lblMessage.Text = "Record Deleted Successfully....";
                            if (dgvRoomCategory.Rows.Count <= 0)
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
                GetCategories();
            }
            catch (System.Threading.ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                Commons.FileLog("frmCategoryMaster -   BtnDeleteOk_Click(object sender, EventArgs e)", ex);
            }
        }

        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow drv in dgvRoomCategory.Rows)
            {
                CheckBox chkDelete = (CheckBox)drv.FindControl("chkDelete");
                if (chkDelete.Checked)
                {
                    //this.modalpopupDelete.Show();
                }
            }
        }
        protected void dgvRoomCategory_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dgvRoomCategory.DataSource = (DataTable)Session["RoomCategoryDetail"];
                dgvRoomCategory.DataBind();
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmCategoryMaster - dgvRoomCategory_PageIndexChanged(object sender, EventArgs e)", ex);
            }
        }
        protected void dgvRoomCategory_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvRoomCategory.PageIndex = e.NewPageIndex;
        }
        protected void dgvRoomCategory_DataBound(object sender, EventArgs e)
        {
            int lintCurrentPage = dgvRoomCategory.PageIndex + 1;
            lblPageCount.Text = "<b>Page</b> " + lintCurrentPage.ToString() + "<b> of </b>" + dgvRoomCategory.PageCount.ToString();
        }
        protected void dgvRoomCategory_RowDataBound(object sender, GridViewRowEventArgs e)
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
                Commons.FileLog("frmCategoryMaster -  dgvRoomCategory_RowDataBound(object sender, GridViewRowEventArgs e)", ex);
            }
        }
    }
}