using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MainProjectHos.Models.BusinessLayer;
using MainProjectHos.Models.DataLayer;
using MainProjectHos.Models.DataModels;
using System.Data;
using System.Drawing;

namespace MainProjectHos
{
    public partial class frmTestParameters : System.Web.UI.Page
    {
        TestBLL mobjDeptBLL = new TestBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] != null)
            {
                EntityLogin entLogin = (EntityLogin)Session["user"];

                List<EntityFormMaster> entFormAcc = (List<EntityFormMaster>)Session["AccessibleForm"];
                int Access = (from tbl in entFormAcc
                              where tbl.FormName.Contains("frmTestParameters")
                              select tbl).Count();

                if (Access > 0)
                {
                    if (!Page.IsPostBack)
                    {
                        Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                        GetTestsWithPara();
                        BindTestName();
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

        private void BindTestName()
        {
            try
            {
                DataTable tblCat = new TestBLL().GetAllTest();
                DataRow dr = tblCat.NewRow();
                dr["TestId"] = 0;
                dr["TestName"] = "---Select---";
                tblCat.Rows.InsertAt(dr, 0);

                ddlTestName.DataSource = tblCat;
                ddlTestName.DataValueField = "TestId";
                ddlTestName.DataTextField = "TestName";
                ddlTestName.DataBind();
            }
            catch (Exception ex)
            {
                //  lblMessage.Text = ex.Message;
            }
        }

        protected void BtnClose_Click(object sender, EventArgs e)
        {
            try
            {
                txtMaxPara.Text = String.Empty;
                //txtDeptDesc.Text = String.Empty;
                GetTestsWithPara();
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
                string id = Convert.ToString(row.Cells[0].Text);
                Session["T_Id"] = Convert.ToInt32(id);
                DataTable tblEmp = new TestBLL().SelectTestParaForEdit(id);
                if (tblEmp.Rows.Count > 0)
                {
                    txtParaName.Text = Convert.ToString(tblEmp.Rows[0]["ParaName"]);
                    txtMinPara.Text = Convert.ToString(tblEmp.Rows[0]["MinPara"]);
                    txtMaxPara.Text = Convert.ToString(tblEmp.Rows[0]["MaxPara"]);
                    ListItem item = ddlTestName.Items.FindByText(Convert.ToString(tblEmp.Rows[0]["TestName"]));
                    ddlTestName.SelectedValue = item.Value;
                    GetTestsWithPara();
                    btnUpdate.Visible = true;
                    BtnSave.Visible = false;
                    MultiView1.SetActiveView(View2);
                }
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
            btnUpdate.Visible = false;
            BtnSave.Visible = true;
            MultiView1.SetActiveView(View2);
            Clear();
        }

        private void Clear()
        {
            ddlTestName.SelectedIndex = 0;
            txtParaName.Text = string.Empty;
            txtMinPara.Text = string.Empty;
            txtMaxPara.Text = string.Empty;
        }

        private void GetTestsWithPara()
        {

            List<EntityTestPara> ldtDept = mobjDeptBLL.GetAllTestsWithPara();
            if (ldtDept.Count > 0)
            {
                dgvTestParameter.DataSource = ldtDept;
                dgvTestParameter.DataBind();
                Session["DepartmentDetail"] = ldtDept;
                int lintRowcount = ldtDept.Count;
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
            EntityTestPara entDept = new EntityTestPara();
            if (Session["update"].ToString() == ViewState["update"].ToString())
            {
                if (ddlTestName.SelectedIndex == 0)
                {
                    lblMsg.Text = "Please Select Test Name";
                    ddlTestName.Focus();
                    return;
                }
                else
                {
                    if (string.IsNullOrEmpty(txtParaName.Text.Trim()))
                    {
                        lblMsg.Text = "Please Enter The Test Parameter Name";
                        txtParaName.Focus();
                        return;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(txtMinPara.Text.Trim()))
                        {
                            lblMsg.Text = "Please Enter Minimum Parameter Value";
                            txtMinPara.Focus();
                            return;
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(txtMaxPara.Text.Trim()))
                            {
                                lblMsg.Text = "Please Enter Maximum Parameter Value";
                                txtMaxPara.Focus();
                                return;
                            }
                            else
                            {
                                if (Convert.ToDecimal(txtMaxPara.Text) <= Convert.ToDecimal(txtMinPara.Text))
                                {
                                    lblMsg.Text = "Maximum Parameter Value Can't Be Less Then Minimum Parameter Value";
                                    txtMaxPara.Text = string.Empty;
                                    txtMaxPara.Focus();
                                    return;
                                }
                                else
                                {
                                    entDept.TestId = Convert.ToInt32(ddlTestName.SelectedValue);
                                    entDept.MinPara = Convert.ToDecimal(txtMinPara.Text);
                                    entDept.MaxPara = Convert.ToDecimal(txtMaxPara.Text);
                                    entDept.ParaName = txtParaName.Text;

                                    if (!mobjDeptBLL.IsRecordExist(entDept))
                                    {
                                        lintcnt = mobjDeptBLL.InsertTestWithPara(entDept);

                                        if (lintcnt > 0)
                                        {
                                            GetTestsWithPara();
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
                        }
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
                EntityTestPara entDept = new EntityTestPara();
                entDept.TstParID = Convert.ToInt32(Session["T_Id"]);
                entDept.TestId = ddlTestName.SelectedIndex;
                entDept.ParaName = txtParaName.Text;
                if (Convert.ToDecimal(txtMaxPara.Text) <= Convert.ToDecimal(txtMinPara.Text))
                {
                    lblMsg.Text = "Maximum Parameter Value Can't Be Less Then Minimum Parameter Value";
                    txtMaxPara.Text = string.Empty;
                    txtMaxPara.Focus();
                    return;
                }
                else
                {
                    entDept.MinPara = Convert.ToDecimal(txtMinPara.Text);
                    entDept.MaxPara = Convert.ToDecimal(txtMaxPara.Text);
                }
                if (!mobjDeptBLL.IsRecordExist(entDept))
                {
                    lintCnt = mobjDeptBLL.UpdatePara(entDept);

                    if (lintCnt > 0)
                    {
                        GetTestsWithPara();
                        lblMessage.Text = "Record Updated Successfully";
                        //this.programmaticModalPopup.Hide();
                    }
                    else
                    {
                        lblMessage.Text = "Record Not Updated";
                    }
                }
                else
                {
                    lblMessage.Text = "Record Already Exist....";
                }
                MultiView1.SetActiveView(View1);
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmDepartmentMaster -  BtnEdit_Click(object sender, EventArgs e)", ex);
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
                //lblMessage.Text = string.Empty;
            }
            else
            {
                Session["DeptCode"] = string.Empty;
            }
        }


        //protected void dgvTestParameter_DataBound(object sender, EventArgs e)
        //{
        //    int lintCurrentPage = dgvTestParameter.PageIndex + 1;
        //    lblPageCount.Text = "<b>Page</b> " + lintCurrentPage.ToString() + "<b> of </b>" + dgvTestParameter.PageCount.ToString();
        //}

        protected void dgvTestParameter_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dgvTestParameter.DataSource = (List<EntityTestPara>)Session["DepartmentDetail"];
                dgvTestParameter.DataBind();
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmDepartmentMaster - dgvDepartment_PageIndexChanged(object sender, EventArgs e)", ex);
            }
        }

        protected void dgvTestParameter_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvTestParameter.PageIndex = e.NewPageIndex;
        }



        protected void dgvTestParameter_RowDataBound(object sender, GridViewRowEventArgs e)
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
                    LinkButton lnkDeptCode = (LinkButton)e.Row.FindControl("lnkDeptCode");
                    CheckBox chkDelete = (CheckBox)e.Row.FindControl("chkDelete");
                    if (lnkDeptCode.Text == "Admin")
                    {
                        lnkDeptCode.Enabled = false;
                        chkDelete.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmDepartmentMaster -  dgvData_RowDataBound(object sender, GridViewRowEventArgs e)", ex);
            }
        }
    }
}