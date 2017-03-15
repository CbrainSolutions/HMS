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
    public partial class frmLogin : System.Web.UI.Page
    {
        GetLoginBLL mobjGetLoginBLL = new GetLoginBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GetDepartments();
            }
        }
        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUserName.Text.Length > 0 && txtPassword.Text.Length > 0)
                {
                    if (Session["user"] != null)
                    {
                        DateTime dtExp = Commons.GetExpDate();
                        DateTime alertDate = dtExp.AddDays(-5);
                        if (DateTime.Now.Date.CompareTo(dtExp) >= 0)
                        {
                            CriticareHospitalDataContext objData = new CriticareHospitalDataContext();
                            objData.STP_BackUpLogin();
                            //Session["Alert"] = "Your trial version will expired soon. Please do payment before expire";
                        }
                        else
                        {
                            Response.Redirect("frmAdminMain.aspx", false);
                        }
                    }
                    else
                    {

                        EntityLogin entLogin = new EntityLogin();
                        entLogin.UserName = txtUserName.Text.Trim();
                        entLogin.Password = txtPassword.Text.Trim();
                        entLogin.UserType = ddlUserType.SelectedValue;
                        DataTable dt = mobjGetLoginBLL.GetLogin(entLogin);

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            if (Convert.ToString(dt.Rows[0]["Password"]) == txtPassword.Text)
                            {
                                entLogin.PKId = Convert.ToInt32(dt.Rows[0]["PKId"]);
                                Session["user"] = entLogin;

                                int Count = (from tbl in Global.UsersData
                                             where tbl.PKId == entLogin.PKId
                                             select tbl).Count();
                                if (Count > 0)
                                {
                                    lblMessage.Text = "This User Is Already Logined";
                                }
                                else
                                {
                                    Global.UsersData.Add(entLogin);
                                    entLogin.IsFirstLogin = mobjGetLoginBLL.CheckLogin(entLogin.UserName);
                                    if (entLogin.IsFirstLogin)
                                    {
                                        Response.Redirect("frmFirstLogin.aspx", false);
                                    }
                                    else
                                    {
                                        DateTime dtExp = Commons.GetExpDate();
                                        DateTime alertDate = dtExp.AddDays(-5);
                                        if (DateTime.Now.Date.CompareTo(dtExp) >= 0)
                                        {
                                            CriticareHospitalDataContext objData = new CriticareHospitalDataContext();
                                            objData.STP_BackUpLogin();
                                            //Session["Alert"] = "Your trial version will expired soon. Please do payment before expire";
                                        }
                                        else
                                        {
                                            Response.Redirect("frmAdminMain.aspx", false);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                Commons.ShowMessage("Invalid User Name or Password.", this.Page);
                                txtUserName.Focus();
                            }
                        }
                        else
                        {
                            Commons.ShowMessage("Invalid User Name or Password.", this.Page);
                            txtUserName.Focus();
                        }
                    }
                }
            }
            catch (System.Threading.ThreadAbortException)
            {
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmlogin - btnLogin_Click(object sender, EventArgs e)", ex);
            }
        }

        public void GetDepartments()
        {
            DataTable ldt = new DataTable();
            ldt = mobjGetLoginBLL.GetDepartments();
            ddlUserType.DataSource = ldt;
            ddlUserType.DataValueField = "UserType";
            ddlUserType.DataTextField = "DeptDesc";
            ddlUserType.DataBind();

            ListItem li = new ListItem();
            li.Text = "--Select Department--";
            li.Value = "0";
            ddlUserType.Items.Insert(0, li);
        }
    }
}