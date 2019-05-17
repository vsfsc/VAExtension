<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LA_My.aspx.cs" Inherits="LearningActivity.Layouts.LearningActivity.LA_My" DynamicMasterPageFile="~masterurl/default.master" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
   
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>


<%--    <script type="text/javascript" src="my/js/jquery.min.js"></script>
    <script type="text/javascript" src="my/js/jquery-ui-1.8.16.custom.min.js"></script>
    <script type="text/javascript" src="my/js/scripts.js"></script>--%>


    <link rel="stylesheet" type="text/css" href="CSS/style.css" />
    <link rel="stylesheet" type="text/css" href="CSS/base.css" />
    <link rel="stylesheet" type="text/css" href="CSS/page.css" />
    <%--<script src="http://code.jquery.com/jquery-1.6.3.min.js"></script>--%>
    <script type="text/javascript">
        function showdiv() {
            document.getElementById("bg").style.display = "block";
            document.getElementById("show").style.display = "block";
        }

        function hidediv() {
            document.getElementById("bg").style.display = 'none';
            document.getElementById("show").style.display = 'none';
        }

    </script>
    <script type="text/javascript">
        function altRows(id) {
            if (document.getElementsByTagName) {

                var table = document.getElementById(id);
                var rows = table.getElementsByTagName("tr");

                for (i = 0; i < rows.length; i++) {
                    if (i % 2 == 0) {
                        rows[i].className = "evenrowcolor";
                    } else {
                        rows[i].className = "oddrowcolor";
                    }
                }
            }
        }

        window.onload = function () {
            altRows('alternatecolor');
        }
    </script>
    <!-- CSS goes in the document HEAD or added to your external stylesheet -->
    <style type="text/css">
        /*标签切换css*/

.content {
    padding: 5px;
}

.tabs {
    width: 600px;
    float: none;
    list-style: none;
    position: relative;
    text-align: left;
}

.tabs li {
    float: left;
    display: block;
}

.tabs input[type="radio"] {
    position: absolute;
    top: -9999px;
    left: -9999px;
}

.tabs label {
    display: block;
    padding: 5px 20px;
    border-radius: 15px 15px 0 0;
    font-size: 20px;
    font-weight: bold;
    background: #424242;
    color: #ffffff;
    cursor: pointer;
    position: relative;
    top: 10px;
    -webkit-transition: all 0.2s ease-in-out;
    -moz-transition: all 0.2s ease-in-out;
    -o-transition: all 0.2s ease-in-out;
    transition: all 0.2s ease-in-out;
}

.tabs label:hover {
    background: #cccccc;
    color: royalblue;
}

.tabs .tab-content {
    z-index: 2;
    display: none;
    overflow: hidden;
    width: 1000px;
    height: 500px;
    overflow-y: auto;
    font-size: 15px;
    line-height: 20px;
    padding: 5px;
    position: absolute;
    top: 45px;
    left: 0;
    background: #cccccc;
}

.tabs [id^="tab"]:checked + label {
    top: 0;
    padding-top: 15px;
    background: #cccccc;
    color: blue;
}

.tabs [id^="tab"]:checked ~ [id^="tab-content"] {
    display: block;
}


