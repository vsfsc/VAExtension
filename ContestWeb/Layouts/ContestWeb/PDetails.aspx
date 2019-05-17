<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PDetails.aspx.cs" Inherits="ContestWeb.Layouts.ContestWeb.PDetails" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <style type="text/css">
       .Pitem{
            padding-top:10px;
            font-size:15px;
			 float:left;
        }
        .PitemLabel{
            width:120px;
            font-weight:bold;
            float:left;
            display:block;
			text-align:right;
            font-family:'Microsoft YaHei UI'
        }
        .PitemContent{
            width:600px;
            float:right;
            display:block;
            font-family:'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;
        }
    </style>
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div id="PdetailDiv">
        <h2 style="padding-left:50px;display:none;">
            <span id="PTitle" runat="server">

            </span>
        </h2>
        <div id="itemCourse" class="Pitem">
            <span class="PitemLabel">
                所属课程：
            </span>
            <span class="PitemContent" id="PCourseTitle" runat="server">

            </span>
        </div>
        <div id="itemPublisher" class="Pitem">
            <span class="PitemLabel">
                发布人：
            </span>
            <span class="PitemContent" id="PPublisher" runat="server">

            </span>
        </div>
        <div id="itemState" class="Pitem">
            <span class="PitemLabel">
                当前赛程：
            </span>
            <span class="PitemContent" id="PState" runat="server">

            </span>
        </div>
        <div id="itemType" class="Pitem">
            <span class="PitemLabel">
                竞赛类别：
            </span>
            <span class="PitemContent" id="PType" runat="server">

            </span>
        </div>
        <div id="itemSaizhi" class="Pitem">
            <span class="PitemLabel">
                赛制规定：
            </span>
            <span class="PitemContent" id="PSaizhi" runat="server">

            </span>
        </div>
        
        <div id="itemMemberCount" class="Pitem">
            <span class="PitemLabel" id="lbforCount" runat="server">
                参赛人/团队数：
            </span>
            <span class="PitemContent" id="PMemberCount" runat="server">

            </span>
        </div>
        <div id="itemTimeSheet" class="Pitem">
            <span class="PitemLabel">
                赛程安排：
            </span>
            <span class="PitemContent" id="PTimeSheet" runat="server">

            </span>
        </div>
        <div id="itemReq" class="Pitem">
            <span class="PitemLabel">
                竞赛要求：
            </span>
            <span class="PitemContent" id="PReq" runat="server">

            </span>
        </div>
    </div>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
比赛详情
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
    <asp:Label ID="lbContestTitle" runat="server" Text="比赛详情"></asp:Label>
    <asp:ImageButton ID="SignupLink" runat="server" ImageUrl="images/sign-up.png" />
</asp:Content>
