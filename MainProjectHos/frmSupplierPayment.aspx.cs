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

namespace MainProjectHos
{
    public partial class frmSupplierPayment : System.Web.UI.Page
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
                              where tbl.FormName.Contains("frmSupplierPayment")
                              select tbl).Count();

                if (Access > 0)
                {
                    if (!Page.IsPostBack)
                    {
                        BindSupplier();
                        GetSupplierTransactionList();
                        BindBanks();
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

        private void BindBanks()
        {
            try
            {
                List<EntityBankMaster> lst = new BankMasterBLL().GetBankDetails();
                lst.Insert(0, new EntityBankMaster() { BankId = 0, BankName = "-----Select------" });
                ddlBank.DataSource = lst;
                ddlBank.DataTextField = "BankName";
                ddlBank.DataValueField = "BankId";
                ddlBank.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private void BindSupplier()
        {
            try
            {
                List<EntitySupplierMaster> lstOther = new PurchaseOrderBLL().GetSupplierList();
                lstOther.Insert(0, new EntitySupplierMaster() { PKId = 0, SupplierName = "--Select--" });
                ddlSupplier.DataSource = lstOther;
                ddlSupplier.DataValueField = "PKId";
                ddlSupplier.DataTextField = "SupplierName";
                ddlSupplier.DataBind();
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
                List<EntityCustomerTransaction> tblPatient = mobjDeptBLL.GetSupplierTransactionList(txtSearch.Text);
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
                    ListItem item = ddlSupplier.Items.FindByText(Convert.ToString(cnt.Cells[2].Text));
                    ddlSupplier.SelectedValue = item.Value;
                    ddlSupplier_SelectedIndexChanged(sender, e);
                    txtTransactDate.Text = string.Format("{0:MM/dd/yyyy}", Convert.ToDateTime(cnt.Cells[1].Text));
                    txtPayAmount.Text = Convert.ToString(cnt.Cells[4].Text);
                    EntityCustomerTransaction transaction = new CustomerTransactionBLL().GetReceiptDetails(Convert.ToInt32(cnt.Cells[0].Text));
                    if (transaction != null)
                    {
                        if (Convert.ToBoolean(transaction.ISCheque))
                        {
                            IsCheque.Checked = Convert.ToBoolean(transaction.ISCheque);
                            IsCash_CheckedChanged(sender, e);
                            ListItem itemBank = ddlBank.Items.FindByText(transaction.PatientName);
                            ddlBank.SelectedValue = itemBank.Value;
                            txtBankName.Text = transaction.BankName;
                            txtChequeNo.Text = transaction.ChequeNo;
                            txtChequeDate.Text = string.Format("{0:MM/dd/yyyy}", transaction.ChequeDate);
                        }
                        else
                        {
                            IsCheque.Checked = Convert.ToBoolean(transaction.ISCheque);
                            RdoCard.Checked = true;
                            IsCash_CheckedChanged(sender, e);
                            ListItem itemBank = ddlBank.Items.FindByText(transaction.PatientName);
                            ddlBank.SelectedValue = itemBank.Value;
                            txtBankName.Text = transaction.BankName;
                            txtChequeNo.Text = transaction.ChequeNo;
                            txtChequeDate.Text = string.Format("{0:MM/dd/yyyy}", transaction.ChequeDate);
                        }
                    }
                    else
                    {
                        IsCash.Checked = true;
                        IsCheque.Checked = false;
                        RdoCard.Checked = false;
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
            EntityCustomerTransaction entCust = new EntityCustomerTransaction();
            entCust.SupplierName = row.Cells[2].Text;
            entCust.ReceiptDate = Convert.ToDateTime(row.Cells[1].Text);
            entCust.Address = row.Cells[3].Text;
            entCust.Amount = Convert.ToDecimal(row.Cells[4].Text);
            Session["SupplierVoucher"] = entCust;
            Session["ReportType"] = "SuppReceipt";
            Response.Redirect("~/PathalogyReport/PathologyReport.aspx", false);
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                ddlSupplier.SelectedIndex = 0;
                txtTotal.Text = string.Empty;
                txtChequeNo.Text = string.Empty;
                txtChequeDate.Text = string.Empty;
                txtBankName.Text = string.Empty;
                txtTransactDate.Text = string.Empty;
                //txtNetAmount.Text = string.Empty;
                BtnUpdate.Visible = false;
                txtPayAmount.Text = string.Empty;
                BtnSave.Visible = true;
                lblMessage.Text = string.Empty;
                lblBankNameI.Visible = false;
                ddlBank.Visible = false;
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
            GetSupplierTransactionList();
        }


        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ddlSupplier.SelectedIndex = 0;
            txtTotal.Text = string.Empty;
            txtChequeNo.Text = string.Empty;
            txtChequeDate.Text = string.Empty;
            txtBankName.Text = string.Empty;
            txtTransactDate.Text = string.Empty;
            //txtNetAmount.Text = string.Empty;
            BtnUpdate.Visible = false;
            txtPayAmount.Text = string.Empty;
            BtnSave.Visible = true;
            lblMessage.Text = string.Empty;
            CheckList(false);
            MultiView1.SetActiveView(View2);
        }

        private void GetSupplierTransactionList()
        {
            try
            {
                List<EntityCustomerTransaction> tblPatient = new CustomerTransactionBLL().GetSupplierTransactionList();
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

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlSupplier.SelectedIndex == 0)
                {
                    lblMsg.Text = "Please Select Patient Name";
                    ddlSupplier.Focus();
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
                    entcust.SupplierId = Convert.ToInt32(ddlSupplier.SelectedValue);
                    entcust.ReceiptDate = Convert.ToDateTime(txtTransactDate.Text);
                    entcust.BillAmount = 0;
                    entcust.PayAmount = Convert.ToDecimal(txtPayAmount.Text);
                    entcust.IsCash = true;
                    if (IsCheque.Checked)
                    {
                        entcust.IsCash = false;
                        entcust.ISCheque = true;
                        entcust.ReceiptDate = Convert.ToDateTime(txtChequeDate.Text);
                        entcust.ChequeDate = Convert.ToDateTime(txtChequeDate.Text);
                        entcust.ChequeNo = Convert.ToString(txtChequeNo.Text);
                        entcust.BankName = Convert.ToString(txtBankName.Text);
                        transact = new EntityCustomerTransaction();
                        transact.BankId = Convert.ToInt32(ddlBank.SelectedValue);
                        transact.ReceiptDate = Convert.ToDateTime(txtChequeDate.Text);
                        transact.PayAmount = Convert.ToDecimal(txtPayAmount.Text);
                        transact.ISCheque = true;
                        transact.BillAmount = 0;
                        transact.ChequeDate = Convert.ToDateTime(txtChequeDate.Text);
                        transact.ChequeNo = Convert.ToString(txtChequeNo.Text);
                        transact.BankName = Convert.ToString(txtBankName.Text);
                    }
                    else if (RdoCard.Checked)
                    {
                        entcust.IsCash = false;
                        entcust.ISCheque = false;
                        transact = new EntityCustomerTransaction();
                        transact.BankId = Convert.ToInt32(ddlBank.SelectedValue);
                        transact.ReceiptDate = Convert.ToDateTime(txtTransactDate.Text);
                        transact.PayAmount = Convert.ToDecimal(txtPayAmount.Text);
                        transact.IsCash = false;
                        transact.ISCheque = false;
                        transact.BillAmount = 0;
                    }
                    int i = new CustomerTransactionBLL().SaveSupplierTransaction(entcust, transact, IsCash.Checked);

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
            GetSupplierTransactionList();
            MultiView1.SetActiveView(View2);
        }

        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                tblCustomerTransaction entcust = new tblCustomerTransaction();
                entcust.PayAmount = Convert.ToDecimal(txtPayAmount.Text);
                entcust.ReceiptNo = Convert.ToInt32(Session["ReceiptNo"]);
                entcust.BillAmount = 0;
                entcust.IsCash = true;
                entcust.ReceiptDate = Convert.ToDateTime(txtTransactDate.Text);
                entcust.SupplierId = Convert.ToInt32(ddlSupplier.SelectedValue);
                EntityCustomerTransaction transact = null;
                if (IsCheque.Checked)
                {
                    entcust.IsCash = false;
                    entcust.ISCheque = true;
                    entcust.ReceiptDate = Convert.ToDateTime(txtChequeDate.Text);
                    entcust.ChequeDate = Convert.ToDateTime(txtChequeDate.Text);
                    entcust.ChequeNo = Convert.ToString(txtChequeNo.Text);
                    entcust.BankName = Convert.ToString(txtBankName.Text);
                    transact = new EntityCustomerTransaction();
                    transact.BankId = Convert.ToInt32(ddlBank.SelectedValue);
                    transact.ReceiptDate = Convert.ToDateTime(txtChequeDate.Text);
                    transact.PayAmount = Convert.ToDecimal(txtPayAmount.Text);
                    transact.ISCheque = true;
                    transact.BillAmount = 0;
                    transact.ChequeDate = Convert.ToDateTime(txtChequeDate.Text);
                    transact.ChequeNo = Convert.ToString(txtChequeNo.Text);
                    transact.BankName = Convert.ToString(txtBankName.Text);
                }
                else if (RdoCard.Checked)
                {
                    entcust.IsCash = false;
                    entcust.ISCheque = false;
                    transact = new EntityCustomerTransaction();
                    transact.BankId = Convert.ToInt32(ddlBank.SelectedValue);
                    transact.ReceiptDate = Convert.ToDateTime(txtTransactDate.Text);
                    transact.PayAmount = Convert.ToDecimal(txtPayAmount.Text);
                    transact.BillAmount = 0;
                }
                int i = new CustomerTransactionBLL().UpdateSupplierTransaction(entcust, transact, IsCash.Checked);
                GetSupplierTransactionList();
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
                lblBankNameI.Visible = false;
                ddlBank.Visible = false;
            }
            else if (IsCheque.Checked)
            {
                CheckList(true);
                lblBankNameI.Visible = true;
                ddlBank.Visible = true;
            }
            else if (RdoCard.Checked)
            {
                CheckList(false);
                lblBankNameI.Visible = true;
                ddlBank.Visible = true;
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
        }


        protected void ddlSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSupplier.SelectedIndex > 0)
            {
                txtTotal.Text = Convert.ToString(mobjDeptBLL.GetSupplierTrans(Convert.ToInt32(ddlSupplier.SelectedValue)));
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