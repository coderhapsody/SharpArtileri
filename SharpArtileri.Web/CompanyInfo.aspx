<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MasterWorkspace.Master" AutoEventWireup="true" CodeBehind="CompanyInfo.aspx.cs" Inherits="SharpArtileri.Web.CompanyInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            width: 200px;
        }
        .auto-style3 {
            width: 2px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainTitle" runat="server">
    Company Information
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphMainContent" runat="server">
    <table class="auto-style1">
        <tr>
            <td class="auto-style2">Company Registration Code</td>
            <td class="auto-style3">:</td>
            <td>
                <asp:Label ID="lblCompanyCode" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="auto-style2">Company Name</td>
            <td class="auto-style3">:</td>
            <td>
                <telerik:RadTextBox ID="txtCompanyName" Runat="server" Width="250px">
                </telerik:RadTextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style2">Address 1</td>
            <td class="auto-style3">:</td>
            <td>
                <telerik:RadTextBox ID="txtAddress1" Runat="server" Width="400px">
                </telerik:RadTextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style2">Address 2</td>
            <td class="auto-style3">:</td>
            <td>
                <telerik:RadTextBox ID="txtAddress2" Runat="server" Width="400px">
                </telerik:RadTextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style2">Purchase Order Approver Name</td>
            <td class="auto-style3">:</td>
            <td>
                <telerik:RadTextBox ID="txtPOApproverName" Runat="server" Width="200px">
                </telerik:RadTextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style2">&nbsp;</td>
            <td class="auto-style3">&nbsp;</td>
            <td>
                <telerik:RadButton ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" Text="Update Information">
                </telerik:RadButton>
            </td>
        </tr>
    </table>
</asp:Content>
