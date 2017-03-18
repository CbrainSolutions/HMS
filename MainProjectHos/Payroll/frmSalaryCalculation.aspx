<%@ Page Title="" Language="C#" MasterPageFile="~/mstAdmin.Master" AutoEventWireup="true" CodeBehind="frmSalaryCalculation.aspx.cs" Inherits="MainProjectHos.Payroll.frmSalaryCalculation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function CheckList() {
            var dis = document.getElementById("<%=txtMonth.ClientID %>").value;
            var arr = dis.split('/');
            if (arr.length == 2) {
                var m = GetMonth(arr[0]);
                var dt = daysInMonth(m, arr[1]);
                document.getElementById("<%=txtDays.ClientID %>").value = dt;
            }
        }
        function GetMonth(Month) {
            switch (Month) {
                case "Jan":
                    return 1;
                    break;
                case "Feb":
                    return 2;
                    break;
                case "Mar":
                    return 3;
                    break;
                case "Apr":
                    return 4;
                    break;
                case "May":
                    return 5;
                    break;
                case "Jun":
                    return 6;
                    break;
                case "Jul":
                    return 7;
                    break;
                case "Aug":
                    return 8;
                    break;
                case "Sep":
                    return 9;
                    break;
                case "Oct":
                    return 10;
                    break;
                case "Nov":
                    return 11;
                    break;
                case "Dec":
                    return 12;
                    break;

            }
        }
        function daysInMonth(month, year) {
            return new Date(year, month, 0).getDate();
        }
        function Leave() {
            var day = document.getElementById("<%=txtDays.ClientID %>").value;
            var Leave = document.getElementById("<%=txtLeavesTaken.ClientID %>").value;
            if (day != null && Leave != null) {
                var total = day - Leave;
                document.getElementById("<%=txtAttendDays.ClientID %>").value = total;
            } return false;
        }
    </script>
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
                                    <asp:Label ID="Label1" runat="server" Text="Salary Calculation" Font-Names="Verdana"
                                        Font-Size="16px" Font-Bold="true" ForeColor="#3b3535"></asp:Label>
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
                                                    Text="Search" Width="80px" />
                                            </td>
                                            <td align="center" style="border-left: none; border-right: none; width: 60px;">
                                                <asp:Button ID="btnReset" runat="server" BackColor="#3b3535" Font-Names="Verdana"
                                                    Font-Size="13px" ForeColor="White" OnClick="btnReset_Click" Style="border: 1px solid black"
                                                    Text="Reset" Width="80px" />
                                            </td>
                                            <td align="center" style="border-left: none; width: 60px;">
                                                <asp:Button ID="BtnAddNewEmp" runat="server" Text="Add New" Font-Names="Verdana"
                                                    Font-Size="13px" OnClick="BtnAddNewEmp_Click" BackColor="#3b3535" ForeColor="White"
                                                    Width="80px" Style="border: 1px solid black" onmouseover="SetBtnMouseOver(this)"
                                                    onmouseout="SetBtnMouseOut(this)" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel ID="pnlShow" BorderColor="Maroon" BorderWidth="1px" runat="server" Style="border-color: Green;
                                        border-style: solid; border-width: 1px">
                                        <table width="60%">
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
                                            <tr style="width: 100%">
                                                <td align="center" style="width: 100%" colspan="3">
                                                    <asp:Label ID="lblMessage" runat="server" Text="" Font-Names="Verdana" Font-Size="11px"
                                                        ForeColor="Red"></asp:Label>
                                                    <asp:GridView ID="dgvSalary" runat="server" CellPadding="4" ForeColor="#333333" DataKeyNames="SalId"
                                                        GridLines="Both" Font-Names="Verdana" Width="1010px" Font-Size="Small" AllowPaging="true"
                                                        PageSize="15" AutoGenerateColumns="false" OnPageIndexChanged="dgvSalary_PageIndexChanged"
                                                        OnPageIndexChanging="dgvSalary_PageIndexChanging" OnDataBound="dgvSalary_DataBound">
                                                        <FooterStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" />
                                                        <RowStyle BackColor="#E0F0E8" Font-Size="11px" Wrap="False" />
                                                        <PagerStyle BackColor="#3b3535" ForeColor="White" HorizontalAlign="Left" Font-Size="11px" />
                                                        <SelectedRowStyle BackColor="#006600" Font-Bold="True" ForeColor="#333333" />
                                                        <HeaderStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" Font-Size="12px"
                                                            Wrap="False" />
                                                        <EditRowStyle BackColor="#2461BF" />
                                                        <AlternatingRowStyle BackColor="White" Wrap="False" />
                                                        <Columns>
                                                            <asp:BoundField DataField="EmpCode" HeaderText="EmpCode" ReadOnly="True" SortExpression="EmpCode" />
                                                            <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" ReadOnly="True"
                                                                SortExpression="EmployeeName" />
                                                            <asp:BoundField DataField="SalDate" HeaderText="Salary Date" ReadOnly="True" SortExpression="SalDate"
                                                                DataFormatString="{0:MM/dd/yyyy}" />
                                                            <asp:BoundField DataField="Sal_Month" HeaderText="Salary Month" ReadOnly="True" SortExpression="Sal_Month" />
                                                            <asp:BoundField DataField="No_of_Days" HeaderText="No of Days " ReadOnly="True" SortExpression="No_of_Days" />
                                                            <asp:BoundField DataField="Attend_Days" HeaderText="Attend Days " ReadOnly="True"
                                                                SortExpression="Attend_Days" />
                                                            <asp:BoundField DataField="LeavesTaken" HeaderText="Leaves Taken  " ReadOnly="True"
                                                                SortExpression="LeavesTaken" />
                                                            <asp:BoundField DataField="OTHours" HeaderText="OTHours  " ReadOnly="True" SortExpression="OTHours" />
                                                            <asp:BoundField DataField="NetPayment" HeaderText="NetPayment " ReadOnly="True" SortExpression="NetPayment" DataFormatString="{0:0.00}"/>
                                                            <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="ImageEdit" runat="server" ImageUrl="~/images/edit.jpg" Height="24px"
                                                                        Width="24px" OnClick="BtnEdit_Click" /></ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Print" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="ImagePrint" runat="server" ImageUrl="~/images/Report.bmp" Height="24px"
                                                                        Width="24px" OnClick="btnPrint_Click" /></ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                    <asp:View ID="View2" runat="server">
                        <table style="height: auto; width: 100%">
                            <tr>
                                <td width="100%">
                                    <asp:Panel ID="pnlGrid" runat="server" Width="100%" Style="text-align: center; background-color: #E0F0E8;
                                        height: 100%;">
                                        <table width="100%" style="border-color: Green; border-style: solid; border-width: 2px"
                                            cellpadding="0">
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="Panel2" runat="server" Width="100%" Style="text-align: center; background-color: #E0F0E8;
                                                        height: auto;" BorderColor="Green" BorderStyle="Solid" BorderWidth="1px">
                                                        <table width="100%">
                                                            <tr>
                                                                <td align="center" colspan="5">
                                                                    <asp:Label ID="lblHeading" runat="server" Text="Salary Calculation" Font-Names="Verdana"
                                                                        Font-Size="15px" Font-Bold="true" ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="5" class="style1">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="5" align="center">
                                                                    <asp:Label ID="lblMsg" runat="server" Text="" Font-Names="Verdana" Font-Size="11px"
                                                                        ForeColor="Red"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" style="width: 20%;">
                                                                    <asp:Label ID="lblEmpName" runat="server" Text="Employee Name:" Font-Names="Verdana"
                                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td align="left" style="width: 30%;">
                                                                    <asp:DropDownList ID="ddlEmployee" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                        ForeColor="#3b3535" Width="250px" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged"
                                                                        AutoPostBack="True">
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red"
                                                                        ControlToValidate="ddlEmployee" Font-Size="13" ValidationGroup="Save" ErrorMessage="*"
                                                                        Display="Dynamic" InitialValue="0"></asp:RequiredFieldValidator>
                                                                </td>
                                                                <td align="right" style="width: 15%;">
                                                                    <asp:Label ID="lblBasic" runat="server" Text="Basic Salary :" Font-Names="Verdana"
                                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td align="left" style="width: 35%;">
                                                                    <asp:Label ID="lblbaseSal" runat="server" Font-Names="Verdana" Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" style="width: 20%;">
                                                                    <asp:Label ID="lblDoj" runat="server" Text="Date Of Join:" Font-Names="Verdana" Font-Size="11px"
                                                                        ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td align="left" style="width: 30%;">
                                                                    <asp:TextBox ID="txtDoj" runat="server" Font-Names="Verdana" Font-Size="11px" Width="150px"
                                                                        MaxLength="10" Format="dd/MM/yyyy"></asp:TextBox>
                                                                    <cc:CalendarExtender ID="calDoj" runat="server" TargetControlID="txtDoj" Format="dd/MM/yyyy">
                                                                    </cc:CalendarExtender>
                                                                </td>
                                                                <td align="right" style="width: 15%;">
                                                                    <asp:Label ID="lblDos" runat="server" Text="Date Of Salary:" Font-Names="Verdana"
                                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td align="left" style="width: 35%;">
                                                                    <asp:TextBox ID="txtDos" runat="server" Font-Names="Verdana" Font-Size="11px" Width="150px"
                                                                        MaxLength="10"></asp:TextBox>
                                                                    <cc:CalendarExtender ID="calDos" runat="server" TargetControlID="txtDos" Format="MM/dd/yyyy">
                                                                    </cc:CalendarExtender>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtDos"
                                                                        Display="Dynamic" ErrorMessage="*" Font-Size="13" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                </td>
                                                                <td colspan="2">
                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtDos"
                                                                        Display="Dynamic" ErrorMessage="Please Enter Only Date With MM/DD/YYYY Format"
                                                                        Font-Bold="False" Font-Names="verdana" Font-Size="11px" ForeColor="Red" ValidationExpression="^([0]\d|[1][0-2])\/([0-2]\d|[3][0-1])\/([2][01]|[1][6-9])\d{2}(\s([0-1]\d|[2][0-3])(\:[0-5]\d){1,2})?$"
                                                                        ValidationGroup="Save">
                                                                    </asp:RegularExpressionValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" style="width: 20%;">
                                                                    <asp:Label ID="lblMonth" runat="server" Text="Salary Month:" Font-Names="Verdana"
                                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td align="left" style="width: 30%;">
                                                                    <asp:TextBox ID="txtMonth" runat="server" Font-Names="Verdana" Font-Size="11px" Width="150px"
                                                                        MaxLength="10" onblur="javascript:return CheckList();"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ForeColor="Red"
                                                                        ControlToValidate="txtMonth" Font-Size="13" ValidationGroup="Save" ErrorMessage="*"
                                                                        Display="Dynamic"></asp:RequiredFieldValidator>
                                                                    <cc:CalendarExtender ID="CalMonth" runat="server" TargetControlID="txtMonth" Format="MMM/yyyy">
                                                                    </cc:CalendarExtender>
                                                                </td>
                                                                <td align="right" style="width: 15%;">
                                                                    <asp:Label ID="lblDays" runat="server" Text="Days In Month:" Font-Names="Verdana"
                                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td align="left" style="width: 35%;">
                                                                    <asp:TextBox ID="txtDays" runat="server" Font-Names="Verdana" Font-Size="11px" ForeColor="#3b3535"
                                                                        Width="150px"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ForeColor="Red"
                                                                        ControlToValidate="txtDays" Font-Size="13" ValidationGroup="Save" ErrorMessage="*"
                                                                        Display="Dynamic"></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                </td>
                                                                <td colspan="2">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" style="width: 20%;">
                                                                    <asp:Label ID="lblAttendDays" runat="server" Text="Attend Days:" Font-Names="Verdana"
                                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td align="left" style="width: 30%;">
                                                                    <asp:TextBox ID="txtAttendDays" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                        ForeColor="#3b3535" Width="150px"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ForeColor="Red"
                                                                        ControlToValidate="txtAttendDays" Font-Size="13" ValidationGroup="Save" ErrorMessage="*"
                                                                        Display="Dynamic"></asp:RequiredFieldValidator>
                                                                </td>
                                                                <td align="right" style="width: 15%;">
                                                                    <asp:Label ID="lblLeaves" runat="server" Text="Leaves Taken:" Font-Names="Verdana"
                                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td align="left" style="width: 35%;">
                                                                    <asp:TextBox ID="txtLeavesTaken" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                        ForeColor="#3b3535" Width="150px" onblur="javascript:return Leave();"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ForeColor="Red"
                                                                        ControlToValidate="txtLeavesTaken" Font-Size="13" ValidationGroup="Save" ErrorMessage="*"
                                                                        Display="Dynamic"></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                </td>
                                                                <td colspan="2">
                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtLeavesTaken"
                                                                        Display="Dynamic" ErrorMessage="Please Enter Valid No of Leaves" Font-Bold="False"
                                                                        Font-Names="verdana" Font-Size="11px" ForeColor="Red" ValidationExpression="[0-3][0-1]?"
                                                                        ValidationGroup="Save">
                                                                    </asp:RegularExpressionValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" style="width: 20%;">
                                                                    <asp:Label ID="lblOtHours" runat="server" Text="OTHours:" Font-Names="Verdana" Font-Size="11px"
                                                                        ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td align="left" style="width: 30%;">
                                                                    <asp:TextBox ID="txtOTHours" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                        ForeColor="#3b3535" Width="150px"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ForeColor="Red"
                                                                        ControlToValidate="txtOTHours" Font-Size="13" ValidationGroup="Save" ErrorMessage="*"
                                                                        Display="Dynamic"></asp:RequiredFieldValidator>
                                                                </td>
                                                                <td style="width: 15%;">
                                                                </td>
                                                                <td align="left" style="width: 30%;">
                                                                    <asp:CheckBox ID="chkIspayment" runat="server" Text="IsPaymentDone" Font-Names="Verdana"
                                                                        Font-Size="11px" ForeColor="#3b3535" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtOTHours"
                                                                        Display="Dynamic" ErrorMessage="Please Enter Valid No of OT Hours" Font-Bold="False"
                                                                        Font-Names="verdana" Font-Size="11px" ForeColor="Red" ValidationExpression="[0-9][0-9]?"
                                                                        ValidationGroup="Save">
                                                                    </asp:RegularExpressionValidator>
                                                                </td>
                                                                <td colspan="2">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" style="width: 100%; padding-left: 26px;" align="left">
                                                                    <table style="border: 1px Solid Black" width="95%">
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lblallowance" runat="server" Text="Allowance:" Font-Names="Verdana"
                                                                                    Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:DropDownList ID="ddlAllowance" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                                    ForeColor="#3b3535" Width="150px" OnSelectedIndexChanged="ddlAllowance_SelectedIndexChanged"
                                                                                    AutoPostBack="True">
                                                                                </asp:DropDownList>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ForeColor="Red"
                                                                                    ControlToValidate="ddlAllowance" Font-Size="13" ValidationGroup="AddAllow" ErrorMessage="*"
                                                                                    Display="Dynamic" InitialValue="0"></asp:RequiredFieldValidator>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblAmount" runat="server" Text="Amount:" Font-Names="Verdana" Font-Size="11px"
                                                                                    ForeColor="#3b3535"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtAllowAmount" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                                    ForeColor="#3b3535" Width="100px"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ForeColor="Red"
                                                                                    ControlToValidate="txtAllowAmount" Font-Size="13" ValidationGroup="AddAllow"
                                                                                    ErrorMessage="*" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="2">
                                                                            </td>
                                                                            <td colspan="2">
                                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" ValidationExpression="^[0-9]{1,11}(?:\.[0-9]{1,3})?$"
                                                                                    ErrorMessage="Please Enter Only Number" Font-Bold="False" Font-Size="11px" ForeColor="Red"
                                                                                    ControlToValidate="txtAllowAmount" runat="server" ValidationGroup="AddAllow"
                                                                                    Display="Dynamic" Font-Names="verdana" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="5" align="center">
                                                                                <asp:Button ID="btnAllowAdd" runat="server" Text="Add" Font-Names="Verdana" Font-Size="12px"
                                                                                    ValidationGroup="AddAllow" BackColor="#3b3535" ForeColor="White" Width="80px"
                                                                                    Style="border: 1px solid black" OnClick="btnAllowAdd_Click" />
                                                                                <asp:Button ID="btnAllowCancel" runat="server" Text="Cancel" Font-Names="Verdana"
                                                                                    Font-Size="12px" ValidationGroup="Save" BackColor="#3b3535" ForeColor="White"
                                                                                    Width="80px" Style="border: 1px solid black" OnClick="btnAllowCancel_Click" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td colspan="2" style="width: 100%">
                                                                    <table style="border: 1px Solid Black" width="90%">
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lblDeduction" runat="server" Text="Deduction:" Font-Names="Verdana"
                                                                                    Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:DropDownList ID="ddlDeduction" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                                    ForeColor="#3b3535" Width="150px" OnSelectedIndexChanged="ddlDeduction_SelectedIndexChanged"
                                                                                    AutoPostBack="True">
                                                                                </asp:DropDownList>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ForeColor="Red"
                                                                                    ControlToValidate="ddlDeduction" Font-Size="13" ValidationGroup="AddDed" ErrorMessage="*"
                                                                                    Display="Dynamic" InitialValue="0"></asp:RequiredFieldValidator>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbldedamount" runat="server" Text="Amount:" Font-Names="Verdana" Font-Size="11px"
                                                                                    ForeColor="#3b3535"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtDedAmount" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                                    ForeColor="#3b3535" Width="100px"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ForeColor="Red"
                                                                                    ControlToValidate="txtDedAmount" Font-Size="13" ValidationGroup="AddDed" ErrorMessage="*"
                                                                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                                                            </td>
                                                                            <td>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="2">
                                                                            </td>
                                                                            <td colspan="2">
                                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationExpression="^[0-9]{1,11}(?:\.[0-9]{1,3})?$"
                                                                                    ErrorMessage="Please Enter Only Number" Font-Bold="False" Font-Size="11px" ForeColor="Red"
                                                                                    ControlToValidate="txtDedAmount" runat="server" ValidationGroup="AddDed" Display="Dynamic"
                                                                                    Font-Names="verdana" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="5">
                                                                                <asp:Button ID="btnDedAdd" runat="server" Text="Add" Font-Names="Verdana" Font-Size="12px"
                                                                                    ValidationGroup="AddDed" BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black"
                                                                                    OnClick="btnDedAdd_Click" />
                                                                                <asp:Button ID="btnDedCancel" runat="server" Text="Cancel" Font-Names="Verdana" Font-Size="12px"
                                                                                    ValidationGroup="Save" BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black"
                                                                                    OnClick="btnDedCancel_Click" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="5">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" style="width: 100%" align="center">
                                                                    <asp:Panel ID="Panel1" ScrollBars="Both" BorderColor="Maroon" Width="90%" BorderWidth="1px"
                                                                        runat="server" Style="border-color: Green; border-style: solid; border-width: 1px">
                                                                        <asp:GridView ID="DgvAllowance" runat="server" CellPadding="2" ForeColor="#333333"
                                                                            GridLines="Both" Font-Names="Verdana" Width="100%" Font-Size="Small" AllowPaging="true"
                                                                            PageSize="4" AutoGenerateColumns="false">
                                                                            <FooterStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" />
                                                                            <RowStyle BackColor="#E0F0E8" Font-Size="11px" Wrap="False" />
                                                                            <PagerStyle BackColor="#3b3535" ForeColor="White" HorizontalAlign="Left" Font-Size="11px" />
                                                                            <SelectedRowStyle BackColor="#006600" Font-Bold="True" ForeColor="#333333" />
                                                                            <HeaderStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" Font-Size="12px"
                                                                                Wrap="False" />
                                                                            <EditRowStyle BackColor="#2461BF" />
                                                                            <AlternatingRowStyle BackColor="White" Wrap="False" />
                                                                            <Columns>
                                                                                <asp:BoundField DataField="SalDetail_Id" HeaderText="SalDetail Id" Visible="true"
                                                                                    ReadOnly="True" SortExpression="SalDetail_Id" />
                                                                                <asp:BoundField DataField="AllowDedId" HeaderText="AllowDedId" Visible="true" ReadOnly="True"
                                                                                    SortExpression="AllowDedId" />
                                                                                <asp:BoundField DataField="Description" HeaderText="Allowance" ReadOnly="True" SortExpression="EmpCode" />
                                                                                <asp:BoundField DataField="Amount" HeaderText="Amount" ReadOnly="True" SortExpression="Amount"
                                                                                    DataFormatString="{0:0.00}" />
                                                                                <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Left">
                                                                                    <ItemTemplate>
                                                                                        <asp:ImageButton ID="ImageDelete" runat="server" ImageUrl="~/images/Erase.png" Height="24px"
                                                                                            Width="24px" OnClick="btnDelete_Click" /></ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </asp:Panel>
                                                                </td>
                                                                <td colspan="2" style="width: 100%" align="left">
                                                                    <asp:Panel ID="Panel3" ScrollBars="Both" BorderColor="Maroon" Width="90%" BorderWidth="1px"
                                                                        runat="server" Style="border-color: Green; border-style: solid; border-width: 1px">
                                                                        <asp:GridView ID="dgvDeduction" runat="server" CellPadding="2" ForeColor="#333333"
                                                                            GridLines="Both" Font-Names="Verdana" Width="100%" Font-Size="Small" AllowPaging="true"
                                                                            PageSize="4" AutoGenerateColumns="false">
                                                                            <FooterStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" />
                                                                            <RowStyle BackColor="#E0F0E8" Font-Size="11px" Wrap="False" />
                                                                            <PagerStyle BackColor="#3b3535" ForeColor="White" HorizontalAlign="Left" Font-Size="11px" />
                                                                            <SelectedRowStyle BackColor="#006600" Font-Bold="True" ForeColor="#333333" />
                                                                            <HeaderStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" Font-Size="12px"
                                                                                Wrap="False" />
                                                                            <EditRowStyle BackColor="#2461BF" />
                                                                            <AlternatingRowStyle BackColor="White" Wrap="False" />
                                                                            <Columns>
                                                                                <asp:BoundField DataField="SalDetail_Id" HeaderText="SalDetail Id" Visible="true"
                                                                                    ReadOnly="True" SortExpression="SalDetail_Id" />
                                                                                <asp:BoundField DataField="AllowDedId" HeaderText="AllowDedId" Visible="true" ReadOnly="True"
                                                                                    SortExpression="AllowDedId" />
                                                                                <asp:BoundField DataField="Description" HeaderText="Deduction" ReadOnly="True" SortExpression="EmpCode" />
                                                                                <asp:BoundField DataField="Amount" HeaderText="Amount" ReadOnly="True" SortExpression="Amount" DataFormatString="{0:0.00}" />
                                                                                <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Left">
                                                                                    <ItemTemplate>
                                                                                        <asp:ImageButton ID="ImageDelete" runat="server" ImageUrl="~/images/Erase.png" Height="24px"
                                                                                            Width="24px" OnClick="btnDedDelete_Click" /></ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="5">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="Label2" runat="server" Text="Amount:" Font-Names="Verdana" Font-Size="11px"
                                                                        ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtNetAmount" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                        ForeColor="#3b3535" Width="200px" ReadOnly="true"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="5">
                                                                </td>
                                                            </tr>
                                                            <tr align="center">
                                                                <td align="center" colspan="5">
                                                                    <asp:Button ID="BtnSave" runat="server" Text="Save" Font-Names="Verdana" Font-Size="12px"
                                                                        ValidationGroup="Save" BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black;
                                                                        height: 20px;" OnClick="BtnSave_Click" />
                                                                    <asp:Button ID="btnUpdate" runat="server" Text="Update" Font-Names="Verdana" Font-Size="12px"
                                                                        ValidationGroup="Update" BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black"
                                                                        OnClick="btnUpdate_Click" />
                                                                    <asp:Button ID="BtnClose" runat="server" Text="Cancel" Font-Names="Verdana" Font-Size="12px"
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
    <input id="hdnPanel" type="hidden" runat="server" value="none" />
</asp:Content>
