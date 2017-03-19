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

namespace MainProjectHos
{
    public partial class frmOTMedicineBill : System.Web.UI.Page
    {
        OTMedicineBillBLL MobjClaim = new OTMedicineBillBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] != null)
            {
                EntityLogin entLogin = (EntityLogin)Session["user"];

                List<EntityFormMaster> entFormAcc = (List<EntityFormMaster>)Session["AccessibleForm"];
                int Access = (from tbl in entFormAcc
                              where tbl.FormName.Contains("frmOTMedicineBill")
                              select tbl).Count();

                if (Access > 0)
                {
                    if (!Page.IsPostBack)
                    {
                        BindTablet();
                        BindPatientList();
                        Session["Myflag"] = string.Empty;
                        BindPrescription();
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

        public void BindTablet()
        {
            try
            {
                DataTable tblDesig = new OTMedicineBillBLL().GetTablet();
                DataRow dr = tblDesig.NewRow();
                dr["PKId"] = 0;
                dr["MedicineName"] = "---Select---";
                tblDesig.Rows.InsertAt(dr, 0);

                ddlTablet.DataSource = tblDesig;
                ddlTablet.DataValueField = "PKId";
                ddlTablet.DataTextField = "MedicineName";
                ddlTablet.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
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

        protected void ddlPatient_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPatient.SelectedIndex > 0)
            {
                EntityPatientAlloc objTxt = new PatientAllocDocBLL().GetPatientType(Convert.ToInt32(ddlPatient.SelectedValue));
                //CalDate.StartDate = objTxt.AdmitDate;
            }
        }

        public void Clear()
        {
            ddlTablet.SelectedIndex = 0;
            txtQuantity.Text = string.Empty;
            txtPrice.Text = string.Empty;
        }

        public void BindData()
        {
            List<EntityOTMedicineBillDetails> lst = MobjClaim.GetPrescription(Convert.ToInt32(ddlPatient.SelectedValue));
            dgvChargeDetails.DataSource = lst;
            Session["Charges"] = lst;
            dgvChargeDetails.DataBind();
        }

        protected void BtnEditCharge_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton imgEdit = (ImageButton)sender;
                GridViewRow row = (GridViewRow)imgEdit.NamingContainer;
                if (row != null)
                {
                    btnUpdatecharge.Visible = true;
                    btnAdd.Visible = false;
                    if (Session["MyFlag"] == "Addnew")
                    {
                        Session["TempId"] = Convert.ToInt32(dgvChargeDetails.DataKeys[row.RowIndex].Value);
                        ListItem itemProduct = ddlTablet.Items.FindByText(row.Cells[0].Text);
                        ddlTablet.SelectedValue = itemProduct.Value;
                        txtQuantity.Text = row.Cells[1].Text;
                        txtPrice.Text = row.Cells[2].Text;

                    }
                    else
                    {
                        Session["TempId"] = Convert.ToInt32(dgvChargeDetails.DataKeys[row.RowIndex].Value);
                        ListItem itemProduct = ddlTablet.Items.FindByText(row.Cells[0].Text);
                        ddlTablet.SelectedValue = itemProduct.Value;
                        txtQuantity.Text = row.Cells[1].Text;
                        txtPrice.Text = row.Cells[2].Text;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ImageButton imgDelete = (ImageButton)sender;
            GridViewRow row = (GridViewRow)imgDelete.NamingContainer;
            List<EntityOTMedicineBillDetails> lst = (List<EntityOTMedicineBillDetails>)Session["Prescript"];
            List<EntityOTMedicineBillDetails> lstFinal = new List<EntityOTMedicineBillDetails>();
            if (BtnSave.Visible)
            {
                if (lst != null)
                {
                    foreach (EntityOTMedicineBillDetails item in lst)
                    {
                        if (item.TempId != Convert.ToInt32(dgvChargeDetails.DataKeys[row.RowIndex].Value))
                        {
                            lstFinal.Add(item);
                        }
                    }
                    dgvChargeDetails.DataSource = lstFinal;
                    dgvChargeDetails.DataBind();
                    Session["Prescript"] = lst;
                }
            }
            else
            {
                foreach (EntityOTMedicineBillDetails item in lst)
                {
                    if (item.TempId == Convert.ToInt32(dgvChargeDetails.DataKeys[row.RowIndex].Value))
                    {
                        item.IsDelete = true;
                    }
                }
                dgvChargeDetails.DataSource = lst.Where(p => p.IsDelete == false).ToList();
                dgvChargeDetails.DataBind();
                Session["Prescript"] = lst;
            }
        }

        protected void btnAddCharge_Click(object sender, EventArgs e)
        {
            try
            {
                List<EntityOTMedicineBillDetails> lst = null;
                if (Convert.ToString(Session["MyFlag"]).Equals("Addnew", StringComparison.CurrentCultureIgnoreCase))
                {
                    lst = (List<EntityOTMedicineBillDetails>)Session["Charges"];
                }
                else
                {
                    lst = (List<EntityOTMedicineBillDetails>)Session["Prescript"];
                }
                lst.Add(new EntityOTMedicineBillDetails
                {
                    TabletId = Convert.ToInt32(ddlTablet.SelectedValue),
                    MedicineName = ddlTablet.SelectedItem.Text,
                    Price = Convert.ToDecimal(txtPrice.Text),
                    Quantity = Convert.ToInt32(txtQuantity.Text),
                    Amount = Convert.ToDecimal(Convert.ToDecimal(txtPrice.Text) * Convert.ToInt32(txtQuantity.Text)),
                    TempId = lst.Count + 1
                });

                dgvChargeDetails.DataSource = lst;
                dgvChargeDetails.DataBind();
                txtTotal.Text = Convert.ToString(lst.Sum(p => p.Amount));
                Session["Unit"] = lst;
                Clear();
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ddlTablet.SelectedIndex = 0;
            txtPrice.Text = string.Empty;
            txtQuantity.Text = string.Empty;
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            tblOTMedicineBill tblins = new tblOTMedicineBill();
            tblins.AdmitId = Convert.ToInt32(ddlPatient.SelectedValue);
            tblins.Bill_Date = Convert.ToDateTime(txtBillDate.Text);
            tblins.TotalAmount = Convert.ToDecimal(txtTotal.Text);
            tblPatientAdmitDetail objFac = MobjClaim.GetEmployee(Convert.ToInt32(ddlPatient.SelectedValue));
            if (objFac != null)
            {
                List<EntityOTMedicineBillDetails> inslst = (List<EntityOTMedicineBillDetails>)Session["Charges"];
                int ClaimId = Convert.ToInt32(MobjClaim.Save(tblins, inslst));
                lblMessage.Text = "Record Saved Successfully.....";
                Session["Charges"] = null;
                Clear();
                inslst = new List<EntityOTMedicineBillDetails>();
                dgvChargeDetails.DataSource = inslst;
                dgvChargeDetails.DataBind();
                lblMsg.Text = string.Empty;
            }
            else
            {
                lblMsg.Text = "Invalid Patient";
            }
            Session["Charges"] = new List<EntityOTMedicineBillDetails>();
            BindPrescription();
            MultiView1.SetActiveView(View1);
        }
        public void BindPrescription()
        {
            List<EntityOTMedicineBill> lst = MobjClaim.GetInsurance();
            Session["PrescriptDetails"] = lst;
            dgvClaim.DataSource = lst;
            int lintRowcount = lst.Count();
            lblRowCount.Text = "<b>Total Records:</b> " + lintRowcount.ToString();
            dgvClaim.DataBind();
        }

        public void BindPrescription(int Id)
        {
            try
            {
                List<EntityOTMedicineBillDetails> lst = MobjClaim.GetPrescription(Id);
                //Session["DetailID"] = lst[0].TempId;
                dgvChargeDetails.DataSource = lst;
                dgvChargeDetails.DataBind();
                Session["Prescript"] = lst;
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
                tblOTMedicineBill tblins = new tblOTMedicineBill();
                tblins.BillNo = Convert.ToInt32(Session["PrescriptionId"]);
                tblins.AdmitId = Convert.ToInt32(ddlPatient.SelectedValue);
                tblins.Bill_Date = Convert.ToDateTime(txtBillDate.Text);
                tblins.TotalAmount = Convert.ToDecimal(txtTotal.Text);
                List<EntityOTMedicineBillDetails> inslst = (List<EntityOTMedicineBillDetails>)Session["Prescript"];
                MobjClaim.Update(tblins, inslst);
                lblMessage.Text = "Record Updated Successfully.....";
                Clear();
                List<EntityOTMedicineBillDetails> lst = new List<EntityOTMedicineBillDetails>();
                dgvChargeDetails.DataSource = lst;
                dgvChargeDetails.DataBind();
                BindPrescription();
                Session["Charges"] = null;
                MultiView1.SetActiveView(View1);
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void btnUpdatecharge_Click(object sender, EventArgs e)
        {
            if (Session["MyFlag"].ToString() == "Addnew")
            {
                List<EntityOTMedicineBillDetails> lst = (List<EntityOTMedicineBillDetails>)Session["Charges"];
                foreach (EntityOTMedicineBillDetails item in lst)
                {
                    if (Convert.ToInt32(Session["TempId"]) == item.TempId)
                    {
                        item.TabletId = Convert.ToInt32(ddlTablet.SelectedValue);
                        item.Price = Convert.ToDecimal(txtPrice.Text);
                        item.Quantity = Convert.ToInt32(txtQuantity.Text);
                        item.Amount = Convert.ToDecimal(txtPrice.Text) * Convert.ToInt32(txtQuantity.Text);
                        item.IsDelete = false;
                    }
                    else
                    {
                        if (item.IsDelete.Value)
                        {
                            lst.Add(new EntityOTMedicineBillDetails()
                            {
                                BillDetailId = item.BillDetailId,
                                BillNo = item.BillNo,
                                IsDelete = item.IsDelete,
                                TabletId = item.TabletId,
                                Quantity = item.Quantity,
                                Price = item.Price,
                                Amount = Convert.ToDecimal(item.Quantity * item.Price)
                            });
                        }
                    }
                }


                dgvChargeDetails.DataSource = lst;
                dgvChargeDetails.DataBind();
                txtTotal.Text = Convert.ToString(lst.Where(p => p.IsDelete == false).ToList().Sum(p => p.Amount));
                Clear();
                btnUpdatecharge.Visible = false;
                btnAdd.Visible = true;
            }
            else
            {
                List<EntityOTMedicineBillDetails> lst = (List<EntityOTMedicineBillDetails>)Session["Prescript"];
                foreach (EntityOTMedicineBillDetails item in lst)
                {
                    if (Convert.ToInt32(Session["TempId"]) == item.TempId)
                    {
                        item.TabletId = Convert.ToInt32(ddlTablet.SelectedValue);
                        item.Price = Convert.ToDecimal(txtPrice.Text);
                        item.Quantity = Convert.ToInt32(txtQuantity.Text);
                        item.Amount = Convert.ToDecimal(txtPrice.Text) * Convert.ToInt32(txtQuantity.Text);
                        item.IsDelete = false;
                    }
                    else
                    {
                        if (item.IsDelete.Value)
                        {
                            lst.Add(new EntityOTMedicineBillDetails()
                            {
                                BillDetailId = item.BillDetailId,
                                BillNo = item.BillNo,
                                IsDelete = item.IsDelete,
                                TabletId = item.TabletId,
                                Quantity = item.Quantity,
                                Price = item.Price,
                                Amount = Convert.ToDecimal(item.Quantity * item.Price)
                            });
                        }
                    }
                }


                dgvChargeDetails.DataSource = lst;
                dgvChargeDetails.DataBind();
                txtTotal.Text = Convert.ToString(lst.Where(p => p.IsDelete == false).ToList().Sum(p => p.Amount));
                Clear();
                btnUpdatecharge.Visible = false;
                btnAdd.Visible = true;
            }
        }

        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            btnAdd.Visible = true;
            btnUpdatecharge.Visible = false;
            BtnSave.Visible = false;
            btnUpdate.Visible = true;
            Session["MyFlag"] = "Edit";
            ImageButton imgEdit = (ImageButton)sender;
            GridViewRow row = (GridViewRow)imgEdit.NamingContainer;
            Session["PrescriptionId"] = Convert.ToInt32(dgvClaim.DataKeys[row.RowIndex].Value);
            ListItem item = ddlPatient.Items.FindByText(Convert.ToString(row.Cells[1].Text));
            if (item != null)
            {
                ddlPatient.SelectedValue = item.Value;
                DateTime MDate = Convert.ToDateTime(row.Cells[2].Text);
                txtBillDate.Text = string.Format("{0:MM/dd/yyyy}", MDate);
                txtTotal.Text = Convert.ToString(row.Cells[3].Text);
                tblOTMedicineBill objPresc = MobjClaim.GetPrescriptionInfo(Convert.ToInt32(Session["PrescriptionId"]));
                BindPrescription(Convert.ToInt32(Session["PrescriptionId"]));
                //InjectionPara(false);
                MultiView1.SetActiveView(View2);
            }
            else
            {
                lblMessage.Text = "Patient Already Discharged";
            }
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
                    List<EntityOTMedicineBill> lst = MobjClaim.GetInsurance(txtSearch.Text);
                    if (lst.Count > 0)
                    {
                        dgvClaim.DataSource = lst;
                        dgvClaim.DataBind();
                        Session["PrescriptDetails"] = lst;
                        int lintRowcount = lst.Count;
                        lblRowCount.Text = "<b>Total Records:</b> " + lintRowcount.ToString();
                    }
                    else
                    {
                        lblMessage.Text = "No Record Found";
                    }
                }
                else
                {
                    lblMessage.Text = "Please Enter Content To Search";
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                BindPrescription();
                txtSearch.Text = string.Empty;
                lblMessage.Text = string.Empty;
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
                ListConverter.ToDataTable((List<EntityOTMedicineBill>)Session["PrescriptDetails"]);
                Session["Details"] = ListConverter.ToDataTable((List<EntityOTMedicineBill>)Session["PrescriptDetails"]);
                Response.Redirect("~/ExcelReport/MonthwiseSalExcel.aspx");
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void BtnAddNewPrescription_Click(object sender, EventArgs e)
        {
            Session["MyFlag"] = "Addnew";
            ddlPatient.Enabled = true;
            ddlPatient.SelectedIndex = 0;
            txtBillDate.Text = string.Empty;
            BtnSave.Visible = true;
            btnUpdate.Visible = false;
            btnAdd.Visible = true;
            btnUpdatecharge.Visible = false;
            List<EntityOTMedicineBillDetails> lst = new List<EntityOTMedicineBillDetails>();
            dgvChargeDetails.DataSource = lst;
            dgvChargeDetails.DataBind();
            Session["Charges"] = lst;
            MultiView1.SetActiveView(View2);
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton imgEdit = (ImageButton)sender;
                GridViewRow cnt = (GridViewRow)imgEdit.NamingContainer;
                Session["BillNo"] = cnt.Cells[0].Text;
                int ID_Issue = Convert.ToInt32(Session["BillNo"]);
                if (ID_Issue > 0)
                {
                    Session["ReportType"] = "OTPatientMedicineBill";
                    Response.Redirect("~/PathalogyReport/PathologyReport.aspx", false);
                }
                else
                {
                    //lblMessage.Text = "This Patient Don't Have Invoice";
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
                dgvClaim.DataSource = (List<EntityOTMedicineBill>)Session["PrescriptDetails"];
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
        protected void dgvChargeDetails_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}