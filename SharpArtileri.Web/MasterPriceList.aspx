<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MasterWorkspace.Master" AutoEventWireup="true" CodeBehind="MasterPriceList.aspx.cs" Inherits="SharpArtileri.Web.MasterPriceList" StylesheetTheme="Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <style type="text/css">
          .col1,
        .col2,
        .col3 {
            margin: 0;
            padding: 0 5px 0 0;
            width: 110px;
            line-height: 14px;
            float: left;
        }

        .rcbHeader ul,
        .rcbFooter ul,
        .rcbItem ul,
        .rcbHovered ul,
        .rcbDisabled ul {
            margin: 0;
            padding: 0;
            width: 100%;
            display: inline-block;
            list-style-type: none;
        }
         .auto-style2 {
             width: 110px;
         }


        .auto-style8 {
            width: 2px;
        }

        .auto-style11 {
            width: 3px;
        }

        .RadComboBox_Default {
            color: #333;
            font: normal 12px/16px "Segoe UI",Arial,Helvetica,sans-serif;
        }

        .RadComboBox {
            margin: 0;
            padding: 0;
            *zoom: 1;
            display: -moz-inline-stack;
            display: inline-block;
            *display: inline;
            text-align: left;
            vertical-align: middle;
            _vertical-align: top;
            white-space: nowrap;
        }

        .RadComboBox_Default .rcbReadOnly .rcbInputCellLeft {
            background-position: 0 -88px;
        }

        .RadComboBox_Default .rcbInputCellLeft {
            background-position: 0 0;
        }

        .RadComboBox_Default .rcbInputCell {
            background-image: url('mvwres://Telerik.Web.UI, Version=2013.3.1114.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png');
            _background-image: url('mvwres://Telerik.Web.UI, Version=2013.3.1114.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSpriteIE6.png');
        }

        .RadComboBox .rcbInputCell {
            width: 100%;
            height: 20px;
            _height: 22px;
            line-height: 20px;
            _line-height: 22px;
            text-align: left;
            vertical-align: middle;
        }

        .RadComboBox .rcbInputCell {
            margin: 0;
            padding: 0;
            background-color: transparent;
            background-repeat: no-repeat;
            *zoom: 1;
        }

        .RadComboBox_Default .rcbReadOnly .rcbInput {
            color: #333;
        }

        .RadComboBox .rcbReadOnly .rcbInput {
            cursor: default;
        }

        .RadComboBox_Default .rcbInput {
            color: #333;
            font: normal 12px "Segoe UI",Arial,Helvetica,sans-serif;
            line-height: 16px;
        }

        .RadComboBox .rcbInput {
            margin: 0;
            padding: 0;
            border: 0;
            background: 0;
            padding: 2px 0 1px;
            _padding: 2px 0 0;
            width: 100%;
            _height: 18px;
            outline: 0;
            -webkit-appearance: none;
        }

        .RadComboBox_Default .rcbReadOnly .rcbArrowCellRight {
            background-position: -162px -176px;
        }

        .RadComboBox_Default .rcbArrowCellRight {
            background-position: -18px -176px;
        }

        .RadComboBox_Default .rcbArrowCell {
            background-image: url('mvwres://Telerik.Web.UI, Version=2013.3.1114.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png');
            _background-image: url('mvwres://Telerik.Web.UI, Version=2013.3.1114.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSpriteIE6.png');
        }

        .RadComboBox .rcbArrowCell {
            width: 18px;
        }

        .RadComboBox .rcbArrowCell {
            margin: 0;
            padding: 0;
            background-color: transparent;
            background-repeat: no-repeat;
            *zoom: 1;
        }

        .RadDropDownList {
            display: inline-block !important;
            width: 160px !important;
        }

        .RadDropDownList_Default {
            color: #333;
            font: normal 12px/16px "Segoe UI",Arial,Helvetica,sans-serif;
        }

        .RadDropDownList {
            margin: 0;
            padding: 0;
            *zoom: 1;
            display: -moz-inline-stack;
            display: inline-block;
            *display: inline;
            width: 160px;
            text-align: left;
            vertical-align: middle;
            _vertical-align: top;
            white-space: nowrap;
            cursor: default;
        }

        .RadDropDownList {
            display: inline-block !important;
            width: 160px !important;
        }

        .RadDropDownList_Default {
            color: #333;
            font: normal 12px/16px "Segoe UI",Arial,Helvetica,sans-serif;
        }

        .RadDropDownList {
            margin: 0;
            padding: 0;
            *zoom: 1;
            display: -moz-inline-stack;
            display: inline-block;
            *display: inline;
            width: 160px;
            text-align: left;
            vertical-align: middle;
            _vertical-align: top;
            white-space: nowrap;
            cursor: default;
        }

        .RadDropDownList_Default .rddlInner {
            border-radius: 3px;
            background-image: url('mvwres://Telerik.Web.UI, Version=2013.3.1114.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radGradientButtonSprite.png');
            _background-image: none;
            border-color: #8a8a8a;
            color: #333;
            background-color: #e8e8e8;
            background-image: -webkit-linear-gradient(top,#faf9f9 0,#e8e8e8 100%);
            background-image: -moz-linear-gradient(top,#faf9f9 0,#e8e8e8 100%);
            background-image: -ms-linear-gradient(top,#faf9f9 0,#e8e8e8 100%);
            background-image: -o-linear-gradient(top,#faf9f9 0,#e8e8e8 100%);
            background-image: linear-gradient(top,#faf9f9 0,#e8e8e8 100%);
        }

        .RadDropDownList .rddlInner {
            padding: 2px 22px 2px 5px;
            border-width: 1px;
            border-style: solid;
            display: block;
            position: relative;
            overflow: hidden;
        }

        .RadDropDownList_Default .rddlInner {
            border-radius: 3px;
            background-image: url('mvwres://Telerik.Web.UI, Version=2013.3.1114.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radGradientButtonSprite.png');
            _background-image: none;
            border-color: #8a8a8a;
            color: #333;
            background-color: #e8e8e8;
            background-image: -webkit-linear-gradient(top,#faf9f9 0,#e8e8e8 100%);
            background-image: -moz-linear-gradient(top,#faf9f9 0,#e8e8e8 100%);
            background-image: -ms-linear-gradient(top,#faf9f9 0,#e8e8e8 100%);
            background-image: -o-linear-gradient(top,#faf9f9 0,#e8e8e8 100%);
            background-image: linear-gradient(top,#faf9f9 0,#e8e8e8 100%);
        }

        .RadDropDownList .rddlInner {
            padding: 2px 22px 2px 5px;
            border-width: 1px;
            border-style: solid;
            display: block;
            position: relative;
            overflow: hidden;
        }

        .RadDropDownList_Default .rddlDefaultMessage {
            color: #a5a5a5;
            font-style: italic;
        }

        .RadDropDownList .rddlDefaultMessage {
            font-style: italic;
        }

        .RadDropDownList .rddlFakeInput {
            margin: 0;
            padding: 0;
            width: 100%;
            height: 16px;
            line-height: 16px;
            display: block;
            overflow: hidden;
        }

        .RadDropDownList_Default .rddlDefaultMessage {
            color: #a5a5a5;
            font-style: italic;
        }

        .RadDropDownList .rddlDefaultMessage {
            font-style: italic;
        }

        .RadDropDownList .rddlFakeInput {
            margin: 0;
            padding: 0;
            width: 100%;
            height: 16px;
            line-height: 16px;
            display: block;
            overflow: hidden;
        }

        .RadDropDownList_Default .rddlIcon {
            background-image: url('mvwres://Telerik.Web.UI, Version=2013.3.1114.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radActionsSprite.png');
            background-position: -21px -20px;
        }

        .RadDropDownList .rddlIcon {
            width: 18px;
            height: 20px;
            border: 0;
            background-repeat: no-repeat;
            position: absolute;
            top: 0;
            right: 0;
        }

        .RadDropDownList_Default .rddlIcon {
            background-image: url('mvwres://Telerik.Web.UI, Version=2013.3.1114.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radActionsSprite.png');
            background-position: -21px -20px;
        }

        .RadDropDownList .rddlIcon {
            width: 18px;
            height: 20px;
            border: 0;
            background-repeat: no-repeat;
            position: absolute;
            top: 0;
            right: 0;
        }

        .rddlSlide {
            _height: 1px;
            float: left;
            display: none;
            position: absolute;
            overflow: hidden;
            z-index: 7000;
        }

        .rddlSlide {
            _height: 1px;
            float: left;
            display: none;
            position: absolute;
            overflow: hidden;
            z-index: 7000;
        }

        .rddlPopup_Default {
            border-color: #8a8a8a;
            color: #333;
            background: white;
            font: normal 12px "Segoe UI",Arial,Helvetica,sans-serif;
            line-height: 16px;
        }

        .rddlPopup {
            *zoom: 1;
            padding: 2px;
            -moz-box-sizing: border-box;
            -webkit-box-sizing: border-box;
            box-sizing: border-box;
            border-width: 1px;
            border-style: solid;
            text-align: left;
            position: relative;
            cursor: default;
            width: 160px;
            *width: 154px;
        }

        .rddlPopup_Default {
            border-color: #8a8a8a;
            color: #333;
            background: white;
            font: normal 12px "Segoe UI",Arial,Helvetica,sans-serif;
            line-height: 16px;
        }

        .rddlPopup {
            *zoom: 1;
            padding: 2px;
            -moz-box-sizing: border-box;
            -webkit-box-sizing: border-box;
            box-sizing: border-box;
            border-width: 1px;
            border-style: solid;
            text-align: left;
            position: relative;
            cursor: default;
            width: 160px;
            *width: 154px;
        }

        .RadInput_Default {
            font: 12px "segoe ui",arial,sans-serif;
        }

        .RadInput {
            vertical-align: middle;
        }

        .RadInput_Default {
            font: 12px "segoe ui",arial,sans-serif;
        }

        .RadInput {
            vertical-align: middle;
        }

        .RadInput_Default {
            font: 12px "segoe ui",arial,sans-serif;
        }

        .RadInput {
            vertical-align: middle;
        }

        .RadInput_Default {
            font: 12px "segoe ui",arial,sans-serif;
        }

        .RadInput {
            vertical-align: middle;
        }

        .RadInput_Default {
            font: 12px "segoe ui",arial,sans-serif;
        }

        .RadInput {
            vertical-align: middle;
        }

        .RadInput_Default {
            font: 12px "segoe ui",arial,sans-serif;
        }

        .RadInput {
            vertical-align: middle;
        }

        .RadInput_Default {
            font: 12px "segoe ui",arial,sans-serif;
        }

        .RadInput {
            vertical-align: middle;
        }

        .RadInput_Default {
            font: 12px "segoe ui",arial,sans-serif;
        }

        .RadInput {
            vertical-align: middle;
        }

        .RadInput_Default {
            font: 12px "segoe ui",arial,sans-serif;
        }

        .RadInput {
            vertical-align: middle;
        }

        .RadInput_Default {
            font: 12px "segoe ui",arial,sans-serif;
        }

        .RadInput {
            vertical-align: middle;
        }

        .RadInput .riTextBox {
            height: 17px;
        }

        .RadInput .riTextBox {
            height: 17px;
        }

        .RadInput .riTextBox {
            height: 17px;
        }

        .RadInput .riTextBox {
            height: 17px;
        }

        .RadInput .riTextBox {
            height: 17px;
        }

        .RadInput .riTextBox {
            height: 17px;
        }

        .RadInput .riTextBox {
            height: 17px;
        }

        .RadInput .riTextBox {
            height: 17px;
        }

        .RadInput .riTextBox {
            height: 17px;
        }

        .RadInput .riTextBox {
            height: 17px;
        }

        .RadPicker {
            vertical-align: middle;
        }

        .RadPicker {
            vertical-align: middle;
        }

        .RadPicker {
            vertical-align: middle;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainTitle" runat="server">
    Price List
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphMainContent" runat="server">
    <asp:MultiView runat="server" ID="mvwForm">
        <asp:View runat="server" ID="viwRead">
            <fieldset>
                <table class="fullwidth">
                    <tr>
                        <td class="auto-style2">Supplier</td>
                        <td class="auto-style8">:</td>
                        <td>
                            <telerik:RadComboBox ID="cboFindSupplier" runat="server" EnableVirtualScrolling="true" HighlightTemplatedItems="true" MarkFirstMatch="true" ShowDropDownOnTextboxClick="False" Width="350px">
                                <HeaderTemplate>
                                    <ul>
                                        <li class="col1" style="display: none;">ID</li>
                                        <li class="col2">Name</li>
                                        <li class="col3">Email</li>
                                    </ul>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <ul>
                                        <li class="col1" style="display: none;"><%# DataBinder.Eval(Container.DataItem, "ID") %></li>
                                        <li class="col1"><%# DataBinder.Eval(Container.DataItem, "Name") %></li>
                                        <li class="col2"><%# DataBinder.Eval(Container.DataItem, "Email") %></li>
                                    </ul>
                                </ItemTemplate>
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style2">Item</td>
                        <td class="auto-style8">:</td>
                        <td>
                            <telerik:RadComboBox ID="cboFindItem" runat="server" EnableLoadOnDemand="true" EnableVirtualScrolling="true" HighlightTemplatedItems="True" MarkFirstMatch="true" OnItemsRequested="cboFindItem_ItemsRequested" ShowDropDownOnTextboxClick="false" Width="300px">
                                <HeaderTemplate>
                                    <ul>
                                        <li class="col1">Code</li>
                                        <li class="col2">Name</li>
                                        <%--<li class="col3">IsTaxed</li>--%>
                                    </ul>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <ul>
                                        <li class="col1"><%# DataBinder.Eval(Container.DataItem, "Code") %></li>
                                        <li class="col2"><%# DataBinder.Eval(Container.DataItem, "Name") %></li>
                                        <%--                                        <li class="col3">
                                            <%# DataBinder.Eval(Container.DataItem, "IsTaxed") %></li>--%>
                                    </ul>
                                </ItemTemplate>
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style2"></td>
                        <td class="auto-style8"></td>
                        <td>
                            <telerik:RadButton runat="server" ID="btnSearch" Text="Search" OnClick="btnSearch_Click"></telerik:RadButton>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style2">&nbsp;</td>
                        <td class="auto-style8">&nbsp;</td>
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
                        <telerik:GridBoundColumn DataField="ItemCode" FilterControlAltText="Filter ItemCode column" HeaderText="ItemCode" SortExpression="ItemCode" UniqueName="ItemCode">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text="" />
                            </ColumnValidationSettings>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ItemName" FilterControlAltText="Filter ItemName column" HeaderText="ItemName" SortExpression="ItemName" UniqueName="ItemName">
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
            <asp:SqlDataSource ID="sdsMaster" runat="server" SelectCommand="proc_GetAllItemPriceList" SelectCommandType="StoredProcedure" OnSelecting="sdsMaster_Selecting">
                <SelectParameters>
                    <asp:Parameter Name="SupplierID" Type="Int32" />
                    <asp:Parameter Name="ItemID" Type="Int32" />
                </SelectParameters>
            </asp:SqlDataSource>
        </asp:View>
        <asp:View runat="server" ID="viwAddEdit">
            <table class="fullwidth">
                <tr>
                    <td class="auto-style2">Item</td>
                    <td class="auto-style11">:</td>
                    <td>
                        <telerik:RadComboBox ID="cboItem" runat="server" AutoPostBack="true" EnableVirtualScrolling="true" HighlightTemplatedItems="True" MarkFirstMatch="true" OnSelectedIndexChanged="cboItem_SelectedIndexChanged" ShowDropDownOnTextboxClick="false" Width="300px" ValidationGroup="AddEdit" CausesValidation="False">
                            <HeaderTemplate>
                                <ul>
                                    <li class="col1">Code</li>
                                    <li class="col2">Name</li>
                                    <%--<li class="col3">IsTaxed</li>--%>
                                </ul>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <ul>
                                    <li class="col1"><%# DataBinder.Eval(Container.DataItem, "Code") %></li>
                                    <li class="col2"><%# DataBinder.Eval(Container.DataItem, "Name") %></li>
                                    <%--                                        <li class="col3">
                                            <%# DataBinder.Eval(Container.DataItem, "IsTaxed") %></li>--%>
                                </ul>
                            </ItemTemplate>
                        </telerik:RadComboBox>
                        &nbsp;<asp:RequiredFieldValidator ID="rqvItem" runat="server" ControlToValidate="cboItem" CssClass="errorMessage" EnableTheming="True" EnableViewState="False" ErrorMessage="&lt;b&gt;Item&lt;/b&gt; must be specified" SetFocusOnError="True" ValidationGroup="AddEdit"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">Supplier</td>
                    <td class="auto-style11">:</td>
                    <td>
                        <telerik:RadComboBox ID="cboSupplier" runat="server" EnableVirtualScrolling="true" HighlightTemplatedItems="true" MarkFirstMatch="true" ShowDropDownOnTextboxClick="False" Width="350px" ValidationGroup="AddEdit">
                            <HeaderTemplate>
                                <ul>
                                    <li class="col1" style="display: none;">ID</li>
                                    <li class="col2">Name</li>
                                    <li class="col3">Email</li>
                                </ul>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <ul>
                                    <li class="col1" style="display: none;"><%# DataBinder.Eval(Container.DataItem, "ID") %></li>
                                    <li class="col1"><%# DataBinder.Eval(Container.DataItem, "Name") %></li>
                                    <li class="col2"><%# DataBinder.Eval(Container.DataItem, "Email") %></li>
                                </ul>
                            </ItemTemplate>
                        </telerik:RadComboBox>
                        &nbsp;<asp:RequiredFieldValidator ID="rqvSupplier" runat="server" ControlToValidate="cboSupplier" CssClass="errorMessage" EnableTheming="True" EnableViewState="False" ErrorMessage="&lt;b&gt;Supplier&lt;/b&gt; must be specified" SetFocusOnError="True" ValidationGroup="AddEdit"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">Unit</td>
                    <td class="auto-style11">:</td>
                    <td class="auto-style6">
                        <telerik:RadDropDownList ID="ddlUnit" runat="server"  Width="70px" ValidationGroup="AddEdit">
                        </telerik:RadDropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">Effective Date</td>
                    <td class="auto-style11">:</td>
                    <td class="auto-style6">
                        <telerik:RadDatePicker ID="dtpDate" runat="server">
                        </telerik:RadDatePicker>
                        <asp:RequiredFieldValidator ID="rqvEffectiveDate" runat="server" ControlToValidate="dtpDate" CssClass="errorMessage" ErrorMessage="&lt;b&gt;Effective Date&lt;/b&gt; must be specified" SetFocusOnError="True" ValidationGroup="AddEdit" EnableTheming="True" EnableViewState="False"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">Price</td>
                    <td class="auto-style11">:</td>
                    <td class="auto-style6">
                        <telerik:RadNumericTextBox ID="ntbUnitPrice" runat="server" Width="180px" MinValue="0" ValidationGroup="AddEdit">
                        </telerik:RadNumericTextBox>
                        &nbsp;<asp:CustomValidator ID="cuvPrice" runat="server" ControlToValidate="ntbUnitPrice" CssClass="errorMessage" EnableViewState="False" ErrorMessage="&lt;b&gt;Price&lt;/b&gt; must be specified and greater than zero" OnServerValidate="cuvPrice_ServerValidate" SetFocusOnError="True" ValidateEmptyText="True" ValidationGroup="AddEdit"></asp:CustomValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">&nbsp;</td>
                    <td class="auto-style11">&nbsp;</td>
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
