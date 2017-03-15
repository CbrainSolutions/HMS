<%@ Page Title="" Language="C#" MasterPageFile="~/mstAdmin.Master" AutoEventWireup="true" CodeBehind="frmUserAuthentication.aspx.cs" Inherits="MainProjectHos.UserAuthentication.frmUserAuthentication" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/content.css" rel="stylesheet" type="text/css" />
    <script src="Notimoo/mootools-1.2-core.js" type="text/javascript"></script>
    <link href="Notimoo/notimoo-documented-1.1.css" rel="stylesheet" type="text/css" />
    <script src="Notimoo/notimoo-documented-1.1.js" type="text/javascript"></script>
    <link href="Notimoo/notimoo-v1.1.css" rel="stylesheet" type="text/css" />
    <script src="Notimoo/notimoo-v1.1.js" type="text/javascript"></script>
    <link href="Style/AdminStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function show_alert(msg) {
            var show1 = new Notimoo({ locationVType: 'bottom', locationHType: 'right' });
            show1.show({ message: msg, sticky: false, visibleTime: 5000, width: 200 });
        }

        function Refresh() {
            window.opener.location.reload();

        }
    </script>
    <script type="text/javascript">
        function hideModalPopup() {
            var modalPopupBehavior = $find('programmaticModalPopupBehavior');
            modalPopupBehavior.hide();
            return false;
        }

        function hideDeleteModalPopup() {
            var modalPopupBehavior = $find('DeleteprogrammaticModalPopupBehavior');
            modalPopupBehavior.hide();
            return false;
        }

        function hideEditModalPopup() {
            var modalPopupBehavior = $find('programmaticModalPopupBehaviorEdit');
            modalPopupBehavior.hide();
            return false;
        }

        function KeyValid(e) {
            var keynum
            var keychar
            var numcheck
            // For Internet Explorer
            if (window.event) {
                keynum = e.keyCode
            }
            // For Netscape/Firefox/Opera
            else if (e.which) {
                keynum = e.which
            }
            keychar = String.fromCharCode(keynum)

            //List of special characters you want to restrict
            if (keychar == "!" || keychar == "@" || keychar == "#"
    || keychar == "$" || keychar == "%" || keychar == "^" || keychar == "&"
    || keychar == "*" || keychar == "(" || keychar == ")"
    || keychar == "<" || keychar == ">") {
                return false;
            }
            else {
                return true;
            }
        }

        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }

        var oldgridcolor;
        function SetMouseOver(element) {
            oldgridcolor = element.style.backgroundColor;
            element.style.backgroundColor = '#B2C2D1';

        }
        function SetMouseOut(element) {
            element.style.backgroundColor = oldgridcolor;
            element.style.textDecoration = 'none';
        }

