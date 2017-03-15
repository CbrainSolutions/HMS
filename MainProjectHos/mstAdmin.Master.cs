using MainProjectHos.Models.BusinessLayer;
using MainProjectHos.Models.DataLayer;
using MainProjectHos.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MainProjectHos
{
    public partial class mstAdmin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (SessionWrapper.UserName == string.Empty || SessionWrapper.UserType != "A")
            {
                //Response.Redirect("Default.aspx");
            }
            else
            {
                lblUserName.Text = "Welcome : " + SessionWrapper.UserName;
            }
            EntityLogin entLogin = (EntityLogin)Session["user"];
            if (entLogin != null)
            {
                EntityEmployee entEmp = new EntityEmployee();
                entEmp.EmpFirstName = new EmployeeBLL().GetEmpName(entLogin.UserName);
                lblUserName.Text = "Welcome : " + entLogin.UserType;
                Session["UserType12"] = entLogin.UserType;
                Session["AdminName"] = entEmp.EmpFirstName;
                if (!entLogin.UserType.Equals("admin", StringComparison.CurrentCultureIgnoreCase))
                {
                    AddForms(entEmp.EmpFirstName);
                    //Userauthenticate(entLogin);
                }
                else
                {
                    List<EntityFormMaster> entLstForms = new UserAuthenticationBLL().GetAllForms();
                    Session["AccessibleForm"] = entLstForms;
                }
            }
        }

        private void AddForms(string UserName)
        {
            List<EntityFormMaster> entLstForms = new UserAuthenticationBLL().ListofForms(UserName);
            Session["AccessibleForm"] = entLstForms;
            EntityFormMaster EntForms = new EntityFormMaster();
            Dictionary<string, MenuItem> DicMenu = new Dictionary<string, MenuItem>();
            foreach (MenuItem item in Menu1.Items)
            {
                int Count = 0;
                if (item.Value == "Main")
                {
                    item.Enabled = true;

                    foreach (MenuItem childItem in item.ChildItems)
                    {
                        if (entLstForms != null)
                        {
                            foreach (EntityFormMaster Lst in entLstForms)
                            {
                                if (childItem.NavigateUrl.Contains(Lst.FormName))
                                {
                                    DicMenu.Add(item.Text + Count.ToString(), childItem);
                                    Count++;
                                }
                            }
                        }
                    }
                }
            }
            foreach (MenuItem item in Menu1.Items)
            {
                if (item.Value == "Main")
                {
                    for (int i = 0; i < item.ChildItems.Count; i++)
                    {
                        item.ChildItems.Clear();
                    }
                }
            }

            foreach (MenuItem item in Menu1.Items)
            {
                if (item.Value == "Main")
                {
                    foreach (KeyValuePair<string, MenuItem> item1 in DicMenu)
                    {
                        if (item1.Key.Contains(item.Text))
                        {
                            item.ChildItems.Add(item1.Value);
                        }
                    }
                }
            }
        }

        protected void lnkLogOut_Click(object sender, EventArgs e)
        {
            EntityLogin UsersDat = (EntityLogin)Session["user"];

            for (int i = 0; i < Global.UsersData.Count; i++)
            {
                if (UsersDat.PKId == Global.UsersData[i].PKId)
                {
                    Global.UsersData.RemoveAt(i);
                }
            }

            Session["user"] = null;
            Session["UserType12"] = null;
            Session["AdminName"] = null;
            Response.Redirect("~/frmlogin.aspx", false);
        }
    }
}