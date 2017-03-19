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

namespace MainProjectHos.InsuranceClaim
{
    public partial class frmCompanyClaimApproval : System.Web.UI.Page
    {
        CompanyClaimBLL MobjClaim = new CompanyClaimBLL();
        CompanyClaimApprovalBLL MobjApprove = new CompanyClaimApprovalBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] != null)
            {
                EntityLogin entLogin = (EntityLogin)Session["user"];

                List<EntityFormMaster> entFormAcc = (List<EntityFormMaster>)Session["AccessibleForm"];
                int Access = (from tbl in entFormAcc
                              where tbl.FormName.Contains("frmCompanyClaimApproval")
                              select tbl).Count();

                if (Access > 0)
                {
                    if (!Page.IsPostBack)
                    {
                        Session["Myflag"] = string.Empty;
                        BindInsuranceClaim();
                        //BindBankNames();
                        MultiView1.SetActiveView(View1);
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

        public void Clear()
        {

            txtPatient.Text = string.Empty;
            txtClaimamount.Text = string.Empty;
            txtClaimDate.Text = string.Empty;
            txtCheckNo.Text = string.Empty;
            txtBankName.Text = string.Empty;
            txtApprovedate.Text = string.Empty;
            txtCoPayment.Text = string.Empty;
            txtBadDebts.Text = string.Empty;
            txtTds.Text = string.Empty;
            txtBankRefNo.Text = string.Empty;
            txtApprovedamt.Text = string.Empty;
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                tblCompanyClaim tblins = new tblCompanyClaim();
                tblins.ComClaimId = Convert.ToInt32(Session["ClaimId"]);
                tblins.ApprovedAmount = Convert.ToDecimal(txtRecAmount.Text);
                tblins.ApprovedDate = Convert.ToDateTime(txtApprovedate.Text);
                tblins.TDS = Convert.ToInt32(txtTds.Text);
                tblins.BadDebts = Convert.ToDecimal(txtBadDebts.Text);
                tblins.CoPayment = Convert.ToDecimal(txtCoPayment.Text);
                tblins.TDSAmt = Convert.ToDecimal(txtApprovedamt.Text) * Convert.ToDecimal(txtTds.Text) / 100;
                tblins.IsApproved = true;
                tblins.AdmitId = Convert.ToInt32(Session["AdmitId"]);
                EntityCustomerTransaction objPatient = new EntityCustomerTransaction();
                objPatient.PatientId = Convert.ToInt32(Session["AdmitId"]);
                objPatient.PayAmount = Convert.ToDecimal(txtRecAmount.Text);
                objPatient.BillAmount = Convert.ToDecimal(txtRecAmount.Text);
                objPatient.ReceiptDate = Convert.ToDateTime(txtApprovedate.Text);
                objPatient.TDSAmt = Convert.ToDecimal(txtApprovedamt.Text) * Convert.ToDecimal(txtTds.Text) / 100;
                objPatient.TransactionDocNo = Convert.ToInt32(Session["ClaimId"]);
                EntityCustomerTransaction objBank = null;

                if (IsNeft.Checked || IsCheque.Checked)
                {
                    objBank = new EntityCustomerTransaction();
                    objBank.TransactionDocNo = Convert.ToInt32(Session["ClaimId"]);
                    objBank.BankRefNo = Convert.ToString(txtBankRefNo.Text);
                    objBank.PatientId = 0;
                    objBank.ChequeDate = Convert.ToDateTime(txtApprovedate.Text);
                    objBank.BillAmount = Convert.ToDecimal(txtRecAmount.Text);
                    objBank.PayAmount = Convert.ToDecimal(txtRecAmount.Text);
                    objBank.TDSAmt = Convert.ToDecimal(txtApprovedamt.Text) * Convert.ToDecimal(txtTds.Text) / 100;
                    objBank.ReceiptDate = Convert.ToDateTime(txtApprovedate.Text);
                    if (IsCheque.Checked)
                    {
                        objBank.BankName = txtBankName.Text;
                        objBank.ChequeNo = txtCheckNo.Text;
                    }
                }

                MobjApprove.InsertData(tblins, objPatient, objBank, IsCash.Checked, IsNeft.Checked, IsCheque.Checked);
                BindInsuranceClaim();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
            MultiView1.SetActiveView(View1);
        }

        public void BindInsuranceClaim()
        {
            try
            {
                List<EntityInsuranceClaim> lst = MobjClaim.GetInsurance();

                Session["ClaimDetails"] = lst;
                dgvClaim.DataSource = lst;
                int lintRowcount = lst.Count();
                lblRowCount.Text = "<b>Total Records:</b> " + lintRowcount.ToString();
                dgvClaim.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void BtnApprove_Click(object sender, EventArgs e)
        {
            try
            {
                IsCash.Checked = true;
                IsCash_CheckedChanged(sender, e);
                ImageButton imgEdit = (ImageButton)sender;
                GridViewRow row = (GridViewRow)imgEdit.NamingContainer;
                Session["ClaimId"] = Convert.ToInt32(dgvClaim.DataKeys[row.RowIndex].Value);
                Session["AdmitId"] = Convert.ToInt32(row.Cells[1].Text);
                txtPatient.Text = row.Cells[2].Text;
                txtClaimamount.Text = row.Cells[5].Text;
                txtClaimDate.Text = row.Cells[4].Text;
                txtPatient.Enabled = false;
                txtClaimDate.Enabled = false;
                txtClaimamount.Enabled = false;
                string approve = row.Cells[6].Text;
                //CalDate.StartDate = Convert.ToDateTime(txtClaimDate.Text);
                if (approve.Equals("Approved"))
                {
                    lblMessage.Text = "You Cannot Change Claim Details. Claim Is Already Approved...";
                }
                else
                {
                    MultiView1.SetActiveView(View2);
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
            MultiView1.SetActiveView(View2);
        }

        protected void BtnClose_Click(object sender, EventArgs e)
        {
            Clear();
            MultiView1.SetActiveView(View1);
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSearch.Text))
                {
                    List<EntityInsuranceClaim> lst = MobjClaim.GetInsurance(txtSearch.Text);

                    Session["ClaimDetails"] = lst;
                    dgvClaim.DataSource = lst;
                    int lintRowcount = lst.Count();
                    lblRowCount.Text = "<b>Total Records:</b> " + lintRowcount.ToString();
                    dgvClaim.DataBind();
                }
                else
                {
                    lblMessage.Text = "Please Fill Search Text...";
                    txtSearch.Focus();
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
                txtSearch.Text = string.Empty;
                BindInsuranceClaim();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                ListConverter.ToDataTable((List<EntityInsuranceClaim>)Session["ClaimDetails"]);
                Session["Details"] = ListConverter.ToDataTable((List<EntityInsuranceClaim>)Session["ClaimDetails"]);
                Response.Redirect("~/ExcelReport/MonthwiseSalExcel.aspx");
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void dgvClaim_DataBound(object sender, EventArgs e)
        {
            int lintCurrentPage = dgvClaim.PageIndex + 1;
            lblPageCount.Text = "<b>Page</b> " + lintCurrentPage.ToString() + "<b> of </b>" + dgvClaim.PageCount.ToString();
        }

        protected void IsCash_CheckedChanged(object sender, EventArgs e)
        {
            if (IsCash.Checked)
            {
                lblbankRefNo.Visible = false;
                lblbankname.Visible = false;
                lblcheckno.Visible = false;
                txtBankRefNo.Visible = false;
                txtBankName.Visible = false;
                txtCheckNo.Visible = false;
                Pnlclaim.Visible = false;
            }
            else if (IsNeft.Checked)
            {
                txtBankRefNo.Visible = true;
                txtBankName.Visible = false;
                txtCheckNo.Visible = false;
                lblbankRefNo.Visible = true;
                lblbankname.Visible = false;
                lblcheckno.Visible = false;
                Pnlclaim.Visible = true;
            }
            else if (IsCheque.Checked)
            {
                txtBankRefNo.Visible = false;
                txtBankName.Visible = true;
                txtCheckNo.Visible = true;
                lblbankRefNo.Visible = false;
                lblbankname.Visible = true;
                lblcheckno.Visible = true;
                Pnlclaim.Visible = true;
            }
        }

        protected void dgvClaim_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dgvClaim.DataSource = (DataTable)Session["ClaimDetails"];
                dgvClaim.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
        protected void dgvClaim_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvClaim.PageIndex = e.NewPageIndex;
        }
    }
}