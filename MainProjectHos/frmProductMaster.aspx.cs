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
    public partial class frmProductMaster : System.Web.UI.Page
    {
        ProductBLL mobjProductBLL = new ProductBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] != null)
            {
                EntityLogin entLogin = (EntityLogin)Session["user"];

                List<EntityFormMaster> entFormAcc = (List<EntityFormMaster>)Session["AccessibleForm"];
                int Access = (from tbl in entFormAcc
                              where tbl.FormName.Contains("frmProductMaster")
                              select tbl).Count();

                if (Access > 0)
                {
                    if (!Page.IsPostBack)
                    {
                        GetProduct();
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
        protected void BtnAddNewProduct_Click(object sender, EventArgs e)
        {
            EntityProduct entProduct = new EntityProduct();
            int ID = mobjProductBLL.GetNewProductId();
            txtProductId.Text = Convert.ToString(ID);
            txtProductName.Text = string.Empty;
            txtUOM.Text = string.Empty;
            txtSubUOM.Text = string.Empty;
            txtPrice.Text = string.Empty;

            BtnSave.Visible = true;
            btnUpdate.Visible = false;
            MultiView1.SetActiveView(View2);
        }
        public void GetProduct()
        {
            DataTable ldtProduct = new DataTable();
            ldtProduct = mobjProductBLL.GetAllProduct();
            if (ldtProduct.Rows.Count > 0 && ldtProduct != null)
            {
                dgvProduct.DataSource = ldtProduct;
                dgvProduct.DataBind();
                Session["ProductDetails"] = ldtProduct;
                int lintRowcount = ldtProduct.Rows.Count;
                lblRowCount.Text = "<b>Total Records:</b> " + lintRowcount.ToString();
                pnlShow.Style.Add(HtmlTextWriterStyle.Display, "");
            }
            else
            {
                pnlShow.Style.Add(HtmlTextWriterStyle.Display, "none");
                //hdnPanel.Value = "none";
            }
        }

        protected void dgvProduct_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                DataTable ldt = new DataTable();
                if (e.CommandName == "EditProduct")
                {
                    //this.programmaticModalPopupEdit.Show();
                    int linIndex = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                    LinkButton lnkProductId = (LinkButton)gvr.FindControl("lnkProductId");
                    string lstrProductId = lnkProductId.Text;
                    //txtEditCompanyCode.Text = lstrCompanyCode;
                    ldt = mobjProductBLL.GetProductForEdit(lstrProductId);
                    FillControls(ldt);
                }
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmProductMaster -  dgvProduct_RowCommand(object sender, GridViewCommandEventArgs e)", ex);
            }
        }

        private void FillControls(DataTable ldt)
        {
            //txtEditCompanyCode.Text=ldt.Rows[0]["CompanyCode"].ToString();
            //txtEditCompanyName.Text = ldt.Rows[0]["CompanyName"].ToString();
            //txtEditAddress.Text=ldt.Rows[0]["Address"].ToString();
            //txtEditPhoneNo.Text = ldt.Rows[0]["PhoneNo"].ToString();
            //txtEditMobileNo.Text = ldt.Rows[0]["MobileNo"].ToString();
            //txtEditVATCSTNo.Text = ldt.Rows[0]["VATCSTNo"].ToString();
            //txtEditExciseNo.Text = ldt.Rows[0]["ExciseNo"].ToString();
            //txtEditEmail.Text = ldt.Rows[0]["Email"].ToString();
            //txtEditServiceTaxNo.Text = ldt.Rows[0]["ServiceTaxNo"].ToString();

        }

        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            int lintCnt = 0;
            try
            {
                EntityProduct entProduct = new EntityProduct();

                entProduct.ProductId = Convert.ToInt32(txtProductId.Text);
                if (string.IsNullOrEmpty(txtProductName.Text))
                {
                    lblMsg.Text = "Please Enter Product Name";
                    txtProductName.Focus();
                    return;
                }
                else
                {
                    entProduct.ProductName = txtProductName.Text;
                }
                entProduct.UOM = txtUOM.Text;
                entProduct.SubUOM = txtSubUOM.Text;
                entProduct.Price = Convert.ToDecimal(txtPrice.Text);
                if (!Commons.IsRecordExists("tblProductMaster", "ProductName", Convert.ToString(entProduct.ProductName)))
                {
                    lintCnt = mobjProductBLL.UpdateProduct(entProduct);

                    if (lintCnt > 0)
                    {
                        GetProduct();
                        lblMessage.Text = "Record Updated Successfully";
                        //this.programmaticModalPopup.Hide();
                    }
                    else
                    {
                        lblMessage.Text = "Record Not Updated";
                    }
                }
                else
                {
                    lblMessage.Text = "Record Already Exist";
                }
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmProductMaster -  BtnEdit_Click(object sender, EventArgs e)", ex);
            }
            MultiView1.SetActiveView(View1);
        }

        protected void chkDelete_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chk.NamingContainer;
            if (chk.Checked)
            {
                LinkButton ProductId = (LinkButton)row.FindControl("lnkProductId");
                Session["ProductId"] = ProductId.Text;
            }
            else
            {
                Session["ProductId"] = string.Empty;
            }
        }
        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow drv in dgvProduct.Rows)
            {
                CheckBox chkDelete = (CheckBox)drv.FindControl("chkDelete");
                if (chkDelete.Checked)
                {
                }
            }
        }

        protected void BtnDeleteOk_Click(object sender, EventArgs e)
        {
            
        }

        protected void dgvProduct_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvProduct.PageIndex = e.NewPageIndex;
        }

        protected void dgvProduct_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap; text-align:left;");
                }

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Attributes.Add("onmouseover", "SetMouseOver(this)");
                    e.Row.Attributes.Add("onmouseout", "SetMouseOut(this)");

                }

            }
            catch (Exception ex)
            {
                Commons.FileLog("frmProductMaster -  dgvProduct_RowDataBound(object sender, GridViewRowEventArgs e)", ex);
            }
        }

        protected void dgvProduct_DataBound(object sender, EventArgs e)
        {
            int lintCurrentPage = dgvProduct.PageIndex + 1;
            lblPageCount.Text = "<b>Page</b> " + lintCurrentPage.ToString() + "<b> of </b>" + dgvProduct.PageCount.ToString();
        }

        protected void dgvProduct_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dgvProduct.DataSource = (DataTable)Session["ProductDetails"];
                dgvProduct.DataBind();
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmProductMaster - dgvProduct_PageIndexChanged(object sender, EventArgs e)", ex);
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton imgEdit = (ImageButton)sender;
                GridViewRow cnt = (GridViewRow)imgEdit.NamingContainer;
                txtProductId.Text = Convert.ToString(cnt.Cells[0].Text);
                txtProductName.Text = Convert.ToString(cnt.Cells[1].Text);
                txtUOM.Text = Convert.ToString(cnt.Cells[2].Text);
                txtSubUOM.Text = Convert.ToString(cnt.Cells[3].Text);
                txtPrice.Text = Convert.ToString(cnt.Cells[4].Text);

                BtnSave.Visible = false;
                btnUpdate.Visible = true;
                MultiView1.SetActiveView(View2);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void BtnClose_Click(object sender, EventArgs e)
        {
            try
            {
                txtProductId.Text = String.Empty;
                txtProductName.Text = String.Empty;
                txtUOM.Text = String.Empty;
                txtSubUOM.Text = String.Empty;
                txtPrice.Text = String.Empty;

                MultiView1.SetActiveView(View1);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            int lintcnt = 0;
            EntityProduct entProduct = new EntityProduct();
            if (string.IsNullOrEmpty(txtProductName.Text.Trim()))
            {
                lblMsg.Text = "Enter Product Description";
                txtProductName.Focus();
                return;
            }
            else
            {
                entProduct.ProductId = Convert.ToInt32(txtProductId.Text);
                entProduct.ProductName = txtProductName.Text.Trim();
                entProduct.UOM = txtUOM.Text.Trim();
                entProduct.SubUOM = txtSubUOM.Text.Trim();
                entProduct.Price = Convert.ToDecimal(txtPrice.Text);
                if (!Commons.IsRecordExists("tblProductMaster", "ProductName", Convert.ToString(entProduct.ProductName)))
                {
                    lintcnt = mobjProductBLL.InsertProduct(entProduct);

                    if (lintcnt > 0)
                    {
                        GetProduct();
                        lblMessage.Text = "Record Inserted Successfully";
                        //this.programmaticModalPopup.Hide();
                    }
                    else
                    {
                        lblMessage.Text = "Record Not Inserted";
                    }
                }
                else
                {
                    lblMessage.Text = "Record Already Exist";
                }
            }
            MultiView1.SetActiveView(View1);


        }
        protected void dgvProduct_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}