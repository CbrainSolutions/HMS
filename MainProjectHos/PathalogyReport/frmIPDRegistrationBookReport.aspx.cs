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
    public partial class frmIPDRegistrationBookReport : System.Web.UI.Page
    {
        PatientInvoiceBLL mobjDeptBLL = new PatientInvoiceBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] != null)
            {
                EntityLogin entLogin = (EntityLogin)Session["user"];

                List<EntityFormMaster> entFormAcc = (List<EntityFormMaster>)Session["AccessibleForm"];
                int Access = (from tbl in entFormAcc
                              where tbl.FormName.Contains("frmIPDRegistrationBookReport")
                              select tbl).Count();

                if (Access > 0)
                {
                    if (!Page.IsPostBack)
                    {
                        Session["MyFlag"] = string.Empty;
                        Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                        //GetDeptCategory();
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

                List<STP_IPDRegistrationBookReportResult> lst = consume.IPDRegistrationBookPatient(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text));
                if (lst != null)
                {
                    lbl.Text = "Monthwise IPD Record Report";
                    lblFrom.Text = txtBillDate.Text;
                    lblTo.Text = txtToDate.Text;
                    DataTable dt = ListConverter.ToDataTable(lst);
                    dt.Columns.Add("colSrNo");
                    DataColumn dcol = new DataColumn();
                    if (dt.Rows.Count > 0)
                    {
                        int count = 1;
                        foreach (DataRow dr in dt.Rows)
                        {
                            dr["colSrNo"] = count;
                            count++;
                        }
                    }
                    dgvTestParameter.DataSource = dt;
                    dgvTestParameter.DataBind();
                    Session["BedConsump"] = dt;
                    //txtBillDate.Text = string.Empty;
                    //txtToDate.Text = string.Empty;
                }

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
            lblTo.Text = string.Empty;
            dgvTestParameter.DataSource = null;
            dgvTestParameter.DataBind();
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