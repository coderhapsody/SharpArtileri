<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MasterWorkspace.Master" AutoEventWireup="true" CodeBehind="ManageCompany.aspx.cs" Inherits="SharpArtileri.Web.ManageCompany" StylesheetTheme="Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 5px;
        }

        .auto-style2 {
            width: 140px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainTitle" runat="server">
    Manage Companies        
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphMainContent" runat="server">
    <asp:MultiView runat="server" ID="mvwForm">
        <asp:View ID="viwRead" runat="server">
            <asp:LinkButton runat="server" ID="lnbAddNew" Text="Add New" OnClick="lnbAddNew_Click" SkinID="AddNewButton" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <%--<asp:LinkButton runat="server" ID="lnbDelete" Text="Delete" OnClick="lnbDelete_Click" SkinID="DeleteButton" OnClientClick="return confirm('Delete marked row(s) ?')" />--%>
            &nbsp;<asp:Label ID="lblStatus" runat="server" EnableViewState="False"></asp:Label>
            <telerik:RadGrid ID="grdMaster" runat="server" AllowPaging="True" AllowSorting="True" CellSpacing="0" DataSourceID="sdsMaster" GridLines="None" ShowGroupPanel="True" AutoGenerateColumns="False" GroupingSettings-CaseSensitive="false" OnItemCommand="grdMaster_ItemCommand">
                <GroupingSettings CaseSensitive="False" />
                <ClientSettings AllowDragToGroup="True" AllowColumnsReorder="True" EnableRowHoverStyle="true">
                </ClientSettings>
                <MasterTableView DataSourceID="sdsMaster" DataKeyNames="Code">
                    <Columns>
                        <telerik:GridBoundColumn DataField="Code" FilterControlAltText="Filter Code column" HeaderText="Code" SortExpression="Code" UniqueName="Code" DataType="System.String" ReadOnly="True">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text="" />
                            </ColumnValidationSettings>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Name" FilterControlAltText="Filter Name column" HeaderText="Name" SortExpression="Name" UniqueName="Name">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text="" />
                            </ColumnValidationSettings>
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn AllowFiltering="false" Groupable="false" ItemStyle-Width="20px">
                            <ItemTemplate>
                                <asp:ImageButton ID="imbEdit" runat="server" SkinID="EditButton" CommandName="EditRow" CommandArgument='<%# Eval("Code") %>' />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
            <asp:SqlDataSource ID="sdsMaster" runat="server" SelectCommand="proc_GetAllCompany" SelectCommandType="StoredProcedure" ConnectionString="<%$ ConnectionStrings:ManagementConnectionString %>" ProviderName="<%$ ConnectionStrings:ManagementConnectionString.ProviderName %>"></asp:SqlDataSource>
        </asp:View>
        <asp:View ID="viwAddEdit" runat="server">
            <table class="fullwidth">
                <tr>
                    <td class="auto-style2">Code</td>
                    <td class="auto-style1">:</td>
                    <td>
                        <telerik:RadTextBox runat="server" ID="txtCode" Width="80px"></telerik:RadTextBox>
                        <asp:RequiredFieldValidator runat="server" ID="rqvCode" ControlToValidate="txtCode" SetFocusOnError="true" CssClass="errorMessage" ErrorMessage="Company code must be specified" EnableViewState="false"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">Name</td>
                    <td class="auto-style1">:</td>
                    <td>
                        <telerik:RadTextBox runat="server" ID="txtName" Width="200px"></telerik:RadTextBox>
                        <asp:RequiredFieldValidator runat="server" ID="rqvName" ControlToValidate="txtName" SetFocusOnError="true" CssClass="errorMessage" ErrorMessage="Company name must be specified" EnableViewState="false"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">Server</td>
                    <td class="auto-style1">:</td>
                    <td>
                        <telerik:RadTextBox runat="server" ID="txtServer"></telerik:RadTextBox>
                        <asp:RequiredFieldValidator runat="server" ID="rqvServer" ControlToValidate="txtServer" SetFocusOnError="true" CssClass="errorMessage" ErrorMessage="Server name must be specified" EnableViewState="false"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">User ID</td>
                    <td class="auto-style1">:</td>
                    <td>
                        <telerik:RadTextBox runat="server" ID="txtUserID"></telerik:RadTextBox>
                        <asp:RequiredFieldValidator runat="server" ID="rqvUserID" ControlToValidate="txtUserID" SetFocusOnError="true" CssClass="errorMessage" ErrorMessage="User ID must be specified" EnableViewState="false"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">Password</td>
                    <td class="auto-style1">:</td>
                    <td>
                        <telerik:RadTextBox runat="server" ID="txtPassword"></telerik:RadTextBox>
                        <asp:RequiredFieldValidator runat="server" ID="rqvPassword" ControlToValidate="txtPassword" SetFocusOnError="true" CssClass="errorMessage" ErrorMessage="Password must be specified" EnableViewState="false"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">Database</td>
                    <td class="auto-style1">:</td>
                    <td>
                        <telerik:RadTextBox runat="server" ID="txtDatabase"></telerik:RadTextBox>
                        <asp:RequiredFieldValidator runat="server" ID="rqvDatabase" ControlToValidate="txtDatabase" SetFocusOnError="true" CssClass="errorMessage" ErrorMessage="Databse name must be specified" EnableViewState="false"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">&nbsp;</td>
                    <td class="auto-style1">&nbsp;</td>
                    <td>
                        <telerik:RadButton runat="server" ID="btnSave" Text="Save" OnClick="btnSave_Click" />
                        &nbsp;&nbsp;&nbsp;
                        <telerik:RadButton runat="server" ID="btnCancel" Text="Cancel" OnClick="btnCancel_Click" CausesValidation="false" />
                        &nbsp;&nbsp;&nbsp;
                        <telerik:RadButton runat="server" ID="btnDelete" Text="Delete" OnClick="btnDelete_Click" CausesValidation="false" />
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
</asp:Content>
