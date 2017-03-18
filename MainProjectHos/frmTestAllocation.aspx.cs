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
    public partial class frmTestAllocation : System.Web.UI.Page
    {
        ShiftBLL mobjDeptBLL = new ShiftBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] != null)
            {
                EntityLogin entLogin = (EntityLogin)Session["user"];

                List<EntityFormMaster> entFormAcc = (List<EntityFormMaster>)Session["AccessibleForm"];
                int Access = (from tbl in entFormAcc
                              where tbl.FormName.Contains("frmTestAllocation")
                              select tbl).Count();

                if (Access > 0)
                {
                    if (!Page.IsPostBack)
                    {
                        BindPatients();
                        GetInvoiceList();
                        BindTests();
                        MultiView1.SetActiveView(View2);
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

        private void BindPatients()
        {
            try
            {
                List<EntityPatientMaster> tblPatient = new clsTestAllocation().GetAllocatedPatient();
                tblPatient.Insert(0, new EntityPatientMaster() { PatientId = 0, FullName = "----Select----" });
                ddlPatient.DataSource = tblPatient;
                ddlPatient.DataTextField = "FullName";
                ddlPatient.DataValueField = "PatientId";
                ddlPatient.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void dgvTestInvoice_PageIndexChanged(object sender, EventArgs e)
        {
            List<EntityTestInvoice> tblPatient = (List<EntityTestInvoice>)Session["TestInvoice"];
            dgvTestInvoice.DataSource = tblPatient;
            dgvTestInvoice.DataBind();
        }

        protected void dgvTestInvoice_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvTestInvoice.PageIndex = e.NewPageIndex;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                List<EntityTestInvoice> tblPatient = new clsTestAllocation().GetTestInvoiceList(txtSearch.Text);
                Session["TestInvoice"] = tblPatient;
                dgvTestInvoice.DataSource = tblPatient;
                dgvTestInvoice.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void ddlPatientName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPatient.SelectedIndex > 0)
            {
                int Val = Convert.ToInt32(ddlPatient.SelectedValue);
                //Session["Pat_Id"] = Val;
                EntityPatientAlloc objTxt = new PatientAllocDocBLL().GetPatientType(Val);
                //CalDOBDate.StartDate = objTxt.AdmitDate;
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                Session["Status"] = "Edit";
                lblMessage.Text = string.Empty;
                ImageButton imgEdit = (ImageButton)sender;
                GridViewRow cnt = (GridViewRow)imgEdit.NamingContainer;
                Session["TestInvoiceId"] = Convert.ToInt32(cnt.Cells[0].Text);
                EntityPatientAdmit entPat = mobjDeptBLL.ChechDischargeDone(Convert.ToInt32(cnt.Cells[0].Text));
                if (entPat != null)
                {
                    List<TestAllocation> lst = new clsTestAllocation().GetTestInvoiceDetails(Convert.ToInt32(cnt.Cells[0].Text));
                    if (cnt != null)
                    {
                        ListItem item = ddlPatient.Items.FindByText(Convert.ToString(cnt.Cells[2].Text));
                        ddlPatient.SelectedValue = item.Value;
                        txtAllocDate.Text = string.Format("{0:MM/dd/yyyy}", Convert.ToDateTime(cnt.Cells[1].Text));
                        txtNetAmount.Text = Convert.ToString(cnt.Cells[4].Text);
                        if (lst.Count > 0)
                        {
                            txtDiscountAmt.Text = Convert.ToString(lst[0].DiscountAmount);
                            txtTotal.Text = Convert.ToString(Convert.ToDecimal(txtNetAmount.Text) + Convert.ToDecimal(txtDiscountAmt.Text));
                            txtDiscount.Text = string.Format("{0:}", Convert.ToDecimal(txtDiscountAmt.Text) / Convert.ToDecimal(txtTotal.Text) * 100);
                        }
                        EntityCustomerTransaction transactionStatus = mobjDeptBLL.GetTransactionStatus(Convert.ToInt32(Session["TestInvoiceId"]));
                        if (transactionStatus != null)
                        {
                            chkIsCash.Checked = Convert.ToBoolean(transactionStatus.IsCash);
                        }
                        foreach (TestAllocation objTest in lst)
                        {
                            foreach (GridViewRow row in dgvAllTests.Rows)
                            {
                                if (Convert.ToInt32(dgvAllTests.DataKeys[row.RowIndex].Value) == objTest.TestId)
                                {
                                    CheckBox chk = (CheckBox)row.FindControl("chkSelect");
                                    chk.Checked = true;
                                    break;
                                }
                            }
                        }
                    }
                    BtnUpdate.Visible = true;
                    BtnSave.Visible = false;
                    MultiView1.SetActiveView(View1);
                }
                else
                {
                    lblMsg.Text = "Patient Is Already Discharged";
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }

        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton imgEdit = (ImageButton)sender;
                GridViewRow cnt = (GridViewRow)imgEdit.NamingContainer;
                Session["TestInvoiceID"] = Convert.ToInt32(dgvTestInvoice.DataKeys[cnt.RowIndex].Value);
                Session["ReportType"] = "TestInvoice";
                Response.Redirect("PathalogyReport/PathologyReport.aspx", false);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private void BindTests()
        {
            DataTable ldtShift = new TestBLL().GetAllTest();
            if (ldtShift.Rows.Count > 0)
            {
                dgvAllTests.DataSource = ldtShift;
                dgvAllTests.DataBind();
                Session["DepartmentDetail"] = ldtShift;
                int lintRowcount = ldtShift.Rows.Count;
                //lblRowCount.Text = "<b>Total Records:</b> " + lintRowcount.ToString();
                pnlShow.Style.Add(HtmlTextWriterStyle.Display, "");
                hdnPanel.Value = "";
            }
            else
            {
                hdnPanel.Value = "none";
            }
        }

        protected void BtnClose_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                ddlPatient.SelectedIndex = 0;
                txtTotal.Text = string.Empty;
                txtDiscount.Text = string.Empty;
                txtDiscountAmt.Text = string.Empty;
                txtNetAmount.Text = string.Empty;
                txtAllocDate.Text = string.Empty;
                MultiView1.SetActiveView(View2);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                ddlPatient.SelectedIndex = 0;
                txtTotal.Text = string.Empty;
                txtDiscount.Text = string.Empty;
                txtDiscountAmt.Text = string.Empty;
                BindTests();
                txtNetAmount.Text = string.Empty;
                BtnUpdate.Visible = false;
                BtnSave.Visible = true;
                lblMessage.Text = string.Empty;
                MultiView1.SetActiveView(View1);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtSearch.Text = string.Empty;
            dgvTestInvoice.PageIndex = 0;
            GetInvoiceList();
        }

        private void GetInvoiceList()
        {
            try
            {
                List<EntityTestInvoice> tblPatient = new clsTestAllocation().GetTestInvoiceList();
                dgvTestInvoice.DataSource = tblPatient;
                Session["TestInvoice"] = tblPatient;
                dgvTestInvoice.DataBind();
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
                if (dgvAllTests.Rows.Count == 0)
                {
                    lblMessage.Text = "Please Add Test before Allocate to Patient.";
                    ddlPatient.Focus();
                    return;
                }

                List<TestAllocation> lst = new List<TestAllocation>();
                tblTestInvoice obj = new tblTestInvoice();
                obj.Amount = Convert.ToDecimal(txtNetAmount.Text);
                obj.Discount = Convert.ToDecimal(txtDiscountAmt.Text);
                obj.PatientId = Convert.ToInt32(ddlPatient.SelectedValue);
                obj.TestInvoiceDate = Convert.ToDateTime(txtAllocDate.Text);

                foreach (GridViewRow item in dgvAllTests.Rows)
                {
                    CheckBox chk = (CheckBox)item.FindControl("chkSelect");
                    if (chk.Checked)
                    {
                        lst.Add(new TestAllocation() { IsTestDone = false, TestId = Convert.ToInt32(dgvAllTests.DataKeys[item.RowIndex].Value), Charges = Convert.ToDecimal(item.Cells[2].Text), TestInvoiceNo = Convert.ToInt32(Session["TestInvoiceId"]) });
                    }
                }
                bool IsCash = chkIsCash.Checked ? true : false;

                int i = new clsTestAllocation().Save(lst, obj, IsCash);

                lblMsg.Text = "Record Saved Successfully";
                BindTests();
                GetInvoiceList();
                //Response.Redirect("frmNewIPDPatient.aspx", false);
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
            MultiView1.SetActiveView(View2);
        }

        protected void dgvAllTests_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType != DataControlRowType.Header || e.Row.RowType != DataControlRowType.Footer)
                {
                    CheckBox chk = (CheckBox)e.Row.FindControl("chkSelect");
                    chk.Attributes.Add("onclick", "CalculateSum()");
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                List<TestAllocation> lst = new List<TestAllocation>();
                tblTestInvoice obj = new tblTestInvoice();
                obj.Amount = Convert.ToDecimal(txtNetAmount.Text);
                obj.TestInvoiceNo = Convert.ToInt32(Session["TestInvoiceId"]);
                obj.Discount = Convert.ToDecimal(txtDiscountAmt.Text);
                obj.PatientId = Convert.ToInt32(ddlPatient.SelectedValue);
                string[] arr = txtAllocDate.Text.Split('/');
                obj.TestInvoiceDate = new DateTime(Convert.ToInt32(arr[arr.Length - 1]), Convert.ToInt32(arr[0]), Convert.ToInt32(arr[1]));
                foreach (GridViewRow item in dgvAllTests.Rows)
                {
                    CheckBox chk = (CheckBox)item.FindControl("chkSelect");
                    if (chk.Checked)
                    {
                        lst.Add(new TestAllocation() { IsTestDone = false, TestId = Convert.ToInt32(dgvAllTests.DataKeys[item.RowIndex].Value), Charges = Convert.ToDecimal(item.Cells[2].Text) });
                    }
                }
                bool IsCash = chkIsCash.Checked ? true : false;
                int i = new clsTestAllocation().Update(lst, obj, IsCash);
                GetInvoiceList();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
            MultiView1.SetActiveView(View2);
        }
    }
}