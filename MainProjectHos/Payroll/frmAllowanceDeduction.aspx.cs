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
    public partial class frmAllowanceDeduction : System.Web.UI.Page
    {
        public string MyFlag { get; set; }
        AllowanceDeductionBLL objallow = new AllowanceDeductionBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] != null)
            {
                EntityLogin entLogin = (EntityLogin)Session["user"];

                List<EntityFormMaster> entFormAcc = (List<EntityFormMaster>)Session["AccessibleForm"];
                int Access = (from tbl in entFormAcc
                              where tbl.FormName.Contains("frmAllowanceDeduction")
                              select tbl).Count();

                if (Access > 0)
                {
                    if (!IsPostBack)
                    {
                        GetAllowanceDed();
                        RdAllowance.Checked = true;
                        lblDescription.Text = "Allowance Name";
                        lblAmount.Text = "Allowance Amount";
                        MultiView1.SetActiveView(View1);
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

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                MyFlag = "Save";
                EntityAllowanceDeduction AllowDed = new EntityAllowanceDeduction();
                if (RdAllowance.Checked == true)
                {
                    AllowDed.Description = txtDescription.Text.Trim().ToUpper();
                    AllowDed.IsFixed = RdFixed.Checked;
                    AllowDed.IsFlexible = RdFlexible.Checked;
                    AllowDed.IsPercentage = RdPercentage.Checked;
                    AllowDed.IsBasic = ChkIsbasic.Checked ? true : false;
                    if (RdPercentage.Checked == true)
                    {
                        int amt = Convert.ToInt32(txtAmount.Text);
                        if (amt >= 100 || amt <= 0)
                        {
                            lblMsg.Text = "Please Enter Correct Percentage Value ";
                            txtAmount.Focus();
                            return;
                        }
                        else
                        {
                            AllowDed.Percentage = Convert.ToInt32(txtAmount.Text);
                        }
                    }
                    else if (RdFlexible.Checked)
                    {
                        AllowDed.Amount = 0;
                    }
                    else if (ChkIsbasic.Checked)
                    {
                        AllowDed.Amount = 0;
                    }
                    else
                    {
                        AllowDed.Amount = Convert.ToDecimal(txtAmount.Text);
                    }
                    AllowDed.IsAllowance = true;
                    AllowDed.IsDeduction = false;

                }
                if (RdDeduction.Checked == true)
                {
                    AllowDed.IsBasic = false;
                    AllowDed.Description = txtDescription.Text.ToUpper().Trim();
                    AllowDed.IsFixed = RdFixed.Checked;
                    AllowDed.IsFlexible = RdFlexible.Checked;
                    AllowDed.IsPercentage = RdPercentage.Checked;
                    if (RdPercentage.Checked == true)
                    {
                        int check = Convert.ToInt32(txtAmount.Text);
                        if (check >= 100 || check <= 0)
                        {
                            lblMsg.Text = "Please Enter Correct Percentage Value";
                            txtAmount.Focus();
                            return;
                        }
                        else
                        {
                            AllowDed.Percentage = Convert.ToInt32(txtAmount.Text);
                        }
                    }
                    else if (RdFlexible.Checked)
                    {
                        AllowDed.Amount = 0;
                    }
                    else
                    {
                        AllowDed.Amount = Convert.ToDecimal(txtAmount.Text);
                    }
                    AllowDed.IsAllowance = false;
                    AllowDed.IsDeduction = true;
                }
                if (objallow.ValidateByName(AllowDed) == null)
                {
                    objallow.Save(AllowDed);
                    btnAddNew.Visible = true;
                    GetAllowanceDed();
                    Clear();
                    MultiView1.SetActiveView(View1);
                }
                else
                {
                    lblMsg.Text = "Allowance Or Deduction Name is Already Exist..";
                    txtDescription.Focus();
                    txtDescription.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }

        }
        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            //int lintcount = 0;
            try
            {
                EntityAllowanceDeduction ent = new EntityAllowanceDeduction();
                ent.AllowDedId = Convert.ToInt32(Session["AllowDedId"]);
                ent.Description = txtDescription.Text;
                ent.IsAllowance = RdAllowance.Checked;
                ent.IsDeduction = RdDeduction.Checked;
                ent.IsFixed = RdFixed.Checked;
                ent.IsFlexible = RdFlexible.Checked;
                ent.IsPercentage = RdPercentage.Checked;
                if (RdPercentage.Checked == true)
                {
                    int check = Convert.ToInt32(txtAmount.Text);
                    if (check >= 100 || check <= 0)
                    {
                        lblMsg.Text = "Please Enter Correct Percentage Value";
                        txtAmount.Focus();
                        return;
                    }
                    else
                    {
                        ent.Percentage = Convert.ToInt32(txtAmount.Text);
                    }
                }
                else if (RdFlexible.Checked)
                {
                    ent.Amount = 0;
                }
                else
                {
                    ent.Amount = Convert.ToDecimal(txtAmount.Text);
                }
                if (objallow.ValidateDescName(ent) != null)
                {
                    objallow.Update(ent);
                    lblMessage.Text = "Record Updated Successfully.";
                    GetAllowanceDed();
                }
                else
                {
                    if (objallow.ValidateByName(ent) == null)
                    {
                        objallow.Update(ent);
                        lblMessage.Text = "Record Updated Successfully.";
                        GetAllowanceDed();
                    }
                    else
                    {
                        lblMessage.Text = "Record Already Exist...";
                        txtDescription.Text = string.Empty;
                        txtDescription.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
            MultiView1.SetActiveView(View1);
        }

        protected void BtnClose_Click(object sender, EventArgs e)
        {
            Clear();
            MultiView1.SetActiveView(View1);
        }
        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                MyFlag = "Edit";
                ImageButton imgEdit = (ImageButton)sender;
                GridViewRow row = (GridViewRow)imgEdit.NamingContainer;
                txtDescription.Text = Convert.ToString(row.Cells[1].Text);
                txtAmount.Text = Convert.ToString(row.Cells[3].Text);
                Session["AllowDedId"] = Convert.ToInt32(row.Cells[0].Text);
                EntityAllowanceDeduction entallow = new AllowanceDeductionBLL().SelectFlags(Convert.ToInt32(row.Cells[0].Text));
                if (entallow != null)
                {
                    RdAllowance.Checked = entallow.IsAllowance;
                    RdDeduction.Checked = entallow.IsDeduction;
                    RdFixed.Checked = entallow.IsFixed;
                    RdFlexible.Checked = entallow.IsFlexible;
                    RdPercentage.Checked = entallow.IsPercentage;
                    if (entallow.IsBasic != null)
                    {
                        ChkIsbasic.Checked = entallow.IsBasic.Value;
                    }
                }
                btnUpdate.Visible = true;
                BtnSave.Visible = false;
                MultiView1.SetActiveView(View2);

            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        public void GetAllowanceDed()
        {
            try
            {
                List<EntityAllowanceDeduction> lst = objallow.GetAllowDed();
                dgvAllowanceDeduction.DataSource = lst;
                dgvAllowanceDeduction.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        public void Clear()
        {
            txtDescription.Text = string.Empty;
            txtAmount.Text = string.Empty;
            txtAmount.Enabled = true;
            RdAllowance.Checked = true;
            RdDeduction.Checked = false;
            RdFixed.Checked = false;
            RdFlexible.Checked = false;
            RdPercentage.Checked = false;
            ChkIsbasic.Checked = false;
        }

        protected void RdAllowance_CheckedChanged(object sender, EventArgs e)
        {
            if (RdAllowance.Checked)
            {
                lblDescription.Text = "Allowance Name";
                lblAmount.Text = "Allowance Amount";
                if (ChkIsbasic.Checked)
                {
                    txtAmount.Enabled = false;
                }
                else
                {
                    txtAmount.Enabled = true;
                }
            }
            else
            {
                lblDescription.Text = "Deduction Name";
                lblAmount.Text = "Deduction Amount";
            }
        }
        protected void RdDeduction_CheckedChanged(object sender, EventArgs e)
        {
            if (RdDeduction.Checked)
            {
                lblDescription.Text = "Deduction Name";
                lblAmount.Text = "Deduction Amount";
            }
            else
            {
                lblDescription.Text = "Allowance Name";
                lblAmount.Text = "Allowance Amount";
            }

        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            Clear();
            btnUpdate.Visible = false;
            BtnSave.Visible = true;
            MultiView1.SetActiveView(View2);
        }

        protected void RdFlexible_CheckedChanged(object sender, EventArgs e)
        {
            if (RdFlexible.Checked)
            {
                txtAmount.Enabled = false;
                txtAmount.Text = string.Empty;
            }
            else if (RdFixed.Checked)
            {
                txtAmount.Enabled = true;
            }
            else if (RdPercentage.Checked)
            {
                txtAmount.Enabled = true;
            }
        }
        protected void ChkIsbasic_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkIsbasic.Checked)
            {
                txtAmount.Enabled = false;
                txtAmount.ReadOnly = true;
                RdFixed.Checked = false;
                RdPercentage.Checked = false;
            }
            else
            {
                txtAmount.Enabled = true;
            }
        }
    }
}