//        var oldgridcolor;
//        function SetBtnMouseOver(element) {
//            oldgridcolor = element.style.backgroundColor;
//            element.style.backgroundColor = 'Black';

        }
        function SetBtnMouseOut(element) {
            element.style.backgroundColor = oldgridcolor;
            element.style.textDecoration = 'none';
        }
    </script>
    <style type="text/css">
        .style6
        {
            width: 89px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="width: 99%">
                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="View1" runat="server">
                        <table width="100%">
                            <tr>
                                <td align="center">
                                    <asp:Label ID="Label1" runat="server" Text="User Authorisation" Font-Names="Verdana"
                                        Font-Size="16px" Font-Bold="true" ForeColor="#3b3535"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel ID="pnlShow" BorderColor="Maroon" BorderWidth="1px" runat="server" Style="border-color: Green;
                                        border-style: solid; border-width: 1px">
                                        <table width="100%">
                                            <tr>
                                                <td align="left" style="width: 11%;">
                                                    <asp:Label ID="lblPageCount" runat="server" Text="" Font-Names="Verdana" Font-Size="11px"
                                                        ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="right" style="width: 35%;">
                                                    <asp:Label ID="lblRowCount" runat="server" Text="" Font-Names="Verdana" Font-Size="11px"
                                                        ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="right" style="padding-right: 85px;">
                                                    <asp:Label ID="lblRowCount1" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                        ForeColor="#3b3535" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="lblDesignation" runat="server" Text="Designation : " Font-Names="Verdana"
                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:DropDownList ID="ddlDesignation" runat="server" Style="width: 150px;" AutoPostBack="True"
                                                        OnSelectedIndexChanged="ddlDesignation_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ForeColor="Red"
                                                        ControlToValidate="ddlDesignation" Font-Size="13" ValidationGroup="Save" ErrorMessage="*"
                                                        Display="Dynamic" InitialValue="0"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="lblEmployee" runat="server" Text="Employee Name :" Font-Names="Verdana"
                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:DropDownList ID="ddlEmployee" runat="server" Style="width: 150px;" AutoPostBack="True"
                                                        OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red"
                                                        ControlToValidate="ddlEmployee" Font-Size="13" ValidationGroup="Save" ErrorMessage="*"
                                                        Display="Dynamic" InitialValue="0"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr style="height: 7px;">
                                                <td colspan="3">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100%" colspan="3">
                                                    <asp:Label ID="lblMessage" runat="server" Text="" Font-Names="Verdana" Font-Size="11px"
                                                        ForeColor="Red"></asp:Label>
                                                    <div style="float: left; width: 47%; margin-left: 40px;">
                                                        <asp:Panel ID="pnl" ScrollBars="Both" runat="server" Width="400px" BorderColor="Green"
                                                            BorderStyle="Solid" BorderWidth="1px" Height="324px">
                                                            <asp:GridView ID="dgvAllForms" runat="server" CellPadding="4" ForeColor="#333333"
                                                                DataKeyNames="FormId" GridLines="Both" Font-Names="Verdana" Width="400px" Font-Size="Small"
                                                                AutoGenerateColumns="false" Style="font-family: System" OnDataBound="dgvAllForms_DataBound"
                                                                 OnPageIndexChanging="dgvAllForms_PageIndexChanging">
                                                                <FooterStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" />
                                                                <RowStyle BackColor="#E0F0E8" Font-Size="11px" Wrap="False" Font-Names="Verdana" />
                                                                <PagerStyle BackColor="#3b3535" ForeColor="White" HorizontalAlign="Left" Font-Size="11px" />
                                                                <SelectedRowStyle BackColor="#006600" Font-Bold="True" ForeColor="#333333" />
                                                                <HeaderStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" Font-Size="12px"
                                                                    Wrap="False" Font-Names="verdana" />
                                                                <EditRowStyle BackColor="#2461BF" />
                                                                <AlternatingRowStyle BackColor="White" Wrap="False" />
                                                                <Columns>
                                                                    <%--<asp:BoundField DataField="BedAllocId" HeaderText="Allocation Id" ReadOnly="True"
                                                                    SortExpression="BedAllocId" />--%>
                                                                    <asp:TemplateField HeaderText="Select" ItemStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chkSelect" Text="" runat="server" /></ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="FormId" HeaderText="Form Id" ReadOnly="True" SortExpression="FormId" />
                                                                    <asp:BoundField DataField="FormTitle" HeaderText="Form Title" ReadOnly="True" SortExpression="FormTitle" />
                                                                </Columns>
                                                            </asp:GridView>
                                                        </asp:Panel>
                                                    </div>
                                                    <div style="float: right; position: relative; width: 46%; top: 1px; left: -23px;">
                                                        <asp:Panel ID="Panel1" ScrollBars="Both" runat="server" Width="400px" BorderColor="Green"
                                                            BorderStyle="Solid" BorderWidth="1px" Height="324px">
                                                            <asp:GridView ID="dgvAllocEmp" runat="server" CellPadding="4" ForeColor="#333333"
                                                                DataKeyNames="FormId" GridLines="Both" Font-Names="Verdana" Width="400px" Font-Size="Small"
                                                                AutoGenerateColumns="false" Style="font-family: System">
                                                                <FooterStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" />
                                                                <RowStyle BackColor="#E0F0E8" Font-Size="11px" Wrap="False" Font-Names="Verdana" />
                                                                <PagerStyle BackColor="#3b3535" ForeColor="White" HorizontalAlign="Left" Font-Size="11px" />
                                                                <SelectedRowStyle BackColor="#006600" Font-Bold="True" ForeColor="#333333" />
                                                                <HeaderStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" Font-Size="12px"
                                                                    Wrap="False" Font-Names="verdana" />
                                                                <EditRowStyle BackColor="#2461BF" />
                                                                <AlternatingRowStyle BackColor="White" Wrap="False" />
                                                                <Columns>
                                                                    <%--<asp:BoundField DataField="BedAllocId" HeaderText="Allocation Id" ReadOnly="True"
                                                                    SortExpression="BedAllocId" />--%>
                                                                    <%--   <asp:TemplateField HeaderText="Select" ItemStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chkSelect" Text="" runat="server" /></ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>--%>
                                                                    <asp:BoundField DataField="FormId" HeaderText="Form Id" ReadOnly="True" SortExpression="Emp_Id" />
                                                                    <asp:BoundField DataField="FormTitle" HeaderText="Form Title" ReadOnly="True" SortExpression="FormTitle" />
                                                                </Columns>
                                                            </asp:GridView>
                                                        </asp:Panel>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td align="center" colspan="3">
                                                    <asp:Button ID="BtnAdd" runat="server" Text="Allocate" Font-Names="Verdana" Font-Size="14px"
                                                        OnClick="BtnAdd_Click" BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black" />
                                                    <asp:Button ID="BtnSave" runat="server" Text="Save" Font-Names="Verdana" Font-Size="14px"
                                                        ValidationGroup="Save" BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black"
                                                        OnClick="BtnSave_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                </asp:MultiView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <input id="hdnPanel" type="hidden" runat="server" value="none" />
</asp:Content>
