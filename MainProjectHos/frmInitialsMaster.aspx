﻿<%@ Page Title="" Language="C#" MasterPageFile="~/mstAdmin.Master" AutoEventWireup="true" CodeBehind="frmInitialsMaster.aspx.cs" Inherits="MainProjectHos.frmInitialsMaster" %>
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
            window.location.reload();

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

        var oldgridcolor;
        function SetBtnMouseOver(element) {
            oldgridcolor = element.style.backgroundColor;
            element.style.backgroundColor = 'Black';

        }
        function SetBtnMouseOut(element) {
            element.style.backgroundColor = oldgridcolor;
            element.style.textDecoration = 'none';
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="width: 99%">
        <asp:Panel ID="pnlAddNew" runat="server">
            <table width="100%">
                <tr>
                    <td align="center">
                        <asp:Label ID="Label1" runat="server" Text="Initials Master" Font-Names="Verdana"
                            Font-Size="16px" Font-Bold="true" ForeColor="#3b3535"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 15%;">
                        <asp:Button ID="BtnAddNewInitial" runat="server" Text="Add New" Font-Names="Verdana"
                            Font-Size="13px" BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black"
                            onmouseover="SetBtnMouseOver(this)" onmouseout="SetBtnMouseOut(this)" OnClick="BtnAddNewInitial_Click" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    <table width="100%">
        <tr>
            <td>
                <asp:Panel ID="pnlShow" BorderColor="Maroon" BorderWidth="1px" runat="server" Style="border-color: Green;
                    border-style: solid; border-width: 1px">
                    <table width="100%">
                        <tr>
                            <td align="left">
                                <asp:Label ID="lblPageCount" runat="server" Text="" Font-Names="Verdana" Font-Size="11px"
                                    ForeColor="#3b3535"></asp:Label>
                            </td>
                            <td align="left">
                                &nbsp;
                            </td>
                            <td align="right">
                                <asp:Label ID="lblRowCount" runat="server" Font-Names="Verdana" Font-Size="11px"
                                    ForeColor="#3b3535" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%" colspan="3">
                                <asp:Panel ID="pnl"  runat="server" Width="100%">
                                    <asp:Label ID="lblMessage" runat="server" Text="" Font-Names="Verdana" Font-Size="11px"
                                        ForeColor="Red"></asp:Label>
                                    <asp:GridView ID="dgvInitials" runat="server" CellPadding="4" ForeColor="#333333"
                                        BorderColor="Green" BorderStyle="Solid" BorderWidth="1px" DataKeyNames="InitialCode"
                                        GridLines="Both" Font-Names="Verdana" Width="100%" Font-Size="Small" AllowPaging="true"
                                        PageSize="20" AutoGenerateColumns="false" OnRowCommand="dgvInitials_RowCommand"
                                        OnDataBound="dgvInitials_DataBound" OnPageIndexChanged="dgvInitials_PageIndexChanged"
                                        OnPageIndexChanging="dgvInitials_PageIndexChanging" OnRowDataBound="dgvInitials_RowDataBound">
                                        <FooterStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" />
                                        <RowStyle BackColor="#E0F0E8" Font-Size="11px" Wrap="False" />
                                        <PagerStyle BackColor="#3b3535" ForeColor="White" HorizontalAlign="Left" Font-Size="11px" />
                                        <SelectedRowStyle BackColor="#006600" Font-Bold="True" ForeColor="#333333" />
                                        <HeaderStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" Font-Size="12px"
                                            Wrap="False" />
                                        <EditRowStyle BackColor="#2461BF" />
                                        <AlternatingRowStyle BackColor="White" Wrap="False" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Initial Code" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkInitialCode" runat="server" CausesValidation="false" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>"
                                                        CommandName="EditInitial" ForeColor="Blue" Font-Underline="false" Text='<%#Eval("InitialCode") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="InitialDesc" HeaderText="Initial Description" ReadOnly="True"
                                                SortExpression="DeptDesc" />
                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <asp:Button runat="server" ID="hiddenTargetControlForModalPopup" Style="display: none" />
                <asp:Panel ID="pnlGrid" runat="server" Width="500px" Style="text-align: center; background-color: #E0F0E8;
                    height: 200px; display: none;">
                    <table width="100%" style="border-color: Green; border-style: solid; border-width: 2px"
                        cellpadding="0">
                        <tr>
                            <td>
                                <asp:Panel ID="Panel2" runat="server" Width="99.7%" Style="text-align: center; background-color: #E0F0E8;
                                    height: 190px;" BorderColor="Green" BorderStyle="Solid" BorderWidth="1px">
                                    <table width="100%">
                                        <tr>
                                            <td align="center" colspan="2">
                                                <asp:Label ID="lblHeading" runat="server" Text="Initials Master" Font-Names="Verdana"
                                                    Font-Size="15px" Font-Bold="true" ForeColor="#3b3535"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="width: 45%;">
                                                <asp:Label ID="lblInitialCode" runat="server" Text="Initial Code :" Font-Names="Verdana"
                                                    Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtInitialCode" runat="server" MaxLength="50" ReadOnly="true" Font-Names="Verdana"
                                                    Font-Size="11px" Width="80px" Font-Bold="true"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="lblInitialName" runat="server" Text="Initial Desciption :" Font-Names="Verdana"
                                                    Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtInitialDesc" runat="server" MaxLength="50" Font-Names="Verdana"
                                                    Font-Size="11px" Width="170px"></asp:TextBox><asp:RequiredFieldValidator ID="rfvInitialDesc"
                                                        runat="server" ForeColor="Red" ControlToValidate="txtInitialDesc" Font-Size="13"
                                                        ValidationGroup="Save">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr align="center">
                                            <td align="center" colspan="2">
                                                <asp:Button ID="BtnSave" runat="server" Text="Save" Font-Names="Verdana" Font-Size="12px"
                                                    ValidationGroup="Save" BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black"
                                                    onmouseover="SetBtnMouseOver(this)" onmouseout="SetBtnMouseOut(this)" OnClick="BtnSave_Click" />
                                                <asp:Button ID="BtnClose" runat="server" Text="Close" Font-Names="Verdana" Font-Size="12px"
                                                    OnClientClick="return hideModalPopup();" BackColor="#3b3535" ForeColor="White"
                                                    Width="80px" Style="border: 1px solid black" onmouseover="SetBtnMouseOver(this)"
                                                    onmouseout="SetBtnMouseOut(this)" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Button runat="server" ID="hiddenTargetControlForModalPopupEdit" Style="display: none" />
                <cc:ModalPopupExtender runat="server" ID="programmaticModalPopupEdit" BehaviorID="programmaticModalPopupBehaviorEdit"
                    TargetControlID="hiddenTargetControlForModalPopupEdit" PopupControlID="pnlEditGrid"
                    BackgroundCssClass="modalBackground" DropShadow="true" PopupDragHandleControlID="programmaticPopupDragHandleEdit"
                    RepositionMode="RepositionOnWindowScroll">
                </cc:ModalPopupExtender>
                <asp:Panel ID="pnlEditGrid" runat="server" Width="500px" Style="text-align: center;
                    background-color: #E0F0E8; height: 200px; ">
                    <table width="100%" style="border-color: Green; border-style: solid; border-width: 2px"
                        cellpadding="0">
                        <tr>
                            <td>
                                <asp:Panel ID="Panel3" runat="server" Width="99.7%" Style="text-align: center; background-color: #E0F0E8;
                                    height: 190px;" BorderColor="Green" BorderStyle="Solid" BorderWidth="1px">
                                    <table width="100%">
                                        <tr>
                                            <td align="center" colspan="2">
                                                <asp:Label ID="Label2" runat="server" Text="Update Initial Master" Font-Names="Verdana"
                                                    Font-Size="15px" Font-Bold="true" ForeColor="#3b3535"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" align="center">
                                                <asp:Label ID="lblMsg" runat="server" Text="" Font-Names="Verdana" Font-Size="11px"
                                                    ForeColor="Red"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label3" runat="server" Text="Initial Code :" Font-Names="Verdana"
                                                    Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                            </td>
                                            <td align="left" colspan="5">
                                                <asp:TextBox ID="txtEditInitialCode" runat="server" MaxLength="50" ReadOnly="true"
                                                    Font-Names="Verdana" Font-Size="11px" Width="80px" Font-Bold="true"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label4" runat="server" Text="Initial Name :" Font-Names="Verdana"
                                                    Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtEditInitialDesc" runat="server" MaxLength="50" Font-Names="Verdana"
                                                    Font-Size="11px" Width="120px"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator4"
                                                        runat="server" ForeColor="Red" ControlToValidate="txtEditInitialDesc" Font-Size="13"
                                                        ValidationGroup="Edit">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr align="center">
                                            <td align="center" colspan="6">
                                                <asp:Button ID="BtnEdit" runat="server" Text="Update" Font-Names="Verdana" Font-Size="12px"
                                                    ValidationGroup="Edit" BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black"
                                                    onmouseover="SetBtnMouseOver(this)" onmouseout="SetBtnMouseOut(this)" OnClick="BtnEdit_Click" />
                                                <asp:Button ID="BtnEditClose" runat="server" Text="Close" Font-Names="Verdana" Font-Size="12px"
                                                    OnClientClick="return hideEditModalPopup();" BackColor="#3b3535" ForeColor="White"
                                                    Width="80px" Style="border: 1px solid black" onmouseover="SetBtnMouseOver(this)"
                                                    onmouseout="SetBtnMouseOut(this)" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Button runat="server" ID="hiddenTargetControlForModalPopupDelete" Style="display: none" />
                
                <asp:Panel ID="pnlDelete" runat="server" Width="500px" Style="text-align: center;
                    background-color: #E0F0E8; display: none;" BorderColor="Green" BorderStyle="Solid"
                    BorderWidth="2px">
                    <%-- <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>--%>
                    <table width="100%">
                        <tr align="center">
                            <td align="center" colspan="6" style="font-family: Verdana; font-size: 15px; color: Green;
                                font-weight: bold;">
                                <asp:Label ID="Label10" runat="server" Text="Do You Want To Delete Record ?" Font-Names="Verdana"
                                    Font-Size="15px" ForeColor="#3b3535"></asp:Label>
                            </td>
                        </tr>
                        <tr align="center">
                            <td align="center" colspan="6">
                                <asp:Button ID="BtnDeleteOk" runat="server" Text="Delete" Font-Names="Verdana" Font-Size="12px"
                                    CausesValidation="false" BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black"
                                    onmouseover="SetBtnMouseOver(this)" onmouseout="SetBtnMouseOut(this)" OnClick="BtnDeleteOk_Click" />
                                <asp:Button ID="Button2" runat="server" Text="Close" Font-Names="Verdana" Font-Size="12px"
                                    OnClientClick="return hideDeleteModalPopup();" BackColor="#3b3535" ForeColor="White"
                                    Width="80px" Style="border: 1px solid black" onmouseover="SetBtnMouseOver(this)"
                                    onmouseout="SetBtnMouseOut(this)" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <asp:Label ID="lblMessage1" runat="server" Text="" Font-Names="Verdana" Font-Size="11px"
                                    ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <input id="hdnPanel" type="hidden" runat="server" value="none" />
</asp:Content>
