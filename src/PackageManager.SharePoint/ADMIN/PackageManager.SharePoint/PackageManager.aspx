<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PackageManager.aspx.cs" Inherits="PackageManager.SharePoint.Layouts.PackageManager.SharePoint.PackageManagerPage" DynamicMasterPageFile="~masterurl/default.master" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="AdminControls" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint.ApplicationPages.Administration" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">

</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    	<table width="100%" cellpadding="0" cellspacing="0" style="padding: 5px 10px 10px 0px;">
		   <tr>
				<td valign="top" width="70%">
						<AdminControls:AdminControlPanel ID="ControlPanel" runat="server" CellPadding="4" CellSpacing="4" Columns="1" Location="PackageManager.SharePoint.Page"
								LinkSectionControl="LinkSectionLevel1.ascx" />
						&#160;
						<WebPartPages:WebPartZone runat="server" FrameType="TitleBarOnly" ID="Left" Title="loc:Left" />
						&#160;
				</td>
				<td>&#160;</td>
				<td valign="top" width="30%">
						<WebPartPages:WebPartZone runat="server" FrameType="TitleBarOnly" ID="Right" Title="loc:Right" />
						&#160;
				</td>
				<td>&#160;</td>
		  </tr>
	</table>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
Package Manager
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
Package Manager
</asp:Content>
