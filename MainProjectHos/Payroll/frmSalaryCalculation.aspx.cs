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
using System.Drawing;

namespace MainProjectHos.Payroll
{
    public partial class frmSalaryCalculation : System.Web.UI.Page
    {
        SalaryCalculationBLL mobjSalBLL = new SalaryCalculationBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] != null)
            {
                EntityLogin entLogin = (EntityLogin)Session["user"];

                List<EntityFormMaster> entFormAcc = (List<EntityFormMaster>)Session["AccessibleForm"];
                int Access = (from tbl in entFormAcc
                              where tbl.FormName.Contains("frmSalaryCalculation")
                              select tbl).Count();

                if (Access > 0)
                {
                    if (!Page.IsPostBack)
                    {
                        MultiView1.SetActiveView(View1);
                        BindAllownce();
                        BindEmployee();
                        BindDeduction();
                        BindSalaryAllocation();
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
            try
            {
                List<EntityEmployee> lst = mobjSalBLL.GetEmployee();
                lst.Insert(0, new EntityEmployee { PKId = 0, EmpName = "--Select--" });
                ddlEmployee.DataSource = lst;
                ddlEmployee.DataValueField = "PKId";
                ddlEmployee.DataTextField = "EmpName";
                ddlEmployee.DataBind();
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                Session["MyFlag"] = "Edit";
                ImageButton imgEdit = (ImageButton)sender;
                GridViewRow row = (GridViewRow)imgEdit.NamingContainer;
                Session["LedgerId"] = Convert.ToInt32(dgvSalary.DataKeys[row.RowIndex].Value);
                tblSalary sal = new tblSalary();
                EntitySalary lst = mobjSalBLL.GetDetail(Convert.ToInt32(Session["LedgerId"]));
                if (lst.IsPaymentDone == true)
                {
                    lblMessage.Text = "Payment is Done.You Cannot Edit Details....";
                    return;
                }
                else
                {
                    ListItem item = ddlEmployee.Items.FindByText(Convert.ToString(row.Cells[1].Text));
                    ddlEmployee.SelectedValue = item.Value;
                    ddlEmployee_SelectedIndexChanged(sender, e);
                    txtDos.Text = string.Format("{0:dd/MM/yyyy}", row.Cells[2].Text).ToString();
                    txtMonth.Text = string.Format("{0:MMM/yyyy}", row.Cells[3].Text).ToString();
                    txtDays.Text = Convert.ToString(row.Cells[4].Text);
                    txtAttendDays.Text = Convert.ToString(row.Cells[5].Text);
                    txtLeavesTaken.Text = Convert.ToString(row.Cells[6].Text);
                    txtOTHours.Text = Convert.ToString(row.Cells[7].Text);
                    txtNetAmount.Text = Convert.ToString(row.Cells[8].Text);
                    ddlEmployee.Enabled = false;
                    lblbaseSal.Enabled = false;
                    txtDoj.Enabled = false;
                    txtDos.Enabled = false;
                    //calDos.Enabled = false;
                    txtMonth.Enabled = false;
                    txtDays.Enabled = false;
                    txtAttendDays.Enabled = false;
                    txtLeavesTaken.Enabled = true;
                    txtOTHours.Enabled = true;
                    BindSalary(Convert.ToInt32(Session["LedgerId"]));
                    BindSalaryDeduction(Convert.ToInt32(Session["LedgerId"]));
                    BtnSave.Visible = false;
                    btnUpdate.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
            MultiView1.SetActiveView(View2);
        }

        public void BindSalary(int Id)
        {
            try
            {
                List<EntityAllowanceDeduction> lst = mobjSalBLL.GetDeductionsForEdit(Id);
                DgvAllowance.DataSource = lst;
                DgvAllowance.DataBind();
                Session["FromEdit"] = lst;
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        private void BindSalaryDeduction(int salId)
        {
            try
            {
                List<EntityAllowanceDeduction> lst = mobjSalBLL.GetAllowancesForEdit(salId);
                dgvDeduction.DataSource = lst;
                dgvDeduction.DataBind();
                Session["FromEditDeduction"] = lst;
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void BtnAddNewEmp_Click(object sender, EventArgs e)
        {
            try
            {
                Session["MyFlag"] = "ADD";
                btnUpdate.Visible = false;
                BtnSave.Visible = true;
                txtDoj.Enabled = false;
                txtAttendDays.Enabled = false;
                Clear();
                txtLeavesTaken.Text = "0";
                txtOTHours.Text = "0";
                ddlEmployee.Enabled = true;
                txtDos.Enabled = true;
                txtMonth.Enabled = true;
                txtNetAmount.Enabled = true;
                txtLeavesTaken.Enabled = true;
                txtDays.Enabled = true;
                //calDos.Enabled = true;
                List<EntityAllowanceDeduction> lstDeduct = new List<EntityAllowanceDeduction>();
                Session["Deduction"] = lstDeduct;
                lstDeduct = new List<EntityAllowanceDeduction>();
                Session["allowance"] = lstDeduct;
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
            MultiView1.SetActiveView(View2);
        }

        public void Clear()
        {
            lblbaseSal.Text = string.Empty;
            txtDos.Text = DateTime.Now.ToString("MM/dd/yyyy");
            txtDoj.Text = DateTime.Now.ToString("MM/dd/yyyy");
            txtLeavesTaken.Text = string.Empty;
            txtMonth.Text = DateTime.Now.ToString("MMM/yyyy");
            txtOTHours.Text = string.Empty;
            txtNetAmount.Text = string.Empty;
            ddlEmployee.SelectedIndex = 0;
            txtDays.Text = string.Empty;
            txtAttendDays.Text = string.Empty;

        }
        public void BindAllownce()
        {
            try
            {
                List<EntityAllowanceDeduction> lst = mobjSalBLL.GetAllowanceName();
                lst.Insert(0, new EntityAllowanceDeduction { AllowDedId = 0, Description = "--Select--" });
                ddlAllowance.DataValueField = "AllowDedId";
                ddlAllowance.DataTextField = "Description";
                ddlAllowance.DataSource = lst;
                ddlAllowance.DataBind();
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        public void BindDeduction()
        {
            try
            {
                List<EntityAllowanceDeduction> lst = mobjSalBLL.GetDeductionName();
                lst.Insert(0, new EntityAllowanceDeduction { AllowDedId = 0, Description = "--Select--" });
                ddlDeduction.DataValueField = "AllowDedId";
                ddlDeduction.DataTextField = "Description";
                ddlDeduction.DataSource = lst;
                ddlDeduction.DataBind();
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void BtnClose_Click(object sender, EventArgs e)
        {
            Clear();
            MultiView1.SetActiveView(View1);
        }

        protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlEmployee.SelectedIndex > 0)
                {
                    List<EntityEmployee> lst = mobjSalBLL.GetDetails(Convert.ToInt32(ddlEmployee.SelectedValue));
                    foreach (EntityEmployee item in lst)
                    {
                        lblbaseSal.Text = string.Format("{0:0.00}", item.BasicSal);
                        txtDoj.Text = Convert.ToString(string.Format("{0:dd/MM/yyyy}", item.EmpDOJ));
                        txtNetAmount.Text = Convert.ToString("0");
                    }
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void btnAllowCancel_Click(object sender, EventArgs e)
        {
            ddlAllowance.SelectedIndex = 0;
            txtAllowAmount.Text = string.Empty;
            btnAllowAdd.Enabled = true;
        }

        protected void btnDedCancel_Click(object sender, EventArgs e)
        {
            ddlDeduction.SelectedIndex = 0;
            txtDedAmount.Text = string.Empty;
            btnDedAdd.Enabled = true;
        }

        protected void ddlAllowance_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlAllowance.SelectedIndex > 0)
                {
                    EntityAllowanceDeduction lst = mobjSalBLL.GetAllowances(Convert.ToInt32(ddlAllowance.SelectedValue));
                    if (lst.IsPercentage == true)
                    {
                        txtAllowAmount.ReadOnly = true;
                        txtAllowAmount.Enabled = false;
                        txtAllowAmount.Text = Convert.ToString(decimal.Round(Convert.ToDecimal(Convert.ToDecimal(lblbaseSal.Text) * lst.Percentage / 100), 2));
                    }
                    else
                    {
                        if (lst.IsFlexible == true)
                        {
                            if (lst.IsBasic != null)
                            {
                                if (lst.IsBasic.Value)
                                {
                                    txtAllowAmount.Text = Convert.ToString(decimal.Round(Convert.ToDecimal(lblbaseSal.Text), 2));
                                    txtAllowAmount.Enabled = false;
                                    txtAllowAmount.ReadOnly = false;
                                }
                                else
                                {
                                    txtAllowAmount.Enabled = true;
                                }
                            }
                            else
                            {
                                txtAllowAmount.Enabled = true;
                            }
                        }
                        else
                        {
                            if (lst.IsBasic != null)
                            {
                                if (lst.IsBasic.Value)
                                {
                                    txtAllowAmount.Text = Convert.ToString(decimal.Round(Convert.ToDecimal(lblbaseSal.Text), 2));
                                    txtAllowAmount.Enabled = false;
                                    txtAllowAmount.ReadOnly = false;
                                }
                                else
                                {
                                    if (lst.IsFlexible)
                                    {
                                        txtAllowAmount.Enabled = true;
                                    }
                                    else
                                    {
                                        txtAllowAmount.Text = Convert.ToString(decimal.Round(lst.Amount, 2));
                                        txtAllowAmount.Enabled = false;
                                    }
                                }
                            }
                            else
                            {
                                txtAllowAmount.Enabled = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void ddlDeduction_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                EntityAllowanceDeduction lst = mobjSalBLL.GetDeduction(Convert.ToInt32(ddlDeduction.SelectedValue));
                if (lst.IsPercentage == true)
                {
                    txtDedAmount.Text = Convert.ToString((Convert.ToDecimal(lblbaseSal.Text) * lst.Percentage) / 100);
                }
                else
                {
                    if (lst.IsFlexible == true)
                    {
                        txtDedAmount.Enabled = true;
                    }
                    else
                    {
                        txtDedAmount.Text = Convert.ToString(lst.Amount);
                    }
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSearch.Text))
                {
                    SelectSalaryDetails(txtSearch.Text);
                }
                else
                {
                    lblMessage.Text = "Please fill search text.";
                    txtSearch.Focus();
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private void SelectSalaryDetails(string Prefix)
        {
            try
            {
                SalaryCalculationBLL Pathology = new SalaryCalculationBLL();
                List<EntitySalary> lst = Pathology.SearchSalary(Prefix);
                if (lst != null)
                {
                    dgvSalary.DataSource = lst;
                    dgvSalary.DataBind();

                    lblRowCount.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtSearch.Text = string.Empty;
            BindSalaryAllocation();
        }
        protected void btnDedDelete_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton imgDelete = (ImageButton)sender;
                GridViewRow row = (GridViewRow)imgDelete.NamingContainer;
                Session["Delete_Charge"] = Convert.ToInt32(row.Cells[1].Text);
                decimal amt = Convert.ToDecimal(row.Cells[3].Text);
                List<EntityAllowanceDeduction> lstfin = new List<EntityAllowanceDeduction>();
                if (Session["MyFlag"] == "ADD")
                {
                    List<EntityAllowanceDeduction> lstDed = (List<EntityAllowanceDeduction>)Session["Deduction"];
                    foreach (EntityAllowanceDeduction item in lstDed)
                    {
                        if (item.AllowDedId != Convert.ToInt32(row.Cells[1].Text))
                        {
                            lstfin.Add(item);
                        }
                    }
                    Session["Deduction"] = lstfin;
                    txtNetAmount.Text = Convert.ToString(Convert.ToDecimal(txtNetAmount.Text) + amt);
                    dgvDeduction.DataSource = Session["Deduction"];
                    dgvDeduction.DataBind();
                }
                if (Session["MyFlag"] == "Edit")
                {
                    List<EntityAllowanceDeduction> lstDed = (List<EntityAllowanceDeduction>)Session["FromEditDeduction"];
                    foreach (EntityAllowanceDeduction item in lstDed)
                    {
                        if (item.AllowDedId != Convert.ToInt32(row.Cells[1].Text))
                        {
                            item.IsDelete = false;
                        }
                        else
                        {
                            item.IsDelete = true;
                        }
                        lstfin.Add(item);
                    }
                    Session["FromEditDeduction"] = lstfin;
                    txtNetAmount.Text = Convert.ToString(Convert.ToDecimal(txtNetAmount.Text) + amt);
                }
                dgvDeduction.DataSource = lstfin.Where(p => p.IsDelete == false).ToList();
                dgvDeduction.DataBind();
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton imgDelete = (ImageButton)sender;
                GridViewRow row = (GridViewRow)imgDelete.NamingContainer;
                Session["Delete_Charge"] = Convert.ToInt32(row.Cells[1].Text);
                decimal amt = Convert.ToDecimal(row.Cells[3].Text);
                List<EntityAllowanceDeduction> lstFinal = new List<EntityAllowanceDeduction>();
                bool IsRemoveBasic = mobjSalBLL.CheckIsBasic(Convert.ToInt32(row.Cells[1].Text));
                if (IsRemoveBasic)
                {
                    lblMsg.Text = "You are not allowed to remove Basic.";
                    return;
                }
                if (Session["MyFlag"] == "ADD")
                {
                    List<EntityAllowanceDeduction> lst = (List<EntityAllowanceDeduction>)Session["allowance"];
                    foreach (EntityAllowanceDeduction item in lst)
                    {
                        if (item.AllowDedId != Convert.ToInt32(row.Cells[1].Text))
                        {
                            lstFinal.Add(item);
                        }

                    }
                    Session["allowance"] = lstFinal;
                    txtNetAmount.Text = Convert.ToString(Convert.ToDecimal(txtNetAmount.Text) - amt);
                    DgvAllowance.DataSource = Session["allowance"];
                    DgvAllowance.DataBind();
                }
                if (Session["MyFlag"] == "Edit")
                {
                    List<EntityAllowanceDeduction> lst = (List<EntityAllowanceDeduction>)Session["FromEdit"];
                    if (IsRemoveBasic)
                    {
                        lblMsg.Text = "You are not allowed to remove Basic.";
                    }
                    foreach (EntityAllowanceDeduction item in lst)
                    {
                        if (item.AllowDedId != Convert.ToInt32(row.Cells[1].Text))
                        {
                            item.IsDelete = false;
                        }
                        else
                        {
                            item.IsDelete = true;
                        }

                        lstFinal.Add(item);
                    }
                    Session["FromEdit"] = lstFinal;
                    txtNetAmount.Text = Convert.ToString(Convert.ToDecimal(txtNetAmount.Text) - amt);
                }
                DgvAllowance.DataSource = lstFinal.Where(p => p.IsDelete == false).ToList();
                DgvAllowance.DataBind();
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void btnAllowAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlAllowance.SelectedIndex > 0)
                {
                    List<EntityAllowanceDeduction> lst = null;
                    //DgvAllowance.DataSource = null;
                    //DgvAllowance.DataBind();
                    //dgvDeduction.DataSource = null;
                    //dgvDeduction.DataBind();
                    if (Convert.ToString(Session["MyFlag"]).Equals("ADD", StringComparison.CurrentCultureIgnoreCase))
                    {
                        lst = (List<EntityAllowanceDeduction>)Session["allowance"];
                    }
                    else
                    {
                        lst = (List<EntityAllowanceDeduction>)Session["FromEdit"];
                    }
                    if (lst != null)
                    {
                        int cnt = (from tbl in lst
                                   where tbl.AllowDedId == Convert.ToInt32(ddlAllowance.SelectedValue)
                                   select tbl).ToList().Count;

                        if (cnt > 0)
                        {
                            lblMsg.Text = "This Allowance is Already Added";
                        }
                        else
                        {
                            lst.Add(new EntityAllowanceDeduction() { Description = Convert.ToString(ddlAllowance.SelectedItem.Text), Amount = Convert.ToDecimal(txtAllowAmount.Text), AllowDedId = Convert.ToInt32(ddlAllowance.SelectedValue), SalDetail_Id = 0 });
                            if (Convert.ToString(Session["MyFlag"]).Equals("ADD", StringComparison.CurrentCultureIgnoreCase))
                            {
                                Session["allowance"] = lst;
                            }
                            else
                            {
                                Session["FromEdit"] = lst;
                            }
                            DgvAllowance.DataSource = lst;
                            DgvAllowance.DataBind();
                            txtNetAmount.Text = Convert.ToString(Convert.ToDecimal(txtNetAmount.Text) + Convert.ToDecimal(txtAllowAmount.Text));
                            ddlAllowance.SelectedIndex = 0;
                            txtAllowAmount.Text = string.Empty;
                        }
                    }
                    else
                    {
                        lst.Add(new EntityAllowanceDeduction() { Description = Convert.ToString(ddlAllowance.SelectedItem.Text), Amount = Convert.ToDecimal(txtAllowAmount.Text), AllowDedId = Convert.ToInt32(ddlAllowance.SelectedValue), SalDetail_Id = 0 });
                        if (Convert.ToString(Session["MyFlag"]).Equals("ADD", StringComparison.CurrentCultureIgnoreCase))
                        {
                            Session["allowance"] = lst;
                        }
                        else
                        {
                            Session["FromEdit"] = lst;
                        }
                        DgvAllowance.DataSource = lst;
                        DgvAllowance.DataBind();
                        txtNetAmount.Text = Convert.ToString(Convert.ToDecimal(txtNetAmount.Text) + Convert.ToDecimal(txtAllowAmount.Text));
                        ddlAllowance.SelectedIndex = 0;
                        txtAllowAmount.Text = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }
        protected void btnDedAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlDeduction.SelectedIndex > 0)
                {
                    List<EntityAllowanceDeduction> lst = null;
                    //DgvAllowance.DataSource = null;
                    //DgvAllowance.DataBind();
                    //dgvDeduction.DataSource = null;
                    //dgvDeduction.DataBind();
                    if (Convert.ToString(Session["MyFlag"]).Equals("ADD", StringComparison.CurrentCultureIgnoreCase))
                    {
                        lst = (List<EntityAllowanceDeduction>)Session["Deduction"];
                    }
                    else
                    {
                        lst = (List<EntityAllowanceDeduction>)Session["FromEditDeduction"];
                    }
                    if (lst.Count > 0)
                    {
                        int cnt = (from tbl in lst
                                   where tbl.AllowDedId == Convert.ToInt32(ddlDeduction.SelectedValue)
                                   select tbl).ToList().Count;

                        if (cnt > 0)
                        {
                            lblMsg.Text = "This Deduction is Already Added";
                        }
                        else
                        {
                            lst.Add(new EntityAllowanceDeduction() { Description = Convert.ToString(ddlDeduction.SelectedItem.Text), Amount = Convert.ToDecimal(txtDedAmount.Text), AllowDedId = Convert.ToInt32(ddlDeduction.SelectedValue) });
                            if (Convert.ToString(Session["MyFlag"]).Equals("ADD", StringComparison.CurrentCultureIgnoreCase))
                            {
                                Session["Deduction"] = lst;
                            }
                            else
                            {
                                Session["FromEditDeduction"] = lst;
                            }
                            dgvDeduction.DataSource = lst;
                            dgvDeduction.DataBind();
                            txtNetAmount.Text = Convert.ToString(Convert.ToDecimal(txtNetAmount.Text) - Convert.ToDecimal(txtDedAmount.Text));
                            ddlDeduction.SelectedIndex = 0;
                            txtDedAmount.Text = string.Empty;
                        }
                    }
                    else
                    {
                        lst.Add(new EntityAllowanceDeduction() { Description = Convert.ToString(ddlDeduction.SelectedItem.Text), Amount = Convert.ToDecimal(txtDedAmount.Text), AllowDedId = Convert.ToInt32(ddlDeduction.SelectedValue) });
                        if (Convert.ToString(Session["MyFlag"]).Equals("ADD", StringComparison.CurrentCultureIgnoreCase))
                        {
                            Session["Deduction"] = lst;
                        }
                        else
                        {
                            Session["FromEditDeduction"] = lst;
                        }
                        dgvDeduction.DataSource = lst;
                        dgvDeduction.DataBind();
                        txtNetAmount.Text = Convert.ToString(Convert.ToDecimal(txtNetAmount.Text) - Convert.ToDecimal(txtDedAmount.Text));
                        ddlDeduction.SelectedIndex = 0;
                        txtDedAmount.Text = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }
        protected void txtLeavesTaken_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtDays.Text) && !string.IsNullOrEmpty(txtLeavesTaken.Text))
                    txtAttendDays.Text = (Convert.ToInt32(txtDays.Text) - Convert.ToInt32(txtLeavesTaken.Text)).ToString();
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        public void BindSalaryAllocation()
        {
            try
            {
                List<STP_Select_SalaryResult> ldtShift = mobjSalBLL.GetSalary();
                if (ldtShift.Count > 0)
                {
                    Session["SalaryDetails"] = ldtShift;
                    dgvSalary.DataSource = ldtShift;
                    dgvSalary.DataBind();
                    int lintRowcount = ldtShift.Count;
                    lblRowCount.Text = "<b>Total Records:</b> " + lintRowcount.ToString();
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }

        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            ImageButton imgEdit = (ImageButton)sender;
            GridViewRow row = (GridViewRow)imgEdit.NamingContainer;
            Session["LedgerId"] = Convert.ToInt32(dgvSalary.DataKeys[row.RowIndex].Value);
            Session["ReportType"] = "Salary";
            Response.Redirect("~/PathalogyReport/PathologyReport.aspx", false);
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                bool flag = false;
                foreach (GridViewRow item in DgvAllowance.Rows)
                {
                    bool IsBasic = mobjSalBLL.CheckIsBasic(Convert.ToInt32(item.Cells[1].Text));
                    if (IsBasic)
                    {
                        flag = true;
                        break;
                    }
                    else
                    {
                        flag = false;
                    }
                }
                if (!flag)
                {
                    lblMsg.Text = "You shuould be add Basic in Salary";
                    return;
                }
                tblSalary sal = new tblSalary();
                EntitySalaryDetails obj = new EntitySalaryDetails();
                EntitySalary salobj = new EntitySalary();
                sal.EmpId = Convert.ToInt32(ddlEmployee.SelectedValue);
                sal.SalDate = Convert.ToDateTime(txtDos.Text);
                sal.Sal_Month = string.Format("{0:MMM-yyyy}", txtMonth.Text);
                sal.No_of_Days = Convert.ToInt32(txtDays.Text);
                sal.Attend_Days = Convert.ToInt32(txtAttendDays.Text);
                sal.LeavesTaken = Convert.ToInt32(txtLeavesTaken.Text);
                sal.OTHours = Convert.ToInt32(txtOTHours.Text);
                decimal day = Convert.ToDecimal(txtNetAmount.Text) / Convert.ToDecimal(txtDays.Text);
                decimal month = day * Convert.ToDecimal(txtAttendDays.Text);
                decimal hour = day / 8;
                decimal tothour = Convert.ToInt32(txtOTHours.Text) * decimal.Round(hour, 2);
                decimal payment = Convert.ToDecimal(month + tothour);
                txtNetAmount.Text = Convert.ToString(payment);
                sal.NetPayment = decimal.Round(payment, 0);

                if (chkIspayment.Checked == true)
                {
                    sal.IsPaymentDone = true;
                }
                else
                {
                    sal.IsPaymentDone = false;
                }
                tblEmployee objFac = mobjSalBLL.GetEmployee(Convert.ToInt32(ddlEmployee.SelectedValue));
                if (objFac != null)
                {
                    tblSalary objExist = mobjSalBLL.CheckExistRecord(objFac.PKId, Convert.ToString(txtMonth.Text));
                    if (objExist == null)
                    {
                        if (mobjSalBLL.ValidateAllocation(sal))
                        {
                            List<tblSalaryDetail> lst = new List<tblSalaryDetail>();
                            foreach (GridViewRow item in DgvAllowance.Rows)
                            {
                                lst.Add(new tblSalaryDetail { SalDate = Convert.ToDateTime(txtDos.Text), AllowanceDeduction_Id = Convert.ToInt32(item.Cells[1].Text), IsDelete = false, Amount = Convert.ToDecimal(item.Cells[3].Text) });
                            }
                            foreach (GridViewRow item in dgvDeduction.Rows)
                            {
                                lst.Add(new tblSalaryDetail { SalDate = Convert.ToDateTime(txtDos.Text), AllowanceDeduction_Id = Convert.ToInt32(item.Cells[1].Text), IsDelete = false, Amount = Convert.ToDecimal(item.Cells[3].Text) });
                            }
                            int SalId = Convert.ToInt32(mobjSalBLL.Save(sal, lst));
                            lblMessage.Text = "Record Saved Successfully.....";
                            Clear();

                        }
                    }
                    else
                    {
                        Clear();
                        List<EntityAllowanceDeduction> lst = new List<EntityAllowanceDeduction>();
                        List<EntityAllowanceDeduction> lstded = new List<EntityAllowanceDeduction>();
                        DgvAllowance.DataSource = lst;
                        DgvAllowance.DataBind();
                        dgvDeduction.DataSource = lstded;
                        dgvDeduction.DataBind();
                        lblMsg.Text = string.Empty;
                        MultiView1.SetActiveView(View1);
                        lblMessage.Text = "Salary Is Already Allocated To Employee!!";

                    }
                }
                else
                {
                    lblMsg.Text = "Invalid Employee";
                }
                Session["allowance"] = new List<EntityAllowanceDeduction>();
                Session["Deduction"] = new List<EntityAllowanceDeduction>();
                BindSalaryAllocation();
                MultiView1.SetActiveView(View1);
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                decimal totalAllowances = 0;
                decimal totalDed = 0;
                tblEmployee objFac = mobjSalBLL.GetEmployee(Convert.ToInt32(ddlEmployee.SelectedValue));
                if (objFac != null)
                {
                    tblSalary obj = new tblSalary();
                    if (chkIspayment.Checked == true)
                    {
                        obj.IsPaymentDone = true;
                    }
                    else
                    {
                        obj.IsPaymentDone = false;
                    }
                    if (mobjSalBLL.ValidateAllocation(obj, Convert.ToInt32(Session[" LedgerId"])))
                    {
                        tblSalary sal = new tblSalary();
                        List<tblSalaryDetail> lst = new List<tblSalaryDetail>();
                        foreach (GridViewRow item in DgvAllowance.Rows)
                        {
                            lst.Add(new tblSalaryDetail { SalDetail_Id = Convert.ToInt32(item.Cells[0].Text), SalDate = Convert.ToDateTime(txtDos.Text), SalId = Convert.ToInt32(Session["LedgerId"]), AllowanceDeduction_Id = Convert.ToInt32(item.Cells[1].Text), IsDelete = false, Amount = Convert.ToDecimal(item.Cells[3].Text) });
                            totalAllowances += Convert.ToDecimal(item.Cells[3].Text);
                        }
                        foreach (GridViewRow item in dgvDeduction.Rows)
                        {
                            lst.Add(new tblSalaryDetail { SalDetail_Id = Convert.ToInt32(item.Cells[0].Text), SalDate = Convert.ToDateTime(txtDos.Text), SalId = Convert.ToInt32(Session["LedgerId"]), AllowanceDeduction_Id = Convert.ToInt32(item.Cells[1].Text), IsDelete = false, Amount = Convert.ToDecimal(item.Cells[3].Text) });
                            totalDed += Convert.ToDecimal(item.Cells[3].Text);
                        }
                        //List<tblSalaryDetail> lstDeletedAllDed = new List<tblSalaryDetail>();
                        List<EntityAllowanceDeduction> lstDel = (List<EntityAllowanceDeduction>)Session["FromEdit"];
                        List<EntityAllowanceDeduction> lstDelDed = (List<EntityAllowanceDeduction>)Session["FromEditDeduction"];
                        foreach (EntityAllowanceDeduction item in lstDel)
                        {
                            if (item.IsDelete)
                            {
                                lst.Add(new tblSalaryDetail() { AllowanceDeduction_Id = item.AllowDedId, SalId = Convert.ToInt32(Session["LedgerId"]), IsDelete = item.IsDelete, Amount = item.Amount });
                            }
                        }
                        foreach (EntityAllowanceDeduction item in lstDelDed)
                        {
                            if (item.IsDelete)
                            {
                                lst.Add(new tblSalaryDetail() { AllowanceDeduction_Id = item.AllowDedId, SalId = Convert.ToInt32(Session["LedgerId"]), IsDelete = item.IsDelete, Amount = item.Amount });
                            }
                        }

                        decimal netPay = totalAllowances - totalDed;
                        decimal dayPayment = netPay / Convert.ToInt32(txtDays.Text);
                        decimal hours = dayPayment / 8;
                        decimal Payment = Convert.ToInt32(txtOTHours.Text) * decimal.Round(hours, 2);
                        decimal monthpayment = dayPayment * Convert.ToDecimal(txtAttendDays.Text);
                        decimal NetPayment = monthpayment + Payment;
                        obj.IsPaymentDone = chkIspayment.Checked;
                        obj.SalId = Convert.ToInt32(Session["LedgerId"]);
                        obj.LeavesTaken = Convert.ToInt32(txtLeavesTaken.Text);
                        obj.OTHours = Convert.ToInt32(txtOTHours.Text);
                        obj.Attend_Days = Convert.ToInt32(txtAttendDays.Text);
                        obj.NetPayment = decimal.Round(NetPayment, 0);
                        mobjSalBLL.Update(obj, lst);
                        lblMessage.Text = "Record Updated successfully.";
                        List<EntityAllowanceDeduction> lstallow = new List<EntityAllowanceDeduction>();
                        List<EntityAllowanceDeduction> lstded = new List<EntityAllowanceDeduction>();
                        DgvAllowance.DataSource = lstallow;
                        DgvAllowance.DataBind();
                        dgvDeduction.DataSource = lstded;
                        dgvDeduction.DataBind();
                        BindSalaryAllocation();
                        Session["FromEditDeduction"] = null;
                        Session["FromEdit"] = null;
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
            MultiView1.SetActiveView(View1);
        }

        protected void dgvSalary_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dgvSalary.DataSource = (List<STP_Select_SalaryResult>)Session["SalaryDetails"];
                dgvSalary.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
        protected void dgvSalary_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvSalary.PageIndex = e.NewPageIndex;
        }
        protected void dgvSalary_DataBound(object sender, EventArgs e)
        {
            int lintCurrentPage = dgvSalary.PageIndex + 1;
            lblPageCount.Text = "<b>Page</b> " + lintCurrentPage.ToString() + "<b> of </b>" + dgvSalary.PageCount.ToString();
        }
    }
}