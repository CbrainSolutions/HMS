using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MainProjectHos.Models.DataLayer;
using MainProjectHos.Models.DataModels;
using MainProjectHos.Models.BusinessLayer;
using System.Data;

namespace MainProjectHos.PathalogyReport
{
    public partial class frmSummarizedDetailReport : System.Web.UI.Page
    {
        PatientInvoiceBLL mobjDeptBLL = new PatientInvoiceBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] != null)
            {
                EntityLogin entLogin = (EntityLogin)Session["user"];

                List<EntityFormMaster> entFormAcc = (List<EntityFormMaster>)Session["AccessibleForm"];
                int Access = (from tbl in entFormAcc
                              where tbl.FormName.Contains("frmSummarizedDetailReport")
                              select tbl).Count();

                if (Access > 0)
                {
                    if (!Page.IsPostBack)
                    {
                        Session["MyFlag"] = string.Empty;
                        Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                        //BindEmployee();
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

        //public void BindEmployee()
        //{
        //    SalaryCalculationBLL mobjSalBLL = new SalaryCalculationBLL();
        //    try
        //    {
        //        List<EntityEmployee> lst = mobjSalBLL.GetEmployee();
        //        lst.Insert(0, new EntityEmployee { PKId = 0, EmpName = "--Select--" });
        //        ddlEmployee.DataSource = lst;
        //        ddlEmployee.DataValueField = "PKId";
        //        ddlEmployee.DataTextField = "EmpName";
        //        ddlEmployee.DataBind();
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                SelectBedConsumtion();
                lblMessage.Text = string.Empty;
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private void SelectBedConsumtion()
        {
            try
            {
                BedConsumtionBLL consume = new BedConsumtionBLL();
                //if (ddlEmployee.SelectedIndex > 0)
                //{
                lbl.Text = "Summarized Report";

                //IPD Sale(Discharge Patient) :-
                decimal invCash = consume.GetCashInvoiceDetails(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text));
                decimal RecCash = consume.GetRecCashDetails(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text));
                txtCash.Text = string.Format("{0:0.00}", invCash + RecCash);
                txtCheque.Text = string.Format("{0:0.00}", consume.GetRecChequeDetails(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text)));
                txtCard.Text = string.Format("{0:0.00}", consume.GetRecCardDetails(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text)));
                txtRTGS.Text = string.Format("{0:0.00}", consume.GetRecRTGSDetails(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text)));
                txtTotal.Text = string.Format("{0:0.00}", Convert.ToDecimal(txtCash.Text) + Convert.ToDecimal(txtCheque.Text) + Convert.ToDecimal(txtCard.Text) + Convert.ToDecimal(txtRTGS.Text));
                txtDiscountIPD.Text = string.Format("{0:0.00}", consume.GetIPDDiscountInvoiceDetails(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text)));

                //OPD Sale(Consulting/Treatments/Daycare) :-
                decimal invCash1 = consume.GetOPDCashInvoiceDetails(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text));
                decimal RecCash1 = consume.GetOPDRecCashDetails(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text));
                txtCash1.Text = string.Format("{0:0.00}", invCash1 + RecCash1);
                txtCheque1.Text = string.Format("{0:0.00}", consume.GetOPDRecChequeDetails(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text)));
                txtCard1.Text = string.Format("{0:0.00}", consume.GetOPDRecCardDetails(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text)));
                txtRTGS1.Text = string.Format("{0:0.00}", consume.GetOPDRecRTGSDetails(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text)));
                txtTotal1.Text = string.Format("{0:0.00}", Convert.ToDecimal(txtCash1.Text) + Convert.ToDecimal(txtCheque1.Text) + Convert.ToDecimal(txtCard1.Text) + Convert.ToDecimal(txtRTGS1.Text));
                txtDiscountOPD.Text = string.Format("{0:0.00}", consume.GetOPDDiscountInvoiceDetails(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text)));

                //IPD Advanced Received :-
                txtCash2.Text = string.Format("{0:0.00}", consume.GetIPDAdvanceRecCashDetails(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text)));
                txtCheque2.Text = string.Format("{0:0.00}", consume.GetIPDAdvanceRecChequeDetails(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text)));
                txtCard2.Text = string.Format("{0:0.00}", consume.GetIPDAdvanceRecCardDetails(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text)));
                txtRTGs2.Text = string.Format("{0:0.00}", consume.GetIPDAdvanceRecRTGSDetails(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text)));
                txtTotal2.Text = string.Format("{0:0.00}", Convert.ToDecimal(txtCash2.Text) + Convert.ToDecimal(txtCheque2.Text) + Convert.ToDecimal(txtCard2.Text) + Convert.ToDecimal(txtRTGs2.Text));

                //IPD OPD Registration Fees :-
                txtRegFeeIPD.Text = string.Format("{0:0.00}", consume.GetIPDRegFeeInvoiceDetails(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text)));
                txtRegFeeOPD.Text = string.Format("{0:0.00}", consume.GetOPDRegFeeInvoiceDetails(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text)));
                txtTotalReg.Text = string.Format("{0:0.00}", Convert.ToDecimal(txtRegFeeIPD.Text) + Convert.ToDecimal(txtRegFeeOPD.Text));

                //IPD Refund Amount :-
                txtIPDCashRefund.Text = string.Format("{0:0.00}", consume.GetIPDCashRefundDetails(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text)));
                txtIPDChequeRefund.Text = string.Format("{0:0.00}", consume.GetIPDChequeRefundDetails(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text)));
                txtIPDCardRefund.Text = string.Format("{0:0.00}", consume.GetIPDCardRefundDetails(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text)));
                txtIPDRTGSRefund.Text = string.Format("{0:0.00}", consume.GetIPDRTGSRefundDetails(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text)));
                txtIPDTotalRefund.Text = string.Format("{0:0.00}", Convert.ToDecimal(txtIPDCashRefund.Text) + Convert.ToDecimal(txtIPDChequeRefund.Text) + Convert.ToDecimal(txtIPDCardRefund.Text) + Convert.ToDecimal(txtIPDRTGSRefund.Text));

                //OPD Refund Amount :-
                txtOPDCashRefund.Text = string.Format("{0:0.00}", consume.GetOPDCashRefundDetails(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text)));
                txtOPDChequeRefund.Text = string.Format("{0:0.00}", consume.GetOPDChequeRefundDetails(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text)));
                txtOPDCardRefund.Text = string.Format("{0:0.00}", consume.GetOPDCardRefundDetails(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text)));
                txtOPDRTGSRefund.Text = string.Format("{0:0.00}", consume.GetOPDRTGSRefundDetails(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text)));
                txtOPDTotalRefund.Text = string.Format("{0:0.00}", Convert.ToDecimal(txtOPDCashRefund.Text) + Convert.ToDecimal(txtOPDChequeRefund.Text) + Convert.ToDecimal(txtOPDCardRefund.Text) + Convert.ToDecimal(txtOPDRTGSRefund.Text));

                //Credit (Bill Pending)Amount :-
                txtCreditAmt.Text = string.Format("{0:0.00}", consume.GetCreditAmtDetails(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text)));

                //Total Collection IPD OPD
                txtTotalCash.Text = string.Format("{0:0.00}", Convert.ToDecimal(txtCash.Text) + Convert.ToDecimal(txtCash1.Text) + Convert.ToDecimal(txtCash2.Text));
                txtTotalCheque.Text = string.Format("{0:0.00}", Convert.ToDecimal(txtCheque.Text) + Convert.ToDecimal(txtCheque1.Text) + Convert.ToDecimal(txtCheque2.Text));
                txtTotalCard.Text = string.Format("{0:0.00}", Convert.ToDecimal(txtCard.Text) + Convert.ToDecimal(txtCard1.Text) + Convert.ToDecimal(txtCard2.Text));
                txtTotalRTGS.Text = string.Format("{0:0.00}", Convert.ToDecimal(txtRTGS.Text) + Convert.ToDecimal(txtRTGS1.Text) + Convert.ToDecimal(txtRTGs2.Text));
                txtTotalAmt.Text = string.Format("{0:0.00}", Convert.ToDecimal(txtTotalCash.Text) + Convert.ToDecimal(txtTotalCheque.Text) + Convert.ToDecimal(txtTotalCard.Text) + Convert.ToDecimal(txtTotalRTGS.Text));

                // Insurance Claim Approval Amount
                txtCashInsurance.Text = string.Format("{0:0.00}", consume.GetInsuranceCashClaimDetails(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text)));
                txtChequeInsurance.Text = string.Format("{0:0.00}", consume.GetInsuranceChequeClaimDetails(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text)));
                txtCardInsurance.Text = string.Format("{0:0.00}", consume.GetInsuranceCardClaimDetails(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text)));
                txtRTGSInsurance.Text = string.Format("{0:0.00}", consume.GetInsuranceRTGSClaimDetails(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text)));
                txtInsuranceTotal.Text = string.Format("{0:0.00}", Convert.ToDecimal(txtCashInsurance.Text) + Convert.ToDecimal(txtChequeInsurance.Text) + Convert.ToDecimal(txtCardInsurance.Text) + Convert.ToDecimal(txtRTGSInsurance.Text));

                // Company Claim Approval Amount
                txtCashCompany.Text = string.Format("{0:0.00}", consume.GetCompanyCashClaimDetails(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text)));
                txtChequeCompany.Text = string.Format("{0:0.00}", consume.GetCompanyChequeClaimDetails(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text)));
                txtCardCompany.Text = string.Format("{0:0.00}", consume.GetCompanyCardClaimDetails(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text)));
                txtRTGSCompany.Text = string.Format("{0:0.00}", consume.GetCompanyRTGSClaimDetails(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text)));
                txtCompanyTotal.Text = string.Format("{0:0.00}", Convert.ToDecimal(txtCashCompany.Text) + Convert.ToDecimal(txtChequeCompany.Text) + Convert.ToDecimal(txtCardCompany.Text) + Convert.ToDecimal(txtRTGSCompany.Text));
                //}
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtBillDate.Text = string.Empty;
            txtToDate.Text = string.Empty;

            lbl.Text = "";

            txtCash.Text = string.Empty;
            txtCash1.Text = string.Empty;
            txtCash2.Text = string.Empty;
            txtTotalCash.Text = string.Empty;
            txtTotalCheque.Text = string.Empty;
            txtTotalCard.Text = string.Empty;
            txtTotalRTGS.Text = string.Empty;
            txtTotalAmt.Text = string.Empty;
            txtCheque.Text = string.Empty;
            txtCheque1.Text = string.Empty;
            txtCheque2.Text = string.Empty;
            txtCard.Text = string.Empty;
            txtCard1.Text = string.Empty;
            txtCard2.Text = string.Empty;
            txtRTGS.Text = string.Empty;
            txtRTGS1.Text = string.Empty;
            txtRTGs2.Text = string.Empty;
            txtCreditAmt.Text = string.Empty;
            txtIPDCashRefund.Text = string.Empty;
            txtIPDChequeRefund.Text = string.Empty;
            txtIPDCardRefund.Text = string.Empty;
            txtIPDRTGSRefund.Text = string.Empty;
            txtIPDTotalRefund.Text = string.Empty;
            txtOPDCashRefund.Text = string.Empty;
            txtOPDChequeRefund.Text = string.Empty;
            txtOPDCardRefund.Text = string.Empty;
            txtOPDRTGSRefund.Text = string.Empty;
            txtOPDTotalRefund.Text = string.Empty;
            txtRegFeeIPD.Text = string.Empty;
            txtRegFeeOPD.Text = string.Empty;
            txtTotal.Text = string.Empty;
            txtTotal1.Text = string.Empty;
            txtTotal2.Text = string.Empty;
            txtDiscountIPD.Text = string.Empty;
            txtDiscountOPD.Text = string.Empty;
            txtRTGSCompany.Text = string.Empty;
            txtRTGSInsurance.Text = string.Empty;
            txtInsuranceTotal.Text = string.Empty;
            txtCompanyTotal.Text = string.Empty;
            lblTo.Text = string.Empty;
            lblFrom.Text = string.Empty;
            lblMessage.Text = string.Empty;

        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(lblFrom.Text) && !string.IsNullOrEmpty(lblTo.Text))
                {
                    Session["Details"] = Session["BedConsump"];
                    Response.Redirect("~/ExcelReport/MonthwiseSalExcel.aspx");
                }
                else
                {
                    lblMessage.Text = "Please Enter Dates";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
    }
}