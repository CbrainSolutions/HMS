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

namespace MainProjectHos.PathalogyReport
{
    public partial class frmIPDPatientReport : System.Web.UI.Page
    {
        PatientInvoiceBLL mobjDeptBLL = new PatientInvoiceBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] != null)
            {
                EntityLogin entLogin = (EntityLogin)Session["user"];

                List<EntityFormMaster> entFormAcc = (List<EntityFormMaster>)Session["AccessibleForm"];
                int Access = (from tbl in entFormAcc
                              where tbl.FormName.Contains("frmIPDPatientReport")
                              select tbl).Count();

                if (Access > 0)
                {
                    if (!Page.IsPostBack)
                    {
                        Session["MyFlag"] = string.Empty;
                        Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                        GetDeptCategory();
                        BindCategory();
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

        private void BindCategory()
        {
            try
            {
                DataTable tblCat = new RoomMasterBLL().GetAllCategory();
                DataRow dr = tblCat.NewRow();
                dr["PKId"] = 0;
                dr["CategoryDesc"] = "---Select---";
                tblCat.Rows.InsertAt(dr, 0);

                ddlWardCategory.DataSource = tblCat;
                ddlWardCategory.DataValueField = "PKId";
                ddlWardCategory.DataTextField = "CategoryDesc";
                ddlWardCategory.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
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

                if (ddlDeptCategory.SelectedIndex != 0 && ddlDeptDoctor.SelectedIndex == 0 && ddlPatientType.SelectedIndex == 0)
                {
                    List<STP_DeptCategorywisePatientDetailsResult> lst = consume.DeptCategorywisePatientDetails(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text), Convert.ToInt32(ddlDeptCategory.SelectedValue));
                    if (lst != null)
                    {
                        lbl.Text = "Patient Details Report";
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

                    }
                }

                if (ddlDeptCategory.SelectedIndex != 0 && ddlDeptDoctor.SelectedIndex != 0 && ddlPatientType.SelectedIndex != 0)
                {
                    List<STP_DepartmentwisePatientDetailsResult> lst = consume.SearchDepartmentwise(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text), Convert.ToInt32(ddlDeptCategory.SelectedValue), Convert.ToInt32(ddlDeptDoctor.SelectedValue), Convert.ToString(ddlPatientType.SelectedItem.Text));
                    if (lst != null)
                    {
                        lbl.Text = "Patient Details Report";
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

                    }
                }

                if (ddlDeptCategory.SelectedIndex != 0 && ddlDeptDoctor.SelectedIndex != 0 && ddlPatientType.SelectedIndex == 0)
                {
                    List<STP_DeptCatDoctorwisePatientDetailsResult> lst = consume.DeptCatDoctorwisePatientDetails(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text), Convert.ToInt32(ddlDeptCategory.SelectedValue), Convert.ToInt32(ddlDeptDoctor.SelectedValue));
                    if (lst != null)
                    {
                        lbl.Text = "Patient Details Report";
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

                    }
                }

                if (ddlPatientType.SelectedIndex != 0 && ddlDeptCategory.SelectedIndex == 0)
                {
                    List<STP_PatientTypewisePatientDetailsResult> lst = consume.SearchDepartmentwise(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text), Convert.ToString(ddlPatientType.SelectedItem.Text));
                    if (lst != null)
                    {
                        lbl.Text = "Departmentwise Patient Details Report";
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
                    }
                }

                if (ddlWardCategory.SelectedIndex != 0 && ddlDeptCategory.SelectedIndex == 0 && ddlDeptDoctor.SelectedIndex <= 0 && ddlPatientType.SelectedIndex == 0)
                {
                    List<STP_WardwisePatientDetailsResult> lst = consume.SearchWardwise(Convert.ToDateTime(txtBillDate.Text), Convert.ToDateTime(txtToDate.Text), Convert.ToString(ddlWardCategory.SelectedItem.Text));
                    if (lst != null)
                    {
                        lbl.Text = " Ward Category Wise Patient Details Report";
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
            ddlDeptCategory.SelectedIndex = 0;
            ddlDeptDoctor.SelectedIndex = 0;
            ddlPatientType.SelectedIndex = 0;
            lblFrom.Text = string.Empty;
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