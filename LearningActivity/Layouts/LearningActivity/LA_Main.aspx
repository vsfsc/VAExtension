<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LA_Main.aspx.cs" Inherits="LearningActivity.Layouts.LearningActivity.LA_Main" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1"/>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script type="text/javascript" src="JS/jquery.min.js"></script>
    <%-- 标签样式加载 --%>
    <link rel="stylesheet" type="text/css" href="CSS/style2.css" /> 
    
    <%-- 时间选择器插件加载 --%>
    <link rel="Stylesheet" type="text/css" href="CSS/dateRange.css" />
    <script type="text/javascript" src="JS/dateRange.js"></script> 

    <%-- echart插件加载 --%>
    <script type="text/javascript" src="Echarts/echarts/echarts.min.js" charset="utf-8"></script>
    <script type="text/javascript" src="Echarts/MyJSForEcharts/DrawEcharts3.0.js"></script>

    <%-- 顶部导航插件 --%>
    <script type="text/javascript" src="JS/posfixed.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#topDivID").posfixed({
                distance: 0,
                pos: "top",
                type: "while",
                hide: false
            });
        });
    </script>

    <style type="text/css">
        /*表格css*/
        table.altrowstable {
            font-family: verdana,arial,sans-serif;
            font-size:14px;
            color:#333333;
            border-width: 2px;
            width: 100%;
            border-color: #0088cc;
            border-collapse: collapse;
        }
        table.altrowstable th {
            border-width: 2px;
            padding: 8px;
            border-style: solid;
            border-color:#0088cc;
        }
        table.altrowstable td {
            border-width: 2px;
            padding: 8px;
            border-style: solid;
            border-color: #0088cc;
        }
        .oddrowcolor{
            background-color:#d4e3e5;
        }
        .evenrowcolor{
            background-color:#c3dde0;
        }

        /*echart表样式*/
        .echartBorder {
            background-color: #f9f9f9;
            border: 1px solid #D0D0D0;
            color: #002166;
            margin: 14px 0 14px 0;
            padding: 12px 10px 12px 10px;
            display: none;
            height: 600px;
        }
        .echartBorder2 {
            background-color: #f9f9f9;
            border: 1px solid #D0D0D0;
            color: #002166;
            margin: 14px 0 14px 0;
            padding: 12px 10px 12px 10px;
            display: none;
            height: 450px;
        }        
    </style>

</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
        
    <div class="main" id="nolaDiv" runat="server" Visible="False">
        <h2>尚未记录任何活动</h2>
        <div><asp:Button ID="btnnew1" runat="server" Text="创建活动" OnClick="addActivity_OnClick"
            CssClass="tab-button" ToolTip="添加新的活动记录" /></div>
    </div>
    <div class="main" id="tabsDiv" runat="server">
        <ul class="tabs">
            <li>
                <input type="radio" checked="checked" name="tabs" id="tab1"/>
                <label for="tab1">我的活动记录</label>
                <div id="tab-content1" class="tab-content animated fadeIn">
                    <%--<div id="divEnergyPie" style="width: 100%; height: 700px;"></div>--%>
                    <%--活动记录列表--%>                  
                    <asp:Button ID="addActivity" runat="server" Text="新增活动" OnClick="addActivity_OnClick"
                        CssClass="tab-button" ToolTip="添加新的活动记录" Visible="false" />
                    <asp:ImageButton ID="addActivityBtn" runat="server" ToolTip="添加新的活动记录"
                        OnClick="addActivityBtn_Click" ImageUrl="images/circle_plus.png" />
                    <div id="myat" runat="server">
                    </div>
                </div>
            </li>
            <li>
                <input type="radio" name="tabs" id="tab2" />
                <label for="tab2">学习情况统计表</label>
                <div id="tab-content2" class="tab-content animated fadeIn"> 
                     <div id="stat1" runat="server">
                    </div>             
                </div>
            </li>
            <li>
                <input type="radio" name="tabs" id="tab3" />
                <label for="tab3">学习情况可视化</label>
                <div id="tab-content3" class="tab-content animated fadeIn">  
                    <div id="topDivID" >
                        <div class="float_left_btn">
                            <%-- 这些按钮会启动DrawEcharts3.0.js使用AJAX获取数据 --%>
                            <input name="ByLocation" type="button" value="按地点" onclick="ByLocation_Click()" />
                            <input name="ByType" type="button" value="按类别" onclick="ByType_Click()" />
                            <input name="AWeek" type="button" value="近7天活动" onclick="AWeek_Click()" />
                            <input name="AMonth" type="button" value="近30天活动" onclick="AMonth_Click()" />
                            <input name="Custom" type="button" value="自定义" onclick="Custom_Click()" />                           
                        </div>

                        <%-- 自定义时间控件 --%>
                        <div id="timect" style="display: none; padding: 5px 10px 5px 10px; float: left">
                            <div class="ta_date" id="div_date1">
                                <span class="date_title" id="date1"></span>
                                <a class="opt_sel" id="input_trigger1" href="#">
                                    <i class="i_orderd"></i>
                                </a>
                            </div>
                            <div id="datePicker1"></div>
                            <br />
                        </div>
                    </div>
                    <%-- end topDivID --%>                                                       
                    <div id="echartID_bar" class="echartBorder"></div>
                    <div id="month_bar" class="echartBorder"></div>
                    <div id="custom_bar" class="echartBorder"></div>
                    <div id="location_pie" class="echartBorder2"></div>
                    <div id="type_pie" class="echartBorder2"></div>
                </div>
            </li>
        </ul>
    </div>


</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
5A for activities
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
</asp:Content>
