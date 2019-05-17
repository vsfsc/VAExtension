<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LA_Public.aspx.cs" Inherits="LearningActivity.Layouts.LearningActivity.LA_Public" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <style type="text/css">
         .WorksMain {
            background-color:whitesmoke;
            margin-left: 10px;
            border-radius: 5px;
            border: 1px solid #CCCCCC;
            -moz-border-radius:5px;
            -webkit-border-radius: 5px;
        }
         .div_InputModel {
             display:inline-block;
             border: 1px solid #87ceeb;
             padding: 2px 3px 2px 3px;
             margin: 2px 3px 2px 3px;
             -moz-border-radius:2px;
            -webkit-border-radius: 2px;
             width: 150px;
             text-align: center;

         }
         .field
        {
            display:inline-block;
            width:20%;
            margin-top:5px;
            background-color:skyblue;
            text-align: center;
        }
        .myLink{
	cursor:pointer;
	text-decoration:none;
	hide-focus: expression(this.hideFocus=true);
	outline:none;
}
.myLink:link,.myLink:visited,.myLink:active{
	text-decoration:none;
}
 .myLink:hover{
    text-decoration:none;
     background-color: #87ceeb;
}

.myLink:focus{
	outline:0; 
}
    </style>
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div id="myat" runat="server" class="WorksMain">
    </div>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
5Alearning学习活动记录
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
学习活动大厅
</asp:Content>
