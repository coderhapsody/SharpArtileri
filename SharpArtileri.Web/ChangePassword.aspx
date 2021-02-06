<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MasterWorkspace.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="SharpArtileri.Web.ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMainTitle" runat="server">
    Change Password
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    <asp:MultiView runat="server" ID="mvwForm">
        <asp:View ID="View1" runat="server">
            <table class="noborder">
                <tr>
                    <td style="width: 200px;">Current Password</td>
                    <td style="width: 5px;">:</td>
                    <td>
                        <telerik:RadTextBox runat="server" ID="txtCurrentPassword" TextMode="Password" MaxLength="30"></telerik:RadTextBox>
                        <asp:RequiredFieldValidator runat="server" ID="rqvCurrentPassword" SetFocusOnError="true" CssClass="errorMessage" Display="Dynamic" ControlToValidate="txtCurrentPassword" ErrorMessage="<b>Current Password</b> must be specified"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>New Password</td>
                    <td>:</td>
                    <td>
                        <telerik:RadTextBox runat="server" ID="txtNewPassword" TextMode="Password" MaxLength="30"></telerik:RadTextBox>
                        <asp:RequiredFieldValidator runat="server" ID="rqvNewPassword" SetFocusOnError="true" CssClass="errorMessage" Display="Dynamic" ControlToValidate="txtNewPassword" ErrorMessage="<b>New Password</b> must be specified"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>Confirm Password</td>
                    <td>:</td>
                    <td>
                        <telerik:RadTextBox runat="server" ID="txtConfirmPassword" TextMode="Password" MaxLength="30"></telerik:RadTextBox>
                        <asp:RequiredFieldValidator runat="server" ID="rqvConfirmPassword" SetFocusOnError="true" CssClass="errorMessage" ControlToValidate="txtConfirmPassword" ErrorMessage="<b>Confirm Password</b> must be specified" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:CompareValidator runat="server" ID="cmpPassword" ControlToValidate="txtConfirmPassword" ControlToCompare="txtNewPassword" CssClass="errorMessage" ErrorMessage="<b>Confirm Password</b> and <b>New Password</b> must be same" Display="Dynamic"></asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td></td>
                    <td>
                        <telerik:RadButton runat="server" ID="btnChangePassword" Text="Change Password" OnClick="btnChangePassword_Click"></telerik:RadButton>
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="View2" runat="server">
            <h1>Change Password Success</h1>
            <div>
                You can try to log in to the application using your new password.
            </div>
        </asp:View>
    </asp:MultiView>

</asp:Content>
