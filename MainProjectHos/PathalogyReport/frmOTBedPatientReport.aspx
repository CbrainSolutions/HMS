<%@ Page Title="" Language="C#" MasterPageFile="~/mstAdmin.Master" AutoEventWireup="true" CodeBehind="frmOTBedPatientReport.aspx.cs" Inherits="MainProjectHos.PathalogyReport.frmOTBedPatientReport" %>
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
            var printWindow = window.open('', '', 'height=400,width=1000');
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
                <table align="center">
                    <tr style="height: 15px;">
                        <td>
                            <table border="1px" cellpadding="0" cellspacing="0" width="450px" style="height: 80px;">
                                <tr>
                                    <td colspan='3' style="border-left: none; border-right: none; border-top: none; border-bottom: none;">
                                        <asp:Label ID="lblStart" runat="server" ForeColor="#3B3535" Text="From:" Width="30px"
                                            Font-Names="Verdana" Font-Size="11px" Font-Bold="True" />
                                        <asp:TextBox ID="txtBillDate" runat="server" Font-Names="Verdana" Font-Size="11px"
                                            Width="120px" MaxLength="10" Format="MM/dd/yyyy hh:mm:ss tt"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ForeColor="Red"
                                            ControlToValidate="txtBillDate" ValidationGroup="Save" Font-Size="13" ErrorMessage="*"></asp:RequiredFieldValidator>
                                        <asp:Label ID="lblToDat" runat="server" ForeColor="#3B3535" Text="To :" Width="30px"
                                            Font-Names="Verdana" Font-Size="11px" Font-Bold="True" />
                                        <asp:TextBox ID="txtToDate" runat="server" Font-Names="Verdana" Font-Size="11px"
                                            Width="120px" MaxLength="10" Format="MM/dd/yyyy hh:mm:ss tt"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red"
                                            ControlToValidate="txtToDate" Font-Size="13" ValidationGroup="Save" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" style="border-left: none; border-right: none; border-top: none;
                                        width: 70px; border-bottom: none;">
                                        <asp:Button ID="btnSearch" Text="Show" Font-Names="Verdana" Font-Size="14px" runat="server"
                                            Width="80px" Style="border: 1px solid black" OnClick="btnSearch_Click" BackColor="#3b3535"
                                            ForeColor="White" />
                                    </td>
                                    <td align="center" style="border-left: none; border-right: none; border-top: none;
                                        width: 70px; border-bottom: none;">
                                        <asp:Button ID="btnPrint" Text="Print" Font-Names="Verdana" Font-Size="14px" runat="server"
                                            Width="80px" Style="border: 1px solid black;" BackColor="#3b3535" ForeColor="White"
                                            OnClientClick="javascript:return PrintPanel()" />
                                    </td>
                                    <td align="center" style="border-bottom: none; border-left: none; border-top: none;
                                        width: 70px; border-right: none;">
                                        <asp:Button ID="btnExcel" runat="server" Text="Excel" Font-Names="Verdana" Font-Size="14px"
                                            BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black;"
                                            onmouseover="SetBtnMouseOver(this)" onmouseout="SetBtnMouseOut(this)" OnClick="btnExcel_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan='3' style="border-bottom: none; border-left: none; border-top: none; border-right: none;">
                                        <asp:RegularExpressionValidator ID="revTxtFirstName" ValidationExpression="^([0]\d|[1][0-2])\/([0-2]\d|[3][0-1])\/([2][01]|[1][6-9])\d{2}(\s([0-1]\d|[2][0-3])(\:[0-5]\d){1,2})?$"
                                            ErrorMessage="Please Enter Only Date With MM/DD/YYYY Format" Font-Bold="False"
                                            Font-Size="11px" ForeColor="Red" ControlToValidate="txtBillDate" runat="server"
                                            ValidationGroup="Save" Display="Dynamic" Font-Names="verdana" />
                                        <cc:CalendarExtender ID="CalBillDate" runat="server" TargetControlID="txtBillDate"
                                            Format="MM/dd/yyyy" DaysModeTitleFormat="MM/dd/yyyy" TodaysDateFormat="MM/dd/yyyy">
                                        </cc:CalendarExtender>
                                    
                                        <asp:RegularExpressionValidator ID="regToDate" ValidationExpression="^([0]\d|[1][0-2])\/([0-2]\d|[3][0-1])\/([2][01]|[1][6-9])\d{2}(\s([0-1]\d|[2][0-3])(\:[0-5]\d){1,2})?$"
                                            ErrorMessage="Please Enter Only Date With MM/DD/YYYY Format" Font-Bold="False"
                                            Font-Size="11px" ForeColor="Red" ControlToValidate="txtToDate" runat="server"
                                            ValidationGroup="Save" Display="Dynamic" Font-Names="verdana" />
                                        <cc:CalendarExtender ID="CalToDate" runat="server" TargetControlID="txtToDate" Format="MM/dd/yyyy"
                                            DaysModeTitleFormat="MM/dd/yyyy" TodaysDateFormat="MM/dd/yyyy">
                                        </cc:CalendarExtender>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <asp:Label ID="lblMessage" runat="server" Text="" Font-Names="Verdana" Font-Size="11px"
                    ForeColor="Red"></asp:Label>
                <asp:Panel ID="pnlContents" runat="server" Width="100%" align="center">
                    <table width="100%" style="overflow: hidden; table-layout: fixed; position: relative;">
                        <tr>
                            <td colspan="2" align="center">
                                <asp:Label ID="lbl" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="width: 100%">
                                <asp:Panel ID="Panel2" ScrollBars="Both" runat="server" Width="100%" Style="text-align: center;
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
                                            <asp:BoundField DataField="BedNo" HeaderText="Bed No" ReadOnly="true" SortExpression="BedNo" />
                                            <asp:BoundField DataField="IPDNo" HeaderText="IPDNo" ReadOnly="True" SortExpression="IDPNo" />
                                            <asp:BoundField DataField="PatientName" HeaderText="PatientName" ReadOnly="True"
                                                SortExpression="PatientName" />
                                            <asp:BoundField DataField="Age" HeaderText="Age" ReadOnly="True" SortExpression="Age" />
                                            <asp:BoundField DataField="GenderDesc" HeaderText="Gender" ReadOnly="True" SortExpression="GenderDesc" />
                                            <asp:BoundField DataField="AllocationDate" HeaderText="Date" ReadOnly="True" SortExpression="AllocationDate"
                                                DataFormatString="{0:MM/dd/yyyy}" />
                                            <asp:BoundField DataField="DoctorName" HeaderText="Doctor" ReadOnly="True" SortExpression="DoctorName" />
                                            <asp:BoundField DataField="Surgeon" HeaderText="Surgeon" ReadOnly="True" SortExpression="Surgeon" />
                                            <asp:BoundField DataField="AnaesthetistName" HeaderText="Anaesthetist" ReadOnly="True"
                                                SortExpression="AnaesthetistName" />
                                            <asp:BoundField DataField="TypeOfAnaesthesia" HeaderText="TypeOfAnaesthesia" ReadOnly="True"
                                                SortExpression="TypeOfAnaesthesia" />
                                            <asp:BoundField DataField="NurseName" HeaderText="AssistantNurse" ReadOnly="True"
                                                SortExpression="NurseName" />
                                            <asp:BoundField DataField="CategoryName" HeaderText="OperationCategory" ReadOnly="True"
                                                SortExpression="CategoryName" />
                                            <asp:BoundField DataField="OperationName" HeaderText="Surgery/OperationName" ReadOnly="True"
                                                SortExpression="OperationName" />
                                            <asp:BoundField DataField="Dignosys" HeaderText="Dignosys" ReadOnly="True" SortExpression="Dignosys" />
                                            <asp:BoundField DataField="Implant" HeaderText="Implant" ReadOnly="True" SortExpression="Implant" />
                                            <asp:BoundField DataField="MaterialForHPE" HeaderText="MaterialForH.P.E." ReadOnly="True"
                                                SortExpression="MaterialForHPE" />
                                            <asp:BoundField DataField="AnaestheticNote" HeaderText="Anaesthetic Note" ReadOnly="true"
                                                SortExpression="AnaestheticNote" />
                                            <asp:BoundField DataField="SurgeryNote" HeaderText="Surgery Note" ReadOnly="true"
                                                SortExpression="SurgeryNote" />
                                            <%--<asp:BoundField DataField="PatientId" HeaderText="PatientId" ReadOnly="True" SortExpression="PatientId" />
                                                <asp:BoundField DataField="AdmitId" HeaderText="AdmitId" ReadOnly="True" SortExpression="AdmitId" />--%>
                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
