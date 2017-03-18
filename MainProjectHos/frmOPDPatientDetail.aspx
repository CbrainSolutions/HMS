<%@ Page Title="" Language="C#" MasterPageFile="~/mstAdmin.Master" AutoEventWireup="true" CodeBehind="frmOPDPatientDetail.aspx.cs" Inherits="MainProjectHos.frmOPDPatientDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/content.css" rel="stylesheet" type="text/css" />
    <script src="Notimoo/mootools-1.2-core.js" type="text/javascript"></script>
    <link href="Notimoo/notimoo-documented-1.1.css" rel="stylesheet" type="text/css" />
    <script src="Notimoo/notimoo-documented-1.1.js" type="text/javascript"></script>
    <link href="Notimoo/notimoo-v1.1.css" rel="stylesheet" type="text/css" />
    <script src="Notimoo/notimoo-v1.1.js" type="text/javascript"></script>
    <link href="Style/AdminStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function show_alert(msg) {
            var show1 = new Notimoo({ locationVType: 'bottom', locationHType: 'right' });
            show1.show({ message: msg, sticky: false, visibleTime: 5000, width: 200 });
        }

        function Refresh() {
            window.opener.location.reload();

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
                                    <asp:Label ID="Label1" runat="server" Text="Patient List" Font-Names="Verdana" Font-Size="16px"
                                        Font-Bold="true" ForeColor="#3b3535"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="width: 15%;">
                                    <table border="2px" cellpadding="0" cellspacing="0" width="860px" style="height: 40px;">
                                        <tr>
                                            <td align="center" style="border-right: none; width: 575px;">
                                                <asp:TextBox ID="txtSearch" Font-Bold="true" Font-Names="Verdana" Font-Size="13px"
                                                    Width="580px" runat="server" />
                                            </td>
                                            <td align="center" style="border-left: none; border-right: none; width: 60px;">
                                                <asp:Button ID="btnSearch" Text="Search" Font-Names="Verdana" Font-Size="14px" runat="server"
                                                    Width="80px" Style="border: 1px solid black" OnClick="btnSearch_Click" BackColor="#3b3535"
                                                    ForeColor="White" />
                                            </td>
                                            <td align="center" style="border-left: none; border-right: none; width: 90px;">
                                                <asp:Button ID="btnReset" runat="server" Text="Reset" Font-Names="Verdana" Font-Size="14px"
                                                    BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black"
                                                    OnClick="btnReset_Click" />
                                            </td>
                                            <td align="center" style="border-left: none; width: 60px; padding-right: 4px;">
                                                <asp:Button ID="AddNew" runat="server" Text="Add" Font-Names="Verdana" Font-Size="14px"
                                                    BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black"
                                                    OnClick="AddNew_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel ID="pnlShow" BorderWidth="1px" runat="server" Style="border-color: Green;
                                        border-style: solid; border-width: 1px">
                                        <table width="100%">
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
                                            <tr>
                                                <td colspan="3">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100%" colspan="3">
                                                    <asp:Panel ID="pnl" runat="server" ScrollBars="Both" Width="1010px">
                                                        <asp:Label ID="lblMessage" Text="" runat="server" ForeColor="Red" Font-Names="Verdana"
                                                            Font-Size="11px" />
                                                        <asp:GridView ID="dgvPatientList" runat="server" CellPadding="4" ForeColor="#333333"
                                                            BorderColor="Green" BorderStyle="Solid" BorderWidth="1px" GridLines="Both" Font-Names="Verdana"
                                                            Width="100%" Font-Size="Small" AllowPaging="true" PageSize="20" AutoGenerateColumns="false"
                                                            DataKeyNames="AdmitId" OnDataBound="dgvPatientList_DataBound" OnPageIndexChanged="dgvPatientList_PageIndexChanged"
                                                            OnPageIndexChanging="dgvPatientList_PageIndexChanging" OnRowCommand="dgvPatientList_RowCommand">
                                                            <FooterStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" />
                                                            <RowStyle BackColor="#E0F0E8" Font-Size="11px" Wrap="False" />
                                                            <PagerStyle BackColor="#3b3535" ForeColor="White" HorizontalAlign="Left" Font-Size="11px" />
                                                            <SelectedRowStyle BackColor="#006600" Font-Bold="True" ForeColor="#333333" />
                                                            <HeaderStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" Font-Size="12px"
                                                                Wrap="False" />
                                                            <EditRowStyle BackColor="#2461BF" />
                                                            <AlternatingRowStyle BackColor="White" Wrap="False" />
                                                            <Columns>
                                                                <asp:BoundField DataField="PatientCode" HeaderText="MRN" ReadOnly="True" SortExpression="PatientCode" />
                                                                <asp:BoundField DataField="PatientAdmitDate" HeaderText="Visit Date" ReadOnly="True"
                                                                    SortExpression="PatientAdmitDate" DataFormatString="{0:MM/dd/yyyy}" />
                                                                <asp:BoundField DataField="FullName" HeaderText="Patient Name" ReadOnly="True" SortExpression="FullName" />
                                                                <asp:BoundField DataField="PatientAddress" HeaderText="Address" ReadOnly="True" SortExpression="PatientAddress" />
                                                                <asp:BoundField DataField="Age" HeaderText="Age" ReadOnly="True" SortExpression="Age" />
                                                                <asp:BoundField DataField="AgeIn" HeaderText="Yrs/Month" ReadOnly="True" SortExpression="AgeIn" />
                                                                <asp:BoundField DataField="GenderDesc" HeaderText="Gender" ReadOnly="True" SortExpression="GenderDesc" />
                                                                <asp:BoundField DataField="PatientContactNo" HeaderText="ContactNo." ReadOnly="True"
                                                                    SortExpression="PatientContactNo" />
                                                                <asp:BoundField DataField="PatientType" HeaderText="PatientType" ReadOnly="True"
                                                                    SortExpression="PatientType" />
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

