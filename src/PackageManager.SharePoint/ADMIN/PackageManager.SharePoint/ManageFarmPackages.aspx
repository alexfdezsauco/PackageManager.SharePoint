<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageFarmPackages.aspx.cs" Inherits="PackageManager.SharePoint.Layouts.PackageManager.SharePoint.ManageFarmPackagesPage" DynamicMasterPageFile="~masterurl/default.master" %>
<%@ Import Namespace="PackageManager.SharePoint.Helpers" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <SharePoint:ScriptLink ID="ScriptLink1" name="SP.js" runat="server" ondemand="false" localizable="false" loadafterui="true" />
    <script language="javascript" type="text/javascript">
        function showJobIsScheduledOrRunningStatusMessage() {
            var statusID = SP.UI.Status.addStatus("Package Manager:", "The solution package installer job is scheduled or running. Please wait and reload the page. Track the progress <a href='/_admin/ServiceRunningJobs.aspx' target='new'>here</a>.");
            SP.UI.Status.setStatusPriColor(statusID, 'blue');
        }
    </script>
</asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <SharePoint:SPGridView runat="server" ID="PackageSourceSPGridView" AutoGenerateColumns="False" DataSourceID="PackagesDataSource" DataKeyNames="id" OnRowCommand="PackageSourceSPGridView_OnRowCommand">
        <AlternatingRowStyle CssClass="ms-alternatingstrong" />
        <Columns>
            <asp:TemplateField HeaderText="Id">
                <ItemTemplate>
                    <asp:Label ID="Id" runat="server" Text='<%# Bind("id") %>' Visible='<%# !BindHelper.IsInstalled(Eval("installed")) %>'/>
                    <asp:HyperLink ID="Id2" runat="server" Text='<%# Bind("id") %>' NavigateUrl='<%# string.Format("/_admin/SolutionStatus.aspx?ItemName={0}", HttpUtility.UrlEncode(Eval("id").ToString()))%>' Visible='<%# BindHelper.IsInstalled(Eval("installed")) %>'/>
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
            <asp:TemplateField HeaderText="Action">
                <ItemTemplate>
                    <asp:Button runat="server" CommandArgument='<%# Eval("id") + ";" + Eval("version") %>' CommandName="Update" Text="Update" Visible='<%# BindHelper.IsInstalled(Eval("installed")) && BindHelper.GreaterThan(Eval("version"), Eval("installedVersion")) %>' Enabled='<%# !this.isJobScheduledOrRunning %>' />
                    <asp:Button runat="server" CommandArgument='<%# Eval("id") + ";" + Eval("version") %>' CommandName="Install" Text="Install" Visible='<%# !BindHelper.IsInstalled(Eval("installed")) %>' Enabled='<%# !this.isJobScheduledOrRunning %>'/>
                    <asp:Label runat="server" Visible='<%# Eval("version").Equals(Eval("installedVersion")) %>' ForeColor="#0072c6">Up to date</asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </SharePoint:SPGridView>
    <asp:ObjectDataSource id="PackagesDataSource" runat="server" SelectMethod="SelectPackages"/>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    Manage Farm Packages
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server">
    <span><a href="/_admin/PackageManager.SharePoint/PackageManager.aspx">Manage Farm Packages</a></span>
    <a href="javascript:;" id="ms-pageDescriptionDiv" class=" js-callout-launchPoint">
	  <span id="ms-pageDescriptionImage">&nbsp;</span>
	</a>
    <span class="ms-accessible" id="ms-pageDescription">
	    This page has a list of the Solutions Package installed in the farm and the available updates from Package Sources.
	</span>
</asp:Content>