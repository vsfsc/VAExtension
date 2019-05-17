<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="testnav.aspx.cs" Inherits="LearningActivity.Layouts.EchartDemo.testnav" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <script type="text/javascript" src="../LearningActivity/JS/jquery.min.js"></script>
    <script type="text/javascript" src="../LearningActivity/JS/posfixed.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#example").posfixed({
                distance: 0,
                pos: "top",
                type: "while",
                hide: false
            });
        });
    </script>
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div id="div1" style="height:300px;background:#ff0000;margin:10px 0px 10px 0px">显示内容</div>
    <div id="div2" style="height:300px;background:#ffd800;margin:10px 0px 10px 0px;align-content:center">
        <div id="example" style="height:100px;width: 728px;background:#0b15cb;margin:10px 0px 10px 0px">show the context</div>
    </div>
    <div id="div3" style="height:300px;background:#00ff90;margin:10px 0px 10px 0px">显示内容</div>
    <div id="div4" style="height:300px;background:#0094ff;margin:10px 0px 10px 0px">显示内容</div>
    <div id="div5" style="height:300px;background:#000000;margin:10px 0px 10px 0px">显示内容</div>
    <div id="div6" style="height:300px;background:#00ff90;margin:10px 0px 10px 0px">显示内容</div>
    <div id="div7" style="height:300px;background:#0094ff;margin:10px 0px 10px 0px">显示内容</div>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
应用程序页
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
我的应用程序页
</asp:Content>
