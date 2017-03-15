<%@ Page Title="" Language="C#" MasterPageFile="~/mstAdmin.Master" AutoEventWireup="true" CodeBehind="frmEmployeeMaster.aspx.cs" Inherits="MainProjectHos.frmEmployeeMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/content.css" rel="stylesheet" type="text/css" />
    <script src="Notimoo/mootools-1.2-core.js" type="text/javascript"></script>
    <link href="Notimoo/notimoo-documented-1.1.css" rel="stylesheet" type="text/css" />
    <script src="Notimoo/notimoo-documented-1.1.js" type="text/javascript"></script>
    <link href="Notimoo/notimoo-v1.1.css" rel="stylesheet" type="text/css" />
    <script src="Notimoo/notimoo-v1.1.js" type="text/javascript"></script>
    <link href="Style/AdminStyle.css" rel="stylesheet" type="text/css" />
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
                                    <asp:Label ID="Label1" runat="server" Text="Employee Master" Font-Names="Verdana"
                                        Font-Size="16px" Font-Bold="true" ForeColor="#3b3535"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="width: 15%;">
                                    <table border="2px" style="border-color: Black; height: 50px;" cellpadding="0" cellspacing="0"
                                        width="900px">
                                        <tr>
                                            <td align="center" style="border-right: none; width: 575px;">
                                                <asp:TextBox ID="txtSearch" runat="server" Font-Bold="true" Font-Names="Verdana"
                                                    Font-Size="13px" Width="580px" />
                                                <%--<cc:TextBoxWatermarkExtender ID="txtSearchWatermark" runat="server" TargetControlID="txtSearch"
                                                    WatermarkCssClass="watermarked" WatermarkText="EmployeeName,EmpCode,Address" />--%>
                                            </td>
                                            <td align="center" style="border-left: none; border-right: none; width: 60px;">
                                                <asp:Button ID="btnSearch" runat="server" BackColor="#3b3535" Font-Names="Verdana"
                                                    Font-Size="13px" ForeColor="White" OnClick="btnSearch_Click" Style="border: 1px solid black"
                                                    Text="Search" Width="70px" />
                                            </td>
                                            <td align="center" style="border-left: none; border-right: none; width: 60px;">
                                                <asp:Button ID="btnReset" runat="server" BackColor="#3b3535" Font-Names="Verdana"
                                                    Font-Size="13px" ForeColor="White" OnClick="btnReset_Click" Style="border: 1px solid black"
                                                    Text="Reset" Width="70px" />
                                            </td>
                                            <td align="center" style="border-left: none; border-right: none; width: 60px;">
                                                <asp:Button ID="BtnAddNewEmp" runat="server" Text="Add New" Font-Names="Verdana"
                                                    Font-Size="13px" OnClick="BtnAddNewEmp_Click" BackColor="#3b3535" ForeColor="White"
                                                    Width="70px" Style="border: 1px solid black" onmouseover="SetBtnMouseOver(this)"
                                                    onmouseout="SetBtnMouseOut(this)" />
                                            </td>
                                            <td align="center" style="border-left: none; border-right: none; width: 60px;">
                                                <asp:Button ID="btnExcel" runat="server" Text="Excel" Font-Names="Verdana" Font-Size="13px"
                                                    BackColor="#3b3535" ForeColor="White" Width="70px" Style="border: 1px solid black;"
                                                    onmouseover="SetBtnMouseOver(this)" onmouseout="SetBtnMouseOut(this)" OnClick="btnExcel_Click" />
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
                                                    <asp:GridView ID="dgvEmployee" runat="server" CellPadding="4" ForeColor="#333333"
                                                        DataKeyNames="EmpCode" GridLines="Both" Font-Names="Verdana" Width="1010px" Font-Size="Small"
                                                        AllowPaging="true" PageSize="15" AutoGenerateColumns="false" 
                                                        OnDataBound="dgvEmployee_DataBound"
                                                        OnPageIndexChanged="dgvEmployee_PageIndexChanged" OnPageIndexChanging="dgvEmployee_PageIndexChanging">
                                                        <FooterStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" />
                                                        <RowStyle BackColor="#E0F0E8" Font-Size="11px" Wrap="False" />
                                                        <PagerStyle BackColor="#3b3535" ForeColor="White" HorizontalAlign="Left" Font-Size="11px" />
                                                        <SelectedRowStyle BackColor="#006600" Font-Bold="True" ForeColor="#333333" />
                                                        <HeaderStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" Font-Size="12px"
                                                            Wrap="False" />
                                                        <EditRowStyle BackColor="#2461BF" />
                                                        <AlternatingRowStyle BackColor="White" Wrap="False" />
                                                        <Columns>
                                                            <asp:BoundField DataField="EmpCode" HeaderText="Emp Code" ReadOnly="True" SortExpression="EmpCode" />
                                                            <asp:BoundField DataField="EmpFirstName" HeaderText="First Name" ReadOnly="True"
                                                                SortExpression="EmpFirstName" />
                                                            <asp:BoundField DataField="EmpMiddleName" HeaderText="Middle Name" ReadOnly="True"
                                                                SortExpression="EmpMiddleName" />
                                                            <asp:BoundField DataField="EmpLastName" HeaderText="Last Name" ReadOnly="True" SortExpression="EmpLastName" />
                                                            <asp:BoundField DataField="EmpDOB" HeaderText="Birth Date" ReadOnly="True" SortExpression="EmpDOB"
                                                                DataFormatString="{0:d}" />
                                                            <asp:BoundField DataField="EmpDOJ" HeaderText="Joining Date" ReadOnly="True" SortExpression="EmpDOJ"
                                                                DataFormatString="{0:d}" />
                                                            <asp:BoundField DataField="EmpAddress" HeaderText="Address" ReadOnly="True" SortExpression="EmpAddress" />
                                                            <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="ImageEdit" runat="server" ImageUrl="~/images/edit.jpg" Height="24px"
                                                                        Width="24px" OnClick="btnUpdate_Click" />
                                                                </ItemTemplate>
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
                        <table width="100%">
                            <tr width="100%">
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
                                                                <td align="center" colspan="2">
                                                                    <asp:Label ID="lblHeading" runat="server" Text="Employee Registration Form" Font-Names="Verdana"
                                                                        Font-Size="15px" Font-Bold="true" ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" class="style1">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" align="center">
                                                                    <asp:Label ID="lblMsg" runat="server" Text="" Font-Names="Verdana" Font-Size="11px"
                                                                        ForeColor="Red"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" style="width: 45%;">
                                                                    <asp:Label ID="lblEmpCode" runat="server" Text="Employee Code :" Font-Names="Verdana"
                                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtEmpCode" runat="server" MaxLength="50" ReadOnly="true" Font-Names="Verdana"
                                                                        Font-Size="11px" Width="130px" Font-Bold="true"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="lblFirstName" runat="server" Text="First Name :" Font-Names="Verdana"
                                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtFirstName" runat="server" MaxLength="50" Font-Names="Verdana" style="text-transform:capitalize"
                                                                        Font-Size="11px" Width="130px"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="rfvFirstName"
                                                                            SetFocusOnError="true" Display="Dynamic" runat="server" ForeColor="Red" ControlToValidate="txtFirstName"
                                                                            Font-Size="13" ValidationGroup="Save" ErrorMessage="*">
                                                                        </asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="lblMiddleName" runat="server" Text="Middle Name :" Font-Names="Verdana"
                                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtMidleName" runat="server" MaxLength="50" Font-Names="Verdana" style="text-transform:capitalize"
                                                                        Font-Size="11px" Width="130px"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvMiddleName" runat="server" ForeColor="Red" SetFocusOnError="true"
                                                                        Display="Dynamic" ControlToValidate="txtMidleName" Font-Size="13" ValidationGroup="Save"
                                                                        ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="lblLastName" runat="server" Text="Last Name :" Font-Names="Verdana"
                                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtLastName" runat="server" MaxLength="50" Font-Names="Verdana" style="text-transform:capitalize"
                                                                        Font-Size="11px" Width="130px"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red"
                                                                        SetFocusOnError="true" Display="Dynamic" ControlToValidate="txtLastName" Font-Size="13"
                                                                        ValidationGroup="Save" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="Label8" runat="server" Text="Education :" Font-Names="Verdana"
                                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtEducation" runat="server"  Font-Names="Verdana" TextMode="MultiLine"
                                                                        Font-Size="11px" Width="60%"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="Label9" runat="server" Text="Registration No:" Font-Names="Verdana"
                                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtRegistrationNo" runat="server" Font-Names="Verdana"
                                                                        Font-Size="11px" Width="60%"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="Label11" runat="server" Text="Department :" Font-Names="Verdana" Font-Size="11px"
                                                                        ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:DropDownList ID="ddlDepartment" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                        Width="130px">
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvDept" runat="server" ForeColor="Red" SetFocusOnError="true"
                                                                        Display="Dynamic" InitialValue="0" ControlToValidate="ddlDepartment" Font-Size="13"
                                                                        ValidationGroup="Save" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="Label2" runat="server" Text="Designation :" Font-Names="Verdana" Font-Size="11px"
                                                                        ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:DropDownList ID="ddlDesignation" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                        Width="130px">
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ForeColor="Red"
                                                                        SetFocusOnError="true" Display="Dynamic" InitialValue="0" ControlToValidate="ddlDesignation"
                                                                        Font-Size="13" ValidationGroup="Save" ErrorMessage="*" Text="*"></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="lblDOB" runat="server" Text="Birth Date :" Font-Names="Verdana" Font-Size="11px"
                                                                        ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtDOBDate" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                        Width="71px" MaxLength="10"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvDOB" runat="server" ValidationGroup="Save" 
                                                                    ErrorMessage="*" SetFocusOnError="true" Display="Dynamic" Font-Size="13pt" 
                                                                    ForeColor="Red" ControlToValidate="txtDOBDate"></asp:RequiredFieldValidator>
                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ForeColor="Red"
                                                                        ControlToValidate="txtDOBDate" ValidationGroup="Save" ErrorMessage="*" SetFocusOnError="true"
                                                                        Display="Dynamic" ValidationExpression="^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$"
                                                                        Font-Size="13pt"></asp:RegularExpressionValidator>
                                                                    <asp:Calendar ID="CalDOBDate" runat="server" TargetControlID="txtDOBDate"
                                                                        Format="dd/MM/yyyy"></asp:Calendar>
                                                                    
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="lblDOJ" runat="server" Text="Joining Date :" Font-Names="Verdana"
                                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtJoinDate" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                        Width="71px" MaxLength="10">
                                                                    </asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="Save" 
                                                                    ErrorMessage="*" SetFocusOnError="true" Display="Dynamic" Font-Size="13pt" 
                                                                    ForeColor="Red" ControlToValidate="txtJoinDate"></asp:RequiredFieldValidator>
                                                                    <asp:RegularExpressionValidator ID="regJoinDate" runat="server" ControlToValidate="txtJoinDate"
                                                                        ValidationGroup="Save" ErrorMessage="*" SetFocusOnError="true" Display="Dynamic"
                                                                        ValidationExpression="^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$"
                                                                        Font-Size="13pt" ForeColor="Red"></asp:RegularExpressionValidator>
                                                                    <asp:Calendar ID="CalendarExtender1" runat="server" TargetControlID="txtJoinDate"
                                                                        Format="dd/MM/yyyy">
                                                                    </asp:Calendar>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="lblAddress" runat="server" Text="Address :" Font-Names="Verdana" Font-Size="11px"
                                                                        ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtAddress" runat="server" MaxLength="200" TextMode="MultiLine" style="text-transform:capitalize"
                                                                        Width="60%" Font-Names="Verdana" Font-Size="11px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="Label3" runat="server" Text="Bank Name :" Font-Names="Verdana" Font-Size="11px"
                                                                        ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtBank" runat="server" Font-Names="Verdana" Font-Size="11px" Width="60%"
                                                                        style="text-transform:capitalize"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="Label4" runat="server" Text="Bank A/C NO. :" Font-Names="Verdana"
                                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtBankAc" runat="server" MaxLength="16" Font-Names="Verdana" Font-Size="11px"
                                                                        Width="200px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="Label5" runat="server" Text="PF NO. :" Font-Names="Verdana" Font-Size="11px"
                                                                        ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtPF" runat="server" MaxLength="20" Font-Names="Verdana" Font-Size="11px"
                                                                        Width="200px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="Label6" runat="server" Text="Pan No :" Font-Names="Verdana" Font-Size="11px"
                                                                        ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtPan" runat="server" Font-Names="Verdana" Font-Size="11px" Width="200px"
                                                                        MaxLength="10"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="Label7" runat="server" Text="Basic Salary:" Font-Names="Verdana" Font-Size="11px"
                                                                        ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtBaseSal" runat="server" MaxLength="50" Font-Names="Verdana" Font-Size="11px"
                                                                        Width="200px"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ForeColor="Red"
                                                                        SetFocusOnError="true" Display="Dynamic" ControlToValidate="txtBaseSal" Font-Size="13"
                                                                        ValidationGroup="Save" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr align="center">
                                                                <td align="center" colspan="2">
                                                                    <asp:Button ID="BtnSave" runat="server" Text="Save" Font-Names="Verdana" Font-Size="12px"
                                                                        OnClick="BtnSave_Click" ValidationGroup="Save" BackColor="#3b3535" ForeColor="White"
                                                                        Width="80px" Style="border: 1px solid black; height: 20px;" />
                                                                    <asp:Button ID="btnUpdate" runat="server" Text="Update" Font-Names="Verdana" Font-Size="12px"
                                                                        OnClick="BtnEdit_Click" ValidationGroup="Update" BackColor="#3b3535" ForeColor="White"
                                                                        Width="80px" Style="border: 1px solid black" />
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
