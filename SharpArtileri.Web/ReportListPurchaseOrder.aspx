<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MasterWorkspace.Master" AutoEventWireup="true" CodeBehind="ReportListPurchaseOrder.aspx.cs" Inherits="SharpArtileri.Web.ReportListPurchaseOrder" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 200px;
        }
        .auto-style2 {
            width: 2px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainTitle" runat="server">
    List of Purchase Order
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphMainContent" runat="server">
    <table class="fullwidth">
        <tr>
            <td class="auto-style1">From Confirmation Letter Date</td>
            <td class="auto-style2">:</td>
            <td><telerik:RadDatePicker runat="server" ID="dtpFromDate" style="margin-left: 0px"></telerik:RadDatePicker> </td>
        </tr>
        <tr>
            <td class="auto-style1">To Confirmation Letter Date</td>
            <td class="auto-style2">:</td>
            <td><telerik:RadDatePicker runat="server" ID="dtpToDate"></telerik:RadDatePicker> </td>
        </tr>
        <tr>
            <td class="auto-style1"></td>
            <td class="auto-style2"></td>
            <td><telerik:RadButton runat="server" ID="btnShowReport" Text="Show Report" AutoPostBack="false" OnClientClicked="ShowReport"></telerik:RadButton> </td>
        </tr>
    </table>
    
    <script>
        function ShowReport() {
            var fromDate = $telerik.findControl(document.forms[0], "<%= dtpFromDate.ClientID %>");
            var toDate = $telerik.findControl(document.forms[0], "<%= dtpToDate.ClientID %>");
            showSimplePopUp('ReportPreview.aspx?ReportName=ListPurchaseOrder&FromDate=' + fromDate.get_selectedDate().format("yyyy-MM-dd") + '&ToDate=' + toDate.get_selectedDate().format("yyyy-MM-dd"));
            return false;
        }
    </script>
</asp:Content>
