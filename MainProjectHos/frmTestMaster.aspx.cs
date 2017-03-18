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
    public partial class frmTestMaster : System.Web.UI.Page
    {
        TestBLL mobjDeptBLL = new TestBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] != null)
            {
                EntityLogin entLogin = (EntityLogin)Session["user"];

                List<EntityFormMaster> entFormAcc = (List<EntityFormMaster>)Session["AccessibleForm"];
                int Access = (from tbl in entFormAcc
                              where tbl.FormName.Contains("frmTestMaster")
                              select tbl).Count();

                if (Access > 0)
                {
                    if (!Page.IsPostBack)
                    {
                        Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                        GetTests();
                        BindCatagory();
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                List<EntityTest> ldtDept = mobjDeptBLL.GetAllTests(txtSearch.Text);
                if (ldtDept.Count > 0)
                {
                    dgvDepartment.DataSource = ldtDept;
                    dgvDepartment.DataBind();
                    Session["DepartmentDetail"] = ldtDept;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                dgvDepartment.PageIndex = 0;
                GetTests();
                txtSearch.Text = string.Empty;
                lblMessage.Text = string.Empty;
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private void BindCatagory()
        {
            try
            {
                List<tblTestCatagory> ldtDept = mobjDeptBLL.GetAllTestCatagory();
                ldtDept.Insert(0, new tblTestCatagory { TestCatId = 0, TestCatDescription = "----Select----" });
                ddlTestCatagory.DataSource = ldtDept;
                ddlTestCatagory.DataTextField = "TestCatDescription";
                ddlTestCatagory.DataValueField = "TestCatId";
                ddlTestCatagory.DataBind();
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
                txtDeptCode.Text = String.Empty;
                txtDeptDesc.Text = String.Empty;
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
                txtDeptCode.Text = Convert.ToString(row.Cells[3].Text);
                txtDeptDesc.Text = Convert.ToString(row.Cells[1].Text);
                testid.Value = Convert.ToString(row.Cells[0].Text);
                txtCharge.Text = Convert.ToString(row.Cells[2].Text);
                ListItem item = ddlTestCatagory.Items.FindByText(Convert.ToString(row.Cells[4].Text));
                bool b = Convert.ToBoolean(row.Cells[5].Text);
                if (!b)
                {
                    rdoPathology.Checked = true;
                    rdoRadiology.Checked = false;
                }
                else
                {
                    rdoPathology.Checked = false;
                    rdoRadiology.Checked = true;
                }
                ddlTestCatagory.SelectedValue = item.Value;
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
        protected void BtnAddNewDept_Click(object sender, EventArgs e)
        {
            ddlTestCatagory.SelectedIndex = 0;
            txtDeptDesc.Text = string.Empty;
            txtDeptCode.Text = string.Empty;
            txtCharge.Text = string.Empty;
            btnUpdate.Visible = false;
            BtnSave.Visible = true;
            MultiView1.SetActiveView(View2);
        }

        private void GetTests()
        {
            try
            {
                List<EntityTest> ldtDept = mobjDeptBLL.GetAllTests();
                if (ldtDept.Count > 0)
                {
                    dgvDepartment.DataSource = ldtDept;
                    dgvDepartment.DataBind();
                    Session["DepartmentDetail"] = ldtDept;
                    int lintRowcount = ldtDept.Count;
                    lblRowCount.Text = "<b>Total Records:</b> " + lintRowcount.ToString();
                    pnlShow.Style.Add(HtmlTextWriterStyle.Display, "");
                    hdnPanel.Value = "";
                }
                else
                {
                    hdnPanel.Value = "none";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int lintcnt = 0;
                EntityTest entDept = new EntityTest();
                if (Session["update"].ToString() == ViewState["update"].ToString())
                {
                    entDept.TestName = txtDeptDesc.Text.Trim();
                    entDept.TestCharge = Convert.ToDecimal(txtCharge.Text);
                    entDept.Precautions = txtDeptCode.Text;
                    entDept.IsRadiology = rdoRadiology.Checked ? true : false;
                    entDept.IsPathology = rdoPathology.Checked ? true : false;
                    entDept.TestCatId = Convert.ToInt32(ddlTestCatagory.SelectedValue);
                    if (!mobjDeptBLL.IsRecordExists(entDept))
                    {
                        lintcnt = mobjDeptBLL.InsertTest(entDept);

                        if (lintcnt > 0)
                        {
                            GetTests();
                            lblMessage.Text = "Record Inserted Successfully.";
                            Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                        }
                        else
                        {
                            lblMessage.Text = "Record Not Inserted";
                        }
                    }
                    else
                    {
                        lblMessage.Text = "Record Already Exist.";
                    }
                }
                MultiView1.SetActiveView(View1);
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
                EntityTest entDept = new EntityTest();
                entDept.Precautions = txtDeptCode.Text;
                entDept.TestName = txtDeptDesc.Text;
                entDept.TestId = Convert.ToInt32(testid.Value);
                entDept.TestCharge = Convert.ToDecimal(txtCharge.Text);
                entDept.IsRadiology = rdoRadiology.Checked ? true : false;
                entDept.IsPathology = rdoPathology.Checked ? true : false;
                entDept.TestCatId = Convert.ToInt32(ddlTestCatagory.SelectedValue);
                EntityTest obj = (from tbl in mobjDeptBLL.GetAllTests()
                                  where tbl.TestId == Convert.ToInt32(testid.Value)
                                  && tbl.TestName.ToUpper().Equals(txtDeptDesc.Text.ToUpper())
                                  select tbl).FirstOrDefault();

                if (obj != null)
                {
                    lintCnt = mobjDeptBLL.Update(entDept);
                    GetTests();
                    lblMessage.Text = "Record Updated Successfully";
                }
                else
                {
                    if (!mobjDeptBLL.IsRecordExists(entDept))
                    {
                        lintCnt = mobjDeptBLL.Update(entDept);

                        if (lintCnt > 0)
                        {
                            GetTests();
                            lblMessage.Text = "Record Updated Successfully";
                        }
                        else
                        {
                            lblMessage.Text = "Record Not Updated.";
                        }
                    }
                    else
                    {
                        lblMessage.Text = "Record Already Exist.";
                    }
                }
                MultiView1.SetActiveView(View1);
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
                dgvDepartment.DataSource = (List<EntityTest>)Session["DepartmentDetail"];
                dgvDepartment.DataBind();
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmDepartmentMaster - dgvDepartment_PageIndexChanged(object sender, EventArgs e)", ex);
            }
        }
        protected void dgvDepartment_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvDepartment.PageIndex = e.NewPageIndex;
        }


    }
}