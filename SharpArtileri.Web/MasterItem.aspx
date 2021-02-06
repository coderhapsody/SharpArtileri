<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MasterWorkspace.Master" AutoEventWireup="true" CodeBehind="MasterItem.aspx.cs" Inherits="SharpArtileri.Web.MasterItem" StylesheetTheme="Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 4px;
        }

        .auto-style3 {
            width: 120px;
        }

        .auto-style4 {
            width: 150px;
        }

        .auto-style5 {
            width: 150px;
            height: 24px;
        }

        .auto-style6 {
            height: 24px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainTitle" runat="server">
    Item
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphMainContent" runat="server">
    <asp:MultiView runat="server" ID="mvwForm">
        <asp:View runat="server" ID="viwRead">
            <fieldset>
                <table class="fullwidth">
                    <tr>
                        <td class="auto-style3">Code</td>
                        <td class="auto-style1">:</td>
                        <td>
                            <telerik:RadTextBox ID="txtFindCode" runat="server" Width="150px">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style3">Name</td>
                        <td class="auto-style1">:</td>
                        <td>
                            <telerik:RadTextBox ID="txtFindName" runat="server" Width="250px">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style3"></td>
                        <td class="auto-style1"></td>
                        <td>
                            <telerik:RadButton runat="server" ID="btnSearch" Text="Search" OnClick="btnSearch_Click"></telerik:RadButton>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style3">&nbsp;</td>
                        <td class="auto-style1">&nbsp;</td>
                        <td>
                            <asp:Label ID="lblStatus" runat="server" EnableViewState="False"></asp:Label>
                        </td>
                    </tr>
                </table>
            </fieldset>
            <br />
            <asp:LinkButton runat="server" ID="lnbAddNew" Text="Add New" OnClick="lnbAddNew_Click" SkinID="AddNewButton" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:LinkButton runat="server" ID="lnbDelete" Text="Delete" OnClick="lnbDelete_Click" SkinID="DeleteButton" OnClientClick="return confirm('Delete marked row(s) ?')" />
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
                        <telerik:GridBoundColumn DataField="Code" FilterControlAltText="Filter Code column" HeaderText="Code" SortExpression="Code" UniqueName="Code">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text="" />
                            </ColumnValidationSettings>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Name" FilterControlAltText="Filter Name column" HeaderText="Name" SortExpression="Name" UniqueName="Name">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text="" />
                            </ColumnValidationSettings>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Type" FilterControlAltText="Filter Type column" HeaderText="Type" SortExpression="Type" UniqueName="Type">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text="" />
                            </ColumnValidationSettings>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="SupplierName" FilterControlAltText="Filter SupplierName column" HeaderText="SupplierName" SortExpression="SupplierName" UniqueName="SupplierName">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text="" />
                            </ColumnValidationSettings>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="UnitName1" FilterControlAltText="Filter UnitName1 column" HeaderText="UnitName1" SortExpression="UnitName1" UniqueName="UnitName1" DataType="System.String">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text="" />
                            </ColumnValidationSettings>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="UnitName2" FilterControlAltText="Filter UnitName2 column" HeaderText="UnitName2" SortExpression="UnitName2" UniqueName="UnitName2" DataType="System.String">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text="" />
                            </ColumnValidationSettings>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="UnitName3" FilterControlAltText="Filter UnitName3 column" HeaderText="UnitName3" SortExpression="UnitName3" UniqueName="UnitName3" DataType="System.String">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text="" />
                            </ColumnValidationSettings>
                        </telerik:GridBoundColumn>
                        <telerik:GridCheckBoxColumn DataField="IsTaxed" DataType="System.Boolean" FilterControlAltText="Filter IsTaxed column" HeaderText="IsTaxed" SortExpression="IsTaxed" UniqueName="IsTaxed">
                        </telerik:GridCheckBoxColumn>
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
            <asp:SqlDataSource ID="sdsMaster" runat="server" SelectCommand="proc_GetAllItems" SelectCommandType="StoredProcedure" OnSelecting="sdsMaster_Selecting">
                <SelectParameters>
                    <asp:Parameter Name="Code" Type="String" />
                    <asp:Parameter Name="Name" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>
        </asp:View>
        <asp:View runat="server" ID="viwAddEdit">
            <table class="fullwidth">
                <tr>
                    <td style="width: 50%">
                        <table class="fullwidth">
                            <tr>
                                <td class="auto-style4">Code</td>
                                <td class="auto-style7">:</td>
                                <td>
                                    <telerik:RadTextBox ID="txtCode" runat="server" ValidationGroup="AddEdit" Width="150px">
                                    </telerik:RadTextBox>
                                    <asp:RequiredFieldValidator ID="rqvCode" runat="server" ControlToValidate="txtCode" CssClass="errorMessage" Display="Dynamic" EnableViewState="False" ErrorMessage="&lt;b&gt;Item Code&lt;/b&gt; must be specified" SetFocusOnError="True" ValidationGroup="AddEdit" />
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style4">Name </td>
                                <td class="auto-style7">:</td>
                                <td>
                                    <telerik:RadTextBox ID="txtName" runat="server" ValidationGroup="AddEdit" Width="250px">
                                    </telerik:RadTextBox>
                                    <asp:RequiredFieldValidator ID="rqvName" runat="server" ControlToValidate="txtName" CssClass="errorMessage" Display="Dynamic" EnableViewState="False" ErrorMessage="&lt;b&gt;Item Name&lt;/b&gt; must be specified" SetFocusOnError="True" ValidationGroup="AddEdit" />
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style4">Type </td>
                                <td class="auto-style7">:</td>
                                <td>
                                    <telerik:RadDropDownList runat="server" ID="ddlType">
                                        <Items>
                                            <telerik:DropDownListItem runat="server" Text="" Value=""/>
                                            <telerik:DropDownListItem runat="server" Text="Barang" Value="B"/>
                                            <telerik:DropDownListItem runat="server" Text="Jasa" Value="J"/>
                                        </Items>
                                    </telerik:RadDropDownList>
                                    <asp:RequiredFieldValidator ID="rqvType" runat="server" ControlToValidate="ddlType" CssClass="errorMessage" Display="Dynamic" EnableViewState="False" ErrorMessage="&lt;b&gt;Item Type&lt;/b&gt; must be specified" SetFocusOnError="True" ValidationGroup="AddEdit" />
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style4">Supplier </td>
                                <td class="auto-style7">:</td>
                                <td>
                                    <telerik:RadDropDownList ID="ddlSupplier" runat="server" ValidationGroup="AddEdit" Width="300px" DropDownHeight="200px">
                                    </telerik:RadDropDownList>
                                    <asp:RequiredFieldValidator ID="rqvSupplier" runat="server" ControlToValidate="ddlSupplier" CssClass="errorMessage" Display="Dynamic" EnableViewState="False" ErrorMessage="&lt;b&gt;Supplier&lt;/b&gt; must be specified" SetFocusOnError="True" ValidationGroup="AddEdit" />
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style5">Taxable</td>
                                <td class="auto-style6">:</td>
                                <td class="auto-style6">
                                    <asp:CheckBox ID="chkIsTaxable" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style5">1st Unit Measurement</td>
                                <td class="auto-style6">:</td>
                                <td class="auto-style6">
                                    <telerik:RadTextBox ID="txtUnitName1" runat="server" ValidationGroup="AddEdit" Width="50px">
                                    </telerik:RadTextBox>
                                    &nbsp;(Ratio of 1st Unit Measurement is always 1)
                        <asp:RequiredFieldValidator ID="rqvUnit1" runat="server" ControlToValidate="txtUnitName1" CssClass="errorMessage" Display="Dynamic" EnableViewState="False" ErrorMessage="&lt;b&gt;Unit Name 1&lt;/b&gt; must be specified" SetFocusOnError="True" ValidationGroup="AddEdit" />
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style5">2nd Unit Measurement</td>
                                <td class="auto-style6">:</td>
                                <td class="auto-style6">
                                    <telerik:RadTextBox ID="txtUnitName2" runat="server" ValidationGroup="AddEdit" Width="50px">
                                    </telerik:RadTextBox>
                                    &nbsp;&nbsp; Ratio :
                        <telerik:RadNumericTextBox ID="ntbUnitRatio2" runat="server" Width="50px">
                        </telerik:RadNumericTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style5">3rd Unit Measurement</td>
                                <td class="auto-style6">:</td>
                                <td class="auto-style6">
                                    <telerik:RadTextBox ID="txtUnitName3" runat="server" ValidationGroup="AddEdit" Width="50px">
                                    </telerik:RadTextBox>
                                    &nbsp;&nbsp; Ratio :
                        <telerik:RadNumericTextBox ID="ntbUnitRatio3" runat="server" Width="50px">
                        </telerik:RadNumericTextBox>
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
                    </td>
                    <td style="width: 50%">
                        <artileri:ItemPriceList ID="ItemPriceList1" runat="server" />
                    </td>
                </tr>
            </table>

        </asp:View>
    </asp:MultiView>
</asp:Content>
