<%@ Page Title="" Language="C#" MasterPageFile="~/mstAdmin.Master" AutoEventWireup="true" CodeBehind="frmMakeDischarge.aspx.cs" Inherits="MainProjectHos.frmMakeDischarge" %>
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
        .style1
        {
            height: 38px;
        }
    </style>
    <%--<script type="text/javascript" language="javascript">
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
    </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="width: 99%">
                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="View1" runat="server">
                        <center>
                            <asp:Label ID="Label1" runat="server" Text="Discharge" Font-Names="Verdana" Font-Size="16px"
                                Font-Bold="true" ForeColor="#3b3535"></asp:Label>
                        </center>
                        <div style="float: left; width: 99%;">
                            <table width="100%">
                                <tr style="height: 15px;">
                                    <td colspan="4">
                                    </td>
                                </tr>
                                <tr style="height: 15px;">
                                    <td colspan="4">
                                        <asp:Label Text="" ID="lblMessage" ForeColor="Red" Font-Names="Verdana" Font-Size="11px"
                                            runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 25%;">
                                        <asp:Label ID="lblShift" runat="server" Text="Patient Name :" Font-Names="Verdana"
                                            Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                    </td>
                                    <td align="left" style="width: 15%;">
                                        <asp:DropDownList ID="ddlPatient" runat="server" Width="245px" OnSelectedIndexChanged="ddlPatient_SelectedIndexChanged"
                                            AutoPostBack="True">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red"
                                            ControlToValidate="ddlPatient" Font-Size="13" ValidationGroup="Save" ErrorMessage="*"
                                            Display="Dynamic" InitialValue="0"></asp:RequiredFieldValidator>
                                    </td>
                                    <td align="right" style="width: 15%;">
                                        <asp:Label Text="Type Of Discharge : " ID="lblTypeOfDischarge" ForeColor="#3b3535"
                                            Font-Names="Verdana" Font-Size="11px" runat="server" />
                                    </td>
                                    <td align="left" style="width: 35%;">
                                        <asp:DropDownList ID="ddlDischargeType" runat="server" Font-Names="Verdana" Font-Size="11px" 
                                            OnSelectedIndexChanged="ddlDischargeType_SelectedIndexChanged" AutoPostBack="True" Width="245px">
                                            <asp:ListItem Selected="True" Text="--Select --" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Routine" Value="Routine"></asp:ListItem>
                                            <asp:ListItem Text="Dama" Value="Dama"></asp:ListItem>
                                            <asp:ListItem Text="Transfer" Value="Transfer"></asp:ListItem>
                                            <asp:ListItem Text="Death" Value="Death"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ForeColor="Red"
                                            ControlToValidate="ddlDischargeType" Font-Size="13" ValidationGroup="Save"
                                            ErrorMessage="*" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 25%;">
                                        <asp:Label ID="Label14" runat="server" Text="Discharge Date :" Font-Names="Verdana"
                                            Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                    </td>
                                    <td align="left" style="width: 15%;">
                                        <asp:TextBox runat="server" ID="txtDischargeDate" Font-Size="11px" Width="245px"
                                            Font-Names="Verdana" />
                                        <cc:CalendarExtender ID="CalDOBDate" runat="server" TargetControlID="txtDischargeDate"
                                            Format="MM/dd/yyyy" DaysModeTitleFormat="MM/dd/yyyy" TodaysDateFormat="MM/dd/yyyy">
                                        </cc:CalendarExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ForeColor="Red"
                                            ControlToValidate="txtDischargeDate" Font-Size="13" ValidationGroup="Save" ErrorMessage="*"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                    <td align="right" style="width: 15%;">
                                        <asp:Label Text="Time Discharge : " ID="Label15" ForeColor="#3b3535" Font-Names="Verdana"
                                            Font-Size="11px" runat="server" />
                                    </td>
                                    <td align="left" style="width: 35%;">
                                        <cc1:TimeSelector ID="AdmissionTimeSelector" runat="server" DisplaySeconds="false"
                                            SelectedTimeFormat="TwentyFour">
                                        </cc1:TimeSelector>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 25%;">
                                    </td>
                                    <td colspan="2" align="left" style="width: 50%">
                                        <asp:RegularExpressionValidator ID="revTxtFirstName" ValidationExpression="^([0]\d|[1][0-2])\/([0-2]\d|[3][0-1])\/([2][01]|[1][6-9])\d{2}(\s([0-1]\d|[2][0-3])(\:[0-5]\d){1,2})?$"
                                            ErrorMessage="Please Enter Only Date With MM/DD/YYYY Format" Font-Bold="False"
                                            Font-Size="11px" ForeColor="Red" ControlToValidate="txtDischargeDate" runat="server"
                                            ValidationGroup="Save" Display="Dynamic" Font-Names="verdana" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label Text="Diagnosis : " ID="lblDiagnosis" ForeColor="#3b3535" Font-Names="Verdana"
                                            Font-Size="11px" runat="server" />
                                    </td>
                                    <td align="left">
                                        <asp:TextBox runat="server" ID="txtDiagnosis" Font-Size="11px" Width="250px" Font-Names="Verdana"
                                            Style="text-transform: capitalize;" />
                                    </td>
                                    <td align="right">
                                        <asp:Label Text="Name Of Surgery : " ID="lblNameOfSurgery" ForeColor="#3b3535" Font-Names="Verdana"
                                            Font-Size="11px" runat="server" />
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlNameOfSurgery" runat="server" Width="250" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlNameOfSurgery_IndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label Text="Complaints & History : " ID="llblHistory" ForeColor="#3b3535" Font-Names="Verdana"
                                            Font-Size="11px" runat="server" />
                                    </td>
                                    <td align="left">
                                        <asp:TextBox runat="server" ID="txtHistory" Font-Size="11px" Width="250px" Font-Names="Verdana"
                                            Style="text-transform: capitalize;" TextMode="MultiLine" Height="50px" />
                                    </td>
                                    <td align="right">
                                        <asp:Label Text="Operational Procedure : " ID="lblOperationalProc" ForeColor="#3b3535"
                                            Font-Names="Verdana" Font-Size="11px" runat="server" />
                                    </td>
                                    <td align="left">
                                        <asp:TextBox runat="server" ID="txtOperatonalProce" Font-Size="11px" Width="250px"
                                            Font-Names="Verdana" Style="text-transform: capitalize;" TextMode="MultiLine"
                                            Height="50px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label Text="Vital Parameters :" Style="text-decoration: underline;" ID="Label16"
                                            ForeColor="#3b3535" Font-Bold="true" Font-Names="Verdana" Font-Size="14px" runat="server" />
                                    </td>
                                    <td colspan="3">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label Text="* Temp : " ID="lblTemp" ForeColor="#3b3535" Font-Names="Verdana"
                                            Font-Size="11px" runat="server" />
                                    </td>
                                    <td align="left">
                                        <asp:TextBox runat="server" ID="txtTemp" Font-Size="11px" Width="75px" Font-Names="Verdana" />
                                        <asp:Label Text="ºF. * Pulse :" ID="lblPulse" Style="display: inline;" Width="35px"
                                            ForeColor="#3b3535" Font-Names="Verdana" Font-Size="11px" runat="server" />
                                        <asp:TextBox runat="server" ID="txtPulse" Font-Size="11px" Width="75px" Font-Names="Verdana" />
                                        <asp:Label Text="/min." ID="Label17" Width="15px" ForeColor="#3b3535" Font-Names="Verdana"
                                            Font-Size="11px" runat="server" />
                                    </td>
                                    <td align="right">
                                        <asp:Label Text="* B.P.: " ID="lblBP" ForeColor="#3b3535" Font-Names="Verdana" Font-Size="11px"
                                            runat="server" />
                                    </td>
                                    <td align="left">
                                        <asp:TextBox runat="server" ID="txtBP" Font-Size="11px" Width="50px" Font-Names="Verdana" />
                                        <asp:Label Text="mm of Hg. * Resp.Rate:" ID="lblRespRate" Style="display: inline;"
                                            Width="35px" ForeColor="#3b3535" Font-Names="Verdana" Font-Size="11px" runat="server" />
                                        <asp:TextBox runat="server" ID="txtRespRate" Font-Size="11px" Width="50px" Font-Names="Verdana" />
                                        <asp:Label Text="/min." ID="Label21" Width="15px" ForeColor="#3b3535" Font-Names="Verdana"
                                            Font-Size="11px" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label Text="General Examination :" Style="text-decoration: underline;" ID="Label22"
                                            ForeColor="#3b3535" Font-Bold="true" Font-Names="Verdana" Font-Size="14px" runat="server" />
                                    </td>
                                    <td colspan="3">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label Text="Pallor : " ID="lblPallor" ForeColor="#3b3535" Font-Names="Verdana"
                                            Font-Size="11px" runat="server" />
                                    </td>
                                    <td align="left">
                                        <asp:TextBox runat="server" ID="txtPallor" Font-Size="11px" Width="75px" Font-Names="Verdana" />
                                        <asp:Label Text=" * Oedema  :" ID="lblOedema" Style="display: inline;" Width="15px"
                                            ForeColor="#3b3535" Font-Names="Verdana" Font-Size="11px" runat="server" />
                                        <asp:TextBox runat="server" ID="txtOedema" Font-Size="11px" Width="85px" Font-Names="Verdana" />
                                    </td>
                                    <td align="right">
                                        <asp:Label Text="Cyanosis : " ID="lblCyanosis" ForeColor="#3b3535" Font-Names="Verdana"
                                            Font-Size="11px" runat="server" />
                                    </td>
                                    <td align="left">
                                        <asp:TextBox runat="server" ID="txtCyanosis" Font-Size="11px" Width="250px" Font-Names="Verdana" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label Text="Clubbing : " ID="lblClubbing" ForeColor="#3b3535" Font-Names="Verdana"
                                            Font-Size="11px" runat="server" />
                                    </td>
                                    <td align="left">
                                        <asp:TextBox runat="server" ID="txtClubbing" Font-Size="11px" Width="75px" Font-Names="Verdana" />
                                        <asp:Label Text=" * Icterus  :" ID="lblIcterus" Style="display: inline;" Width="15px"
                                            ForeColor="#3b3535" Font-Names="Verdana" Font-Size="11px" runat="server" />
                                        <asp:TextBox Style="margin-left: 8px;" runat="server" ID="txtIcterus" Font-Size="11px"
                                            Width="85px" Font-Names="Verdana" />
                                    </td>
                                    <td align="right">
                                        <asp:Label Text="Skin : " ID="lblSkin" ForeColor="#3b3535" Font-Names="Verdana" Font-Size="11px"
                                            runat="server" />
                                    </td>
                                    <td align="left">
                                        <asp:TextBox runat="server" ID="txtSkin" Font-Size="11px" Width="250px" Font-Names="Verdana" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label Text="Systemic Examination :" Style="text-decoration: underline;" ID="Label20"
                                            ForeColor="#3b3535" Font-Bold="true" Font-Names="Verdana" Font-Size="14px" runat="server" />
                                    </td>
                                    <td colspan="3">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label Text="Resp. System : " ID="lblRespSystem" ForeColor="#3b3535" Font-Names="Verdana"
                                            Font-Size="11px" runat="server" />
                                    </td>
                                    <td align="left">
                                        <asp:TextBox runat="server" ID="txtRespSystem" Font-Size="11px" Width="250px" Font-Names="Verdana" />
                                    </td>
                                    <td align="right">
                                        <asp:Label Text="CNS : " ID="lblCNS" ForeColor="#3b3535" Font-Names="Verdana" Font-Size="11px"
                                            runat="server" />
                                    </td>
                                    <td align="left">
                                        <asp:TextBox runat="server" ID="txtCNS" Font-Size="11px" Width="250px" Font-Names="Verdana" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label Text="Per Abd. : " ID="lblPerAbd" ForeColor="#3b3535" Font-Names="Verdana"
                                            Font-Size="11px" runat="server" />
                                    </td>
                                    <td align="left">
                                        <asp:TextBox runat="server" ID="txtPerAbd" Font-Size="11px" Width="250px" Font-Names="Verdana" />
                                    </td>
                                    <td align="right">
                                        <asp:Label Text="CVS : " ID="lblCVS" ForeColor="#3b3535" Font-Names="Verdana" Font-Size="11px"
                                            runat="server" />
                                    </td>
                                    <td align="left">
                                        <asp:TextBox runat="server" ID="txtCVS" Font-Size="11px" Width="250px" Font-Names="Verdana" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label Text="Investigations :" Style="text-decoration: underline;" ID="Label25"
                                            ForeColor="#3b3535" Font-Bold="true" Font-Names="Verdana" Font-Size="14px" runat="server" />
                                    </td>
                                    <td colspan="3">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label Text="Haemogram : " ID="lblHaemogram" ForeColor="#3b3535" Font-Names="Verdana"
                                            Font-Size="11px" runat="server" />
                                    </td>
                                    <td align="left">
                                        <asp:TextBox runat="server" ID="txtHaemogram" Font-Size="11px" Width="250px" Font-Names="Verdana" />
                                    </td>
                                    <td align="right">
                                        <asp:Label Text="Urine R : " ID="lblUrineR" ForeColor="#3b3535" Font-Names="Verdana"
                                            Font-Size="11px" runat="server" />
                                    </td>
                                    <td align="left">
                                        <asp:TextBox runat="server" ID="txtUrineR" Font-Size="11px" Width="250px" Font-Names="Verdana" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label Text="B.S.L. : " ID="lblBSL" ForeColor="#3b3535" Font-Names="Verdana"
                                            Font-Size="11px" runat="server" />
                                    </td>
                                    <td align="left">
                                        <asp:TextBox runat="server" ID="txtBSL" Font-Size="11px" Width="250px" Font-Names="Verdana" />
                                    </td>
                                    <td align="right">
                                        <asp:Label Text="B.U. L. : " ID="lblBUL" ForeColor="#3b3535" Font-Names="Verdana"
                                            Font-Size="11px" runat="server" />
                                    </td>
                                    <td align="left">
                                        <asp:TextBox runat="server" ID="txtBUL" Font-Size="11px" Width="250px" Font-Names="Verdana" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label Text="S. Creat : " ID="lblSCreat" ForeColor="#3b3535" Font-Names="Verdana"
                                            Font-Size="11px" runat="server" />
                                    </td>
                                    <td align="left">
                                        <asp:TextBox runat="server" ID="txtSCreat" Font-Size="11px" Width="250px" Font-Names="Verdana" />
                                    </td>
                                    <td align="right">
                                        <asp:Label Text="S.Elect : " ID="lblSElect" ForeColor="#3b3535" Font-Names="Verdana"
                                            Font-Size="11px" runat="server" />
                                    </td>
                                    <td align="left">
                                        <asp:TextBox runat="server" ID="txtSElect" Font-Size="11px" Width="250px" Font-Names="Verdana" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label Text="X-Ray : " ID="lblXray" ForeColor="#3b3535" Font-Names="Verdana"
                                            Font-Size="11px" runat="server" />
                                    </td>
                                    <td align="left">
                                        <asp:TextBox runat="server" ID="txtXRay" Font-Size="11px" Width="250px" Font-Names="Verdana" />
                                    </td>
                                    <td align="right">
                                        <asp:Label Text="E.C.G. : " ID="lblECG" ForeColor="#3b3535" Font-Names="Verdana"
                                            Font-Size="11px" runat="server" />
                                    </td>
                                    <td align="left">
                                        <asp:TextBox runat="server" ID="txtECG" Font-Size="11px" Width="250px" Font-Names="Verdana" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label Text="Treatment Given In Hospital : " Style="text-decoration: underline;"
                                            ID="lblHospitalisation" ForeColor="#3b3535" Font-Names="Verdana" Font-Size="11px"
                                            Font-Bold="true" runat="server" />
                                    </td>
                                    <td align="left">
                                        <asp:TextBox runat="server" ID="txtTreatmentHospital" Font-Size="11px" Width="250px"
                                            Style="text-transform: capitalize;" Font-Names="Verdana" TextMode="MultiLine"
                                            Height="50px" />
                                    </td>
                                    <td align="right" class="style1">
                                        <asp:Label Text="Others : " ID="lblOthers" ForeColor="#3b3535" Font-Names="Verdana"
                                            Font-Size="11px" runat="server" />
                                    </td>
                                    <td align="left" class="style1">
                                        <asp:TextBox runat="server" ID="txtOthers" Font-Size="11px" Width="250px" Font-Names="Verdana"
                                            Style="text-transform: capitalize;" TextMode="MultiLine" Height="50px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label Text="Advice On Discharge : " ID="lblDischarge" ForeColor="#3b3535" Font-Names="Verdana"
                                            Font-Bold="true" Style="text-decoration: underline;" Font-Size="11px" runat="server" />
                                    </td>
                                    <td align="left">
                                        <asp:TextBox runat="server" ID="txtAdviceDischarge" Font-Size="11px" Width="250px"
                                            Font-Names="Verdana" Style="text-transform: capitalize;" TextMode="MultiLine"
                                            Height="50px" />
                                    </td>
                                    <td align="right">
                                        <asp:Label Text="Next FollowUp VisitOn: " ID="lblFollowUp" ForeColor="#3b3535" Font-Bold="true"
                                            Style="text-decoration: underline;" Font-Names="Verdana" Font-Size="11px" runat="server" />
                                    </td>
                                    <td align="left">
                                        <asp:TextBox runat="server" ID="txtFollowUp" Font-Size="11px" Width="250px" Font-Names="Verdana"
                                            TextMode="MultiLine" Height="50px" />
                                    </td>
                                </tr>
                                <tr style="height: 20px;">
                                    <td colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" align="center">
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
                        </div>
                    </asp:View>
                    <asp:View runat="server" ID="View2">
                        <div style="width: 100%; height: auto">
                            <table style="width: 1040px;">
                                <tr>
                                    <td align="center" colspan="4">
                                        <asp:Label Text="Discharge" ID="lblHeaderInvoice" Font-Names="Verdana" Font-Size="16px"
                                            Font-Bold="true" ForeColor="#3b3535" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="4">
                                        <table border="2px" cellpadding="0" cellspacing="0" width="860px" style="height: 40px;">
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
                                DataKeyNames="DichargeId" GridLines="Both" Font-Names="Verdana" Width="100%"
                                Font-Size="Small" AllowPaging="true" PageSize="20" AutoGenerateColumns="false"
                                OnDataBound="dgvTestInvoice_DataBound" OnPageIndexChanged="dgvTestInvoice_PageIndexChanged"
                                OnPageIndexChanging="dgvTestInvoice_PageIndexChanging" Style="font-family: System">
                                <FooterStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" />
                                <RowStyle BackColor="#E0F0E8" Font-Size="11px" Wrap="False" Font-Names="Verdana" />
                                <PagerStyle BackColor="#3b3535" ForeColor="White" HorizontalAlign="Left" Font-Size="11px" />
                                <SelectedRowStyle BackColor="#006600" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" Font-Size="12px"
                                    Wrap="False" Font-Names="verdana" />
                                <EditRowStyle BackColor="#2461BF" />
                                <AlternatingRowStyle BackColor="White" Wrap="False" />
                                <Columns>
                                    <asp:BoundField DataField="MRN" HeaderText="MRN" ReadOnly="True" SortExpression="MRN" />
                                    <asp:BoundField DataField="PatientId" HeaderText="Admit ID" ReadOnly="True" SortExpression="PatientId" />
                                    <asp:BoundField DataField="DischargeReceiptDate" HeaderText="Receipt Date" ReadOnly="True"
                                        SortExpression="DischargeReceiptDate" DataFormatString="{0:MM/dd/yyyy}" />
                                    <asp:BoundField DataField="PatName" HeaderText="Patient Name" ReadOnly="True" SortExpression="PatName" />
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