/* ------------------------------------------------- */


        /*表格css*/
        table.altrowstable {
            font-family: verdana,arial,sans-serif;
            font-size:11px;
            color:#333333;
            border-width: 1px;
            width: 100%;
            border-color: #a9c6c9;
            border-collapse: collapse;
        }
        table.altrowstable th {
            border-width: 1px;
            padding: 8px;
            border-style: solid;
            border-color: #a9c6c9;
        }
        table.altrowstable td {
            border-width: 1px;
            padding: 8px;
            border-style: solid;
            border-color: #a9c6c9;
        }
        .oddrowcolor{
            background-color:#d4e3e5;
        }
        .evenrowcolor{
            background-color:#c3dde0;
        }

        
        /*弹出遮罩*/
        #bg {
            display: none;
            position: absolute;
            top: 0%;
            left: 0%;
            width: 100%;
            height: 100%;
            background-color: black;
            z-index: 1001;
            -moz-opacity: 0.7;
            opacity: .70;
            filter: alpha(opacity=70);
        }
        
        #show {
            display: none;
            position: absolute;
            top: 25%;
            left: 22%;
            width: 53%;
            height: 49%;
            padding: 8px;
            border: 8px solid #E8E9F7;
            background-color: white;
            z-index: 1002;
            overflow: auto;
        }

        /*全局css*/
        .WorksMain {
            background-color:whitesmoke;
            margin-left: 10px;
            border-radius: 5px;
            border: 1px solid #CCCCCC;
            -moz-border-radius:5px;
            -webkit-border-radius: 5px;
            overflow-x: hidden;
            overflow-y: auto;
        }
        .yuanjiaobtn {
            font-family: Arial;
            border: 1px solid #379082;
            border-radius: 5px 5px 5px 5px;
            background-color: #999999;
            cursor: pointer;
            font-size: 16px;
            font-weight: bold;
            color: blue;
        }
        .divSingle {
            background-color:#F5FFFA;
            margin: 5px 5px 5px 10px;
            padding: 5px 5px 5px 5px;
            border-radius: 10px;
            border: 1px solid #CCCCCC;
            -moz-border-radius:5px;
            -webkit-border-radius: 5px;
        }
        .divSingle:hover {
            border:#379082 solid 1px;
        }
        .divDouble {
            background-color:#CAE1FF;
            margin: 5px 5px 5px 10px;
            padding: 5px 5px 5px 5px;
            border-radius: 10px;
            border: 1px solid #CCCCCC;
            -moz-border-radius:5px;
            -webkit-border-radius: 5px;
        }
        .divDouble:hover {
            border:#379082 solid 1px;
        }
        .myspan1 {
            padding-left: 18px; 
            font-size: 16px;
            font-weight: bold;
            font-family:Arial ;
            display:-moz-inline-box; 
            display:inline-block; 

        }
        .myspan2 {
            padding-left: 20px;
            padding-right: 20px; 
            font-size: 14px;
            font-weight: bold;
            font-family:微软雅黑 ;
            display:-moz-inline-box; 
            display:inline-block;  
        }
    </style>
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server" Visible="False">
   <%-- <input id="btnshow" type="button" value="Show" onclick="showdiv();" />
    <asp:Button ID="showReport" runat="server" Text="查看报表" OnClick="showReport_OnClick"/>
    <div id="bg" runat="server"></div>
    <div id="show" runat="server">
       
        <input id="btnclose" type="button" value="Close" onclick="hidediv();" />
    </div>--%>
    <div class="divDouble" id="nolaDiv" runat="server" Visible="False">
        <div class='wt'  style="color: red;width:400px"><h2>尚未记录任何活动</h2></div>
        <div><asp:Button ID="btnnew1" runat="server" Text="创建活动" OnClick="addActivity_OnClick"
            CssClass="yuanjiaobtn" ToolTip="添加新的活动记录" /></div>
    </div>
    <div class="content" id="mycontent" runat="server">
        <ul class="tabs">

            <li> <%--活动记录列表--%>
                <input type="radio" name="tabs" id="tab1" checked />
                <label for="tab1">记录列表</label>
                <div id="tab-content1" class="tab-content">
                    <div style="margin-top: 10px; margin-bottom: 5px;">
                        <asp:Button ID="addActivity" runat="server" Text="新增活动" OnClick="addActivity_OnClick"
                            CssClass="yuanjiaobtn" ToolTip="添加新的活动记录" />
                    </div>

                    <div id="myat" runat="server">
                       
                    </div>

                </div>
            </li>
            <li> <%--活动记录统计1:周期总量统计--%>
                <input type="radio" name="tabs" id="tab2" />
                <label for="tab2">记录汇总表</label>
                 <div id="tab-content2" class="tab-content">

                    <div id="stat1" runat="server">
                    </div>
                </div>
            </li>

            <li><%--数据统计仪表板--%>
                <input type="radio" name="tabs" id="tab3" />
                <label for="tab3">统计报表</label>
                <div id="tab-content3" class="tab-content">
                    <div class="WorksMain" id="DateDiv">
                        <div style="margin: 5px 5px 5px 10px; padding: 5px 5px 5px 5px;">
                            <h2>选择时间区间</h2>
                        </div>
                        <table>
                            <tr>
                                <td>开始日期:
                                </td>
                                <td>
                                    <SharePoint:DateTimeControl ID="startDT" runat="server" AutoPostBack="True" DateOnly="True"
                                        OnDateChanged="startDT_OnDateChanged" />
                                </td>
                                <td>截止日期:
                                </td>
                                <td>
                                    <SharePoint:DateTimeControl ID="endDT" runat="server" AutoPostBack="True" OnDateChanged="endDT_OnDateChanged"
                                        DateOnly="True" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <!--startprint-->
                    <div class="WorksMain" id="DayDiv">
                        <asp:Chart ID="ctDay" runat="server" BorderlineDashStyle="Solid" BorderlineColor="Gray"
                            Width="600px" BackGradientStyle="DiagonalLeft" BackSecondaryColor="AliceBlue"
                            BackColor="WhiteSmoke">
                            <Series>
                                <asp:Series Name="我的累计时长" ChartType="Column" LabelToolTip="#VALX" IsValueShownAsLabel="true"
                                    CustomProperties="DrawingStyle=Cylinder, MinPixelPointWidth=20, MaxPixelPointWidth=35, PointWidth=0.3"
                                    IsXValueIndexed="False" ShadowOffset="1" Legend="Lgd1" ChartArea="ca1">
                                </asp:Series>
                                <asp:Series Name="所有用户累计时长" ChartType="Column" LabelToolTip="#VALX" IsValueShownAsLabel="true"
                                    CustomProperties="DrawingStyle=Cylinder, MinPixelPointWidth=20, MaxPixelPointWidth=35, PointWidth=0.3"
                                    IsXValueIndexed="False" ShadowOffset="1" Legend="Lgd1" ChartArea="ca1">
                                </asp:Series>
                                <asp:Series Name="我的累计记录数" ChartType="Column" LabelToolTip="#VALX" IsValueShownAsLabel="true"
                                    CustomProperties="DrawingStyle=Cylinder, MinPixelPointWidth=20, MaxPixelPointWidth=35, PointWidth=0.3"
                                    IsXValueIndexed="False" ShadowOffset="1" Legend="Lgd1" ChartArea="ca2">
                                </asp:Series>
                                <asp:Series Name="所有用户累计记录数" ChartType="Column" LabelToolTip="#VALX" IsValueShownAsLabel="true"
                                    CustomProperties="DrawingStyle=Cylinder, MinPixelPointWidth=20, MaxPixelPointWidth=35, PointWidth=0.3"
                                    IsXValueIndexed="False" ShadowOffset="1" Legend="Lgd1" ChartArea="ca2">
                                </asp:Series>
                            </Series>
                            <Legends>
                                <asp:Legend Name="Lgd1" BackColor="Transparent" Docking="Bottom" Alignment="Center" />
                                <%--<asp:Legend Name="Lgd2" BackColor="Transparent" Docking="Top" Alignment="Far" DockedToChartArea="ca2" />--%>
                            </Legends>
                            <ChartAreas>
                                <asp:ChartArea Name="ca1" BackColor="White" BackSecondaryColor="Azure" BackGradientStyle="DiagonalLeft"
                                    ShadowOffset="2">
                                    <AxisY Title="时长:分钟">
                                        <MajorGrid LineColor="LightSlateGray" LineDashStyle="Dash" />
                                    </AxisY>
                                    <AxisX Title="日期">
                                        <MajorGrid Enabled="False" />
                                        <LabelStyle Font="Microsoft Sans Serif, 8pt" />
                                    </AxisX>
                                </asp:ChartArea>
                                <asp:ChartArea Name="ca2" BackColor="White" BackSecondaryColor="Azure" BackGradientStyle="DiagonalLeft"
                                    ShadowOffset="2">
                                    <AxisY Title="记录数:条">
                                        <MajorGrid LineColor="LightSlateGray" LineDashStyle="Dash" />
                                    </AxisY>
                                    <AxisX Title="日期">
                                        <MajorGrid Enabled="False" />
                                        <LabelStyle Font="Microsoft Sans Serif, 8pt" />
                                    </AxisX>
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                    </div>
                    <div class="WorksMain" id="LocaDiv">
                        <asp:Chart ID="ctLoca" runat="server" BorderlineDashStyle="Solid" BorderlineColor="Gray"
                            Width="600px" BackGradientStyle="DiagonalLeft" BackSecondaryColor="AliceBlue"
                            BackColor="WhiteSmoke">
                            <Series>
                                <asp:Series Name="分地点活动记录次数统计" ChartType="Pie" LabelToolTip="#VALX #VAL{N}次" IsValueShownAsLabel="true"
                                    CustomProperties="DrawingStyle=Cylinder, MinPixelPointWidth=20, MaxPixelPointWidth=35, PointWidth=0.3"
                                    IsXValueIndexed="False" ShadowOffset="1" ChartArea="ca1" Legend="Lgd1">
                                </asp:Series>
                                <asp:Series Name="分地点活动累计时长统计" ChartType="Pie"
                                    LabelToolTip="#VALX #VAL{N}分钟" IsValueShownAsLabel="true" CustomProperties="DrawingStyle=Cylinder, MinPixelPointWidth=20, MaxPixelPointWidth=35, PointWidth=0.3"
                                    IsXValueIndexed="False" ShadowOffset="1" Legend="Lgd2" ChartArea="ca2">
                                </asp:Series>
                            </Series>
                            <Legends>
                                <asp:Legend Name="Lgd1" BackColor="Transparent" Docking="Bottom" Alignment="Center" />
                                <asp:Legend Name="Lgd2" BackColor="Transparent" Docking="Top" Alignment="Center"
                                    Enabled="False" />
                            </Legends>
                            <ChartAreas>
                                <asp:ChartArea Name="ca1" BackColor="White" BackSecondaryColor="Azure" BackGradientStyle="DiagonalLeft"
                                    ShadowOffset="2">
                                    <AxisY>
                                        <MajorGrid LineColor="LightSlateGray" LineDashStyle="Dash" />
                                    </AxisY>
                                    <AxisX>
                                        <MajorGrid Enabled="False" />
                                        <LabelStyle Font="Microsoft Sans Serif, 8pt" />
                                    </AxisX>
                                    <Position Height="100" Width="50" />
                                    <InnerPlotPosition Height="100" Width="50" X="30" />
                                </asp:ChartArea>
                                <asp:ChartArea Name="ca2" BackColor="White" BackSecondaryColor="Azure" BackGradientStyle="DiagonalLeft"
                                    ShadowOffset="2">
                                    <AxisY>
                                        <MajorGrid LineColor="LightSlateGray" LineDashStyle="Dash" />
                                    </AxisY>
                                    <AxisX>
                                        <MajorGrid Enabled="False" />
                                        <LabelStyle Font="Microsoft Sans Serif, 8pt" />
                                    </AxisX>
                                    <Position Height="100" Width="50" X="50" />
                                    <InnerPlotPosition Height="100" Width="50" X="30" />
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                    </div>
                    <div class="WorksMain" id="TypeDiv">
                        <asp:Chart ID="ctType" runat="server" BorderlineDashStyle="Solid" BorderlineColor="Gray"
                            Width="600px" BackGradientStyle="DiagonalLeft" BackSecondaryColor="AliceBlue"
                            BackColor="WhiteSmoke">
                            <Series>
                                <asp:Series Name="分类别活动记录次数统计" ChartType="Pie"
                                    LabelToolTip="#VALX #VAL{N}次" IsValueShownAsLabel="true" CustomProperties="DrawingStyle=Cylinder, MinPixelPointWidth=20, MaxPixelPointWidth=35, PointWidth=0.3"
                                    IsXValueIndexed="False" ShadowOffset="1" Legend="Lgd1" ChartArea="ca1">
                                </asp:Series>
                                <asp:Series Name="分类别活动累计时长统计" ChartType="Pie"
                                    LabelToolTip="#VALX #VAL{N}分钟" IsValueShownAsLabel="true" CustomProperties="DrawingStyle=Cylinder, MinPixelPointWidth=20, MaxPixelPointWidth=35, PointWidth=0.3"
                                    IsXValueIndexed="False" ShadowOffset="1" Legend="Lgd2" ChartArea="ca2">
                                </asp:Series>
                            </Series>
                            <Legends>
                                <asp:Legend Name="Lgd1" BackColor="Transparent" Docking="Bottom" Alignment="Center" />
                                <asp:Legend Name="Lgd2" BackColor="Transparent" Docking="Top" Alignment="Center"
                                    Enabled="False" />
                            </Legends>
                            <ChartAreas>
                                <asp:ChartArea Name="ca1" BackColor="White" BackSecondaryColor="Azure" BackGradientStyle="DiagonalLeft"
                                    ShadowOffset="2">
                                    <AxisY>
                                        <MajorGrid LineColor="LightSlateGray" LineDashStyle="Dash" />
                                    </AxisY>
                                    <AxisX>
                                        <MajorGrid Enabled="False" />
                                        <LabelStyle Font="Microsoft Sans Serif, 8pt" />
                                    </AxisX>
                                    <Position Height="100" Width="50" />
                                    <InnerPlotPosition Height="100" Width="50" X="30" />
                                </asp:ChartArea>
                                <asp:ChartArea Name="ca2" BackColor="White" BackSecondaryColor="Azure" BackGradientStyle="DiagonalLeft"
                                    ShadowOffset="2">
                                    <AxisY>
                                        <MajorGrid LineColor="LightSlateGray" LineDashStyle="Dash" />
                                    </AxisY>
                                    <AxisX>
                                        <MajorGrid Enabled="False" />
                                        <LabelStyle Font="Microsoft Sans Serif, 8pt" />
                                    </AxisX>
                                    <Position Height="100" Width="50" X="50" />
                                    <InnerPlotPosition Height="100" Width="50" X="30" />
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                    </div>
                    <!--endprint-->
                    <div align="center" style="margin: 5px 0 5px 0">
                        <input id="btnPrint" type="button" value="打印报表" onclick="doPrint()" />
                    </div>
                    
                </div>
            </li>
        </ul>
    </div>
   
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
5Alearning学习活动记录
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
我的学习活动
</asp:Content>
