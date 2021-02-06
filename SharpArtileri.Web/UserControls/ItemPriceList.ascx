<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ItemPriceList.ascx.cs" Inherits="SharpArtileri.Web.UserControls.ItemPriceList" %>
<style type="text/css">
    a {
        text-decoration: none;
    }
</style>
<b>Price List History</b>
<telerik:RadGrid ID="grdMaster" runat="server" AllowPaging="True" AllowSorting="True" CellSpacing="0" DataSourceID="sdsMaster" GridLines="None" ShowGroupPanel="True" AutoGenerateColumns="False" GroupingSettings-CaseSensitive="false">
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
            <telerik:GridBoundColumn DataField="SupplierName" FilterControlAltText="Filter SupplierName column" HeaderText="SupplierName" SortExpression="SupplierName" UniqueName="SupplierName" DataType="System.String">
                <ColumnValidationSettings>
                    <ModelErrorMessage Text="" />
                </ColumnValidationSettings>
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="UnitName" FilterControlAltText="Filter UnitName column" HeaderText="UnitName" SortExpression="UnitName" UniqueName="UnitName" DataType="System.String">
                <ColumnValidationSettings>
                    <ModelErrorMessage Text="" />
                </ColumnValidationSettings>
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="EffectiveDate" FilterControlAltText="Filter EffectiveDate column" HeaderText="EffectiveDate" SortExpression="EffectiveDate" UniqueName="EffectiveDate" DataType="System.DateTime" DataFormatString="{0:dd-MMM-yyyy}">
                <ColumnValidationSettings>
                    <ModelErrorMessage Text="" />
                </ColumnValidationSettings>
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="Price" FilterControlAltText="Filter Price column" HeaderText="Price" SortExpression="Price" UniqueName="Price" DataType="System.Decimal" DataFormatString="{0:c}">
                <ColumnValidationSettings>
                    <ModelErrorMessage Text="" />
                </ColumnValidationSettings>
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="ChangedWhen" FilterControlAltText="Filter ChangedWhen column" HeaderText="ChangedWhen" SortExpression="ChangedWhen" UniqueName="ChangedWhen" DataType="System.DateTime" DataFormatString="{0:dd-MMM-yyyy HH:mm}">
                <ColumnValidationSettings>
                    <ModelErrorMessage Text="" />
                </ColumnValidationSettings>
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="ChangedWho" FilterControlAltText="Filter ChangedWho column" HeaderText="ChangedWho" SortExpression="ChangedWho" UniqueName="ChangedWho" DataType="System.String">
                <ColumnValidationSettings>
                    <ModelErrorMessage Text="" />
                </ColumnValidationSettings>
            </telerik:GridBoundColumn>
        </Columns>
    </MasterTableView>
</telerik:RadGrid>

<asp:SqlDataSource ID="sdsMaster" runat="server" SelectCommand="proc_GetAllItemPriceList" SelectCommandType="StoredProcedure" OnSelecting="sdsMaster_Selecting">
    <SelectParameters>
        <asp:Parameter Name="SupplierID" Type="Int32" />
        <asp:Parameter Name="ItemID" Type="Int32" />
    </SelectParameters>
</asp:SqlDataSource>


