using MainProjectHos.Models.BusinessLayer;
using MainProjectHos.Models.DataLayer;
using MainProjectHos.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MainProjectHos.Billing
{
    public partial class frmAdvancePayment : System.Web.UI.Page
    {
        CustomerTransactionBLL mobjDeptBLL = new CustomerTransactionBLL();
        AdvancePaymentBLL mobjAdvanceBLL = new AdvancePaymentBLL();
        public bool flag = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] != null)
            {
                EntityLogin entLogin = (EntityLogin)Session["user"];

                List<EntityFormMaster> entFormAcc = (List<EntityFormMaster>)Session["AccessibleForm"];
                int Access = (from tbl in entFormAcc
                              where tbl.FormName.Contains("frmAdvancePayment")
                              select tbl).Count();

                if (Access > 0)
                {
                    if (!Page.IsPostBack)
                    {
                        Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                        BindPatients();
                        GetCustTransactionList();
                        MultiView1.SetActiveView(View2);
                    }
                }
                else
                {
                    Response.Redirect("~/frmAdminMain.aspx", false);
                }
            }
            else
            {
                Response.Redirect("~/frmlogin.aspx", false);
            }
        }

        private void BindPatients()
        {
            try
            {
                List<EntityPatientMaster> tblpatient = new CustomerTransactionBLL().GetAllocatedPatientInfo();
                tblpatient.Insert(0, new EntityPatientMaster() { PatientId = 0, FullName = "----Select----" });
                ddlPatient.DataSource = tblpatient;
                ddlPatient.DataTextField = "FullName";
                ddlPatient.DataValueField = "PatientId";
                ddlPatient.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void dgvCustomer_PageIndexChanged(object sender, EventArgs e)
        {
            List<EntityCustomerTransaction> tblPatient = (List<EntityCustomerTransaction>)Session["CustomerTransaction"];
            //dgvTestInvoice.DataSource = tblPatient;
            //dgvTestInvoice.DataBind();
        }

        protected void dgvCustomerTansact(object sender, GridViewPageEventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            ImageButton imgEdit = (ImageButton)sender;
            GridViewRow row = (GridViewRow)imgEdit.NamingContainer;
            Session["ReceiptNo"] = Convert.ToInt32(dgvCustTransaction.DataKeys[row.RowIndex].Value);
            Session["ReportType"] = "Refund";
            Response.Redirect("~/PathalogyReport/PathologyReport.aspx", false);
        }

        protected void BtnClose_Click(object sender, EventArgs e)
        {
            MultiView1.SetActiveView(View2);
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                ddlPatient.SelectedIndex = 0;
                txtTransactDate.Enabled = false;
                txtTransactDate.Text = DateTime.UtcNow.Date.ToString("MM/dd/yyyy");
                txtTotal.Text = string.Empty;
                txtChequeNo.Text = string.Empty;
                txtChequeDate.Text = string.Empty;
                txtBankName.Text = string.Empty;
                BtnUpdate.Visible = false;
                BtnSave.Visible = true;
                lblMessage.Text = string.Empty;
                MultiView1.SetActiveView(View1);

                CheckList(false);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            GetCustTransactionList();
        }

        private void GetCustTransactionList()
        {
            try
            {
                List<EntityCustomerTransaction> tblPatient = new AdvancePaymentBLL().GetCustomerTransactionList();
                if (tblPatient.Count > 0)
                {
                    dgvCustTransaction.DataSource = tblPatient;
                    dgvCustTransaction.DataBind();
                    Session["DepartmentDetail"] = tblPatient;
                    int lintRowcount = tblPatient.Count;
                    lblRowCount1.Text = "<b>Total Records:</b> " + lintRowcount.ToString();
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

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlPatient.SelectedIndex == 0)
                {
                    lblMsg.Text = "Please Select Patient Name";
                    ddlPatient.Focus();
                    return;
                }
                else
                {
                    if ((Convert.ToDecimal(txtPayAmount.Text).CompareTo(Convert.ToDecimal(txtTotal.Text)) == 1) || (Convert.ToDecimal(txtPayAmount.Text).CompareTo(Convert.ToDecimal(txtTotal.Text)) == -1))
                    {
                        lblMsg.Text = "Please Enter Proper Full Amount";
                        txtPayAmount.Text = string.Empty;
                        txtPayAmount.Focus();
                        return;
                    }
                    else
                    {
                        if (IsCheque.Checked)
                        {
                            if (string.IsNullOrEmpty(txtChequeNo.Text))
                            {
                                lblMsg.Text = "Please Enter Cheque Number";
                                txtChequeNo.Focus();
                                return;
                            }
                            else if (string.IsNullOrEmpty(txtChequeDate.Text))
                            {
                                lblMsg.Text = "Please Enter Cheque Date";
                                txtChequeDate.Focus();
                                return;
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(txtBankName.Text))
                                {
                                    lblMsg.Text = "Please Enter Bank Name";
                                    txtBankName.Focus();
                                    return;
                                }
                            }
                        }
                        else
                        {
                            List<EntityCustomerTransaction> lst = new List<EntityCustomerTransaction>();
                            EntityCustomerTransaction entcust = new EntityCustomerTransaction();
                            entcust.PatientId = Convert.ToInt32(ddlPatient.SelectedValue);
                            entcust.EmpName = Convert.ToString(Session["AdminName"]);
                            entcust.ReceiptDate = Convert.ToDateTime(txtTransactDate.Text);
                            entcust.BillRefNo = Convert.ToString(txtBillRefNo.Text);
                            entcust.PayAmount = Convert.ToDecimal(0);
                            if (IsCash.Checked)
                            {
                                entcust.IsCash = true;
                                entcust.ISCheque = false;
                                entcust.IsCard = false;
                                entcust.IsRTGS = false;
                                entcust.BillAmount = Convert.ToDecimal(txtTotal.Text);
                            }
                            if (IsCheque.Checked)
                            {
                                entcust.ChequeDate = Convert.ToDateTime(txtChequeDate.Text);
                                entcust.ChequeNo = Convert.ToString(txtChequeNo.Text);
                                entcust.BankName = Convert.ToString(txtBankName.Text);
                                entcust.BillAmount = Convert.ToDecimal(txtTotal.Text);
                            }
                            if (IsCard.Checked)
                            {
                                entcust.IsCash = false;
                                entcust.ISCheque = false;
                                entcust.IsCard = true;
                                entcust.IsRTGS = false;
                                entcust.BankRefNo = Convert.ToString(txtBankRefNo.Text);
                                entcust.BillAmount = Convert.ToDecimal(txtTotal.Text);
                            }
                            if (IsRTGS.Checked)
                            {
                                entcust.IsCash = false;
                                entcust.ISCheque = false;
                                entcust.IsCard = false;
                                entcust.IsRTGS = true;
                                entcust.BankRefNo = Convert.ToString(txtBankRefNo.Text);
                                entcust.BillAmount = Convert.ToDecimal(txtTotal.Text);
                            }
                            int i = new AdvancePaymentBLL().Save(entcust, IsCash.Checked);

                            if (i > 0)
                            {
                                lblMessage.Text = "Record Saved Successfully";
                            }
                            else
                            {
                                lblMessage.Text = "Record Not Saved";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
            GetCustTransactionList();
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
            
        }
        protected void IsCash_CheckedChanged(object sender, EventArgs e)
        {
            if (IsCash.Checked)
            {
                CheckList(false);
            }
            if (IsCheque.Checked)
            {
                CheckList(true);
                lblBankRefNo.Visible = false;
                txtBankRefNo.Visible = false;
            }
            if (IsCard.Checked)
            {
                CheckList(false);
                lblBankRefNo.Visible = true;
                txtBankRefNo.Visible = true;
            }
            if (IsRTGS.Checked)
            {
                CheckList(false);
                lblBankRefNo.Visible = true;
                txtBankRefNo.Visible = true;
            }
        }

        private void CheckList(bool flag)
        {
            lblCashAmount.Visible = true;
            txtPayAmount.Visible = true;
            lblChequeDate.Visible = flag;
            lblchequeNo.Visible = flag;
            lblBankName.Visible = flag;
            txtChequeDate.Visible = flag;
            txtChequeNo.Visible = flag;
            txtBankName.Visible = flag;
            lblBankRefNo.Visible = flag;
            txtBankRefNo.Visible = flag;
        }

        protected void dgvCustTransact_DataBound(object sender, EventArgs e)
        {

        }

        protected void ddlPatient_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlPatient.SelectedIndex > 0)
                {
                    List<tblCustomerTransaction> lst = new CustomerTransactionBLL().GetPatientTransByPatientAdmitId(Convert.ToInt32(ddlPatient.SelectedValue));
                    decimal DR = Convert.ToDecimal(lst.Sum(p => p.BillAmount));
                    decimal CR = Convert.ToDecimal(lst.Sum(p => p.PayAmount)) + Convert.ToDecimal(lst.Sum(p => p.AdvanceAmount));
                    if (CR > DR)
                    {
                        txtTotal.Text = Convert.ToString(CR - DR);
                    }
                    else
                    {
                        txtTotal.Text = Convert.ToString(DR - CR);
                    }
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void dgvCustTransaction_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}