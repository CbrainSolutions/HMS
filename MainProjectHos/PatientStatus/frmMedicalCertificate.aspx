<%@ Page Title="" Language="C#" MasterPageFile="~/mstAdmin.Master" AutoEventWireup="true" CodeBehind="frmMedicalCertificate.aspx.cs" Inherits="MainProjectHos.PatientStatus.frmMedicalCertificate" %>
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
    <script type="text/javascript">
        function hideModalPopup() {
            var modalPopupBehavior = $find('programmaticModalPopupBehavior');
            modalPopupBehavior.hide();
            return false;
        }

        function hideDeleteModalPopup() {
            var modalPopupBehavior = $find('DeleteprogrammaticModalPopupBehavior');
            modalPopupBehavior.hide();
            return false;
        }

        function hideEditModalPopup() {
            var modalPopupBehavior = $find('programmaticModalPopupBehaviorEdit');
            modalPopupBehavior.hide();
            return false;
        }

        function KeyValid(e) {
            var keynum
            var keychar
            var numcheck
            // For Internet Explorer
            if (window.event) {
                keynum = e.keyCode
            }
            // For Netscape/Firefox/Opera
            else if (e.which) {
                keynum = e.which
            }
            keychar = String.fromCharCode(keynum)

            //List of special characters you want to restrict
            if (keychar == "!" || keychar == "@" || keychar == "#"
    || keychar == "$" || keychar == "%" || keychar == "^" || keychar == "&"
    || keychar == "*" || keychar == "(" || keychar == ")"
    || keychar == "<" || keychar == ">") {
                return false;
            }
            else {
                return true;
            }
        }

        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }

        var oldgridcolor;
        function SetMouseOver(element) {
            oldgridcolor = element.style.backgroundColor;
            element.style.backgroundColor = '#B2C2D1';

        }
        function SetMouseOut(element) {
            element.style.backgroundColor = oldgridcolor;
            element.style.textDecoration = 'none';
        }

