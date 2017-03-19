<%@ Page Title="" Language="C#" MasterPageFile="~/mstAdmin.Master" AutoEventWireup="true" CodeBehind="frmStockDetailReport.aspx.cs" Inherits="MainProjectHos.PathalogyReport.frmStockDetailReport" %>
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
                <table align="center" width="100%">
                    <tr style="height: 15px;">
                        <td>
                            <table border="1px" cellpadding="0" cellspacing="0" style="height: 40px;" width="610px"
                                align="center">
                                <tr>
                                    <td style="border-right: none; width: 105px;" align="center">
                                        <asp:Label ID="lblPatientName" runat="server" ForeColor="#3b3535" Text="Product Name : "
                                            Font-Names="Verdana" Font-Size="11px" Font-Bold="True" />
                                    </td>
                                    <td style="border-left: none; border-right: none; width: 140px;">
                                        <asp:DropDownList ID="ddlProduct" runat="server" Width="150px">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlProduct"
                                            Display="Dynamic" ErrorMessage="*" Font-Size="13" ForeColor="Red" InitialValue="0"
                                            ValidationGroup="Save"></asp:RequiredFieldValidator>
                                    </td>
                                    <td style="border-left: none; border-right: none; width: 60px;">
                                        <asp:Button ID="btnSearch" Text="Search" Font-Names="Verdana" Font-Size="14px" runat="server"
                                            Width="80px" Style="border: 1px solid black" OnClick="btnSearch_Click" BackColor="#3b3535"
                                            ForeColor="White" />
                                    </td>
                                    <td align="center" style="border-left: none; border-right: none; width: 60px;">
                                        <asp:Button ID="btnReset" Text="Reset" Font-Names="Verdana" Font-Size="14px" runat="server"
                                            Width="80px" Style="border: 1px solid black" OnClick="btnReset_Click" BackColor="#3b3535"
                                            ForeColor="White" />
                                    </td>
                                    <td style="width: 60px; border-left: none; border-right: none;">
                                        <asp:Button ID="btnPrint" Text="Print" Font-Names="Verdana" Font-Size="14px" runat="server"
                                            Width="80px" Style="border: 1px solid black;" BackColor="#3b3535" ForeColor="White"
                                            OnClientClick="javascript:return PrintPanel()" />
                                    </td>
                                    <td align="center" style="border-left: none; width: 60px;">
                                        <asp:Button ID="btnExcel" runat="server" Text="Excel" Font-Names="Verdana" Font-Size="14px"
                                            BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black;"
                                            onmouseover="SetBtnMouseOver(this)" onmouseout="SetBtnMouseOut(this)" OnClick="btnExcel_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <asp:Label ID="lblMessage" runat="server" Text="" Font-Names="Verdana" Font-Size="11px"
                    ForeColor="Red"></asp:Label>
                <asp:Panel ID="pnlContents" runat="server" Width="100%" align="center">
                    <table width="100%" align="center">
                        <tr style="width: 100%;" align="center">
                            <td style="width: 100%;" align="center">
                                <table width="60%" align="center">
                                    <tr style="width: 100%;" align="center">
                                        <td colspan="3" style="width: 100%;" align="center">
                                            <asp:Label ID="lbl" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="width: 100%;" align="center">
                                        <td align="left" style="width: 30%;">
                                            <asp:Label ID="lblFrom" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td align="center" style="width: 40%;">
                                            <asp:Label ID="lblUoM" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td align="right" style="width: 30%;">
                                            <asp:Label ID="lblTo" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table width="90%" align="center">
                        <tr>
                            <td>
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
                                            <asp:BoundField DataField="DocumentNo" HeaderText="Bill No." ReadOnly="True" SortExpression="DocumentNo" />
                                            <asp:BoundField DataField="TransactionType" HeaderText="Doc Type" ReadOnly="True"
                                                SortExpression="TransactionType" />
                                            <asp:BoundField DataField="InwardQty" HeaderText="Inward Qauntity" ReadOnly="True"
                                                SortExpression="InwardQty" />
                                            <asp:BoundField DataField="InwardPrice" HeaderText="Inward Price" ReadOnly="True"
                                                SortExpression="InwardPrice" />
                                            <asp:BoundField DataField="InwardAmount" HeaderText="Inward Amount" ReadOnly="True"
                                                SortExpression="InwardAmount" />
                                            <asp:BoundField DataField="OutwardQty" HeaderText="Outward Qauntity" ReadOnly="True"
                                                SortExpression="OutwardQty" />
                                            <asp:BoundField DataField="OutwardPrice" HeaderText="Outward Price" ReadOnly="True"
                                                SortExpression="OutwardPrice" />
                                            <asp:BoundField DataField="OutwardAmount" HeaderText="Outward Amount" ReadOnly="True"
                                                SortExpression="OutwardAmount" />
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

