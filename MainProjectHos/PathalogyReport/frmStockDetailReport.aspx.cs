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
    public partial class frmStockDetailReport : System.Web.UI.Page
    {
        PatientInvoiceBLL mobjDeptBLL = new PatientInvoiceBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] != null)
            {
                EntityLogin entLogin = (EntityLogin)Session["user"];

                List<EntityFormMaster> entFormAcc = (List<EntityFormMaster>)Session["AccessibleForm"];
                int Access = (from tbl in entFormAcc
                              where tbl.FormName.Contains("frmStockDetailReport")
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
                List<EntityProduct> tblpatient = new CustomerTransactionBLL().GetAllocatedProduct();
                tblpatient.Insert(0, new EntityProduct() { ProductId = 0, ProductName = "----Select----" });
                ddlProduct.DataSource = tblpatient;
                ddlProduct.DataTextField = "ProductName";
                ddlProduct.DataValueField = "ProductId";
                ddlProduct.DataBind();
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
                lbl.Text = "STOCK DETAIL REPORT";
                lblFrom.Text = ddlProduct.SelectedItem.Text;
                lblTo.Text = string.Format("{0:dd-MMM-yyyy}", DateTime.Now);
                lblMessage.Text = string.Empty;
                EntityProduct entPro = new CustomerTransactionBLL().GetUnitOfMeasurement(ddlProduct.SelectedValue);
                lblUoM.Text = "U.O.M" + "-" + entPro.UOM;
                List<tblStockDetail> lst = new CustomerTransactionBLL().GetProductTransByProductId(Convert.ToInt32(ddlProduct.SelectedValue));
                tblStockDetail obj = new tblStockDetail()
                {

                };
                lst.Add(obj);
                tblStockDetail objReceived = new tblStockDetail()
                {
                    TransactionType = "Total Inward Amount",
                    InwardAmount = Convert.ToDecimal(lst.Sum(p => p.InwardAmount))
                };
                lst.Add(objReceived);
                tblStockDetail objBills = new tblStockDetail()
                {
                    TransactionType = "Total Outward Amount",
                    OutwardAmount = Convert.ToDecimal(lst.Sum(p => p.OutwardAmount))
                };
                lst.Add(objBills);
                tblStockDetail objIqty = new tblStockDetail()
                {
                    TransactionType = "Total Inward Quntity",
                    InwardQty = Convert.ToInt32(lst.Sum(p => p.InwardQty))
                };
                lst.Add(objIqty);
                tblStockDetail objOqty = new tblStockDetail()
                {
                    TransactionType = "Total Outward Quntity",
                    OutwardQty = Convert.ToInt32(lst.Sum(p => p.OutwardQty))
                };
                lst.Add(objOqty);
                tblStockDetail objFinal = new tblStockDetail()
                {
                    TransactionType = "Balance Quntity",
                    InwardQty = objIqty.InwardQty - objOqty.OutwardQty
                };
                lst.Add(objFinal);
                dgvTestParameter.DataSource = lst;
                dgvTestParameter.DataBind();
                Session["StockDetailsRep"] = ListConverter.ToDataTable(lst);
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
            ddlProduct.SelectedIndex = 0;
            List<EntityCustomerTransaction> lst = new List<EntityCustomerTransaction>();
            dgvTestParameter.DataSource = lst;
            dgvTestParameter.DataBind();
            lbl.Text = string.Empty;
            lblFrom.Text = string.Empty;
            lblTo.Text = string.Empty;
            lblUoM.Text = string.Empty;
        }
        protected void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(lblFrom.Text) && !string.IsNullOrEmpty(lblTo.Text))
                {
                    Session["Details"] = Session["StockDetailsRep"];
                    Response.Redirect("~/ExcelReport/MonthwiseSalExcel.aspx");
                }
                else
                {
                    lblMessage.Text = "Please Select Product Name";
                }

            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
    }
}