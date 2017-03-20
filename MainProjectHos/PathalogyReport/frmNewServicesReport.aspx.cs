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
    public partial class frmNewServicesReport : System.Web.UI.Page
    {
        PatientInvoiceBLL mobjDeptBLL = new PatientInvoiceBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] != null)
            {
                EntityLogin entLogin = (EntityLogin)Session["user"];

                List<EntityFormMaster> entFormAcc = (List<EntityFormMaster>)Session["AccessibleForm"];
                int Access = (from tbl in entFormAcc
                              where tbl.FormName.Contains("frmNewServicesReport")
                              select tbl).Count();

                if (Access > 0)
                {
                    if (!Page.IsPostBack)
                    {
                        Session["MyFlag"] = string.Empty;
                        Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                        BindEmployee();
                        lblamount.Visible = false;
                        txtamount.Visible = false;
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

        public void BindEmployee()
        {
            SalaryCalculationBLL mobjSalBLL = new SalaryCalculationBLL();
            try
            {
                List<EntityEmployee> lst = mobjSalBLL.GetEmployeeUserList();
                lst.Insert(0, new EntityEmployee { PKId = 0, EmpName = "--Select--" });
                ddlEmployee.DataSource = lst;
                ddlEmployee.DataValueField = "PKId";
                ddlEmployee.DataTextField = "EmpName";
                ddlEmployee.DataBind();
            }
            catch (Exception ex)
            {

            }
        }

        public void GetDeptCategory()
        {
            PatientMasterBLL mobjPatient = new PatientMasterBLL();
            try
            {
                DataTable ldt = new DataTable();
                ldt = mobjPatient.GetDeptCategory();
                ddlServices.DataSource = ldt;
                ddlServices.DataValueField = "CategoryId";
                ddlServices.DataTextField = "CategoryName";
                ddlServices.DataBind();

                ListItem li = new ListItem();
                li.Text = "--Select Services--";
                li.Value = "0";
                ddlServices.Items.Insert(0, li);
            }
            catch (Exception ex)
            {
                // lblMsg.Text = ex.Message;
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
                if (ddlServices.SelectedIndex == 0 && ddlEmployee.SelectedIndex == 0)
                {
                    List<STP_ServiceswiseReportAllResult> lst = consume.NewAllServices(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text));
                    if (lst != null)
                    {
                        lbl.Text = "New Services Datewise Report";
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
                        lblamount.Visible = true;
                        txtamount.Visible = true;

                        decimal amt = 0;
                        foreach (GridViewRow item in dgvTestParameter.Rows)
                        {
                            if (item.Cells[7].Text == "&nbsp;")
                            {
                                item.Cells[7].Text = Convert.ToString(0);
                                amt = amt + Convert.ToDecimal(item.Cells[7].Text);
                            }
                            else
                            {
                                amt = amt + Convert.ToDecimal(item.Cells[7].Text);
                            }
                        }
                        txtamount.Text = Convert.ToString(amt);
                        //txtBillDate.Text = string.Empty;
                        //txtToDate.Text = string.Empty;
                    }
                }
                if (ddlEmployee.SelectedIndex == 0 && ddlServices.SelectedIndex != 0)
                {
                    List<STP_ServiceswiseReportResult> lst = consume.SearchServicewiseReport(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text), Convert.ToString(ddlServices.SelectedItem.Text));
                    if (lst != null)
                    {
                        lbl.Text = "New Serviceswise Report";
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
                        lblamount.Visible = true;
                        txtamount.Visible = true;

                        decimal amt = 0;
                        foreach (GridViewRow item in dgvTestParameter.Rows)
                        {
                            if (item.Cells[7].Text == "&nbsp;")
                            {
                                item.Cells[7].Text = Convert.ToString(0);
                                amt = amt + Convert.ToDecimal(item.Cells[7].Text);
                            }
                            else
                            {
                                amt = amt + Convert.ToDecimal(item.Cells[7].Text);
                            }
                        }
                        txtamount.Text = Convert.ToString(amt);
                        //txtBillDate.Text = string.Empty;
                        //txtToDate.Text = string.Empty;
                    }
                }
                if (ddlEmployee.SelectedIndex != 0 && ddlServices.SelectedIndex == 0)
                {
                    List<STP_ServiceswiseReportUserResult> lst = consume.SearchServicewiseReportUser(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text), Convert.ToString(ddlEmployee.SelectedItem.Text));
                    if (lst != null)
                    {
                        lbl.Text = "New Services Userwise Report";
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
                        lblamount.Visible = true;
                        txtamount.Visible = true;

                        decimal amt = 0;
                        foreach (GridViewRow item in dgvTestParameter.Rows)
                        {
                            if (item.Cells[7].Text == "&nbsp;")
                            {
                                item.Cells[7].Text = Convert.ToString(0);
                                amt = amt + Convert.ToDecimal(item.Cells[7].Text);
                            }
                            else
                            {
                                amt = amt + Convert.ToDecimal(item.Cells[7].Text);
                            }
                        }
                        txtamount.Text = Convert.ToString(amt);
                        //txtBillDate.Text = string.Empty;
                        //txtToDate.Text = string.Empty;
                    }
                }

                if (ddlEmployee.SelectedIndex != 0 && ddlServices.SelectedIndex != 0)
                {
                    List<STP_ServiceswiseReportUserSerResult> lst = consume.SearchServicewiseReportUserSer(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text),
                        Convert.ToString(ddlEmployee.SelectedItem.Text), Convert.ToString(ddlServices.SelectedItem.Text));
                    if (lst != null)
                    {
                        lbl.Text = "New Services Userwise Report";
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
                        lblamount.Visible = true;
                        txtamount.Visible = true;

                        decimal amt = 0;
                        foreach (GridViewRow item in dgvTestParameter.Rows)
                        {
                            if (item.Cells[7].Text == "&nbsp;")
                            {
                                item.Cells[7].Text = Convert.ToString(0);
                                amt = amt + Convert.ToDecimal(item.Cells[7].Text);
                            }
                            else
                            {
                                amt = amt + Convert.ToDecimal(item.Cells[7].Text);
                            }
                        }
                        txtamount.Text = Convert.ToString(amt);
                        //txtBillDate.Text = string.Empty;
                        //txtToDate.Text = string.Empty;
                    }
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
            ddlServices.SelectedIndex = 0;
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