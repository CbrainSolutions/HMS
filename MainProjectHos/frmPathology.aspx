<%@ Page Title="" Language="C#" MasterPageFile="~/mstAdmin.Master" AutoEventWireup="true" CodeBehind="frmPathology.aspx.cs" Inherits="MainProjectHos.frmPathology" %>
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="BtnimgUpload" />
        </Triggers>
        <ContentTemplate>
            <div style="width: 99%">
                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="View1" runat="server">
                        <table width="100%">
                            <tr>
                                <td align="center">
                                    <asp:Label ID="Label1" runat="server" Text="Pathology Lab" Font-Names="Verdana" Font-Size="16px"
                                        Font-Bold="True" ForeColor="#3B3535"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="width: 15%;">
                                    <table border="1px" cellpadding="0" cellspacing="0" width="860px" style="height: 40px;">
                                        <tr>
                                            <td align="center" style="border-right: none; width: 575px;">
                                                <asp:TextBox ID="txtSearch" Font-Bold="true" Font-Names="Verdana" Font-Size="13px"
                                                    Width="580px" runat="server" />
                                                
                                            </td>
                                            <td align="center" style="border-left: none; border-right: none; width: 60px;">
                                                <asp:Button ID="btnSearch" Text="Search" Font-Names="Verdana" Font-Size="13px" runat="server"
                                                    Width="80px" Style="border: 1px solid black" OnClick="btnSearch_Click" BackColor="#3b3535"
                                                    ForeColor="White" />
                                            </td>
                                            <td align="center" style="border-left: none; border-right: none; width: 90px;">
                                                <asp:Button ID="btnReset" runat="server" Text="Reset" Font-Names="Verdana" Font-Size="13px"
                                                    BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black"
                                                    OnClick="btnReset_Click" />
                                            </td>
                                            <td style="border-left: none; width: 60px; padding-right: 4px;">
                                                <asp:Button ID="BtnAddNewDept" runat="server" Text="Add New" Font-Names="Verdana"
                                                    Font-Size="13px" OnClick="BtnAddNewDept_Click" BackColor="#3b3535" ForeColor="White"
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
                                                <td colspan="3">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100%" colspan="3">
                                                    <asp:Label ID="lblMessage" runat="server" Text="" Font-Names="Verdana" Font-Size="11px"
                                                        ForeColor="Red"></asp:Label>
                                                    <asp:Panel ID="pnl" ScrollBars="Horizontal" runat="server" Width="1010px" BorderColor="Green"
                                                        BorderStyle="Solid" BorderWidth="1px">
                                                        <asp:GridView ID="dgvTestParameter" runat="server" CellPadding="4" ForeColor="#333333"
                                                            DataKeyNames="LabId" GridLines="Both" Font-Names="Verdana" Width="100%" Font-Size="Small"
                                                            AllowPaging="true" PageSize="20" AutoGenerateColumns="false" OnPageIndexChanged="dgvTestParameter_PageIndexChanged"
                                                            OnPageIndexChanging="dgvTestParameter_PageIndexChanging">
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
                                                                <asp:BoundField DataField="EmpName" HeaderText="Test Done By" ReadOnly="True" SortExpression="EmpName" />
                                                                <asp:BoundField DataField="TestDate" HeaderText="TestDate" ReadOnly="True" SortExpression="TestDate"
                                                                    DataFormatString="{0:MM/dd/yyyy}" />
                                                                <asp:BoundField DataField="FinalComment" HeaderText="Final Comment" ReadOnly="True"
                                                                    SortExpression="FinalComment" />
                                                                <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="ImageEdit" runat="server" ImageUrl="~/images/edit.jpg" Height="24px"
                                                                            Width="24px" OnClick="btnUpdate_Click" /></ItemTemplate>
                                                                </asp:TemplateField>
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
                        <table width="1050px" style="border-color: Green; border-style: solid; border-width: 2px"
                            cellpadding="0">
                            <tr style="width: 100%">
                                <td style="width: 100%">
                                    <asp:Panel ID="Panel2" runat="server" Width="100%" Style="text-align: center; background-color: #E0F0E8;
                                        height: 450px;" BorderColor="Green" BorderStyle="Solid" BorderWidth="1px">
                                        <table style="padding-left: 45%;">
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="lblHeading" runat="server" Text="Pathology Result" Font-Names="Verdana"
                                                        Font-Size="15px" Font-Bold="True" ForeColor="#3B3535"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:Label ID="lblMsg" runat="server" Text="" Font-Names="Verdana" Font-Size="11px"
                                                        ForeColor="Red"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                        <div style="float: left; position: relative; width: 50%;">
                                            <table style="margin-right: 2px;" align="right">
                                                <tr>
                                                    <td align="right">
                                                        <asp:Label ID="lblPatientName" Text="Patient Name :" runat="server" ForeColor="#3b3535" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <asp:Label ID="lblDate" Text="Test Type :" runat="server" ForeColor="#3B3535" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div style="float: right; position: relative; width: 50%;">
                                            <table style="margin-left: 2px;" align="left">
                                                <tr>
                                                    <td align="left">
                                                        <asp:DropDownList ID="ddlPatient" runat="server" Width="150px" AutoPostBack="True"
                                                            OnSelectedIndexChanged="ddlPatient_SelectedIndexChanged1">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red"
                                                            ControlToValidate="ddlPatient" Font-Size="13" ValidationGroup="Save" ErrorMessage="*"
                                                            Display="Dynamic" InitialValue="0"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <asp:DropDownList ID="ddlTest" runat="server" AutoPostBack="true" Width="150px" OnSelectedIndexChanged="ddlTest_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ForeColor="Red"
                                                            ControlToValidate="ddlTest" Font-Size="13" ValidationGroup="Save" ErrorMessage="*"
                                                            Display="Dynamic" InitialValue="0"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div style="float: left; position: relative; width: 100%; padding-right: 5; top: 0px;
                                            left: 0px; align: center;">
                                            <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" DataKeyNames="TestParaId"
                                                GridLines="Both" Font-Names="Verdana" Width="80%" Font-Size="Small" AutoGenerateColumns="false"
                                                OnRowDataBound="GridView1_RowDataBound">
                                                <FooterStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" />
                                                <RowStyle BackColor="#E0F0E8" Font-Size="11px" Wrap="False" />
                                                <PagerStyle BackColor="#3b3535" ForeColor="White" HorizontalAlign="Left" Font-Size="11px" />
                                                <SelectedRowStyle BackColor="#006600" Font-Bold="True" ForeColor="#333333" />
                                                <HeaderStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" Font-Size="12px"
                                                    Wrap="False" />
                                                <EditRowStyle BackColor="#2461BF" />
                                                <AlternatingRowStyle BackColor="White" Wrap="False" />
                                                <Columns>
                                                    <asp:BoundField DataField="TestParaName" HeaderText="Test Name" ReadOnly="True" SortExpression="TestParaName" />
                                                    <asp:BoundField DataField="Min" HeaderText="Min" ReadOnly="True" SortExpression="Min" />
                                                    <asp:BoundField DataField="Max" HeaderText="Max" ReadOnly="True" SortExpression="Max" />
                                                    <asp:TemplateField HeaderText="Result">
                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" ID="txtResult" OnTextChanged="txtResult_TextChanged" AutoPostBack="true">
                                                            </asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unit">
                                                        <ItemTemplate>
                                                            <asp:DropDownList runat="server" ID="ddlUnit" Width="100px">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Test Method">
                                                        <ItemTemplate>
                                                            <asp:DropDownList runat="server" ID="ddlTestMethod" Width="150px">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Comment">
                                                        <ItemTemplate>
                                                            <asp:DropDownList runat="server" ID="ddlComment" Width="150px">
                                                                <asp:ListItem Text="----Select----" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="High" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="Low" Value="2"></asp:ListItem>
                                                                <asp:ListItem Text="Normal" Value="3"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <table style="padding-left: 80;" align="center">
                                                <tr style="height: 15px;">
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label Text="Upload File : " ID="lblUpload" ForeColor="#3b3535" Font-Names="Verdana"
                                                            Font-Size="11px" runat="server" />
                                                    </td>
                                                    <td align="left">
                                                        <asp:FileUpload ID="imgUpload" runat="server" Font-Bold="true" Font-Names="Verdana"
                                                            Font-Size="11px" />
                                                        <asp:Button ID="BtnimgUpload" runat="server" Text="Upload Image" Font-Names="Verdana"
                                                            Font-Size="12px" BackColor="#3b3535" ForeColor="White" Width="100px" Style="border: 1px solid black"
                                                            OnClick="BtnimgUpload_Click" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblFinalComment" runat="server" Text="Final Comment : " Font-Names="Verdana"
                                                            Font-Size="11px" ForeColor="#3B3535"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtFinalComment" runat="server" Width="250px" Font-Names="Verdana"
                                                            Font-Size="11px"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ForeColor="Red"
                                                            ControlToValidate="txtFinalComment" Font-Size="13px" ValidationGroup="Save" ErrorMessage="*"
                                                            Display="Dynamic" InitialValue="0"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <br />
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                        <table width="100%">
                            <tr align="center">
                                <td align="center" colspan="2">
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
                    </asp:View>
                </asp:MultiView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <input id="hdnPanel" type="hidden" runat="server" value="none" />
</asp:Content>

