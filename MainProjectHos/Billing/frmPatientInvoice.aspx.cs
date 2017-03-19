using MainProjectHos.Models.BusinessLayer;
using MainProjectHos.Models.DataLayer;
using MainProjectHos.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MainProjectHos.Billing
{
    public partial class frmPatientInvoice : System.Web.UI.Page
    {
        PatientInvoiceBLL mobjDeptBLL = new PatientInvoiceBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] != null)
            {
                EntityLogin entLogin = (EntityLogin)Session["user"];

                List<EntityFormMaster> entFormAcc = (List<EntityFormMaster>)Session["AccessibleForm"];
                int Access = (from tbl in entFormAcc
                              where tbl.FormName.Contains("frmPatientInvoice")
                              select tbl).Count();

                if (Access > 0)
                {
                    if (!Page.IsPostBack)
                    {
                        Session["MyFlag"] = string.Empty;
                        GetPatientInvoice();
                        BindOtherCharge();

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

        protected void ddlBillType_IndexChanged(object sender, EventArgs e)
        {
            if (ddlBillType.SelectedItem.Text == "Intermediate")
            {
                BindPatientList1();
                Label4.Visible = false;
                txtPatientType.Visible = false;
            }
            else
            {
                if (btnUpdate.Enabled == true)
                {
                    Label4.Visible = true;
                    txtPatientType.Visible = true;
                }
                else
                {
                    BindPatientList(false);
                    Label4.Visible = true;
                    txtPatientType.Visible = true;
                }
            }
        }

        protected void btnEditCharges_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton imgEdit = (ImageButton)sender;
                GridViewRow row = (GridViewRow)imgEdit.NamingContainer;
                Session["tempid"] = Convert.ToInt32(GridView1.DataKeys[row.RowIndex].Value);
                if (row != null)
                {
                    List<EntityInvoiceDetails> lst = (List<EntityInvoiceDetails>)Session["BillDetails"];
                    if (lst != null)
                    {
                        EntityInvoiceDetails objEdited = (from tmp in lst
                                                          where tmp.TempId == Convert.ToInt32(Session["tempid"])
                                                          select tmp).FirstOrDefault();
                        if (objEdited != null)
                        {
                            txtNoofDays.Text = Convert.ToString(objEdited.NoOfDays);
                            txtQuantity.Text = Convert.ToString(objEdited.Quantity);
                            txtRemarks.Text = Convert.ToString(objEdited.Remarks);
                            txtChargePerDay.Text = Convert.ToString(objEdited.PerDayCharge);
                            txtAmount.Text = Convert.ToString(objEdited.Amount);
                            ListItem objCharge = ddlOther.Items.FindByText(objEdited.ChargesName);
                            if (objCharge != null)
                            {
                                ddlOther.SelectedValue = objCharge.Value;
                            }
                            btnUpdateCharge.Visible = true;
                            btnAdd.Visible = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void btnUpdateCharge_Click(object sender, EventArgs e)
        {
            try
            {
                btnUpdateCharge.Visible = false;
                btnAdd.Visible = true;
                if (Session["tempid"] != null)
                {
                    int id = Convert.ToInt32(Session["tempid"]);
                    if (id > 0)
                    {
                        List<EntityInvoiceDetails> lst = (List<EntityInvoiceDetails>)Session["BillDetails"];
                        if (lst != null)
                        {
                            foreach (EntityInvoiceDetails item in lst)
                            {
                                if (id == item.TempId)
                                {
                                    item.PerDayCharge = Convert.ToDecimal(txtChargePerDay.Text);
                                    item.Remarks = Convert.ToString(txtRemarks.Text);
                                    item.NoOfDays = string.IsNullOrEmpty(txtNoofDays.Text) == false ? Convert.ToInt32(txtNoofDays.Text) : 0;
                                    item.Quantity = string.IsNullOrEmpty(txtQuantity.Text) == false ? Convert.ToInt32(txtQuantity.Text) : 0;
                                    item.Amount = string.IsNullOrEmpty(txtAmount.Text) == false ? Convert.ToDecimal(txtAmount.Text) : 0;
                                    ClearOther();
                                    break;
                                }
                            }

                            GridView1.DataSource = lst.Where(p => p.IsDelete == false).ToList();
                            GridView1.DataBind();
                            txtTotal.Text = Convert.ToString(lst.Where(p => p.IsDelete == false).Sum(p => p.Amount));
                            txtNetAmount.Text = Convert.ToString(lst.Where(p => p.IsDelete == false).Sum(p => p.Amount));
                            //Calculation();
                            Session["BillDetails"] = lst;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        private void BindOtherCharge()
        {
            try
            {
                List<EntityChargeCategory> lstOther = mobjDeptBLL.GetOtherChargeList();
                ddlOther.DataSource = lstOther;
                lstOther.Insert(0, new EntityChargeCategory() { ChargesId = 0, ChargeCategoryName = "--Select--" });
                ddlOther.DataValueField = "ChargesId";
                ddlOther.DataTextField = "ChargeCategoryName";
                ddlOther.DataBind();
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        private void BindPatientList(bool IsDischarge)
        {
            try
            {
                List<EntityPatientMaster> lstPat = mobjDeptBLL.GetPatientList(IsDischarge);
                ddlPatient.DataSource = lstPat;
                lstPat.Insert(0, new EntityPatientMaster() { PatientId = 0, PatientFirstName = "--Select--" });
                ddlPatient.DataValueField = "PatientId";
                ddlPatient.DataTextField = "PatientFirstName";
                ddlPatient.DataBind();
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        private void BindPatientList1()
        {
            try
            {
                List<EntityPatientMaster> lstPat = mobjDeptBLL.GetPatientList1();
                ddlPatient.DataSource = lstPat;
                lstPat.Insert(0, new EntityPatientMaster() { PatientId = 0, PatientFirstName = "--Select--" });
                ddlPatient.DataValueField = "PatientId";
                ddlPatient.DataTextField = "PatientFirstName";
                ddlPatient.DataBind();
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void BtnClose_Click(object sender, EventArgs e)
        {
            try
            {
                Clear();
                GetPatientInvoice();
                MultiView1.SetActiveView(View1);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void btnOPDRefund_Click(object sender, EventArgs e)
        {
            int lintCnt = 0;
            try
            {
                lblMessage.Text = string.Empty;
                ImageButton imgEdit = (ImageButton)sender;
                GridViewRow row = (GridViewRow)imgEdit.NamingContainer;
                Session["Bill_Id"] = Convert.ToInt32(row.Cells[0].Text);

                List<EntityInvoiceDetails> lst = mobjDeptBLL.SelectPatientInvoiceForEdit(Convert.ToInt32(Session["Bill_Id"]));
                EntityPatientInvoice entInvoice = new EntityPatientInvoice();
                entInvoice.BillNo = Convert.ToInt32(Session["Bill_Id"]);
                entInvoice.BillType = Convert.ToString(row.Cells[5].Text);
                entInvoice.NetAmount = Convert.ToDecimal(row.Cells[4].Text);
                entInvoice.Description = Convert.ToString(row.Cells[6].Text);
                if (Convert.ToString(row.Cells[5].Text) == "Original" && (Convert.ToString(row.Cells[6].Text) == "OPD"))
                {
                    lintCnt = mobjDeptBLL.UpdateOPDRefund(entInvoice, lst);

                    if (lintCnt > 0)
                    {
                        GetPatientInvoice();
                        lblMessage.Text = "OPD Bill Refund Successfully";
                    }
                    else
                    {
                        lblMessage.Text = "Not Refund";
                    }

                    GetPatientInvoice();
                }
                else
                {
                    if (Convert.ToString(row.Cells[5].Text) == "Intermediate")
                    {
                        lintCnt = mobjDeptBLL.UpdateOPDRefund(entInvoice, lst);

                        if (lintCnt > 0)
                        {
                            GetPatientInvoice();
                            lblMessage.Text = "Intermediate Bill Refund Successfully";
                        }
                    }
                    else
                    {
                        lblMessage.Text = "IPD Original Bill Not Refundable ";
                        GetPatientInvoice();
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
            MultiView1.SetActiveView(View1);
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                BindPatientList(true);
                ImageButton imgEdit = (ImageButton)sender;
                GridViewRow row = (GridViewRow)imgEdit.NamingContainer;
                Session["Bill_Id"] = Convert.ToInt32(row.Cells[0].Text);

                List<EntityInvoiceDetails> tblEmp = mobjDeptBLL.SelectPatientInvoiceForEdit(Convert.ToInt32(Session["Bill_Id"]));
                if (tblEmp.Count > 0)
                {
                    if (Convert.ToString(tblEmp[0].BillType) == "Original")
                    {
                        lblMessage.Text = "Patient Original Bill is Already Created...";
                    }
                    else
                    {
                        if (Convert.ToString(tblEmp[0].BillType) == "Intermediate")
                        {
                            lblMessage.Text = "Patient Intermediate Bill is Already Created...";
                        }
                        else
                        {
                            ListItem itemBill = ddlBillType.Items.FindByText(Convert.ToString(tblEmp[0].BillType));
                            ddlBillType.SelectedValue = itemBill.Value;

                            ddlPatient.Enabled = false;
                            ListItem item = ddlPatient.Items.FindByText(Convert.ToString(tblEmp[0].PatientName));
                            ddlPatient.SelectedValue = item.Value;

                            GridView1.DataSource = tblEmp;
                            GridView1.DataBind();
                            txtBillDate.Text = DateTime.UtcNow.Date.ToString("MM/dd/yyyy");
                            txtBillDate.Enabled = false;
                            txtDiscount.Text = Convert.ToString(tblEmp[0].Discount);
                            txtPatientType.Text = Convert.ToString(tblEmp[0].PatientType);
                            txtTotal.Text = Convert.ToString(tblEmp[0].Total);
                            txtNetAmount.Text = Convert.ToString(tblEmp[0].NetAmount);
                            CustomerTransactionBLL mobjCustBLL = new CustomerTransactionBLL();
                            txtReceivedAmount.Text = Convert.ToString(0);
                            txtBalance.Text = Convert.ToString(mobjCustBLL.GetPatientBalance(Convert.ToInt32(ddlPatient.SelectedValue)));
                            txtTotalAdvance.Text = Convert.ToString(mobjCustBLL.GetPatientTotalAdvance(Convert.ToInt32(ddlPatient.SelectedValue)));
                            txtRefund.Text = Convert.ToString(0);
                            foreach (EntityInvoiceDetails item1 in tblEmp)
                            {
                                if (item1.NoOfDays > 0 && item1.DocAllocationId > 0)
                                {
                                    Session["Days"] = item1.NoOfDays;
                                    break;
                                }
                            }
                            if (row.Cells[4].Text == "True")
                            {
                                chkIsCash.Checked = true;
                            }
                            btnUpdate.Visible = true;
                            BtnSave.Visible = false;
                            btnUpdateCharge.Visible = false;
                            Session["BillDetails"] = tblEmp;
                            MultiView1.SetActiveView(View2);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }


        protected void BtnAddNewDept_Click(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;
            //Session["MyFlag"] = "ADD";
            BindPatientList(false);
            Session["BillDetails"] = new List<EntityInvoiceDetails>();
            btnUpdate.Visible = false;
            txtBillDate.Enabled = true;
            lblWard.Visible = false;
            txtWard.Visible = false;
            Label4.Visible = false;
            txtPatientType.Visible = false;
            BtnSave.Visible = true;
            btnUpdateCharge.Visible = false;
            btnAdd.Visible = true;
            txtBillDate.Enabled = false;
            txtBillDate.Text = DateTime.UtcNow.Date.ToString("MM/dd/yyyy");
            //CalBillDate.StartDate = DateTime.Now.Date;
            ddlPatient.Enabled = true;
            ddlBillType.SelectedIndex = 0;
            Clear();
            MultiView1.SetActiveView(View2);
        }

        private void Clear()
        {
            ddlPatient.SelectedIndex = 0;
            ClearOther();
            txtAmount.Enabled = false;
            GridView1.DataSource = new List<EntityInvoiceDetails>();
            GridView1.DataBind();
            txtPatientType.Text = string.Empty;
            txtTotal.Text = string.Empty;
            txtNetAmount.Text = string.Empty;
            txtDiscount.Text = string.Empty;
            txtReceivedAmount.Text = Convert.ToString(0);
            //txtVAT.Text = Convert.ToString(0);
            //txtService.Text = Convert.ToString(0);
            txtSearch.Text = string.Empty;
            txtBalance.Text = Convert.ToString(0);
            ddlBillType.SelectedIndex = 0;
            //txtBillDate.Text = string.Empty;
        }

        private void ClearSub()
        {
            ddlOther.SelectedIndex = 0;
            txtChargePerDay.Text = string.Empty;
            txtNoofDays.Text = string.Empty;
            txtQuantity.Text = string.Empty;
            txtAmount.Text = string.Empty;
            txtRemarks.Text = string.Empty;
        }

        private void GetPatientInvoice()
        {
            try
            {
                List<STP_GetPatientInvoiceResult> ldtRoom = mobjDeptBLL.GetPatientInvoice();
                if (ldtRoom.Count > 0 && ldtRoom != null)
                {
                    dgvTestParameter.DataSource = ldtRoom;
                    dgvTestParameter.DataBind();
                    Session["RoomDetails"] = ldtRoom;
                    int lintRowcount = ldtRoom.Count;
                    lblRowCount.Text = "<b>Total Records:</b> " + lintRowcount.ToString();
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                CriticareHospitalDataContext objData = new CriticareHospitalDataContext();
                List<STP_GetPatientInvoiceResult> lst = (from tbl in objData.STP_GetPatientInvoice()
                                                         where tbl.FullName.ToUpper().Contains(txtSearch.Text.ToUpper()) || tbl.PatientType.ToUpper().Contains(txtSearch.Text.ToUpper())
                                                         select tbl).ToList();
                dgvTestParameter.DataSource = lst;
                dgvTestParameter.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }

        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            GetPatientInvoice();
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int Invoice = 0;
                EntityPatientInvoice entInvoice = new EntityPatientInvoice();
                EntityInvoiceDetails entInvoiceDetails = new EntityInvoiceDetails();

                if (ddlPatient.SelectedIndex == 0)
                {
                    lblMsg.Text = "Please Select Patient Name";
                    ddlPatient.Focus();
                    return;
                }
                else
                {
                    if (string.IsNullOrEmpty(txtBillDate.Text.Trim()))
                    {
                        lblMsg.Text = "Please Select Bill Date";
                        CalBillDate.Focus();
                        return;
                    }
                    else
                    {
                        if (ddlOther.SelectedIndex > 0 && string.IsNullOrEmpty(txtAmount.Text.Trim()))
                        {
                            lblMsg.Text = "Please Enter Amount";
                            txtAmount.Focus();
                            return;
                        }
                        else
                        {
                            if (ddlBillType.SelectedIndex == 0)
                            {
                                lblMsg.Text = "Please Select Bill Type";
                                ddlBillType.Focus();
                                return;
                            }

                            entInvoice.PatientId = Convert.ToInt32(ddlPatient.SelectedValue);
                            entInvoice.BillType = Convert.ToString(ddlBillType.SelectedItem.Text);
                            entInvoice.PreparedByName = Convert.ToString(Session["AdminName"]);
                            entInvoice.PatientType = Convert.ToString(txtPatientType.Text);
                            entInvoice.Amount = Convert.ToDecimal(txtTotal.Text);
                            entInvoice.BillDate = Convert.ToDateTime(txtBillDate.Text);
                            entInvoice.NetAmount = Convert.ToDecimal(txtNetAmount.Text);
                            entInvoice.TotalAdvance = Convert.ToDecimal(txtTotalAdvance.Text);
                            entInvoice.BalanceAmount = Convert.ToDecimal(txtBalance.Text);
                            entInvoice.ReceivedAmount = Convert.ToDecimal(txtReceivedAmount.Text);
                            entInvoice.RefundAmount = Convert.ToDecimal(txtRefund.Text);

                            if (string.IsNullOrEmpty(txtDiscount.Text))
                            {
                                entInvoice.FixedDiscount = 0;
                            }
                            else
                            {
                                entInvoice.FixedDiscount = Convert.ToDecimal(txtDiscount.Text);
                            }
                            
                            List<EntityInvoiceDetails> lstInvoice = (List<EntityInvoiceDetails>)Session["BillDetails"];

                            Invoice = mobjDeptBLL.InsertInvoice(entInvoice, lstInvoice, chkIsCash.Checked);
                            if (Invoice > 0)
                            {
                                GetPatientInvoice();
                                lblMessage.Text = "Record Inserted Successfully";
                            }
                            else
                            {
                                lblMessage.Text = "Record Not Inserted...";
                            }
                            Session["BillDetails"] = new List<EntityInvoiceDetails>();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
            MultiView1.SetActiveView(View1);
        }


        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            int lintCnt = 0;
            try
            {
                List<EntityInvoiceDetails> lst = (List<EntityInvoiceDetails>)Session["BillDetails"];
                EntityPatientInvoice entInvoice = new EntityPatientInvoice();
                entInvoice.PatientId = Convert.ToInt32(ddlPatient.SelectedValue);
                entInvoice.BillType = Convert.ToString(ddlBillType.SelectedItem.Text);
                entInvoice.PreparedByName = Convert.ToString(Session["AdminName"]);
                entInvoice.Amount = Convert.ToDecimal(txtTotal.Text);
                entInvoice.BillDate = Convert.ToDateTime(txtBillDate.Text);
                entInvoice.NetAmount = Convert.ToDecimal(txtNetAmount.Text);
                entInvoice.BillNo = Convert.ToInt32(Session["Bill_Id"]);
                entInvoice.TotalAdvance = Convert.ToDecimal(txtTotalAdvance.Text);
                entInvoice.BalanceAmount = Convert.ToDecimal(txtBalance.Text);
                entInvoice.ReceivedAmount = Convert.ToDecimal(txtReceivedAmount.Text);
                entInvoice.RefundAmount = Convert.ToDecimal(txtRefund.Text);
                if (chkIsCash.Checked)
                {
                    entInvoice.IsCash = true;
                }
                else
                {
                    entInvoice.IsCash = false;
                }

                if (string.IsNullOrEmpty(txtDiscount.Text))
                {
                    entInvoice.FixedDiscount = 0;
                }
                else
                {
                    entInvoice.FixedDiscount = Convert.ToDecimal(txtDiscount.Text);
                }
                
                lintCnt = mobjDeptBLL.UpdateInvoice(entInvoice, lst);

                if (lintCnt > 0)
                {
                    GetPatientInvoice();
                    lblMessage.Text = "Record Updated Successfully";
                }
                else
                {
                    lblMessage.Text = "Record Not Updated";
                }
                GetPatientInvoice();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
            MultiView1.SetActiveView(View1);
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton imgDelete = (ImageButton)sender;
                GridViewRow row = (GridViewRow)imgDelete.NamingContainer;
                Session["tempid"] = Convert.ToInt32(GridView1.DataKeys[row.RowIndex].Value);
                List<EntityInvoiceDetails> lstFinal = new List<EntityInvoiceDetails>();
                List<EntityInvoiceDetails> lst = (List<EntityInvoiceDetails>)Session["BillDetails"];
                EntityInvoiceDetails obj = (from tbl in lst
                                            where tbl.TempId == Convert.ToInt32(Session["tempid"])
                                            select tbl).FirstOrDefault();
                if (obj != null)
                {
                    if (obj.BedAllocId > 0 || obj.DocAllocationId > 0 || obj.OTBedAllocId > 0)
                    {
                        lblMsg.Text = "You can not remove basic charges.";
                    }
                    else
                    {
                        if (BtnSave.Visible)
                        {
                            foreach (EntityInvoiceDetails item in lst)
                            {
                                if (item.OtherId != Convert.ToInt32(row.Cells[0].Text))
                                {
                                    lstFinal.Add(item);
                                }
                            }
                            Session["BillDetails"] = lstFinal;
                            GridView1.DataSource = lstFinal.Where(p => p.IsDelete == false).ToList();
                            txtTotal.Text = Convert.ToString(lstFinal.Where(p => p.IsDelete == false).Sum(p => p.Amount));
                        }
                        else
                        {
                            foreach (EntityInvoiceDetails item in lst)
                            {
                                if (item.TempId == Convert.ToInt32(Session["tempid"]))
                                {
                                    item.IsDelete = true;
                                }
                            }
                            Session["BillDetails"] = lst;
                            GridView1.DataSource = lst.Where(p => p.IsDelete == false).ToList();
                            txtTotal.Text = Convert.ToString(lst.Where(p => p.IsDelete == false).Sum(p => p.Amount));
                        }
                        GridView1.DataBind();
                        Calculation();
                    }
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void txtBillDate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (GridView1.Rows.Count > 0)
                {
                    if (Session["BillDetails"] != null)
                    {
                        if (ddlPatient.SelectedIndex > 0)
                        {
                            Session["Pat_Id"] = ddlPatient.SelectedValue;
                            EntityPatientMaster Cate = mobjDeptBLL.GetPatientCate(Convert.ToInt32(Session["Pat_Id"]));
                            EntityPatientAlloc objTxt = new PatientAllocDocBLL().GetPatientType(Convert.ToInt32(ddlPatient.SelectedValue));
                            //CalBillDate.StartDate = objTxt.AdmitDate;
                            Session["PatientType"] = Cate.PatientType;
                            List<EntityChargeCategory> lstCat = new ChargeCategoryBLL().GetChargeDetail();
                            List<EntityInvoiceDetails> lst = new List<EntityInvoiceDetails>();
                            foreach (EntityChargeCategory item in lstCat)
                            {
                                int IPD_Days = 0;
                                if (item.IsBed)
                                {
                                    decimal bedCharges = 0;
                                    List<EntityPatientInvoice> lstBed = mobjDeptBLL.GetOTBedCharges(Convert.ToInt32(Session["Pat_Id"]));
                                    if (lstBed.Count > 0)
                                    {
                                        //EntityInvoiceDetails entInv = new EntityInvoiceDetails() { BedAllocId = lstBed[0].BedAllocId, ChargesName = item.ChargeCategoryName, OtherChargesId = item.ChargesId };
                                        if (DateTime.Now.Date.CompareTo(lstBed[0].AllocDate.Date) == 0)
                                        {
                                            EntityInvoiceDetails entInv = new EntityInvoiceDetails() { BedAllocId = lstBed[0].BedAllocId, ChargesName = item.ChargeCategoryName, OtherChargesId = item.ChargesId };
                                            IPD_Days = 1;
                                            entInv.NoOfDays = IPD_Days;
                                            entInv.Quantity = 1;
                                            entInv.PerDayCharge = Convert.ToDecimal(lstBed[0].Amount);
                                            entInv.Amount = IPD_Days * entInv.Quantity * entInv.PerDayCharge;
                                            entInv.TempId = lst.Count + 1;
                                            entInv.IsDelete = false;
                                            lst.Add(entInv);
                                        }
                                        else
                                        {
                                            List<EntityBedAllocToPatient> entBedAlloc = new ICUDischargeBilling().GetICUAllocatedBedDetails(Convert.ToInt32(Session["Pat_Id"]));
                                            foreach (var item1 in entBedAlloc)
                                            {
                                                if (item1.CategoryDesc == "IPD")
                                                {
                                                    EntityInvoiceDetails entInv = new EntityInvoiceDetails() { BedAllocId = item1.BedAllocId, ChargesName = item.ChargeCategoryName, OtherChargesId = item.ChargesId };
                                                    if (item1.ShiftDate == null)
                                                    {
                                                        IPD_Days = IPD_Days + Convert.ToInt32(Convert.ToDateTime(txtBillDate.Text).Date.Subtract(item1.AllocationDate.Value.Date).Days);
                                                        bedCharges = bedCharges + ((Convert.ToInt32(Convert.ToDateTime(txtBillDate.Text).Date.Subtract(item1.AllocationDate.Value.Date).Days) + 1) * item1.Charges);
                                                        IPD_Days++;
                                                        entInv.Quantity = 1;
                                                        entInv.NoOfDays = Convert.ToInt32(Convert.ToDateTime(txtBillDate.Text).Date.Subtract(item1.AllocationDate.Value.Date).Days) + 1;
                                                        entInv.PerDayCharge = Convert.ToDecimal(item1.Charges);
                                                        entInv.Amount = ((Convert.ToInt32(Convert.ToDateTime(txtBillDate.Text).Date.Subtract(item1.AllocationDate.Value.Date).Days) + 1) * item1.Charges);
                                                        entInv.TempId = lst.Count + 1;
                                                        entInv.IsDelete = false;
                                                        lst.Add(entInv);
                                                    }
                                                    else
                                                    {
                                                        IPD_Days = IPD_Days + Convert.ToInt32(item1.ShiftDate.Value.Date.Subtract(item1.AllocationDate.Value.Date).Days);
                                                        bedCharges = bedCharges + (Convert.ToInt32(item1.ShiftDate.Value.Date.Subtract(item1.AllocationDate.Value.Date).Days) * item1.Charges);
                                                        entInv.NoOfDays = Convert.ToInt32(item1.ShiftDate.Value.Date.Subtract(item1.AllocationDate.Value.Date).Days);
                                                        entInv.PerDayCharge = Convert.ToDecimal(item1.Charges);
                                                        entInv.Quantity = 1;
                                                        entInv.Amount = (Convert.ToInt32(item1.ShiftDate.Value.Date.Subtract(item1.AllocationDate.Value.Date).Days) * item1.Charges);
                                                        entInv.TempId = lst.Count + 1;
                                                        entInv.IsDelete = false;
                                                        lst.Add(entInv);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

                                if (item.IsConsulting)
                                {
                                    IPD_Days = 0;
                                    if (Cate.PatientType == "IPD")
                                    {
                                        decimal bedCharges = 0;
                                        List<EntityPatientInvoice> lstConsult = mobjDeptBLL.GetConsultChargesOPD(Convert.ToInt32(Session["Pat_Id"]));
                                        if (lstConsult.Count > 0)
                                        {
                                            EntityInvoiceDetails entInv = new EntityInvoiceDetails() { DocAllocationId = lstConsult[0].DocAllocId, Amount = Convert.ToDecimal(lstConsult[0].NoOfDays * lstConsult[0].Amount), ChargesName = item.ChargeCategoryName, OtherChargesId = item.ChargesId };
                                            if (DateTime.Now.Date.CompareTo(lstConsult[0].AllocDate.Date) == 0)
                                            {
                                                IPD_Days = 1;
                                            }
                                            else
                                            {
                                                List<EntityBedAllocToPatient> entBedAlloc = new ICUDischargeBilling().GetICUAllocatedBedDetails(Convert.ToInt32(Session["Pat_Id"]));
                                                foreach (var item1 in entBedAlloc)
                                                {
                                                    if (item1.CategoryDesc == "IPD")
                                                    {
                                                        if (item1.ShiftDate == null)
                                                        {
                                                            IPD_Days = IPD_Days + Convert.ToInt32(Convert.ToDateTime(txtBillDate.Text).Date.Subtract(item1.AllocationDate.Value.Date).Days);
                                                            bedCharges = bedCharges + (Convert.ToInt32(Convert.ToDateTime(txtBillDate.Text).Date.Subtract(item1.AllocationDate.Value.Date).Days) * item1.Charges);
                                                            IPD_Days++;
                                                        }
                                                        else
                                                        {
                                                            IPD_Days = IPD_Days + Convert.ToInt32(item1.ShiftDate.Value.Date.Subtract(item1.AllocationDate.Value.Date).Days);
                                                            bedCharges = bedCharges + (Convert.ToInt32(item1.ShiftDate.Value.Date.Subtract(item1.AllocationDate.Value.Date).Days) * item1.Charges);
                                                        }

                                                    }
                                                }
                                                Session["Days"] = IPD_Days;
                                            }
                                            entInv.NoOfDays = IPD_Days;
                                            entInv.Quantity = 1;
                                            entInv.PerDayCharge = Convert.ToDecimal(lstConsult[0].Amount);
                                            entInv.Amount = Convert.ToDecimal(IPD_Days * lstConsult[0].Amount);
                                            entInv.TempId = lst.Count + 1;
                                            entInv.IsDelete = false;
                                            lst.Add(entInv);
                                        }
                                    }
                                    else
                                    {
                                        txtNoofDays.ReadOnly = true;
                                        txtQuantity.ReadOnly = true;
                                        txtAmount.ReadOnly = true;
                                        txtQuantity.Text = "1";
                                        txtNoofDays.Text = "1";
                                        Session["Days"] = 1;
                                        List<EntityPatientInvoice> lstConsult = mobjDeptBLL.GetConsultChargesOPD(Convert.ToInt32(Session["Pat_Id"]));
                                        if (lstConsult.Count > 0)
                                        {
                                            EntityInvoiceDetails entInv = new EntityInvoiceDetails() { DocAllocationId = lstConsult[0].DocAllocId, Amount = Convert.ToDecimal(lstConsult[0].NoOfDays * lstConsult[0].Amount), ChargesName = item.ChargeCategoryName, OtherChargesId = item.ChargesId };
                                            entInv.NoOfDays = 1;
                                            entInv.Quantity = 1;
                                            entInv.PerDayCharge = Convert.ToDecimal(lstConsult[0].Amount);
                                            entInv.Amount = Convert.ToDecimal(lstConsult[0].Amount);
                                            entInv.IsDelete = false;
                                            lst.Add(entInv);
                                        }
                                    }
                                }

                                if (item.IsNursing)
                                {
                                    if (Cate.PatientType == "IPD")
                                    {
                                        List<EntityPatientInvoice> lstRegistration = new List<EntityPatientInvoice>();
                                        if (lstRegistration.Count == 0)
                                        {
                                            EntityInvoiceDetails entInv = new EntityInvoiceDetails()
                                            {
                                                OTBedAllocId = item.ChargesId,
                                                Amount = 100,
                                                NoOfDays = 1,
                                                Quantity = 1,
                                                PerDayCharge = 1 * 100,
                                                ChargesName = item.ChargeCategoryName,
                                                OtherChargesId = item.ChargesId,
                                                TempId = lst.Count + 1,
                                                IsDelete = false,
                                            };
                                            lst.Add(entInv);
                                        }
                                    }
                                }

                                if (item.IsRMO)
                                {
                                    if (Cate.PatientType == "IPD")
                                    {
                                        List<EntityPatientInvoice> lstRegistration = new List<EntityPatientInvoice>();
                                        if (lstRegistration.Count == 0)
                                        {
                                            EntityInvoiceDetails entInv = new EntityInvoiceDetails()
                                            {
                                                OTBedAllocId = item.ChargesId,
                                                Amount = 100,
                                                NoOfDays = 1,
                                                Quantity = 1,
                                                PerDayCharge = 1 * 100,
                                                ChargesName = item.ChargeCategoryName,
                                                OtherChargesId = item.ChargesId,
                                                TempId = lst.Count + 1,
                                                IsDelete = false,
                                            };
                                            lst.Add(entInv);
                                        }
                                    }
                                }

                                if (item.IsOperation)
                                {
                                    List<EntityPatientInvoice> lstOpera = mobjDeptBLL.GetOperaCharges(Convert.ToInt32(Session["Pat_Id"]));
                                    if (lstOpera.Count > 0)
                                    {
                                        foreach (EntityPatientInvoice itemOperation in lstOpera)
                                        {
                                            EntityInvoiceDetails entInv = new EntityInvoiceDetails()
                                            {
                                                OTBedAllocId = itemOperation.OTBedAllocId,
                                                Amount = Convert.ToDecimal(itemOperation.Amount),
                                                NoOfDays = 0,
                                                Quantity = 0,
                                                PerDayCharge = 0,
                                                ChargesName = itemOperation.Description,
                                                OtherChargesId = item.ChargesId,
                                                TempId = lst.Count + 1,
                                                IsDelete = false,
                                            };
                                            lst.Add(entInv);
                                        }
                                    }
                                }
                            }
                            Session["BillDetails"] = lst;
                            GridView1.DataSource = lst;
                            GridView1.DataBind();
                            txtTotal.Text = Convert.ToString(lst.Sum(p => p.Amount));
                            txtNetAmount.Text = Convert.ToString(lst.Sum(p => p.Amount));
                            //Calculation();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void dgvTestParameter_DataBound(object sender, EventArgs e)
        {
            int lintCurrentPage = dgvTestParameter.PageIndex + 1;
            lblPageCount.Text = "<b>Page</b> " + lintCurrentPage.ToString() + "<b> of </b>" + dgvTestParameter.PageCount.ToString();
        }

        protected void dgvTestParameter_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dgvTestParameter.DataSource = (List<STP_GetPatientInvoiceResult>)Session["RoomDetails"];
                dgvTestParameter.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void dgvTestParameter_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvTestParameter.PageIndex = e.NewPageIndex;
        }


        protected void btnAddCharge_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlPatient.SelectedIndex > 0)
                {
                    List<EntityInvoiceDetails> lst = (List<EntityInvoiceDetails>)Session["BillDetails"];
                    if (Convert.ToString(Session["PatientType"]).Equals("OPD", StringComparison.CurrentCultureIgnoreCase))
                    {
                        lst.Add(
                        new EntityInvoiceDetails()
                        {
                            OtherId = Convert.ToInt32(ddlOther.SelectedValue),
                            Amount = Convert.ToDecimal(txtAmount.Text),
                            ChargesName = ddlOther.SelectedItem.Text,
                            Remarks = Convert.ToString(txtRemarks.Text),
                            OtherChargesId = Convert.ToInt32(ddlOther.SelectedValue),
                            PerDayCharge = Convert.ToDecimal(txtChargePerDay.Text),
                            NoOfDays = string.IsNullOrEmpty(txtNoofDays.Text) == false ? Convert.ToInt32(txtNoofDays.Text) : 1,
                            Quantity = string.IsNullOrEmpty(txtQuantity.Text) == false ? Convert.ToInt32(txtQuantity.Text) : 1,
                            TempId = lst.Count + 1
                        });
                    }
                    else
                    {
                        lst.Add(
                        new EntityInvoiceDetails()
                        {
                            OtherId = Convert.ToInt32(ddlOther.SelectedValue),
                            Amount = Convert.ToDecimal(txtAmount.Text),
                            ChargesName = ddlOther.SelectedItem.Text,
                            Remarks = Convert.ToString(txtRemarks.Text),
                            OtherChargesId = Convert.ToInt32(ddlOther.SelectedValue),
                            PerDayCharge = Convert.ToDecimal(txtChargePerDay.Text),
                            NoOfDays = string.IsNullOrEmpty(txtNoofDays.Text) == false ? Convert.ToInt32(txtNoofDays.Text) : 1,
                            Quantity = string.IsNullOrEmpty(txtQuantity.Text) == false ? Convert.ToInt32(txtQuantity.Text) : 1,
                            TempId = lst.Count + 1,
                        });
                    }

                    Session["BillDetails"] = lst;
                    GridView1.DataSource = lst.Where(p => p.IsDelete == false).ToList();
                    GridView1.DataBind();
                    txtTotal.Text = Convert.ToString(lst.Where(p => p.IsDelete == false).Sum(p => p.Amount));
                    txtNetAmount.Text = Convert.ToString(lst.Where(p => p.IsDelete == false).Sum(p => p.Amount));
                    //Calculation();
                    ClearOther();
                }
                //}
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        private void ClearOther()
        {
            txtAmount.Text = Convert.ToString(0);
            txtChargePerDay.Text = string.Empty;
            txtQuantity.Text = Convert.ToString(1);
            txtNoofDays.Text = Convert.ToString(1);
            txtRemarks.Text = string.Empty;
            txtAmount.Enabled = false;
            ddlOther.SelectedIndex = 0;
        }

        private void Calculation()
        {
            
        }


        protected void btnCancel_Click(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;
            ClearOther();
        }

        protected void ddlOther_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlOther.SelectedIndex > 0)
            {
                Session["OtherCharge_ID"] = ddlOther.SelectedValue;
                EntityPatientInvoice patInv = mobjDeptBLL.GetChargesForCate(Convert.ToInt32(ddlOther.SelectedValue));
                txtChargePerDay.Text = Convert.ToString(patInv.Amount);
                txtAmount.Enabled = true;
                txtAmount.Text = Convert.ToString(patInv.Amount);
                txtNoofDays.Text = Convert.ToString(1);
            }
            else
            {
                ClearOther();
            }
            //Calculation();
        }

        
        protected void ddlPatient_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlPatient.SelectedIndex > 0)
                {
                    Session["Pat_Id"] = ddlPatient.SelectedValue;

                    CustomerTransactionBLL mobjCustBLL = new CustomerTransactionBLL();
                    //txtReceivedAmount.Text = Convert.ToString(mobjCustBLL.GetPatientTotalReceivedAmount(Convert.ToInt32(ddlPatient.SelectedValue)));
                    txtBalance.Text = Convert.ToString(mobjCustBLL.GetPatientBalance(Convert.ToInt32(ddlPatient.SelectedValue)));
                    txtTotalAdvance.Text = Convert.ToString(mobjCustBLL.GetPatientTotalAdvance(Convert.ToInt32(ddlPatient.SelectedValue)));
                    txtRefund.Text = Convert.ToString(mobjCustBLL.GetPatientRefund(Convert.ToInt32(ddlPatient.SelectedValue)));

                    List<EntityChargeCategory> lstCat = new ChargeCategoryBLL().GetChargeDetail();
                    List<EntityInvoiceDetails> lst = new List<EntityInvoiceDetails>();

                    if (ddlBillType.SelectedItem.Text == "Intermediate")
                    {
                        Label4.Visible = false;
                        txtPatientType.Visible = false;
                    }
                    else
                    {
                        Label4.Visible = true;
                        txtPatientType.Visible = true;
                        EntityPatientMaster Cate = mobjDeptBLL.GetPatientCate(Convert.ToInt32(Session["Pat_Id"]));
                        EntityPatientAlloc objTxt = new PatientAllocDocBLL().GetPatientType(Convert.ToInt32(ddlPatient.SelectedValue));
                        //CalBillDate.StartDate = objTxt.AdmitDate;
                        Session["PatientType"] = Cate.PatientType;
                        txtPatientType.Text = Convert.ToString(Cate.PatientType);
                        if (Cate.PatientType == "IPD")
                        {
                            lblWard.Visible = true;
                            txtWard.Visible = true;
                            EntityPatientInvoice ward = mobjDeptBLL.GetWardName(Convert.ToInt32(Session["Pat_Id"]));
                            if (ward != null)
                            {
                                txtWard.Text = Convert.ToString(ward.Description);
                            }
                            else
                            {
                                lblWard.Visible = false;
                                txtWard.Visible = false;
                            }
                        }

                        foreach (EntityChargeCategory item in lstCat)
                        {
                            int IPD_Days = 0;
                            if (item.IsBed)
                            {
                                List<EntityPatientInvoice> lstBed = mobjDeptBLL.GetOTBedCharges(Convert.ToInt32(Session["Pat_Id"]));
                                if (lstBed.Count > 0)
                                {
                                    decimal bedCharges = 0;

                                    if (DateTime.Now.Date.CompareTo(lstBed[0].AllocDate.Date) == 0 && lstBed[0].ShiftDate == null)
                                    {

                                        EntityInvoiceDetails entInv = new EntityInvoiceDetails() { BedAllocId = lstBed[0].BedAllocId, ChargesName = item.ChargeCategoryName + ' ' + lstBed[0].Description, OtherChargesId = item.ChargesId };
                                        IPD_Days = 1;
                                        entInv.NoOfDays = IPD_Days;
                                        entInv.Quantity = 1;
                                        entInv.Remarks = string.Empty;
                                        entInv.PerDayCharge = Convert.ToDecimal(lstBed[0].Amount);
                                        entInv.Amount = Convert.ToDecimal(entInv.PerDayCharge * entInv.NoOfDays * entInv.Quantity);
                                        entInv.TempId = lst.Count + 1;
                                        entInv.IsDelete = false;
                                        lst.Add(entInv);
                                    }
                                    else
                                    {
                                        List<EntityBedAllocToPatient> entBedAlloc = new ICUDischargeBilling().GetICUAllocatedBedDetails(Convert.ToInt32(Session["Pat_Id"]));
                                        foreach (var item1 in entBedAlloc)
                                        {
                                            if (item1.CategoryDesc == "IPD")
                                            {
                                                EntityInvoiceDetails entInv = new EntityInvoiceDetails() { BedAllocId = item1.BedAllocId, OtherChargesId = item.ChargesId };
                                                if (item1.ShiftDate == null)
                                                {
                                                    entInv.ChargesName = item.ChargeCategoryName + ' ' + item1.BedNo;
                                                    IPD_Days = IPD_Days + Convert.ToInt32(DateTime.Now.Date.Subtract(item1.AllocationDate.Value.Date).Days);
                                                    bedCharges = bedCharges + ((Convert.ToInt32(DateTime.Now.Date.Subtract(item1.AllocationDate.Value.Date).Days) + 1) * item1.Charges);
                                                    IPD_Days++;
                                                    entInv.NoOfDays = Convert.ToInt32(DateTime.Now.Date.Subtract(item1.AllocationDate.Value.Date).Days) + 1;
                                                    entInv.Quantity = 1;
                                                    entInv.Remarks = string.Empty;
                                                    entInv.PerDayCharge = Convert.ToDecimal(item1.Charges);
                                                    entInv.Amount = ((Convert.ToInt32(DateTime.Now.Date.Subtract(item1.AllocationDate.Value.Date).Days) + 1) * item1.Charges);
                                                    entInv.TempId = lst.Count + 1;
                                                    entInv.IsDelete = false;
                                                    lst.Add(entInv);
                                                }
                                                else
                                                {
                                                    entInv.ChargesName = item.ChargeCategoryName + ' ' + item1.BedNo;
                                                    IPD_Days = IPD_Days + Convert.ToInt32(item1.ShiftDate.Value.Date.Subtract(item1.AllocationDate.Value.Date).Days);
                                                    bedCharges = bedCharges + (Convert.ToInt32(item1.ShiftDate.Value.Date.Subtract(item1.AllocationDate.Value.Date).Days) * item1.Charges);
                                                    entInv.NoOfDays = Convert.ToInt32(item1.ShiftDate.Value.Date.Subtract(item1.AllocationDate.Value.Date).Days);
                                                    entInv.PerDayCharge = Convert.ToDecimal(item1.Charges);
                                                    entInv.Quantity = 1;
                                                    entInv.Remarks = string.Empty;
                                                    entInv.Amount = (Convert.ToInt32(item1.ShiftDate.Value.Date.Subtract(item1.AllocationDate.Value.Date).Days) * item1.Charges);
                                                    entInv.TempId = lst.Count + 1;
                                                    entInv.IsDelete = false;
                                                    lst.Add(entInv);
                                                }

                                            }
                                        }
                                    }
                                }
                            }

                            if (item.IsRMO)
                            {
                                if (Cate.PatientType == "IPD")
                                {
                                    List<EntityPatientInvoice> lstRegistration = new List<EntityPatientInvoice>();
                                    if (lstRegistration.Count == 0)
                                    {
                                        EntityInvoiceDetails entInv = new EntityInvoiceDetails()
                                        {
                                            OTBedAllocId = item.ChargesId,
                                            Amount = 100,
                                            NoOfDays = 1,
                                            Quantity = 1,
                                            Remarks = string.Empty,
                                            PerDayCharge = 1 * 100,
                                            ChargesName = item.ChargeCategoryName,
                                            OtherChargesId = item.ChargesId,
                                            TempId = lst.Count + 1,
                                            IsDelete = false,
                                        };
                                        lst.Add(entInv);
                                    }
                                }
                            }

                            if (item.IsNursing)
                            {
                                if (Cate.PatientType == "IPD")
                                {
                                    List<EntityPatientInvoice> lstRegistration = new List<EntityPatientInvoice>();
                                    if (lstRegistration.Count == 0)
                                    {
                                        EntityInvoiceDetails entInv = new EntityInvoiceDetails()
                                        {
                                            OTBedAllocId = item.ChargesId,
                                            Amount = 100,
                                            NoOfDays = 1,
                                            Quantity = 1,
                                            Remarks = string.Empty,
                                            PerDayCharge = 1 * 100,
                                            ChargesName = item.ChargeCategoryName,
                                            OtherChargesId = item.ChargesId,
                                            TempId = lst.Count + 1,
                                            IsDelete = false,
                                        };
                                        lst.Add(entInv);
                                    }
                                }
                            }

                            if (item.IsConsulting)
                            {
                                if (Cate.PatientType == "IPD")
                                {
                                    //List<EntityPatientInvoice> lstConsult = mobjDeptBLL.GetConsultChargesIPD(Convert.ToInt32(Session["Pat_Id"]));
                                    List<EntityPatientInvoice> lstConsult = mobjDeptBLL.GetConsultChargesOPD(Convert.ToInt32(Session["Pat_Id"]));
                                    if (lstConsult.Count > 0)
                                    {
                                        IPD_Days = 0;
                                        EntityInvoiceDetails entInv = new EntityInvoiceDetails() { DocAllocationId = lstConsult[0].DocAllocId, Amount = Convert.ToDecimal(lstConsult[0].NoOfDays * lstConsult[0].Amount), ChargesName = item.ChargeCategoryName, OtherChargesId = item.ChargesId };
                                        if (DateTime.Now.Date.CompareTo(lstConsult[0].AllocDate.Date) == 0)
                                        {
                                            IPD_Days = 1;
                                        }
                                        else
                                        {
                                            List<EntityBedAllocToPatient> entBedAlloc = new ICUDischargeBilling().GetICUAllocatedBedDetails(Convert.ToInt32(Session["Pat_Id"]));
                                            foreach (var item1 in entBedAlloc)
                                            {
                                                if (item1.CategoryDesc == "IPD")
                                                {
                                                    if (item1.ShiftDate == null)
                                                    {
                                                        IPD_Days = IPD_Days + Convert.ToInt32(DateTime.Now.Date.Subtract(item1.AllocationDate.Value.Date).Days);
                                                        IPD_Days++;
                                                    }
                                                    else
                                                    {
                                                        IPD_Days = IPD_Days + Convert.ToInt32(item1.ShiftDate.Value.Date.Subtract(item1.AllocationDate.Value.Date).Days);
                                                    }

                                                }
                                            }
                                            Session["Days"] = IPD_Days;
                                        }
                                        if (IPD_Days > 0)
                                        {
                                            entInv.NoOfDays = IPD_Days;
                                        }
                                        else
                                        {
                                            IPD_Days = 1;
                                            entInv.NoOfDays = IPD_Days;
                                        }
                                        entInv.Quantity = 1;
                                        entInv.Remarks = string.Empty;
                                        entInv.PerDayCharge = Convert.ToDecimal(lstConsult[0].Amount);
                                        entInv.Amount = Convert.ToDecimal(IPD_Days * lstConsult[0].Amount);
                                        entInv.TempId = lst.Count + 1;
                                        entInv.IsDelete = false;
                                        lst.Add(entInv);
                                    }
                                }
                                else
                                {
                                    txtNoofDays.ReadOnly = false;
                                    txtQuantity.ReadOnly = false;
                                    txtAmount.ReadOnly = false;
                                    txtNoofDays.Text = "1";
                                    txtRemarks.Text = string.Empty;
                                    txtQuantity.Text = "1";
                                    Session["Days"] = 1;
                                    List<EntityPatientInvoice> lstConsult = mobjDeptBLL.GetConsultChargesOPD(Convert.ToInt32(Session["Pat_Id"]));
                                    if (lstConsult.Count > 0)
                                    {
                                        EntityInvoiceDetails entInv = new EntityInvoiceDetails() { DocAllocationId = lstConsult[0].DocAllocId, Amount = Convert.ToDecimal(lstConsult[0].NoOfDays * lstConsult[0].Amount), ChargesName = item.ChargeCategoryName, OtherChargesId = item.ChargesId };
                                        entInv.NoOfDays = 1;
                                        entInv.Quantity = 1;
                                        entInv.IsDelete = false;
                                        entInv.Remarks = string.Empty;
                                        entInv.PerDayCharge = Convert.ToDecimal(lstConsult[0].Amount);
                                        entInv.Amount = Convert.ToDecimal(lstConsult[0].Amount);
                                        entInv.TempId = lst.Count + 1;
                                        lst.Add(entInv);
                                    }
                                }
                            }
                            if (item.IsOperation)
                            {
                                List<EntityPatientInvoice> lstOpera = mobjDeptBLL.GetOperaCharges(Convert.ToInt32(Session["Pat_Id"]));
                                if (lstOpera.Count > 0)
                                {
                                    foreach (EntityPatientInvoice itemOperation in lstOpera)
                                    {
                                        EntityInvoiceDetails entInv = new EntityInvoiceDetails()
                                        {
                                            OTBedAllocId = itemOperation.OTBedAllocId,
                                            Amount = Convert.ToDecimal(itemOperation.Amount),
                                            NoOfDays = 0,
                                            Quantity = 0,
                                            PerDayCharge = 0,
                                            Remarks = string.Empty,
                                            ChargesName = itemOperation.Description,
                                            OtherChargesId = item.ChargesId,
                                            TempId = lst.Count + 1,
                                            IsDelete = false,
                                        };
                                        lst.Add(entInv);
                                    }
                                }
                            }
                        }

                        Session["BillDetails"] = lst;
                        GridView1.DataSource = lst;
                        GridView1.DataBind();
                        txtTotal.Text = Convert.ToString(lst.Sum(p => p.Amount));
                        txtNetAmount.Text = Convert.ToString(lst.Sum(p => p.Amount));
                        //Calculation();
                    }
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }


        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                //e.Row.Enabled = false;
                if (!e.Row.Cells[0].Text.Equals("Charge Id", StringComparison.CurrentCultureIgnoreCase))
                {
                    
                }
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap; text-align:left;");
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
            Session["BILLNo"] = Convert.ToInt32(dgvTestParameter.DataKeys[row.RowIndex].Value);
            Session["ReportType"] = "Invoice";
            Response.Redirect("~/PathalogyReport/PathologyReport.aspx", false);
        }
    }
}