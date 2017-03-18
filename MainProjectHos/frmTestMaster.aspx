<%@ Page Title="" Language="C#" MasterPageFile="~/mstAdmin.Master" AutoEventWireup="true" CodeBehind="frmTestMaster.aspx.cs" Inherits="MainProjectHos.frmTestMaster" %>
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
                                    <asp:Label ID="Label1" runat="server" Text="Test Master" Font-Names="Verdana" Font-Size="16px"
                                        Font-Bold="True" ForeColor="#3B3535"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <table border="2px" cellpadding="0" cellspacing="0" width="860px" style="height: 40px;">
                                        <tr>
                                            <td align="center" style="border-right: none; width: 575px;">
                                                <asp:TextBox ID="txtSearch" runat="server" Width="550px"></asp:TextBox>
                                                <cc:TextBoxWatermarkExtender ID="txtSearchWatermark" runat="server" TargetControlID="txtSearch"
                                                    WatermarkText="Test Name, Catagory" WatermarkCssClass="watermarked" />
                                            </td>
                                            <td align="center" style="border-left: none; border-right: none; width: 60px;">
                                                <asp:Button ID="btnSearch" runat="server" Text="Search" Font-Names="Verdana" Font-Size="13px"
                                                    BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black"
                                                    OnClick="btnSearch_Click" />
                                            </td>
                                            <td align="center" style="border-left: none; border-right: none; width: 90px;">
                                                <asp:Button ID="btnReset" runat="server" BackColor="#3b3535" Font-Names="Verdana"
                                                    Font-Size="13px" ForeColor="White" OnClick="btnReset_Click" Style="border: 1px solid black"
                                                    Text="Reset" Width="80px" />
                                            </td>
                                            <td align="center" style="border-left: none; width: 60px; padding-right: 4px;">
                                                <asp:Button ID="BtnAddNewDept" runat="server" Text="Add New" Font-Names="Verdana"
                                                    Font-Size="13px" OnClick="BtnAddNewDept_Click" BackColor="#3b3535" ForeColor="White"
                                                    Width="80px" Style="border: 1px solid black" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel ID="pnlShow" BorderColor="Maroon" BorderWidth="1px" runat="server" Style="border-color: Green;
                                        border-style: solid; border-width: 1px">
                                        <table width="100%">
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lblPageCount" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                        ForeColor="#3B3535"></asp:Label>
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
                                                    <asp:Label ID="lblMessage" runat="server" Text="" Font-Names="Verdana" Font-Size="11px"
                                                        ForeColor="Red"></asp:Label>
                                                    <asp:Panel ID="pnl" runat="server" Width="1010px" BorderColor="Green"
                                                        BorderStyle="Solid" BorderWidth="1px">
                                                        <asp:GridView ID="dgvDepartment" runat="server" CellPadding="4" ForeColor="#333333"
                                                            DataKeyNames="TestId" GridLines="Both" Font-Names="Verdana" Width="100%" Font-Size="Small"
                                                            AllowPaging="true" PageSize="20" AutoGenerateColumns="false" OnPageIndexChanged="dgvDepartment_PageIndexChanged"
                                                            OnPageIndexChanging="dgvDepartment_PageIndexChanging">
                                                            <FooterStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" />
                                                            <RowStyle BackColor="#E0F0E8" Font-Size="11px" Wrap="False" />
                                                            <PagerStyle BackColor="#3b3535" ForeColor="White" HorizontalAlign="Left" Font-Size="11px" />
                                                            <SelectedRowStyle BackColor="#006600" Font-Bold="True" ForeColor="#333333" />
                                                            <HeaderStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" Font-Size="12px"
                                                                Wrap="False" />
                                                            <EditRowStyle BackColor="#2461BF" />
                                                            <AlternatingRowStyle BackColor="White" Wrap="False" />
                                                            <Columns>
                                                                <asp:BoundField DataField="TestId" HeaderText="Test Id" ReadOnly="True" SortExpression="TestId" />
                                                                <asp:BoundField DataField="TestName" HeaderText="Test Name" ReadOnly="True" SortExpression="TestName" />
                                                                <asp:BoundField DataField="TestCharge" HeaderText="Test Charge" ReadOnly="True" SortExpression="TestCharge" />
                                                                <asp:BoundField DataField="Precautions" HeaderText="Precautions" ReadOnly="True"
                                                                    SortExpression="Precautions" />
                                                                <asp:BoundField DataField="TestCatagoryName" HeaderText="Test Catagory" ReadOnly="True"
                                                                    SortExpression="TestCatagoryName" />
                                                                <asp:BoundField DataField="IsRadiology" HeaderText="Test Type" ReadOnly="True" SortExpression="IsRadiology" />
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
                                        height: 270px;" BorderColor="Green" BorderStyle="Solid" BorderWidth="1px">
                                        <table width="100%">
                                            <tr>
                                                <td align="center" colspan="2">
                                                    <asp:Label ID="lblHeading" runat="server" Text="Test Master" Font-Names="Verdana"
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
                                                    <asp:Label ID="lblMsg" runat="server" Text="" Font-Names="Verdana" Font-Size="11px"
                                                        ForeColor="Red"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="lblDeptName" runat="server" Text="Test Name :" Font-Names="Verdana"
                                                        Font-Size="11px" ForeColor="#3B3535"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtDeptDesc" runat="server" MaxLength="50" Font-Names="Verdana"
                                                        Font-Size="11px" Width="170px"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvDeptDesc" runat="server" ForeColor="Red" ControlToValidate="txtDeptDesc"
                                                        Font-Size="13" ValidationGroup="Save" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label3" runat="server" Text="Test Catagory :" Font-Names="Verdana"
                                                        Font-Size="11px" ForeColor="#3B3535"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:DropDownList ID="ddlTestCatagory" runat="server" Height="23px" Width="172px">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ForeColor="Red"
                                                        ControlToValidate="ddlTestCatagory" Font-Size="13" ValidationGroup="Save" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label4" runat="server" Text="Test Type :" Font-Names="Verdana" Font-Size="11px"
                                                        ForeColor="#3B3535"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:RadioButton ID="rdoPathology" Text="Pathology" GroupName="tType" Checked="true"
                                                        runat="server" Font-Names="Verdana" Font-Size="11px" ForeColor="#3B3535" />
                                                    <asp:RadioButton ID="rdoRadiology" Text="Radiology" GroupName="tType" runat="server"
                                                        Checked="false" Font-Names="Verdana" Font-Size="11px" ForeColor="#3B3535" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label2" runat="server" Text="Charge :" Font-Names="Verdana" Font-Size="11px"
                                                        ForeColor="#3B3535"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtCharge" runat="server" MaxLength="50" Font-Names="Verdana" Font-Size="11px"
                                                        Width="170px"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationExpression="^[0-9]{1,11}(?:\.[0-9]{1,3})?$"
                                                        ErrorMessage="Please Enter Only Number" Font-Size="11px" ForeColor="Red" Font-Names="Verdana"
                                                        ControlToValidate="txtCharge" runat="server" ValidationGroup="Save" Display="Dynamic" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red"
                                                        ControlToValidate="txtCharge" Font-Size="13" ValidationGroup="Save" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="width: 45%;">
                                                    <asp:Label ID="lblDeptCode" runat="server" Text="Precautions :" Font-Names="Verdana"
                                                        Font-Size="11px" ForeColor="#3B3535"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtDeptCode" runat="server" MaxLength="100" TextMode="MultiLine"
                                                        Font-Names="Verdana" Font-Size="11px" Width="275px" Font-Bold="False"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
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
