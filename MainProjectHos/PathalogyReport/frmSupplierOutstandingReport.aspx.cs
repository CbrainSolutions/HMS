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
    public partial class frmSupplierOutstandingReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] != null)
            {
                EntityLogin entLogin = (EntityLogin)Session["user"];

                List<EntityFormMaster> entFormAcc = (List<EntityFormMaster>)Session["AccessibleForm"];
                int Access = (from tbl in entFormAcc
                              where tbl.FormName.Contains("frmSupplierOutstandingReport")
                              select tbl).Count();

                if (Access > 0)
                {
                    if (!Page.IsPostBack)
                    {
                        Session["MyFlag"] = string.Empty;
                        Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                SelectOutstandingReport();
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
                if (!string.IsNullOrEmpty(lblFrom.Text) && !string.IsNullOrEmpty(lblTo.Text))
                {
                    Session["Details"] = Session["SuppOutstand"];
                    Response.Redirect("~/ExcelReport/MonthwiseSalExcel.aspx", false);
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

        private void SelectOutstandingReport()
        {
            try
            {
                OutstandingBLL consume = new OutstandingBLL();
                List<STP_SupplierOutstandingReportResult> lst = consume.SearchSupplierOutstanding(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text));
                if (lst != null)
                {
                    lbl.Text = "Supplier Outstanding Report";
                    lblFrom.Text = txtBillDate.Text;
                    lblTo.Text = txtToDate.Text;
                    dgvTestParameter.DataSource = lst;
                    dgvTestParameter.DataBind();
                    Session["SuppOutstand"] = ListConverter.ToDataTable(lst);
                    txtBillDate.Text = string.Empty;
                    txtToDate.Text = string.Empty;
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
                txtBillDate.Text = string.Empty;
                txtToDate.Text = string.Empty;
                lblFrom.Text = string.Empty;
                lblTo.Text = string.Empty;
                lbl.Text = string.Empty;
                dgvTestParameter.DataSource = new List<STP_SupplierOutstandingReportResult>();
                dgvTestParameter.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }

        }
    }
}