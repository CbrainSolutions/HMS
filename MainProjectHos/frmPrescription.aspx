<%@ Page Title="" Language="C#" MasterPageFile="~/mstAdmin.Master" AutoEventWireup="true" CodeBehind="frmPrescription.aspx.cs" Inherits="MainProjectHos.frmPrescription" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" language="javascript">
 
    </script>
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
                                    <asp:Label ID="Label1" runat="server" Text="Prescription Master" Font-Names="Verdana"
                                        Font-Size="16px" Font-Bold="True" ForeColor="#3B3535"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="width: 15%;">
                                    <table border="1px" cellpadding="0" cellspacing="0" style="height: 40px;" width="930px">
                                        <tr>
                                            <td align="center" style="border-right: none; width: 575px;">
                                                <asp:TextBox ID="txtSearch" runat="server" Font-Bold="true" Font-Names="Verdana"
                                                    Font-Size="13px" Width="580px" />
                                            </td>
                                            <td align="center" style="border-left: none; border-right: none; width: 60px;">
                                                <asp:Button ID="btnSearch" runat="server" BackColor="#3b3535" Font-Names="Verdana"
                                                    Font-Size="14px" ForeColor="White" OnClick="btnSearch_Click" Style="border: 1px solid black"
                                                    Text="Search" Width="80px" />
                                            </td>
                                            <td align="center" style="border-left: none; border-right: none; width: 60px;">
                                                <asp:Button ID="btnReset" runat="server" BackColor="#3b3535" Font-Names="Verdana"
                                                    Font-Size="14px" ForeColor="White" OnClick="btnReset_Click" Style="border: 1px solid black"
                                                    Text="Reset" Width="80px" />
                                            </td>
                                            <td align="center" style="border-left: none; border-right: none; width: 60px;">
                                                <asp:Button ID="BtnAddNewPrescription" runat="server" Text="Add New" Font-Names="Verdana"
                                                    Font-Size="14px" BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black"
                                                    onmouseover="SetBtnMouseOver(this)" onmouseout="SetBtnMouseOut(this)" OnClick="BtnAddNewPrescription_Click" />
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
                            <tr>
                                <td>
                                    <asp:Panel ID="pnlShow" BorderColor="Maroon" BorderWidth="1px" runat="server" Style="border-color: Green;
                                        border-style: solid; border-width: 1px">
                                        <table width="100%">
                                            <tr>
                                                <td align="left" class="style1">
                                                    <asp:Label ID="lblPageCount" runat="server" Text="" Font-Names="Verdana" Font-Size="11px"
                                                        ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="right" class="style1">
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
                                                    <asp:Panel ID="pnl" runat="server" Width="1020px" Style="border-color: Green; border-style: solid;
                                                        border-width: 1px">
                                                        <asp:Label ID="lblMessage" runat="server" Text="" Font-Names="Verdana" Font-Size="11px"
                                                            ForeColor="Red"></asp:Label>
                                                        <asp:GridView ID="dgvClaim" runat="server" CellPadding="4" ForeColor="#333333" GridLines="Both"
                                                            Font-Names="Verdana" Width="100%" Font-Size="Small" AllowPaging="true" PageSize="20"
                                                            AutoGenerateColumns="false" DataKeyNames="Prescription_Id" OnDataBound="dgvClaim_DataBound"
                                                            OnPageIndexChanged="dgvClaim_PageIndexChanged" OnPageIndexChanging="dgvClaim_PageIndexChanging">
                                                            <FooterStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" />
                                                            <RowStyle BackColor="#E0F0E8" Font-Size="11px" Wrap="False" />
                                                            <PagerStyle BackColor="#3b3535" ForeColor="White" HorizontalAlign="Left" Font-Size="11px" />
                                                            <SelectedRowStyle BackColor="#006600" Font-Bold="True" ForeColor="#333333" />
                                                            <HeaderStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" Font-Size="12px"
                                                                Wrap="False" />
                                                            <EditRowStyle BackColor="#2461BF" />
                                                            <AlternatingRowStyle BackColor="White" Wrap="False" />
                                                            <Columns>
                                                                <asp:BoundField DataField="Prescription_Id" HeaderText="Prescription No." ReadOnly="True"
                                                                    SortExpression="Prescription_Id" />
                                                                <asp:BoundField DataField="PatientName" HeaderText="Patient Name" ReadOnly="True"
                                                                    SortExpression="PatientName" />
                                                                <asp:BoundField DataField="Prescription_Date" HeaderText="Prescription Date" ReadOnly="True"
                                                                    SortExpression="Prescription_Date" DataFormatString="{0:MM/dd/yyyy}" />
                                                                <asp:BoundField DataField="CategoryName" HeaderText="Department" ReadOnly="True"
                                                                    SortExpression="CategoryName" />
                                                                <asp:BoundField DataField="DeptDoctor" HeaderText="Dept.Doctor" ReadOnly="True" SortExpression="DeptDoctor" />
                                                                <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="ImageEdit" runat="server" ImageUrl="~/images/edit.jpg" Height="24px"
                                                                            Width="24px" OnClick="BtnEdit_Click" /></ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Print" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="ImgPrint" runat="server" ImageUrl="~/images/Report.bmp" Height="24px"
                                                                            Width="24px" OnClick="btnPrint_Click" /></ItemTemplate>
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
                    <asp:View ID="View2" runat="server">
                        <table width="100%">
                            <tr style="width: 100%;">
                                <td width="100%">
                                    <asp:Panel ID="pnlGrid" runat="server" Width="1040px" Style="text-align: center;
                                        background-color: #E0F0E8; height: auto;">
                                        <table width="100%" style="border-color: #3b3535; border-style: solid; border-width: 2px"
                                            cellpadding="0">
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="Panel2" runat="server" Width="99.7%" Style="text-align: center; background-color: #E0F0E8;
                                                        height: auto;" BorderColor="#3b3535" BorderStyle="Solid" BorderWidth="1px">
                                                        <table width="100%">
                                                            <tr>
                                                                <td align="center" colspan="4">
                                                                    <asp:Label ID="lblHeading" runat="server" Text="Prescription Master" Font-Names="Verdana"
                                                                        Font-Size="15px" Font-Bold="true" ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4" align="center" style="height: 20px;">
                                                                    <asp:Label ID="lblMsg" Text="" runat="server" ForeColor="Red" Font-Names="Verdana"
                                                                        Font-Size="11px"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 30%;" align="right">
                                                                    <asp:Label ID="Label7" runat="server" Text="Department :" Font-Names="Verdana" Font-Size="11px"
                                                                        ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td style="width: 20%;" align="left">
                                                                    <asp:DropDownList ID="ddlDeptCategory" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                        Width="150px" AutoPostBack="true" OnSelectedIndexChanged="ddlDeptCategory_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ForeColor="Red"
                                                                        ControlToValidate="ddlDeptCategory" Font-Size="11" ValidationGroup="Save" ErrorMessage="*"
                                                                        Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                </td>
                                                                <td style="width: 12%;" align="right">
                                                                    <asp:Label ID="Label20" runat="server" Text="Dept. Doctor :" Font-Names="Verdana"
                                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td style="width: 38%;" align="left">
                                                                    <asp:TextBox ID="txtDeptCat" runat="server" MaxLength="100" Font-Names="Verdana"
                                                                        Font-Size="11px" Width="150px" Font-Bold="true"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="Label2" Text="Patient Name : " runat="server" Font-Names="Verdana"
                                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:DropDownList ID="ddlPatient" runat="server" Width="150px" AutoPostBack="true"
                                                                        OnSelectedIndexChanged="ddlPatient_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red"
                                                                        ControlToValidate="ddlPatient" Font-Size="13" ValidationGroup="Save" ErrorMessage="*"
                                                                        Display="Dynamic" InitialValue="0"></asp:RequiredFieldValidator>
                                                                </td>
                                                                <td align="right">
                                                                    <asp:Label ID="Label4" Text="Prescription Date : " runat="server" Font-Names="Verdana"
                                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtPrescriptionDate" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                        Width="150px" MaxLength="10"></asp:TextBox>
                                                                    <cc:CalendarExtender ID="CalDate" runat="server" TargetControlID="txtPrescriptionDate"
                                                                        Format="MM/dd/yyyy">
                                                                    </cc:CalendarExtender>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtPrescriptionDate"
                                                                        Display="Dynamic" ErrorMessage="*" Font-Size="13" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                </td>
                                                                <td align="left">
                                                                </td>
                                                                <td align="right">
                                                                </td>
                                                                <td align="left">
                                                                    <asp:RegularExpressionValidator ID="revTxtFirstName" ValidationExpression="^([0]\d|[1][0-2])\/([0-2]\d|[3][0-1])\/([2][01]|[1][6-9])\d{2}(\s([0-1]\d|[2][0-3])(\:[0-5]\d){1,2})?$"
                                                                        ErrorMessage="Please Enter Only Date With MM/DD/YYYY Format" Font-Bold="False"
                                                                        Font-Size="11px" ForeColor="Red" ControlToValidate="txtPrescriptionDate" runat="server"
                                                                        ValidationGroup="Save" Display="Dynamic" Font-Names="verdana" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:CheckBox ID="chkDress" runat="server" AutoPostBack="true" Font-Names="Verdana"
                                                                        Font-Size="11px" ForeColor="#3b3535" Text="Dressing" />
                                                                </td>
                                                                <td align="left">
                                                                    <asp:CheckBox ID="chkInject" runat="server" AutoPostBack="true" Font-Names="Verdana"
                                                                        Font-Size="11px" ForeColor="#3b3535" OnCheckedChanged="chkInject_CheckedChanged"
                                                                        Text="Injection" />
                                                                </td>
                                                                <td align="right">
                                                                    <asp:Label ID="lblInjection" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                        ForeColor="#3b3535" Text="Injection : "></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtInjection" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                        MaxLength="30" Width="150px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="Panel3" runat="server" Width="99.7%" Style="border-color: #3b3535;
                                                        border-style: solid; border-width: 1px">
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="width: 35%;" align="right">
                                                                    <asp:Label ID="Label3" Text="Medicine Name : " runat="server" Font-Names="Verdana"
                                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td style="width: 15%;" align="left">
                                                                    <asp:DropDownList ID="ddlTablet" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                        AutoPostBack="True" Width="130px" OnSelectedIndexChanged="ddlTablet_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvDept" runat="server" ForeColor="Red" SetFocusOnError="true"
                                                                        Display="Dynamic" InitialValue="0" ControlToValidate="ddlTablet" Font-Size="13"
                                                                        ValidationGroup="Add" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                </td>
                                                                <td style="width: 10%;" align="right">
                                                                    <asp:Label ID="Label11" Text="सकाळी : " runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                        ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td style="width: 50%;" align="left">
                                                                    <asp:TextBox ID="txtMorning" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                        Width="150px" MaxLength="30"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="Label6" Text="Balance Quantity : " runat="server" Font-Names="Verdana"
                                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:Label ID="lblBalQty" runat="server" Font-Names="verdana" Font-Size="11px" ForeColor="#3B3535" />
                                                                </td>
                                                                <td align="right">
                                                                    <asp:Label ID="lblafter" Text="दुपारी : " runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                        ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtafternoon" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                        Width="150px" MaxLength="30"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="lblNoDays" Text="NoOfDays : " runat="server" Font-Names="Verdana"
                                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtNoOfDays" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                        Width="150px" MaxLength="30"></asp:TextBox>
                                                                </td>
                                                                <td align="right">
                                                                    <asp:Label ID="lblNight" Text="संध्याकाळी : " runat="server" Font-Names="Verdana"
                                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtNight" runat="server" Font-Names="Verdana" Font-Size="11px" Width="150px"
                                                                        MaxLength="30"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="Label12" Text="Quantity : " runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                        ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td align="left" colspan='3'>
                                                                    <asp:TextBox ID="txtQuantity" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                        Width="150px" MaxLength="10"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr style="height: 30px;">
                                                                <td colspan='4'>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4" align="center" style="border-top: none; border-bottom: none;">
                                                                    <asp:Button ID="btnAdd" runat="server" Text="Add" Font-Names="Verdana" Font-Size="12px"
                                                                        ValidationGroup="AddOther" BackColor="#3b3535" ForeColor="White" Width="80px"
                                                                        Style="border: 1px solid black" OnClick="btnAddCharge_Click" />
                                                                    <asp:Button ID="btnUpdatecharge" runat="server" Text="Update" Font-Names="Verdana"
                                                                        Font-Size="12px" ValidationGroup="AddOther" BackColor="#3b3535" ForeColor="White"
                                                                        Width="80px" Style="border: 1px solid black" OnClick="btnUpdatecharge_Click" />
                                                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" Font-Names="Verdana" Font-Size="12px"
                                                                        BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black"
                                                                        OnClick="btnCancel_Click" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4">
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="Panel1" runat="server" ScrollBars="Both" Width="99.7%" Style="border-color: #3b3535;
                                                        border-style: solid; border-width: 1px">
                                                        <asp:GridView ID="dgvChargeDetails" runat="server" CellPadding="4" ForeColor="#333333"
                                                            GridLines="Both" Font-Names="Verdana" Width="100%" Font-Size="Small" AllowPaging="false"
                                                            AutoGenerateColumns="false" DataKeyNames="TempId" OnSelectedIndexChanged="dgvChargeDetails_SelectedIndexChanged">
                                                            <FooterStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" />
                                                            <RowStyle BackColor="#E0F0E8" Font-Size="11px" Wrap="False" />
                                                            <PagerStyle BackColor="#3b3535" ForeColor="White" HorizontalAlign="Left" Font-Size="11px" />
                                                            <SelectedRowStyle BackColor="#006600" Font-Bold="True" ForeColor="#333333" />
                                                            <HeaderStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" Font-Size="12px"
                                                                Wrap="False" />
                                                            <EditRowStyle BackColor="#2461BF" />
                                                            <AlternatingRowStyle BackColor="White" Wrap="False" />
                                                            <Columns>
                                                                <asp:BoundField DataField="ProductName" HeaderText="Medicine Name" ReadOnly="True"
                                                                    SortExpression="ProductName" />
                                                                <asp:BoundField DataField="Morning" HeaderText="Morning" ReadOnly="True" SortExpression="Morning" />
                                                                <asp:BoundField DataField="Afternoon" HeaderText="Afternoon" ReadOnly="True" SortExpression="Afternoon" />
                                                                <asp:BoundField DataField="Night" HeaderText="Night" ReadOnly="True" SortExpression="Night" />
                                                                <asp:BoundField DataField="NoOfDays" HeaderText="No Of Days" ReadOnly="True" SortExpression="NoOfDays" />
                                                                <asp:BoundField DataField="Quantity" HeaderText="Quantity" ReadOnly="True" SortExpression="Quantity" />
                                                                <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="ImageEdit" runat="server" ImageUrl="~/images/edit.jpg" Height="24px"
                                                                            Width="24px" OnClick="BtnEditCharge_Click" /></ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="ImgDelete" runat="server" ImageUrl="~/images/Erase.png" Height="24px"
                                                                            Width="24px" OnClick="btnDelete_Click" /></ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="lblInvestigation" Text="Investigations Done : " runat="server" Font-Names="Verdana"
                                                                    Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtInvestigation" runat="server" Font-Names="Verdana" Font-Size="11px" TextMode="MultiLine"
                                                                    Width="300px" Height="35px"></asp:TextBox>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Label ID="lblImpression" Text="Impression : " runat="server" Font-Names="Verdana"
                                                                    Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtImpression" runat="server" Font-Names="Verdana" Font-Size="11px" TextMode="MultiLine"
                                                                    Width="300px" Height="35px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="lblAdvice" Text="Advice Note : " runat="server" Font-Names="Verdana"
                                                                    Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtAdviceNote" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                    TextMode="MultiLine" Width="300px" Height="35px"></asp:TextBox>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Label ID="Label5" Text="Remarks: " runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                    ForeColor="#3b3535"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtRemarks" runat="server" Font-Names="Verdana" Font-Size="11px" Width="300px"
                                                                    TextMode="MultiLine" Height="35px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="BtnSave" runat="server" Text="Save" Font-Names="Verdana" Font-Size="12px"
                                                        ValidationGroup="Save" BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black"
                                                        OnClick="BtnSave_Click" />
                                                    <asp:Button ID="btnUpdate" runat="server" Text="Update" Font-Names="Verdana" Font-Size="12px"
                                                        BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black"
                                                        OnClick="BtnUpdate_Click" />
                                                    <asp:Button ID="BtnClose" runat="server" Text="Close" Font-Names="Verdana" Font-Size="12px"
                                                        BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black"
                                                        OnClick="BtnClose_Click" />
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
</asp:Content>

