<%@ Page Title="" Language="C#" MasterPageFile="~/mstAdmin.Master" AutoEventWireup="true" CodeBehind="frmSupplierPayment.aspx.cs" Inherits="MainProjectHos.frmSupplierPayment" %>
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
                                    <asp:Label ID="lblCUstomer" runat="server" Text="Supplier Payment" Font-Names="Verdana"
                                        Font-Size="16px" Font-Bold="True" ForeColor="#3B3535"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Label ID="lblMsg" ForeColor="Red" runat="server" Font-Names="verdana" Font-Size="11px" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                     <table>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Label ID="lblNote" Text="NOTE :- Please Pay Full Amount" runat="server" ForeColor="Red"
                                                        Font-Names="Verdana" Font-Size="11px" />
                                            </td>                                            
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblShift" runat="server" Text="Supplier Name :" Font-Names="Verdana"
                                                    Font-Size="11px" ForeColor="#3B3535"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlSupplier" runat="server" Style="width: 150px;" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlSupplier_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red"
                                                    ControlToValidate="ddlSupplier" Font-Size="13" ValidationGroup="Save" ErrorMessage="*"
                                                    Display="Dynamic" InitialValue="0"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                         <tr>
                                             <td>
                                                 <asp:Label ID="Label7" runat="server" Text="TransactionDate :" Font-Names="Verdana"
                                                     Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                             </td>
                                             <td>
                                                 <asp:TextBox ID="txtTransactDate" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                     Width="150px" MaxLength="10" Format="MM/dd/yyyy hh:mm:ss tt"></asp:TextBox>
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ForeColor="Red"
                                                     ControlToValidate="txtTransactDate" Font-Size="13" ValidationGroup="Save" ErrorMessage="*"
                                                     Display="Dynamic"></asp:RequiredFieldValidator>
                                                 <asp:Calendar ID="CalDOBDate" runat="server" TargetControlID="txtTransactDate"
                                                     Format="MM/dd/yyyy" DaysModeTitleFormat="MM/dd/yyyy" TodaysDateFormat="MM/dd/yyyy hh:mm:ss tt">
                                                 </asp:Calendar>
                                             </td>
                                         </tr>
                                         <tr>
                                             <td align="center" colspan="2">
                                                 <asp:RegularExpressionValidator ID="revTxtFirstName" ValidationExpression="^([0]\d|[1][0-2])\/([0-2]\d|[3][0-1])\/([2][01]|[1][6-9])\d{2}(\s([0-1]\d|[2][0-3])(\:[0-5]\d){1,2})?$"
                                                     ErrorMessage="Please Enter Only Date With MM/DD/YYYY Format" Font-Bold="False"
                                                     Font-Size="11px" ForeColor="Red" ControlToValidate="txtTransactDate" runat="server"
                                                     ValidationGroup="Save" Display="Dynamic" Font-Names="Verdana" />
                                             </td>
                                         </tr>
                                         <tr>
                                             <td align="right">
                                                 <asp:Label ID="Label3" runat="server" Text="Payable Amount" Font-Names="Verdana"
                                                     Font-Size="11px" ForeColor="#3B3535"></asp:Label>
                                             </td>
                                             <td align="left">
                                                 <asp:TextBox ID="txtTotal" runat="server"></asp:TextBox>
                                             </td>
                                         </tr>
                                         <tr>
                                             <td align="right">
                                                 <asp:Label ID="lbltransact" runat="server" Text="Transaction Type :" Font-Names="Verdana"
                                                     Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                             </td>
                                             <td align="left">
                                                 <asp:RadioButton ID="IsCash" runat="server" Text="Is Cash" GroupName="AmountType"
                                                     OnCheckedChanged="IsCash_CheckedChanged" AutoPostBack="True" Font-Names="verdana"
                                                     Font-Size="11px"></asp:RadioButton>
                                                 <asp:RadioButton ID="IsCheque" runat="server" Text="Is Cheque" GroupName="AmountType"
                                                     OnCheckedChanged="IsCash_CheckedChanged" AutoPostBack="True" Font-Names="verdana"
                                                     Font-Size="11px"></asp:RadioButton>
                                                 <asp:RadioButton ID="RdoCard" runat="server" Text="Is Card" GroupName="AmountType"
                                                     OnCheckedChanged="IsCash_CheckedChanged" AutoPostBack="True" Font-Names="verdana"
                                                     Font-Size="11px"></asp:RadioButton>
                                             </td>
                                         </tr>
                                         <tr>
                                             <td align="right">
                                                 <asp:Label ID="lblBankNameI" runat="server" Text="Deposited In :" Font-Names="Verdana"
                                                     Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                             </td>
                                             <td align="left">
                                                 <asp:DropDownList ID="ddlBank" runat="server" Style="width: 150px;">
                                                 </asp:DropDownList>
                                                 <asp:RequiredFieldValidator ID="rfvBank" runat="server" ForeColor="Red" ControlToValidate="ddlBank"
                                                     Font-Size="13" ValidationGroup="Save" ErrorMessage="*" Display="Dynamic" InitialValue="0"></asp:RequiredFieldValidator>
                                             </td>
                                         </tr>
                                         <tr>
                                             <td align="right">
                                                 <asp:Label ID="lblCashAmount" runat="server" Text="Amount :" Font-Names="Verdana"
                                                     Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                             </td>
                                             <td align="left">
                                                 <asp:TextBox ID="txtPayAmount" runat="server"></asp:TextBox>
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ForeColor="Red"
                                                     ControlToValidate="txtPayAmount" Font-Size="13" ValidationGroup="Save" ErrorMessage="*"
                                                     Display="Dynamic"></asp:RequiredFieldValidator>
                                             </td>
                                         </tr>
                                         <tr>
                                             <td align="right">
                                                 <asp:Label ID="lblchequeNo" runat="server" Text="Cheque No :" Font-Names="Verdana"
                                                     Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                             </td>
                                             <td align="left">
                                                 <asp:TextBox ID="txtChequeNo" runat="server"></asp:TextBox>
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ForeColor="Red"
                                                     ControlToValidate="txtChequeNo" Font-Size="13" ValidationGroup="Save" ErrorMessage="*"
                                                     Display="Dynamic"></asp:RequiredFieldValidator>
                                             </td>
                                         </tr>
                                         <tr>
                                             <td align="right">
                                                 <asp:Label ID="lblChequeDate" runat="server" Text="Cheque Date :" Font-Names="Verdana"
                                                     Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                             </td>
                                             <td align="left">
                                                 <asp:TextBox ID="txtChequeDate" runat="server"></asp:TextBox>
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ForeColor="Red"
                                                     ControlToValidate="txtChequeDate" Font-Size="13" ValidationGroup="Save" ErrorMessage="*"
                                                     Display="Dynamic"></asp:RequiredFieldValidator>
                                                 <asp:Calendar ID="CalChecqueDate" runat="server" TargetControlID="txtChequeDate"
                                                     Format="MM/dd/yyyy" DaysModeTitleFormat="MM/dd/yyyy" TodaysDateFormat="MM/dd/yyyy hh:mm:ss tt">
                                                 </asp:Calendar>
                                             </td>
                                         </tr>
                                         <tr>
                                             <td align="center" colspan="2">
                                                 <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationExpression="^([0]\d|[1][0-2])\/([0-2]\d|[3][0-1])\/([2][01]|[1][6-9])\d{2}(\s([0-1]\d|[2][0-3])(\:[0-5]\d){1,2})?$"
                                                     ErrorMessage="Please Enter Only Date With MM/DD/YYYY Format" Font-Bold="False"
                                                     Font-Size="11px" ForeColor="Red" ControlToValidate="txtChequeDate" runat="server"
                                                     ValidationGroup="Save" Display="Dynamic" Font-Names="Verdana" />
                                             </td>
                                         </tr>
                                         <tr>
                                             <td align="right">
                                                 <asp:Label ID="lblBankName" runat="server" Text="Bank Name :" Font-Names="Verdana"
                                                     Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                             </td>
                                             <td align="left">
                                                 <asp:TextBox ID="txtBankName" runat="server"></asp:TextBox>
                                             </td>
                                         </tr>
                                         <tr style="height: 7px;">
                                             <td colspan="3">
                                                 &nbsp;
                                             </td>
                                         </tr>
                                         <tr align="center">
                                             <td align="center" colspan="3">
                                                 <asp:Button ID="BtnSave" runat="server" Text="Save" Font-Names="Verdana" Font-Size="12px"
                                                     ValidationGroup="Save" BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black;"
                                                     OnClick="BtnSave_Click" />
                                                 <asp:Button ID="BtnUpdate" runat="server" BackColor="#3b3535" Font-Names="Verdana"
                                                     Font-Size="12px" ForeColor="White" OnClick="BtnEdit_Click" Style="border: 1px solid black"
                                                     Text="Update" ValidationGroup="Save" Width="80px" />
                                                 <asp:Button ID="btnCancel" runat="server" BackColor="#3b3535" Font-Names="Verdana"
                                                     Font-Size="12px" ForeColor="White" OnClick="btnCancel_Click" Style="border: 1px solid black"
                                                     Text="Cancel" Width="80px" />
                                             </td>
                                         </tr>
                                     </table>                               
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                    <asp:View runat="server" ID="View2">
                        <div style="width: 100%; height: auto">
                            <br />
                            <table width="100%">
                                <tr style="width: 100%;">
                                    <td colspan="3" align="center">
                                        <asp:Label ID="lblPatientReceipt" runat="server" Text="Supplier Payment" Font-Names="Verdana"
                                            Font-Size="16px" Font-Bold="True" ForeColor="#3B3535"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" style="width: 15%;" colspan="3">
                                        <table border="1px" cellpadding="0" cellspacing="0" width="860px" style="height: 40px;">
                                            <tr style="width: 100%">
                                                <td align="center" style="border-right: none; width: 575px;">
                                                    <asp:TextBox ID="txtSearch" runat="server" Width="580px"></asp:TextBox>
                                                    <cc:TextBoxWatermarkExtender ID="txtSearchWatermark" runat="server" TargetControlID="txtSearch"
                                                        WatermarkText="Patient Name, Address" WatermarkCssClass="watermarked" />
                                                </td>
                                                <td align="center" style="border-left: none; border-right: none; width: 60px;">
                                                    <asp:Button ID="btnSearch" runat="server" Text="Search" Font-Names="Verdana" Font-Size="12px"
                                                        BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black"
                                                        OnClick="btnSearch_Click" />
                                                </td>
                                                <td align="center" style="border-left: none; border-right: none; width: 90px;">
                                                    <asp:Button ID="btnReset" runat="server" BackColor="#3b3535" Font-Names="Verdana"
                                                        Font-Size="12px" ForeColor="White" OnClick="btnReset_Click" Style="border: 1px solid black"
                                                        Text="Reset" Width="80px" />
                                                </td>
                                                <td style="border-left: none; width: 60px; padding-right: 4px;">
                                                    <asp:Button ID="btnAddNew" runat="server" BackColor="#3b3535" Font-Names="Verdana"
                                                        Font-Size="12px" ForeColor="White" OnClick="btnAddNew_Click" Style="border: 1px solid black"
                                                        Text="New" Width="80px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="2">
                                        <asp:Label ID="lblPageCount" runat="server" Text="" Font-Names="Verdana" Font-Size="11px"
                                            ForeColor="#3b3535"></asp:Label>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="lblRowCount1" runat="server" Font-Names="Verdana" Font-Size="11px"
                                            ForeColor="#3b3535" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="3">
                                        <asp:Label ID="lblMessage" runat="server" Text="" Font-Names="Verdana" Font-Size="11px"
                                            ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <asp:GridView ID="dgvCustTransaction" runat="server" CellPadding="4" ForeColor="#333333"
                                DataKeyNames="ReceiptNo" GridLines="Both" Font-Names="Verdana" 
                                Width="100%" Font-Size="Small"
                                AllowPaging="true" PageSize="15" AutoGenerateColumns="false" 
                                OnPageIndexChanged="dgvCustTransaction_PageIndexChanged" OnPageIndexChanging="dgvCustTransaction_PageIndexChanging">
                                <FooterStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" />
                                <RowStyle BackColor="#E0F0E8" Font-Size="11px" Wrap="False" Font-Names="Verdana" />
                                <PagerStyle BackColor="#3b3535" ForeColor="White" HorizontalAlign="Left" Font-Size="11px" />
                                <SelectedRowStyle BackColor="#006600" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" Font-Size="12px"
                                    Wrap="False" Font-Names="verdana" />
                                <EditRowStyle BackColor="#2461BF" />
                                <AlternatingRowStyle BackColor="White" Wrap="False" />
                                <Columns>
                                    <asp:BoundField DataField="ReceiptNo" HeaderText="Voucher No." ReadOnly="True" SortExpression="ReceiptNo" />
                                    <asp:BoundField DataField="ReceiptDate" HeaderText="Date" ReadOnly="True" SortExpression="ReceiptDate"
                                        DataFormatString="{0:MM/dd/yyyy}" />
                                    <asp:BoundField DataField="SupplierName" HeaderText="Supplier Name" ReadOnly="True"
                                        SortExpression="SupplierName" />
                                        <asp:BoundField DataField="Address" HeaderText="Address" ReadOnly="True"
                                        SortExpression="Address" />
                                    <asp:BoundField DataField="Amount" HeaderText="Amount" ReadOnly="True" SortExpression="Amount" />
                                    <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageEdit" runat="server" ImageUrl="~/images/edit.jpg" Height="24px"
                                                Width="24px" OnClick="btnUpdate_Click" /></ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Print" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImagePrint" runat="server" ImageUrl="~/images/Report.bmp" Height="24px"
                                                Width="24px" OnClick="btnPrint_Click" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </asp:View>
                </asp:MultiView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
