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
    public partial class frmBedAllocToPatient : System.Web.UI.Page
    {
        BedStatusBLL mobjDeptBLL = new BedStatusBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] != null)
            {
                EntityLogin entLogin = (EntityLogin)Session["user"];

                List<EntityFormMaster> entFormAcc = (List<EntityFormMaster>)Session["AccessibleForm"];
                int Access = (from tbl in entFormAcc
                              where tbl.FormName.Contains("frmBedAllocToPatient")
                              select tbl).Count();

                if (Access > 0)
                {
                    if (!Page.IsPostBack)
                    {
                        GetBedAlloc();
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

        protected void BtnClose_Click(object sender, EventArgs e)
        {
            try
            {
                txtBedNo.Text = String.Empty;
                txtFloorName.Text = String.Empty;
                GetBedAlloc();
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
                lblMessage.Text = string.Empty;
                ImageButton imgEdit = (ImageButton)sender;
                GridViewRow row = (GridViewRow)imgEdit.NamingContainer;
                Session["Bed_Id"] = Convert.ToInt32(row.Cells[0].Text);
                EntityBedMaster lstB = new BedStatusBLL().GetFloorRoomBed(Convert.ToInt32(Session["Bed_Id"]));
                Session["Floor_Id"] = lstB.FloorNo;
                Session["Room_Id"] = lstB.RoomId;
                txtBedNo.Text = Convert.ToString(row.Cells[1].Text);
                txtFloorName.Text = Convert.ToString(row.Cells[3].Text);
                if (!Convert.ToString(row.Cells[5].Text).Equals("&nbsp;", StringComparison.CurrentCultureIgnoreCase))
                {
                    txtAllocDate.Text = string.Empty;
                }
                else
                {
                    txtAllocDate.Text = string.Empty;
                }
                txtRoomNo.Text = Convert.ToString(row.Cells[2].Text);
                GetBedAlloc();
                GetPatientList();
                btnUpdate.Visible = true;
                BtnSave.Visible = false;
                MultiView1.SetActiveView(View2);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private void GetPatientList()
        {
            try
            {
                DataTable tblCat = new BedStatusBLL().GetAllPatient();
                if (tblCat.Rows.Count > 0)
                {
                    DataRow dr = tblCat.NewRow();
                    dr["AdmitId"] = 0;
                    dr["FullName"] = "---Select---";
                    tblCat.Rows.InsertAt(dr, 0);

                    ddlPatientName.DataSource = tblCat;
                    ddlPatientName.DataValueField = "AdmitId";
                    ddlPatientName.DataTextField = "FullName";
                    ddlPatientName.DataBind();
                }
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
        protected void BtnAddNewDept_Click(object sender, EventArgs e)
        {
            txtFloorName.Text = string.Empty;
            txtBedNo.Text = string.Empty;
            txtRoomNo.Text = string.Empty;
            btnUpdate.Visible = false;
            BtnSave.Visible = true;
            MultiView1.SetActiveView(View2);
        }

        private void GetBedAlloc()
        {
            try
            {
                List<sp_GetAllBedAllocResult> ldtDept = new BedStatusBLL().GetAllBedAlloc();
                if (ldtDept.Count > 0)
                {
                    dgvDepartment.DataSource = ldtDept;
                    dgvDepartment.DataBind();
                    Session["DepartmentDetail"] = ldtDept;
                    int lintRowcount = ldtDept.Count;
                    lblRowCount.Text = "<b>Total Records:</b> " + lintRowcount.ToString();
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }

        }

        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            int lintCnt = 0;
            try
            {
                EntityBedAllocToPatient entDept = new EntityBedAllocToPatient();
                entDept.BedId = Convert.ToInt32(Session["Bed_Id"]);
                entDept.FloorId = Convert.ToInt32(Session["Floor_Id"]);
                entDept.RoomId = Convert.ToInt32(Session["Room_Id"]);
                entDept.PatientId = Convert.ToInt32(Session["Pat_Id"]);
                if (!string.IsNullOrEmpty(txtAllocDate.Text))
                {
                    entDept.AllocationDate = Convert.ToDateTime(txtAllocDate.Text);
                }
                else
                {
                    entDept.AllocationDate = null;
                }
                PatientMasterBLL mobjPatient = new PatientMasterBLL();
                EntityPatientAdmit entAdmit = new EntityPatientAdmit() { PatientId = Convert.ToInt32(ddlPatientName.SelectedValue) };
                bool Status = mobjPatient.CheckPatientAlreadyAllocated(entAdmit);
                if (!Status)
                {
                    lintCnt = mobjDeptBLL.InsertAllocBed(entDept);
                    if (lintCnt > 0)
                    {
                        lblMessage.Text = "Record Updated Successfully";
                    }
                    else
                    {
                        lblMessage.Text = "Record Not Updated";
                    }
                }
                else
                {
                    lblMessage.Text = "This Patient Already Allocated to Another Bed";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
            GetBedAlloc();
            MultiView1.SetActiveView(View1);
        }

        protected void dgvDepartment_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dgvDepartment.DataSource = (List<sp_GetAllBedAllocResult>)Session["DepartmentDetail"];
                dgvDepartment.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
        protected void dgvDepartment_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvDepartment.PageIndex = e.NewPageIndex;
        }

        protected void dgvDepartment_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (!e.Row.Cells[0].Text.Equals("Bed Id", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (e.Row.Cells.Count > 5)
                    {
                        if (!e.Row.Cells[5].Text.Equals("&nbsp;", StringComparison.CurrentCultureIgnoreCase))
                        {
                            if (e.Row.Cells.Count > 6)
                            {
                                if (!e.Row.Cells[6].Text.Equals("&nbsp;", StringComparison.CurrentCultureIgnoreCase))
                                {
                                    e.Row.Enabled = true;
                                    e.Row.BackColor = Color.GhostWhite;
                                }
                                else
                                {
                                    e.Row.Enabled = false;
                                    e.Row.BackColor = Color.LightSkyBlue;
                                }
                            }
                        }
                        else
                        {
                            e.Row.Enabled = true;
                            e.Row.BackColor = Color.GhostWhite;
                        }
                    }
                }
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        try
                        {
                            if (e.Row.Cells.Count < i)
                            {
                                e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap; text-align:left;");
                            }
                        }
                        catch (Exception ex)
                        {
                            lblMessage.Text = ex.Message;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }



        protected void ddlPatientName_SelectedIndexChanged1(object sender, EventArgs e)
        {
            try
            {
                if (ddlPatientName.SelectedIndex > 0)
                {
                    Session["Pat_Id"] = Convert.ToInt32(ddlPatientName.SelectedValue);
                    EntityPatientAlloc objTxt = new PatientAllocDocBLL().GetPatientType(Convert.ToInt32(ddlPatientName.SelectedValue));
                    //CalDOBDate.StartDate = objTxt.AdmitDate;
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }

        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSearch.Text))
                {
                    SearchBedAllocDetails(txtSearch.Text);
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

        private void SearchBedAllocDetails(string Prefix)
        {
            try
            {
                List<sp_GetAllBedAllocResult> lst = mobjDeptBLL.SelectBedAlloc(Prefix);
                if (lst != null)
                {
                    dgvDepartment.DataSource = lst;
                    dgvDepartment.DataBind();
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
                GetBedAlloc();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
    }
}