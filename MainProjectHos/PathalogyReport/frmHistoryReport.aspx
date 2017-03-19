<%@ Page Title="" Language="C#" MasterPageFile="~/mstAdmin.Master" AutoEventWireup="true" CodeBehind="frmHistoryReport.aspx.cs" Inherits="MainProjectHos.PathalogyReport.frmHistoryReport" %>
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
            var printWindow = window.open('', '', 'height=auto;width=auto');
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
                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View runat="server" ID="View1">
                        <table width="100%" align="center">
                            <tr>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table align="center" border="2px" cellpadding="0" cellspacing="0" width="845px">
                                        <tr style="height: 10px;">
                                            <td colspan="4" style="border-bottom: none;">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="width: 575px; border-top: none; border-bottom: none; border-right: none;">
                                                <asp:TextBox runat="server" ID="txtSearch" Width="550px" Font-Bold="true" Font-Names="Verdana"
                                                    Font-Size="13px" />
                                                <cc:TextBoxWatermarkExtender ID="txtSearchWatermark" runat="server" TargetControlID="txtSearch"
                                                    WatermarkText="Patient Name,Patitent Type" WatermarkCssClass="watermarked" />
                                            </td>
                                            <td style="width: 82px; border-top: none; border-bottom: none; border-right: none;
                                                border-left: none;">
                                                <asp:Button ID="btnSearch" Text="Search" runat="server" Font-Names="Verdana" Font-Size="14px"
                                                    ForeColor="White" BackColor="#3b3535" Width="80px" Style="border: 1px solid black"
                                                    OnClick="btnSearch_Click" />
                                            </td>
                                            <td align="center" style="border-left: none; border-top: none; border-right: none;
                                                border-bottom: none; width: 90px;">
                                                <asp:Button ID="btnReset" runat="server" Text="Reset" Font-Names="Verdana" Font-Size="14px"
                                                    BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black"
                                                    OnClick="btnReset_Click" />
                                            </td>
                                            <td style="width: 82px; border-top: none; border-bottom: none; border-left: none;">
                                                <asp:Button Text="Print" runat="server" Font-Names="Verdana" Font-Size="14px" Width="80px"
                                                    Style="border: 1px solid black" BackColor="#3b3535" ForeColor="White" OnClientClick="javascript:return PrintPanel()" />
                                            </td>
                                        </tr>
                                        <tr style="height: 10px;">
                                            <td colspan="4" style="border-top: none; border-bottom: none;">
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="border-top: none;">
                                    <asp:Label ID="lblMessage" runat="server" Text="" Font-Names="Verdana" Font-Size="11px"
                                        ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Panel ID="pnlContents" runat="server" Height="100%" Width="100%" align="center">
                                        <center>
                                            <asp:Label Text="Patient History" ID="lblPatientHistory" ForeColor="#3b3535" Font-Names="Verdana"
                                                Font-Size="16px" Font-Bold="true" runat="server" />
                                        </center>
                                        <asp:GridView ID="dgvTestParameter" runat="server" CellPadding="4" ForeColor="#333333"
                                            GridLines="Both" Font-Names="Verdana" Width="845px" Font-Size="Small" AllowPaging="false"
                                            AutoGenerateColumns="false" OnRowDataBound="dgvTestParameter_RowDataBound" align="Center">
                                            <FooterStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" />
                                            <RowStyle BackColor="#E0F0E8" Font-Size="11px" Wrap="False" />
                                            <PagerStyle BackColor="#3b3535" ForeColor="White" HorizontalAlign="Left" Font-Size="11px" />
                                            <SelectedRowStyle BackColor="#006600" Font-Bold="True" ForeColor="#333333" />
                                            <HeaderStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" Font-Size="12px"
                                                Wrap="False" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <AlternatingRowStyle BackColor="White" Wrap="False" />
                                            <Columns>
                                                <asp:BoundField DataField="PatientID" HeaderText="Patient Id" ReadOnly="true" SortExpression="PatientID" />
                                                <asp:BoundField DataField="PatientName" HeaderText="Patient Name" ReadOnly="True"
                                                    SortExpression="PatientName" />
                                                <asp:BoundField DataField="AdmitDate" HeaderText="Admit Date" ReadOnly="True" SortExpression="AdmitDate"
                                                    DataFormatString="{0:MM/dd/yyyy}" />
                                                <asp:BoundField DataField="BillDate" HeaderText="Discharge Date" ReadOnly="True"
                                                    SortExpression="BillDate" DataFormatString="{0:MM/dd/yyyy}" />
                                                <asp:BoundField DataField="ChargesName" HeaderText="Patient Type" ReadOnly="True"
                                                    SortExpression="ChargesName" />
                                                <asp:TemplateField HeaderText="Invoice" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ImgInvoice" runat="server" ImageUrl="~/images/Report.bmp" Height="24px"
                                                            Width="24px" OnClick="btnInvoicePrint_Click" /></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Show Test" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgTest" runat="server" ImageUrl="~/images/find_test.png" Height="24px"
                                                            Width="24px" OnClick="btnShowTest_Click" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:TemplateField HeaderText="Show Report" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgReport" runat="server" ImageUrl="~/Images/pdf-icon.png" Height="24px"
                                                            Width="24px" OnClick="btnShowReport_Click" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                    <asp:View runat="server" ID="View2">
                        <asp:Panel runat="server" ID="pnlGrid" Width="1010px">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td align="left">
                                        <asp:Button Text="Back" runat="server" Font-Names="Verdana" Font-Size="14px" BackColor="#3b3535"
                                            ForeColor="White" Width="80px" Style="border: 1px solid black" OnClick="btnBack_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Panel ID="Panel1" runat="server" Height="100%" Width="100%" align="center">
                                            <center>
                                                &nbsp;<asp:Label Text="Patient Test History" ID="Label1" ForeColor="#3b3535" Font-Names="Verdana"
                                                    Font-Size="16px" Font-Bold="true" runat="server" />
                                            </center>
                                            <asp:GridView runat="server" ID="dgvTest" CellPadding="4" ForeColor="#333333" GridLines="Both"
                                                Font-Names="Verdana" Width="845px" Font-Size="Small" AllowPaging="false" AutoGenerateColumns="false"
                                                DataKeyNames="LabId">
                                                <FooterStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" />
                                                <RowStyle BackColor="#E0F0E8" Font-Size="11px" Wrap="False" />
                                                <PagerStyle BackColor="#3b3535" ForeColor="White" HorizontalAlign="Left" Font-Size="11px" />
                                                <SelectedRowStyle BackColor="#006600" Font-Bold="True" ForeColor="#333333" />
                                                <HeaderStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" Font-Size="12px"
                                                    Wrap="False" />
                                                <EditRowStyle BackColor="#2461BF" />
                                                <AlternatingRowStyle BackColor="White" Wrap="False" />
                                                <Columns>
                                                    <asp:BoundField DataField="PatientId" HeaderText="Patient Id" ReadOnly="True" SortExpression="PatientId" />
                                                    <asp:BoundField DataField="PatientName" HeaderText="Patient Name" ReadOnly="True"
                                                        SortExpression="PatientName" />
                                                    <asp:BoundField DataField="TestName" HeaderText="Test Name" ReadOnly="True" SortExpression="TestName" />
                                                    <asp:BoundField DataField="TestDoneById" HeaderText="Test Done By" ReadOnly="True"
                                                        SortExpression="TestDoneById" />
                                                    <asp:BoundField DataField="TestDate" HeaderText="TestDate" ReadOnly="True" SortExpression="TestDate" />
                                                    <asp:BoundField DataField="FinalComment" HeaderText="Final Comment" ReadOnly="True"
                                                        SortExpression="FinalComment" />
                                                    <asp:TemplateField HeaderText="Print" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImagePrint" runat="server" ImageUrl="~/images/Report.bmp" Height="24px"
                                                                Width="24px" OnClick="btnPrint_Click" /></ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </asp:View>
                    <asp:View runat="server" ID="View3">
                        <asp:Panel runat="server" ID="pnlGrid1" Width="1010px">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td align="left">
                                        <asp:Button ID="Button1" Text="Back" runat="server" Font-Names="Verdana" Font-Size="14px"
                                            BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black"
                                            OnClick="btnBack1_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Panel ID="Panel3" runat="server" Height="100%" Width="100%" align="center">
                                            <center>
                                                &nbsp;<asp:Label Text="Patient Documents Report" ID="Label2" ForeColor="#3b3535"
                                                    Font-Names="Verdana" Font-Size="16px" Font-Bold="true" runat="server" />
                                            </center>
                                            <asp:GridView ID="dgvDocument" runat="server" CellPadding="4" ForeColor="#333333" GridLines="Both"
                                                Font-Names="Verdana" Width="100%" Font-Size="Small" AllowPaging="false" AutoGenerateColumns="false"
                                                OnRowCommand="dgvDocument_RowCommand">
                                                <FooterStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" />
                                                <RowStyle BackColor="#E0F0E8" Font-Size="11px" Wrap="False" />
                                                <PagerStyle BackColor="#3b3535" ForeColor="White" HorizontalAlign="Left" Font-Size="11px" />
                                                <SelectedRowStyle BackColor="#006600" Font-Bold="True" ForeColor="#333333" />
                                                <HeaderStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" Font-Size="12px"
                                                    Wrap="False" />
                                                <EditRowStyle BackColor="#2461BF" />
                                                <AlternatingRowStyle BackColor="White" Wrap="False" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sr. No." ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPKId" runat="server" Text='<%#Eval("PKId") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="UploadDate" HeaderText="Date" ReadOnly="True" SortExpression="UploadDate"
                                                        DataFormatString="{0:MM/dd/yyyy}" />
                                                    <asp:BoundField DataField="PatientName" HeaderText="Patient Name" ReadOnly="True"
                                                        SortExpression="PatientName" />
                                                    <asp:TemplateField HeaderText="Document Name" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDocumentName" runat="server" Text='<%#Eval("DocumentName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Document File" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="DownloadFile" runat="server" ImageUrl="~/Images/pdf-icon.png"
                                                                ImageAlign="Middle" ToolTip="Download File" CommandName="DownloadFile" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </asp:View>
                </asp:MultiView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
