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
    public partial class frmOperationMaster : System.Web.UI.Page
    {
        OpeartionMasterBLL mobjOperationMasterBLL = new OpeartionMasterBLL();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] != null)
            {
                EntityLogin entLogin = (EntityLogin)Session["user"];

                List<EntityFormMaster> entFormAcc = (List<EntityFormMaster>)Session["AccessibleForm"];
                int Access = (from tbl in entFormAcc
                              where tbl.FormName.Contains("frmOperationMaster")
                              select tbl).Count();

                if (Access > 0)
                {
                    if (!Page.IsPostBack)
                    {
                        Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                        //BtnDelete.Enabled = false;
                        GetOperationDetails();
                        BindCategory();
                        BindFloor();
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

        private void BindFloor()
        {
            //try
            //{
            //    DataTable tblCat = new RoomMasterBLL().GetAllFloor();
            //    DataRow dr = tblCat.NewRow();
            //    dr["FloorId"] = 0;
            //    dr["FloorName"] = "---Select---";
            //    tblCat.Rows.InsertAt(dr, 0);

            //    ddlFloorNo.DataSource = tblCat;
            //    ddlFloorNo.DataValueField = "FloorId";
            //    ddlFloorNo.DataTextField = "FloorName";
            //    ddlFloorNo.DataBind();
            //}
            //catch (Exception ex)
            //{
            //    lblMessage.Text = ex.Message;
            //}
        }

        private void GetOperationDetails()
        {
            List<EntityOperationMaster> ldtOpera = mobjOperationMasterBLL.GetAllOperationDetails();

            if (ldtOpera.Count > 0)
            {
                dgvRoomMaster.DataSource = ldtOpera;
                dgvRoomMaster.DataBind();
                Session["DepartmentDetail"] = ldtOpera;
                int lintRowcount = ldtOpera.Count;
                lblRowCount.Text = "<b>Total Records:</b> " + lintRowcount.ToString();

            }
        }

        private void BindCategory()
        {
            try
            {
                DataTable tblCat = new OpeartionMasterBLL().GetAllCategoryName();
                DataRow dr = tblCat.NewRow();
                dr["CategoryId"] = 0;
                dr["CategoryName"] = "---Select---";
                tblCat.Rows.InsertAt(dr, 0);

                ddlCategoryName.DataSource = tblCat;
                ddlCategoryName.DataValueField = "CategoryId";
                ddlCategoryName.DataTextField = "CategoryName";
                ddlCategoryName.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            ViewState["update"] = Session["update"];
        }
        protected void BtnAddNewRoom_Click(object sender, EventArgs e)
        {
            DataTable ldt = new DataTable();
            BtnSave.Visible = true;
            btnUpdate.Visible = false;
            Clear();
            MultiView1.SetActiveView(View2);
        }
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            int lintcnt = 0;
            EntityOperationMaster entOpera = new EntityOperationMaster();
            if (Session["update"].ToString() == ViewState["update"].ToString())
            {
                if (ddlCategoryName.SelectedIndex == 0)
                {
                    lblMsg.Text = "Please Select Operation Category Name";
                    ddlCategoryName.Focus();
                    return;
                }
                else
                {
                    if (string.IsNullOrEmpty(txtCatOperation.Text))
                    {
                        lblMsg.Text = "Please Enter Description";
                        txtCatOperation.Focus();
                        return;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(txtPrice.Text))
                        {
                            lblMsg.Text = "Please Enter Cost For Description";
                            txtPrice.Focus();
                            return;
                        }
                        else
                        {
                            entOpera.OperationCategoryId = Convert.ToInt32(Session["Cat_Id"]);
                            entOpera.OperationName = txtCatOperation.Text;
                            entOpera.Price = Convert.ToDecimal(txtPrice.Text);

                            if (!mobjOperationMasterBLL.IsRecordExists(Convert.ToString(entOpera.OperationName)))
                            {
                                lintcnt = mobjOperationMasterBLL.InsertOperationName(entOpera);

                                if (lintcnt > 0)
                                {
                                    GetOperationDetails();
                                    lblMessage.Text = "Record Inserted Successfully....";
                                    //this.programmaticModalPopup.Hide();
                                    //this.Load += new EventHandler(frmDepartmentMaster_Load);
                                    ////this.Response.Redirect("frmDepartmentMaster.aspx");
                                    Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                                }
                                else
                                {
                                    lblMessage.Text = "Record Not Inserted...";
                                }
                            }
                            else
                            {
                                lblMessage.Text = "Record Already Exist....";
                            }
                            Clear();
                            MultiView1.SetActiveView(View1);
                        }
                    }
                }
            }
            else
            {

            }
        }

        public void Clear()
        {
            txtCatOperation.Text = string.Empty;
            txtPrice.Text = string.Empty;
            ddlCategoryName.SelectedIndex = 0;
        }
        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            int lintCnt = 0;
            try
            {
                EntityOperationMaster entOpera = new EntityOperationMaster();
                //entRoom.RoomId = Convert.ToInt32(txtRoomId.Text);
                entOpera.OperationId = Convert.ToInt32(Session["Opera_Id"]);
                entOpera.OperationName = txtCatOperation.Text;
                entOpera.OperationCategoryId = Convert.ToInt32(Session["Cat_Id"]);
                entOpera.Price = Convert.ToDecimal(txtPrice.Text);
                OpeartionMasterBLL objOtherAcc = new OpeartionMasterBLL();
                lintCnt = objOtherAcc.Update(entOpera);
                if (lintCnt > 0)
                {
                    GetOperationDetails();
                    lblMessage.Text = "Record Updated Successfully";
                    //this.programmaticModalPopup.Hide();
                }
                else
                {
                    lblMessage.Text = "Record Not Updated";
                }
                MultiView1.SetActiveView(View1);
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmDepartmentMaster -  BtnEdit_Click(object sender, EventArgs e)", ex);
            }
        }


        protected void BtnClose_Click(object sender, EventArgs e)
        {
            try
            {
                Clear();
                GetOperationDetails();
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
                Session["Opera_Id"] = Convert.ToInt32(cnt.Cells[0].Text);
                EntityOperationMaster tblOpera = new OpeartionMasterBLL().SelectOperation(Convert.ToInt32(cnt.Cells[0].Text));
                if (tblOpera != null)
                {
                    ListItem Desig = ddlCategoryName.Items.FindByText(Convert.ToString(tblOpera.CatName));
                    ddlCategoryName.SelectedValue = Desig.Value;
                    Session["Cat_Id"] = ddlCategoryName.SelectedValue;
                    txtCatOperation.Text = tblOpera.OperationName;
                    txtPrice.Text = Convert.ToString(tblOpera.Price);
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
        protected void dgvRoomMaster_DataBound(object sender, EventArgs e)
        {
            int lintCurrentPage = dgvRoomMaster.PageIndex + 1;
            lblPageCount.Text = "<b>Page</b> " + lintCurrentPage.ToString() + "<b> of </b>" + dgvRoomMaster.PageCount.ToString();
        }
        protected void dgvRoomMaster_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dgvRoomMaster.DataSource = (DataTable)Session["RoomDetails"];
                dgvRoomMaster.DataBind();
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmRoomMaster - dgvRoomMaster_PageIndexChanged(object sender, EventArgs e)", ex);
            }
        }
        protected void dgvRoomMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvRoomMaster.PageIndex = e.NewPageIndex;
        }
        protected void dgvRoomMaster_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                DataTable ldt = new DataTable();
                if (e.CommandName == "EditDept")
                {
                    int linIndex = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                    LinkButton lnkRoomId = (LinkButton)gvr.FindControl("lnkRoomId");
                    string lstrRoomId = lnkRoomId.Text;
                    //txtEditDeptCode.Text = lstrDeptCode;
                    //  ldt = mobjRoomMasterBLL.GetRoomForEdit(lstrRoomId);
                    FillControls(ldt);
                }
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmDepartmentMaster -  dgvDepartment_RowCommand(object sender, GridViewCommandEventArgs e)", ex);
            }
        }

        private void FillControls(DataTable ldt)
        {

        }
        protected void dgvRoomMaster_RowDataBound(object sender, GridViewRowEventArgs e)
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
                    LinkButton lnkRoomId = (LinkButton)e.Row.FindControl("lnkRoomId");
                    CheckBox chkDelete = (CheckBox)e.Row.FindControl("chkDelete");
                    if (lnkRoomId.Text == "Admin")
                    {
                        lnkRoomId.Enabled = false;
                        chkDelete.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmDepartmentMaster -  dgvData_RowDataBound(object sender, GridViewRowEventArgs e)", ex);
            }
        }
        protected void ddlCategoryName_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["Cat_Id"] = Convert.ToInt32(ddlCategoryName.SelectedValue);
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                txtSearch.Text = string.Empty;
                GetOperationDetails();
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
                if (!string.IsNullOrEmpty(txtSearch.Text))
                {
                    List<EntityOperationMaster> ldtOpera = mobjOperationMasterBLL.GetAllOperationDetails(txtSearch.Text);

                    if (ldtOpera.Count > 0)
                    {
                        dgvRoomMaster.DataSource = ldtOpera;
                        dgvRoomMaster.DataBind();
                        Session["DepartmentDetail"] = ldtOpera;
                        int lintRowcount = ldtOpera.Count;
                        lblRowCount.Text = "<b>Total Records:</b> " + lintRowcount.ToString();

                    }
                }
            }
            catch (Exception ex)
            {

                lblMessage.Text = ex.Message;
            }
        }
    }
}