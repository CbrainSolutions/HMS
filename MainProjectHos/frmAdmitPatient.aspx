<%@ Page Title="" Language="C#" MasterPageFile="~/mstAdmin.Master" AutoEventWireup="true" CodeBehind="frmAdmitPatient.aspx.cs" Inherits="MainProjectHos.frmAdmitPatient" %>
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
        $(function () {
            var $txt = $('input[id$=txtNew]');
            var $ddl = $('select[id$=ddlPatientName]');
            var $items = $('select[id$=ddlPatientName] option');

            $txt.keyup(function () {
                searchDdl($txt.val());
            });

            function searchDdl(item) {
                $ddl.empty();
                var exp = new RegExp(item, "i");
                var arr = $.grep($items,
                    function (n) {
                        return exp.test($(n).text());
                    });

                if (arr.length > 0) {
                    countItemsFound(arr.length);
                    $.each(arr, function () {
                        $ddl.append(this);
                        $ddl.get(0).selectedIndex = 0;
                    }
                    );
                }
                else {
                    countItemsFound(arr.length);
                    $ddl.append("<option>No Items Found</option>");
                }
            }

            //            function countItemsFound(num) {
            //                $("#para").empty();
            //                if ($txt.val().length) {
            //                    $("#para").html(num + " items found");
            //                }

            //            }
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="width: 100%">
                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="View1" runat="server">
                        <table width="90%">
                            <tr>
                                <td align="center">
                                    <asp:Label ID="Label1" runat="server" Text="Registered Patient" Font-Names="Verdana"
                                        Font-Size="16px" Font-Bold="True" ForeColor="#3B3535"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="width: 15%;">
                                    <table border="2px" cellpadding="0" cellspacing="0" width="860px" style="height: 40px;">
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
                                            <td align="center" style="border-left: none; border-right: none; width: 90px;">
                                                <asp:Button ID="btnReset" runat="server" BackColor="#3b3535" Font-Names="Verdana"
                                                    Font-Size="13px" ForeColor="White" OnClick="btnReset_Click" Style="border: 1px solid black"
                                                    Text="Reset" Width="80px" />
                                            </td>
                                            <td align="center" style="border-left: none; width: 60px; padding-right: 4px;">
                                                <asp:Button ID="BtnAddNewProduct" runat="server" Text="Add New" Font-Names="Verdana"
                                                    Font-Size="13px" OnClick="BtnAddNewProduct_Click" BackColor="#3b3535" ForeColor="White"
                                                    Width="80px" Style="border: 1px solid black" />
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
                                                <td colspan="2">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100%" colspan="3">
                                                    <asp:Panel ID="pnl" ScrollBars="Both" runat="server" Width="1020px" Style="border-color: Green;
                                                        border-style: solid; border-width: 1px">
                                                        <asp:Label ID="lblMessage" runat="server" Text="" Font-Names="Verdana" Font-Size="11px"
                                                            ForeColor="Red"></asp:Label>
                                                        <asp:GridView ID="dgvPatientList" runat="server" CellPadding="4" ForeColor="#333333"
                                                            BorderColor="Green" BorderStyle="Solid" BorderWidth="1px" GridLines="Both" Font-Names="Verdana"
                                                            Width="100%" Font-Size="Small" AllowPaging="true" PageSize="20" AutoGenerateColumns="false"
                                                            DataKeyNames="AdmitId" OnPageIndexChanged="dgvPatientList_PageIndexChanged" OnPageIndexChanging="dgvPatientList_PageIndexChanging">
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
                                                                <asp:BoundField DataField="FullName" HeaderText="PatientName" ReadOnly="True" SortExpression="FullName" />
                                                                <asp:BoundField DataField="AdmitDate" HeaderText="Visit Date" ReadOnly="True" SortExpression="AdmitDate"
                                                                    DataFormatString="{0:MM/dd/yyyy}" />
                                                                <asp:BoundField DataField="Age" HeaderText="Age" ReadOnly="True" SortExpression="Age" />
                                                                <asp:BoundField DataField="AgeIn" HeaderText="Y/M/D" ReadOnly="True" SortExpression="AgeIn" />
                                                                <asp:BoundField DataField="CompanyName" HeaderText="Company" ReadOnly="True" SortExpression="CompanyName" />
                                                                <asp:BoundField DataField="InsuranceDesc" HeaderText="Insurance" ReadOnly="True"
                                                                    SortExpression="InsuranceDesc" />
                                                                <asp:BoundField DataField="PatientType" HeaderText="Type" ReadOnly="true" SortExpression="PatientType" />
                                                                <asp:BoundField DataField="Dignosys" HeaderText="Dignosys" ReadOnly="true" SortExpression="Dignosys" />
                                                                <asp:BoundField DataField="CategoryName" HeaderText="Dept." ReadOnly="True" SortExpression="CategoryName" />
                                                                <asp:BoundField DataField="EmpName" HeaderText="Doctor" ReadOnly="true" SortExpression="EmpName" />
                                                                <asp:BoundField DataField="IPDNo" HeaderText="IPD No" ReadOnly="true" SortExpression="IPDNo"
                                                                    Visible="true" />
                                                                <asp:BoundField DataField="OPDNo" HeaderText="OPD No" ReadOnly="true" SortExpression="OPDNo"
                                                                    Visible="true" />
                                                                <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="ImageEdit" runat="server" ImageUrl="~/images/edit.jpg" Height="24px"
                                                                            Width="24px" OnClick="btnUpdate_Click" /></ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Shift to IPD" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="ImageShift" runat="server" ImageUrl="~/images/edit.jpg" Height="24px"
                                                                            Width="24px" OnClick="ImageShift_Click" /></ItemTemplate>
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
                    <asp:View ID="View2" runat="server">
                        <table width="100%">
                            <tr style="width: 100%;">
                                <td width="100%">
                                    <asp:Panel ID="pnlGrid" runat="server" Width="1020px" Style="text-align: center;
                                        background-color: #E0F0E8; height: 460px;">
                                        <table width="100%" style="border-color: Green; border-style: solid; border-width: 2px"
                                            cellpadding="0">
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="Panel2" runat="server" Width="99.7%" Style="text-align: center; background-color: #E0F0E8;
                                                        height: 450px;" BorderColor="Green" BorderStyle="Solid" BorderWidth="1px">
                                                        <table width="100%">
                                                            <tr>
                                                                <td align="center" colspan="4">
                                                                    <asp:Label ID="lblHeading" runat="server" Text="Registered Patient" Font-Names="Verdana"
                                                                        Font-Size="15px" Font-Bold="True" ForeColor="#3B3535"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4">
                                                                    <asp:Label Text="" ID="lblMsg" ForeColor="Red" Font-Names="Verdana" Font-Size="11px"
                                                                        runat="server" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 30%;" align="right">
                                                                    <asp:Label ID="Label7" runat="server" Text="Department :" Font-Names="Verdana" Font-Size="11px"
                                                                        ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td align="left" style="width: 20%;">
                                                                    <asp:DropDownList ID="ddlDeptCategory" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                        Width="160px" AutoPostBack="true" OnSelectedIndexChanged="ddlDeptCategory_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ForeColor="Red"
                                                                        ControlToValidate="ddlDeptCategory" Font-Size="11" ValidationGroup="Save" ErrorMessage="*"
                                                                        Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                </td>
                                                                <td style="width: 12%;" align="right">
                                                                    <asp:Label ID="Label5" runat="server" Text="Incharge Doctor :" Font-Names="Verdana"
                                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td style="width: 38%;" align="left">
                                                                    <asp:DropDownList ID="ddlDeptDoctor" runat="server" Font-Names="Verdana" Font-Size="11px">
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ForeColor="Red"
                                                                        ControlToValidate="ddlDeptDoctor" Font-Size="11" ValidationGroup="Save" ErrorMessage="*"
                                                                        Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="lblProductId" runat="server" Text="Patient Name :" Font-Names="Verdana"
                                                                        Font-Size="11px" ForeColor="#3B3535"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtNew" runat="server" Width="248px" AutoPostBack="true" OnTextChanged="txtNew_TextChanged"></asp:TextBox>
                                                                    <cc:TextBoxWatermarkExtender ID="txtNewWatermark" runat="server" TargetControlID="txtNew"
                                                                        WatermarkText="Type First Name For Search" WatermarkCssClass="watermarked" />
                                                                    <asp:DropDownList ID="ddlPatientName" runat="server" Height="18px" Width="248px"
                                                                        AutoPostBack="true" OnSelectedIndexChanged="ddlPatientName_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red"
                                                                        ControlToValidate="ddlPatientName" Font-Size="13" ValidationGroup="Save" ErrorMessage="*"
                                                                        Display="Dynamic" InitialValue="0"></asp:RequiredFieldValidator>
                                                                </td>
                                                                <td align="right">
                                                                    <asp:Label ID="lblProductName" runat="server" Text="Registration Date :" Font-Names="Verdana"
                                                                        Font-Size="11px" ForeColor="#3B3535"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtAdmitDate" runat="server" MaxLength="100" Font-Names="Verdana"
                                                                        Font-Size="11px" Width="129px"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvProductName" runat="server" ForeColor="Red" ControlToValidate="txtAdmitDate"
                                                                        Font-Size="13" ValidationGroup="Save" ErrorMessage="*" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                    <cc:CalendarExtender ID="calAdmitDate" runat="server" TargetControlID="txtAdmitDate"
                                                                        Format="MM/dd/yyyy">
                                                                    </cc:CalendarExtender>
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
                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtAdmitDate"
                                                                        Display="Dynamic" ErrorMessage="Please Enter Only Date With MM/DD/YYYY Format"
                                                                        Font-Bold="False" Font-Names="verdana" Font-Size="11px" ForeColor="Red" ValidationExpression="^([0]\d|[1][0-2])\/([0-2]\d|[3][0-1])\/([2][01]|[1][6-9])\d{2}(\s([0-1]\d|[2][0-3])(\:[0-5]\d){1,2})?$"
                                                                        ValidationGroup="Save">
                                                                    </asp:RegularExpressionValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="Labe23" runat="server" Text="Admission Time :" Font-Names="Verdana"
                                                                        Font-Size="11px" ForeColor="#3B3535"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <cc1:TimeSelector ID="AdmissionTimeSelector" runat="server" DisplaySeconds="false"
                                                                        SelectedTimeFormat="Twelve">
                                                                    </cc1:TimeSelector>
                                                                </td>
                                                                <td align="right">
                                                                    <asp:Label ID="lblUOM" runat="server" Text="Patient Type :" Font-Names="Verdana"
                                                                        Font-Size="11px" ForeColor="#3B3535"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:RadioButton ID="rbtnOPD" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                        ForeColor="#3b3535" GroupName="PatientType" Text="OPD" AutoPostBack="true" OnCheckedChanged="OPD_CheckedChanged" />
                                                                    <asp:RadioButton ID="rbtnIPD" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                        ForeColor="#3b3535" GroupName="PatientType" Text="IPD" AutoPostBack="true" OnCheckedChanged="IPD_CheckedChanged" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="lblIPDNo" runat="server" Text="IPD No :" Font-Names="Verdana" Font-Size="11px"
                                                                        ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtIPDNo" runat="server" Font-Names="Verdana" Font-Size="11px" Width="80px"
                                                                        Font-Bold="true"></asp:TextBox>
                                                                </td>
                                                                <td align="right">
                                                                    <asp:Label ID="lblDiagnosis" runat="server" Text="Diagnosis :" Font-Names="Verdana"
                                                                        Font-Size="11px" ForeColor="#3B3535"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtDignosys" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                        Width="129px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="Label4" runat="server" Text="D O B :" Font-Names="Verdana" Font-Size="11px"
                                                                        ForeColor="#3B3535"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:Label ID="lblDOB" runat="server" Text="" Font-Names="Verdana" Font-Size="11px"
                                                                        ForeColor="#3B3535"></asp:Label>
                                                                </td>
                                                                <td align="right">
                                                                    <asp:Label ID="Label3" runat="server" Text="Age :" Font-Names="Verdana" Font-Size="11px"
                                                                        ForeColor="#3B3535"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtAge" runat="server" MaxLength="50" Font-Names="Verdana" Font-Size="11px"
                                                                        Width="60px"></asp:TextBox>
                                                                    <asp:DropDownList ID="ddlAge" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                        Width="120px">
                                                                        <asp:ListItem Selected="True" Text="--Select --" Value="0"></asp:ListItem>
                                                                        <asp:ListItem Text="Years" Value="Years"></asp:ListItem>
                                                                        <asp:ListItem Text="Months" Value="Months"></asp:ListItem>
                                                                        <asp:ListItem Text="Days" Value="Days"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="Label2" runat="server" Text="Weight :" Font-Names="Verdana" Font-Size="11px"
                                                                        ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td align="left" colspan="3">
                                                                    <asp:TextBox ID="txtWeight" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                        Width="120px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6" align="left" style="border-top-style: solid; border-top-color: Green;
                                                                    border-top-width: 1px; border-bottom-color: Green; border-bottom-style: solid;
                                                                    border-bottom-width: 1px">
                                                                    <asp:Label ID="Label18" runat="server" Text="Company Detail" Font-Names="Verdana"
                                                                        Font-Size="11px" Font-Bold="true" ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6" align="left">
                                                                    <asp:CheckBox ID="chkCom" runat="server" Text="Company Patient" Font-Names="Verdana"
                                                                        Checked="false" Font-Size="11px" ForeColor="#3b3535" AutoPostBack="true" OnCheckedChanged="chkCom_CheckedChanged" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="lblName" runat="server" Text="Company Name :" Font-Names="Verdana"
                                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td align="left" colspan="5">
                                                                    <asp:DropDownList ID="ddlCompName" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                        Width="250px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6" align="left" style="border-top-style: solid; border-top-color: Green;
                                                                    border-top-width: 1px; border-bottom-color: Green; border-bottom-style: solid;
                                                                    border-bottom-width: 1px">
                                                                    <asp:Label ID="Label24" runat="server" Text="Insurance Detail" Font-Names="Verdana"
                                                                        Font-Size="11px" Font-Bold="true" ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" colspan="6">
                                                                    <asp:CheckBox ID="ChkInsurance" runat="server" Text="Insurance Patient" Font-Names="Verdana"
                                                                        Font-Size="11px" ForeColor="#3b3535" OnCheckedChanged="ChkInsurance_CheckedChanged"
                                                                        Checked="false" AutoPostBack="true" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="Label25" runat="server" Text="Insurance Company :" Font-Names="Verdana"
                                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td align="left" colspan="5">
                                                                    <asp:DropDownList ID="ddlInsurance" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                        Width="246px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4">
                                                                </td>
                                                            </tr>
                                                            <tr align="center">
                                                                <td align="center" colspan="4">
                                                                    <asp:Button ID="BtnSave" runat="server" Text="Save" Font-Names="Verdana" Font-Size="12px"
                                                                        BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black;"
                                                                        onmouseover="SetBtnMouseOver(this)" onmouseout="SetBtnMouseOut(this)" OnClick="BtnSave_Click" />
                                                                    <asp:Button ID="btnUpdate" runat="server" Text="Update" Font-Names="Verdana" Font-Size="12px"
                                                                        OnClick="BtnEdit_Click" ValidationGroup="Update" BackColor="#3b3535" ForeColor="White"
                                                                        Width="80px" Style="border: 1px solid black" />
                                                                    <asp:Button ID="BtnClose" runat="server" Text="Close" Font-Names="Verdana" Font-Size="12px"
                                                                        BackColor="#3b3535" ForeColor="White" OnClick="BtnClose_Click" Width="80px" Style="border: 1px solid black" />
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
</asp:Content>
