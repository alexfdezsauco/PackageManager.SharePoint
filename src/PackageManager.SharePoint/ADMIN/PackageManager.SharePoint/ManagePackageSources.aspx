<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManagePackageSources.aspx.cs" Inherits="PackageManager.SharePoint.Layouts.PackageManager.SharePoint.ManagePackageSourcesPage" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <script type="text/javascript" src="Scripts/PackageManager.js"></script>
</asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <a href="javascript:newPackageSource();">Create New</a>
    <SharePoint:SPGridView runat="server" ID="PackageSourceSPGridView" AutoGenerateColumns="False" DataSourceID="PackageSourcesDataSource" DataKeyNames="id">
        <AlternatingRowStyle CssClass="ms-alternatingstrong" />
        <Columns>
            <asp:CommandField ButtonType="Image" ShowEditButton="true" ShowDeleteButton="True" EditImageUrl="/_layouts/15/IMAGES/PackageManager.SharePoint/edit.gif" UpdateImageUrl="/_layouts/15/IMAGES/PackageManager.SharePoint/save.gif" DeleteImageUrl="/_layouts/15/IMAGES/PackageManager.SharePoint/delete.gif" CancelImageUrl="/_layouts/15/IMAGES/PackageManager.SharePoint/cancel.gif"/>
            <asp:TemplateField HeaderText="">
                <ItemTemplate>
                    <asp:HiddenField ID="id" runat="server" Value='<%# Bind("id") %>'/>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Name">
                <ItemTemplate>
                    <asp:Label ID="name1" runat="server" Text='<%# Bind("name") %>'/>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="name2" runat="server" Text='<%# Bind("name") %>'/>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Source">
                <ItemTemplate>
                    <asp:Label ID="url1" runat="server" Text='<%# Bind("source") %>'/>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="url2" runat="server" Text='<%# Bind("source") %>'/>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Enabled">
                <ItemTemplate>
                    <asp:CheckBox Enabled="False" ID="enabled2" runat="server" Checked='<%# Bind("enabled") %>'/>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:CheckBox ID="enabled2" runat="server" Checked='<%# Bind("enabled") %>'/>
                </EditItemTemplate>
            </asp:TemplateField>
        </Columns>
    </SharePoint:SPGridView>

    <asp:ObjectDataSource id="PackageSourcesDataSource" runat="server" SelectMethod="SelectPackageSources" UpdateMethod="UpdatePackageSource" DeleteMethod="DeletePackageSource">
        <deleteparameters>
            <asp:parameter name="id" type="Object"  />
          </deleteparameters>
    </asp:ObjectDataSource>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    Manage Package Sources
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server">
    Manage Package Sources
</asp:Content>