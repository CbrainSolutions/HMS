<%@ Page Title="" Language="C#" MasterPageFile="~/mstAdmin.Master" AutoEventWireup="true" CodeBehind="frmSupplierMaster.aspx.cs" Inherits="MainProjectHos.frmSupplierMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   <style type="text/css">
        .unwatermarked
        {
            height: 18px;
            width: 148px;
        }
        .watermarked
        {
            height: 20px;
            width: 150px;
            padding: 2px 0 0 2px;
            border: 1px solid #BEBEBE;
            background-color: #F0F8FF;
            color: gray;
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
                                    <asp:Label ID="Label1" runat="server" Text="Supplier Master" Font-Names="Verdana"
                                        Font-Size="16px" Font-Bold="true" ForeColor="#3b3535"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="width: 15%;">
                                    <table border="2px" cellpadding="0" cellspacing="0" width="860px" style="height: 40px;">
                                        <tr>
                                            <td align="center" style="border-right: none; width: 575px;">
                                                <asp:TextBox ID="txtSearch" runat="server" Font-Bold="true" Font-Names="Verdana"
                                                    Font-Size="13px" Width="580px" />
                                            </td>
                                            <td align="center" style="border-left: none; border-right: none; width: 60px;">
                                                <asp:Button ID="btnSearch" runat="server" BackColor="#3b3535" Font-Names="Verdana"
                                                    Font-Size="13px" ForeColor="White" OnClick="btnSearch_Click" Style="border: 1px solid black"
                                                    Text="Search" Width="80px" />
                                            </td>
                                            <td align="center" style="border-left: none; border-right: none; width: 90px;">
                                                <asp:Button ID="btnReset" runat="server" BackColor="#3b3535" Font-Names="Verdana"
                                                    Font-Size="13px" ForeColor="White" OnClick="btnReset_Click" Style="border: 1px solid black"
                                                    Text="Reset" Width="80px" />
                                            </td>
                                            <td align="center" style="border-left: none; width: 60px; padding-right: 4px;">
                                                <asp:Button ID="BtnAddNewEmp" runat="server" Text="Add New" Font-Names="Verdana"
                                                    Font-Size="13px" OnClick="BtnAddNewSupplier_Click" BackColor="#3b3535" ForeColor="White"
                                                    Width="80px" Style="border: 1px solid black" onmouseover="SetBtnMouseOver(this)"
                                                    onmouseout="SetBtnMouseOut(this)" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr style="width: 100%">
                                <td style="width: 100%">
                                    <asp:Panel ID="pnlShow" BorderColor="Maroon" BorderWidth="1px" runat="server" Style="border-color: Green;
                                        border-style: solid; border-width: 1px">
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
                                            <tr style="width: 100%">
                                                <td align="center" style="width: 100%" colspan="3">
                                                    <asp:Label ID="lblMessage" runat="server" Text="" Font-Names="Verdana" Font-Size="11px"
                                                        ForeColor="Red"></asp:Label>
                                                    <asp:GridView ID="dgvSupplier" runat="server" CellPadding="4" ForeColor="#333333"
                                                        DataKeyNames="SupplierCode" GridLines="Both" Font-Names="Verdana" Width="1000px"
                                                        Font-Size="Small" AllowPaging="true" PageSize="15" AutoGenerateColumns="false"
                                                        OnPageIndexChanged="dgvSupplier_PageIndexChanged"
                                                        OnPageIndexChanging="dgvSupplier_PageIndexChanging">
                                                        <FooterStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" />
                                                        <RowStyle BackColor="#E0F0E8" Font-Size="11px" Wrap="False" />
                                                        <PagerStyle BackColor="#3b3535" ForeColor="White" HorizontalAlign="Left" Font-Size="11px" />
                                                        <SelectedRowStyle BackColor="#006600" Font-Bold="True" ForeColor="#333333" />
                                                        <HeaderStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" Font-Size="12px"
                                                            Wrap="False" />
                                                        <EditRowStyle BackColor="#2461BF" />
                                                        <AlternatingRowStyle BackColor="White" Wrap="False" />
                                                        <Columns>
                                                            <asp:BoundField DataField="SupplierCode" HeaderText="Supplier Code" ReadOnly="True" SortExpression="SupplierCode" />
                                                            <asp:BoundField DataField="SupplierName" HeaderText="Supplier Name" ReadOnly="True"
                                                                SortExpression="SupplierName" />
                                                            <asp:BoundField DataField="Address" HeaderText="Address" ReadOnly="True" SortExpression="Address" />
                                                            <asp:BoundField DataField="PhoneNo" HeaderText="Phone No" ReadOnly="True" SortExpression="PhoneNo" />
                                                            <asp:BoundField DataField="MobileNo" HeaderText="Mobile No" ReadOnly="True" SortExpression="MobileNo" />
                                                            <asp:BoundField DataField="VATCSTNo" HeaderText="VATCSTNo" ReadOnly="True" SortExpression="VATCSTNo" />
                                                            <asp:BoundField DataField="ExciseNo" HeaderText=" ExciseNo" ReadOnly="True" SortExpression="ExciseNo" />
                                                            <asp:BoundField DataField="Email" HeaderText="Emailid" ReadOnly="True" SortExpression="Email" />
                                                            <asp:BoundField DataField="ServiceTaxNo" HeaderText="Service Tax No" ReadOnly="True"
                                                                SortExpression="ServiceTaxNo" />
                                                            <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="ImageEdit" runat="server" ImageUrl="~/images/edit.jpg" Height="24px"
                                                                        Width="24px" OnClick="btnUpdate_Click" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
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
                            <tr width="100%">
                                <td width="100%">
                                    <asp:Panel ID="pnlGrid" runat="server" Width="100%" Style="text-align: center; background-color: #E0F0E8;
                                        height: 100%;">
                                        <table width="100%" style="border-color: Green; border-style: solid; border-width: 2px"
                                            cellpadding="0">
                                            <tr style="width: 100%">
                                                <td style="width: 100%">
                                                    <asp:Panel ID="Panel2" runat="server" Width="100%" Style="text-align: center; background-color: #E0F0E8;
                                                        height: 390px;" BorderColor="Green" BorderStyle="Solid" BorderWidth="1px">
                                                        <table width="100%">
                                                            <tr>
                                                                <td align="center" colspan="2">
                                                                    <asp:Label ID="lblHeading" runat="server" Text="Supplier Master" Font-Names="Verdana"
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
                                                                <td align="right" style="width: 45%;">
                                                                    <asp:Label ID="lblSupplierCode" runat="server" Text="Supplier Code :" Font-Names="Verdana"
                                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtSupplierCode" runat="server" MaxLength="10" Font-Names="Verdana"
                                                                        ReadOnly="true" Font-Size="11px" Width="150px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="lblSupplierName" runat="server" Text="Supplier Name :" Font-Names="Verdana"
                                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtSupplierName" runat="server" MaxLength="100" Font-Names="Verdana"
                                                                        Font-Size="11px" Width="250px"></asp:TextBox><asp:RequiredFieldValidator ID="rfvSupplierName"
                                                                            runat="server" ForeColor="Red" ControlToValidate="txtSupplierName" Font-Size="13"
                                                                            ValidationGroup="Save" ErrorMessage="*" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="lblAddress" runat="server" Text="Address :" Font-Names="Verdana" Font-Size="11px"
                                                                        ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtAddress" runat="server" MaxLength="100" Font-Names="Verdana"
                                                                        Font-Size="11px" Width="250px"></asp:TextBox><asp:RequiredFieldValidator ID="rfvAddress"
                                                                            runat="server" ForeColor="Red" ControlToValidate="txtAddress" Font-Size="13"
                                                                            ValidationGroup="Save" ErrorMessage="*" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="lblPhoneNo" runat="server" Text="Phone No :" Font-Names="Verdana"
                                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtPhoneNo" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                        Width="130px" MaxLength="14" onkeypress="return KeyValid(event);"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                </td>
                                                                <td align="left">
                                                                    <asp:RegularExpressionValidator ID="regPhNo" runat="server" ForeColor="Red" ControlToValidate="txtPhoneNo"
                                                                        Display="Dynamic" SetFocusOnError="true" Font-Size="11px" Font-Names="Verdana"
                                                                        ValidationGroup="Save" ErrorMessage="Invalid Phone No." ValidationExpression="[0-9]{14}"></asp:RegularExpressionValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="lblMobileNo" runat="server" Text="Mobile No :" Font-Names="Verdana"
                                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtMobileNo" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                        Width="130px" MaxLength="10"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                </td>
                                                                <td align="left">
                                                                    <asp:RegularExpressionValidator ID="RegMobNo" runat="server" ForeColor="Red" ControlToValidate="txtMobileNo"
                                                                        Display="Dynamic" SetFocusOnError="true" Font-Size="11px" Font-Names="Verdana"
                                                                        ValidationGroup="Save" ErrorMessage="Invalid Mobile No." ValidationExpression="[0-9]{10}"></asp:RegularExpressionValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="lblVATCSTNo" runat="server" Text="VATCST No :" Font-Names="Verdana"
                                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtVATCSTNo" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                        Width="130px" MaxLength="50" onkeypress="return KeyValid(event);"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="lblExciseNo" runat="server" Text="Excise No :" Font-Names="Verdana"
                                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtExciseNo" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                        Width="130px" MaxLength="50" onkeypress="return KeyValid(event);"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="lblEmail" runat="server" Text="Email ID :" Font-Names="Verdana" Font-Size="11px"
                                                                        ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtEmail" runat="server" Font-Names="Verdana" Font-Size="11px" Width="160px"
                                                                        MaxLength="50"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                </td>
                                                                <td align="left">
                                                                    <asp:RegularExpressionValidator ID="regMail" runat="server" ForeColor="Red" ControlToValidate="txtEmail"
                                                                        Display="Dynamic" SetFocusOnError="true" Font-Size="11px" Font-Names="Verdana"
                                                                        ValidationGroup="Save" ErrorMessage="Invalid Mail Id." ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                                                                    </asp:RegularExpressionValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="lblServiceTaxNo" runat="server" Text="Service Tax No :" Font-Names="Verdana"
                                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtServiceTaxNo" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                        Width="130px" MaxLength="10" onkeypress="return KeyValid(event);"></asp:TextBox>
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
                                                                        BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black"
                                                                        ValidationGroup="Save" OnClick="BtnSave_Click" />
                                                                    <asp:Button ID="btnUpdate" runat="server" BackColor="#3b3535" Font-Names="Verdana"
                                                                        Font-Size="12px" ForeColor="White" OnClick="BtnEdit_Click" Style="border: 1px solid black"
                                                                        Text="Update" Width="80px" ValidationGroup="Save" />
                                                                    <asp:Button ID="BtnClose" runat="server" Text="Close" Font-Names="Verdana" Font-Size="12px"
                                                                        BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black"
                                                                        OnClick="btnClose_Click" />
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
    <input id="hdnPanel" type="hidden" runat="server" value="none" />
</asp:Content>
