<%@ Page Title="" Language="C#" MasterPageFile="~/mstAdmin.Master" AutoEventWireup="true" CodeBehind="frmTestAllocation.aspx.cs" Inherits="MainProjectHos.frmTestAllocation" %>
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
    <script type="text/javascript" language="javascript">
        function CalculateSum() {
            var gridtest = document.getElementById("<%=dgvAllTests.ClientID %>");
            var sum = 0;
            if (gridtest.rows.length > 0) {
                for (var i = 1; i < gridtest.rows.length; i++) {
                    var idappend = i - 1;
                    var id = 'ContentPlaceHolder1_dgvAllTests_chkSelect_' + idappend;
                    var chkbox = document.getElementById(id);
                    if (chkbox != null) {
                        if (chkbox.checked == true) {
                            sum = sum + parseFloat(gridtest.rows[i].cells[2].innerHTML);
                        }
                    }
                }
                document.getElementById("<%=txtTotal.ClientID %>").value = sum;
                CalDiscount();
            }
        }
        function CalDiscount() {
            var dis = document.getElementById("<%=txtDiscount.ClientID %>").value;
            var total = document.getElementById("<%=txtTotal.ClientID %>").value;
            if (dis != null && total != null) {
                var disamt = total * dis / 100;
                document.getElementById("<%=txtDiscountAmt.ClientID %>").value = disamt
                document.getElementById("<%=txtNetAmount.ClientID %>").value = total - disamt;
            }
            else {
                if (total == null) {
                    alert("Please select test first");
                    return false;
                }
                if (dis == null) {
                    alert("Please fill discount first. ");
                    return false;
                }
                if (total == 0) {
                    document.getElementById("<%=txtDiscountAmt.ClientID %>").value = "";
                    document.getElementById("<%=txtNetAmount.ClientID %>").value = "";
                }
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
                        <table width="1040px" align="center">
                            <tr>
                                <td align="center">
                                    <asp:Label ID="Label1" runat="server" Text="Test Invoice" Font-Names="Verdana" Font-Size="16px"
                                        Font-Bold="true" ForeColor="#3b3535"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel ID="pnlShow" BorderColor="Maroon" BorderWidth="1px" runat="server" Style="border-color: Green;
                                        border-style: solid; border-width: 1px">
                                        <table width="100%">
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="lblShift" runat="server" Text="Patient Name :" Font-Names="Verdana"
                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:DropDownList ID="ddlPatient" runat="server" Style="width: 150px;" 
                                                    OnSelectedIndexChanged="ddlPatientName_SelectedIndexChanged" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red"
                                                        ControlToValidate="ddlPatient" Font-Size="13" ValidationGroup="Save" ErrorMessage="*"
                                                        Display="Dynamic" InitialValue="0"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label8" runat="server" Text="Is Cash :" Font-Names="Verdana" Font-Size="11px"
                                                        ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:CheckBox ID="chkIsCash" runat="server" Checked="false" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label7" runat="server" Text="Test Date :" Font-Names="Verdana" Font-Size="11px"
                                                        ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtAllocDate" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                        Width="150px" MaxLength="10" Format="MM/dd/yyyy hh:mm:ss tt"></asp:TextBox>
                                                    <cc:CalendarExtender ID="CalDOBDate" runat="server" TargetControlID="txtAllocDate"
                                                        Format="MM/dd/yyyy" DaysModeTitleFormat="MM/dd/yyyy" TodaysDateFormat="MM/dd/yyyy hh:mm:ss tt">
                                                    </cc:CalendarExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="2">
                                                    <asp:RegularExpressionValidator ID="revTxtFirstName" ValidationExpression="^([0]\d|[1][0-2])\/([0-2]\d|[3][0-1])\/([2][01]|[1][6-9])\d{2}(\s([0-1]\d|[2][0-3])(\:[0-5]\d){1,2})?$"
                                                        ErrorMessage="Please Enter Only Date With MM/DD/YYYY Format" Font-Bold="False"
                                                        Font-Size="11px" ForeColor="Red" ControlToValidate="txtAllocDate" runat="server"
                                                        ValidationGroup="Save" Display="Dynamic" Font-Names="Verdana" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label3" runat="server" Text="Total Amount :" Font-Names="Verdana"
                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtTotal" runat="server" Enabled="False"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label4" runat="server" Text="Discount % :" Font-Names="Verdana" Font-Size="11px"
                                                        ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtDiscount" runat="server" onblur="javascript:return CalDiscount()"
                                                        MaxLength="2"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ForeColor="Red"
                                                        ControlToValidate="txtDiscount" Font-Size="13" ValidationGroup="Save" ErrorMessage="*"
                                                        Display="Dynamic"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label5" runat="server" Text="Discount Amount :" Font-Names="Verdana"
                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtDiscountAmt" runat="server" Enabled="False"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label6" runat="server" Text="Net Amount :" Font-Names="Verdana" Font-Size="11px"
                                                        ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtNetAmount" runat="server" Enabled="False"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr style="height: 7px;">
                                                <td colspan="3">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100%" colspan="3" align="center">
                                                    <asp:Label ID="lblMessage" runat="server" Text="" Font-Names="Verdana" Font-Size="11px"
                                                        ForeColor="Red"></asp:Label>
                                                    <div style="float: left; width: 100%;">
                                                        <asp:Panel ID="pnl" ScrollBars="Horizontal" runat="server" Width="400px" BorderColor="Green"
                                                            BorderStyle="Solid" BorderWidth="1px">
                                                            <asp:GridView ID="dgvAllTests" runat="server" CellPadding="4" ForeColor="#333333"
                                                                DataKeyNames="TestId" GridLines="Both" Font-Names="Verdana" Width="400px" Font-Size="Small"
                                                                AutoGenerateColumns="false" Style="font-family: System" OnRowDataBound="dgvAllTests_RowDataBound">
                                                                <FooterStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" />
                                                                <RowStyle BackColor="#E0F0E8" Font-Size="11px" Wrap="False" Font-Names="Verdana" />
                                                                <PagerStyle BackColor="#3b3535" ForeColor="White" HorizontalAlign="Left" Font-Size="11px" />
                                                                <SelectedRowStyle BackColor="#006600" Font-Bold="True" ForeColor="#333333" />
                                                                <HeaderStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" Font-Size="12px"
                                                                    Wrap="False" Font-Names="verdana" />
                                                                <EditRowStyle BackColor="#2461BF" />
                                                                <AlternatingRowStyle BackColor="White" Wrap="False" />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Select" ItemStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chkSelect" Text="" runat="server" /></ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="TestName" HeaderText="Test Title" ReadOnly="True" SortExpression="TestName" />
                                                                    <asp:BoundField DataField="TestCharge" HeaderText="Charges" ReadOnly="True" SortExpression="TestCharge" />
                                                                </Columns>
                                                            </asp:GridView>
                                                        </asp:Panel>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td align="center" colspan="3">
                                                    <asp:Button ID="BtnSave" runat="server" Text="Save" Font-Names="Verdana" Font-Size="12px"
                                                        ValidationGroup="Save" BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black"
                                                        OnClick="BtnSave_Click" />
                                                    <asp:Button ID="BtnUpdate" runat="server" BackColor="#3b3535" Font-Names="Verdana"
                                                        Font-Size="12px" ForeColor="White" OnClick="BtnEdit_Click" Style="border: 1px solid black"
                                                        Text="Update" ValidationGroup="Save" Width="80px" />
                                                    <asp:Button ID="btnClose" runat="server" BackColor="#3b3535" Font-Names="Verdana"
                                                        Font-Size="12px" ForeColor="White" OnClick="BtnClose_Click" Style="border: 1px solid black"
                                                        Text="Close" Width="80px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                    <asp:View runat="server" ID="View2">
                        <div style="width: 100%; height: auto">
                            <table style="width: 1040px;">
                                <tr>
                                    <td align="center" colspan="4">
                                        <asp:Label Text="Test Invoice" ID="lblHeaderInvoice" Font-Names="Verdana" Font-Size="16px"
                                            Font-Bold="true" ForeColor="#3b3535" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="4">
                                        <table border="1px" cellpadding="0" cellspacing="0" width="860px" style="height: 40px;">
                                            <tr>
                                                <td align="center" style="border-right: none; width: 575px;">
                                                    <asp:TextBox ID="txtSearch" runat="server" Width="550px"></asp:TextBox>
                                                    <cc:TextBoxWatermarkExtender ID="txtSearchWatermark" runat="server" TargetControlID="txtSearch"
                                                        WatermarkText="Patient Name, Address" WatermarkCssClass="watermarked" />
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
                                                <td style="border-left: none; width: 60px; padding-right: 4px;">
                                                    <asp:Button ID="btnAddNew" runat="server" BackColor="#3b3535" Font-Names="Verdana"
                                                        Font-Size="13px" ForeColor="White" OnClick="btnAddNew_Click" Style="border: 1px solid black"
                                                        Text="New" Width="80px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="left">
                                        <asp:Label ID="lblPageCount" runat="server" Text="" Font-Names="Verdana" Font-Size="11px"
                                            ForeColor="#3b3535"></asp:Label>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="lblRowCount1" runat="server" Font-Names="Verdana" Font-Size="11px"
                                            ForeColor="#3b3535" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" align="center">
                                        <asp:Label ID="lblMsg" ForeColor="Red" Font-Names="verdana" Font-Size="11px" Text=""
                                            runat="server" />
                                    </td>
                                </tr>
                            </table>
                            <asp:GridView ID="dgvTestInvoice" runat="server" CellPadding="4" ForeColor="#333333"
                                DataKeyNames="TestInvoiceNo" GridLines="Both" Font-Names="Verdana" Width="100%"
                                Font-Size="Small" AllowPaging="true" PageSize="20" AutoGenerateColumns="false"
                                OnPageIndexChanged="dgvTestInvoice_PageIndexChanged" OnPageIndexChanging="dgvTestInvoice_PageIndexChanging"
                                Style="font-family: System">
                                <FooterStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" />
                                <RowStyle BackColor="#E0F0E8" Font-Size="11px" Wrap="False" Font-Names="Verdana" />
                                <PagerStyle BackColor="#3b3535" ForeColor="White" HorizontalAlign="Left" Font-Size="11px" />
                                <SelectedRowStyle BackColor="#006600" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" Font-Size="12px"
                                    Wrap="False" Font-Names="verdana" />
                                <EditRowStyle BackColor="#2461BF" />
                                <AlternatingRowStyle BackColor="White" Wrap="False" />
                                <Columns>
                                    <asp:BoundField DataField="TestInvoiceNo" HeaderText="Bill No." ReadOnly="True" SortExpression="TestInvoiceNo" />
                                    <asp:BoundField DataField="TestInvoiceDate" HeaderText="Date" ReadOnly="True" SortExpression="TestInvoiceDate"
                                        DataFormatString="{0:MM/dd/yyyy}" />
                                    <asp:BoundField DataField="PatientName" HeaderText="Patient Time" ReadOnly="True"
                                        SortExpression="PatientName" />
                                    <asp:BoundField DataField="Address" HeaderText="Address" ReadOnly="True" SortExpression="Address" />
                                    <asp:BoundField DataField="Amount" HeaderText="Amount" ReadOnly="True" SortExpression="Amount" DataFormatString="{0:0.00}" />
                                    <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageEdit" runat="server" ImageUrl="~/images/edit.jpg" Height="24px"
                                                Width="24px" OnClick="btnUpdate_Click" />
                                        </ItemTemplate>
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
    <input id="hdnPanel" type="hidden" runat="server" value="none" />
</asp:Content>
