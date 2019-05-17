<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Statistics.aspx.cs" Inherits="LearningActivity.Layouts.LearningActivity.Statistics" DynamicMasterPageFile="~masterurl/default.master" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <style type="text/css">
         .WorksMain {
            background-color: #F0F0F0;
            margin-left: 10px;
            border-radius: 5px;
            border: 1px solid #CCCCCC;
            -moz-border-radius:5px;
            -webkit-border-radius: 5px;
            width:600px;
        }
    </style>
    <script type="text/javascript">
        function doPrint() { 
        bdhtml=window.document.body.innerHTML; 
        sprnstr="<!--startprint-->"; 
        eprnstr="<!--endprint-->"; 
        prnhtml=bdhtml.substr(bdhtml.indexOf(sprnstr)+17); 
        prnhtml=prnhtml.substring(0,prnhtml.indexOf(eprnstr)); 
        window.document.body.innerHTML=prnhtml; 
        window.print(); 
        }
    </script>
    <script type="text/javascript" src="./Echarts/js/echarts.js"></script>
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    
    <div class="WorksMain" id="DateDiv">
        <div style="margin: 5px 5px 5px 10px; padding: 5px 5px 5px 5px;">
            <h2>选择时间区间</h2>
            <asp:Button ID="btnBack" runat="server" Text="返回记录列表" OnClick="btnBack_OnClick"/>
            <input id="btnPrint" type="button" value="打印报表" onclick="doPrint()" />
        </div>
        <table>
            <tr>
                <td>
                    开始日期:
                </td>
                <td>
                    <SharePoint:DateTimeControl ID="startDT" runat="server" AutoPostBack="True" DateOnly="True" OnDateChanged="startDT_OnDateChanged" />
                </td>
                <td>
                    截止日期:
                </td>
                <td>
                    <SharePoint:DateTimeControl ID="endDT" runat="server" AutoPostBack="True" OnDateChanged="endDT_OnDateChanged" DateOnly="True" />
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
                <asp:Legend Name="Lgd1" BackColor="Transparent" Docking="Bottom" Alignment="Center"  />
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
                <asp:Series Name="分地点活动记录次数统计" ChartType="Pie" LabelToolTip="#VALX #VAL{N}次" IsValueShownAsLabel="true" CustomProperties="DrawingStyle=Cylinder, MinPixelPointWidth=20, MaxPixelPointWidth=35, PointWidth=0.3"
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
                <asp:Legend Name="Lgd1" BackColor="Transparent" Docking="Bottom" Alignment="Center"/>
                <asp:Legend Name="Lgd2" BackColor="Transparent" Docking="Top" Alignment="Center" Enabled="False"/>
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
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
活动统计
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
活动统计
</asp:Content>
