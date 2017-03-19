using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MainProjectHos.Models.BusinessLayer;
using MainProjectHos.Models.DataLayer;
using MainProjectHos.Models.DataModels;
using System.IO;
using System.Data;
using System.Drawing;

namespace MainProjectHos
{
    public partial class frmPathology : System.Web.UI.Page
    {
        PathologyBLL mobjDeptBLL = new PathologyBLL();
        public int labid { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] != null)
            {
                EntityLogin entLogin = (EntityLogin)Session["user"];

                List<EntityFormMaster> entFormAcc = (List<EntityFormMaster>)Session["AccessibleForm"];
                int Access = (from tbl in entFormAcc
                              where tbl.FormName.Contains("frmPathology")
                              select tbl).Count();

                if (Access > 0)
                {
                    if (!Page.IsPostBack)
                    {
                        Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                        BindPatientList();
                        BindPatientPathologyTest();
                        lblFinalComment.Visible = false;
                        txtFinalComment.Visible = false;
                        lblMessage.Text = string.Empty;
                        imgUpload.Visible = false;
                        lblUpload.Visible = false;
                        BtnimgUpload.Visible = false;
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

        private void BindPatientPathologyTest()
        {
            DataTable ldtShift = mobjDeptBLL.GetPathologyTests();
            if (ldtShift.Rows.Count > 0)
            {
                dgvTestParameter.DataSource = ldtShift;
                dgvTestParameter.DataBind();
                int lintRowcount = ldtShift.Rows.Count;
            }
        }

        private void BindPatientList()
        {
            List<TestAllocation> lstPat = mobjDeptBLL.GetPatientList();
            ddlPatient.DataSource = lstPat;
            lstPat.Insert(0, new TestAllocation() { AdmitId = 0, PatientName = "--Select--" });
            ddlPatient.DataValueField = "AdmitId";
            ddlPatient.DataTextField = "PatientName";
            ddlPatient.DataBind();
        }


        protected void BtnClose_Click(object sender, EventArgs e)
        {
            try
            {
                Clear();
                MultiView1.SetActiveView(View1);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void txtResult_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox imgEdit = (TextBox)sender;
                GridViewRow row = (GridViewRow)imgEdit.NamingContainer;

                if (!string.IsNullOrEmpty(imgEdit.Text))
                {
                    DropDownList ddlComment = (DropDownList)row.FindControl("ddlComment");
                    if (ddlComment != null)
                    {
                        decimal Result = 0;
                        bool b = decimal.TryParse(imgEdit.Text, out Result);
                        if (b)
                        {
                            decimal min = Convert.ToDecimal(row.Cells[1].Text);
                            decimal max = Convert.ToDecimal(row.Cells[2].Text);
                            if (Result > min && Result < max)
                            {
                                ddlComment.SelectedIndex = 3;
                            }
                            else if (Result < min)
                            {
                                ddlComment.SelectedIndex = 2;
                            }
                            else if (Result > max)
                            {
                                ddlComment.SelectedIndex = 1;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }


        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton imgEdit = (ImageButton)sender;
                GridViewRow row = (GridViewRow)imgEdit.NamingContainer;
                Session["LabId"] = Convert.ToInt32(dgvTestParameter.DataKeys[row.RowIndex].Value);
                int id = Convert.ToInt32(row.Cells[0].Text);
                Session["T_Id"] = id;
                int Dis = mobjDeptBLL.CheckPatientDischarge(id);
                if (Dis > 0)
                {
                    lblMessage.Text = "Patient Is Already Discharged So Cannot Edit Tests";
                    MultiView1.SetActiveView(View1);
                }
                else
                {
                    lblMessage.Text = string.Empty;
                    DataTable tblPath = mobjDeptBLL.SelectPatientTest(Convert.ToInt32(Session["LabId"]));
                    if (tblPath.Rows.Count > 0)
                    {
                        ListItem item = ddlPatient.Items.FindByText(Convert.ToString(tblPath.Rows[0]["FullName"]));
                        ddlPatient.SelectedValue = item.Value;
                        ddlPatient_SelectedIndexChanged1(sender, e);
                        ListItem lst = ddlTest.Items.FindByText(Convert.ToString(tblPath.Rows[0]["TestName"]));
                        ddlTest.SelectedValue = lst.Value;
                        ddlTest_SelectedIndexChanged(sender, e);
                        ddlPatient.Enabled = false;
                        ddlTest.Enabled = false;
                        foreach (GridViewRow var in GridView1.Rows)
                        {
                            var textbox = var.FindControl("txtResult") as TextBox;
                            textbox.Text = Convert.ToString(tblPath.Rows[var.RowIndex]["Result"]);
                            var textboxcomm = var.FindControl("ddlComment") as DropDownList;
                            ListItem comment = textboxcomm.Items.FindByText(Convert.ToString(tblPath.Rows[var.RowIndex]["Comment"]));
                            textboxcomm.SelectedValue = comment.Value;
                            var ddl = var.FindControl("ddlUnit") as DropDownList;
                            ListItem lstunit = ddl.Items.FindByText(Convert.ToString(tblPath.Rows[var.RowIndex]["unitname"]));
                            ddl.SelectedValue = lstunit.Value;
                            var ddltest = var.FindControl("ddlTestMethod") as DropDownList;
                            ListItem lsttest = ddltest.Items.FindByText(Convert.ToString(tblPath.Rows[var.RowIndex]["TestMethodName"]));
                            ddltest.SelectedValue = lsttest.Value;
                        }
                        txtFinalComment.Text = Convert.ToString(tblPath.Rows[0]["FinalComment"]);
                        btnUpdate.Visible = true;
                        BtnSave.Visible = false;
                    }
                    MultiView1.SetActiveView(View2);
                }

            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }

        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            ViewState["update"] = Session["update"];
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            ImageButton imgEdit = (ImageButton)sender;
            GridViewRow row = (GridViewRow)imgEdit.NamingContainer;
            Session["LabId"] = Convert.ToInt32(dgvTestParameter.DataKeys[row.RowIndex].Value);
            Session["ReportType"] = "Pathology";
            Response.Redirect("PathalogyReport/PathologyReport.aspx", false);
        }

        protected void BtnAddNewDept_Click(object sender, EventArgs e)
        {
            btnUpdate.Visible = false;
            BtnSave.Visible = true;
            ddlPatient.Enabled = true;
            ddlTest.Enabled = true;
            Clear();
            MultiView1.SetActiveView(View2);

        }

        private void Clear()
        {
            ddlPatient.SelectedIndex = 0;
            List<TestAllocation> lst = new List<TestAllocation>();
            ddlTest.DataTextField = "TestName";
            ddlTest.DataValueField = "TestId";
            ddlTest.DataSource = lst;
            ddlTest.DataBind();
            GridView1.DataSource = new List<EntityPathology>();
            GridView1.DataBind();
        }

        protected void BtnimgUpload_Click(object sender, EventArgs e)
        {
            if (imgUpload.HasFile)
            {
                if (Path.GetExtension(imgUpload.PostedFile.FileName) == ".jpg"
                    || Path.GetExtension(imgUpload.PostedFile.FileName) == ".bmp"
                    || Path.GetExtension(imgUpload.PostedFile.FileName) == ".png"
                    || Path.GetExtension(imgUpload.PostedFile.FileName) == ".jpeg")
                {
                    HttpPostedFile Fs = imgUpload.PostedFile;
                    Session["Photo"] = Fs;
                }
                else
                {
                    lblMsg.Text = "Please Select Only Image File With Extension .JPG, .BMP, .PNG ...";
                }
            }
        }
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlPatient.SelectedIndex == 0)
                {
                    lblMsg.Text = "Please Select Patient";
                    ddlPatient.SelectedIndex = 0;
                    ddlPatient.Focus();
                }
                if (ddlTest.SelectedIndex == 0)
                {
                    lblMsg.Text = "Please Select Test";
                    ddlTest.SelectedIndex = 0;
                    ddlTest.Focus();
                }
                if (txtFinalComment.Text == null)
                {
                    lblMsg.Text = "Please Enter Final Comment";
                    ddlTest.SelectedIndex = 0;
                    ddlTest.Focus();
                    return;
                }
                bool flag = false;
                List<tblPathologyDetail> lst = new List<tblPathologyDetail>();
                foreach (GridViewRow item in GridView1.Rows)
                {
                    var textbox = item.FindControl("txtResult") as TextBox;
                    var a = textbox.Text;
                    if (string.IsNullOrEmpty(a))
                    {
                        flag = true;
                        break;
                    }
                    var textboxCommnet = item.FindControl("ddlComment") as DropDownList;
                    var comm = textboxCommnet.SelectedItem.Text;
                    if (string.IsNullOrEmpty(comm))
                    {
                        flag = true;
                        break;
                    }
                    var ddl = item.FindControl("ddlUnit") as DropDownList;
                    int unit = Convert.ToInt32(ddl.SelectedValue);
                    if (unit == 0)
                    {
                        flag = true;
                        break;
                    }
                    var ddltestMeth = item.FindControl("ddlTestMethod") as DropDownList;
                    int testmethod = Convert.ToInt32(ddltestMeth.SelectedValue);
                    if (testmethod == 0)
                    {
                        flag = true;
                        break;
                    }
                    lst.Add(new tblPathologyDetail { TestParaId = Convert.ToInt32(GridView1.DataKeys[item.RowIndex].Value), Result = Convert.ToDecimal(a), UnitId = Convert.ToInt32(unit), TestMethodId = Convert.ToInt32(testmethod), Comment = comm, Isdelete = false });
                }

                if (flag)
                {
                    lblMsg.Text = "Please Fill Valid & Complete Data";
                }
                else
                {
                    EntityLogin entLogin = (EntityLogin)Session["user"];

                    PathologyBLL PBll = new PathologyBLL();
                    tblPathology pathology = new tblPathology();
                    pathology.PatientId = Convert.ToInt32(ddlPatient.SelectedValue);
                    pathology.TestId = Convert.ToInt32(ddlTest.SelectedValue);
                    pathology.TestDate = DateTime.Now.Date;
                    pathology.FinalComment = txtFinalComment.Text;
                    pathology.TestDoneById = entLogin.PKId;
                    HttpPostedFile File = (HttpPostedFile)Session["Photo"];
                    if (File != null)
                    {
                        string appPath = Server.MapPath("~/PathologyImages/");
                        string Id = Convert.ToString(mobjDeptBLL.GetLabId());
                        File.SaveAs(appPath + Id + ".jpg");

                    }

                    int i = PBll.Save(pathology, lst);
                    lblMessage.Text = "Record Saved Sucessfully";
                    BindPatientPathologyTest();
                    MultiView1.SetActiveView(View1);
                }

            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }

        }


        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            EntityLogin entLogin = (EntityLogin)Session["user"];
            tblPathology tblpath = new tblPathology();
            tblpath.PatientId = Convert.ToInt32(Session["T_Id"]);
            tblpath.TestId = Convert.ToInt32(ddlTest.SelectedValue);
            tblpath.TestDate = DateTime.Now.Date;
            tblpath.TestDoneById = entLogin.PKId;
            tblpath.LabId = Convert.ToInt32(Session["LabId"]);
            tblpath.FinalComment = Convert.ToString(txtFinalComment.Text);
            List<tblPathologyDetail> lst = new List<tblPathologyDetail>();
            foreach (GridViewRow item in GridView1.Rows)
            {
                var textbox = item.FindControl("txtResult") as TextBox;
                var a = textbox.Text;
                var textboxCommnet = item.FindControl("ddlComment") as DropDownList;
                var comm = textboxCommnet.SelectedItem.Text;
                var ddl = item.FindControl("ddlUnit") as DropDownList;
                var unit = ddl.SelectedValue;
                var ddltestMeth = item.FindControl("ddlTestMethod") as DropDownList;
                var testmethod = ddltestMeth.SelectedValue;
                lst.Add(new tblPathologyDetail
                {
                    LabId = Convert.ToInt32(Session["LabId"]),
                    TestParaId = Convert.ToInt32(GridView1.DataKeys[0].Value),
                    Result = Convert.ToDecimal(a),
                    UnitId = Convert.ToInt32(unit),
                    TestMethodId = Convert.ToInt32(testmethod),
                    Comment = comm,
                    Isdelete = false
                });
            }
            mobjDeptBLL.Update(lst, tblpath);
            BindPatientPathologyTest();
            lblMessage.Text = "Record Updated Sucessfully......";
            MultiView1.SetActiveView(View1);
        }


        protected void dgvTestParameter_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dgvTestParameter.DataSource = (DataTable)Session["DepartmentDetail"];
                dgvTestParameter.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void dgvTestParameter_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvTestParameter.PageIndex = e.NewPageIndex;
        }

        public void BindAllocatedTestToPatient()
        {
            try
            {
                if (ddlPatient.SelectedIndex > 0 && ddlTest.SelectedIndex > 0)
                {
                    List<EntityPathology> lst = new PathologyBLL().GetAllocatedTestsToPatient(Convert.ToInt32(ddlPatient.SelectedValue), Convert.ToInt32(ddlTest.SelectedValue));
                    GridView1.DataSource = lst;
                    lblFinalComment.Visible = true;
                    txtFinalComment.Visible = true;
                    GridView1.DataBind();
                }
                else
                {
                    if (ddlPatient.SelectedIndex > 0 && ddlTest.SelectedIndex == 0)
                    {
                        ddlTest.Focus();
                        return;
                    }
                    if (ddlPatient.SelectedIndex == 0 && ddlTest.SelectedIndex > 0)
                    {
                        ddlPatient.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }


        protected void ddlPatient_SelectedIndexChanged1(object sender, EventArgs e)
        {
            if (ddlPatient.SelectedIndex > 0)
            {
                Session["pat_Id"] = ddlPatient.SelectedIndex;
                List<TestAllocation> lst = mobjDeptBLL.GetAllocatedTests(Convert.ToInt32(ddlPatient.SelectedValue));
                lst.Insert(0, new TestAllocation { TestId = 0, TestName = "---Select---" });

                ddlTest.DataTextField = "TestName";
                ddlTest.DataValueField = "TestId";
                ddlTest.DataSource = lst;
                ddlTest.DataBind();
            }
        }

        protected void ddlTest_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["Test_ID"] = ddlTest.SelectedValue;
            bool Radio = mobjDeptBLL.GetTestStatus(Convert.ToInt32(Session["Test_ID"]));
            if (Radio == true)
            {
                imgUpload.Visible = true;
                lblUpload.Visible = true;
                BtnimgUpload.Visible = true;
            }
            else
            {
                imgUpload.Visible = false;
                lblUpload.Visible = false;
                BtnimgUpload.Visible = false;
            }
            BindAllocatedTestToPatient();
        }


        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DropDownList groupdwn = (DropDownList)e.Row.FindControl("ddlUnit");
                    List<EntityPathology> lstPat = mobjDeptBLL.GetUnit();
                    groupdwn.DataSource = lstPat;
                    lstPat.Insert(0, new EntityPathology() { Unit = 0, unitname = "--Select--" });
                    groupdwn.DataValueField = "Unit";
                    groupdwn.DataTextField = "unitname";
                    groupdwn.DataBind();
                    DropDownList ddlTestMethod = (DropDownList)e.Row.FindControl("ddlTestMethod");
                    List<EntityPathology> lst = mobjDeptBLL.GetTestMethod();
                    ddlTestMethod.DataSource = lst;
                    lst.Insert(0, new EntityPathology() { TestMethodId = 0, TestMethodName = "--Select--" });
                    ddlTestMethod.DataValueField = "TestMethodId";
                    ddlTestMethod.DataTextField = "TestMethodName";
                    ddlTestMethod.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSearch.Text))
                {
                    SelectPathologyDetail(txtSearch.Text);
                }
                else
                {
                    lblMessage.Text = "Please fill search text.";
                    txtSearch.Focus();
                }

            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
        private void SelectPathologyDetail(string Prefix)
        {
            PathologyBLL Pathology = new PathologyBLL();
            List<EntityPathology> lst = Pathology.SearchPathologyDetails(Prefix);
            if (lst != null)
            {
                dgvTestParameter.DataSource = lst;
                dgvTestParameter.DataBind();

                lblRowCount.Text = string.Empty;
            }
        }


        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtSearch.Text = string.Empty;
            BindPatientPathologyTest();
        }

    }
}