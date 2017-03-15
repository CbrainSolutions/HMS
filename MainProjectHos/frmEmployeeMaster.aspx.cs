using MainProjectHos.Models.BusinessLayer;
using MainProjectHos.Models.DataLayer;
using MainProjectHos.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MainProjectHos
{
    public partial class frmEmployeeMaster : System.Web.UI.Page
    {
        EmployeeBLL mobjEmpBLL = new EmployeeBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] != null)
            {
                EntityLogin entLogin = (EntityLogin)Session["user"];
                List<EntityFormMaster> entFormAcc = (List<EntityFormMaster>)Session["AccessibleForm"];
                int Access = (from tbl in entFormAcc
                              where tbl.FormName.Contains("frmEmployeeMaster")
                              select tbl).Count();

                if (Access > 0)
                {
                    if (!Page.IsPostBack)
                    {
                        GetEmployee();
                        BindDesignation();
                        GetDepartments();
                        //GetDepartmentsForEdit();
                        //CalendarExtender1.EndDate = DateTime.Now;
                        //CalendarExtender1.StartDate = DateTime.Now.AddYears(-60);
                        //CalDOBDate.StartDate = DateTime.Now.Date.AddYears(-60);
                        //CalDOBDate.EndDate = DateTime.Now.Date;
                        btnUpdate.Visible = false;
                        BtnSave.Visible = true;
                        MultiView1.SetActiveView(View1);
                    }
                }
                else
                {
                    Response.Redirect("frmAdminMain.aspx", false);
                }
            }
            else
            {
                Response.Redirect("frmlogin.aspx", false);
            }
        }



        private void BindDesignation()
        {
            try
            {
                DataTable tblDesig = new DesignationBLL().GetAllDesignations();
                DataRow dr = tblDesig.NewRow();
                dr["PKId"] = 0;
                dr["DesignationDesc"] = "---Select---";
                tblDesig.Rows.InsertAt(dr, 0);

                ddlDesignation.DataSource = tblDesig;
                ddlDesignation.DataValueField = "PKId";
                ddlDesignation.DataTextField = "DesignationDesc";
                ddlDesignation.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
        protected void BtnAddNewEmp_Click(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;
            DataTable ldt = new DataTable();
            txtAddress.Text = string.Empty;
            txtFirstName.Text = string.Empty;
            txtMidleName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtEducation.Text = string.Empty;
            txtRegistrationNo.Text = string.Empty;
            txtJoinDate.Text = string.Empty;
            txtDOBDate.Text = string.Empty;
            ddlDepartment.SelectedIndex = 0;
            ldt = mobjEmpBLL.GetNewEmpCode();
            txtEmpCode.Text = ldt.Rows[0].ItemArray[0].ToString();
            btnUpdate.Visible = false;
            BtnSave.Visible = true;
            txtBank.Text = string.Empty;
            txtBankAc.Text = string.Empty;
            txtPF.Text = string.Empty;
            txtPan.Text = string.Empty;
            txtBaseSal.Text = string.Empty;
            ddlDesignation.SelectedIndex = 0;
            MultiView1.SetActiveView(View2);
        }

        protected void dgvEmployee_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dgvEmployee.DataSource = (List<EntityEmployee>)Session["EmployeeDetail"];
                dgvEmployee.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
        protected void dgvEmployee_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvEmployee.PageIndex = e.NewPageIndex;
        }

        public void GetDepartments()
        {
            DataTable ldt = new DataTable();
            ldt = mobjEmpBLL.GetDepartments();

            DataRow dr = ldt.NewRow();
            dr["PKId"] = 0;
            dr["DeptDesc"] = "---Select---";
            ldt.Rows.InsertAt(dr, 0);

            ddlDepartment.DataSource = ldt;
            ddlDepartment.DataValueField = "PKId";
            ddlDepartment.DataTextField = "DeptDesc";
            ddlDepartment.DataBind();

        }


        protected void dgvEmployee_DataBound(object sender, EventArgs e)
        {
            int lintCurrentPage = dgvEmployee.PageIndex + 1;
            lblPageCount.Text = "<b>Page</b> " + lintCurrentPage.ToString() + "<b> of </b>" + dgvEmployee.PageCount.ToString();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton imgEdit = (ImageButton)sender;
                GridViewRow cnt = (GridViewRow)imgEdit.NamingContainer;
                string id = Convert.ToString(cnt.Cells[0].Text);
                DataTable tblEmp = new EmployeeBLL().SelectEmployeeForEdit(id);
                if (tblEmp.Rows.Count > 0)
                {
                    txtEmpCode.Text = Convert.ToString(tblEmp.Rows[0]["EmpCode"]);
                    txtFirstName.Text = Convert.ToString(tblEmp.Rows[0]["EmpFirstName"]);
                    txtMidleName.Text = Convert.ToString(tblEmp.Rows[0]["EmpMiddleName"]);
                    txtLastName.Text = Convert.ToString(tblEmp.Rows[0]["EmpLastName"]);
                    txtEducation.Text = Convert.ToString(tblEmp.Rows[0]["Education"]);
                    txtRegistrationNo.Text = Convert.ToString(tblEmp.Rows[0]["RegNo"]);
                    ListItem item = ddlDepartment.Items.FindByText(Convert.ToString(tblEmp.Rows[0]["DeptDesc"]));
                    ddlDepartment.SelectedValue = item.Value;
                    ListItem Desig = ddlDesignation.Items.FindByText(Convert.ToString(tblEmp.Rows[0]["DesignationDesc"]));
                    ddlDesignation.SelectedValue = Desig.Value;
                    txtDOBDate.Text = Convert.ToString(tblEmp.Rows[0]["EmpDOB"]);
                    txtJoinDate.Text = Convert.ToString(tblEmp.Rows[0]["EmpDOJ"]);
                    txtAddress.Text = Convert.ToString(tblEmp.Rows[0]["EmpAddress"]);
                    txtBank.Text = Convert.ToString(tblEmp.Rows[0]["BankName"]);
                    txtBankAc.Text = Convert.ToString(tblEmp.Rows[0]["BankACNo"]);
                    txtPF.Text = Convert.ToString(tblEmp.Rows[0]["PFNo"]);
                    txtPan.Text = Convert.ToString(tblEmp.Rows[0]["PanNo"]);
                    txtBaseSal.Text = Convert.ToString(tblEmp.Rows[0]["BasicSalary"]);
                    txtEmpCode.ReadOnly = true;
                    btnUpdate.Visible = true;
                    BtnSave.Visible = false;
                    MultiView1.SetActiveView(View2);
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }

        }

        private void GetEmployee()
        {
            //DataTable ldtEmp = new DataTable();
            List<EntityEmployee> ldtEmp = mobjEmpBLL.SelectAllEmployee();
            if (ldtEmp != null)
            {
                dgvEmployee.DataSource = ldtEmp;
                dgvEmployee.DataBind();
                Session["EmployeeDetail"] = ldtEmp;
                int lintRowcount = ldtEmp.Count;
                lblRowCount.Text = "<b>Total Records:</b> " + lintRowcount.ToString();
            }
        }
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            int cnt = 0;
            try
            {
                EntityEmployee entEmployee = new EntityEmployee();
                entEmployee.EmpCode = txtEmpCode.Text;
                entEmployee.EmpFirstName = FirstCharToUpper(txtFirstName.Text);
                entEmployee.EmpMiddleName = FirstCharToUpper(txtMidleName.Text);
                entEmployee.EmpLastName = FirstCharToUpper(txtLastName.Text);
                entEmployee.Education = txtEducation.Text.Trim();
                entEmployee.RegistrationNo = txtRegistrationNo.Text.Trim();
                entEmployee.DesignationId = Convert.ToInt32(ddlDesignation.SelectedValue);
                if (txtDOBDate.Text == string.Empty)
                {
                    entEmployee.EmpDOB = System.DateTime.Today.Date;
                }
                else
                {
                    entEmployee.EmpDOB = Commons.ConvertToDate(txtDOBDate.Text);
                }
                if (txtJoinDate.Text == string.Empty)
                {
                    entEmployee.EmpDOJ = System.DateTime.Today.Date;
                }
                else
                {
                    entEmployee.EmpDOJ = Commons.ConvertToDate(txtJoinDate.Text);
                }
                if (entEmployee.EmpDOB >= entEmployee.EmpDOJ)
                {
                    lblMsg.Text = "Birth Date should be older than Joining Date...";
                    return;
                }

                if (entEmployee.EmpDOJ.Year - entEmployee.EmpDOB.Year < 18)
                {
                    lblMsg.Text = "Employee Cannot Be Less Then 18 Age";
                }
                else
                {
                    entEmployee.EmpAddress = FirstCharToUpper(txtAddress.Text);
                    entEmployee.BankName = FirstCharToUpper(txtBank.Text);
                    entEmployee.BankACNo = txtBankAc.Text.Trim();
                    entEmployee.PFNo = txtPF.Text;
                    entEmployee.PanNo = txtPan.Text;
                    entEmployee.BasicSal = Convert.ToDecimal(txtBaseSal.Text);
                    entEmployee.UserType = Convert.ToString(ddlDepartment.SelectedItem.Text);
                    entEmployee.DeptId = Convert.ToInt32(ddlDepartment.SelectedValue);
                    entEmployee.EntryBy = SessionWrapper.UserName;

                    cnt = mobjEmpBLL.InsertEmployee(entEmployee);
                    if (cnt > 0)
                    {
                        lblMessage.Text = "Record saved successfully.";
                        GetEmployee();
                        MultiView1.SetActiveView(View1);
                    }
                    else
                    {
                        lblMessage.Text = "Record Not Inserted";
                    }
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            int cnt = 0;
            List<EntityEmployee> lstentEmployee = new List<EntityEmployee>();
            try
            {
                EntityEmployee entEmployee = new EntityEmployee();
                entEmployee.EmpCode = txtEmpCode.Text;
                entEmployee.EmpFirstName = FirstCharToUpper(txtFirstName.Text);
                entEmployee.EmpMiddleName = FirstCharToUpper(txtMidleName.Text);
                entEmployee.EmpLastName = FirstCharToUpper(txtLastName.Text);
                entEmployee.Education = txtEducation.Text.Trim();
                entEmployee.RegistrationNo = txtRegistrationNo.Text.Trim();
                if (txtDOBDate.Text == string.Empty)
                {
                    entEmployee.EmpDOB = System.DateTime.Today.Date;
                }
                else
                {
                    entEmployee.EmpDOB = Convert.ToDateTime(txtDOBDate.Text.Trim()).Date;
                }

                if (txtJoinDate.Text == string.Empty)
                {
                    entEmployee.EmpDOJ = System.DateTime.Today.Date;
                }
                else
                {
                    entEmployee.EmpDOJ = Convert.ToDateTime(txtJoinDate.Text.Trim()).Date;
                }

                entEmployee.EmpAddress = FirstCharToUpper(txtAddress.Text);
                entEmployee.BankName = FirstCharToUpper(txtBank.Text);
                entEmployee.BankACNo = txtBankAc.Text.Trim();
                entEmployee.PFNo = txtPF.Text;
                entEmployee.PanNo = txtPan.Text;
                entEmployee.BasicSal = Convert.ToDecimal(txtBaseSal.Text);
                entEmployee.DeptId = Convert.ToInt32(ddlDepartment.SelectedValue);
                entEmployee.DesignationId = Convert.ToInt32(ddlDesignation.SelectedValue);
                entEmployee.ChangeBy = SessionWrapper.UserName;
                cnt = mobjEmpBLL.UpdateEmployee(entEmployee);

                if (cnt > 0)
                {
                    GetEmployee();
                    lblMessage.Text = "Record Updated Successfully.";
                }
                else
                {
                    lblMessage.Text = "Record Not Updated.";
                }
                MultiView1.SetActiveView(View1);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }


        protected void BtnClose_Click(object sender, EventArgs e)
        {
            txtAddress.Text = string.Empty;
            txtFirstName.Text = string.Empty;
            txtMidleName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtEducation.Text = string.Empty;
            txtRegistrationNo.Text = string.Empty;
            txtJoinDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtDOBDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            ddlDepartment.SelectedIndex = 0;
            txtBaseSal.Text = string.Empty;
            txtBank.Text = string.Empty;
            txtBankAc.Text = string.Empty;
            txtPan.Text = string.Empty;
            txtPF.Text = string.Empty;
            txtEmpCode.Text = string.Empty;
            GetEmployee();
            MultiView1.SetActiveView(View1);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                if (!string.IsNullOrEmpty(txtSearch.Text))
                {
                    SearchEmployeeDetails(txtSearch.Text);
                }
                else
                {
                    lblMessage.Text = "Please Fill Search Text.";
                    txtSearch.Focus();
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }

        }

        private void SearchEmployeeDetails(string Prefix)
        {
            List<EntityEmployee> lst = mobjEmpBLL.SelectEmployee(Prefix);
            if (lst != null)
            {
                dgvEmployee.DataSource = lst;
                dgvEmployee.DataBind();
            }
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;
            txtSearch.Text = string.Empty;
            GetEmployee();
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                Session["Details"] = Session["EmployeeDetail"];
                Response.Redirect("~/ExcelReport/MonthwiseSalExcel.aspx", false);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        public string FirstCharToUpper(string value)
        {
            char[] array = value.ToCharArray();
            // Handle the first letter in the string.
            if (array.Length >= 1)
            {
                if (char.IsLower(array[0]))
                {
                    array[0] = char.ToUpper(array[0]);
                }
            }
            // Scan through the letters, checking for spaces.
            // ... Uppercase the lowercase letters following spaces.
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i - 1] == ' ')
                {
                    if (char.IsLower(array[i]))
                    {
                        array[i] = char.ToUpper(array[i]);
                    }
                }
            }
            return new string(array);
        }
    }
}