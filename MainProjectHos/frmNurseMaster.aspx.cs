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
    public partial class frmNurseMaster : System.Web.UI.Page
    {
        NurseBLL mobjReligionBLL = new NurseBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] != null)
            {
                EntityLogin entLogin = (EntityLogin)Session["user"];

                List<EntityFormMaster> entFormAcc = (List<EntityFormMaster>)Session["AccessibleForm"];
                int Access = (from tbl in entFormAcc
                              where tbl.FormName.Contains("frmNurseMaster")
                              select tbl).Count();

                if (Access > 0)
                {
                    if (!Page.IsPostBack)
                    {
                        GetReligion();
                        GetDeptCategory();
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

        protected void BtnAddNewReligion_Click(object sender, EventArgs e)
        {
            DataTable ldt = new DataTable();
            ldt = mobjReligionBLL.GetNewReligionCode();
            txtReligionCode.Text = ldt.Rows[0][0].ToString();
            txtReligionDesc.Text = string.Empty;
            //this.programmaticModalPopup.Show();
        }

        public void GetDeptCategory()
        {
            PatientMasterBLL mobjPatient = new PatientMasterBLL();
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

        public void GetEditDeptCategory()
        {
            PatientMasterBLL mobjPatient = new PatientMasterBLL();
            try
            {
                DataTable ldt = new DataTable();
                ldt = mobjPatient.GetDeptCategory();
                ddlEditDeptCategory.DataSource = ldt;
                ddlEditDeptCategory.DataValueField = "CategoryId";
                ddlEditDeptCategory.DataTextField = "CategoryName";
                ddlEditDeptCategory.DataBind();

                ListItem li = new ListItem();
                li.Text = "--Select DeptCategory--";
                li.Value = "0";
                ddlEditDeptCategory.Items.Insert(0, li);
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }


        public void GetReligion()
        {
            DataTable ldtReligion = new DataTable();
            ldtReligion = mobjReligionBLL.GetAllReligion();
            if (ldtReligion.Rows.Count > 0 && ldtReligion != null)
            {
                dgvReligion.DataSource = ldtReligion;
                dgvReligion.DataBind();
                Session["ReligionDetails"] = ldtReligion;
                int lintRowcount = ldtReligion.Rows.Count;
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
            EntityNurse entReligion = new EntityNurse();
            if (string.IsNullOrEmpty(txtReligionCode.Text.Trim()))
            {
                lblMsg.Text = "Please Enter Nurse Code";
            }
            else
            {
                if (string.IsNullOrEmpty(txtReligionDesc.Text.Trim()))
                {
                    lblMsg.Text = "Please Enter Nurse Name";
                }
                else
                {
                    if (ddlDeptCategory.SelectedIndex == 0)
                    {
                        lblMsg.Text = "Please Select Department ";
                    }
                    else
                    {
                        entReligion.NurseCode = txtReligionCode.Text.Trim();
                        entReligion.NurseName = txtReligionDesc.Text.Trim();
                        entReligion.DeptId = Convert.ToInt32(ddlDeptCategory.SelectedValue);
                        entReligion.DepartmentName = Convert.ToString(ddlDeptCategory.SelectedItem.Text);
                        lintcnt = mobjReligionBLL.InsertReligion(entReligion);

                        if (lintcnt > 0)
                        {
                            GetReligion();
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

        }

        protected void dgvReligion_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                DataTable ldt = new DataTable();
                if (e.CommandName == "EditReligion")
                {
                    GetEditDeptCategory();
                    //this.programmaticModalPopupEdit.Show();
                    int linIndex = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                    LinkButton lnkReligionCode = (LinkButton)gvr.FindControl("lnkReligionCode");
                    string lstrReligionCode = lnkReligionCode.Text;
                    txtEditReligionCode.Text = lstrReligionCode;
                    ldt = mobjReligionBLL.GetReligionForEdit(lstrReligionCode);
                    FillControls(ldt);
                }
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmNurseMaster -  dgvReligion_RowCommand(object sender, GridViewCommandEventArgs e)", ex);
            }
        }

        private void FillControls(DataTable ldt)
        {
            txtEditReligionDesc.Text = ldt.Rows[0]["NurseName"].ToString();
            ddlEditDeptCategory.SelectedValue = ldt.Rows[0]["DeptId"].ToString();
        }

        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            int lintCnt = 0;
            try
            {
                EntityNurse entReligion = new EntityNurse();

                entReligion.NurseCode = txtEditReligionCode.Text;
                entReligion.NurseName = txtEditReligionDesc.Text;
                entReligion.DeptId = Convert.ToInt32(ddlEditDeptCategory.SelectedValue);
                entReligion.DepartmentName = Convert.ToString(ddlEditDeptCategory.SelectedItem.Text);
                lintCnt = mobjReligionBLL.UpdateReligion(entReligion);

                if (lintCnt > 0)
                {
                    GetReligion();
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
                Commons.FileLog("frmReligionMaster -  BtnEdit_Click(object sender, EventArgs e)", ex);
            }
        }

        protected void chkDelete_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chk.NamingContainer;
            if (chk.Checked)
            {
                LinkButton ReligionCode = (LinkButton)row.FindControl("lnkReligionCode");
                Session["NurseCode"] = ReligionCode.Text;
                lblMessage.Text = string.Empty;
            }
            else
            {
                Session["NurseCode"] = string.Empty;
            }
        }

        protected void BtnDeleteOk_Click(object sender, EventArgs e)
        {
            EntityNurse entReligion = new EntityNurse();
            int cnt = 0;

            try
            {
                foreach (GridViewRow drv in dgvReligion.Rows)
                {
                    CheckBox chkDelete = (CheckBox)drv.FindControl("chkDelete");
                    if (chkDelete.Checked)
                    {
                        LinkButton lnkReligionCode = (LinkButton)drv.FindControl("lnkReligionCode");
                        string lstrReligionCode = lnkReligionCode.Text;
                        entReligion.NurseCode = lstrReligionCode;

                        cnt = mobjReligionBLL.DeleteReligion(entReligion);
                        if (cnt > 0)
                        {
                            //this.modalpopupDelete.Hide();

                            lblMessage.Text = "Record Deleted Successfully....";

                            if (dgvReligion.Rows.Count <= 0)
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
                GetReligion();
            }
            catch (System.Threading.ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                Commons.FileLog("frmNurseMaster -   BtnDeleteOk_Click(object sender, EventArgs e)", ex);
            }

        }

        protected void BtnDelete_Click(object sender, EventArgs e)
        {

            foreach (GridViewRow drv in dgvReligion.Rows)
            {
                CheckBox chkDelete = (CheckBox)drv.FindControl("chkDelete");
                if (chkDelete.Checked)
                {
                    //this.modalpopupDelete.Show();
                }
            }
        }

        protected void dgvReligion_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dgvReligion.DataSource = (DataTable)Session["ReligionDetail"];
                dgvReligion.DataBind();
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmNurseMaster - dgvReligion_PageIndexChanged(object sender, EventArgs e)", ex);
            }
        }

        protected void dgvReligion_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvReligion.PageIndex = e.NewPageIndex;
        }

        protected void dgvReligion_RowDataBound(object sender, GridViewRowEventArgs e)
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
                Commons.FileLog("frmNurseMaster -  dgvReligion_RowDataBound(object sender, GridViewRowEventArgs e)", ex);
            }
        }

        protected void dgvReligion_DataBound(object sender, EventArgs e)
        {
            int lintCurrentPage = dgvReligion.PageIndex + 1;
            lblPageCount.Text = "<b>Page</b> " + lintCurrentPage.ToString() + "<b> of </b>" + dgvReligion.PageCount.ToString();
        }
    }
}