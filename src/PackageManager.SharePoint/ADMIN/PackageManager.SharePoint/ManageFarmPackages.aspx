<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageFarmPackages.aspx.cs" Inherits="PackageManager.SharePoint.Layouts.PackageManager.SharePoint.ManageFarmPackagesPage" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
</asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    
    <SharePoint:SPGridView runat="server" ID="PackageSourceSPGridView" AutoGenerateColumns="False" DataSourceID="PackagesDataSource" DataKeyNames="id">
        <Columns>
            <asp:TemplateField HeaderText="Id">
                <ItemTemplate>
                    <asp:Label ID="Id" runat="server" Text='<%# Bind("id") %>'/>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Installed">
                <ItemTemplate>
                    <asp:Label ID="installed" runat="server" Text='<%# Bind("installed") %>'/>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Installed Version">
                <ItemTemplate>
                    <asp:Label ID="installedVersion" runat="server" Text='<%# Bind("installedVersion") %>'/>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Available Version">
                <ItemTemplate>
                    <asp:Label ID="name1" runat="server" Text='<%# Bind("version") %>'/>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button runat="server" Text="Upgrade" Visible='<%# !Eval("version").Equals(Eval("installedVersion")) %>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </SharePoint:SPGridView>

    <asp:ObjectDataSource id="PackagesDataSource" runat="server" SelectMethod="SelectPackages">
    </asp:ObjectDataSource>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    Manage Farm Packages
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server">
    Manage Farm Packages
</asp:Content>