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
    public partial class frmDailyCollectionIPDOPD : System.Web.UI.Page
    {
        PatientInvoiceBLL mobjDeptBLL = new PatientInvoiceBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] != null)
            {
                EntityLogin entLogin = (EntityLogin)Session["user"];

                List<EntityFormMaster> entFormAcc = (List<EntityFormMaster>)Session["AccessibleForm"];
                int Access = (from tbl in entFormAcc
                              where tbl.FormName.Contains("frmDailyCollectionIPDOPD")
                              select tbl).Count();

                if (Access > 0)
                {
                    if (!Page.IsPostBack)
                    {
                        Session["MyFlag"] = string.Empty;
                        Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                        lblAdvance.Visible = false;
                        txtAdvance.Visible = false;
                        lblBalance.Visible = false;
                        txtBalance.Visible = false;
                        lblRefund.Visible = false;
                        txtRefund.Visible = false;
                        lblREceived.Visible = false;
                        txtReceived.Visible = false;
                        GetDeptCategory();
                        BindEmployee();
                        string va = Convert.ToString(Session["UserType12"]);
                        if (Convert.ToString(Session["UserType12"]) != "admin")
                        {
                            Label1.Visible = false;
                            ddlEmployee.Visible = false;
                            Label12.Visible = false;
                            ddlDeptCategory.Visible = false;
                            ddlDeptDoctor.Visible = false;
                            Label20.Visible = false;
                        }
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
                ddlDeptCategory.DataSource = ldt;
                ddlDeptCategory.DataValueField = "CategoryId";
                ddlDeptCategory.DataTextField = "CategoryName";
                ddlDeptCategory.DataBind();

                ListItem li = new ListItem();
                li.Text = "--Select DeptCategory--";
                li.Value = "0";
                ddlDeptCategory.Items.Insert(0, li);
            }
            catch (Exception ex)
            {
                //lblMsg.Text = ex.Message;
            }
        }

        protected void ddlDeptCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                PatientMasterBLL mobjPatient = new PatientMasterBLL();
                int linReligionId = Convert.ToInt32(ddlDeptCategory.SelectedValue);
                DataTable ldt = new DataTable();
                ldt = mobjPatient.GetDeptDoctors(linReligionId);
                if (ldt.Rows.Count > 0 && ldt != null)
                {
                    ddlDeptDoctor.DataSource = ldt;
                    ddlDeptDoctor.DataValueField = "DocAllocId";
                    ddlDeptDoctor.DataTextField = "EmpName";
                    ddlDeptDoctor.DataBind();

                    FillDeptDoctorCast();
                    //ddlDeptDoctor.Enabled = true;
                }
                else
                {
                    //ddlDeptDoctor.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                //lblMsg.Text = ex.Message;
            }
        }

        private void FillDeptDoctorCast()
        {
            try
            {
                ListItem li = new ListItem();
                li.Text = "--Select Dept.Doctor--";
                li.Value = "0";
                ddlDeptDoctor.Items.Insert(0, li);
            }
            catch (Exception ex)
            {
                //lblMsg.Text = ex.Message;
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

                if (Convert.ToString(Session["UserType12"]) != "admin")
                {
                    if (ddlPatientType.SelectedIndex != 0)
                    {
                        List<STP_IPDOPDDailyCollectionReportUserCatResult> lst = consume.SearchIPDOPDCollOtherUser(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text),
                                                                                  Convert.ToString(Session["AdminName"]), Convert.ToString(ddlPatientType.SelectedItem.Text));
                        if (lst != null)
                        {
                            lbl.Text = "Userwise Patient Daily Collection Report";
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

                            lblAdvance.Visible = true;
                            txtAdvance.Visible = true;
                            lblBalance.Visible = true;
                            txtBalance.Visible = true;
                            lblRefund.Visible = true;
                            txtRefund.Visible = true;
                            lblREceived.Visible = true;
                            txtReceived.Visible = true;

                            decimal invCash = consume.GetDDEP(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text),
                                Convert.ToString(Session["AdminName"]), Convert.ToString(ddlPatientType.SelectedItem.Text));
                            decimal invCash1 = consume.GetDDEP1(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text),
                                Convert.ToString(Session["AdminName"]), Convert.ToString(ddlPatientType.SelectedItem.Text));
                            decimal invCash2 = consume.GetDDEP2(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text),
                                Convert.ToString(Session["AdminName"]), Convert.ToString(ddlPatientType.SelectedItem.Text));
                            txtReceived.Text = Convert.ToString(invCash + invCash1);
                            txtAdvance.Text = Convert.ToString(invCash2);

                            decimal billA = 0;
                            foreach (GridViewRow item in dgvTestParameter.Rows)
                            {
                                if ((item.Cells[5].Text) != null)
                                {
                                    if (item.Cells[5].Text == "&nbsp;")
                                    {
                                        item.Cells[5].Text = Convert.ToString(0);
                                        billA = billA + Convert.ToDecimal(item.Cells[5].Text);
                                    }
                                    else
                                    {
                                        billA = billA + Convert.ToDecimal(item.Cells[5].Text);
                                    }
                                }
                            }

                            if (Convert.ToDecimal(billA) - Convert.ToDecimal(invCash + invCash1 + invCash2) < 0)
                            {
                                txtRefund.Text = Convert.ToString(Convert.ToDecimal(invCash + invCash1 + invCash2) - Convert.ToDecimal(billA));
                                txtBalance.Text = Convert.ToString(0);
                            }
                            else
                            {
                                txtBalance.Text = Convert.ToString(Convert.ToDecimal(billA) - Convert.ToDecimal(invCash + invCash1 + invCash2));
                                txtRefund.Text = Convert.ToString(0);
                            }
                        }
                    }
                    if (ddlPatientType.SelectedIndex == 0)
                    {
                        List<STP_IPDOPDDailyCollectionReportUserwiseResult> lst = consume.SearchIPDOPdCollUserwise(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text), Convert.ToString(Session["AdminName"]));
                        if (lst != null)
                        {
                            lbl.Text = "Userwise Patient Daily Collection Report";
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

                            lblAdvance.Visible = true;
                            txtAdvance.Visible = true;
                            lblBalance.Visible = true;
                            txtBalance.Visible = true;
                            lblRefund.Visible = true;
                            txtRefund.Visible = true;
                            lblREceived.Visible = true;
                            txtReceived.Visible = true;

                            decimal invCash = consume.GetDDEPb1(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text),
                                Convert.ToString(Session["AdminName"]));
                            decimal invCash1 = consume.GetDDEPb2(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text),
                                Convert.ToString(Session["AdminName"]));
                            decimal invCash2 = consume.GetDDEb3(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text),
                                Convert.ToString(Session["AdminName"]));
                            txtReceived.Text = Convert.ToString(invCash + invCash1);
                            txtAdvance.Text = Convert.ToString(invCash2);

                            decimal billA = 0;
                            foreach (GridViewRow item in dgvTestParameter.Rows)
                            {
                                if ((item.Cells[5].Text) != null)
                                {
                                    if (item.Cells[5].Text == "&nbsp;")
                                    {
                                        item.Cells[5].Text = Convert.ToString(0);
                                        billA = billA + Convert.ToDecimal(item.Cells[5].Text);
                                    }
                                    else
                                    {
                                        billA = billA + Convert.ToDecimal(item.Cells[5].Text);
                                    }
                                }
                            }

                            if (Convert.ToDecimal(billA) - Convert.ToDecimal(invCash + invCash1 + invCash2) < 0)
                            {
                                txtRefund.Text = Convert.ToString(Convert.ToDecimal(invCash + invCash1 + invCash2) - Convert.ToDecimal(billA));
                                txtBalance.Text = Convert.ToString(0);
                            }
                            else
                            {
                                txtBalance.Text = Convert.ToString(Convert.ToDecimal(billA) - Convert.ToDecimal(invCash + invCash1 + invCash2));
                                txtRefund.Text = Convert.ToString(0);
                            }
                        }
                    }

                }
                else
                {
                    if (ddlPatientType.SelectedItem.Text == "OPD" && ddlEmployee.SelectedIndex == 0 && ddlDeptCategory.SelectedIndex == 0)
                    {
                        List<STP_IPDOPDDailyCollectionReportResult> lst = consume.SearchIPDOPDDailyCollReport(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text), Convert.ToString(ddlPatientType.SelectedItem.Text));
                        if (lst != null)
                        {
                            lbl.Text = "Daily Collection OPD Report";
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
                            lblAdvance.Visible = true;
                            txtAdvance.Visible = true;
                            lblBalance.Visible = true;
                            txtBalance.Visible = true;
                            lblRefund.Visible = true;
                            txtRefund.Visible = true;
                            lblREceived.Visible = true;
                            txtReceived.Visible = true;

                            decimal invCash = consume.GetDDEPc1(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text),
                                Convert.ToString(ddlPatientType.SelectedItem.Text));
                            decimal invCash1 = consume.GetDDEPc2(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text),
                                Convert.ToString(ddlPatientType.SelectedItem.Text));
                            decimal invCash2 = consume.GetDDEPc3(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text),
                               Convert.ToString(ddlPatientType.SelectedItem.Text));
                            txtReceived.Text = Convert.ToString(invCash + invCash1);
                            txtAdvance.Text = Convert.ToString(invCash2);

                            decimal billA = 0;
                            foreach (GridViewRow item in dgvTestParameter.Rows)
                            {
                                if ((item.Cells[5].Text) != null)
                                {
                                    if (item.Cells[5].Text == "&nbsp;")
                                    {
                                        item.Cells[5].Text = Convert.ToString(0);
                                        billA = billA + Convert.ToDecimal(item.Cells[5].Text);
                                    }
                                    else
                                    {
                                        billA = billA + Convert.ToDecimal(item.Cells[5].Text);
                                    }
                                }
                            }

                            if (Convert.ToDecimal(billA) - Convert.ToDecimal(invCash + invCash1 + invCash2) < 0)
                            {
                                txtRefund.Text = Convert.ToString(Convert.ToDecimal(invCash + invCash1 + invCash2) - Convert.ToDecimal(billA));
                                txtBalance.Text = Convert.ToString(0);
                            }
                            else
                            {
                                txtBalance.Text = Convert.ToString(Convert.ToDecimal(billA) - Convert.ToDecimal(invCash + invCash1 + invCash2));
                                txtRefund.Text = Convert.ToString(0);
                            }
                        }
                    }

                    if (ddlPatientType.SelectedItem.Text == "IPD" && ddlEmployee.SelectedIndex == 0 && ddlDeptCategory.SelectedIndex == 0)
                    {
                        List<STP_IPDOPDDailyCollectionReportResult> lst = consume.SearchIPDOPDDailyCollReport(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text), Convert.ToString(ddlPatientType.SelectedItem.Text));
                        if (lst != null)
                        {
                            lbl.Text = "Daily Collection IPD Report";
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

                            lblAdvance.Visible = true;
                            txtAdvance.Visible = true;
                            lblBalance.Visible = true;
                            txtBalance.Visible = true;
                            lblRefund.Visible = true;
                            txtRefund.Visible = true;
                            lblREceived.Visible = true;
                            txtReceived.Visible = true;

                            decimal invCash = consume.GetDDEPc1(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text),
                                Convert.ToString(ddlPatientType.SelectedItem.Text));
                            decimal invCash1 = consume.GetDDEPc2(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text),
                                Convert.ToString(ddlPatientType.SelectedItem.Text));
                            decimal invCash2 = consume.GetDDEPc3(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text),
                                Convert.ToString(ddlPatientType.SelectedItem.Text));
                            txtReceived.Text = Convert.ToString(invCash + invCash1);
                            txtAdvance.Text = Convert.ToString(invCash2);

                            decimal billA = 0;
                            foreach (GridViewRow item in dgvTestParameter.Rows)
                            {
                                if ((item.Cells[5].Text) != null)
                                {
                                    if (item.Cells[5].Text == "&nbsp;")
                                    {
                                        item.Cells[5].Text = Convert.ToString(0);
                                        billA = billA + Convert.ToDecimal(item.Cells[5].Text);
                                    }
                                    else
                                    {
                                        billA = billA + Convert.ToDecimal(item.Cells[5].Text);
                                    }
                                }
                            }

                            if (Convert.ToDecimal(billA) - Convert.ToDecimal(invCash + invCash1 + invCash2) < 0)
                            {
                                txtRefund.Text = Convert.ToString(Convert.ToDecimal(invCash + invCash1 + invCash2) - Convert.ToDecimal(billA));
                                txtBalance.Text = Convert.ToString(0);
                            }
                            else
                            {
                                txtBalance.Text = Convert.ToString(Convert.ToDecimal(billA) - Convert.ToDecimal(invCash + invCash1 + invCash2));
                                txtRefund.Text = Convert.ToString(0);
                            }
                        }
                    }

                    if (ddlPatientType.SelectedItem.Text == "Company" && ddlEmployee.SelectedIndex == 0 && ddlDeptCategory.SelectedIndex == 0)
                    {
                        List<STP_IPDOPDDailyCollectionCompanyReportResult> lst = consume.SearchIPDOPDDailyCollCompanyReport(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text));
                        if (lst != null)
                        {
                            lbl.Text = "Daily Collection Company Patient Report";
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

                            lblAdvance.Visible = true;
                            txtAdvance.Visible = true;
                            lblBalance.Visible = true;
                            txtBalance.Visible = true;
                            lblRefund.Visible = true;
                            txtRefund.Visible = true;
                            lblREceived.Visible = true;
                            txtReceived.Visible = true;

                            decimal invCash = consume.GetDDEPd1(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text),
                                 Convert.ToString(ddlPatientType.SelectedItem.Text));
                            decimal invCash1 = consume.GetDDEPd2(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text),
                                Convert.ToString(ddlPatientType.SelectedItem.Text));
                            decimal invCash2 = consume.GetDDEPd3(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text),
                                Convert.ToString(ddlPatientType.SelectedItem.Text));
                            txtReceived.Text = Convert.ToString(invCash + invCash1);
                            txtAdvance.Text = Convert.ToString(invCash2);

                            decimal billA = 0;
                            foreach (GridViewRow item in dgvTestParameter.Rows)
                            {
                                if ((item.Cells[5].Text) != null)
                                {
                                    if (item.Cells[5].Text == "&nbsp;")
                                    {
                                        item.Cells[5].Text = Convert.ToString(0);
                                        billA = billA + Convert.ToDecimal(item.Cells[5].Text);
                                    }
                                    else
                                    {
                                        billA = billA + Convert.ToDecimal(item.Cells[5].Text);
                                    }
                                }
                            }

                            if (Convert.ToDecimal(billA) - Convert.ToDecimal(invCash + invCash1 + invCash2) < 0)
                            {
                                txtRefund.Text = Convert.ToString(Convert.ToDecimal(invCash + invCash1 + invCash2) - Convert.ToDecimal(billA));
                                txtBalance.Text = Convert.ToString(0);
                            }
                            else
                            {
                                txtBalance.Text = Convert.ToString(Convert.ToDecimal(billA) - Convert.ToDecimal(invCash + invCash1 + invCash2));
                                txtRefund.Text = Convert.ToString(0);
                            }
                        }
                    }

                    if (ddlPatientType.SelectedItem.Text == "Insurance" && ddlEmployee.SelectedIndex == 0 && ddlDeptCategory.SelectedIndex == 0)
                    {
                        List<STP_IPDOPDDailyCollectionInsuranceReportResult> lst = consume.SearchIPDOPDDailyCollInsuranceReport(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text));
                        if (lst != null)
                        {
                            lbl.Text = "Daily Collection Insurance Patient Report";
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

                            lblAdvance.Visible = true;
                            txtAdvance.Visible = true;
                            lblBalance.Visible = true;
                            txtBalance.Visible = true;
                            lblRefund.Visible = true;
                            txtRefund.Visible = true;
                            lblREceived.Visible = true;
                            txtReceived.Visible = true;

                            decimal invCash = consume.GetDDEPe1(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text),
                                  Convert.ToString(ddlPatientType.SelectedItem.Text));
                            decimal invCash1 = consume.GetDDEPe2(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text),
                                Convert.ToString(ddlPatientType.SelectedItem.Text));
                            decimal invCash2 = consume.GetDDEPe3(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text),
                                Convert.ToString(ddlPatientType.SelectedItem.Text));
                            txtReceived.Text = Convert.ToString(invCash + invCash1);
                            txtAdvance.Text = Convert.ToString(invCash2);

                            decimal billA = 0;
                            foreach (GridViewRow item in dgvTestParameter.Rows)
                            {
                                if ((item.Cells[5].Text) != null)
                                {
                                    if (item.Cells[5].Text == "&nbsp;")
                                    {
                                        item.Cells[5].Text = Convert.ToString(0);
                                        billA = billA + Convert.ToDecimal(item.Cells[5].Text);
                                    }
                                    else
                                    {
                                        billA = billA + Convert.ToDecimal(item.Cells[5].Text);
                                    }
                                }
                            }

                            if (Convert.ToDecimal(billA) - Convert.ToDecimal(invCash + invCash1 + invCash2) < 0)
                            {
                                txtRefund.Text = Convert.ToString(Convert.ToDecimal(invCash + invCash1 + invCash2) - Convert.ToDecimal(billA));
                                txtBalance.Text = Convert.ToString(0);
                            }
                            else
                            {
                                txtBalance.Text = Convert.ToString(Convert.ToDecimal(billA) - Convert.ToDecimal(invCash + invCash1 + invCash2));
                                txtRefund.Text = Convert.ToString(0);
                            }
                        }
                    }

                    if (ddlPatientType.SelectedIndex == 0 && ddlEmployee.SelectedIndex != 0 && ddlDeptCategory.SelectedIndex == 0)
                    {
                        List<STP_IPDOPDDailyCollectionReportUserwiseResult> lst = consume.SearchIPDOPdCollUserwise(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text), Convert.ToString(ddlEmployee.SelectedItem.Text));
                        if (lst != null)
                        {
                            lbl.Text = "Userwise Patient Daily Collection Report";
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

                            lblAdvance.Visible = true;
                            txtAdvance.Visible = true;
                            lblBalance.Visible = true;
                            txtBalance.Visible = true;
                            lblRefund.Visible = true;
                            txtRefund.Visible = true;
                            lblREceived.Visible = true;
                            txtReceived.Visible = true;

                            decimal invCash = consume.GetDDEPf1(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text),
                                Convert.ToString(ddlEmployee.SelectedItem.Text));
                            decimal invCash1 = consume.GetDDEPf2(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text),
                                Convert.ToString(ddlEmployee.SelectedItem.Text));
                            decimal invCash2 = consume.GetDDEPf3(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text),
                                Convert.ToString(ddlEmployee.SelectedItem.Text));
                            txtReceived.Text = Convert.ToString(invCash + invCash1);
                            txtAdvance.Text = Convert.ToString(invCash2);

                            decimal billA = 0;
                            foreach (GridViewRow item in dgvTestParameter.Rows)
                            {
                                if ((item.Cells[5].Text) != null)
                                {
                                    if (item.Cells[5].Text == "&nbsp;")
                                    {
                                        item.Cells[5].Text = Convert.ToString(0);
                                        billA = billA + Convert.ToDecimal(item.Cells[5].Text);
                                    }
                                    else
                                    {
                                        billA = billA + Convert.ToDecimal(item.Cells[5].Text);
                                    }
                                }
                            }

                            if (Convert.ToDecimal(billA) - Convert.ToDecimal(invCash + invCash1 + invCash2) < 0)
                            {
                                txtRefund.Text = Convert.ToString(Convert.ToDecimal(invCash + invCash1 + invCash2) - Convert.ToDecimal(billA));
                                txtBalance.Text = Convert.ToString(0);
                            }
                            else
                            {
                                txtBalance.Text = Convert.ToString(Convert.ToDecimal(billA) - Convert.ToDecimal(invCash + invCash1 + invCash2));
                                txtRefund.Text = Convert.ToString(0);
                            }
                        }
                    }

                    if (ddlPatientType.SelectedIndex != 0 && ddlEmployee.SelectedIndex != 0 && ddlDeptCategory.SelectedIndex == 0 && ddlDeptDoctor.SelectedIndex == 0)
                    {
                        List<STP_IPDOPDDailyCollectionReportUserCatResult> lst = consume.SearchIPDOPDCollUserCategorywise(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text),
                                                                                  Convert.ToString(ddlEmployee.SelectedItem.Text), Convert.ToString(ddlPatientType.SelectedItem.Text));
                        if (lst != null)
                        {
                            lbl.Text = "Userwise Patient Daily Collection Report";
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

                            lblAdvance.Visible = true;
                            txtAdvance.Visible = true;
                            lblBalance.Visible = true;
                            txtBalance.Visible = true;
                            lblRefund.Visible = true;
                            txtRefund.Visible = true;
                            lblREceived.Visible = true;
                            txtReceived.Visible = true;

                            decimal invCash = consume.GetDDEPg1(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text),
                                Convert.ToString(ddlEmployee.SelectedItem.Text), Convert.ToString(ddlPatientType.SelectedItem.Text));
                            decimal invCash1 = consume.GetDDEPg2(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text),
                                Convert.ToString(ddlEmployee.SelectedItem.Text), Convert.ToString(ddlPatientType.SelectedItem.Text));
                            decimal invCash2 = consume.GetDDEPg3(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text),
                                Convert.ToString(ddlEmployee.SelectedItem.Text), Convert.ToString(ddlPatientType.SelectedItem.Text));
                            txtReceived.Text = Convert.ToString(invCash + invCash1);
                            txtAdvance.Text = Convert.ToString(invCash2);

                            decimal billA = 0;
                            foreach (GridViewRow item in dgvTestParameter.Rows)
                            {
                                if ((item.Cells[5].Text) != null)
                                {
                                    if (item.Cells[5].Text == "&nbsp;")
                                    {
                                        item.Cells[5].Text = Convert.ToString(0);
                                        billA = billA + Convert.ToDecimal(item.Cells[5].Text);
                                    }
                                    else
                                    {
                                        billA = billA + Convert.ToDecimal(item.Cells[5].Text);
                                    }
                                }
                            }

                            if (Convert.ToDecimal(billA) - Convert.ToDecimal(invCash + invCash1 + invCash2) < 0)
                            {
                                txtRefund.Text = Convert.ToString(Convert.ToDecimal(invCash + invCash1 + invCash2) - Convert.ToDecimal(billA));
                                txtBalance.Text = Convert.ToString(0);
                            }
                            else
                            {
                                txtBalance.Text = Convert.ToString(Convert.ToDecimal(billA) - Convert.ToDecimal(invCash + invCash1 + invCash2));
                                txtRefund.Text = Convert.ToString(0);
                            }
                        }
                    }

                    if (ddlDeptCategory.SelectedIndex != 0 && ddlEmployee.SelectedIndex == 0 && ddlDeptDoctor.SelectedIndex == 0 && ddlPatientType.SelectedIndex == 0)
                    {
                        List<STP_IPDOPDDailyCollectionReportDepartmentwiseResult> lst = consume.SearchIPDOPDCollDepartmentwise(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text),
                                                                                  Convert.ToString(ddlDeptCategory.SelectedItem.Text));
                        if (lst != null)
                        {
                            lbl.Text = "Departmentwise Patient Daily Collection Report";
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
                            Session["BedConsump"] = dt;

                            lblAdvance.Visible = true;
                            txtAdvance.Visible = true;
                            lblBalance.Visible = true;
                            txtBalance.Visible = true;
                            lblRefund.Visible = true;
                            txtRefund.Visible = true;
                            lblREceived.Visible = true;
                            txtReceived.Visible = true;

                            decimal invCash = consume.GetDDEPi1(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text),
                                Convert.ToString(ddlDeptCategory.SelectedItem.Text));
                            decimal invCash1 = consume.GetDDEPi2(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text),
                                Convert.ToString(ddlDeptCategory.SelectedItem.Text));
                            decimal invCash2 = consume.GetDDEPi3(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text),
                                Convert.ToString(ddlDeptCategory.SelectedItem.Text));
                            txtReceived.Text = Convert.ToString(invCash + invCash1);
                            txtAdvance.Text = Convert.ToString(invCash2);

                            decimal billA = 0;
                            foreach (GridViewRow item in dgvTestParameter.Rows)
                            {
                                if ((item.Cells[5].Text) != null)
                                {
                                    if (item.Cells[5].Text == "&nbsp;")
                                    {
                                        item.Cells[5].Text = Convert.ToString(0);
                                        billA = billA + Convert.ToDecimal(item.Cells[5].Text);
                                    }
                                    else
                                    {
                                        billA = billA + Convert.ToDecimal(item.Cells[5].Text);
                                    }
                                }
                            }

                            if (Convert.ToDecimal(billA) - Convert.ToDecimal(invCash + invCash1 + invCash2) < 0)
                            {
                                txtRefund.Text = Convert.ToString(Convert.ToDecimal(invCash + invCash1 + invCash2) - Convert.ToDecimal(billA));
                                txtBalance.Text = Convert.ToString(0);
                            }
                            else
                            {
                                txtBalance.Text = Convert.ToString(Convert.ToDecimal(billA) - Convert.ToDecimal(invCash + invCash1 + invCash2));
                                txtRefund.Text = Convert.ToString(0);
                            }
                        }
                    }

                    if (ddlDeptCategory.SelectedIndex != 0 && ddlEmployee.SelectedIndex == 0 && ddlDeptDoctor.SelectedIndex != 0 && ddlPatientType.SelectedIndex == 0)
                    {
                        List<STP_IPDOPDDailyCollectionReportDoctorwiseResult> lst = consume.SearchIPDOPDCollDoctorwise(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text),
                                                                                  Convert.ToString(ddlDeptCategory.SelectedItem.Text), Convert.ToString(ddlDeptDoctor.SelectedItem.Text));
                        if (lst != null)
                        {
                            lbl.Text = "Consult Doctorwise Patient Daily Collection Report";
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

                            lblAdvance.Visible = true;
                            txtAdvance.Visible = true;
                            lblBalance.Visible = true;
                            txtBalance.Visible = true;
                            lblRefund.Visible = true;
                            txtRefund.Visible = true;
                            lblREceived.Visible = true;
                            txtReceived.Visible = true;

                            decimal invCash = consume.GetDDEPj1(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text),
                               Convert.ToString(ddlDeptCategory.SelectedItem.Text), Convert.ToString(ddlDeptDoctor.SelectedItem.Text));
                            decimal invCash1 = consume.GetDDEPj2(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text),
                                Convert.ToString(ddlDeptCategory.SelectedItem.Text), Convert.ToString(ddlDeptDoctor.SelectedItem.Text));
                            decimal invCash2 = consume.GetDDEPj3(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text),
                                Convert.ToString(ddlDeptCategory.SelectedItem.Text), Convert.ToString(ddlDeptDoctor.SelectedItem.Text));
                            txtReceived.Text = Convert.ToString(invCash + invCash1);
                            txtAdvance.Text = Convert.ToString(invCash2);

                            decimal billA = 0;
                            foreach (GridViewRow item in dgvTestParameter.Rows)
                            {
                                if ((item.Cells[5].Text) != null)
                                {
                                    if (item.Cells[5].Text == "&nbsp;")
                                    {
                                        item.Cells[5].Text = Convert.ToString(0);
                                        billA = billA + Convert.ToDecimal(item.Cells[5].Text);
                                    }
                                    else
                                    {
                                        billA = billA + Convert.ToDecimal(item.Cells[5].Text);
                                    }
                                }
                            }

                            if (Convert.ToDecimal(billA) - Convert.ToDecimal(invCash + invCash1 + invCash2) < 0)
                            {
                                txtRefund.Text = Convert.ToString(Convert.ToDecimal(invCash + invCash1 + invCash2) - Convert.ToDecimal(billA));
                                txtBalance.Text = Convert.ToString(0);
                            }
                            else
                            {
                                txtBalance.Text = Convert.ToString(Convert.ToDecimal(billA) - Convert.ToDecimal(invCash + invCash1 + invCash2));
                                txtRefund.Text = Convert.ToString(0);
                            }
                        }
                    }

                    if (ddlDeptCategory.SelectedIndex != 0 && ddlEmployee.SelectedIndex == 0 && ddlDeptDoctor.SelectedIndex != 0 && ddlPatientType.SelectedIndex != 0)
                    {
                        List<STP_IPDOPDDailyCollectionReportDeptPatCatResult> lst = consume.SearchIPDOPDCollDoctDeptwise(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text),
                                                                                  Convert.ToString(ddlDeptCategory.SelectedItem.Text), Convert.ToString(ddlDeptDoctor.SelectedItem.Text),
                                                                                    Convert.ToString(ddlPatientType.SelectedItem.Text));
                        if (lst != null)
                        {
                            lbl.Text = "Daily Collection Report";
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

                            lblAdvance.Visible = true;
                            txtAdvance.Visible = true;
                            lblBalance.Visible = true;
                            txtBalance.Visible = true;
                            lblRefund.Visible = true;
                            txtRefund.Visible = true;
                            lblREceived.Visible = true;
                            txtReceived.Visible = true;

                            decimal invCash = consume.GetDDEPk1(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text),
                                Convert.ToString(ddlDeptCategory.SelectedItem.Text), Convert.ToString(ddlDeptDoctor.SelectedItem.Text),
                                                                                    Convert.ToString(ddlPatientType.SelectedItem.Text));
                            decimal invCash1 = consume.GetDDEPk2(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text),
                                Convert.ToString(ddlDeptCategory.SelectedItem.Text), Convert.ToString(ddlDeptDoctor.SelectedItem.Text),
                                                                                    Convert.ToString(ddlPatientType.SelectedItem.Text));
                            decimal invCash2 = consume.GetDDEPk3(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text),
                                Convert.ToString(ddlDeptCategory.SelectedItem.Text), Convert.ToString(ddlDeptDoctor.SelectedItem.Text),
                                                                                    Convert.ToString(ddlPatientType.SelectedItem.Text));
                            txtReceived.Text = Convert.ToString(invCash + invCash1);
                            txtAdvance.Text = Convert.ToString(invCash2);

                            decimal billA = 0;
                            foreach (GridViewRow item in dgvTestParameter.Rows)
                            {
                                if ((item.Cells[5].Text) != null)
                                {
                                    if (item.Cells[5].Text == "&nbsp;")
                                    {
                                        item.Cells[5].Text = Convert.ToString(0);
                                        billA = billA + Convert.ToDecimal(item.Cells[5].Text);
                                    }
                                    else
                                    {
                                        billA = billA + Convert.ToDecimal(item.Cells[5].Text);
                                    }
                                }
                            }

                            if (Convert.ToDecimal(billA) - Convert.ToDecimal(invCash + invCash1 + invCash2) < 0)
                            {
                                txtRefund.Text = Convert.ToString(Convert.ToDecimal(invCash + invCash1 + invCash2) - Convert.ToDecimal(billA));
                                txtBalance.Text = Convert.ToString(0);
                            }
                            else
                            {
                                txtBalance.Text = Convert.ToString(Convert.ToDecimal(billA) - Convert.ToDecimal(invCash + invCash1 + invCash2));
                                txtRefund.Text = Convert.ToString(0);
                            }
                        }
                    }

                    if (ddlDeptCategory.SelectedIndex == 0 && ddlDeptDoctor.SelectedIndex <= 0 && ddlEmployee.SelectedIndex != 0 && ddlPatientType.SelectedIndex != 0)
                    {
                        List<STP_IPDOPDDailyCollectionReportpattyUserwiseResult> lst = consume.SearchIPDOPDCollpattyuser(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text),
                                                                                   Convert.ToString(ddlEmployee.SelectedItem.Text),
                                                                                    Convert.ToString(ddlPatientType.SelectedItem.Text));
                        if (lst != null)
                        {
                            lbl.Text = "Daily Collection Report";
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

                            lblAdvance.Visible = true;
                            txtAdvance.Visible = true;
                            lblBalance.Visible = true;
                            txtBalance.Visible = true;
                            lblRefund.Visible = true;
                            txtRefund.Visible = true;
                            lblREceived.Visible = true;
                            txtReceived.Visible = true;

                            decimal invCash = consume.GetDDEP(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text),
                                Convert.ToString(ddlEmployee.SelectedItem.Text), Convert.ToString(ddlPatientType.SelectedItem.Text));
                            decimal invCash1 = consume.GetDDEP1(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text),
                                Convert.ToString(ddlEmployee.SelectedItem.Text), Convert.ToString(ddlPatientType.SelectedItem.Text));
                            decimal invCash2 = consume.GetDDEP2(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text),
                                Convert.ToString(ddlEmployee.SelectedItem.Text), Convert.ToString(ddlPatientType.SelectedItem.Text));
                            txtReceived.Text = Convert.ToString(invCash + invCash1);
                            txtAdvance.Text = Convert.ToString(invCash2);

                            decimal billA = 0;
                            foreach (GridViewRow item in dgvTestParameter.Rows)
                            {
                                if ((item.Cells[5].Text) != null)
                                {
                                    if (item.Cells[5].Text == "&nbsp;")
                                    {
                                        item.Cells[5].Text = Convert.ToString(0);
                                        billA = billA + Convert.ToDecimal(item.Cells[5].Text);
                                    }
                                    else
                                    {
                                        billA = billA + Convert.ToDecimal(item.Cells[5].Text);
                                    }
                                }
                            }

                            if (Convert.ToDecimal(billA) - Convert.ToDecimal(invCash + invCash1 + invCash2) < 0)
                            {
                                txtRefund.Text = Convert.ToString(Convert.ToDecimal(invCash + invCash1 + invCash2) - Convert.ToDecimal(billA));
                                txtBalance.Text = Convert.ToString(0);
                            }
                            else
                            {
                                txtBalance.Text = Convert.ToString(Convert.ToDecimal(billA) - Convert.ToDecimal(invCash + invCash1 + invCash2));
                                txtRefund.Text = Convert.ToString(0);
                            }
                        }
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
            lblAdvance.Visible = false;
            txtAdvance.Visible = false;
            lblBalance.Visible = false;
            txtBalance.Visible = false;
            lblRefund.Visible = false;
            txtRefund.Visible = false;
            lblREceived.Visible = false;
            txtReceived.Visible = false;
            lbl.Text = "";

            ddlPatientType.SelectedIndex = -1;
            ddlDeptCategory.SelectedIndex = 0;
            ddlDeptDoctor.SelectedIndex = 0;
            lblTo.Text = string.Empty;
            lblFrom.Text = string.Empty;
            lblMessage.Text = string.Empty;
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