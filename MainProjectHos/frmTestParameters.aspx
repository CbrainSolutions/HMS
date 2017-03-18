<%@ Page Title="" Language="C#" MasterPageFile="~/mstAdmin.Master" AutoEventWireup="true" CodeBehind="frmTestParameters.aspx.cs" Inherits="MainProjectHos.frmTestParameters" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="width: 99%">
                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="View1" runat="server">
                        <table width="100%">
                            <tr>
                                <td align="center">
                                    <asp:Label ID="Label1" runat="server" Text="Test Parameters" Font-Names="Verdana"
                                        Font-Size="16px" Font-Bold="true" ForeColor="#3b3535"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 15%;">
                                    <asp:Button ID="BtnAddNewDept" runat="server" Text="Add New" Font-Names="Verdana"
                                        Font-Size="13px" OnClick="BtnAddNewDept_Click" BackColor="#3b3535" ForeColor="White"
                                        Width="80px" Style="border: 1px solid black" />
                                </td>
                            </tr>
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
                                                <td align="right">
                                                    <asp:Label ID="lblRowCount" runat="server" Text="" Font-Names="Verdana" Font-Size="11px"
                                                        ForeColor="#3b3535"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100%" colspan="3">
                                                    <asp:Label ID="lblMessage" runat="server" Text="" Font-Names="Verdana" Font-Size="11px"
                                                        ForeColor="Red"></asp:Label>
                                                    <asp:Panel ID="pnl"  runat="server" Width="1010px" BorderColor="Green"
                                                        BorderStyle="Solid" BorderWidth="1px">
                                                        <asp:GridView ID="dgvTestParameter" runat="server" CellPadding="4" ForeColor="#333333"
                                                            DataKeyNames="TstParID" GridLines="Both" Font-Names="Verdana" Width="100%" Font-Size="Small"
                                                            AllowPaging="true" PageSize="15" AutoGenerateColumns="false" 
                                                             OnPageIndexChanged="dgvTestParameter_PageIndexChanged"
                                                            OnPageIndexChanging="dgvTestParameter_PageIndexChanging" >
                                                            <FooterStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" />
                                                            <RowStyle BackColor="#E0F0E8" Font-Size="11px" Wrap="False" />
                                                            <PagerStyle BackColor="#3b3535" ForeColor="White" HorizontalAlign="Left" Font-Size="11px" />
                                                            <SelectedRowStyle BackColor="#006600" Font-Bold="True" ForeColor="#333333" />
                                                            <HeaderStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" Font-Size="12px"
                                                                Wrap="False" />
                                                            <EditRowStyle BackColor="#2461BF" />
                                                            <AlternatingRowStyle BackColor="White" Wrap="False" />
                                                            <Columns>
                                                                <asp:BoundField DataField="TstParID" HeaderText="Test Parameter Id" ReadOnly="True"
                                                                    SortExpression="TstParID" />
                                                                <asp:BoundField DataField="TestName" HeaderText="Test Name" ReadOnly="True" SortExpression="TestName" />
                                                                <asp:BoundField DataField="ParaName" HeaderText="Parameter Name" ReadOnly="True"
                                                                    SortExpression="ParaName" />
                                                                <asp:BoundField DataField="MinPara" HeaderText="Min" ReadOnly="True" SortExpression="MinPara" />
                                                                <asp:BoundField DataField="MaxPara" HeaderText="Max" ReadOnly="True" SortExpression="MaxPara" />
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
                        <table width="100%" style="border-color: Green; border-style: solid; border-width: 2px"
                            cellpadding="0">
                            <tr style="width: 100%">
                                <td style="width: 100%">
                                    <asp:Panel ID="Panel2" runat="server" Width="99.7%" Style="text-align: center; background-color: #E0F0E8;
                                        height: 190px;" BorderColor="Green" BorderStyle="Solid" BorderWidth="1px">
                                        <table width="100%">
                                            <tr>
                                                <td align="center" colspan="2">
                                                    <asp:Label ID="lblHeading" runat="server" Text="Test Parameters" Font-Names="Verdana"
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
                                                    <asp:Label ID="lblTestname" runat="server" Text="Test Name :" Font-Names="Verdana"
                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:DropDownList ID="ddlTestName" runat="server">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvDeptDesc" runat="server" ForeColor="Red" ControlToValidate="ddlTestName"
                                                        Font-Size="13" ValidationGroup="Save" ErrorMessage="*" InitialValue="0" Display="Dynamic"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="lblParaName" runat="server" Text="Parameter Name :" Font-Names="Verdana"
                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtParaName" runat="server" MaxLength="50" Font-Names="Verdana"
                                                        Font-Size="11px" Width="150px"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="revTxtFirstName" ValidationExpression="[a-zA-Z ]*$"
                                                        ErrorMessage="Please Enter Only Character" Font-Bold="False" Font-Size="11px"
                                                        ForeColor="Red" ControlToValidate="txtParaName" runat="server" ValidationGroup="Save"
                                                        Display="Dynamic" Font-Names="verdana" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ForeColor="Red"
                                                        ControlToValidate="txtParaName" Font-Size="13" ValidationGroup="Save" ErrorMessage="*"
                                                        Display="Dynamic"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="lblMinPara" runat="server" Text="Minimum Parameter :" Font-Names="Verdana"
                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtMinPara" runat="server" MaxLength="50" Font-Names="Verdana" Font-Size="11px"
                                                        Width="150px"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationExpression="^[0-9]{1,11}(?:\.[0-9]{1,3})?$"
                                                        ErrorMessage="Please Enter Only Number" Font-Bold="False" Font-Size="11px" ForeColor="Red"
                                                        ControlToValidate="txtMinPara" runat="server" ValidationGroup="Save" Display="Dynamic"
                                                        Font-Names="verdana" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red"
                                                        ControlToValidate="txtMinPara" Font-Size="13" ValidationGroup="Save" ErrorMessage="*"
                                                        Display="Dynamic"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="width: 45%;">
                                                    <asp:Label ID="lblMaxPara" runat="server" Text="Maximum Parameter :" Font-Names="Verdana"
                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtMaxPara" runat="server" MaxLength="100" Font-Names="Verdana"
                                                        Font-Size="11px" Width="150px" Font-Bold="False"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationExpression="^[0-9]{1,11}(?:\.[0-9]{1,3})?$"
                                                        ErrorMessage="Please Enter Only Number" Font-Bold="False" Font-Size="11px" ForeColor="Red"
                                                        ControlToValidate="txtMaxPara" runat="server" ValidationGroup="Save" Display="Dynamic"
                                                        Font-Names="verdana" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ForeColor="Red"
                                                        ControlToValidate="txtMaxPara" Font-Size="13" ValidationGroup="Save" ErrorMessage="*"
                                                        Display="Dynamic"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr style="height: 2px;">
                                                <td colspan="2">
                                                    <asp:HiddenField ID="testid" runat="server" />
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td align="center" colspan="2">
                                                    <asp:Button ID="BtnSave" runat="server" Text="Save" Font-Names="Verdana" Font-Size="12px"
                                                        ValidationGroup="Save" BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black"
                                                        OnClick="BtnSave_Click" />
                                                    <asp:Button ID="btnUpdate" runat="server" Text="Update" Font-Names="Verdana" Font-Size="12px"
                                                        BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black"
                                                        OnClick="BtnEdit_Click" />
                                                    <asp:Button ID="BtnClose" runat="server" Text="Close" Font-Names="Verdana" Font-Size="12px"
                                                        BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black"
                                                        OnClick="BtnClose_Click" />
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

