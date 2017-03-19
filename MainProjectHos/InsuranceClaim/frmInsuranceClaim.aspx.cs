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

namespace MainProjectHos.InsuranceClaim
{
    public partial class frmInsuranceClaim : System.Web.UI.Page
    {
        InsuranceClaimBLL MobjClaim = new InsuranceClaimBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] != null)
            {
                EntityLogin entLogin = (EntityLogin)Session["user"];

                List<EntityFormMaster> entFormAcc = (List<EntityFormMaster>)Session["AccessibleForm"];
                int Access = (from tbl in entFormAcc
                              where tbl.FormName.Contains("frmInsuranceClaim")
                              select tbl).Count();

                if (Access > 0)
                {
                    if (!Page.IsPostBack)
                    {
                        BindPatientList();
                        BindInsuranceComName();
                        Session["Myflag"] = string.Empty;
                        BindInsuranceClaim();
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
        public void BindPatientList()
        {
            try
            {
                List<EntityPatientAdmit> lst = MobjClaim.GetPatientList();
                ddlPatient.DataSource = lst;
                lst.Insert(0, new EntityPatientAdmit() { AdmitId = 0, PatientFirstName = "--Select--" });
                ddlPatient.DataValueField = "AdmitId";
                ddlPatient.DataTextField = "PatientFirstName";
                ddlPatient.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        public void BindInsuranceComName()
        {
            try
            {
                List<EntityPatientAdmit> lst = MobjClaim.GetInsuranceName();
                ddlInsuranceComName.DataSource = lst;
                lst.Insert(0, new EntityPatientAdmit { CompanyId = 0, CompanyName = "---Select---" });
                ddlInsuranceComName.DataValueField = "CompanyId";
                ddlInsuranceComName.DataTextField = "CompanyName";
                ddlInsuranceComName.DataBind();
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        public void Clear()
        {
            ddlPatient.SelectedIndex = 0;
            ddlInsuranceComName.SelectedIndex = 0;
            txtClaimDate.Text = string.Empty;
            chkOtherBills.Checked = false;
            txtBillNo.Text = string.Empty;
            txtBilldate.Text = string.Empty;
            txtBilltype.Text = string.Empty;
            txtBillAmount.Text = string.Empty;
            txtClaimAmount.Text = string.Empty;
        }

        public void BindData()
        {
            try
            {
                List<EntityinsuranceClaimDetails> lst = MobjClaim.GetDetails(Convert.ToInt32(ddlPatient.SelectedValue));
                dgvChargeDetails.DataSource = lst;
                // Session["Myflag"] = "mainDelete";
                Session["Charges"] = lst;
                dgvChargeDetails.DataBind();
                decimal amt = 0;
                foreach (GridViewRow item in dgvChargeDetails.Rows)
                {
                    amt = amt + Convert.ToDecimal(item.Cells[3].Text);
                }
                txtClaimAmount.Text = Convert.ToString(decimal.Round(amt, 2));
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void BtnEditCharge_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton imgEdit = (ImageButton)sender;
                GridViewRow row = (GridViewRow)imgEdit.NamingContainer;
                Session["tempid"] = Convert.ToInt32(dgvChargeDetails.DataKeys[row.RowIndex].Value);
                List<EntityinsuranceClaimDetails> lst = (List<EntityinsuranceClaimDetails>)Session["ClaimCharges"];

                foreach (EntityinsuranceClaimDetails item in lst)
                {
                    if (Convert.ToInt32(Session["tempid"]) == item.TempId)
                    {
                        if (item.IsOtherBill == true)
                        {
                            txtBillNo.Text = Convert.ToString(item.BillNo);
                            txtBilldate.Text = Convert.ToString(item.BillDate);
                            txtBilltype.Text = Convert.ToString(item.BillType);
                            txtBillAmount.Text = Convert.ToString(item.BillAmount);
                            chkOtherBills.Checked = item.IsOtherBill;
                        }
                        else
                        {
                            lblMsg.Text = "You cannot Edit This Bill Details...";
                            break;
                        }
                    }
                }
                btnAdd.Visible = false;
                btnUpdatecharge.Visible = true;
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
                int TempId = Convert.ToInt32(dgvChargeDetails.DataKeys[row.RowIndex].Value);
                List<EntityinsuranceClaimDetails> lstinsurance = new List<EntityinsuranceClaimDetails>();
                if (Session["MyFlag"].ToString() == "Addnew")
                {
                    List<EntityinsuranceClaimDetails> lst = (List<EntityinsuranceClaimDetails>)Session["Charges"];

                    foreach (EntityinsuranceClaimDetails item in lst)
                    {
                        if (item.IsOtherBill == true)
                        {
                            if (item.TempId != TempId)
                            {
                                lstinsurance.Add(item);
                            }
                        }
                        else
                        {
                            lblMsg.Text = "You Cannot delete this Bills..";
                            lstinsurance.Add(item);
                        }
                    }
                    txtClaimAmount.Text = Convert.ToString(lstinsurance.Where(p => p.IsDelete == false).ToList().Sum(p => p.BillAmount));
                    dgvChargeDetails.DataSource = lstinsurance;
                    // Session["Myflag"] = "mainDelete";
                    Session["Charges"] = lstinsurance;
                    dgvChargeDetails.DataBind();
                }

                if (Session["MyFlag"].ToString() == "Edit")
                {
                    List<EntityinsuranceClaimDetails> lstDel = (List<EntityinsuranceClaimDetails>)Session["ClaimCharges"];

                    foreach (EntityinsuranceClaimDetails item in lstDel)
                    {
                        if (item.IsOtherBill == true)
                        {
                            if (item.TempId != TempId)
                            {
                                item.IsDelete = false;
                            }
                            else
                            {
                                item.IsDelete = true;
                            }
                        }
                        lstinsurance.Add(item);
                    }
                    txtClaimAmount.Text = Convert.ToString(lstinsurance.Where(p => p.IsDelete == false).ToList().Sum(p => p.BillAmount));
                    dgvChargeDetails.DataSource = lstinsurance.Where(p => p.IsDelete == false).ToList();
                    dgvChargeDetails.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void btnAddCharge_Click(object sender, EventArgs e)
        {
            try
            {
                List<EntityinsuranceClaimDetails> lst = new List<EntityinsuranceClaimDetails>(); ;
                if (Convert.ToString(Session["MyFlag"]).Equals("Addnew", StringComparison.CurrentCultureIgnoreCase))
                {
                    lst = (List<EntityinsuranceClaimDetails>)Session["Charges"];
                }
                else
                {
                    lst = (List<EntityinsuranceClaimDetails>)Session["ClaimCharges"];
                }
                lst.Add(new EntityinsuranceClaimDetails
                {
                    BillNo = Convert.ToInt32(txtBillNo.Text),
                    BillDate = Convert.ToDateTime(txtBilldate.Text),
                    BillType = txtBilltype.Text,
                    BillAmount = Convert.ToDecimal(txtBillAmount.Text),
                    IsOtherBill = true,
                    TempId = lst.Count + 1
                });

                txtClaimAmount.Text = string.Format("{0:0.00}", (lst.Sum(P => P.BillAmount)));
                dgvChargeDetails.DataSource = lst;
                dgvChargeDetails.DataBind();
                txtBillNo.Text = string.Empty;
                txtBilltype.Text = string.Empty;
                txtBilldate.Text = string.Empty;
                txtBillAmount.Text = string.Empty;
                chkOtherBills.Checked = false;
                chkOtherBills_CheckedChanged(sender, e);
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtBillNo.Text = string.Empty;
            txtBilltype.Text = string.Empty;
            txtBilldate.Text = string.Empty;
            txtBillAmount.Text = string.Empty;
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                tblInsuranceClaim tblins = new tblInsuranceClaim();

                tblins.AdmitId = Convert.ToInt32(ddlPatient.SelectedValue);
                tblins.CompanyId = Convert.ToInt32(ddlInsuranceComName.SelectedValue);
                tblins.ClaimDate = Convert.ToDateTime(txtClaimDate.Text);
                tblins.Total = Convert.ToDecimal(txtClaimAmount.Text);

                tblPatientAdmitDetail objFac = MobjClaim.GetEmployee(Convert.ToInt32(ddlPatient.SelectedValue));
                if (objFac != null)
                {
                    tblInsuranceClaim objExist = MobjClaim.CheckExistRecord(objFac.AdmitId);
                    if (objExist == null)
                    {
                        if (MobjClaim.ValidateAllocation(tblins))
                        {
                            List<EntityinsuranceClaimDetails> inslst = (List<EntityinsuranceClaimDetails>)Session["Charges"];
                            int ClaimId = Convert.ToInt32(MobjClaim.Save(tblins, inslst));
                            lblMessage.Text = "Record Saved Successfully.....";
                            Clear();
                            Session["Charges"] = null;
                        }
                    }
                    else
                    {
                        Clear();
                        List<EntityinsuranceClaimDetails> lst = new List<EntityinsuranceClaimDetails>();
                        dgvChargeDetails.DataSource = lst;
                        dgvChargeDetails.DataBind();
                        lblMsg.Text = string.Empty;
                        MultiView1.SetActiveView(View1);
                        lblMessage.Text = "Insurance Is Already Allocated To Patient!!";
                    }
                }
                else
                {
                    lblMsg.Text = "Invalid Patient";
                }
                Session["Charges"] = new List<EntityinsuranceClaimDetails>();
                BindInsuranceClaim();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
            MultiView1.SetActiveView(View1);
        }

        public void BindInsuranceClaim()
        {
            try
            {
                List<EntityInsuranceClaim> lst = MobjClaim.GetInsurance();

                Session["ClaimDetails"] = lst;
                dgvClaim.DataSource = lst;
                int lintRowcount = lst.Count();
                lblRowCount.Text = "<b>Total Records:</b> " + lintRowcount.ToString();
                dgvClaim.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        public void BindClaim(int Id)
        {
            try
            {
                List<EntityinsuranceClaimDetails> lst = MobjClaim.GetClaim(Id);
                dgvChargeDetails.DataSource = lst;
                dgvChargeDetails.DataBind();
                Session["ClaimCharges"] = lst;
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                ddlPatient.Enabled = false;
                tblInsuranceClaim tblins = new tblInsuranceClaim();
                tblins.ClaimId = Convert.ToInt32(Session["ClaimId"]);
                tblins.AdmitId = Convert.ToInt32(ddlPatient.SelectedValue);
                tblins.CompanyId = Convert.ToInt32(ddlInsuranceComName.SelectedValue);
                tblins.ClaimDate = Convert.ToDateTime(txtClaimDate.Text);
                tblins.Total = Convert.ToDecimal(txtClaimAmount.Text);
                List<EntityinsuranceClaimDetails> inslst = (List<EntityinsuranceClaimDetails>)Session["ClaimCharges"];
                MobjClaim.Update(tblins, inslst);
                lblMessage.Text = "Record Updated Successfully.....";
                Clear();
                List<EntityinsuranceClaimDetails> lst = new List<EntityinsuranceClaimDetails>();
                dgvChargeDetails.DataSource = lst;
                dgvChargeDetails.DataBind();
                BindInsuranceClaim();
                Session["ClaimCharges"] = null;
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
            MultiView1.SetActiveView(View1);
        }

        protected void btnUpdatecharge_Click(object sender, EventArgs e)
        {
            try
            {
                List<EntityinsuranceClaimDetails> lst = (List<EntityinsuranceClaimDetails>)Session["ClaimCharges"];
                // List<EntityinsuranceClaimDetails> lstDel = (List<EntityinsuranceClaimDetails>)Session["ClaimCharges"];
                foreach (EntityinsuranceClaimDetails item in lst)
                {
                    if (Convert.ToInt32(Session["tempid"]) == item.TempId)
                    {
                        item.BillNo = Convert.ToInt32(txtBillNo.Text);
                        item.BillType = txtBilltype.Text;
                        item.BillAmount = Convert.ToDecimal(txtBillAmount.Text);
                        item.BillDate = Convert.ToDateTime(txtBilldate.Text);
                    }
                    else
                        if (item.IsDelete)
                    {
                        lst.Add(new EntityinsuranceClaimDetails()
                        {
                            ClaimDetailId = item.ClaimDetailId,
                            ClaimId = item.ClaimId,
                            IsDelete = item.IsDelete,
                            BillNo = item.BillNo,
                            BillAmount = item.BillAmount,
                            BillType = item.BillType,
                            BillDate = item.BillDate,
                            IsOtherBill = item.IsOtherBill
                        });
                    }
                }
                MobjClaim.Update(lst);
                txtClaimAmount.Text = Convert.ToString(lst.Sum(p => p.BillAmount));
                dgvChargeDetails.DataSource = lst;
                dgvChargeDetails.DataBind();
                txtBillNo.Text = string.Empty;
                txtBilltype.Text = string.Empty;
                txtBilldate.Text = string.Empty;
                txtBillAmount.Text = string.Empty;
                chkOtherBills.Checked = false;
                chkOtherBills_CheckedChanged(sender, e);
                btnUpdatecharge.Visible = false;
                btnAdd.Visible = true;
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
                btnAdd.Visible = true;
                btnUpdatecharge.Visible = false;
                BtnSave.Visible = false;
                btnUpdate.Visible = true;
                Session["MyFlag"] = "Edit";
                ImageButton imgEdit = (ImageButton)sender;
                GridViewRow row = (GridViewRow)imgEdit.NamingContainer;
                Session["ClaimId"] = Convert.ToInt32(dgvClaim.DataKeys[row.RowIndex].Value);
                EntityInsuranceClaim lst = MobjClaim.GetApproval(Convert.ToInt32(Session["ClaimId"]));
                if (lst.IsApproved == true)
                {
                    lblMessage.Text = "Approval is Done.You Cannot Edit Details....";
                    return;
                }
                else
                {
                    ListItem item = ddlPatient.Items.FindByText(Convert.ToString(row.Cells[1].Text));
                    ddlPatient.SelectedValue = item.Value;
                    ddlPatient_SelectedIndexChanged(sender, e);
                    ListItem ilist = ddlInsuranceComName.Items.FindByText(Convert.ToString(row.Cells[2].Text));
                    ddlInsuranceComName.SelectedValue = ilist.Value;
                    txtClaimDate.Text = (row.Cells[3].Text).ToString();
                    txtClaimAmount.Text = row.Cells[4].Text;
                    BindClaim(Convert.ToInt32(Session["ClaimId"]));
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
            MultiView1.SetActiveView(View2);
        }

        protected void BtnClose_Click(object sender, EventArgs e)
        {
            Clear();
            MultiView1.SetActiveView(View1);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSearch.Text))
                {
                    List<EntityInsuranceClaim> lst = MobjClaim.GetInsurance(txtSearch.Text);

                    Session["ClaimDetails"] = lst;
                    dgvClaim.DataSource = lst;
                    int lintRowcount = lst.Count();
                    lblRowCount.Text = "<b>Total Records:</b> " + lintRowcount.ToString();
                    dgvClaim.DataBind();
                }
                else
                {
                    lblMessage.Text = "Please Fill Search Text...";
                    txtSearch.Focus();
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
                txtSearch.Text = string.Empty;
                BindInsuranceClaim();
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
                ListConverter.ToDataTable((List<EntityInsuranceClaim>)Session["ClaimDetails"]);
                Session["Details"] = ListConverter.ToDataTable((List<EntityInsuranceClaim>)Session["ClaimDetails"]);
                Response.Redirect("~/ExcelReport/MonthwiseSalExcel.aspx");
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void chkOtherBills_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOtherBills.Checked == true)
            {
                txtBillNo.Enabled = true;
                txtBilldate.Enabled = true;
                txtBilltype.Enabled = true;
                txtBillAmount.Enabled = true;
                //EntityPatientAdmit lstADate = MobjClaim.GetAdmitDate(Convert.ToInt32(ddlPatient.SelectedValue));
                //calBillDate.StartDate = lstADate.AdmitDate;
                //if (txtClaimDate.Text != null)
                //{
                //    calBillDate.EndDate = Convert.ToDateTime(txtClaimDate.Text);
                //}
                //else
                //{
                //    calBillDate.EndDate = DateTime.Now.Date;
                //}
            }
            else
            {
                txtBillNo.Text = string.Empty;
                txtBilltype.Text = string.Empty;
                txtBilldate.Text = string.Empty;
                txtBillAmount.Text = string.Empty;
                txtBillNo.Enabled = false;
                txtBilldate.Enabled = false;
                txtBilltype.Enabled = false;
                txtBillAmount.Enabled = false;

            }
        }
        protected void BtnAddNewclaim_Click(object sender, EventArgs e)
        {
            try
            {
                Clear();
                Session["MyFlag"] = "Addnew";
                ddlPatient.Enabled = true;
                chkOtherBills.Checked = false;
                chkOtherBills_CheckedChanged(sender, e);
                BtnSave.Visible = true;
                btnUpdate.Visible = false;
                ddlInsuranceComName.Enabled = false;
                btnAdd.Visible = true;
                btnUpdatecharge.Visible = false;
                List<EntityinsuranceClaimDetails> lst = new List<EntityinsuranceClaimDetails>();
                dgvChargeDetails.DataSource = lst;
                dgvChargeDetails.DataBind();
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }

            MultiView1.SetActiveView(View2);
        }

        protected void ddlPatient_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlPatient.SelectedIndex > 0)
                {
                    EntityPatientAdmit lst = MobjClaim.GetinsuranceComName(Convert.ToInt32(ddlPatient.SelectedValue));
                    ListItem lstitem = ddlInsuranceComName.Items.FindByText(lst.CompanyName);
                    if (lstitem != null)
                    {
                        ddlInsuranceComName.SelectedValue = lstitem.Value;
                    }
                    BindData();

                }
                else
                {
                    lblMsg.Text = "Please Select Patient.";
                }

            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void dgvClaim_DataBound(object sender, EventArgs e)
        {
            int lintCurrentPage = dgvClaim.PageIndex + 1;
            lblPageCount.Text = "<b>Page</b> " + lintCurrentPage.ToString() + "<b> of </b>" + dgvClaim.PageCount.ToString();
        }

        protected void dgvClaim_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dgvClaim.DataSource = (DataTable)Session["ClaimDetails"];
                dgvClaim.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void dgvClaim_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvClaim.PageIndex = e.NewPageIndex;
        }
    }
}