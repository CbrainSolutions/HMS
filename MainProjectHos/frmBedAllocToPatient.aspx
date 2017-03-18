<%@ Page Title="" Language="C#" MasterPageFile="~/mstAdmin.Master" AutoEventWireup="true" CodeBehind="frmBedAllocToPatient.aspx.cs" Inherits="MainProjectHos.frmBedAllocToPatient" %>
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="width: 99%">
                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="View1" runat="server">
                        <table width="100%">
                            <tr>
                                <td align="center">
                                    <asp:Label ID="Label1" runat="server" Text="Bed Allocation To Patient" Font-Names="Verdana"
                                        Font-Size="16px" Font-Bold="true" ForeColor="#3b3535"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="width: 15%;">
                                    <table border="2px" cellpadding="0" cellspacing="0" width="780px" style="height: 40px;">
                                        <tr>
                                            <td align="center" style="border-right: none; width: 575px;">
                                              <asp:TextBox ID="txtSearch" runat="server" Font-Bold="true" 
                                                    Font-Names="Verdana" Font-Size="13px" Width="580px" />
                                            </td>
                                            <td align="center" style="border-left: none; border-right: none; width: 60px;">
                                                <asp:Button ID="btnSearch" runat="server" BackColor="#3b3535" 
                                                    Font-Names="Verdana" Font-Size="13px" ForeColor="White" 
                                                    OnClick="btnSearch_Click" Style="border: 1px solid black" Text="Search" 
                                                    Width="80px" />
                                            </td>
                                            <td align="center" style="border-left: none; border-right: none; width: 90px;">
                                                 <asp:Button ID="btnReset" runat="server" BackColor="#3b3535" 
                                                    Font-Names="Verdana" Font-Size="13px" ForeColor="White" 
                                                    OnClick="btnReset_Click" Style="border: 1px solid black" Text="Reset" 
                                                    Width="80px" />
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
                                                        <asp:GridView ID="dgvDepartment" runat="server" CellPadding="4" ForeColor="#333333"
                                                            DataKeyNames="BedId" GridLines="Both" Font-Names="Verdana" Width="100%" Font-Size="Small"
                                                            AllowPaging="true" PageSize="50" AutoGenerateColumns="false"
                                                            OnPageIndexChanged="dgvDepartment_PageIndexChanged"
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
                                                                <asp:BoundField DataField="BedId" HeaderText="Bed Id" ReadOnly="True" SortExpression="BedId" />
                                                                <asp:BoundField DataField="BedNo" HeaderText="Bed Number" ReadOnly="True" SortExpression="BedNo" />
                                                                <asp:BoundField DataField="RoomNo" HeaderText="Room Number" ReadOnly="True" SortExpression="RoomNo" />
                                                                <asp:BoundField DataField="FloorName" HeaderText="Floor Name" ReadOnly="True" SortExpression="FloorName" />
                                                                <asp:BoundField DataField="FullName" HeaderText="Patient Name" ReadOnly="true" SortExpression="FullName" />
                                                                <asp:BoundField DataField="AllocationDate" HeaderText="Allocation Date" ReadOnly="true"
                                                                    SortExpression="AllocationDate" />
                                                                <asp:BoundField DataField="DischargeDate" HeaderText="Discharge Date" ReadOnly="true"
                                                                    SortExpression="DischargeDate" />
                                                                <asp:TemplateField HeaderText="Allocate" ItemStyle-HorizontalAlign="Left"><ItemTemplate><asp:ImageButton ID="ImageEdit" runat="server" ImageUrl="~/images/edit.jpg" Height="24px"
                                                                            Width="24px" OnClick="btnUpdate_Click" /></ItemTemplate><ItemStyle HorizontalAlign="Left" /></asp:TemplateField>
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
                                        height: 190px;" BorderColor="Green" BorderStyle="Solid" BorderWidth="1px">
                                        <table width="100%">
                                            <tr style="width: 100%;">
                                                <td align="center" colspan="4">
                                                    <asp:Label ID="lblHeading" runat="server" Text="Bed Allocation To Patient" Font-Names="Verdana"
                                                        Font-Size="15px" Font-Bold="true" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                            </tr>
                                             <tr>
                                                <td colspan="4" class="style1">
                                                    <asp:Label ID="lblMsg" runat="server" Text="" Font-Names="Verdana"
                                                        Font-Size="11px"  ForeColor="Red"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" class="style1">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr style="width: 100%;">
                                                <td align="right" style="width: 25%;">
                                                    <asp:Label ID="lblFloorname" runat="server" Text="Floor Name :" Font-Names="Verdana"
                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left" style="width: 10%;">
                                                    <asp:TextBox ID="txtFloorName" runat="server" MaxLength="50" Font-Names="Verdana"
                                                        Font-Size="11px" Width="150px" ReadOnly="True"></asp:TextBox>
                                                </td>
                                                <td align="right" style="width: 10%;">
                                                    <asp:Label ID="lblRoomNo" runat="server" Text="Room Number :" Font-Names="Verdana"
                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left" style="width: 25%;">
                                                    <asp:TextBox ID="txtRoomNo" runat="server" MaxLength="50" Font-Names="Verdana" Font-Size="11px"
                                                        Width="150px" ReadOnly="True"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="lblBedNo" runat="server" Text="Bed Number :" Font-Names="Verdana"
                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtBedNo" runat="server" MaxLength="100" Font-Names="Verdana" Font-Size="11px"
                                                        Width="150px" Font-Bold="False" ReadOnly="True"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="lblPatientName" runat="server" Text="Patient Name :" Font-Names="Verdana"
                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:DropDownList ID="ddlPatientName" runat="server" Width="150px" 
                                                        OnSelectedIndexChanged="ddlPatientName_SelectedIndexChanged1" 
                                                        AutoPostBack="True">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvDeptDesc" runat="server" ForeColor="Red" ControlToValidate="ddlPatientName"
                                                        Font-Size="13" ValidationGroup="Save" ErrorMessage="*" InitialValue="0" Display="Dynamic"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="lblAllocationDate" runat="server" Text="Allocation Date :" Font-Names="Verdana"
                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtAllocDate" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                        Width="150px" MaxLength="10" Format="MM/dd/yyyy hh:mm:ss tt"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="revTxtFirstName" ValidationExpression="^([0]\d|[1][0-2])\/([0-2]\d|[3][0-1])\/([2][01]|[1][6-9])\d{2}(\s([0-1]\d|[2][0-3])(\:[0-5]\d){1,2})?$"
                                                        ErrorMessage="Please Enter Only Date With MM/DD/YYYY Format" Font-Bold="False"
                                                        Font-Size="11px" ForeColor="Red" ControlToValidate="txtAllocDate" runat="server"
                                                        ValidationGroup="Save" Display="Dynamic" Font-Names="verdana" />
                                                    <asp:Calendar ID="CalDOBDate" runat="server" TargetControlID="txtAllocDate"
                                                        Format="MM/dd/yyyy" DaysModeTitleFormat="MM/dd/yyyy" TodaysDateFormat="MM/dd/yyyy"></asp:Calendar>
                                                </td>
                                                
                                            </tr>
                                            <tr style="height: 10px;">
                                                <td colspan="4">
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td align="center" colspan="4">
                                                    <asp:Button ID="BtnSave" runat="server" Text="Save" Font-Names="Verdana" Font-Size="12px"
                                                        ValidationGroup="Save" BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black;
                                                        height: 20px;" OnClick="BtnSave_Click" />
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
