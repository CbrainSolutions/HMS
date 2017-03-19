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
    public partial class frmMakeDischarge : System.Web.UI.Page
    {
        ShiftBLL mobjDeptBLL = new ShiftBLL();

        MakeDischargeBLL mobjDischarge = new MakeDischargeBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] != null)
            {
                EntityLogin entLogin = (EntityLogin)Session["user"];

                List<EntityFormMaster> entFormAcc = (List<EntityFormMaster>)Session["AccessibleForm"];
                int Access = (from tbl in entFormAcc
                              where tbl.FormName.Contains("frmMakeDischarge")
                              select tbl).Count();

                if (Access > 0)
                {
                    if (!Page.IsPostBack)
                    {
                        Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                        BindPatients();
                        BindSurgery();
                        GetDischargeList();
                        MultiView1.SetActiveView(View2);
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

        private void BindSurgery()
        {
            try
            {
                List<EntitySurgeryMaster> tblSurgery = new SurgeryMasterBLL().GetAllSurgeryName();
                tblSurgery.Insert(0, new EntitySurgeryMaster() { PKId = 0, NameOfSurgery = "----Select----" });
                ddlNameOfSurgery.DataSource = tblSurgery;
                ddlNameOfSurgery.DataTextField = "NameOFSurgery";
                ddlNameOfSurgery.DataValueField = "PKId";
                ddlNameOfSurgery.DataBind();
            }
            catch (Exception ex)
            {
                //lblMessage.Text = ex.Message;
            }
        }

        protected void ddlNameOfSurgery_IndexChanged(object sender, EventArgs e)
        {
            int Val = Convert.ToInt32(ddlNameOfSurgery.SelectedValue);
            EntitySurgeryMaster objTxt = new SurgeryMasterBLL().GetOperationalProcedure(Val);
            txtOperatonalProce.Text = objTxt.OperationalProcedure;
        }

        protected void ddlDischargeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDischargeType.SelectedItem.Text == "Routine")
            {
                lblFollowUp.Visible = true;
                txtFollowUp.Visible = true;
            }
            else
            {
                lblFollowUp.Visible = false;
                txtFollowUp.Visible = false;
            }
        }

        private void BindPatients()
        {
            try
            {
                List<EntityPatientMaster> tblPatient = new clsTestAllocation().GetPatientListForDischarge();
                tblPatient.Insert(0, new EntityPatientMaster() { PatientId = 0, FullName = "----Select----" });
                ddlPatient.DataSource = tblPatient;
                ddlPatient.DataTextField = "FullName";
                ddlPatient.DataValueField = "PatientId";
                ddlPatient.DataBind();
            }
            catch (Exception ex)
            {
                //lblMessage.Text = ex.Message;
            }
        }

        protected void ddlPatient_SelectedIndexChanged(object sender, EventArgs e)
        {
            int Val = Convert.ToInt32(ddlPatient.SelectedValue);
            EntityPatientMaster objTxt = new PatientMasterBLL().GetDischargeOnDiadnosis(Val);
            txtDiagnosis.Text = objTxt.Dignosys;
        }

        protected void dgvTestInvoice_PageIndexChanged(object sender, EventArgs e)
        {
            List<EntityMakeDischarge> tblPatient = (List<EntityMakeDischarge>)Session["TestInvoice"];
            dgvTestInvoice.DataSource = tblPatient;
            dgvTestInvoice.DataBind();
        }

        protected void dgvTestInvoice_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvTestInvoice.PageIndex = e.NewPageIndex;
        }

        protected void dgvTestInvoice_DataBound(object sender, EventArgs e)
        {
            int lintCurrentPage = dgvTestInvoice.PageIndex + 1;
            lblPageCount.Text = "<b>Page</b> " + lintCurrentPage.ToString() + "<b> of </b>" + dgvTestInvoice.PageCount.ToString();
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                List<EntityTestInvoice> tblPatient = new clsTestAllocation().GetTestInvoiceList(txtSearch.Text);
                Session["TestInvoice"] = tblPatient;
                dgvTestInvoice.DataSource = tblPatient;
                dgvTestInvoice.DataBind();
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                Session["Status"] = "Edit";
                lblMessage.Text = string.Empty;
                ImageButton imgEdit = (ImageButton)sender;
                GridViewRow cnt = (GridViewRow)imgEdit.NamingContainer;
                Session["DischargeId"] = dgvTestInvoice.DataKeys[cnt.RowIndex].Value;
                EntityMakeDischarge lst = mobjDischarge.GetDischargeDetails(Convert.ToInt32(Session["DischargeId"]));
                if (lst != null)
                {
                    ListItem item = ddlPatient.Items.FindByText(Convert.ToString(cnt.Cells[3].Text));
                    ddlPatient.SelectedValue = item.Value;
                    ListItem itemSur = ddlNameOfSurgery.Items.FindByText(Convert.ToString(lst.NameOfSurgery));
                    ddlNameOfSurgery.SelectedValue = itemSur.Value;
                    Session["patientID"] = ddlPatient.SelectedValue;
                    Session["SurID"] = ddlNameOfSurgery.SelectedValue;
                    ddlPatient.Enabled = false;
                    ddlDischargeType.Text = lst.TypeOfDischarge;
                    if (lst.TypeOfDischarge == "Routine")
                    {
                        lblFollowUp.Visible = true;
                        txtFollowUp.Visible = true;
                    }
                    else
                    {
                        lblFollowUp.Visible = false;
                        txtFollowUp.Visible = false;
                    }
                    txtDiagnosis.Text = lst.Diagnosis;
                    txtDischargeDate.Text = Convert.ToDateTime(lst.DischargeReceiptDate).ToString("MM/dd/yyyy");
                    txtOperatonalProce.Text = lst.OperationalProcedure;
                    txtHistory.Text = lst.HistoryClinical;
                    txtXRay.Text = lst.XRay;
                    txtHaemogram.Text = lst.Haemogram;
                    txtOthers.Text = lst.Others;
                    txtTreatmentHospital.Text = lst.TreatmentInHospitalisation;
                    txtAdviceDischarge.Text = lst.AdviceOnDischarge;
                    txtFollowUp.Text = lst.FollowUp;
                    txtECG.Text = lst.ECG;
                    txtBUL.Text = lst.BUL;
                    txtSCreat.Text = lst.SCreat;
                    txtSElect.Text = lst.SElect;
                    txtBSL.Text = lst.BSL;
                    txtUrineR.Text = lst.UrineR;
                    txtTemp.Text = lst.Temp;
                    txtPulse.Text = lst.Pulse;
                    txtBP.Text = lst.BP;
                    txtRespRate.Text = lst.RespRate;
                    txtPallor.Text = lst.Pallor;
                    txtOedema.Text = lst.Oedema;
                    txtCyanosis.Text = lst.Cyanosis;
                    txtClubbing.Text = lst.Clubbing;
                    txtIcterus.Text = lst.Icterus;
                    txtSkin.Text = lst.Skin;
                    txtRespSystem.Text = lst.RespSystem;
                    txtCNS.Text = lst.CNS;
                    txtPerAbd.Text = lst.PerAbd;
                    txtCVS.Text = lst.CVS;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
            BtnUpdate.Visible = true;
            BtnSave.Visible = false;
            MultiView1.SetActiveView(View1);
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton imgEdit = (ImageButton)sender;
                GridViewRow cnt = (GridViewRow)imgEdit.NamingContainer;
                Session["AdmintId"] = Convert.ToInt32(cnt.Cells[1].Text);
                Session["DischargeId"] = dgvTestInvoice.DataKeys[cnt.RowIndex].Value;
                Session["ReportType"] = "Discharge";
                Response.Redirect("~/PathalogyReport/PathologyReport.aspx", false);
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
                lblMessage.Text = string.Empty;
                ddlPatient.SelectedIndex = 0;
                ddlNameOfSurgery.SelectedIndex = 0;
                txtOperatonalProce.Text = string.Empty;
                txtDiagnosis.Text = string.Empty;
                txtAdviceDischarge.Text = string.Empty;
                txtFollowUp.Text = string.Empty;
                txtHistory.Text = string.Empty;
                txtTreatmentHospital.Text = string.Empty;
                txtOthers.Text = string.Empty;
                ddlDischargeType.SelectedIndex = 0;
                txtXRay.Text = string.Empty;
                txtHaemogram.Text = string.Empty;
                txtBUL.Text = string.Empty;
                txtSCreat.Text = string.Empty;
                txtSElect.Text = string.Empty;
                txtBSL.Text = string.Empty;
                txtECG.Text = string.Empty;
                txtUrineR.Text = string.Empty;
                txtTemp.Text = string.Empty;
                txtPulse.Text = string.Empty;
                txtBP.Text = string.Empty;
                txtRespRate.Text = string.Empty;
                txtPallor.Text = string.Empty;
                txtOedema.Text = string.Empty;
                txtCyanosis.Text = string.Empty;
                txtClubbing.Text = string.Empty;
                txtIcterus.Text = string.Empty;
                txtSkin.Text = string.Empty;
                txtRespSystem.Text = string.Empty;
                txtCNS.Text = string.Empty;
                txtPerAbd.Text = string.Empty;
                txtCVS.Text = string.Empty;
                MultiView1.SetActiveView(View2);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                ddlPatient.SelectedIndex = 0;
                ddlPatient.Enabled = true;
                ddlNameOfSurgery.SelectedIndex = 0;
                txtOperatonalProce.Text = string.Empty;
                txtDischargeDate.Enabled = false;
                txtDischargeDate.Text = DateTime.UtcNow.Date.ToString("MM/dd/yyyy");
                txtDiagnosis.Text = string.Empty;
                txtAdviceDischarge.Text = string.Empty;
                txtFollowUp.Text = string.Empty;
                txtHistory.Text = string.Empty;
                txtTreatmentHospital.Text = string.Empty;
                txtOthers.Text = string.Empty;
                ddlDischargeType.SelectedIndex = 0;
                txtXRay.Text = string.Empty;
                txtHaemogram.Text = string.Empty;
                txtBUL.Text = string.Empty;
                txtSCreat.Text = string.Empty;
                txtSElect.Text = string.Empty;
                txtBSL.Text = string.Empty;
                txtECG.Text = string.Empty;
                txtUrineR.Text = string.Empty;
                txtTemp.Text = string.Empty;
                txtPulse.Text = string.Empty;
                txtBP.Text = string.Empty;
                txtRespRate.Text = string.Empty;
                txtPallor.Text = string.Empty;
                txtOedema.Text = string.Empty;
                txtCyanosis.Text = string.Empty;
                txtClubbing.Text = string.Empty;
                txtIcterus.Text = string.Empty;
                txtSkin.Text = string.Empty;
                txtRespSystem.Text = string.Empty;
                txtCNS.Text = string.Empty;
                txtPerAbd.Text = string.Empty;
                txtCVS.Text = string.Empty;
                //lblAdminName.Text = Convert.ToString(Session["AdminName"]);
                BtnUpdate.Visible = false;
                MultiView1.SetActiveView(View1);
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtSearch.Text = string.Empty;
            dgvTestInvoice.PageIndex = 0;
            GetDischargeList();
        }

        private void GetDischargeList()
        {
            try
            {
                List<EntityMakeDischarge> tblPatient = mobjDischarge.GetDischargeInvoiceList();
                dgvTestInvoice.DataSource = tblPatient;
                Session["TestInvoice"] = tblPatient;
                dgvTestInvoice.DataBind();
                int lintRowcount = tblPatient.Count;
                lblRowCount1.Text = "<b>Total Records:</b> " + lintRowcount.ToString();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                EntityMakeDischarge entDischarge = new EntityMakeDischarge();
                entDischarge.PatientId = Convert.ToInt32(ddlPatient.SelectedValue);
                entDischarge.SurgeryId = Convert.ToInt32(ddlNameOfSurgery.SelectedValue);
                entDischarge.NameOfSurgery = Convert.ToString(ddlNameOfSurgery.SelectedItem.Text);
                TimeSpan span = new TimeSpan(0,0, 0);
                entDischarge.TypeOfDischarge = ddlDischargeType.SelectedItem.Text;
                entDischarge.Diagnosis = FirstCharToUpper(txtDiagnosis.Text);
                entDischarge.OperationalProcedure = FirstCharToUpper(txtOperatonalProce.Text);
                entDischarge.HistoryClinical = FirstCharToUpper(txtHistory.Text);
                entDischarge.XRay = txtXRay.Text;
                entDischarge.Haemogram = txtHaemogram.Text;
                entDischarge.Others = txtOthers.Text;
                entDischarge.TreatmentInHospitalisation = FirstCharToUpper(txtTreatmentHospital.Text);
                entDischarge.AdviceOnDischarge = FirstCharToUpper(txtAdviceDischarge.Text);
                entDischarge.FollowUp = txtFollowUp.Text;
                entDischarge.DischargeReceiptDate = Convert.ToDateTime(txtDischargeDate.Text).Add(span);
                entDischarge.BUL = txtBUL.Text;
                entDischarge.SCreat = txtSCreat.Text;
                entDischarge.SElect = txtSElect.Text;
                entDischarge.BSL = txtBSL.Text;
                entDischarge.UrineR = txtUrineR.Text;
                entDischarge.ECG = txtECG.Text;
                entDischarge.Temp = txtTemp.Text;
                entDischarge.Pulse = txtPulse.Text;
                entDischarge.BP = txtBP.Text;
                entDischarge.RespRate = txtRespRate.Text; ;
                entDischarge.Pallor = txtPallor.Text;
                entDischarge.Oedema = txtOedema.Text;
                entDischarge.Cyanosis = txtCyanosis.Text;
                entDischarge.Clubbing = txtClubbing.Text;
                entDischarge.Icterus = txtIcterus.Text;
                entDischarge.Skin = txtSkin.Text;
                entDischarge.RespSystem = txtRespSystem.Text;
                entDischarge.CNS = txtCNS.Text;
                entDischarge.PerAbd = txtPerAbd.Text;
                entDischarge.CVS = txtCVS.Text;
                entDischarge.PreparedByName = Convert.ToString(Session["AdminName"]);

                int i = mobjDischarge.Save(entDischarge);
                if (i > 0)
                {
                    lblMsg.Text = "Record Saved Successfully";
                }
                else
                {
                    lblMsg.Text = "Record Not Saved";
                }
                GetDischargeList();
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
            MultiView1.SetActiveView(View2);
        }

        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                EntityMakeDischarge entDischarge = new EntityMakeDischarge();
                entDischarge.PatientId = Convert.ToInt32(Session["patientID"]);
                TimeSpan span = new TimeSpan(0,0, 0);
                entDischarge.TypeOfDischarge = Convert.ToString(ddlDischargeType.SelectedItem.Text);
                entDischarge.Diagnosis = FirstCharToUpper(txtDiagnosis.Text);
                entDischarge.SurgeryId = Convert.ToInt32(ddlNameOfSurgery.SelectedValue);
                entDischarge.NameOfSurgery = Convert.ToString(ddlNameOfSurgery.SelectedItem.Text);
                entDischarge.OperationalProcedure = FirstCharToUpper(txtOperatonalProce.Text);
                entDischarge.HistoryClinical = FirstCharToUpper(txtHistory.Text);
                entDischarge.XRay = txtXRay.Text;
                entDischarge.ECG = txtECG.Text;
                entDischarge.Others = txtOthers.Text;
                entDischarge.TreatmentInHospitalisation = FirstCharToUpper(txtTreatmentHospital.Text);
                entDischarge.AdviceOnDischarge = FirstCharToUpper(txtAdviceDischarge.Text);
                entDischarge.FollowUp = txtFollowUp.Text;
                entDischarge.DischargeReceiptDate = Convert.ToDateTime(txtDischargeDate.Text).Add(span);
                entDischarge.Haemogram = txtHaemogram.Text;
                entDischarge.BUL = txtBUL.Text;
                entDischarge.SCreat = txtSCreat.Text;
                entDischarge.SElect = txtSElect.Text;
                entDischarge.BSL = txtBSL.Text;
                entDischarge.UrineR = txtUrineR.Text;
                entDischarge.Pulse = txtPulse.Text;
                entDischarge.Temp = txtTemp.Text;
                entDischarge.BP = txtBP.Text;
                entDischarge.RespRate = txtRespRate.Text;
                entDischarge.Pallor = txtPallor.Text;
                entDischarge.Oedema = txtOedema.Text;
                entDischarge.Cyanosis = txtCyanosis.Text;
                entDischarge.Clubbing = txtClubbing.Text;
                entDischarge.Icterus = txtIcterus.Text;
                entDischarge.Skin = txtSkin.Text;
                entDischarge.RespSystem = txtRespSystem.Text;
                entDischarge.CNS = txtCNS.Text;
                entDischarge.PerAbd = txtPerAbd.Text;
                entDischarge.CVS = txtCVS.Text;
                entDischarge.PreparedByName = Convert.ToString(Session["AdminName"]);

                int i = mobjDischarge.Update(entDischarge, Convert.ToInt32(Session["DischargeId"]));
                if (i > 0)
                {
                    lblMsg.Text = "Record Updated Successfully";
                }
                else
                {
                    lblMsg.Text = "Record Not Updated";
                }
                GetDischargeList();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
            MultiView1.SetActiveView(View2);
        }

        public string FirstCharToUpper(string value)
        {
            char[] array = value.ToCharArray();
            // Handle the first letter in the string.
            if (array.Length >= 1)
            {
                if (char.IsLower(array[0]))
                {
                    array[0] = char.ToUpper(array[0]);
                }
            }
            // Scan through the letters, checking for spaces.
            // ... Uppercase the lowercase letters following spaces.
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i - 1] == ' ')
                {
                    if (char.IsLower(array[i]))
                    {
                        array[i] = char.ToUpper(array[i]);
                    }
                }
            }
            return new string(array);
        }

    }
}