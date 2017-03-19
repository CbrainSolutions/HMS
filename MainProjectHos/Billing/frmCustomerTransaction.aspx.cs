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

namespace MainProjectHos.Billing
{
    public partial class frmCustomerTransaction : System.Web.UI.Page
    {
        CustomerTransactionBLL mobjDeptBLL = new CustomerTransactionBLL();
        public bool flag = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] != null)
            {
                EntityLogin entLogin = (EntityLogin)Session["user"];

                List<EntityFormMaster> entFormAcc = (List<EntityFormMaster>)Session["AccessibleForm"];
                int Access = (from tbl in entFormAcc
                              where tbl.FormName.Contains("frmCustomerTransaction")
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
                List<EntityPatientMaster> tblpatient = new CustomerTransactionBLL().GetAllocatedPatient();
                tblpatient.Insert(0, new EntityPatientMaster() { PatientId = 0, FullName = "----Select----" });
                ddlPatient.DataSource = tblpatient;
                ddlPatient.DataTextField = "Fullname";
                ddlPatient.DataValueField = "PatientId";
                ddlPatient.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void txtNew_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtNew.Text))
                {
                    GetPatientListsearch(txtNew.Text);
                }
                else
                {
                    BindPatients();
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private void GetPatientListsearch(string Prefix)
        {
            try
            {
                OPDPatientMasterBLL mobjPatientMasterBLL = new OPDPatientMasterBLL();
                List<EntityPatientMaster> ldtRequisition = mobjPatientMasterBLL.GetAllPatientssearch(Prefix);
                ldtRequisition.Insert(0, new EntityPatientMaster() { PatientId = 0, FullName = "----Select----" });
                ddlPatient.DataSource = ldtRequisition;
                ddlPatient.DataTextField = "FullName";
                ddlPatient.DataValueField = "PatientId";
                ddlPatient.DataBind();
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                List<EntityCustomerTransaction> tblPatient = mobjDeptBLL.GetCustomerTransactionList(txtSearch.Text);
                //Session["Status"] = "Search";
                Session["DepartmentDetail"] = tblPatient;
                dgvCustTransaction.DataSource = tblPatient;
                dgvCustTransaction.DataBind();
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
                Session["Status"] = "Edit";
                lblMessage.Text = string.Empty;
                ImageButton imgEdit = (ImageButton)sender;
                GridViewRow cnt = (GridViewRow)imgEdit.NamingContainer;
                Session["ReceiptNo"] = Convert.ToInt32(cnt.Cells[0].Text);

                if (cnt != null)
                {
                    ListItem item = ddlPatient.Items.FindByText(Convert.ToString(cnt.Cells[2].Text));
                    ddlPatient.SelectedValue = item.Value;
                    ddlPatient_SelectedIndexChanged(sender, e);
                    txtTransactDate.Text = string.Format("{0:MM/dd/yyyy}", Convert.ToDateTime(cnt.Cells[1].Text));
                    txtAdvanceAmount.Text = Convert.ToString(cnt.Cells[4].Text);
                    txtPayAmount.Text = Convert.ToString(cnt.Cells[5].Text);
                    EntityCustomerTransaction transaction = new CustomerTransactionBLL().GetReceiptDetails(Convert.ToInt32(cnt.Cells[0].Text));
                    if (transaction != null)
                    {
                        if (Convert.ToBoolean(transaction.ISCheque))
                        {
                            IsCheque.Checked = Convert.ToBoolean(transaction.ISCheque);
                            IsCash_CheckedChanged(sender, e);
                            txtBankName.Text = transaction.BankName;
                            txtChequeNo.Text = transaction.ChequeNo;
                            txtChequeDate.Text = string.Format("{0:MM/dd/yyyy}", transaction.ChequeDate);
                        }
                        else
                        {
                            IsCheque.Checked = Convert.ToBoolean(transaction.ISCheque);
                            IsCard.Checked = true;
                            IsCash_CheckedChanged(sender, e);
                            txtBankName.Text = transaction.BankName;
                            txtChequeNo.Text = transaction.ChequeNo;
                            txtChequeDate.Text = string.Format("{0:MM/dd/yyyy}", transaction.ChequeDate);
                        }
                    }
                    else
                    {
                        IsCash.Checked = true;
                        IsCheque.Checked = false;
                        IsCard.Checked = false;
                        IsCash_CheckedChanged(sender, e);
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
            BtnUpdate.Visible = true;
            BtnSave.Visible = false;
            MultiView1.SetActiveView(View1);
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            ImageButton imgEdit = (ImageButton)sender;
            GridViewRow row = (GridViewRow)imgEdit.NamingContainer;
            Session["ReceiptNo"] = Convert.ToInt32(dgvCustTransaction.DataKeys[row.RowIndex].Value);
            Session["ReportType"] = "Receipt";
            Response.Redirect("~/PathalogyReport/PathologyReport.aspx", false);
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                ddlPatient.SelectedIndex = 0;
                txtNew.Text = string.Empty;
                txtTotal.Text = string.Empty;
                txtAdvanceAmount.Text = Convert.ToString(0);
                txtChequeNo.Text = string.Empty;
                txtChequeDate.Text = string.Empty;
                txtBankName.Text = string.Empty;
                txtBankRefNo.Text = string.Empty;
                txtBillRefNo.Text = string.Empty;
                txtTransactDate.Enabled = false;
                txtTransactDate.Text = DateTime.UtcNow.Date.ToString("MM/dd/yyyy");
                BtnUpdate.Visible = false;
                txtPayAmount.Text = Convert.ToString(0);
                ddlPatientCategory.SelectedIndex = 0;
                BtnSave.Visible = true;
                lblMessage.Text = string.Empty;
                CheckList(false);
                MultiView1.SetActiveView(View1);
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


        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ddlPatient.SelectedIndex = 0;
            txtNew.Text = string.Empty;
            txtTotal.Text = string.Empty;
            txtAdvanceAmount.Text = Convert.ToString(0);
            txtChequeNo.Text = string.Empty;
            txtChequeDate.Text = string.Empty;
            txtBankName.Text = string.Empty;
            txtBillRefNo.Text = string.Empty;
            txtTransactDate.Text = string.Empty;
            //txtNetAmount.Text = string.Empty;
            BtnUpdate.Visible = false;
            txtPayAmount.Text = string.Empty;
            BtnSave.Visible = true;
            lblMessage.Text = string.Empty;
            CheckList(false);
            MultiView1.SetActiveView(View2);
        }

        private void GetCustTransactionList()
        {
            try
            {
                List<EntityCustomerTransaction> tblPatient = new CustomerTransactionBLL().GetCustomerTransactionList();
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
                    EntityCustomerTransaction entcust = new EntityCustomerTransaction();
                    EntityCustomerTransaction transact = null;
                    entcust.PatientId = Convert.ToInt32(ddlPatient.SelectedValue);
                    entcust.EmpName = Convert.ToString(Session["AdminName"]);
                    entcust.ReceiptDate = Convert.ToDateTime(txtTransactDate.Text);
                    entcust.BillRefNo = Convert.ToString(txtBillRefNo.Text);
                    entcust.BillAmount = 0;
                    entcust.PayAmount = Convert.ToDecimal(txtPayAmount.Text);
                    entcust.AdvanceAmount = Convert.ToDecimal(txtAdvanceAmount.Text);
                    entcust.IsCash = true;
                    if (IsCheque.Checked && ddlPatientCategory.SelectedItem.Text == "Self")
                    {
                        entcust.IsCash = false;
                        entcust.ISCheque = true;
                        entcust.IsCard = false;
                        entcust.IsRTGS = false;
                        entcust.ReceiptDate = Convert.ToDateTime(txtTransactDate.Text);
                        entcust.ChequeDate = Convert.ToDateTime(txtChequeDate.Text);
                        entcust.ChequeNo = Convert.ToString(txtChequeNo.Text);
                        entcust.BankName = Convert.ToString(txtBankName.Text);
                        entcust.BillRefNo = Convert.ToString(txtBillRefNo.Text);
                        entcust.PatientCategory = Convert.ToString(ddlPatientCategory.SelectedItem.Text);
                        transact = new EntityCustomerTransaction();
                        transact.InsuranceId = 0;
                        transact.CompanyId = 0;
                        transact.CompanyName = "";
                        transact.InsuranceName = "";
                        transact.ReceiptDate = Convert.ToDateTime(txtChequeDate.Text);
                        transact.BillAmount = Convert.ToDecimal(txtPayAmount.Text);
                        transact.AdvanceAmount = Convert.ToDecimal(txtAdvanceAmount.Text);
                        transact.ISCheque = true;
                        transact.PayAmount = 0;
                        transact.ChequeDate = Convert.ToDateTime(txtChequeDate.Text);
                        transact.ChequeNo = Convert.ToString(txtChequeNo.Text);
                        transact.BankName = Convert.ToString(txtBankName.Text);
                    }
                    else if (IsCheque.Checked && ddlPatientCategory.SelectedItem.Text == "Company")
                    {
                        entcust.IsCash = false;
                        entcust.ISCheque = true;
                        entcust.IsCard = false;
                        entcust.IsRTGS = false;
                        entcust.ReceiptDate = Convert.ToDateTime(txtTransactDate.Text);
                        entcust.ChequeDate = Convert.ToDateTime(txtChequeDate.Text);
                        entcust.ChequeNo = Convert.ToString(txtChequeNo.Text);
                        entcust.BankName = Convert.ToString(txtBankName.Text);
                        entcust.BillRefNo = Convert.ToString(txtBillRefNo.Text);
                        entcust.PatientCategory = Convert.ToString(ddlPatientCategory.SelectedItem.Text);
                        transact = new EntityCustomerTransaction();
                        transact.InsuranceId = 0;
                        transact.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                        transact.CompanyName = Convert.ToString(ddlCompany.SelectedItem.Text);
                        transact.InsuranceName = "";
                        transact.ReceiptDate = Convert.ToDateTime(txtChequeDate.Text);
                        transact.BillAmount = Convert.ToDecimal(txtPayAmount.Text);
                        transact.AdvanceAmount = Convert.ToDecimal(txtAdvanceAmount.Text);
                        transact.ISCheque = true;
                        transact.PayAmount = 0;
                        transact.ChequeDate = Convert.ToDateTime(txtChequeDate.Text);
                        transact.ChequeNo = Convert.ToString(txtChequeNo.Text);
                        transact.BankName = Convert.ToString(txtBankName.Text);
                    }
                    else if (IsCheque.Checked && ddlPatientCategory.SelectedItem.Text == "Insurance")
                    {
                        entcust.IsCash = false;
                        entcust.ISCheque = false;
                        entcust.IsCard = false;
                        entcust.IsRTGS = true;
                        entcust.ReceiptDate = Convert.ToDateTime(txtTransactDate.Text);
                        entcust.ChequeDate = Convert.ToDateTime(txtChequeDate.Text);
                        entcust.ChequeNo = Convert.ToString(txtChequeNo.Text);
                        entcust.BankName = Convert.ToString(txtBankName.Text);
                        entcust.BillRefNo = Convert.ToString(txtBillRefNo.Text);
                        entcust.PatientCategory = Convert.ToString(ddlPatientCategory.SelectedItem.Text);
                        transact = new EntityCustomerTransaction();
                        transact.InsuranceId = Convert.ToInt32(ddlCompany.SelectedValue);
                        transact.CompanyId = 0;
                        transact.InsuranceName = Convert.ToString(ddlCompany.SelectedItem.Text);
                        transact.CompanyName = "";
                        transact.ReceiptDate = Convert.ToDateTime(txtChequeDate.Text);
                        transact.BillAmount = Convert.ToDecimal(txtPayAmount.Text);
                        transact.AdvanceAmount = Convert.ToDecimal(txtAdvanceAmount.Text);
                        transact.ISCheque = true;
                        transact.PayAmount = 0;
                        transact.ChequeDate = Convert.ToDateTime(txtChequeDate.Text);
                        transact.ChequeNo = Convert.ToString(txtChequeNo.Text);
                        transact.BankName = Convert.ToString(txtBankName.Text);
                    }
                    else if (IsCard.Checked && ddlPatientCategory.SelectedItem.Text == "Self")
                    {
                        entcust.IsCash = false;
                        entcust.ISCheque = false;
                        entcust.IsCard = true;
                        entcust.IsRTGS = false;
                        entcust.ReceiptDate = Convert.ToDateTime(txtTransactDate.Text);
                        entcust.BankRefNo = Convert.ToString(txtBankRefNo.Text);
                        entcust.BillRefNo = Convert.ToString(txtBillRefNo.Text);
                        entcust.PatientCategory = Convert.ToString(ddlPatientCategory.SelectedItem.Text);
                        transact = new EntityCustomerTransaction();
                        transact.InsuranceId = 0;
                        transact.CompanyId = 0;
                        transact.CompanyName = "";
                        transact.InsuranceName = "";
                        transact.ReceiptDate = Convert.ToDateTime(txtTransactDate.Text);
                        transact.BillAmount = Convert.ToDecimal(txtPayAmount.Text);
                        transact.AdvanceAmount = Convert.ToDecimal(txtAdvanceAmount.Text);
                        transact.IsCard = true;
                        transact.PayAmount = 0;
                        transact.BankRefNo = Convert.ToString(txtBankRefNo.Text);
                    }
                    else if (IsCard.Checked && ddlPatientCategory.SelectedItem.Text == "Company")
                    {
                        entcust.IsCash = false;
                        entcust.ISCheque = false;
                        entcust.IsCard = true;
                        entcust.IsRTGS = false;
                        entcust.ReceiptDate = Convert.ToDateTime(txtTransactDate.Text);
                        entcust.BankRefNo = Convert.ToString(txtBankRefNo.Text);
                        entcust.BillRefNo = Convert.ToString(txtBillRefNo.Text);
                        entcust.PatientCategory = Convert.ToString(ddlPatientCategory.SelectedItem.Text);
                        transact = new EntityCustomerTransaction();
                        transact.InsuranceId = 0;
                        transact.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                        transact.CompanyName = Convert.ToString(ddlCompany.SelectedItem.Text);
                        transact.InsuranceName = "";
                        transact.ReceiptDate = Convert.ToDateTime(txtTransactDate.Text);
                        transact.BillAmount = Convert.ToDecimal(txtPayAmount.Text);
                        transact.AdvanceAmount = Convert.ToDecimal(txtAdvanceAmount.Text);
                        transact.IsCard = true;
                        transact.PayAmount = 0;
                        transact.BankRefNo = Convert.ToString(txtBankRefNo.Text);
                    }
                    else if (IsCard.Checked && ddlPatientCategory.SelectedItem.Text == "Insurance")
                    {
                        entcust.IsCash = false;
                        entcust.ISCheque = false;
                        entcust.IsCard = true;
                        entcust.IsRTGS = false;
                        entcust.BankRefNo = Convert.ToString(txtBankRefNo.Text);
                        entcust.BillRefNo = Convert.ToString(txtBillRefNo.Text);
                        entcust.PatientCategory = Convert.ToString(ddlPatientCategory.SelectedItem.Text);
                        transact = new EntityCustomerTransaction();
                        transact.InsuranceId = Convert.ToInt32(ddlCompany.SelectedValue);
                        transact.CompanyId = 0;
                        transact.InsuranceName = Convert.ToString(ddlCompany.SelectedItem.Text);
                        transact.CompanyName = "";
                        transact.ReceiptDate = Convert.ToDateTime(txtTransactDate.Text);
                        transact.BillAmount = Convert.ToDecimal(txtPayAmount.Text);
                        transact.AdvanceAmount = Convert.ToDecimal(txtAdvanceAmount.Text);
                        transact.IsCard = true;
                        transact.PayAmount = 0;
                        transact.BankRefNo = Convert.ToString(txtBankRefNo.Text);
                    }

                    else if (IsRTGS.Checked && ddlPatientCategory.SelectedItem.Text == "Self")
                    {
                        entcust.IsCash = false;
                        entcust.ISCheque = false;
                        entcust.IsCard = false;
                        entcust.IsRTGS = true;
                        entcust.ReceiptDate = Convert.ToDateTime(txtTransactDate.Text);
                        entcust.BankRefNo = Convert.ToString(txtBankRefNo.Text);
                        entcust.BillRefNo = Convert.ToString(txtBillRefNo.Text);
                        entcust.PatientCategory = Convert.ToString(ddlPatientCategory.SelectedItem.Text);
                        transact = new EntityCustomerTransaction();
                        transact.InsuranceId = 0;
                        transact.CompanyId = 0;
                        transact.CompanyName = "";
                        transact.InsuranceName = "";
                        transact.ReceiptDate = Convert.ToDateTime(txtTransactDate.Text);
                        transact.BillAmount = Convert.ToDecimal(txtPayAmount.Text);
                        transact.AdvanceAmount = Convert.ToDecimal(txtAdvanceAmount.Text);
                        transact.IsRTGS = true;
                        transact.PayAmount = 0;
                        transact.BankRefNo = Convert.ToString(txtBankRefNo.Text);
                    }
                    else if (IsRTGS.Checked && ddlPatientCategory.SelectedItem.Text == "Company")
                    {
                        entcust.IsCash = false;
                        entcust.ISCheque = false;
                        entcust.IsCard = false;
                        entcust.IsRTGS = true;
                        entcust.ReceiptDate = Convert.ToDateTime(txtTransactDate.Text);
                        entcust.BankRefNo = Convert.ToString(txtBankRefNo.Text);
                        entcust.BillRefNo = Convert.ToString(txtBillRefNo.Text);
                        entcust.PatientCategory = Convert.ToString(ddlPatientCategory.SelectedItem.Text);
                        transact = new EntityCustomerTransaction();
                        transact.InsuranceId = 0;
                        transact.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                        transact.CompanyName = Convert.ToString(ddlCompany.SelectedItem.Text);
                        transact.InsuranceName = "";
                        transact.ReceiptDate = Convert.ToDateTime(txtTransactDate.Text);
                        transact.BillAmount = Convert.ToDecimal(txtPayAmount.Text);
                        transact.AdvanceAmount = Convert.ToDecimal(txtAdvanceAmount.Text);
                        transact.IsRTGS = true;
                        transact.PayAmount = 0;
                        transact.BankRefNo = Convert.ToString(txtBankRefNo.Text);
                    }
                    else if (IsRTGS.Checked && ddlPatientCategory.SelectedItem.Text == "Insurance")
                    {
                        entcust.IsCash = false;
                        entcust.ISCheque = false;
                        entcust.IsCard = false;
                        entcust.IsRTGS = true;
                        entcust.BankRefNo = Convert.ToString(txtBankRefNo.Text);
                        entcust.BillRefNo = Convert.ToString(txtBillRefNo.Text);
                        entcust.PatientCategory = Convert.ToString(ddlPatientCategory.SelectedItem.Text);
                        transact = new EntityCustomerTransaction();
                        transact.InsuranceId = Convert.ToInt32(ddlCompany.SelectedValue);
                        transact.CompanyId = 0;
                        transact.InsuranceName = Convert.ToString(ddlCompany.SelectedItem.Text);
                        transact.CompanyName = "";
                        transact.ReceiptDate = Convert.ToDateTime(txtTransactDate.Text);
                        transact.BillAmount = Convert.ToDecimal(txtPayAmount.Text);
                        transact.AdvanceAmount = Convert.ToDecimal(txtAdvanceAmount.Text);
                        transact.IsCard = true;
                        transact.PayAmount = 0;
                        transact.BankRefNo = Convert.ToString(txtBankRefNo.Text);
                    }
                    int i = new CustomerTransactionBLL().Save(entcust, transact, IsCash.Checked);

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
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
            GetCustTransactionList();
            MultiView1.SetActiveView(View2);
        }

        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                tblCustomerTransaction entcust = new tblCustomerTransaction();
                entcust.PayAmount = Convert.ToDecimal(txtPayAmount.Text);
                entcust.AdvanceAmount = Convert.ToDecimal(txtAdvanceAmount.Text);
                entcust.ReceiptNo = Convert.ToInt32(Session["ReceiptNo"]);
                entcust.BillAmount = 0;
                entcust.IsCash = true;
                entcust.ReceiptDate = Convert.ToDateTime(txtTransactDate.Text);
                entcust.PatientId = Convert.ToInt32(ddlPatient.SelectedValue);
                entcust.PreparedByName = Convert.ToString(Session["AdminName"]);
                EntityCustomerTransaction transact = null;
                if (IsCheque.Checked && ddlPatientCategory.SelectedItem.Text == "Self")
                {
                    entcust.IsCash = false;
                    entcust.ISCheque = true;
                    entcust.IsCard = false;
                    entcust.IsRTGS = false;
                    entcust.ReceiptDate = Convert.ToDateTime(txtChequeDate.Text);
                    entcust.ChequeDate = Convert.ToDateTime(txtChequeDate.Text);
                    entcust.ChequeNo = Convert.ToString(txtChequeNo.Text);
                    entcust.BankName = Convert.ToString(txtBankName.Text);
                    entcust.PatientCategory = Convert.ToString(ddlPatientCategory.SelectedItem.Text);
                    transact = new EntityCustomerTransaction();
                    transact.InsuranceId = 0;
                    transact.CompanyId = 0;
                    transact.CompanyName = "";
                    transact.InsuranceName = "";
                    transact.ReceiptDate = Convert.ToDateTime(txtChequeDate.Text);
                    transact.BillAmount = Convert.ToDecimal(txtPayAmount.Text);
                    transact.AdvanceAmount = Convert.ToDecimal(txtAdvanceAmount.Text);
                    transact.ISCheque = true;
                    transact.PayAmount = 0;
                    transact.ChequeDate = Convert.ToDateTime(txtChequeDate.Text);
                    transact.ChequeNo = Convert.ToString(txtChequeNo.Text);
                    transact.BankName = Convert.ToString(txtBankName.Text);
                }
                else if (IsCheque.Checked && ddlPatientCategory.SelectedItem.Text == "Company")
                {
                    entcust.IsCash = false;
                    entcust.ISCheque = true;
                    entcust.IsCard = false;
                    entcust.IsRTGS = false;
                    entcust.ReceiptDate = Convert.ToDateTime(txtChequeDate.Text);
                    entcust.ChequeDate = Convert.ToDateTime(txtChequeDate.Text);
                    entcust.ChequeNo = Convert.ToString(txtChequeNo.Text);
                    entcust.BankName = Convert.ToString(txtBankName.Text);
                    entcust.PatientCategory = Convert.ToString(ddlPatientCategory.SelectedItem.Text);
                    transact = new EntityCustomerTransaction();
                    transact.InsuranceId = 0;
                    transact.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                    transact.CompanyName = Convert.ToString(ddlCompany.SelectedItem.Text);
                    transact.InsuranceName = "";
                    transact.ReceiptDate = Convert.ToDateTime(txtChequeDate.Text);
                    transact.BillAmount = Convert.ToDecimal(txtPayAmount.Text);
                    transact.AdvanceAmount = Convert.ToDecimal(txtAdvanceAmount.Text);
                    transact.ISCheque = true;
                    transact.PayAmount = 0;
                    transact.ChequeDate = Convert.ToDateTime(txtChequeDate.Text);
                    transact.ChequeNo = Convert.ToString(txtChequeNo.Text);
                    transact.BankName = Convert.ToString(txtBankName.Text);
                }
                else if (IsCheque.Checked && ddlPatientCategory.SelectedItem.Text == "Insurance")
                {
                    entcust.IsCash = false;
                    entcust.ISCheque = false;
                    entcust.IsCard = false;
                    entcust.IsRTGS = false;
                    entcust.ReceiptDate = Convert.ToDateTime(txtChequeDate.Text);
                    entcust.ChequeDate = Convert.ToDateTime(txtChequeDate.Text);
                    entcust.ChequeNo = Convert.ToString(txtChequeNo.Text);
                    entcust.BankName = Convert.ToString(txtBankName.Text);
                    entcust.PatientCategory = Convert.ToString(ddlPatientCategory.SelectedItem.Text);
                    transact = new EntityCustomerTransaction();
                    transact.InsuranceId = Convert.ToInt32(ddlCompany.SelectedValue);
                    transact.CompanyId = 0;
                    transact.InsuranceName = Convert.ToString(ddlCompany.SelectedItem.Text);
                    transact.CompanyName = "";
                    transact.ReceiptDate = Convert.ToDateTime(txtChequeDate.Text);
                    transact.BillAmount = Convert.ToDecimal(txtPayAmount.Text);
                    transact.AdvanceAmount = Convert.ToDecimal(txtAdvanceAmount.Text);
                    transact.ISCheque = true;
                    transact.PayAmount = 0;
                    transact.ChequeDate = Convert.ToDateTime(txtChequeDate.Text);
                    transact.ChequeNo = Convert.ToString(txtChequeNo.Text);
                    transact.BankName = Convert.ToString(txtBankName.Text);
                }
                int i = new CustomerTransactionBLL().Update(entcust, transact, IsCash.Checked);
                GetCustTransactionList();
                MultiView1.SetActiveView(View2);
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }

        }

        protected void IsCash_CheckedChanged(object sender, EventArgs e)
        {
            if (IsCash.Checked)
            {
                CheckList(false);
            }
            if (IsCheque.Checked && ddlPatientCategory.SelectedItem.Text == "Self")
            {
                CheckList(true);
                ddlCompany.Visible = false;
                lblCompanyName.Visible = false;
                lblBankRefNo.Visible = false;
                txtBankRefNo.Visible = false;
            }
            else if (IsCheque.Checked && ddlPatientCategory.SelectedItem.Text == "Company")
            {
                PatientMasterBLL mobjPatient = new PatientMasterBLL();
                CheckList(true);
                lblBankRefNo.Visible = false;
                txtBankRefNo.Visible = false;
                DataTable ldt = new DataTable();
                ldt = mobjPatient.GetCompaniesId();
                ddlCompany.DataSource = ldt;
                ddlCompany.DataValueField = "PKId";
                ddlCompany.DataTextField = "CompanyName";
                ddlCompany.DataBind();

                ListItem li = new ListItem();
                li.Text = "--Select Company--";
                li.Value = "0";
                ddlCompany.Items.Insert(0, li);
            }
            else if (IsCheque.Checked && ddlPatientCategory.SelectedItem.Text == "Insurance")
            {
                PatientMasterBLL mobjPatient = new PatientMasterBLL();
                CheckList(true);
                lblBankRefNo.Visible = false;
                txtBankRefNo.Visible = false;
                DataTable ldt = new DataTable();
                ldt = mobjPatient.GetInsuranceCompaniesId();
                ddlCompany.DataSource = ldt;
                ddlCompany.DataValueField = "PKId";
                ddlCompany.DataTextField = "InsuranceDesc";
                ddlCompany.DataBind();

                ListItem li = new ListItem();
                li.Text = "--Select Company--";
                li.Value = "0";
                ddlCompany.Items.Insert(0, li);
            }
            if (IsCard.Checked && ddlPatientCategory.SelectedItem.Text == "Self")
            {
                CheckList(true);
                lblChequeDate.Visible = false;
                lblchequeNo.Visible = false;
                lblBankName.Visible = false;
                txtChequeDate.Visible = false;
                txtChequeNo.Visible = false;
                txtBankName.Visible = false;
                ddlCompany.Visible = false;
                lblCompanyName.Visible = false;
            }
            else if (IsCard.Checked && ddlPatientCategory.SelectedItem.Text == "Company")
            {
                PatientMasterBLL mobjPatient = new PatientMasterBLL();
                CheckList(true);
                lblChequeDate.Visible = false;
                lblchequeNo.Visible = false;
                lblBankName.Visible = false;
                txtChequeDate.Visible = false;
                txtChequeNo.Visible = false;
                txtBankName.Visible = false;
                DataTable ldt = new DataTable();
                ldt = mobjPatient.GetCompaniesId();
                ddlCompany.DataSource = ldt;
                ddlCompany.DataValueField = "PKId";
                ddlCompany.DataTextField = "CompanyName";
                ddlCompany.DataBind();

                ListItem li = new ListItem();
                li.Text = "--Select Company--";
                li.Value = "0";
                ddlCompany.Items.Insert(0, li);
            }
            else if (IsCard.Checked && ddlPatientCategory.SelectedItem.Text == "Insurance")
            {
                PatientMasterBLL mobjPatient = new PatientMasterBLL();
                CheckList(true);
                lblChequeDate.Visible = false;
                lblchequeNo.Visible = false;
                lblBankName.Visible = false;
                txtChequeDate.Visible = false;
                txtChequeNo.Visible = false;
                txtBankName.Visible = false;
                DataTable ldt = new DataTable();
                ldt = mobjPatient.GetInsuranceCompaniesId();
                ddlCompany.DataSource = ldt;
                ddlCompany.DataValueField = "PKId";
                ddlCompany.DataTextField = "InsuranceDesc";
                ddlCompany.DataBind();

                ListItem li = new ListItem();
                li.Text = "--Select Company--";
                li.Value = "0";
                ddlCompany.Items.Insert(0, li);
            }
            if (IsRTGS.Checked && ddlPatientCategory.SelectedItem.Text == "Self")
            {
                CheckList(true);
                lblChequeDate.Visible = false;
                lblchequeNo.Visible = false;
                lblBankName.Visible = false;
                txtChequeDate.Visible = false;
                txtChequeNo.Visible = false;
                txtBankName.Visible = false;
                ddlCompany.Visible = false;
                lblCompanyName.Visible = false;
            }
            else if (IsRTGS.Checked && ddlPatientCategory.SelectedItem.Text == "Company")
            {
                PatientMasterBLL mobjPatient = new PatientMasterBLL();
                CheckList(true);
                lblChequeDate.Visible = false;
                lblchequeNo.Visible = false;
                lblBankName.Visible = false;
                txtChequeDate.Visible = false;
                txtChequeNo.Visible = false;
                txtBankName.Visible = false;
                DataTable ldt = new DataTable();
                ldt = mobjPatient.GetCompaniesId();
                ddlCompany.DataSource = ldt;
                ddlCompany.DataValueField = "PKId";
                ddlCompany.DataTextField = "CompanyName";
                ddlCompany.DataBind();

                ListItem li = new ListItem();
                li.Text = "--Select Company--";
                li.Value = "0";
                ddlCompany.Items.Insert(0, li);
            }
            else if (IsRTGS.Checked && ddlPatientCategory.SelectedItem.Text == "Insurance")
            {
                PatientMasterBLL mobjPatient = new PatientMasterBLL();
                CheckList(true);
                lblChequeDate.Visible = false;
                lblchequeNo.Visible = false;
                lblBankName.Visible = false;
                txtChequeDate.Visible = false;
                txtChequeNo.Visible = false;
                txtBankName.Visible = false;
                DataTable ldt = new DataTable();
                ldt = mobjPatient.GetInsuranceCompaniesId();
                ddlCompany.DataSource = ldt;
                ddlCompany.DataValueField = "PKId";
                ddlCompany.DataTextField = "InsuranceDesc";
                ddlCompany.DataBind();

                ListItem li = new ListItem();
                li.Text = "--Select Company--";
                li.Value = "0";
                ddlCompany.Items.Insert(0, li);
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
            lblCompanyName.Visible = flag;
            ddlCompany.Visible = flag;
            lblBankRefNo.Visible = flag;
            txtBankRefNo.Visible = flag;
        }

        protected void ddlPatient_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlPatient.SelectedIndex > 0)
                {
                    txtTotal.Text = Convert.ToString(mobjDeptBLL.GetPatientTrans(Convert.ToInt32(ddlPatient.SelectedValue)));
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void dgvCustTransaction_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dgvCustTransaction.DataSource = (List<EntityCustomerTransaction>)Session["DepartmentDetail"];
                dgvCustTransaction.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void dgvCustTransaction_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvCustTransaction.PageIndex = e.NewPageIndex;
        }
    }
}