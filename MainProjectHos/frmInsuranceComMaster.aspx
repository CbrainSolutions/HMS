<%@ Page Title="" Language="C#" MasterPageFile="~/mstAdmin.Master" AutoEventWireup="true" CodeBehind="frmInsuranceComMaster.aspx.cs" Inherits="MainProjectHos.frmInsuranceComMaster" %>
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
    <asp:MultiView runat="server" ID="ViewData">
        <asp:View ID="View1" runat="server">
            <div style="width: 99%">
                <asp:Panel ID="pnlAddNew" runat="server">
                    <table width="100%">
                        <tr>
                            <td align="center">
                                <asp:Label ID="Label1" runat="server" Text="Insurance Company Master" Font-Names="Verdana"
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
                                            <asp:Button ID="BtnAddNewInsurance" runat="server" Text="Add New" Font-Names="Verdana"
                                                Font-Size="13px" BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black"
                                                OnClick="BtnAddNewInsurance_Click" />
                                        </td>
                                    </tr>
                                </table>
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
                                        <asp:Panel ID="pnl" runat="server" Width="1027px" BorderColor="Green" BorderStyle="Solid"
                                            BorderWidth="1px">
                                            <asp:Label ID="lblMessage" runat="server" Text="" Font-Names="Verdana" Font-Size="11px"
                                                ForeColor="Red"></asp:Label>
                                            <asp:GridView ID="dgvInsurance" runat="server" CellPadding="4" ForeColor="#333333"
                                                DataKeyNames="PKId" GridLines="Both" Font-Names="Verdana" Width="100%" Font-Size="Small"
                                                AllowPaging="true" PageSize="15" AutoGenerateColumns="false" OnPageIndexChanged="dgvInsurance_PageIndexChanged"
                                                OnPageIndexChanging="dgvInsurance_PageIndexChanging">
                                                <FooterStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" />
                                                <RowStyle BackColor="#E0F0E8" Font-Size="11px" Wrap="False" />
                                                <PagerStyle BackColor="#3b3535" ForeColor="White" HorizontalAlign="Left" Font-Size="11px" />
                                                <SelectedRowStyle BackColor="#006600" Font-Bold="True" ForeColor="#333333" />
                                                <HeaderStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" Font-Size="12px"
                                                    Wrap="False" />
                                                <EditRowStyle BackColor="#2461BF" />
                                                <AlternatingRowStyle BackColor="White" Wrap="False" />
                                                <Columns>
                                                    <asp:BoundField DataField="InsuranceDesc" HeaderText="Company Name" ReadOnly="True"
                                                        SortExpression="InsuranceDesc" />
                                                    <asp:BoundField DataField="Address" HeaderText="Address " ReadOnly="True" SortExpression="Address" />
                                                    <asp:BoundField DataField="Country" HeaderText="Country " ReadOnly="True" SortExpression="Country" />
                                                    <asp:BoundField DataField="State" HeaderText="State " ReadOnly="True" SortExpression="State" />
                                                    <asp:BoundField DataField="City" HeaderText="City" ReadOnly="True" SortExpression="City" />
                                                    <asp:BoundField DataField="EmailID" HeaderText="Email ID" ReadOnly="true" SortExpression="EmailID" />
                                                    <asp:BoundField DataField="ContactNo" HeaderText="Phone No" ReadOnly="true" SortExpression="PhNo" />
                                                    <asp:BoundField DataField="PostalCode" HeaderText="Postal Code" ReadOnly="true" SortExpression="PostalCode" />
                                                    <asp:BoundField DataField="FaxNumber" HeaderText="Fax Number" ReadOnly="true" SortExpression="FaxNumber" />
                                                    <asp:BoundField DataField="ContactPerson" HeaderText="Contact Person" ReadOnly="true"
                                                        SortExpression="Contact Person" />
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
            <table style="height: 100%">
                <tr>
                    <td>
                        <asp:Panel ID="pnlGrid" runat="server" Width="1045px" Style="text-align: center;
                            background-color: #E0F0E8; height: 700px">
                            <table width="100%" style="border-color: Green; border-style: solid; border-width: 2px"
                                cellpadding="0">
                                <tr>
                                    <td>
                                        <asp:Panel ID="Panel2" runat="server" Width="99.7%" Style="text-align: center; background-color: #E0F0E8;
                                            height: 100%;" BorderColor="Green" BorderStyle="Solid" BorderWidth="1px">
                                            <table width="100%">
                                                <tr>
                                                    <td align="center" colspan="2">
                                                        <asp:Label ID="lblHeading" runat="server" Text="Insurance Master" Font-Names="Verdana"
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
                                                        <asp:Label ID="lblInsuranceCode" runat="server" Text="Insurance Code :" Font-Names="Verdana"
                                                            Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtInsuranceCode" runat="server" MaxLength="10" ReadOnly="true"
                                                            Font-Names="Verdana" Font-Size="11px" Width="80px" Font-Bold="true"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <asp:Label ID="lblInsuranceName" runat="server" Text="Insurance Company Name :" Font-Names="Verdana"
                                                            Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtInsuranceDesc" runat="server" MaxLength="50" Font-Names="Verdana"
                                                            Font-Size="11px" Width="200px"></asp:TextBox><asp:RequiredFieldValidator ID="rfvInsuranceDesc"
                                                                runat="server" ForeColor="Red" ControlToValidate="txtInsuranceDesc" Font-Size="11"
                                                                ValidationGroup="Save" ErrorMessage="*" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <asp:Label ID="lblAddress" runat="server" Text="Address :" Font-Names="Verdana" Font-Size="11px"
                                                            ForeColor="#3b3535"></asp:Label>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtAddress" runat="server" MaxLength="100" Font-Names="Verdana"
                                                            TextMode="MultiLine" Font-Size="11px" Width="170px"></asp:TextBox><asp:RequiredFieldValidator
                                                                ID="rfvAddress" runat="server" ForeColor="Red" ControlToValidate="txtAddress"
                                                                Font-Size="11" ValidationGroup="Save" ErrorMessage="*" SetFocusOnError="true"
                                                                Display="Dynamic"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <asp:Label ID="lblCountry" runat="server" Text="Country Name :" Font-Names="Verdana"
                                                            Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtCountry" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <asp:Label ID="lblState" runat="server" Text="State :" Font-Names="Verdana" Font-Size="11px"
                                                            ForeColor="#3b3535"></asp:Label>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtState" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <asp:Label ID="lblCity" runat="server" Text="City :" Font-Names="Verdana" Font-Size="11px"
                                                            ForeColor="#3b3535"></asp:Label>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtCity" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <asp:Label ID="lblEmailID" runat="server" Text="Email ID:" Font-Names="Verdana" Font-Size="11px"
                                                            ForeColor="#3b3535"></asp:Label>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtEmailID" runat="server" MaxLength="50" Font-Names="Verdana" Font-Size="11px"
                                                            Width="170px"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator6"
                                                                runat="server" ForeColor="Red" ControlToValidate="txtEmailID" Font-Size="11"
                                                                ValidationGroup="Save" ErrorMessage="*" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <asp:Label ID="lblPhNo" runat="server" Text="Contact No:" Font-Names="Verdana" Font-Size="11px"
                                                            ForeColor="#3b3535"></asp:Label>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtPhNo" runat="server" MaxLength="20" Font-Names="Verdana" Font-Size="11px"
                                                            Width="140px"></asp:TextBox>
                                                       
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <asp:Label ID="lblPostalCode" runat="server" Text="Postal Code :" Font-Names="Verdana"
                                                            Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtPostalCode" runat="server" MaxLength="10" Font-Names="Verdana"
                                                            Font-Size="11px" Width="140px"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator8"
                                                                runat="server" ForeColor="Red" ControlToValidate="txtPostalCode" Font-Size="11"
                                                                ValidationGroup="Save" ErrorMessage="*" SetFocusOnError="true" Display="Dynamic">
                                                            </asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <asp:Label ID="lblFaxNo" runat="server" Text="Fax Number :" Font-Names="Verdana"
                                                            Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtFaxNo" runat="server" MaxLength="20" Font-Names="Verdana" Font-Size="11px"
                                                            Width="140px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <asp:Label ID="lblContactPerson" runat="server" Text="Contact Person :" Font-Names="Verdana"
                                                            Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtContactPerson" runat="server" MaxLength="20" Font-Names="Verdana"
                                                            Font-Size="11px" Width="140px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <asp:Label ID="lblContactPhNO" runat="server" Text="Contact Number :" Font-Names="Verdana"
                                                            Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtContactPhNo" runat="server" MaxLength="20" Font-Names="Verdana"
                                                            Font-Size="11px" Width="140px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <asp:Label ID="lblContactMobNo" runat="server" Text="Contact Mobile No :" Font-Names="Verdana"
                                                            Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtContactMobNo" runat="server" MaxLength="10" Font-Names="Verdana"
                                                            Font-Size="11px" Width="140px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <asp:Label ID="lblContactEmail" runat="server" Text="Contact Email Id :" Font-Names="Verdana"
                                                            Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtContactEmail" runat="server" MaxLength="50" Font-Names="Verdana"
                                                            Font-Size="11px" Width="170px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <asp:Label ID="lblNotes" runat="server" Text="Notes :" Font-Names="Verdana" Font-Size="11px"
                                                            ForeColor="#3b3535"></asp:Label>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtNotes" runat="server" MaxLength="100" Font-Names="Verdana" Font-Size="11px"
                                                            TextMode="MultiLine" Width="170px"></asp:TextBox>
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
                                                            OnClick="BtnSave_Click" ValidationGroup="Save" />
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
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
</asp:Content>
