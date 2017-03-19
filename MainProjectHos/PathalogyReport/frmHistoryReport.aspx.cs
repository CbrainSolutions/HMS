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
using System.IO;

namespace MainProjectHos.PathalogyReport
{
    public partial class frmHistoryReport : System.Web.UI.Page
    {
        PatientInvoiceBLL mobjDeptBLL = new PatientInvoiceBLL();
        PathologyBLL mobjPath = new PathologyBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] != null)
            {
                EntityLogin entLogin = (EntityLogin)Session["user"];

                List<EntityFormMaster> entFormAcc = (List<EntityFormMaster>)Session["AccessibleForm"];
                int Access = (from tbl in entFormAcc
                              where tbl.FormName.Contains("frmHistoryReport")
                              select tbl).Count();

                if (Access > 0)
                {
                    if (!Page.IsPostBack)
                    {
                        Session["MyFlag"] = string.Empty;
                        Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                        SelectPatientHistory();
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {

                if (!string.IsNullOrEmpty(txtSearch.Text))
                {
                    SearchPatientHistory(txtSearch.Text);
                }
                else
                {
                    lblMessage.Text = "Please Enter Patient Name OR Patient Type To Search";
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
            txtSearch.Text = string.Empty;
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            MultiView1.SetActiveView(View1);
        }

        protected void btnBack1_Click(object sender, EventArgs e)
        {
            MultiView1.SetActiveView(View1);
        }

        protected void btnInvoicePrint_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton imgEdit = (ImageButton)sender;
                GridViewRow cnt = (GridViewRow)imgEdit.NamingContainer;
                Session["Patient_ID"] = cnt.Cells[0].Text;
                int BillNo = mobjDeptBLL.GetBillNo(Convert.ToInt32(Session["Patient_ID"]));
                if (BillNo > 0)
                {
                    Session["BILLNo"] = BillNo;
                    Session["ReportType"] = "Invoice";
                    Response.Redirect("~/PathalogyReport/PathologyReport.aspx", false);
                }
                else
                {
                    lblMessage.Text = "This Patient Don't Have Invoice";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void btnShowTest_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton imgEdit = (ImageButton)sender;
                GridViewRow cnt = (GridViewRow)imgEdit.NamingContainer;
                Session["Patient_ID"] = cnt.Cells[0].Text;
                PathologyBLL Pathology = new PathologyBLL();
                List<EntityPathology> lst = Pathology.SearchPathologyDetails(Convert.ToString(Session["Patient_ID"]));
                if (lst.Count > 0)
                {
                    dgvTest.DataSource = lst;
                    dgvTest.DataBind();
                    MultiView1.SetActiveView(View2);
                    lblMessage.Text = string.Empty;
                }
                else
                {
                    lblMessage.Text = "No Test Done";
                    MultiView1.SetActiveView(View1);
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void btnShowReport_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton imgEdit = (ImageButton)sender;
                GridViewRow cnt = (GridViewRow)imgEdit.NamingContainer;
                Session["Patient_ID"] = cnt.Cells[1].Text;
                DocumentBLL Pathology = new DocumentBLL();
                List<EntityDocument> lst = Pathology.SearchDocumentDetails(Convert.ToString(Session["Patient_ID"]));
                if (lst.Count > 0)
                {
                    dgvDocument.DataSource = lst;
                    dgvDocument.DataBind();
                    MultiView1.SetActiveView(View3);
                    lblMessage.Text = string.Empty;
                }
                else
                {
                    lblMessage.Text = "No Any Report";
                    MultiView1.SetActiveView(View1);
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void dgvDocument_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                DataTable ldt = new DataTable();
                DocumentBLL mobjDocBLL = new DocumentBLL();
                GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;

                if (e.CommandName == "DownloadFile")
                {
                    Label lblPKId = (Label)gvr.FindControl("lblPKId");
                    Label lblDocument = (Label)gvr.FindControl("lblDocumentName");

                    int lintPKId = Commons.ConvertToInt(lblPKId.Text);
                    string lstrFullName = lblDocument.Text;
                    ldt = mobjDocBLL.GetDocumentByName(lintPKId, lstrFullName);

                    if (ldt.Rows.Count > 0 && ldt != null)
                    {
                        Response.Clear();
                        Byte[] sBytes = (Byte[])ldt.Rows[0]["FileContent"];
                        MemoryStream ms = new MemoryStream(sBytes);
                        Response.Charset = "";
                        Response.ContentType = "application/pdf";
                        Response.AddHeader("content-disposition", "attachment;filename=Document.pdf");
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
            //catch (Exception ex)
            //{
            //    Commons.FileLog("frmViewManualQuotation -  dgvCustomerDetail_RowCommand(object sender, GridViewCommandEventArgs e)", ex);
            //}
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            ImageButton imgEdit = (ImageButton)sender;
            GridViewRow row = (GridViewRow)imgEdit.NamingContainer;
            Session["LabId"] = Convert.ToInt32(dgvTest.DataKeys[row.RowIndex].Value);
            Session["ReportType"] = "Pathology";
            Response.Redirect("~/PathalogyReport/PathologyReport.aspx", false);
        }

        private void SearchPatientHistory(string Prefix)
        {
            try
            {
                PatientMasterBLL objOPDPatient = new PatientMasterBLL();
                List<EntityInvoiceDetails> lst = objOPDPatient.SearchForPatientHistory(Prefix);
                if (lst != null)
                {
                    dgvTestParameter.DataSource = lst;
                    dgvTestParameter.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private void SelectPatientHistory()
        {
            try
            {
                PatientMasterBLL PatHist = new PatientMasterBLL();
                List<EntityInvoiceDetails> lst = PatHist.ShowPatientHistory();
                if (lst != null)
                {
                    dgvTestParameter.DataSource = lst;
                    dgvTestParameter.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void dgvTestParameter_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (!e.Row.Cells[0].Text.Equals("Patient Id", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (!e.Row.Cells[2].Text.Equals("&nbsp;", StringComparison.CurrentCultureIgnoreCase))
                    {
                        if (e.Row.Cells[3].Text.Equals("01/01/0001", StringComparison.CurrentCultureIgnoreCase))
                        {
                            e.Row.Cells[3].Text = string.Empty;
                        }
                        else
                        {

                        }
                    }
                    else
                    {

                    }
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
    }
}