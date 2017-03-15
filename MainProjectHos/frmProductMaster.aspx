<%@ Page Title="" Language="C#" MasterPageFile="~/mstAdmin.Master" AutoEventWireup="true" CodeBehind="frmProductMaster.aspx.cs" Inherits="MainProjectHos.frmProductMaster" %>
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
            if (charCode > 31 && (charCode < 46 || charCode > 57 || charCode == 47)) {
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

        //var oldgridcolor;
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="width: 100%">
                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="View1" runat="server">
                        <table width="100%">
                            <tr>
                                <td align="center">
                                    <asp:Label ID="Label1" runat="server" Text="Product Master" Font-Names="Verdana"
                                        Font-Size="16px" Font-Bold="True" ForeColor="#3B3535"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:Button ID="BtnAddNewProduct" runat="server" Text="Add New" Font-Names="Verdana"
                                        Font-Size="13px" OnClick="BtnAddNewProduct_Click" BackColor="#3b3535" ForeColor="White"
                                        Width="80px" Style="border: 1px solid black" onmouseover="SetBtnMouseOver(this)"
                                        onmouseout="SetBtnMouseOut(this)" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel ID="pnlShow" BorderColor="Maroon" BorderWidth="1px" runat="server">
                                        <table width="100%">
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lblPageCount" runat="server" Text="" Font-Names="Verdana" Font-Size="11px"
                                                        ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="lblRowCount" runat="server" Text="" Font-Names="Verdana" Font-Size="11px"
                                                        ForeColor="#3b3535"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100%" colspan="3">
                                                    <asp:Panel ID="pnl" ScrollBars="Both" runat="server" Width="100%" Style="border-color: Green;
                                                        border-style: solid; border-width: 1px">
                                                        <asp:Label ID="lblMessage" runat="server" Text="" Font-Names="Verdana" Font-Size="11px"
                                                            ForeColor="Red"></asp:Label>
                                                        <asp:GridView ID="dgvProduct" runat="server" CellPadding="4" ForeColor="#333333"
                                                            DataKeyNames="ProductId" GridLines="Both" Font-Names="Verdana" Width="100%" Font-Size="Small"
                                                            AllowPaging="true" PageSize="20" AutoGenerateColumns="false" OnRowCommand="dgvProduct_RowCommand"
                                                            OnDataBound="dgvProduct_DataBound" OnPageIndexChanged="dgvProduct_PageIndexChanged"
                                                            OnPageIndexChanging="dgvProduct_PageIndexChanging" OnRowDataBound="dgvProduct_RowDataBound"
                                                            OnSelectedIndexChanged="dgvProduct_SelectedIndexChanged">
                                                            <FooterStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" />
                                                            <RowStyle BackColor="#E0F0E8" Font-Size="11px" Wrap="False" />
                                                            <PagerStyle BackColor="#3b3535" ForeColor="White" HorizontalAlign="Left" Font-Size="11px" />
                                                            <SelectedRowStyle BackColor="#006600" Font-Bold="True" ForeColor="#333333" />
                                                            <HeaderStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" Font-Size="12px"
                                                                Wrap="False" />
                                                            <EditRowStyle BackColor="#2461BF" />
                                                            <AlternatingRowStyle BackColor="White" Wrap="False" />
                                                            <Columns>
                                                                <asp:BoundField DataField="ProductId" HeaderText="ProductId" ReadOnly="True" SortExpression="ProductId" />
                                                                <asp:BoundField DataField="ProductName" HeaderText="Product Name" ReadOnly="True"
                                                                    SortExpression="ProductName" />
                                                                <asp:BoundField DataField="UOM" HeaderText="U.O.M" ReadOnly="True" SortExpression="UOM" />
                                                                <asp:BoundField DataField="SubUOM" HeaderText="SubU.O.M" ReadOnly="True" SortExpression="SubUOM" />
                                                                <asp:BoundField DataField="Price" HeaderText="Product Price" ReadOnly="True" SortExpression="Price" />
                                                                <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="ImageEdit" runat="server" ImageUrl="~/images/edit.jpg" Height="24px"
                                                                            Width="24px" OnClick="btnUpdate_Click" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
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
                    </asp:View>
                    <asp:View ID="View2" runat="server">
                        <table width="100%">
                            <tr style="width: 100%;">
                                <td style="width: 100%;">
                                    <asp:Panel ID="pnlGrid" runat="server" Width="100%" Style="text-align: center; background-color: #E0F0E8;
                                        height: 430px;">
                                        <table width="100%" style="border-color: Green; border-style: solid; border-width: 2px"
                                            cellpadding="0">
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="Panel2" runat="server" Width="99.7%" Style="text-align: center; background-color: #E0F0E8;
                                                        height: 420px;" BorderColor="Green" BorderStyle="Solid" BorderWidth="1px">
                                                        <table width="100%">
                                                            <tr>
                                                                <td align="center" colspan="2">
                                                                    <asp:Label ID="lblHeading" runat="server" Text="Product Master" Font-Names="Verdana"
                                                                        Font-Size="15px" Font-Bold="True" ForeColor="#3B3535"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" align="center">
                                                                    <asp:Label ID="lblMsg" Text="" ForeColor="Red" Font-Names="Verdana" Font-Size="11px"
                                                                        runat="server"></asp:Label>
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" style="width: 45%;">
                                                                    <asp:Label ID="lblProductId" runat="server" Text="Product Id :" Font-Names="Verdana"
                                                                        Font-Size="11px" ForeColor="#3B3535"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtProductId" runat="server" MaxLength="10" Font-Names="Verdana"
                                                                        ReadOnly="true" Font-Size="11px" Width="150px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="lblProductName" runat="server" Text="Product Name :" Font-Names="Verdana"
                                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtProductName" runat="server" MaxLength="100" Font-Names="Verdana"
                                                                        Font-Size="11px" Width="250px"></asp:TextBox><asp:RequiredFieldValidator ID="rfvProductName"
                                                                            runat="server" ForeColor="Red" ControlToValidate="txtProductName" Font-Size="13"
                                                                            ValidationGroup="Save">*</asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="lblUOM" runat="server" Text="U.O.M :" Font-Names="Verdana" Font-Size="11px"
                                                                        ForeColor="#3B3535"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtUOM" runat="server" MaxLength="100" Font-Names="Verdana" Font-Size="11px"
                                                                        Width="250px"></asp:TextBox><asp:RequiredFieldValidator ID="rfvUOM" runat="server"
                                                                            ForeColor="Red" ControlToValidate="txtUOM" Font-Size="13" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="lblSubUOM" runat="server" Text="Sub U.O.M :" Font-Names="Verdana"
                                                                        Font-Size="11px" ForeColor="#3B3535"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtSubUOM" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                        Width="130px" MaxLength="20" onkeypress="return KeyValid(event);"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvSubUOM" runat="server" ForeColor="Red" ControlToValidate="txtSubUOM"
                                                                        Font-Size="13" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="lblProductPrice" runat="server" Text="Product Price :" Font-Names="Verdana"
                                                                        Font-Size="11px" ForeColor="#3B3535"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtPrice" runat="server" Font-Names="Verdana" Font-Size="11px" Width="130px"
                                                                        OnKeyDown="return isNumber(event);" MaxLength="10" onkeypress="return KeyValid(event);"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvPrice" runat="server" ForeColor="Red" ControlToValidate="txtPrice"
                                                                        Font-Size="13" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr style="height: 10px;">
                                                                <td colspan="2">
                                                                </td>
                                                            </tr>
                                                            <tr align="center">
                                                                <td align="center" colspan="2">
                                                                    <asp:Button ID="BtnSave" runat="server" Text="Save" Font-Names="Verdana" Font-Size="12px"
                                                                        BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black;"
                                                                        onmouseover="SetBtnMouseOver(this)" onmouseout="SetBtnMouseOut(this)" OnClick="BtnSave_Click" />
                                                                    <asp:Button ID="btnUpdate" runat="server" Text="Update" Font-Names="Verdana" Font-Size="12px"
                                                                        OnClick="BtnEdit_Click" ValidationGroup="Update" BackColor="#3b3535" ForeColor="White"
                                                                        Width="80px" Style="border: 1px solid black" />
                                                                    <asp:Button ID="BtnClose" runat="server" Text="Close" Font-Names="Verdana" Font-Size="12px"
                                                                        BackColor="#3b3535" ForeColor="White" OnClick="BtnClose_Click" Width="80px" Style="border: 1px solid black" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
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
</asp:Content>
