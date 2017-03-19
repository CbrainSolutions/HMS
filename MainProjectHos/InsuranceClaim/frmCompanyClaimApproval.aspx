<%@ Page Title="" Language="C#" MasterPageFile="~/mstAdmin.Master" AutoEventWireup="true" CodeBehind="frmCompanyClaimApproval.aspx.cs" Inherits="MainProjectHos.InsuranceClaim.frmCompanyClaimApproval" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" language="javascript">
        function CheckValue() {
            var claim = document.getElementById("<%=txtClaimamount.ClientID %>").value;
            var pay = document.getElementById("<%=txtApprovedamt.ClientID %>").value;
            if (pay > claim) {
                alert("Approved Amount Shoud Be Less Than Claim Amount....");
                document.getElementById("<%=txtApprovedamt.ClientID %>").value = null;
                document.getElementById("<%=txtApprovedamt.ClientID %>").focus();
                return false;
            }
        }

        function CheckList() {
            var tds = document.getElementById("<%=txtTds.ClientID %>").value;
            var approv = document.getElementById("<%=txtApprovedamt.ClientID %>").value;

            if ((approv == null || approv == "") && (tds == null || tds == "")) {
                document.getElementById("<%=txtRecAmount.ClientID %>").value = null;
            }
            else if (approv != null && (tds == null || tds == "")) {
                document.getElementById("<%=txtRecAmount.ClientID %>").value = approv;
            }
            else if (approv != null && tds != null) {
                var TdsAmt = approv * tds / 100;
                document.getElementById("<%=txtRecAmount.ClientID %>").value = approv - TdsAmt;
            }
        }
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
                                    <asp:Label ID="Label1" runat="server" Text="Claim Approval" Font-Names="Verdana"
                                        Font-Size="16px" Font-Bold="True" ForeColor="#3B3535"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="width: 15%;">
                                    <table border="1px" cellpadding="0" cellspacing="0" style="height: 40px;" width="860px">
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
                                                    <asp:Panel ID="pnl"  runat="server" Width="1020px" Style="border-color: Green;
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
                                                                <asp:BoundField DataField="AdmitId" HeaderText="Admit Id" ReadOnly="True" SortExpression="AdmitId" />
                                                                <asp:BoundField DataField="PatientName" HeaderText="Patient Name" ReadOnly="True"
                                                                    SortExpression="PatientName" />
                                                                <asp:BoundField DataField="CompanyName" HeaderText="Insurance Company Name" ReadOnly="True"
                                                                    SortExpression="CompanyName" />
                                                                <asp:BoundField DataField="ClaimDate" HeaderText="Claim Date" ReadOnly="True" SortExpression="ClaimDate"
                                                                    DataFormatString="{0:MM/dd/yyyy}" />
                                                                <asp:BoundField DataField="Total" HeaderText="Claim Amount" ReadOnly="True" SortExpression="Total" />
                                                                <asp:BoundField DataField="Category" HeaderText="Approved/Pending" ReadOnly="True"
                                                                    SortExpression="Category" />
                                                                <asp:BoundField DataField="ApprovedAmount" HeaderText=" ApprovedAmount" ReadOnly="True"
                                                                    SortExpression="ApprovedAmount" DataFormatString="{0:0.00}"/>
                                                                <asp:TemplateField HeaderText="Approve Claim" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="ImageApprove" runat="server" ImageUrl="~/images/Knob/Knob Valid Green.png"
                                                                            Height="24px" Width="24px" OnClick="BtnApprove_Click" />
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
                                                                    <asp:Label ID="lblHeading" runat="server" Text="Claim Approval" Font-Names="Verdana"
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
                                                                <td colspan="2">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 50%;" align="right">
                                                                    <asp:Label ID="Label2" Text="Patient Name:" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                        ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td style="width: 50%;" align="left">
                                                                    <asp:TextBox ID="txtPatient" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                        Width="150px" MaxLength="10"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 50%;" align="right">
                                                                    <asp:Label ID="Label3" Text="Claim Amount:" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                        ForeColor="#3B3535"></asp:Label>
                                                                </td>
                                                                <td style="width: 50%;" align="left">
                                                                    <asp:TextBox ID="txtClaimamount" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                        Width="150px" MaxLength="10"></asp:TextBox>
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
                                                                </td>
                                                            </tr>
                                                            
                                                            <tr>
                                                                <td style="width: 50%;" align="right">
                                                                    <asp:Label ID="Label5" Text="Approved Amount:" runat="server" Font-Names="Verdana"
                                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td style="width: 50%;" align="left">
                                                                    <asp:TextBox ID="txtApprovedamt" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                        Width="150px" MaxLength="10" onblur="javascript:return CheckValue()"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtApprovedamt"
                                                                        Display="Dynamic" ErrorMessage="*" Font-Size="13" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 50%;" align="right">
                                                                    <asp:Label ID="Label6" Text="Approved Date:" runat="server" Font-Names="Verdana"
                                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td style="width: 50%;" align="left">
                                                                    <asp:TextBox ID="txtApprovedate" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                        Width="150px" MaxLength="10"></asp:TextBox>
                                                                    <asp:Calendar ID="CalDate" runat="server" TargetControlID="txtApprovedate"
                                                                        Format="MM/dd/yyyy">
                                                                    </asp:Calendar>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtApprovedate"
                                                                        Display="Dynamic" ErrorMessage="*" Font-Size="13" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 50%;">
                                                                </td>
                                                                <td style="width: 50%;" align="left">
                                                                    <asp:RegularExpressionValidator ID="revTxtFirstName" ValidationExpression="^([0]\d|[1][0-2])\/([0-2]\d|[3][0-1])\/([2][01]|[1][6-9])\d{2}(\s([0-1]\d|[2][0-3])(\:[0-5]\d){1,2})?$"
                                                                        ErrorMessage="Please Enter Only Date With MM/DD/YYYY Format" Font-Bold="False"
                                                                        Font-Size="11px" ForeColor="Red" ControlToValidate="txtApprovedate" runat="server"
                                                                        ValidationGroup="Save" Display="Dynamic" Font-Names="verdana" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 50%;" align="right">
                                                                    <asp:Label ID="Label11" Text="Co.Payment:" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                        ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td style="width: 50%;" align="left">
                                                                    <asp:TextBox ID="txtCoPayment" runat="server" Font-Names="Verdana" Font-Size="11px" 
                                                                        Width="150px" MaxLength="10" ></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 50%;" align="right">
                                                                    <asp:Label ID="Label12" Text="Bad Debts:" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                        ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td style="width: 50%;" align="left">
                                                                    <asp:TextBox ID="txtBadDebts" runat="server" Font-Names="Verdana" Font-Size="11px" 
                                                                        Width="150px" MaxLength="10"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 50%;" align="right">
                                                                    <asp:Label ID="Label8" Text="TDS:" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                        ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td style="width: 50%;" align="left">
                                                                    <asp:TextBox ID="txtTds" runat="server" Font-Names="Verdana" Font-Size="11px" Width="150px"
                                                                        MaxLength="10" onblur="javascript:return CheckList();"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtTds"
                                                                        Display="Dynamic" ErrorMessage="*" Font-Size="13" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 50%;" align="right">
                                                                    <asp:Label ID="Label10" Text="Received Amount:" runat="server" Font-Names="Verdana"
                                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td style="width: 50%;" align="left">
                                                                    <asp:TextBox ID="txtRecAmount" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                        Width="150px" MaxLength="10"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtRecAmount"
                                                                        Display="Dynamic" ErrorMessage="*" Font-Size="13" ForeColor="Red" ValidationGroup="Save">
                                                                    </asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 50%;" align="right">
                                                                    <asp:Label ID="Label7" Text="Transaction Type:" runat="server" Font-Names="Verdana"
                                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td style="width: 50%;" align="left">
                                                                    <asp:RadioButton ID="IsCash" runat="server" Text="IsCash" GroupName="AmountType"
                                                                        AutoPostBack="True" Font-Names="verdana" Font-Size="11px" OnCheckedChanged="IsCash_CheckedChanged">
                                                                    </asp:RadioButton>
                                                                    <asp:RadioButton ID="IsNeft" runat="server" Text="IsNeft" GroupName="AmountType"
                                                                        AutoPostBack="True" Font-Names="verdana" Font-Size="11px" OnCheckedChanged="IsCash_CheckedChanged">
                                                                    </asp:RadioButton>
                                                                    <asp:RadioButton ID="IsCheque" runat="server" Text="IsCheque" GroupName="AmountType"
                                                                        AutoPostBack="True" Font-Names="verdana" Font-Size="11px" OnCheckedChanged="IsCash_CheckedChanged">
                                                                    </asp:RadioButton>
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
                                                    <asp:Panel ID="Pnlclaim" runat="server" Width="1020px" Style="border-color: Green;
                                                        border-style: solid; border-width: 1px">
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="width: 50%;" align="right">
                                                                    <asp:Label ID="lblbankRefNo" Text="Bank Ref.No:" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                        ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td style="width: 50%;" align="left">
                                                                    <asp:TextBox ID="txtBankRefNo" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                        Width="200px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 50%;" align="right">
                                                                    <asp:Label ID="lblbankname" Text="Bank Name:" runat="server" Font-Names="Verdana"
                                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td style="width: 50%;" align="left">
                                                                    <asp:TextBox ID="txtBankName" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                        Width="200px" MaxLength="50"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtBankName"
                                                                        Display="Dynamic" ErrorMessage="*" Font-Size="13" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 50%;" align="right">
                                                                    <asp:Label ID="lblcheckno" Text="Check Number:" runat="server" Font-Names="Verdana"
                                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td style="width: 50%;" align="left">
                                                                    <asp:TextBox ID="txtCheckNo" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                        Width="150px" MaxLength="6"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtCheckNo"
                                                                        Display="Dynamic" ErrorMessage="*" Font-Size="13" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <tr>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="BtnSave" runat="server" Text="Approve" Font-Names="Verdana" Font-Size="12px"
                                                        ValidationGroup="Save" BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black"
                                                        OnClick="BtnSave_Click" />
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
