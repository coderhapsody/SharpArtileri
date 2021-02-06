<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MasterWorkspace.Master" AutoEventWireup="true" CodeBehind="ManageRolePrivilege.aspx.cs" Inherits="SharpArtileri.Web.ManageRolePrivilege" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <style type="text/css">
        #kiri {
            float: left;
            width: 300px;
        }

        #kanan {
            float: left;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainTitle" runat="server">
    Role Privilege
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphMainContent" runat="server">
            <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="tvwMenus">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cblPrivilege" />                    
                    <telerik:AjaxUpdatedControl ControlID="btnSave" />   
                    <telerik:AjaxUpdatedControl ControlID="lblStatusAddEdit" />   
                </UpdatedControls>
            </telerik:AjaxSetting>
            
             <telerik:AjaxSetting AjaxControlID="btnSave">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblStatusAddEdit" />   
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <%--<asp:Panel ID="pnlPanel" runat="server">--%>
    <div id="kiri">
        <asp:Label ID="lblRoleName" runat="server" />
        <telerik:RadTreeView runat="server" ID="tvwMenus" OnNodeClick="tvwMenus_NodeClick">            
        </telerik:RadTreeView>
    </div>
    <div id="kanan">
        <asp:CheckBoxList runat="server" ID="cblPrivilege" >
            <asp:ListItem Value="C">Add New</asp:ListItem>
            <asp:ListItem Value="U">Update</asp:ListItem>
            <asp:ListItem Value="D">Delete</asp:ListItem>
        </asp:CheckBoxList>
        <br/><br/>
        <telerik:RadButton runat="server" ID="btnSave" EnableViewState="False" Text="Save" OnClick="btnSave_Click" ValidationGroup="AddEdit"></telerik:RadButton>
        <br/><br/>
        <asp:HyperLink runat="server" ID="hypBackToRoles" Text="Back to Roles" CssClass="k-button" NavigateUrl="MasterRole.aspx"></asp:HyperLink>
        <br/>
        <br/>
        <asp:Label ID="lblStatusAddEdit" runat="server" EnableViewState="false" />
    </div>
        <%--</asp:Panel>--%>
</asp:Content>
