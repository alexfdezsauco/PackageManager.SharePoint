<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewPackageSource.aspx.cs" Inherits="PackageManager.SharePoint.Layouts.PackageManager.SharePoint.NewPackageSource" DynamicMasterPageFile="~masterurl/default.master" %>
<%@ Import Namespace="System.Web.DynamicData" %>
<%@ Import Namespace="System.Web.UI" %>
<%@ Import Namespace="System.Web.UI.WebControls" %>
<%@ Import Namespace="System.Web.UI.WebControls" %>
<%@ Import Namespace="System.Web.UI.WebControls.Expressions" %>
<%@ Import Namespace="System.Web.UI.WebControls.WebParts" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <table>
        <tr>
            <td>
                <asp:Label runat="server" Text="Name:"/>
            </td>
            <td>
                <asp:TextBox id="NameTextBox" runat="server" Width="335px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label runat="server" Text="Source:"/>
            </td>
            <td>
                <asp:TextBox id="SourceTextBox" runat="server" Width="335px"></asp:TextBox>
            </td>
        </tr>
    </table>
    <div style="bottom: 0; height: 40px; margin-top: 40px; position: absolute; right: 0; text-align: right;">
        <asp:Button ID="Button1" runat="server" Text="Ok" OnClick="Button1_OnClick"/>
        <asp:Button ID="Button2" runat="server" Text="Cancel" OnClick="Button2_OnClick"/>
    </div>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    New Package Source
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server">
    New Package Source
</asp:Content>