<%@ Page Title="" Language="C#" MasterPageFile="~/mstAdmin.Master" AutoEventWireup="true" CodeBehind="frmSummarizedDetailReport.aspx.cs" Inherits="MainProjectHos.PathalogyReport.frmSummarizedDetailReport" %>
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
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=pnlContents.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head><title>SEVADHAM HOSPITAL TALEGAON DABHADE(STATION)</title>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div>
                <table align="center" border="1px">
                    <tr style="height: 15px;">
                        <td>
                            <table cellpadding="0" cellspacing="0" width="980px" style="height: 40px;">
                                <tr>
                                    <td colspan="5">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="padding-left: 5px;">
                                        <asp:Label ID="Label2" runat="server" Text="From Date:" Font-Names="Verdana" Font-Size="11px"
                                            ForeColor="#3b3535"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtBillDate" runat="server" Font-Names="Verdana" Font-Size="11px"
                                            Width="110px" MaxLength="10" Format="MM/dd/yyyy hh:mm:ss tt"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ForeColor="Red"
                                            ControlToValidate="txtBillDate" Font-Size="13" ValidationGroup="Save" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="Label3" runat="server" Text="To Date:" Font-Names="Verdana" Font-Size="11px"
                                            ForeColor="#3b3535"></asp:Label>
                                    </td>
                                    <td align="left" colspan="2">
                                        <asp:TextBox ID="txtToDate" runat="server" Font-Names="Verdana" Font-Size="11px"
                                            Width="120px" MaxLength="10" Format="MM/dd/yyyy hh:mm:ss tt"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red"
                                            ControlToValidate="txtToDate" Font-Size="13" ValidationGroup="Save" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:RegularExpressionValidator ID="revTxtFirstName" ValidationExpression="^([0]\d|[1][0-2])\/([0-2]\d|[3][0-1])\/([2][01]|[1][6-9])\d{2}(\s([0-1]\d|[2][0-3])(\:[0-5]\d){1,2})?$"
                                            ErrorMessage="Please Enter Only Date With MM/DD/YYYY Format" Font-Bold="False"
                                            Font-Size="11px" ForeColor="Red" ControlToValidate="txtBillDate" runat="server"
                                            ValidationGroup="Save" Display="Dynamic" Font-Names="verdana" />
                                        <cc:CalendarExtender ID="CalBillDate" runat="server" TargetControlID="txtBillDate"
                                            Format="MM/dd/yyyy" DaysModeTitleFormat="MM/dd/yyyy" TodaysDateFormat="MM/dd/yyyy">
                                        </cc:CalendarExtender>
                                    </td>
                                    <td colspan="4">
                                        <asp:RegularExpressionValidator ID="regToDate" ValidationExpression="^([0]\d|[1][0-2])\/([0-2]\d|[3][0-1])\/([2][01]|[1][6-9])\d{2}(\s([0-1]\d|[2][0-3])(\:[0-5]\d){1,2})?$"
                                            ErrorMessage="Please Enter Only Date With MM/DD/YYYY Format" Font-Bold="False"
                                            Font-Size="11px" ForeColor="Red" ControlToValidate="txtToDate" runat="server"
                                            ValidationGroup="Save" Display="Dynamic" Font-Names="verdana" />
                                        <cc:CalendarExtender ID="CalToDate" runat="server" TargetControlID="txtToDate" Format="MM/dd/yyyy"
                                            DaysModeTitleFormat="MM/dd/yyyy" TodaysDateFormat="MM/dd/yyyy">
                                        </cc:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        &nbsp;
                                    </td>
                                </tr>
                                <%--<tr>
                                    <td align="right" style="padding-left: 5px;">
                                        <asp:Label ID="Label1" runat="server" Text="User Name :" Font-Names="Verdana" Font-Size="11px"
                                            ForeColor="#3b3535"></asp:Label>
                                    </td>
                                    <td align="left" colspan="4">
                                        <asp:DropDownList ID="ddlEmployee" runat="server" Font-Names="Verdana" Font-Size="11px"
                                            Width="120px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        &nbsp;
                                    </td>
                                </tr>--%>
                                <tr>
                                    <td align="center" colspan="5">
                                        <asp:Button ID="btnSearch" Text="Search" Font-Names="Verdana" Font-Size="14px" runat="server"
                                            Width="80px" Style="border: 1px solid black" OnClick="btnSearch_Click" BackColor="#3b3535"
                                            ForeColor="White" />
                                        <asp:Button ID="btnReset" runat="server" BackColor="#3b3535" Font-Names="Verdana"
                                            Font-Size="14px" ForeColor="White" OnClick="btnReset_Click" Style="border: 1px solid black"
                                            Text="Reset" Width="80px" />
                                        <asp:Button ID="btnExcel" runat="server" Text="Excel" Font-Names="Verdana" Font-Size="14px"
                                            BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black;"
                                            onmouseover="SetBtnMouseOver(this)" onmouseout="SetBtnMouseOut(this)" OnClick="btnExcel_Click" />
                                        <asp:Button ID="btnPrint" Text="Print" Font-Names="Verdana" Font-Size="14px" runat="server"
                                            Width="80px" Style="border: 1px solid black;" BackColor="#3b3535" ForeColor="White"
                                            OnClientClick="javascript:return PrintPanel()" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <asp:Label ID="lblMessage" runat="server" Text="" Font-Names="Verdana" Font-Size="11px"
                    ForeColor="Red"></asp:Label>
                <asp:Panel ID="pnlContents" runat="server" Width="100%" align="center">
                    <table style="width: 60%;" align="center">
                        <tr>
                            <td colspan="2" align="center">
                                <asp:Label ID="lbl" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:Label ID="lblFrom" runat="server" Text=""></asp:Label>
                            </td>
                            <td align="right">
                                <asp:Label ID="lblTo" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                    
                    <table width="80%">
                        <tr>
                            <td colspan='5'>
                                <asp:Label ID="Label7" Font-Bold="true" runat="server" Text="IPD Sale (Discharged Patients) : "></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 16%" align="left">
                                <asp:Label ID="lblAdvance" runat="server" Text="Cash : "></asp:Label>
                                <asp:TextBox ID="txtCash" runat="server" Width="125px" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td align="left" style="width: 16%">
                                <asp:Label ID="lblREceived" runat="server" Text="Cheque: "></asp:Label>
                                <asp:TextBox ID="txtCheque" runat="server" Width="125px" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td style="width: 16%" align="left">
                                <asp:Label ID="Label4" runat="server" Text="RTGS/NEFT: "></asp:Label>
                                <asp:TextBox ID="txtRTGS" runat="server" Width="125px" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td style="width: 16%" align="left">
                                <asp:Label ID="Label5" runat="server" Text="Card: "></asp:Label>
                                <asp:TextBox ID="txtCard" runat="server" Width="125px" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td style="width: 16%" align="left">
                                <asp:Label ID="Label6" runat="server" Text="Total: "></asp:Label>
                                <asp:TextBox ID="txtTotal" runat="server" Width="125px" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td style="width: 20%" align="left">
                                <asp:Label ID="Label19" runat="server" Text="Discount: "></asp:Label>
                                <asp:TextBox ID="txtDiscountIPD" runat="server" Width="125px" ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan='5'>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan='5'>
                                <asp:Label ID="Label13" Font-Bold="true" runat="server" Text="IPD Advanced Received : "></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 16%" align="left">
                                <asp:Label ID="Label14" runat="server" Text="Cash : "></asp:Label>
                                <asp:TextBox ID="txtCash2" runat="server" Width="125px" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td align="left" style="width: 16%">
                                <asp:Label ID="Label15" runat="server" Text="Cheque: "></asp:Label>
                                <asp:TextBox ID="txtCheque2" runat="server" Width="125px" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td style="width: 16%" align="left">
                                <asp:Label ID="Label16" runat="server" Text="RTGS/NEFT: "></asp:Label>
                                <asp:TextBox ID="txtRTGs2" runat="server" Width="125px" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td style="width: 16%" align="left">
                                <asp:Label ID="Label17" runat="server" Text="Card: "></asp:Label>
                                <asp:TextBox ID="txtCard2" runat="server" Width="125px" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td style="width: 16%" align="left">
                                <asp:Label ID="Label18" runat="server" Text="Total: "></asp:Label>
                                <asp:TextBox ID="txtTotal2" runat="server" Width="125px" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td style="width: 20%" align="left">
                            
                            </td>
                        </tr>
                        <tr>
                            <td colspan='5'>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan='5'>
                                <asp:Label ID="Label25" Font-Bold="true" runat="server" Text="Insurance Sale : "></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 16%" align="left">
                                <asp:Label ID="Label52" runat="server" Text="Cash : "></asp:Label>
                                <asp:TextBox ID="txtCashInsurance" runat="server" Width="125px" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td align="left" style="width: 16%">
                                <asp:Label ID="Label45" runat="server" Text="Cheque: "></asp:Label>
                                <asp:TextBox ID="txtChequeInsurance" runat="server" Width="125px" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td style="width: 16%" align="left">
                                <asp:Label ID="Label46" runat="server" Text="RTGS/NEFT: "></asp:Label>
                                <asp:TextBox ID="txtRTGSInsurance" runat="server" Width="125px" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td style="width: 16%" align="left">
                                <asp:Label ID="Label53" runat="server" Text="Card: "></asp:Label>
                                <asp:TextBox ID="txtCardInsurance" runat="server" Width="125px" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td style="width: 16%" align="left">
                                <asp:Label ID="Label48" runat="server" Text="Total: "></asp:Label>
                                <asp:TextBox ID="txtInsuranceTotal" runat="server" Width="125px" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td style="width: 20%" align="left">
                            
                            </td>
                        </tr>
                        <tr>
                            <td colspan='5'>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan='5'>
                                <asp:Label ID="Label26" Font-Bold="true" runat="server" Text="Company Sale : "></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 16%" align="left">
                                <asp:Label ID="Label54" runat="server" Text="Cash : "></asp:Label>
                                <asp:TextBox ID="txtCashCompany" runat="server" Width="125px" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td align="left" style="width: 16%">
                                <asp:Label ID="Label49" runat="server" Text="Cheque: "></asp:Label>
                                <asp:TextBox ID="txtChequeCompany" runat="server" Width="125px" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td style="width: 16%" align="left">
                                <asp:Label ID="Label47" runat="server" Text="RTGS/NEFT: "></asp:Label>
                                <asp:TextBox ID="txtRTGSCompany" runat="server" Width="125px" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td style="width: 16%" align="left">
                                <asp:Label ID="Label55" runat="server" Text="Card: "></asp:Label>
                                <asp:TextBox ID="txtCardCompany" runat="server" Width="125px" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td style="width: 16%" align="left">
                                <asp:Label ID="Label50" runat="server" Text="Total: "></asp:Label>
                                <asp:TextBox ID="txtCompanyTotal" runat="server" Width="125px" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td style="width: 20%" align="left">
                            
                            </td>
                        </tr>
                        <tr>
                            <td colspan='5'>
                                &nbsp;
                            </td>
                        </tr>
                       <tr>
                            <td colspan='5'>
                                <asp:Label ID="Label1" Font-Bold="true" runat="server" Text="OPD Sale (Consulting/Treatments/Daycare) : "></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 16%" align="left">
                                <asp:Label ID="Label8" runat="server" Text="Cash : "></asp:Label>
                                <asp:TextBox ID="txtCash1" runat="server" Width="125px" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td align="left" style="width: 16%">
                                <asp:Label ID="Label9" runat="server" Text="Cheque: "></asp:Label>
                                <asp:TextBox ID="txtCheque1" runat="server" Width="125px" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td style="width: 16%" align="left">
                                <asp:Label ID="Label10" runat="server" Text="RTGS/NEFT: "></asp:Label>
                                <asp:TextBox ID="txtRTGS1" runat="server" Width="125px" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td style="width: 16%" align="left">
                                <asp:Label ID="Label11" runat="server" Text="Card: "></asp:Label>
                                <asp:TextBox ID="txtCard1" runat="server" Width="125px" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td style="width: 16%" align="left">
                                <asp:Label ID="Label12" runat="server" Text="Total: "></asp:Label>
                                <asp:TextBox ID="txtTotal1" runat="server" Width="125px" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td style="width: 20%" align="left">
                                <asp:Label ID="Label20" runat="server" Text="Discount: "></asp:Label>
                                <asp:TextBox ID="txtDiscountOPD" runat="server" Width="125px" ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan='5'>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan='5'>
                                <asp:Label ID="Label33" Font-Bold="true" runat="server" Text="IPD Refund : "></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 16%" align="left">
                                <asp:Label ID="Label34" runat="server" Text="Cash : "></asp:Label>
                                <asp:TextBox ID="txtIPDCashRefund" runat="server" Width="125px" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td align="left" style="width: 16%">
                                <asp:Label ID="Label35" runat="server" Text="Cheque: "></asp:Label>
                                <asp:TextBox ID="txtIPDChequeRefund" runat="server" Width="125px" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td style="width: 16%" align="left">
                                <asp:Label ID="Label36" runat="server" Text="RTGS/NEFT: "></asp:Label>
                                <asp:TextBox ID="txtIPDRTGSRefund" runat="server" Width="125px" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td style="width: 16%" align="left">
                                <asp:Label ID="Label37" runat="server" Text="Card: "></asp:Label>
                                <asp:TextBox ID="txtIPDCardRefund" runat="server" Width="125px" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td style="width: 16%" align="left">
                                <asp:Label ID="Label38" runat="server" Text="Total: "></asp:Label>
                                <asp:TextBox ID="txtIPDTotalRefund" runat="server" Width="125px" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td style="width: 20%" align="left">
                            
                            </td>
                        </tr>
                        <tr>
                            <td colspan='5'>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan='5'>
                                <asp:Label ID="Label39" Font-Bold="true" runat="server" Text="OPD Refund : "></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 16%" align="left">
                                <asp:Label ID="Label40" runat="server" Text="Cash : "></asp:Label>
                                <asp:TextBox ID="txtOPDCashRefund" runat="server" Width="125px" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td align="left" style="width: 16%">
                                <asp:Label ID="Label41" runat="server" Text="Cheque: "></asp:Label>
                                <asp:TextBox ID="txtOPDChequeRefund" runat="server" Width="125px" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td style="width: 16%" align="left">
                                <asp:Label ID="Label42" runat="server" Text="RTGS/NEFT: "></asp:Label>
                                <asp:TextBox ID="txtOPDRTGSRefund" runat="server" Width="125px" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td style="width: 16%" align="left">
                                <asp:Label ID="Label43" runat="server" Text="Card: "></asp:Label>
                                <asp:TextBox ID="txtOPDCardRefund" runat="server" Width="125px" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td style="width: 16%" align="left">
                                <asp:Label ID="Label44" runat="server" Text="Total: "></asp:Label>
                                <asp:TextBox ID="txtOPDTotalRefund" runat="server" Width="125px" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td style="width: 20%" align="left">
                            
                            </td>
                        </tr>
                        <tr>
                            <td colspan='5'>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan='5'>
                                <asp:Label ID="Label21" Font-Bold="true" runat="server" Text="Registration Fees : "></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 16%" align="left">
                                <asp:Label ID="Label22" runat="server" Text="IPD : "></asp:Label>
                                <asp:TextBox ID="txtRegFeeIPD" runat="server" Width="125px" ReadOnly="true" ></asp:TextBox>
                            </td>
                            <td align="left" style="width: 16%">
                                <asp:Label ID="Label23" runat="server" Text="OPD : "></asp:Label>
                                <asp:TextBox ID="txtRegFeeOPD" runat="server" Width="125px" ReadOnly="true" ></asp:TextBox>
                            </td>
                            <td style="width: 16%" align="left">
                                
                            </td>
                            <td style="width: 16%" align="left">
                                
                            </td>
                            <td style="width: 16%" align="left">
                                <asp:Label ID="Label51" runat="server" Text="Total: "></asp:Label>
                                <asp:TextBox ID="txtTotalReg" runat="server" Width="125px" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td style="width: 20%" align="left">
                            
                            </td>
                        </tr>
                        <tr>
                            <td colspan='5'>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan='5'>
                                <asp:Label ID="Label24" Font-Bold="true" runat="server" Text="Credit Amount: "></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 16%" align="left">
                                <asp:TextBox ID="txtCreditAmt" runat="server" Width="125px" ReadOnly="true" ></asp:TextBox>
                            </td>
                            <td align="left" style="width: 16%">
                               
                            </td>
                            <td style="width: 16%" align="left">
                                
                            </td>
                            <td style="width: 16%" align="left">
                                
                            </td>
                            <td style="width: 16%" align="left">
                                
                            </td>
                            <td style="width: 20%" align="left">
                            
                            </td>
                        </tr>
                        <tr>
                            <td colspan='5'>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan='5'>
                                <asp:Label ID="Label27" Font-Bold="true" runat="server" Text="Total Collection : "></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 16%" align="left">
                                <asp:Label ID="Label28" runat="server" Text="Cash : "></asp:Label>
                                <asp:TextBox ID="txtTotalCash" runat="server" Width="125px" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td align="left" style="width: 16%">
                                <asp:Label ID="Label29" runat="server" Text="Cheque: "></asp:Label>
                                <asp:TextBox ID="txtTotalCheque" runat="server" Width="125px" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td style="width: 16%" align="left">
                                <asp:Label ID="Label30" runat="server" Text="RTGS/NEFT: "></asp:Label>
                                <asp:TextBox ID="txtTotalRTGS" runat="server" Width="125px" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td style="width: 16%" align="left">
                                <asp:Label ID="Label31" runat="server" Text="Card: "></asp:Label>
                                <asp:TextBox ID="txtTotalCard" runat="server" Width="125px" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td style="width: 16%" align="left">
                                <asp:Label ID="Label32" runat="server" Text="Total: "></asp:Label>
                                <asp:TextBox ID="txtTotalAmt" runat="server" Width="125px" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td style="width: 20%" align="left">
                            
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
