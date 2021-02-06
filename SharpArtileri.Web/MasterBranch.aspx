<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MasterWorkspace.Master" AutoEventWireup="true" CodeBehind="MasterBranch.aspx.cs" Inherits="SharpArtileri.Web.MasterBranch" StyleSheetTheme="Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainTitle" runat="server">
    Branch
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphMainContent" runat="server">
    <asp:MultiView runat="server" ID="mvwForm">
        <asp:View runat="server" ID="viwRead">
            <asp:LinkButton runat="server" ID="lnbAddNew" Text="Add New" OnClick="lnbAddNew_Click" SkinID="AddNewButton" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:LinkButton runat="server" ID="lnbDelete" Text="Delete" OnClick="lnbDelete_Click" SkinID="DeleteButton" OnClientClick="return confirm('Delete marked row(s) ?')" />
            &nbsp;<asp:Label ID="lblStatus" runat="server" EnableViewState="False"></asp:Label>
            <telerik:RadGrid ID="grdMaster" runat="server" AllowPaging="True" AllowSorting="True" CellSpacing="0" DataSourceID="sdsMaster" GridLines="None" ShowGroupPanel="True" AutoGenerateColumns="False" GroupingSettings-CaseSensitive="false" OnItemCommand="grdMaster_ItemCommand">
                <GroupingSettings CaseSensitive="False" />
                <ClientSettings AllowDragToGroup="True" AllowColumnsReorder="True" EnableRowHoverStyle="true">
                </ClientSettings>
                <MasterTableView DataSourceID="sdsMaster" DataKeyNames="ID">
                    <Columns>
                        <telerik:GridBoundColumn DataField="ID" FilterControlAltText="Filter ID column" HeaderText="ID" SortExpression="ID" UniqueName="ID" DataType="System.Int32" ReadOnly="True">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text="" />
                            </ColumnValidationSettings>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Name" FilterControlAltText="Filter Name column" HeaderText="Name" SortExpression="Name" UniqueName="Name">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text="" />
                            </ColumnValidationSettings>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Address" FilterControlAltText="Filter Address column" HeaderText="Address" SortExpression="Address" UniqueName="Address" DataType="System.String">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text="" />
                            </ColumnValidationSettings>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Phone" FilterControlAltText="Filter Phone column" HeaderText="Phone" SortExpression="Phone" UniqueName="Phone" DataType="System.String">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text="" />
                            </ColumnValidationSettings>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Email" FilterControlAltText="Filter Email column" HeaderText="Email" SortExpression="Email" UniqueName="Email" DataType="System.String">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text="" />
                            </ColumnValidationSettings>
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn AllowFiltering="false" Groupable="false" ItemStyle-Width="20px">
                            <ItemTemplate>
                                <asp:ImageButton ID="imbEdit" runat="server" SkinID="EditButton" CommandName="EditRow" CommandArgument='<%# Eval("ID") %>' />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn AllowFiltering="false" Groupable="false" ItemStyle-Width="20px">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkDelete" runat="server" ToolTip="Mark this row to delete" data-value='<%# Eval("ID") %>' />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
            <asp:SqlDataSource ID="sdsMaster" runat="server" SelectCommand="proc_GetAllBranches" SelectCommandType="StoredProcedure" OnSelecting="sdsMaster_Selecting">
            </asp:SqlDataSource>
        </asp:View>
        <asp:View runat="server" ID="viwAddEdit">
            <table class="fullwidth">
                <tr>
                    <td class="auto-style4">Name </td>
                    <td class="auto-style7">:</td>
                    <td>
                        <telerik:RadTextBox ID="txtName" runat="server" ValidationGroup="AddEdit" Width="250px">
                        </telerik:RadTextBox>
                        <asp:RequiredFieldValidator ID="rqvName" runat="server" ControlToValidate="txtName" CssClass="errorMessage" Display="Dynamic" EnableViewState="False" ErrorMessage="&lt;b&gt;Branch Name&lt;/b&gt; must be specified" SetFocusOnError="True" ValidationGroup="AddEdit" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style4">Address</td>
                    <td class="auto-style7">:</td>
                    <td>
                        <telerik:RadTextBox ID="txtAddress" runat="server" ValidationGroup="AddEdit" Width="400px">
                        </telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style4">Phone</td>
                    <td class="auto-style7">:</td>
                    <td>
                        <telerik:RadTextBox ID="txtPhone" runat="server" ValidationGroup="AddEdit" Width="150px">
                        </telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style4">Email</td>
                    <td class="auto-style7">:</td>
                    <td>
                        <telerik:RadTextBox ID="txtName0" runat="server" ValidationGroup="AddEdit" Width="200px">
                        </telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style4">&nbsp;</td>
                    <td class="auto-style7">&nbsp;</td>
                    <td>
                        <telerik:RadButton ID="btnSave" runat="server" EnableViewState="False" OnClick="btnSave_Click" Text="Save" ValidationGroup="AddEdit">
                        </telerik:RadButton>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <telerik:RadButton ID="btnCancel" runat="server" CausesValidation="False" EnableViewState="False" OnClick="btnCancel_Click" Text="Cancel">
                        </telerik:RadButton>
                        &nbsp;<asp:Label ID="lblStatusAddEdit" runat="server" EnableViewState="False"></asp:Label>
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
</asp:Content>
