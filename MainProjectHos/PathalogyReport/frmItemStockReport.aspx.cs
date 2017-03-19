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
    public partial class frmItemStockReport : System.Web.UI.Page
    {
        ProductBLL mobjDeptBLL = new ProductBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] != null)
            {
                EntityLogin entLogin = (EntityLogin)Session["user"];

                List<EntityFormMaster> entFormAcc = (List<EntityFormMaster>)Session["AccessibleForm"];
                int Access = (from tbl in entFormAcc
                              where tbl.FormName.Contains("frmItemStockReport")
                              select tbl).Count();

                if (Access > 0)
                {
                    if (!Page.IsPostBack)
                    {
                        Session["MyFlag"] = string.Empty;
                        Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                        GetStockDetail();
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

        private void GetStockDetail()
        {
            try
            {
                List<StockInfo> lst = mobjDeptBLL.GetRawItemsForReport();
                if (lst != null)
                {
                    lbl.Text = "Stock Detail Report";
                    dgvTestParameter.DataSource = lst;
                    dgvTestParameter.DataBind();
                    Session["BedConsump"] = ListConverter.ToDataTable(lst);
                }
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
                Session["Details"] = Session["BedConsump"];
                Response.Redirect("~/ExcelReport/MonthwiseSalExcel.aspx");

            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
    }
}