//        var oldgridcolor;
//        function SetBtnMouseOver(element) {
//            oldgridcolor = element.style.backgroundColor;
//            element.style.backgroundColor = 'Black';

        //}
        function SetBtnMouseOut(element) {
            element.style.backgroundColor = oldgridcolor;
            element.style.textDecoration = 'none';
        }
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
                                    <asp:Label ID="Label1" runat="server" Text="Medical Certificate" Font-Names="Verdana"
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
                                                <cc:TextBoxWatermarkExtender ID="txtSearchWatermark" runat="server" TargetControlID="txtSearch"
                                                    WatermarkCssClass="watermarked" WatermarkText="PatientName" />
                                            </td>
                                            <td align="center" style="border-left: none; border-right: none; width: 60px;">
                                                <asp:Button ID="btnSearch" runat="server" BackColor="#3b3535" Font-Names="Verdana"
                                                    Font-Size="13px" ForeColor="White" Style="border: 1px solid black" Text="Search"
                                                    Width="70px" OnClick="btnSearch_Click" />
                                            </td>
                                            <td align="center" style="border-left: none; border-right: none; width: 60px;">
                                                <asp:Button ID="btnReset" runat="server" BackColor="#3b3535" Font-Names="Verdana"
                                                    Font-Size="13px" ForeColor="White" Style="border: 1px solid black" Text="Reset"
                                                    Width="70px" OnClick="btnReset_Click" />
                                            </td>
                                            <td align="center" style="border-left: none; border-right: none; width: 60px;">
                                                <asp:Button ID="BtnAddNewDept" runat="server" Text="Add New" Font-Names="Verdana"
                                                    Font-Size="13px" OnClick="BtnAddNewDept_Click" BackColor="#3b3535" ForeColor="White"
                                                    Width="80px" Style="border: 1px solid black" />
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
                                                    <asp:Label ID="lblMessage" runat="server" Text="" Font-Names="Verdana" Font-Size="11px"
                                                        ForeColor="Red"></asp:Label>
                                                    <asp:Panel ID="pnl" runat="server" Width="1010px" BorderColor="Green" BorderStyle="Solid"
                                                        BorderWidth="1px">
                                                        <asp:GridView ID="dgvShift" runat="server" CellPadding="4" ForeColor="#333333" DataKeyNames="CertiID"
                                                            GridLines="Both" Font-Names="Verdana" Width="100%" Font-Size="Small" AllowPaging="true"
                                                            PageSize="20" AutoGenerateColumns="false" OnRowCommand="dgvDepartment_RowCommand"
                                                            OnDataBound="dgvDepartment_DataBound" OnPageIndexChanged="dgvDepartment_PageIndexChanged"
                                                            OnPageIndexChanging="dgvDepartment_PageIndexChanging" OnRowDataBound="dgvDepartment_RowDataBound"
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
                                                                <asp:BoundField DataField="CertiID" HeaderText="Certificate Id" ReadOnly="True"
                                                                    SortExpression="CertiID" />
                                                                <asp:BoundField DataField="FullName" HeaderText="Patient Name" ReadOnly="True" SortExpression="FullName" />
                                                                 <asp:BoundField DataField="Age" HeaderText="Age" ReadOnly="True" SortExpression="Age" />
                                                                 <asp:BoundField DataField="Daignosis" HeaderText="Daignosis" ReadOnly="True" SortExpression="Daignosis" />
                                                                <asp:BoundField DataField="OPDFrom" HeaderText="OPD FromDate" ReadOnly="True" SortExpression="OPDFrom"
                                                                    DataFormatString="{0:MM/dd/yyyy}" />
                                                                <asp:BoundField DataField="OPDTo" HeaderText="OPD ToDate" ReadOnly="True" SortExpression="OPDTo"
                                                                    DataFormatString="{0:MM/dd/yyyy}" />
                                                                <asp:BoundField DataField="IndoorOn" HeaderText="AdmittedIndoor OnDate" ReadOnly="True" SortExpression="IndoorOn"
                                                                    DataFormatString="{0:MM/dd/yyyy}" />
                                                                <asp:BoundField DataField="DischargeOn" HeaderText="Discharge OnDate" ReadOnly="True" SortExpression="DischargeOn"
                                                                    DataFormatString="{0:MM/dd/yyyy}" />
                                                                <%--<asp:BoundField DataField="OperatedFor" HeaderText="Operated For" ReadOnly="True" SortExpression="OperatedFor" />
                                                                <asp:BoundField DataField="OperatedForOn" HeaderText="Operated OnDate" ReadOnly="True" SortExpression="OperatedForOn"
                                                                    DataFormatString="{0:MM/dd/yyyy}" />
                                                                <asp:BoundField DataField="AdvisedRestDays" HeaderText="Advised RestDays" ReadOnly="True" SortExpression="AdvisedRestDays" />
                                                                <asp:BoundField DataField="AdvisedRestFrom" HeaderText="AdvisedRest FromDate" ReadOnly="True" SortExpression="AdvisedRestFrom"
                                                                    DataFormatString="{0:MM/dd/yyyy}" />
                                                                <asp:BoundField DataField="ContinueRestFrom" HeaderText="ContinueRest FromDate" ReadOnly="True" SortExpression="ContinueRestFrom"
                                                                    DataFormatString="{0:MM/dd/yyyy}" />
                                                                <asp:BoundField DataField="ContinuedRestDays" HeaderText="Continued RestDays" ReadOnly="True" SortExpression="ContinuedRestDays" />
                                                                <asp:BoundField DataField="WorkFrom" HeaderText="Work FromDate" ReadOnly="True" SortExpression="WorkFrom"
                                                                    DataFormatString="{0:MM/dd/yyyy}" />--%>
                                                               <%-- <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="ImageEdit" runat="server" ImageUrl="~/images/edit.jpg" Height="24px"
                                                                            Width="24px" OnClick="btnUpdate_Click" /></ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </asp:TemplateField>--%>
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
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                    <asp:View ID="View2" runat="server">
                        <table width="100%" style="border-color: Green; border-style: solid; border-width: 2px"
                            cellpadding="0">
                            <tr style="width: 100%">
                                <td style="width: 100%">
                                    <asp:Panel ID="Panel2" runat="server" Width="99.7%" Style="text-align: center; background-color: #E0F0E8;
                                        height: auto;" BorderColor="Green" BorderStyle="Solid" BorderWidth="1px">
                                        <table width="100%">
                                            <tr style="width: 100%;">
                                                <td align="center" colspan="4">
                                                    <asp:Label ID="lblHeading" runat="server" Text="Medical Certificate" Font-Names="Verdana"
                                                        Font-Size="15px" Font-Bold="true" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" align="center">
                                                    <asp:Label ID="lblMsg" runat="server" Text="" Font-Names="Verdana" Font-Size="11px"
                                                        ForeColor="Red"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="width: 30%;">
                                                    <asp:Label ID="lblPatientName" runat="server" Text="Patient Name :" Font-Names="Verdana"
                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left" style="width: 16%;">
                                                    <asp:DropDownList ID="ddlPatientName" runat="server" Width="150px" AutoPostBack="True">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvDeptDesc" runat="server" ForeColor="Red" ControlToValidate="ddlPatientName"
                                                        Font-Size="13" ValidationGroup="Save" ErrorMessage="*" InitialValue="0" Display="Dynamic"></asp:RequiredFieldValidator>
                                                </td>
                                                <td align="right" style="width: 13%;">
                                                    <asp:Label ID="lblGrandFather" runat="server" Text="Age :" Font-Names="Verdana"
                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left" style="width: 30%;">
                                                    <asp:TextBox ID="txtAge" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                        Width="100px" MaxLength="10"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="lblNewBirth" runat="server" Text="Daignosis : " Font-Names="Verdana"
                                                        Font-Size="13px" ForeColor="#3B3535" Font-Bold="False" Font-Underline="False"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtDaignosis" runat="server" Font-Names="Verdana" Font-Size="11px" TextMode="MultiLine"
                                                        Width="150px" MaxLength="10"></asp:TextBox>
                                                </td>
                                                
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label2" runat="server" Text="Treated as an OPD Patient From Date:" Font-Names="Verdana"
                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtOPDFrom" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                        Width="66px" MaxLength="10"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red"
                                                        ControlToValidate="txtOPDFrom" Font-Size="11" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                                                    <cc:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtOPDFrom"
                                                        Format="MM/dd/yyyy">
                                                    </cc:CalendarExtender>
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="Label3" runat="server" Text=" To Date :" Font-Names="Verdana"
                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtOPDTo" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                        Width="66px" MaxLength="10"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ForeColor="Red"
                                                        ControlToValidate="txtOPDTo" Font-Size="11" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                                                    <cc:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtOPDTo"
                                                        Format="MM/dd/yyyy">
                                                    </cc:CalendarExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="lblBirthDate" runat="server" Text=" Admitted as an indoor Patient On Date :" Font-Names="Verdana"
                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtIndoorOn" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                        Width="66px" MaxLength="10"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ForeColor="Red"
                                                        ControlToValidate="txtIndoorOn" Font-Size="11" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                                                    <cc:CalendarExtender ID="CalAppDate" runat="server" TargetControlID="txtIndoorOn"
                                                        Format="MM/dd/yyyy">
                                                    </cc:CalendarExtender>
                                                </td>
                                               <td align="right">
                                                    <asp:Label ID="Label4" runat="server" Text=" Discahrged On Date :" Font-Names="Verdana"
                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtDischargeOn" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                        Width="66px" MaxLength="10"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ForeColor="Red"
                                                        ControlToValidate="txtDischargeOn" Font-Size="11" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                                                    <cc:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtDischargeOn"
                                                        Format="MM/dd/yyyy">
                                                    </cc:CalendarExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="lblWeight" runat="server" Text="Operated For :" Font-Names="Verdana" Font-Size="11px"
                                                        ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtOperatedFor" runat="server" MaxLength="150" Font-Names="Verdana" Font-Size="11px" TextMode="MultiLine"
                                                        Width="150px"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="Label5" runat="server" Text=" On Date :" Font-Names="Verdana"
                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtOperatedForOn" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                        Width="66px" MaxLength="10"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ForeColor="Red"
                                                        ControlToValidate="txtOperatedForOn" Font-Size="11" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                                                    <cc:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtOperatedForOn"
                                                        Format="MM/dd/yyyy">
                                                    </cc:CalendarExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label6" runat="server" Text="Advised Rest Days :" Font-Names="Verdana" Font-Size="11px"
                                                        ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtAdvisedRestDays" runat="server" MaxLength="150" Font-Names="Verdana" Font-Size="11px" 
                                                        Width="80px"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="Label7" runat="server" Text=" Rest From Date :" Font-Names="Verdana"
                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtAdvisedRestFrom" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                        Width="66px" MaxLength="10"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ForeColor="Red"
                                                        ControlToValidate="txtAdvisedRestFrom" Font-Size="11" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                                                    <cc:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtAdvisedRestFrom"
                                                        Format="MM/dd/yyyy">
                                                    </cc:CalendarExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label9" runat="server" Text=" Advised To continue rest From :" Font-Names="Verdana"
                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtContinueRestFrom" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                        Width="66px" MaxLength="10"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ForeColor="Red"
                                                        ControlToValidate="txtContinueRestFrom" Font-Size="11" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                                                    <cc:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="txtContinueRestFrom"
                                                        Format="MM/dd/yyyy">
                                                    </cc:CalendarExtender>
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="Label8" runat="server" Text="for another Days :" Font-Names="Verdana" Font-Size="11px"
                                                        ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtContinuedRestDays" runat="server" MaxLength="150" Font-Names="Verdana" Font-Size="11px" 
                                                        Width="80px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label10" runat="server" Text=" normal duties/Light workFrom :" Font-Names="Verdana"
                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtWorkFrom" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                        Width="66px" MaxLength="10"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ForeColor="Red"
                                                        ControlToValidate="txtWorkFrom" Font-Size="11" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                                                    <cc:CalendarExtender ID="CalendarExtender7" runat="server" TargetControlID="txtWorkFrom"
                                                        Format="MM/dd/yyyy">
                                                    </cc:CalendarExtender>
                                                </td>
                                            </tr>
                                            <tr style="height: 10px;">
                                                <td colspan="2">
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td align="center" colspan="4">
                                                    <asp:Button ID="BtnSave" runat="server" Text="Save" Font-Names="Verdana" Font-Size="12px"
                                                        ValidationGroup="Save" BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black"
                                                        OnClick="BtnSave_Click" />
                                                    <asp:Button ID="btnUpdate" runat="server" Text="Update" Font-Names="Verdana" Font-Size="12px"
                                                        BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black"
                                                        OnClick="BtnEdit_Click" />
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
    <input id="hdnPanel" type="hidden" runat="server" value="none" />
</asp:Content>
