<%@ Page Title="" Language="C#" MasterPageFile="~/Mstlogin.Master" AutoEventWireup="true" CodeBehind="frmLogin.aspx.cs" Inherits="MainProjectHos.frmLogin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/content.css" rel="stylesheet" type="text/css" />
    <script src="Notimoo/mootools-1.2-core.js" type="text/javascript"></script>
    <link href="Notimoo/notimoo-documented-1.1.css" rel="stylesheet" type="text/css" />
    <script src="Notimoo/notimoo-documented-1.1.js" type="text/javascript"></script>
    <link href="Notimoo/notimoo-v1.1.css" rel="stylesheet" type="text/css" />
    <script src="Notimoo/notimoo-v1.1.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function ResetControls() {
            var ddlUser = document.getElementById("ContentPlaceHolder1_ddlUserType");
            var UserName = document.getElementById("ContentPlaceHolder1_txtUserName");
            var Password = document.getElementById("ContentPlaceHolder1_txtPassword");
            ddlUser.selectedIndex = 0;
            UserName.value = "";
            Password.value = "";
            return false;
        }

        function show_alert(msg) {
            var show1 = new Notimoo({ locationVType: 'bottom', locationHType: 'right' });
            show1.show({ message: msg, sticky: false, visibleTime: 5000, width: 200 });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <table width="100%">
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Label ID="lblHeading" runat="server" Text="सेवाधाम हॉस्पिटल  "
                        Font-Names="Tahoma" Font-Bold="true" Font-Size="28px" ForeColor="white"></asp:Label>
                </td>
            </tr>
                
            <tr>
                <td align="center">
                    <asp:Label ID="Label5" runat="server" Text=" तळेगाव दाभाडे (स्टेशन), ता.मावळ ,जि.पुणे ४१०५०७"
                        Font-Names="Tahoma" Font-Bold="true" Font-Size="15px" ForeColor="white"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Panel ID="pnlLogin" runat="server" Width="30%" BackColor=" #f0f7fe" BorderColor="#003300"
                        BorderStyle="Solid" BorderWidth="1px">
                        <table width="100%">
                            <tr>
                                <td colspan="2">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                    <asp:Label ID="Label1" runat="server" Text="Login Detail" Font-Names="Tahoma" Font-Bold="true"
                                        Font-Size="14px" ForeColor="#3b3535"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label2" runat="server" Text="User Type :" Font-Names="Tahoma" Font-Size="12px"
                                        ForeColor="#3b3535"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlUserType" runat="server" Font-Names="Tahoma" Font-Size="12px"
                                        Width="150px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label3" runat="server" Text="User Name :" Font-Names="Tahoma" Font-Size="12px"
                                        ForeColor="#3b3535"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtUserName" runat="server" Font-Names="Tahoma" Font-Size="12px"
                                        Width="145px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label4" runat="server" Text="Password :" Font-Names="Tahoma" Font-Size="12px"
                                        ForeColor="#3b3535"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Font-Names="Tahoma"
                                        Font-Size="12px" Width="145px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                    <asp:Label ID="lblMessage" runat="server" Text="" Font-Names="Verdana" Font-Size="13px"
                                        ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                    <asp:Button ID="BtnLogin" runat="server" Text="Login" Font-Names="Tahoma" Font-Size="12px"
                                        BackColor="#3b3535" ForeColor="White" Font-Bold="true" OnClick="BtnLogin_Click"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                                    <asp:Button ID="BtnReset" runat="server" Text="Reset" Font-Names="Tahoma" Font-Size="12px"
                                        BackColor="#3b3535" Font-Bold="true" ForeColor="White" OnClientClick="return ResetControls();"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>