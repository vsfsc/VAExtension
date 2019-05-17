<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SmoothNavMenu.ascx.cs" Inherits="SmoothNavigationMenu.ControlTemplates.SmoothNavigationMenu.SmoothNavMenu" %>
<link rel="stylesheet" type="text/css" href="/_layouts/15/SmoothNavigationMenu/css/ddsmoothmenu.css" />
		<link rel="stylesheet" type="text/css" href="/_layouts/15/SmoothNavigationMenu/css/ddsmoothmenu-v.css" />

		<script type="text/javascript" src="/_layouts/15/SmoothNavigationMenu/js/jquery-1.8.2.min.js"></script>
		<script type="text/javascript" src="/_layouts/15/SmoothNavigationMenu/js/ddsmoothmenu.js">
			/***********************************************
			 * Smooth Navigational Menu- (c) Dynamic Drive DHTML code library (www.dynamicdrive.com)
			 * Please keep this notice intact
			 * Visit Dynamic Drive at http://www.dynamicdrive.com/dynamicindex1/ddsmoothmenu.htm for full source code
			 ***********************************************/
		</script>

		<script type="text/javascript">
			ddsmoothmenu.init({
				mainmenuid: "smoothmenu1", //menu DIV id
				orientation: 'h', //Horizontal or vertical menu: Set to "h" or "v"
				classname: 'ddsmoothmenu', //class added to menu's outer DIV
				//customtheme: ["#1c5a80", "#18374a"],
				contentsource: "markup" //"markup" or ["container_id", "path_to_menu_file"]
			})

			ddsmoothmenu.init({
				mainmenuid: "smoothmenu2", //Menu DIV id
				orientation: 'v', //Horizontal or vertical menu: Set to "h" or "v"
				classname: 'ddsmoothmenu-v', //class added to menu's outer DIV
				method: 'toggle', // set to 'hover' (default) or 'toggle'
				arrowswap: true, // enable rollover effect on menu arrow images?
				//customtheme: ["#804000", "#482400"],
				contentsource: "markup" //"markup" or ["container_id", "path_to_menu_file"]
			})
		</script>

<div id="smoothmenu1" class="ddsmoothmenu" style="width:100%;">
    <asp:Literal ID="menuContenLiteral" runat="server"></asp:Literal>
</div>