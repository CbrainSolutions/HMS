<%@ Page Title="" Language="C#" MasterPageFile="~/mstAdmin.Master" AutoEventWireup="true" CodeBehind="frmAllowanceDeduction.aspx.cs" Inherits="MainProjectHos.Payroll.frmAllowanceDeduction" %>
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
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="width: 99%;">
                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="View1" runat="server">
                        <table width="100%">
                            <tr>
                                <td align="center">
                                    <asp:Label ID="Label1" runat="server" Text="Allowances And Deduction" Font-Names="Verdana"
                                        Font-Size="16px" Font-Bold="true" ForeColor="#3b3535"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 15%;">
                                    <asp:Button ID="btnAddNew" runat="server" Text="Add New" ForeColor="White" BackColor="#3b3535"
                                        Font-Names="Verdana" Font-Size="13px" Width="80px" Style="border: 1px solid black;"
                                        OnClick="btnAddNew_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel ID="pnlShow" BorderWidth="1px" runat="server" Style="border-color: Green;
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
                                                    <asp:Label Text="" ID="lblMessage" ForeColor="Red" Font-Names="Verdana" Font-Size="11px"
                                                        runat="server" />
                                                    <asp:Panel ID="pnl" ScrollBars="Horizontal" runat="server" Width="1010px">
                                                        <asp:GridView ID="dgvAllowanceDeduction" runat="server" CellPadding="4" ForeColor="#333333"
                                                            BorderColor="Green" BorderStyle="Solid" BorderWidth="1px" GridLines="Both" Font-Names="Verdana"
                                                            Width="100%" Font-Size="Small" AllowPaging="true" PageSize="20" AutoGenerateColumns="false">
                                                            <FooterStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" />
                                                            <RowStyle BackColor="#E0F0E8" Font-Size="11px" Wrap="False" />
                                                            <PagerStyle BackColor="#3b3535" ForeColor="White" HorizontalAlign="Left" Font-Size="11px" />
                                                            <SelectedRowStyle BackColor="#006600" Font-Bold="True" ForeColor="#333333" />
                                                            <HeaderStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" Font-Size="12px"
                                                                Wrap="False" />
                                                            <EditRowStyle BackColor="#2461BF" />
                                                            <AlternatingRowStyle BackColor="White" Wrap="False" />
                                                            <Columns>
                                                                <asp:BoundField DataField="AllowDedId" HeaderText="Allow/DedID" ReadOnly="True" SortExpression="AllowDedId" />
                                                                <asp:BoundField DataField="Description" HeaderText="Description " ReadOnly="True"
                                                                    SortExpression="Description" />
                                                                <asp:BoundField DataField="Type" HeaderText="Type" ReadOnly="True" SortExpression="Type" />
                                                                <asp:BoundField DataField="Amount" HeaderText="Amount" ReadOnly="True" SortExpression="Amount" DataFormatString="{0:0.00}" />
                                                                <asp:BoundField DataField="CategoryDesc" HeaderText="Allowance/Deduction" ReadOnly="True"
                                                                    SortExpression="CategoryDesc" />
                                                                <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="ImageEdit" runat="server" ImageUrl="~/images/edit.jpg" Height="24px"
                                                                            Width="24px" OnClick="BtnUpdate_Click" /></ItemTemplate>
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
                                    <asp:Panel ID="pnlGrid" runat="server" Width="100%" Style="text-align: center; background-color: #E0F0E8;
                                        height: auto;">
                                        <table width="100%" style="border-color: Green; border-style: solid; border-width: 2px"
                                            cellpadding="0">
                                            <tr>
                                                <td style="width: 100%;">
                                                    <asp:Panel ID="Panel2" runat="server" Width="99.7%" Style="text-align: center; background-color: #E0F0E8;
                                                        height: auto;" BorderColor="Green" BorderStyle="Solid" BorderWidth="1px">
                                                        <table width="100%">
                                                            <tr>
                                                                <td align="center" colspan="4">
                                                                    <asp:Label ID="lblHeading" runat="server" Text="Allowances And Deductions" Font-Names="Verdana"
                                                                        Font-Size="15px" Font-Bold="true" ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr style="height: 5px;">
                                                                <td colspan="4">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" align="center">
                                                                    <asp:Label ID="Label2" runat="server" Text="Note :-" Font-Names="Verdana" Font-Size="13px"
                                                                        ForeColor="#3b3535"></asp:Label>
                                                                    <asp:Label ID="Label3" runat="server" Text="Percentage Will Be Applied On Basic Salary."
                                                                        Font-Names="Verdana" Font-Size="13px" ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" align="center">
                                                                    <asp:Label ID="lblMsg" Text="" runat="server" ForeColor="Red" Font-Names="Verdana"
                                                                        Font-Size="11px"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center" colspan="2">
                                                                    <asp:RadioButton ID="RdAllowance" runat="server" Text="Allowance" GroupName="AllowDed"
                                                                        OnCheckedChanged="RdAllowance_CheckedChanged" Font-Size="11px" ForeColor="#3b3535"
                                                                        Font-Names="Verdana" AutoPostBack="True" />
                                                                    <asp:RadioButton ID="RdDeduction" runat="server" Text="Deduction" GroupName="AllowDed"
                                                                        Font-Size="11px" ForeColor="#3b3535" Font-Names="Verdana" AutoPostBack="True"
                                                                        OnCheckedChanged="RdDeduction_CheckedChanged" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" style="width: 50%;">
                                                                    <asp:Label ID="lblDescription" runat="server" Font-Size="11px" ForeColor="#3b3535"
                                                                        Font-Names="Verdana"></asp:Label>
                                                                </td>
                                                                <td align="left" style="width: 50%;">
                                                                    <asp:TextBox ID="txtDescription" runat="server" Font-Size="11px" ForeColor="#3b3535"
                                                                        Font-Names="Verdana"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvDescription" ErrorMessage="*" Display="Dynamic"
                                                                        ControlToValidate="txtDescription" Font-Size="13px" SetFocusOnError="true" ValidationGroup="Save"
                                                                        ForeColor="Red" runat="server"></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr style="height: 5px;">
                                                                <td colspan="2">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center" colspan="3" style="height: 50px;">
                                                                    <table style="border: 1px solid black;">
                                                                        <tr>
                                                                            <td style="height: 50px;">
                                                                                <asp:RadioButton ID="RdFlexible" runat="server" Text="Flexible" GroupName="grpType"
                                                                                    Font-Size="11px" ForeColor="#3b3535" Font-Names="verdana" AutoPostBack="True"
                                                                                    OnCheckedChanged="RdFlexible_CheckedChanged" />
                                                                                <asp:RadioButton ID="RdFixed" runat="server" Text="Fixed" GroupName="grpType" Font-Size="11px"
                                                                                    ForeColor="#3b3535" Font-Names="Verdana" AutoPostBack="True" OnCheckedChanged="RdFlexible_CheckedChanged" />
                                                                                <asp:RadioButton ID="RdPercentage" runat="server" Text="Percentage" GroupName="grpType"
                                                                                    Font-Size="11px" ForeColor="#3b3535" Font-Names="Verdana" AutoPostBack="True"
                                                                                    OnCheckedChanged="RdFlexible_CheckedChanged" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr style="height: 5px;">
                                                                <td colspan="2" style="height: 5px;">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" style="width: 50%;">
                                                                    <asp:Label ID="lblisbasic" runat="server" Text="IsBasic" Font-Size="11px" ForeColor="#3b3535"
                                                                        Font-Names="Verdana"></asp:Label>
                                                                </td>
                                                                <td align="left" style="width: 50%;">
                                                                    <asp:CheckBox ID="ChkIsbasic" runat="server" Font-Size="11px" 
                                                                        ForeColor="#3b3535" oncheckedchanged="ChkIsbasic_CheckedChanged" autoPostBack="true"/>
                                                                </td>
                                                            </tr>
                                                             <tr style="height: 5px;">
                                                                <td colspan="2" style="height: 5px;">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" style="width: 50%;">
                                                                    <asp:Label ID="lblAmount" runat="server" Font-Size="11px" ForeColor="#3b3535" Font-Names="Verdana"></asp:Label>
                                                                </td>
                                                                <td align="left" style="width: 50%;">
                                                                    <asp:TextBox ID="txtAmount" runat="server" Font-Size="11px" ForeColor="#3b3535" 
                                                                        Font-Names="Verdana" Height="21px"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvAmount" runat="server" ForeColor="Red" ControlToValidate="txtAmount"
                                                                        Font-Size="13" ValidationGroup="Save" Display="Dynamic" ErrorMessage="*" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" align="center">
                                                                </td>
                                                            </tr>
                                                            <tr align="center">
                                                                <td align="center" colspan="4">
                                                                    <asp:Button ID="BtnSave" runat="server" Text="Save" Font-Names="Verdana" Font-Size="12px"
                                                                        BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black"
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
