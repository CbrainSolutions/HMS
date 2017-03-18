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
    public partial class frmTestMethodMaster : System.Web.UI.Page
    {
        TestMethodBLL mobjTestBLL = new TestMethodBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] != null)
            {
                EntityLogin entLogin = (EntityLogin)Session["user"];

                List<EntityFormMaster> entFormAcc = (List<EntityFormMaster>)Session["AccessibleForm"];
                int Access = (from tbl in entFormAcc
                              where tbl.FormName.Contains("frmTestMethodMaster")
                              select tbl).Count();

                if (Access > 0)
                {
                    if (!Page.IsPostBack)
                    {
                        Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                        GetTests();
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
                txtTestMethodId.Text = String.Empty;
                txtTestMethodDesc.Text = String.Empty;
                GetTests();
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
                txtTestMethodId.Text = Convert.ToString(row.Cells[0].Text);
                txtTestMethodDesc.Text = Convert.ToString(row.Cells[1].Text);
                txtTestMethodId.ReadOnly = true;
                GetTests();
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
        protected void BtnAddNewTestMethod_Click(object sender, EventArgs e)
        {
            EntityTestMethod entTest = new EntityTestMethod();
            int ID = mobjTestBLL.GetNewTestMethodId();
            txtTestMethodId.Text = Convert.ToString(ID);
            txtTestMethodDesc.Text = string.Empty;

            btnUpdate.Visible = false;
            BtnSave.Visible = true;
            MultiView1.SetActiveView(View2);
        }

        private void GetTests()
        {
            DataTable ldtTest = new DataTable();
            ldtTest = mobjTestBLL.GetAllTests();

            if (ldtTest.Rows.Count > 0 && ldtTest != null)
            {
                dgvTestMethod.DataSource = ldtTest;
                dgvTestMethod.DataBind();
                Session["TestMethodDetail"] = ldtTest;
                int lintRowcount = ldtTest.Rows.Count;
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
            EntityTestMethod entTest = new EntityTestMethod();

            if (string.IsNullOrEmpty(txtTestMethodDesc.Text.Trim()))
            {
                lblMsg.Text = "Please Enter Test Method Description";
            }
            else
            {
                entTest.TestMethodId = Convert.ToInt32(txtTestMethodId.Text);
                entTest.TestMethodDesc = txtTestMethodDesc.Text.Trim();
                lintcnt = mobjTestBLL.InsertTestMethod(entTest);
                if (lintcnt > 0)
                {
                    GetTests();
                    lblMessage.Text = "Record Inserted Successfully";
                    //this.programmaticModalPopup.Hide();
                }
                else
                {
                    lblMessage.Text = "Record Not Inserted";
                }
            }

            MultiView1.SetActiveView(View1);
        }

        void frmlTestMethodMaster_Load(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected void dgvTestMethod_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                DataTable ldt = new DataTable();
                if (e.CommandName == "EditTestMethod")
                {
                    //this.programmaticModalPopupEdit.Show();
                    int linIndex = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                    LinkButton lnkTestMethodId = (LinkButton)gvr.FindControl("lnkTestMethodId");
                    string lstrTestMethodId = lnkTestMethodId.Text;
                    //txtEditDeptCode.Text = lstrDeptCode;
                    ldt = mobjTestBLL.GetTestMethodForEdit(lstrTestMethodId);
                    FillControls(ldt);
                }
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmTestMethodMaster -  dgvTestMethod_RowCommand(object sender, GridViewCommandEventArgs e)", ex);
            }
        }

        private void FillControls(DataTable ldt)
        {
            //txtEditDeptDesc.Text = ldt.Rows[0]["DeptDesc"].ToString();
        }

        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            int lintCnt = 0;
            try
            {
                EntityTestMethod entTest = new EntityTestMethod();
                entTest.TestMethodId = Convert.ToInt32(txtTestMethodId.Text);

                entTest.TestMethodDesc = txtTestMethodDesc.Text;

                lintCnt = mobjTestBLL.UpdateTestMethod(entTest);

                if (lintCnt > 0)
                {
                    GetTests();
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
                Commons.FileLog("frmTestMethodMaster -  BtnEdit_Click(object sender, EventArgs e)", ex);
            }
        }

        protected void chkDelete_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chk.NamingContainer;
            if (chk.Checked)
            {
                LinkButton TestMethodId = (LinkButton)row.FindControl("lnkTestMethodId");
                Session["TestMethodId"] = TestMethodId.Text;
                //lblMessage.Text = string.Empty;
                //BtnDelete.Enabled = true;
            }
            else
            {
                Session["TestMethodId"] = string.Empty;
                //BtnDelete.Enabled = false;
            }
        }
        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow drv in dgvTestMethod.Rows)
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
            //EntityTestMethod entTest = new EntityTestMethod();
            //int cnt = 0;

            //try
            //{
            //    foreach (GridViewRow drv in dgvTestMethod.Rows)
            //    {
            //        CheckBox chkDelete = (CheckBox)drv.FindControl("chkDelete");
            //        if (chkDelete.Checked)
            //        {
            //            LinkButton lnkTestMethodId = (LinkButton)drv.FindControl("lnkTestMethodId");
            //            string lstrTestMethodId = Convert.ToString(lnkTestMethodId.Text);
            //            entTest.TestMethodId = Convert.ToString(lstrTestMethodId);

            //            cnt = mobjTestBLL.DeleteTestMethod(entTest);
            //            if (cnt > 0)
            //            {
            //                //this.modalpopupDelete.Hide();

            //                ="Record Deleted Successfully....";

            //                if (dgvTestMethod.Rows.Count <= 0)
            //                {
            //                    //pnlShow.Style.Add(HtmlTextWriterStyle.Display, "none");
            //                    hdnPanel.Value = "none";
            //                }

            //            }
            //            else
            //            {
            //                ="Record Not Deleted....";
            //            }
            //        }
            //    }
            //    GetTests();

            //}
            //catch (System.Threading.ThreadAbortException)
            //{

            //}
            //catch (Exception ex)
            //{
            //    Commons.FileLog("frmTestMethodMaster -   BtnDeleteOk_Click(object sender, EventArgs e)", ex);
            //}
        }
        protected void dgvTestMethod_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dgvTestMethod.DataSource = (DataTable)Session["TestMethodDetail"];
                dgvTestMethod.DataBind();
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmTestMethodMaster - dgvTestMethod_PageIndexChanged(object sender, EventArgs e)", ex);
            }
        }
        protected void dgvTestMethod_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvTestMethod.PageIndex = e.NewPageIndex;
        }
        protected void dgvTestMethod_RowDataBound(object sender, GridViewRowEventArgs e)
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
                Commons.FileLog("frmTestMethodMaster -  dgvData_RowDataBound(object sender, GridViewRowEventArgs e)", ex);
            }
        }
        protected void dgvTestMethod_DataBound(object sender, EventArgs e)
        {
            int lintCurrentPage = dgvTestMethod.PageIndex + 1;
            lblPageCount.Text = "<b>Page</b> " + lintCurrentPage.ToString() + "<b> of </b>" + dgvTestMethod.PageCount.ToString();
        }
    }
}