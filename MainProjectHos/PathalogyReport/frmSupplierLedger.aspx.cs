using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MainProjectHos.Models.DataLayer;
using MainProjectHos.Models.DataModels;
using MainProjectHos.Models.BusinessLayer;

namespace MainProjectHos.PathalogyReport
{
    public partial class frmSupplierLedger : System.Web.UI.Page
    {
        PatientInvoiceBLL mobjDeptBLL = new PatientInvoiceBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] != null)
            {
                EntityLogin entLogin = (EntityLogin)Session["user"];

                List<EntityFormMaster> entFormAcc = (List<EntityFormMaster>)Session["AccessibleForm"];
                int Access = (from tbl in entFormAcc
                              where tbl.FormName.Contains("frmSupplierLedger")
                              select tbl).Count();

                if (Access > 0)
                {
                    if (!Page.IsPostBack)
                    {
                        BindPatients();
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
                List<EntitySupplierMaster> tblpatient = new CustomerTransactionBLL().GetAllocatedSupplier();
                tblpatient.Insert(0, new EntitySupplierMaster() { PKId = 0, SupplierName = "----Select----" });
                ddlSupplier.DataSource = tblpatient;
                ddlSupplier.DataTextField = "SupplierName";
                ddlSupplier.DataValueField = "PKId";
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
                DateTime Curr = Convert.ToDateTime(DateTime.Now);
                DateTime Next = Curr.AddYears(1);
                lbl.Text = "SUPPLIER LEDGER REPORT" + " " + string.Format("{0:yyyy}", Curr) + "-" + string.Format("{0:yyyy}", Next);

                lblFrom.Text = ddlSupplier.SelectedItem.Text;
                lblTo.Text = string.Format("{0:dd-MMM-yyyy}", DateTime.Now);
                lblMessage.Text = string.Empty;
                List<tblCustomerTransaction> lst = new CustomerTransactionBLL().GetSupplierTransBySupplierId(Convert.ToInt32(ddlSupplier.SelectedValue));
                tblCustomerTransaction obj = new tblCustomerTransaction() { };
                lst.Add(obj);
                tblCustomerTransaction objReceived = new tblCustomerTransaction()
                {
                    TransactionType = "Total Received",
                    PayAmount = Convert.ToDecimal(lst.Sum(p => p.PayAmount))
                };
                lst.Add(objReceived);
                tblCustomerTransaction objBills = new tblCustomerTransaction()
                {
                    TransactionType = "Total Bills",
                    BillAmount = Convert.ToDecimal(lst.Sum(p => p.BillAmount))
                };
                lst.Add(objBills);
                tblCustomerTransaction objFinal = new tblCustomerTransaction()
                {
                    TransactionType = "Total Bills",
                    BillAmount = objBills.BillAmount - objReceived.PayAmount
                };
                lst.Add(objFinal);
                dgvTestParameter.DataSource = lst;
                dgvTestParameter.DataBind();
                Session["SuppLedg"] = ListConverter.ToDataTable(lst);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private void SelectBedConsumtion()
        {

        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            ddlSupplier.SelectedIndex = 0;
            List<EntityCustomerTransaction> lst = new List<EntityCustomerTransaction>();
            dgvTestParameter.DataSource = lst;
            dgvTestParameter.DataBind();
            lbl.Text = string.Empty;
            lblFrom.Text = string.Empty;
            lblTo.Text = string.Empty;
        }
        protected void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(lblFrom.Text) && !string.IsNullOrEmpty(lblTo.Text))
                {
                    Session["Details"] = Session["SuppLedg"];
                    Response.Redirect("~/ExcelReport/MonthwiseSalExcel.aspx");
                }
                else
                {
                    lblMessage.Text = "Please Select Supplier Name";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
    }
}