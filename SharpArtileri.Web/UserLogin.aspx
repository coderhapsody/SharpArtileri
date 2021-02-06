<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MasterUnregisteredPage.Master" AutoEventWireup="true" CodeBehind="UserLogin.aspx.cs" Inherits="SharpArtileri.Web.UserLogin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="server">
    <style>
        .centered {
            width: 300px;
            margin: 0 auto;
        }        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTitle" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphMainContent" runat="server">
    <span style="text-align: center;">
        <img src="Images/purchasing.jpg" />
    </span>
    <hr />
    <div class="centered">
        <asp:ValidationSummary runat="server" ID="vsmSummary" ShowMessageBox="true" ShowSummary="false" DisplayMode="List"/>
        <table class="noborder">
            <tr>
                <td class="auto-style1">Company</td>
                <td class="auto-style1">:</td>
                <td class="auto-style1">
                    <telerik:RadDropDownList runat="server" ID="ddlCompany" DefaultMessage="Select company"></telerik:RadDropDownList>
                    <asp:RequiredFieldValidator runat="server" ID="rqvCompany" ControlToValidate="ddlCompany" Display="None" SetFocusOnError="true" EnableViewState="false" ErrorMessage="Company must be selected"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>User Name</td>
                <td>:</td>
                <td>
                    <telerik:RadTextBox ID="txtUserName" runat="server">
                    </telerik:RadTextBox>
                </td>
            </tr>
            <tr>
                <td>Password</td>
                <td>:</td>
                <td>
                    <telerik:RadTextBox ID="txtPassword" runat="server" TextMode="Password">
                    </telerik:RadTextBox>
                </td>

            </tr>
            <tr>
                <td></td>
                <td></td>
                <td>
                    <telerik:RadButton runat="server" ID="btnLogin" Text="Login" OnClick="btnLogin_Click" EnableViewState="false"></telerik:RadButton>
                </td>
            </tr>
        </table>
        <asp:Label runat="server" ID="lblStatus" EnableViewState="false"></asp:Label>
    </div>

</asp:Content>
