<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowEcharts.aspx.cs" Inherits="LearningActivity.Layouts.EchartDemo.ShowEchart" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <link rel="stylesheet" type="text/css" href="../LearningActivity/CSS/animate.min.css" />
    <link rel="stylesheet" type="text/css" href="../LearningActivity/CSS/default2.css"/>
    <link rel="stylesheet" type="text/css" href="../LearningActivity/CSS/style2.css" />

    
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <script type="text/javascript" src="../LearningActivity/JS/jquery-1.9.1.js"></script>
    <script type="text/javascript" src="../LearningActivity/Echarts/echarts/echarts.js"></script>
    <script type="text/javascript" src="../LearningActivity/Echarts/echarts/theme/macarons.js"></script>
    <script type="text/javascript" src="../LearningActivity/Echarts/MyJSForEcharts/DrawEChart.js"></script>

    <div class="main">
            <ul class="tabs">
                <li>
                    <input type="radio" checked="checked" name="tabs" id="tab1" />
                    <label for="tab1">测试图</label>
                    <div id="tab-content1" class="tab-content animated fadeIn">
                        <div id="divTest" style="width: 1000px; height: 700px;"></div>
                    </div>
                </li>
                <li>
                    <input type="radio" name="tabs" id="tab2" />
                    <label for="tab2">折柱</label>
                    <div id="tab-content2" class="tab-content animated fadeIn">
                        <%--<div id="divEnergy" style="width: 1000px; height: 650px;"></div>--%>
                    </div>
                </li>
                <li>
                    <input type="radio" name="tabs" id="tab3" />
                    <label for="tab3">tab 3</label>
                    <div id="tab-content3" class="tab-content animated fadeIn">
                          预留                    
                    </div>
                </li>
            </ul>
        </div>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
echarts
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
my echarts
</asp:Content>
