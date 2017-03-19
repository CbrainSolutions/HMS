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

namespace MainProjectHos.Store
{
    public partial class frmPurchaseOrder : System.Web.UI.Page
    {
        PurchaseOrderBLL mobjDeptBLL = new PurchaseOrderBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] != null)
            {
                EntityLogin entLogin = (EntityLogin)Session["user"];

                List<EntityFormMaster> entFormAcc = (List<EntityFormMaster>)Session["AccessibleForm"];
                int Access = (from tbl in entFormAcc
                              where tbl.FormName.Contains("frmPurchaseOrder")
                              select tbl).Count();

                if (Access > 0)
                {
                    if (!Page.IsPostBack)
                    {
                        Session["MyFlag"] = string.Empty;
                        GetPurchaseOrder();
                        BindSupplierList();
                        BindProductList();
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

        private void BindSupplierList()
        {
            List<EntitySupplierMaster> lstOther = mobjDeptBLL.GetSupplierList();
            ddlSupplier.DataSource = lstOther;
            lstOther.Insert(0, new EntitySupplierMaster() { PKId = 0, SupplierName = "--Select--" });
            ddlSupplier.DataValueField = "PKId";
            ddlSupplier.DataTextField = "SupplierName";
            ddlSupplier.DataBind();
        }

        private void BindProductList()
        {
            List<EntityProduct> lstPat = mobjDeptBLL.GetProductList();
            ddlProduct.DataSource = lstPat;
            lstPat.Insert(0, new EntityProduct() { ProductId = 0, ProductName = "--Select--" });
            ddlProduct.DataValueField = "ProductId";
            ddlProduct.DataTextField = "ProductName";
            ddlProduct.DataBind();
        }

        protected void BtnClose_Click(object sender, EventArgs e)
        {
            try
            {
                Clear();
                GetPurchaseOrder();
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
                Session["MyFlag"] = "EDIT";
                ImageButton imgEdit = (ImageButton)sender;
                GridViewRow row = (GridViewRow)imgEdit.NamingContainer;
                Session["PO_ID"] = Convert.ToInt32(row.Cells[0].Text);
                bool order = mobjDeptBLL.SelectPurchaseOrderStatus(Convert.ToInt32(Session["PO_ID"]));
                if (order)
                {
                    List<EntityPurchaseOrderDetails> tblEmp = mobjDeptBLL.SelectPurchaseOrderForEdit(Convert.ToInt32(Session["PO_ID"]));
                    if (tblEmp.Count > 0)
                    {
                        ListItem item = ddlSupplier.Items.FindByText(Convert.ToString(tblEmp[0].VendorName));
                        ddlSupplier.SelectedValue = item.Value;
                        //ddlPatient_SelectedIndexChanged(sender, e);
                        txtPurchaseDate.Text = row.Cells[2].Text;
                        GridView1.DataSource = tblEmp;
                        Session["FromEdit"] = tblEmp;
                        Session["ForUpdate"] = tblEmp;
                        GridView1.DataBind();
                        Session["BillSrNO"] = Convert.ToInt32(tblEmp[0].SR_No);
                        txtTotal.Text = Convert.ToString(tblEmp[0].NetTotal);
                        txtItemCharge.Text = Convert.ToString(0);
                        txtItemCharge.Enabled = false;
                        txtQuantity.Text = string.Empty;
                        btnUpdate.Visible = true;
                        BtnSave.Visible = false;
                        MultiView1.SetActiveView(View2);
                    }
                }
                else
                {
                    lblMessage.Text = "Purchase Order already Completed you can not change IT.";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }


        protected void BtnAddNewDept_Click(object sender, EventArgs e)
        {
            Session["MyFlag"] = "ADD";
            Session["BillDetails"] = new List<EntityPurchaseOrderDetails>();
            btnUpdate.Visible = false;
            BtnSave.Visible = true;
            //CalPurchaseDate.StartDate = DateTime.Now.Date;
            Clear();
            MultiView1.SetActiveView(View2);
        }

        private void Clear()
        {
            ddlSupplier.SelectedIndex = 0;
            ClearOther();
            txtPurchaseDate.Text = string.Empty;
            GridView1.DataSource = new List<EntityPurchaseOrderDetails>();
            GridView1.DataBind();
            txtTotal.Text = string.Empty;
            txtSearch.Text = string.Empty;
        }

        private void GetPurchaseOrder()
        {
            List<EntityPurchaseOrder> ldtOpera = mobjDeptBLL.GetPurchaseOder();

            if (ldtOpera.Count > 0)
            {
                dgvTestParameter.DataSource = ldtOpera;
                dgvTestParameter.DataBind();
                Session["DepartmentDetail"] = ldtOpera;
                int lintRowcount = ldtOpera.Count;
                lblRowCount.Text = "<b>Total Records:</b> " + lintRowcount.ToString();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            List<EntityPurchaseOrder> ldtOpera = mobjDeptBLL.GetPurchaseOder(txtSearch.Text);

            if (ldtOpera.Count > 0)
            {
                dgvTestParameter.DataSource = ldtOpera;
                dgvTestParameter.DataBind();
                Session["DepartmentDetail"] = ldtOpera;
                int lintRowcount = ldtOpera.Count;
                lblRowCount.Text = "<b>Total Records:</b> " + lintRowcount.ToString();
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            GetPurchaseOrder();
            txtSearch.Text = string.Empty;
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            int Invoice = 0;
            EntityPurchaseOrder entPurchaseOrder = new EntityPurchaseOrder();
            EntityPurchaseOrderDetails entPurchaseOrderDetails = new EntityPurchaseOrderDetails();
            if (ddlSupplier.SelectedIndex == 0)
            {
                lblMsg.Text = "Please Select Supplier Name";
                ddlSupplier.Focus();
                return;
            }
            else
            {
                if (string.IsNullOrEmpty(txtPurchaseDate.Text.Trim()))
                {
                    lblMsg.Text = "Please Selct Purchase Order Date";
                    CalPurchaseDate.Focus();
                    return;
                }
                else
                {
                    entPurchaseOrder.VendorId = Convert.ToInt32(ddlSupplier.SelectedValue);
                    entPurchaseOrder.PO_Amount = Convert.ToDecimal(txtTotal.Text);
                    entPurchaseOrder.PO_Date = Convert.ToDateTime(txtPurchaseDate.Text);

                    List<EntityPurchaseOrderDetails> lstInvoice = (List<EntityPurchaseOrderDetails>)Session["BillDetails"];

                    Invoice = mobjDeptBLL.InsertPurchaseOrder(entPurchaseOrder, lstInvoice);
                    if (Invoice > 0)
                    {
                        GetPurchaseOrder();
                        lblMessage.Text = "Record Inserted Successfully....";
                        Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                    }
                    else
                    {
                        lblMessage.Text = "Record Not Inserted...";
                    }
                    Session["BillDetails"] = new List<EntityPurchaseOrderDetails>();
                }
            }
            MultiView1.SetActiveView(View1);
        }


        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            int lintCnt = 0;
            try
            {
                List<EntityPurchaseOrderDetails> lstEdited = (List<EntityPurchaseOrderDetails>)Session["FromEdit"];
                List<EntityPurchaseOrderDetails> lstUpdate = (List<EntityPurchaseOrderDetails>)Session["ForUpdate"];
                EntityPurchaseOrder entPurchaseOrder = new EntityPurchaseOrder();
                EntityPurchaseOrderDetails entPurchaseOrderDetails = new EntityPurchaseOrderDetails();
                entPurchaseOrder.VendorId = Convert.ToInt32(ddlSupplier.SelectedValue);
                entPurchaseOrder.PO_Amount = Convert.ToDecimal(txtTotal.Text);
                entPurchaseOrder.PO_Date = Convert.ToDateTime(txtPurchaseDate.Text);
                lstEdited[0].Total = Convert.ToDecimal(txtTotal.Text);

                lintCnt = mobjDeptBLL.UpdatePurchaseOrderDetails(lstEdited, lstUpdate);

                if (lintCnt > 0)
                {
                    GetPurchaseOrder();
                    lblMessage.Text = "Record Updated Successfully";
                }
                else
                {
                    lblMessage.Text = "Record Not Updated";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
            MultiView1.SetActiveView(View1);
        }


        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ImageButton imgDelete = (ImageButton)sender;
            GridViewRow row = (GridViewRow)imgDelete.NamingContainer;
            Session["Delete_Charge"] = Convert.ToInt32(row.Cells[0].Text);
            List<EntityPurchaseOrderDetails> lstFinal = new List<EntityPurchaseOrderDetails>();
            if (Convert.ToString(Session["MyFlag"]).Equals("ADD", StringComparison.CurrentCultureIgnoreCase))
            {
                List<EntityPurchaseOrderDetails> lst = (List<EntityPurchaseOrderDetails>)Session["BillDetails"];
                if (lst.Count > 0)
                {
                    foreach (EntityPurchaseOrderDetails item in lst)
                    {
                        if (item.Product_Id != Convert.ToInt32(row.Cells[0].Text))
                        {
                            lstFinal.Add(item);
                        }
                    }
                    Session["BillDetails"] = lstFinal;
                }

            }
            if (Convert.ToString(Session["MyFlag"]).Equals("EDIT", StringComparison.CurrentCultureIgnoreCase))
            {
                List<EntityPurchaseOrderDetails> lst = (List<EntityPurchaseOrderDetails>)Session["FromEdit"];

                foreach (EntityPurchaseOrderDetails item in lst)
                {
                    if (item.Product_Id != Convert.ToInt32(row.Cells[0].Text))
                    {
                        lstFinal.Add(item);
                    }
                }
                Session["FromEdit"] = lstFinal;
            }
            GridView1.DataSource = lstFinal;
            GridView1.DataBind();
            txtTotal.Text = Convert.ToString(lstFinal.Sum(p => p.Rate));
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton imgEdit = (ImageButton)sender;
                GridViewRow cnt = (GridViewRow)imgEdit.NamingContainer;
                Session["Purchase_Id"] = cnt.Cells[0].Text;
                int ID_Purchase = Convert.ToInt32(Session["Purchase_Id"]);
                if (ID_Purchase > 0)
                {
                    Session["ReportType"] = "PurchaseOrder";
                    Response.Redirect("~/PathalogyReport/PathologyReport.aspx", false);
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
        protected void dgvTestParameter_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dgvTestParameter.DataSource = (List<EntityPurchaseOrder>)Session["DepartmentDetail"];
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
            lblMsg.Text = "";
            if (ddlProduct.SelectedIndex > 0)
            {
                if (!string.IsNullOrEmpty(txtQuantity.Text))
                {
                    if (Convert.ToString(Session["MyFlag"]).Equals("ADD", StringComparison.CurrentCultureIgnoreCase))
                    {
                        List<EntityPurchaseOrderDetails> lst = (List<EntityPurchaseOrderDetails>)Session["BillDetails"];

                        int cnt = (from tbl in lst
                                   where tbl.Product_Id == Convert.ToInt32(ddlProduct.SelectedValue)
                                   select tbl).ToList().Count;
                        if (cnt > 0)
                        {
                            lblMsg.Text = "This Product Already Added";
                        }
                        else
                        {
                            lst.Add(new EntityPurchaseOrderDetails() { Product_Id = Convert.ToInt32(ddlProduct.SelectedValue), ProductName = ddlProduct.SelectedItem.Text, Quantity = Convert.ToInt32(txtQuantity.Text), Rate = Convert.ToDecimal(txtItemCharge.Text), Total = Convert.ToDecimal(txtItemCharge.Text) * (Convert.ToInt32(txtQuantity.Text)) });
                            Session["BillDetails"] = lst;
                            GridView1.DataSource = lst;
                            GridView1.DataBind();
                            txtTotal.Text = Convert.ToString(lst.Sum(p => p.Total));
                            ClearOther();
                        }
                    }
                    if (Convert.ToString(Session["MyFlag"]).Equals("EDIT", StringComparison.CurrentCultureIgnoreCase))
                    {
                        List<EntityPurchaseOrderDetails> lst = (List<EntityPurchaseOrderDetails>)Session["FromEdit"];
                        int cnt = (from tbl in lst
                                   where tbl.Product_Id == Convert.ToInt32(ddlProduct.SelectedValue)
                                   select tbl).ToList().Count;
                        if (cnt > 0)
                        {
                            lblMsg.Text = "This Product Already Added";
                        }
                        else
                        {
                            lst.Add(new EntityPurchaseOrderDetails() { Product_Id = Convert.ToInt32(ddlProduct.SelectedValue), ProductName = ddlProduct.SelectedItem.Text, Quantity = Convert.ToInt32(txtQuantity.Text), Rate = Convert.ToDecimal(txtItemCharge.Text), Total = Convert.ToDecimal(txtItemCharge.Text) * (Convert.ToInt32(txtQuantity.Text)) });
                            Session["FromEdit"] = lst;
                            GridView1.DataSource = lst;
                            GridView1.DataBind();
                            txtTotal.Text = Convert.ToString(lst.Sum(p => p.Total));
                            ClearOther();
                        }
                    }
                }
            }
            else
            {
                lblMessage.Text = "Please Select Patient Name";
            }
        }

        private void ClearOther()
        {
            txtItemCharge.Text = Convert.ToString(0);
            txtItemCharge.Enabled = false;
            ddlProduct.SelectedIndex = 0;
            txtQuantity.Text = Convert.ToString(1);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearOther();
        }

        protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProduct.SelectedIndex > 0)
            {
                Session["Prod_ID"] = ddlProduct.SelectedIndex;
                EntityProduct entProduct = mobjDeptBLL.GetProductPrice(Convert.ToInt32(Session["Prod_ID"]));
                txtItemCharge.Text = Convert.ToString(entProduct.Price);
                txtQuantity.Text = Convert.ToString(1);
                List<tblStockDetail> lst1 = new CustomerTransactionBLL().GetProductTransByProductId(Convert.ToInt32(ddlProduct.SelectedValue));
                if (lst1 != null)
                {
                    lblBalQty.Text = Convert.ToString(Convert.ToInt32(lst1.Sum(p => p.InwardQty)) - Convert.ToInt32(lst1.Sum(p => p.OutwardQty)));
                }
                else
                {
                    lblBalQty.Text = string.Empty;
                }
            }
        }
    }
}