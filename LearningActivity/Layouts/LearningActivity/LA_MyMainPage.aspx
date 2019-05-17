<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LA_MyMainPage.aspx.cs" Inherits="LearningActivity.Layouts.LearningActivity.LA_MyMainPage" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <%-- 加载echart --%>
    <script type="text/javascript" src="Echarts/echarts/echarts.min.js" charset="utf-8"></script>
    <script type="text/javascript" src="JS/jquery-1.9.1.js"></script>
    <%--<script type="text/javascript" src="Echarts/js/jquery.min.js"></script>--%>
    <script type="text/javascript" src="Echarts/MyJSForEcharts/DrawEcharts3.0.js"></script>

    <%-- 加载样式 --%>
    <%--<link rel="stylesheet" type="text/css" href="CSS/animate.min.css" />
    <link rel="stylesheet" type="text/css" href="CSS/default2.css" />
    <link rel="stylesheet" type="text/css" href="CSS/style2.css" />--%>

    <%-- 时间选择器插件加载 --%>
    <%--<script type="text/javascript" src="Echarts/MyJSForEcharts/jquery.min.js"></script>--%>
    <link rel="Stylesheet" type="text/css" href="CSS/dateRange.css" />
    <script type="text/javascript" src="JS/dateRange.js"></script>
    <style type="text/css">
        .echartBorder {
            /*font-family: Consolas, Monaco, Courier New, Courier, monospace;
            font-size: 12px;*/
            background-color: #f9f9f9;
            border: 1px solid #D0D0D0;
            color: #002166;            
            margin: 14px 0 14px 0;
            padding: 12px 10px 12px 10px;
            display:none; 
            height:600px
        }
        #topDivID {
            background:#D0D0D0;
            border:2px solid #0094ff;
            padding:20px;
        }
    </style>

</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">    
    <div id="topDivID">
        <%-- 这些按钮会启动DrawEcharts3.0.js使用AJAX获取数据 --%>
        <input name="AWeek" type="button"  value="近7天活动" onclick="AWeek_Click()"/> 
        <input name="AMonth" type="button"  value="近30天活动" onclick="AMonth_Click()"/> 
        <input name="Custom" type="button"  value="自定义" onclick="Custom_Click()"/>  
        <br />    
        <div id="timect" style="display:none;padding:5px 0px 0px 10px">                              
            <div class="ta_date" id="div_date_demo3">
                <span class="date_title" id="date_demo3"></span>
                <a class="opt_sel" id="input_trigger_demo3" href="#">
                    <i class="i_orderd"></i>
                </a>
            </div>
            <div id="datePicker"></div>
            <br /> 
            <div id="dCon_demo3">
                <br />               
                开始时间：2013-04-14
                <br />
                结束时间：2013-04-14
            </div>             
        </div>
    </div>
    <div id="custom_bar"  class="echartBorder"></div>
    <div id="echartID_bar" class="echartBorder"></div>
    <div id="month_bar" class="echartBorder"></div>    
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
echart
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
echarts3.0测试
</asp:Content>
