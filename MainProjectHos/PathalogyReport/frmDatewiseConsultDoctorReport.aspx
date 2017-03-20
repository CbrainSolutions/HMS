<%@ Page Title="" Language="C#" MasterPageFile="~/mstAdmin.Master" AutoEventWireup="true" CodeBehind="frmDatewiseConsultDoctorReport.aspx.cs" Inherits="MainProjectHos.PathalogyReport.frmDatewiseConsultDoctorReport" %>
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

        function CheckList() {
            var dis = document.getElementById("<%=txtRecovery.ClientID %>").value;
            var Ser = document.getElementById("<%=txtPayable.ClientID %>").value;
            var total = document.getElementById("<%=txtamount.ClientID %>").value;

            var disAmt = total * dis / 100;
            var pay = parseFloat(total) - parseFloat(disAmt);
            document.getElementById("<%=txtPayable.ClientID %>").value = parseFloat(pay).toFixed(2);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div>
                <table align="center">
                    <tr>
                        <td>
                            <table border="1px" cellpadding="0" cellspacing="0" style="height: 60px;" width="702px"
                                align="center">
                                <tr>
                                    <td style="border-right: none; width: 150px; border-bottom: none;" align="center">
                                        <asp:TextBox ID="txtBillDate" runat="server" Font-Names="Verdana" Font-Size="11px"
                                            Width="150px" MaxLength="10" Format="MM/dd/yyyy hh:mm:ss tt"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ForeColor="Red"
                                            ControlToValidate="txtBillDate" Font-Size="13" ValidationGroup="Save" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    </td>
                                    <td style="border-left: none; border-right: none; width: 150px; border-bottom: none;">
                                        <asp:TextBox ID="txtToDate" runat="server" Font-Names="Verdana" Font-Size="11px"
                                            Width="150px" MaxLength="10" Format="MM/dd/yyyy hh:mm:ss tt"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red"
                                            ControlToValidate="txtToDate" Font-Size="13" ValidationGroup="Save" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    </td>
                                    <td align="center" style="border-left: none; border-right: none; width: 60px; border-bottom: none;">
                                        <asp:Button ID="btnSearch" Text="Search" Font-Names="Verdana" Font-Size="14px" runat="server"
                                            Width="80px" Style="border: 1px solid black" OnClick="btnSearch_Click" BackColor="#3b3535"
                                            ForeColor="White" />
                                    </td>
                                    <td align="center" style="border-left: none; border-right: none; width: 60px; border-bottom: none;">
                                        <asp:Button ID="btnPrint" Text="Print" Font-Names="Verdana" Font-Size="14px" runat="server"
                                            Width="80px" Style="border: 1px solid black" BackColor="#3b3535" ForeColor="White"
                                            OnClientClick="javascript:return PrintPanel()" />
                                    </td>
                                    <td align="center" style="border-bottom: none; border-left: none; width: 60px;">
                                        <asp:Button ID="btnExcel" runat="server" Text="Excel" Font-Names="Verdana" Font-Size="14px"
                                            BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black;"
                                            onmouseover="SetBtnMouseOver(this)" onmouseout="SetBtnMouseOut(this)" OnClick="btnExcel_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="border-top: none; border-right: none;">
                                        <asp:RegularExpressionValidator ID="revTxtFirstName" ValidationExpression="^([0]\d|[1][0-2])\/([0-2]\d|[3][0-1])\/([2][01]|[1][6-9])\d{2}(\s([0-1]\d|[2][0-3])(\:[0-5]\d){1,2})?$"
                                            ErrorMessage="Please Enter Only Date With MM/DD/YYYY Format" Font-Bold="False"
                                            Font-Size="11px" ForeColor="Red" ControlToValidate="txtBillDate" runat="server"
                                            ValidationGroup="Save" Display="Dynamic" Font-Names="verdana" />
                                        <asp:Calendar ID="CalBillDate" runat="server" TargetControlID="txtBillDate"
                                            Format="MM/dd/yyyy" DaysModeTitleFormat="MM/dd/yyyy" TodaysDateFormat="MM/dd/yyyy">
                                        </asp:Calendar>
                                    </td>
                                    <td style="border-top: none; border-left: none;" colspan="4">
                                        <asp:RegularExpressionValidator ID="regToDate" ValidationExpression="^([0]\d|[1][0-2])\/([0-2]\d|[3][0-1])\/([2][01]|[1][6-9])\d{2}(\s([0-1]\d|[2][0-3])(\:[0-5]\d){1,2})?$"
                                            ErrorMessage="Please Enter Only Date With MM/DD/YYYY Format" Font-Bold="False"
                                            Font-Size="11px" ForeColor="Red" ControlToValidate="txtToDate" runat="server"
                                            ValidationGroup="Save" Display="Dynamic" Font-Names="verdana" />
                                        <asp:Calendar ID="CalToDate" runat="server" TargetControlID="txtToDate" Format="MM/dd/yyyy"
                                            DaysModeTitleFormat="MM/dd/yyyy" TodaysDateFormat="MM/dd/yyyy">
                                        </asp:Calendar>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="padding-left: 5px; border-top: none; border-right: none;"
                                        colspan='2'>
                                        <asp:Label ID="Label12" runat="server" Text="Dept. Category :" Font-Names="Verdana"
                                            Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                        <asp:DropDownList ID="ddlDeptCategory" runat="server" Font-Names="Verdana" Font-Size="11px"
                                            Width="140px" AutoPostBack="true" OnSelectedIndexChanged="ddlDeptCategory_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="left" style="border-top: none; border-left: none;" colspan='3'>
                                        <asp:Label ID="Label20" runat="server" Text="Incharge Doctor :" Font-Names="Verdana"
                                            Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                        <asp:DropDownList ID="ddlDeptDoctor" runat="server" Font-Names="Verdana" Font-Size="11px"
                                            Width="140px">
                                        </asp:DropDownList>
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
                    <asp:Panel ID="Panel2" ScrollBars="Both" runat="server" Width="80%" Style="text-align: center;
                        background-color: #E0F0E8; height: auto;" BorderColor="Green" BorderStyle="Solid"
                        BorderWidth="1px">
                        <asp:GridView ID="dgvTestParameter" runat="server" CellPadding="4" ForeColor="#333333"
                            GridLines="Both" Font-Names="Verdana" Width="100%" Font-Size="Small" AllowPaging="false"
                            AutoGenerateColumns="false" Height="100">
                            <FooterStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" />
                            <RowStyle BackColor="#E0F0E8" Font-Size="11px" Wrap="False" />
                            <PagerStyle BackColor="#3b3535" ForeColor="White" HorizontalAlign="Left" Font-Size="11px" />
                            <SelectedRowStyle BackColor="#006600" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" Font-Size="12px"
                                Wrap="False" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" Wrap="False" />
                            <Columns>
                                <asp:BoundField DataField="colSrNo" HeaderText="Sr No" ReadOnly="True" SortExpression="colSrNo" />
                                <asp:BoundField DataField="Consult_Date" HeaderText="Treatment Date" ReadOnly="True"
                                    DataFormatString="{0:MM/dd/yyy}" SortExpression="Consult_Date" />
                                <asp:BoundField DataField="CategoryName" HeaderText="Category Name" ReadOnly="True"
                                    SortExpression="CategoryName" />
                                <asp:BoundField DataField="AnaesthetistName" HeaderText="Consulting Doctor" ReadOnly="True"
                                    SortExpression="AnaesthetistName" />
                                <asp:BoundField DataField="ConsultCharges" HeaderText="Charges" ReadOnly="True" SortExpression="ConsultCharges" />
                                <asp:BoundField DataField="PatientName" HeaderText="Patient Name" ReadOnly="True"
                                    SortExpression="PatientName" />
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>
                    <table width="80%">
                        <tr>
                            <td style="width: 50%" align="right">
                                <asp:Label ID="lblamount" runat="server" Text="Total Amount : "></asp:Label>
                            </td>
                            <td align="left" style="width: 50%;">
                                <asp:TextBox ID="txtamount" runat="server" Width="140px">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 50%" align="right">
                                <asp:Label ID="lblRecoveryAmt" runat="server" Text="Hospital Recovery Charge(%): "></asp:Label>
                            </td>
                            <td align="left" style="width: 50%;">
                                <asp:TextBox ID="txtRecovery" runat="server" Width="42px" MaxLength="10" OnTextChanged="txtRecovery_TextChanged"
                                    AutoPostBack="true">
                                </asp:TextBox>
                                <asp:TextBox ID="txtRecAmt" runat="server" Width="90px" MaxLength="10" OnTextChanged="txtRecAmt_TextChanged"
                                    AutoPostBack="true">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 50%" align="right">
                                <asp:Label ID="lblPayable" runat="server" Text="Payable Amount: "></asp:Label>
                            </td>
                            <td align="left" style="width: 50%;">
                                <asp:TextBox ID="txtPayable" runat="server" Width="140px">
                                </asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
