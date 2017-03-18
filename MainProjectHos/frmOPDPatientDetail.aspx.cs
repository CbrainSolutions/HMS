using MainProjectHos.Models.BusinessLayer;
using MainProjectHos.Models.DataLayer;
using MainProjectHos.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MainProjectHos
{
    public partial class frmOPDPatientDetail : System.Web.UI.Page
    {
        OPDPatientMasterBLL mobjPatientMasterBLL = new OPDPatientMasterBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] != null)
            {
                EntityLogin entLogin = (EntityLogin)Session["user"];

                List<EntityFormMaster> entFormAcc = (List<EntityFormMaster>)Session["AccessibleForm"];
                if (entFormAcc != null)
                {
                    int Access = (from tbl in entFormAcc
                                  where tbl.FormName.Contains("frmOPDPatientDetail")
                                  select tbl).Count();

                    if (Access > 0)
                    {
                        if (!Page.IsPostBack)
                        {
                            GetOPDPatientList();
                            pnlShow.Visible = true;
                            pnl.Visible = true;
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
                    Response.Redirect("frmAdminMain.aspx", false);
                }
            }
            else
            {
                Response.Redirect("frmlogin.aspx", false);
            }

        }

        public void GetOPDPatientList()
        {
            List<EntityPatientMaster> ldtRequisition = mobjPatientMasterBLL.GetPatientList();
            if (ldtRequisition.Count > 0 && ldtRequisition != null)
            {
                dgvPatientList.DataSource = ldtRequisition;
                dgvPatientList.DataBind();
                Session["PatientList"] = ldtRequisition;
                int lintRowcount = ldtRequisition.Count;
                lblRowCount.Text = "<b>Total Records:</b> " + lintRowcount.ToString();
            }
        }

        protected void dgvOPDPatientDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditPatient")
            {
                DataTable ldt = new DataTable();
                DataTable ldtConCharge = new DataTable();
                ldt.Columns.Add("SrNo");
                ldt.Columns.Add("DrugName");
                ldt.Columns.Add("Morning");
                ldt.Columns.Add("AfterNoon");
                ldt.Columns.Add("Night");
                ldt.Rows.Add("1", "", "", "", "");
                ViewState["CurrentTable"] = ldt;
                GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                LinkButton lnkDeptCode = (LinkButton)gvr.FindControl("lnkPatientCode");
                Label lblPatientName = (Label)gvr.FindControl("lbPatientName");
                Label lblAppointNo = (Label)gvr.FindControl("lblAppNo");
                Session["AppointmentNo"] = lblAppointNo.Text;
                Label lblConsultantId = (Label)gvr.FindControl("lblConsultantId");
                ldtConCharge = mobjPatientMasterBLL.GetOPDChargesForConsultant(Commons.ConvertToInt(lblConsultantId.Text));
                if (ldtConCharge.Rows.Count > 0 && ldtConCharge != null)
                {
                    Session["ConsultantCharge"] = ldtConCharge.Rows[0]["Charge"].ToString();
                }
                else
                {
                    Commons.ShowMessage("Please Set Consultant Fees In Master....", this.Page);
                    return;
                }
                Session["ConsultantId"] = lblConsultantId.Text;
            }

        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton imgEdit = (ImageButton)sender;
                GridViewRow cnt = (GridViewRow)imgEdit.NamingContainer;

                DataTable dt = new PatientMasterBLL().GetPatientDetail(cnt.Cells[0].Text);
                Session["Patient_Details"] = dt;
                Session["EDIT"] = true;
                Response.Redirect("~/frmOPDPatient.aspx");
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            List<EntityOPDPatient> lstentOPDPatient = new List<EntityOPDPatient>();
            Decimal ldcTotalRateOfInj = 0.0M;
            Decimal ldcInjection = 0.0M;
            Decimal ldcDressing = 0.0M;
            decimal ldcRevisit = 0.0M;
            Decimal ldcTotalOPDBill = 0.0M;
            Decimal ldcConsultantCharges = 0.0M;
            int cnt = 0;

            try
            {
                EntityOPDPatient entOPDPatient = new EntityOPDPatient();
                entOPDPatient.EntryBy = SessionWrapper.UserName;
                entOPDPatient.AppointNO = Commons.ConvertToInt(Session["AppointmentNo"]);
                ldcDressing = Commons.ConvertToDecimal(Session["DressingRate"]);
                ldcRevisit = Commons.ConvertToDecimal(Session["Revisit"]);
                ldcInjection = Commons.ConvertToDecimal(Session["InjectionRate"]);
                ldcConsultantCharges = Commons.ConvertToDecimal(Session["ConsultantCharge"]);

                if (ldcRevisit == 0.0M && ldcRevisit == null)
                {
                    entOPDPatient.ConsultantCharge = ldcConsultantCharges;
                    entOPDPatient.RevisitCharge = 0.0M;
                    entOPDPatient.PatientVisitType = "N";
                }
                else
                {
                    entOPDPatient.RevisitCharge = ldcRevisit;
                    entOPDPatient.ConsultantCharge = 0.0M;
                    entOPDPatient.PatientVisitType = "R";
                }

                ldcTotalOPDBill = ldcTotalRateOfInj + ldcRevisit + ldcDressing + ldcConsultantCharges;
                entOPDPatient.InjectionCharge = ldcTotalRateOfInj;
                entOPDPatient.DressingCharge = ldcDressing;
                entOPDPatient.ConsultantCharge = ldcConsultantCharges;
                entOPDPatient.TotalOPDBill = ldcTotalOPDBill;
                entOPDPatient.ConsultedBy = Commons.ConvertToInt(Session["ConsultantId"]);
                DataTable ldt = new DataTable();
                string lstrDiagnosisNo = string.Empty;
                ldt = mobjPatientMasterBLL.GetNewDiagnosisCode();
                if (ldt.Rows.Count > 0 && ldt != null)
                {
                    lstrDiagnosisNo = ldt.Rows[0]["DiagnosisCode"].ToString();
                }
                entOPDPatient.DiagnosisCode = lstrDiagnosisNo;
                cnt = mobjPatientMasterBLL.InsertOPDPatientTreatmentDetail(lstentOPDPatient, entOPDPatient);
                if (cnt > 0)
                {
                    lblMessage.Text = "Patient Inserted Successfully";
                    GetOPDPatientList();
                }
                else
                {
                    lblMessage.Text = "Error!!!!!";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void dgvPatientList_DataBound(object sender, EventArgs e)
        {
            int lintCurrentPage = dgvPatientList.PageIndex + 1;
            lblPageCount.Text = "<b>Page</b> " + lintCurrentPage.ToString() + "<b> of </b>" + dgvPatientList.PageCount.ToString();
        }
        protected void dgvPatientList_PageIndexChanged(object sender, EventArgs e)
        {
            dgvPatientList.DataSource = (List<EntityPatientMaster>)Session["PatientList"];
            dgvPatientList.DataBind();
        }
        protected void dgvPatientList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvPatientList.PageIndex = e.NewPageIndex;
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            dgvPatientList.PageIndex = 0;
            GetOPDPatientList();
            txtSearch.Text = string.Empty;
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSearch.Text))
                {
                    SelectPatient(txtSearch.Text);
                }
                else
                {
                    lblMessage.Text = "Please fill search text.";
                    txtSearch.Focus();
                }

            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private void SelectPatient(string Prefix)
        {
            OPDPatientMasterBLL objOPDPatient = new OPDPatientMasterBLL();
            List<EntityPatientMaster> lst = objOPDPatient.SearchPatient(Prefix);
            if (lst != null)
            {
                dgvPatientList.DataSource = lst;
                dgvPatientList.DataBind();

                lblRowCount.Text = string.Empty;
            }
        }
        protected void AddNew_Click(object sender, EventArgs e)
        {
            Session["Edit"] = false;
            Response.Redirect("frmOPDPatient.aspx", false);
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton imgEdit = (ImageButton)sender;
                GridViewRow row = (GridViewRow)imgEdit.NamingContainer;
                Session["AdmitId"] = Convert.ToInt32(dgvPatientList.DataKeys[row.RowIndex].Value);
                Session["ReportType"] = "OPDPaper";
                Response.Redirect("~/PathalogyReport/PathologyReport.aspx", false);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void dgvPatientList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                DataTable ldt = new DataTable();
                GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;

                if (e.CommandName == "DownloadEndoscopy")
                {
                    ImageButton imgEdit = (ImageButton)sender;
                    GridViewRow cnt = (GridViewRow)imgEdit.NamingContainer;

                    DataTable dt = new PatientMasterBLL().GetPatientDetail(cnt.Cells[0].Text);
                    string lintPKId = dt.Rows[0]["PatientCode"].ToString();

                    ldt = new PatientMasterBLL().GetPatientDetail(lintPKId);

                    if (ldt.Rows.Count > 0 && ldt != null)
                    {
                        Response.Clear();
                        Byte[] sBytes = (Byte[])ldt.Rows[0]["EndoscopyFile"];
                        MemoryStream ms = new MemoryStream(sBytes);
                        Response.Charset = "";
                        Response.ContentType = "application/pdf";
                        Response.AddHeader("content-disposition", "attachment;filename=Endoscopy.pdf");
                        Response.Buffer = true;
                        ms.WriteTo(Response.OutputStream);
                        Response.BinaryWrite(sBytes);
                        Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        Response.End();
                    }
                }
                if (e.CommandName == "DownloadAudiometry")
                {
                    ImageButton imgEdit = (ImageButton)sender;
                    GridViewRow cnt = (GridViewRow)imgEdit.NamingContainer;

                    DataTable dt = new PatientMasterBLL().GetPatientDetail(cnt.Cells[0].Text);

                    string lintPKId = dt.Rows[0]["PatientCode"].ToString();

                    ldt = new PatientMasterBLL().GetPatientDetail(lintPKId);

                    if (ldt.Rows.Count > 0 && ldt != null)
                    {
                        Response.Clear();
                        Byte[] sBytes = (Byte[])ldt.Rows[0]["AudiometryFile"];
                        MemoryStream ms = new MemoryStream(sBytes);
                        Response.Charset = "";
                        Response.ContentType = "application/pdf";
                        Response.AddHeader("content-disposition", "attachment;filename=Audiometry.pdf");
                        Response.Buffer = true;
                        ms.WriteTo(Response.OutputStream);
                        Response.BinaryWrite(sBytes);
                        Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        Response.End();
                    }
                }
            }
            catch (System.Threading.ThreadAbortException)
            {

            }
        }
    }
}