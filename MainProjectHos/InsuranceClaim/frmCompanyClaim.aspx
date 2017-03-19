<%@ Page Title="" Language="C#" MasterPageFile="~/mstAdmin.Master" AutoEventWireup="true" CodeBehind="frmCompanyClaim.aspx.cs" Inherits="MainProjectHos.InsuranceClaim.frmCompanyClaim" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" language="javascript">
 
    </script>
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
                                    <asp:Label ID="Label1" runat="server" Text="Company Claim" Font-Names="Verdana" Font-Size="16px"
                                        Font-Bold="True" ForeColor="#3B3535"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="width: 15%;">
                                    <table border="1px" cellpadding="0" cellspacing="0" style="height: 40px;" width="900px">
                                        <tr>
                                            <td align="center" style="border-right: none; width: 575px;">
                                                <asp:TextBox ID="txtSearch" runat="server" Font-Bold="true" Font-Names="Verdana"
                                                    Font-Size="13px" Width="580px" />
                                            </td>
                                            <td align="center" style="border-left: none; border-right: none; width: 60px;">
                                                <asp:Button ID="btnSearch" runat="server" BackColor="#3b3535" Font-Names="Verdana"
                                                    Font-Size="13px" ForeColor="White" OnClick="btnSearch_Click" Style="border: 1px solid black"
                                                    Text="Search" Width="70px" />
                                            </td>
                                            <td align="center" style="border-left: none; border-right: none; width: 60px;">
                                                <asp:Button ID="btnReset" runat="server" BackColor="#3b3535" Font-Names="Verdana"
                                                    Font-Size="13px" ForeColor="White" OnClick="btnReset_Click" Style="border: 1px solid black"
                                                    Text="Reset" Width="70px" />
                                            </td>
                                            <td align="center" style="border-left: none; border-right: none; width: 60px;">
                                                <asp:Button ID="BtnAddNewclaim" runat="server" Text="Add New" Font-Names="Verdana"
                                                    Font-Size="13px" BackColor="#3b3535" ForeColor="White" Width="70px" Style="border: 1px solid black"
                                                    onmouseover="SetBtnMouseOver(this)" onmouseout="SetBtnMouseOut(this)" OnClick="BtnAddNewclaim_Click" />
                                            </td>
                                            <td align="center" style="border-left: none; width: 60px;">
                                                <asp:Button ID="btnExcel" runat="server" Text="Excel" Font-Names="Verdana" Font-Size="13px"
                                                    BackColor="#3b3535" ForeColor="White" Width="70px" Style="border: 1px solid black;"
                                                    onmouseover="SetBtnMouseOver(this)" onmouseout="SetBtnMouseOut(this)" OnClick="btnExcel_Click" />
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
                                                <td align="left" class="style1">
                                                    <asp:Label ID="lblPageCount" runat="server" Text="" Font-Names="Verdana" Font-Size="11px"
                                                        ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="right" class="style1">
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
                                                    <asp:Panel ID="pnl" runat="server" Width="1020px" Style="border-color: Green;
                                                        border-style: solid; border-width: 1px">
                                                        <asp:Label ID="lblMessage" runat="server" Text="" Font-Names="Verdana" Font-Size="11px"
                                                            ForeColor="Red"></asp:Label>
                                                        <asp:GridView ID="dgvClaim" runat="server" CellPadding="4" ForeColor="#333333" GridLines="Both"
                                                            Font-Names="Verdana" Width="100%" Font-Size="Small" AllowPaging="true" PageSize="15"
                                                            AutoGenerateColumns="false" DataKeyNames="ComClaimId" 
                                                            OnDataBound="dgvClaim_DataBound" onpageindexchanged="dgvClaim_PageIndexChanged" 
                                                            onpageindexchanging="dgvClaim_PageIndexChanging">
                                                            <FooterStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" />
                                                            <RowStyle BackColor="#E0F0E8" Font-Size="11px" Wrap="False" />
                                                            <PagerStyle BackColor="#3b3535" ForeColor="White" HorizontalAlign="Left" Font-Size="11px" />
                                                            <SelectedRowStyle BackColor="#006600" Font-Bold="True" ForeColor="#333333" />
                                                            <HeaderStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" Font-Size="12px"
                                                                Wrap="False" />
                                                            <EditRowStyle BackColor="#2461BF" />
                                                            <AlternatingRowStyle BackColor="White" Wrap="False" />
                                                            <Columns>
                                                                <asp:BoundField DataField="ComClaimId" HeaderText="Claim No." ReadOnly="True" SortExpression="ComClaimId" />
                                                                <asp:BoundField DataField="PatientName" HeaderText="Patient Name" ReadOnly="True"
                                                                    SortExpression="PatientName" />
                                                                <asp:BoundField DataField="CompanyName" HeaderText="Insurance Company Name" ReadOnly="True"
                                                                    SortExpression="CompanyName" />
                                                                <asp:BoundField DataField="ClaimDate" HeaderText="Claim Date" ReadOnly="True" SortExpression="ClaimDate" DataFormatString="{0:MM/dd/yyyy}"/>
                                                                <asp:BoundField DataField="Total" HeaderText="Claim Amount" ReadOnly="True" SortExpression="Total" DataFormatString="{0:0.00}"/>
                                                                <asp:BoundField DataField="Category" HeaderText="Approved/Pending" ReadOnly="True"
                                                                    SortExpression="Category" />
                                                                <asp:BoundField DataField="ApprovedAmount" HeaderText=" ApprovedAmount" ReadOnly="True"
                                                                    SortExpression="ApprovedAmount" DataFormatString="{0:0.00}"/>
                                                                <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="ImageEdit" runat="server" ImageUrl="~/images/edit.jpg" Height="24px"
                                                                            Width="24px" OnClick="BtnEdit_Click" />
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
                            <tr width="100%">
                                <td width="100%">
                                    <asp:Panel ID="pnlGrid" runat="server" Width="1040px" Style="text-align: center;
                                        background-color: #E0F0E8; height: auto;">
                                        <table width="100%" style="border-color: Green; border-style: solid; border-width: 2px"
                                            cellpadding="0">
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="Panel2" runat="server" Width="99.7%" Style="text-align: center; background-color: #E0F0E8;
                                                        height: auto;" BorderColor="Green" BorderStyle="Solid" BorderWidth="1px">
                                                        <table width="100%">
                                                            <tr>
                                                                <td align="center" colspan="2">
                                                                    <asp:Label ID="lblHeading" runat="server" Text="Company Claim" Font-Names="Verdana"
                                                                        Font-Size="15px" Font-Bold="true" ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" align="center">
                                                                    <asp:Label ID="lblMsg" Text="" runat="server" ForeColor="Red" Font-Names="Verdana"
                                                                        Font-Size="11px"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 50%;" align="right">
                                                                    <asp:Label ID="Label2" Text="Patient Name:" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                        ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td style="width: 50%;" align="left">
                                                                    <asp:DropDownList ID="ddlPatient" runat="server" Width="150px" AutoPostBack="True"
                                                                        OnSelectedIndexChanged="ddlPatient_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red"
                                                                        ControlToValidate="ddlPatient" Font-Size="13" ValidationGroup="Save" ErrorMessage="*"
                                                                        Display="Dynamic" InitialValue="0"></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 50%;" align="right">
                                                                    <asp:Label ID="Label3" Text="Company Name:" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                        ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td style="width: 50%;" align="left">
                                                                    <asp:DropDownList ID="ddlInsuranceComName" runat="server" Width="150px" AutoPostBack="True">
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ForeColor="Red"
                                                                        ControlToValidate="ddlInsuranceComName" Font-Size="13" ValidationGroup="Save"
                                                                        ErrorMessage="*" Display="Dynamic" InitialValue="0"></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 50%;" align="right">
                                                                    <asp:Label ID="Label4" Text="Claim Date:" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                        ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td style="width: 50%;" align="left">
                                                                    <asp:TextBox ID="txtClaimDate" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                        Width="150px" MaxLength="10"></asp:TextBox>
                                                                    <asp:Calendar ID="CalDate" runat="server" TargetControlID="txtClaimDate" Format="MM/dd/yyyy">
                                                                    </asp:Calendar>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtClaimDate"
                                                                        Display="Dynamic" ErrorMessage="*" Font-Size="13" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:RegularExpressionValidator ID="revTxtFirstName" ValidationExpression="^([0]\d|[1][0-2])\/([0-2]\d|[3][0-1])\/([2][01]|[1][6-9])\d{2}(\s([0-1]\d|[2][0-3])(\:[0-5]\d){1,2})?$"
                                                                        ErrorMessage="Please Enter Only Date With MM/DD/YYYY Format" Font-Bold="False"
                                                                        Font-Size="11px" ForeColor="Red" ControlToValidate="txtClaimDate" runat="server"
                                                                        ValidationGroup="Save" Display="Dynamic" Font-Names="verdana" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="Panel3" runat="server" Width="1020px" Style="border-color: Green;
                                                        border-style: solid; border-width: 1px">
                                                        <table width="100%">
                                                            <tr>
                                                                <td colspan="4" align="center">
                                                                    <asp:CheckBox ID="chkOtherBills" runat="server" OnCheckedChanged="chkOtherBills_CheckedChanged"
                                                                        AutoPostBack="True" />
                                                                    <asp:Label ID="Label5" Text="External Bills" runat="server" Font-Names="Verdana"
                                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr style="width: 100%">
                                                                <td style="width: 25%" align="right">
                                                                    <asp:Label ID="Label6" Text="Bill No.:" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                        ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td style="width: 25%" align="left">
                                                                    <asp:TextBox ID="txtBillNo" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                        Width="150px" MaxLength="10" ValidationGroup="AddOther"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ForeColor="Red"
                                                                        ControlToValidate="txtBillNo" Font-Size="13" ValidationGroup="AddOther" ErrorMessage="*"
                                                                        Display="Dynamic"></asp:RequiredFieldValidator>
                                                                </td>
                                                                <td style="width: 25%" align="right">
                                                                    <asp:Label ID="Label7" Text="Bill Date:" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                        ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td style="width: 25%" align="left">
                                                                    <asp:TextBox ID="txtBilldate" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                        Width="150px"></asp:TextBox>
                                                                    <asp:Calendar ID="calBillDate" runat="server" TargetControlID="txtBilldate"
                                                                        Format="MM/dd/yyyy">
                                                                    </asp:Calendar>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtBilldate"
                                                                        Display="Dynamic" ErrorMessage="*" Font-Size="13" ForeColor="Red" ></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="3">
                                                                </td>
                                                                <td align="left">
                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationExpression="^([0]\d|[1][0-2])\/([0-2]\d|[3][0-1])\/([2][01]|[1][6-9])\d{2}(\s([0-1]\d|[2][0-3])(\:[0-5]\d){1,2})?$"
                                                                        ErrorMessage="Please Enter Only Date With MM/DD/YYYY Format" Font-Bold="False"
                                                                        Font-Size="11px" ForeColor="Red" ControlToValidate="txtBilldate" runat="server"
                                                                        ValidationGroup="Save" Display="Dynamic" Font-Names="verdana" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 25%" align="right">
                                                                    <asp:Label ID="Label8" Text="Bill Type:" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                        ForeColor="#3b3535" Display="Dynamic"></asp:Label>
                                                                </td>
                                                                <td style="width: 25%" align="left">
                                                                    <asp:TextBox ID="txtBilltype" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                        Width="150px"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtBilltype"
                                                                        Display="Dynamic" ErrorMessage="*" Font-Size="13" ForeColor="Red" ValidationGroup="AddOther"></asp:RequiredFieldValidator>
                                                                </td>
                                                                <td style="width: 25%" align="right">
                                                                    <asp:Label ID="Label9" Text="Bill Amount:" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                        ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td style="width: 25%" align="left">
                                                                    <asp:TextBox ID="txtBillAmount" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                        Width="150px"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtBillAmount"
                                                                        Display="Dynamic" ErrorMessage="*" Font-Size="13" ForeColor="Red" ValidationGroup="AddOther"></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4" align="center" style="border-top: none; border-bottom: none;">
                                                                    <asp:Button ID="btnAdd" runat="server" Text="Add" Font-Names="Verdana" Font-Size="12px"
                                                                        ValidationGroup="AddOther" BackColor="#3b3535" ForeColor="White" Width="80px"
                                                                        Style="border: 1px solid black" OnClick="btnAddCharge_Click" />
                                                                    <asp:Button ID="btnUpdatecharge" runat="server" Text="Update" Font-Names="Verdana"
                                                                        Font-Size="12px" ValidationGroup="AddOther" BackColor="#3b3535" ForeColor="White"
                                                                        Width="80px" Style="border: 1px solid black" OnClick="btnUpdatecharge_Click" />
                                                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" Font-Names="Verdana" Font-Size="12px"
                                                                        BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black"
                                                                        OnClick="btnCancel_Click" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4">
                                                                </td>
                                                            </tr>
                                                        </table>
                                                          </asp:Panel>
                                                </td>
                                            </tr>
                                                        <tr>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Panel ID="Panel1" ScrollBars="Both" runat="server" Width="1020px" Style="border-color: Green;
                                                        border-style: solid; border-width: 1px">
                                                                <asp:GridView ID="dgvChargeDetails" runat="server" CellPadding="4" ForeColor="#333333"
                                                                    GridLines="Both" Font-Names="Verdana" Width="100%" Font-Size="Small" AllowPaging="false"
                                                                    AutoGenerateColumns="false" DataKeyNames="TempId">
                                                                    <FooterStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" />
                                                                    <RowStyle BackColor="#E0F0E8" Font-Size="11px" Wrap="False" />
                                                                    <PagerStyle BackColor="#3b3535" ForeColor="White" HorizontalAlign="Left" Font-Size="11px" />
                                                                    <SelectedRowStyle BackColor="#006600" Font-Bold="True" ForeColor="#333333" />
                                                                    <HeaderStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" Font-Size="12px"
                                                                        Wrap="False" />
                                                                    <EditRowStyle BackColor="#2461BF" />
                                                                    <AlternatingRowStyle BackColor="White" Wrap="False" />
                                                                    <Columns>
                                                                        <asp:BoundField DataField="BillNo" HeaderText="Bill No." ReadOnly="True" SortExpression="BillNo" />
                                                                        <asp:BoundField DataField="BillDate" HeaderText="Bill Date" ReadOnly="True" SortExpression="BillDate"
                                                                            DataFormatString="{0:MM/dd/yyyy}" />
                                                                        <asp:BoundField DataField="BillType" HeaderText="Bill Type" ReadOnly="True" SortExpression="BillType" />
                                                                        <asp:BoundField DataField="BillAmount" HeaderText="BillAmount" ReadOnly="True" SortExpression="BillAmount" DataFormatString="{0:0.00}"/>
                                                                        <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ID="ImageEdit" runat="server" ImageUrl="~/images/edit.jpg" Height="24px"
                                                                                    Width="24px" OnClick="BtnEditCharge_Click" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ID="ImgDelete" runat="server" ImageUrl="~/images/Erase.png" Height="24px"
                                                                                    Width="24px" OnClick="btnDelete_Click" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                                </asp:Panel>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                    
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label10" Text="Claim Amount:" runat="server" Font-Names="Verdana"
                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                    <asp:TextBox ID="txtClaimAmount" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                        Width="150px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="BtnSave" runat="server" Text="Save" Font-Names="Verdana" Font-Size="12px"
                                                        ValidationGroup="Save" BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black"
                                                        OnClick="BtnSave_Click" />
                                                    <asp:Button ID="btnUpdate" runat="server" Text="Update" Font-Names="Verdana" Font-Size="12px"
                                                        BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black"
                                                        OnClick="BtnUpdate_Click" />
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
</asp:Content>
