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
    public partial class frmSupplierMaster : System.Web.UI.Page
    {
        SupplierBLL mobjSupplierBLL = new SupplierBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] != null)
            {
                EntityLogin entLogin = (EntityLogin)Session["user"];

                List<EntityFormMaster> entFormAcc = (List<EntityFormMaster>)Session["AccessibleForm"];
                int Access = (from tbl in entFormAcc
                              where tbl.FormName.Contains("frmSupplierMaster")
                              select tbl).Count();

                if (Access > 0)
                {
                    if (!Page.IsPostBack)
                    {
                        GetSupplier();
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

        protected void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                txtSupplierCode.Text = String.Empty;
                txtSupplierName.Text = String.Empty;
                txtAddress.Text = String.Empty;
                txtPhoneNo.Text = String.Empty;
                txtMobileNo.Text = String.Empty;
                txtVATCSTNo.Text = String.Empty;
                txtExciseNo.Text = String.Empty;
                txtEmail.Text = String.Empty;
                txtServiceTaxNo.Text = String.Empty;
                MultiView1.SetActiveView(View1);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton imgEdit = (ImageButton)sender;
                GridViewRow cnt = (GridViewRow)imgEdit.NamingContainer;
                txtSupplierCode.Text = Convert.ToString(cnt.Cells[0].Text);
                txtSupplierName.Text = Convert.ToString(cnt.Cells[1].Text);
                txtAddress.Text = Convert.ToString(cnt.Cells[2].Text);
                txtPhoneNo.Text = Convert.ToString(cnt.Cells[3].Text);
                txtMobileNo.Text = Convert.ToString(cnt.Cells[4].Text);
                txtVATCSTNo.Text = Convert.ToString(cnt.Cells[5].Text);
                txtExciseNo.Text = Convert.ToString(cnt.Cells[6].Text);
                txtEmail.Text = Convert.ToString(cnt.Cells[7].Text);
                txtServiceTaxNo.Text = Convert.ToString(cnt.Cells[8].Text);
                BtnSave.Visible = false;
                btnUpdate.Visible = true;
                MultiView1.SetActiveView(View2);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void BtnAddNewSupplier_Click(object sender, EventArgs e)
        {
            lblMsg.Text = string.Empty;
            DataTable ldt = new DataTable();
            ldt = mobjSupplierBLL.GetNewSupplierCode();
            txtSupplierCode.Text = ldt.Rows[0]["SupplierCode"].ToString();
            txtSupplierName.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtPhoneNo.Text = string.Empty;
            txtMobileNo.Text = string.Empty;
            txtVATCSTNo.Text = string.Empty;
            txtExciseNo.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtServiceTaxNo.Text = string.Empty;
            BtnSave.Visible = true;
            btnUpdate.Visible = false;
            MultiView1.SetActiveView(View2);
            //this.programmaticModalPopup.Show();

        }
        public void GetSupplier()
        {
            List<EntitySupplierMaster> ldtSupplier = mobjSupplierBLL.GetAllSupplier();
            if (ldtSupplier.Count > 0 && ldtSupplier != null)
            {
                dgvSupplier.DataSource = ldtSupplier;
                dgvSupplier.DataBind();
                Session["SupplierDetail"] = ldtSupplier;
                int lintRowcount = ldtSupplier.Count;
                lblRowCount.Text = "<b>Total Records:</b> " + lintRowcount.ToString();
            }
        }

        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            int lintCnt = 0;
            try
            {
                EntitySupplier entSupplier = new EntitySupplier();

                entSupplier.SupplierCode = txtSupplierCode.Text;
                entSupplier.SupplierName = txtSupplierName.Text;
                entSupplier.Address = txtAddress.Text;
                entSupplier.PhoneNo = txtPhoneNo.Text;
                entSupplier.MobileNo = txtMobileNo.Text;
                entSupplier.VATCSTNo = txtVATCSTNo.Text;
                entSupplier.ExciseNo = txtExciseNo.Text;
                entSupplier.Email = txtEmail.Text;
                entSupplier.ServiceTaxNo = txtServiceTaxNo.Text;
                entSupplier.ChangeBy = SessionWrapper.UserName;
                if (mobjSupplierBLL.GetSupplier(entSupplier) != null)
                {
                    lintCnt = mobjSupplierBLL.UpdateSupplier(entSupplier);
                    if (lintCnt > 0)
                    {
                        GetSupplier();
                        lblMessage.Text = "Record Updated Successfully";
                    }
                    else
                    {
                        lblMessage.Text = "Record Not Updated";
                    }
                }
                else
                {
                    lblMessage.Text = "Record Already Exist...";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
            MultiView1.SetActiveView(View1);
        }

        protected void dgvSupplier_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvSupplier.PageIndex = e.NewPageIndex;
        }

        protected void dgvSupplier_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dgvSupplier.DataSource = (List<EntitySupplierMaster>)Session["SupplierDetail"];
                dgvSupplier.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            int lintcnt = 0;
            EntitySupplier entSupplier = new EntitySupplier();
            if (string.IsNullOrEmpty(txtSupplierCode.Text.Trim()))
            {
                lblMsg.Text = "Please Enter Discount Code";
            }
            else
            {
                if (string.IsNullOrEmpty(txtSupplierName.Text.Trim()))
                {
                    lblMsg.Text = "Please Enter Supplier Name";
                    txtSupplierName.Focus();
                    return;
                }
                else
                {
                    entSupplier.SupplierCode = txtSupplierCode.Text.Trim();
                    entSupplier.SupplierName = txtSupplierName.Text.Trim();
                    entSupplier.Address = txtAddress.Text.Trim();
                    entSupplier.PhoneNo = txtPhoneNo.Text.Trim();
                    entSupplier.MobileNo = txtMobileNo.Text.Trim();
                    entSupplier.VATCSTNo = txtVATCSTNo.Text.Trim();
                    entSupplier.ExciseNo = txtExciseNo.Text.Trim();
                    entSupplier.Email = txtEmail.Text.Trim();
                    entSupplier.ServiceTaxNo = txtServiceTaxNo.Text.Trim();
                    entSupplier.EntryBy = SessionWrapper.UserName;

                    if (!Commons.IsRecordExists("tblSupplierMaster", "VATCSTNo", entSupplier.VATCSTNo))
                    {
                        lintcnt = mobjSupplierBLL.InsertSupplier(entSupplier);
                        if (lintcnt > 0)
                        {
                            GetSupplier();
                            lblMessage.Text = "Record Inserted Successfully";
                        }
                        else
                        {
                            lblMessage.Text = "Record Not Inserted";
                        }
                    }
                    else
                    {
                        lblMessage.Text = "Record Already Exist...";
                    }
                }
                MultiView1.SetActiveView(View1);
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                if (!string.IsNullOrEmpty(txtSearch.Text))
                {
                    SearchSupplierDetails(txtSearch.Text);
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

        private void SearchSupplierDetails(string Prefix)
        {
            List<EntitySupplierMaster> lst = mobjSupplierBLL.SelectSupplier(Prefix);
            if (lst != null)
            {
                dgvSupplier.DataSource = lst;
                dgvSupplier.DataBind();
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;
            txtSearch.Text = string.Empty;
            GetSupplier();
        }
    }
}