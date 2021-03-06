﻿<%@ Page Title="" Language="C#" MasterPageFile="~/mstAdmin.Master" AutoEventWireup="true" CodeBehind="frmIPDPatientReport.aspx.cs" Inherits="MainProjectHos.PathalogyReport.frmIPDPatientReport" %>
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
            var printWindow = window.open('', '', 'height=400,width=980');
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
                                            Width="110px" MaxLength="10" Format="MM/dd/yyyy hh:mm:ss tt"></asp:TextBox>
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
                                        <asp:Calendar ID="CalBillDate" runat="server" TargetControlID="txtBillDate"
                                            Format="MM/dd/yyyy" DaysModeTitleFormat="MM/dd/yyyy" TodaysDateFormat="MM/dd/yyyy">
                                        </asp:Calendar>
                                    </td>
                                    <td colspan="4">
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
                                    <td colspan="5">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="padding-left: 5px;">
                                        <asp:Label ID="Label12" runat="server" Text="Dept. Category :" Font-Names="Verdana"
                                            Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlDeptCategory" runat="server" Font-Names="Verdana" Font-Size="11px"
                                            Width="140px" AutoPostBack="true" OnSelectedIndexChanged="ddlDeptCategory_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="Label20" runat="server" Text="Incharge Doctor :" Font-Names="Verdana"
                                            Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlDeptDoctor" runat="server" Font-Names="Verdana" Font-Size="11px"
                                            Width="140px">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="Label4" runat="server" Text="Patient Type:" Font-Names="Verdana" Font-Size="11px"
                                            ForeColor="#3b3535"></asp:Label>
                                        <asp:DropDownList ID="ddlPatientType" runat="server" Font-Names="Verdana" Font-Size="11px">
                                            <asp:ListItem Text="Select" Value="0" Selected="True">--Select--</asp:ListItem>
                                            <asp:ListItem Text="OPD" Value="OPD">OPD</asp:ListItem>
                                            <asp:ListItem Text="IPD" Value="IPD">IPD</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:Label ID="Label1" runat="server" Text="Ward Category :" Font-Names="Verdana"
                                            Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                        <asp:DropDownList ID="ddlWardCategory" runat="server" Font-Names="Verdana" Font-Size="11px"
                                            Width="120px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="5">
                                        <asp:Button ID="btnSearch" Text="Search" Font-Names="Verdana" Font-Size="14px" runat="server"
                                            Width="80px" Style="border: 1px solid black" OnClick="btnSearch_Click" BackColor="#3b3535"
                                            ForeColor="White" />
                                        <asp:Button ID="btnPrint" Text="Print" Font-Names="Verdana" Font-Size="14px" runat="server"
                                            Width="80px" Style="border: 1px solid black;" BackColor="#3b3535" ForeColor="White"
                                            OnClientClick="javascript:return PrintPanel()" />
                                        <asp:Button ID="btnExcel" runat="server" Text="Excel" Font-Names="Verdana" Font-Size="14px"
                                            BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black;"
                                            onmouseover="SetBtnMouseOver(this)" onmouseout="SetBtnMouseOut(this)" OnClick="btnExcel_Click" />
                                        <asp:Button ID="btnReset" runat="server" BackColor="#3b3535" Font-Names="Verdana"
                                            Font-Size="13px" ForeColor="White" OnClick="btnReset_Click" Style="border: 1px solid black"
                                            Text="Reset" Width="80px" />
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
                            AutoGenerateColumns="false">
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
                                <asp:BoundField DataField="AdmitDate" HeaderText="Admit Date" ReadOnly="True" SortExpression="AdmitDate"
                                    DataFormatString="{0:MM/dd/yyyy}" />
                                <asp:BoundField DataField="PatientCode" HeaderText="MRN NO" ReadOnly="True" SortExpression="PatientCode" />
                                <asp:BoundField DataField="PatientName" HeaderText="PatientName" ReadOnly="True"
                                    SortExpression="PatientName" />
                                <asp:BoundField DataField="PatientType" HeaderText="PatientType" ReadOnly="True"
                                    SortExpression="PatientType" />
                                <asp:BoundField DataField="Dignosys" HeaderText="Dignosys" ReadOnly="True" SortExpression="Dignosys" />
                                <asp:BoundField DataField="CategoryName" HeaderText="Dept.Name" ReadOnly="True" SortExpression="CategoryName" />
                                <asp:BoundField DataField="DeptDoctor" HeaderText="Consult Doctor" ReadOnly="True"
                                    SortExpression="DeptDoctor" />
                                <asp:BoundField DataField="Status" HeaderText="Status" ReadOnly="True" SortExpression="Status" />
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
