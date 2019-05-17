<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" Inherits="ContestDll.MyPeriods" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <link rel="stylesheet" type="text/css" href="css/base.css" />
    <link rel="stylesheet" type="text/css" href="css/page.css" />
    <link rel="stylesheet" type="text/css" href="css/Combox.css" />
    <style type="text/css">
        #showDiv{ 
            margin:0;
            text-align:left;
            background-color:#fff;
            padding:10px 20px 5px;
        }
        .f {
            color:#FFFFFF; 
            text-align:center;
        }
        .f ul{ margin:0; padding:0;text-align:left;margin-left:10px;margin-top:15px;}
        .f ul li{ padding:2px 0;}
        .f ul li a{ font-size:13px; font-family:'微软雅黑'; color:#b7b7b7; text-decoration:none;}
        .f ul li a:hover{ color:#ff6633}
        .grouptd
        {
            text-align: left;
            height: 25px;
            padding:0;
            margin:0;
        }
        .WorksMain {
            width: 100%; 
            background-color:whitesmoke;
            margin-left: 10px;
            border-radius: 5px;
            border: 1px solid #CCCCCC;
            -moz-border-radius:5px;
            -webkit-border-radius: 5px;
            overflow-x: hidden;
            overflow-y: auto;
        }
        .buttoncssb
        {
            color: #4141a4;
            /*width: 50px;*/
            height: 25px;
            background: #fff;
            font-size: 14px;
            cursor: pointer;
            text-decoration: underline;
            float: left;
        }
        .liuyan{font-weight:bold; font-size:13px;}
        .fenshu{color:#f00}
        .neirong{ color:#666}
    </style>
    <style type="text/css">
        /*gridview样式*/
<!--
      .mGrid {    
   width: 100%;    
    background-color: #fff;    
    margin: 5px 0 10px 0;    
    border: solid 1px #525252;    
    border-collapse:collapse;    
}   
.mGrid td {    
    padding: 2px;    
    border: solid 1px #c1c1c1;    
    color: #717171;    
}   
.mGrid th {    
    padding: 4px 2px;    
   color: #fff;    
    background: #424242 url(grd_head.png) repeat-x top;    
    border-left: solid 1px #525252;    
    font-size: 0.9em;    
}   
.mGrid .alt { background: #fcfcfc url(grd_alt.png) repeat-x top; }   
.mGrid .pgr { background: #424242 url(grd_pgr.png) repeat-x top; }   
.mGrid .pgr table { margin: 5px 0; }   
.mGrid .pgr td {    
    border-width: 0;    
    padding: 0 6px;    
    border-left: solid 1px #666;    
    font-weight: bold;    
    color: #fff;    
    line-height: 12px;    
 }      
.mGrid .pgr a { color: #666; text-decoration: none; }   
.mGrid .pgr a:hover { color: #000; text-decoration: none; }  
    -->
</style>
    <script type="text/javascript" src="JS/jquery-1.9.1.min.js"></script>
 
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    
    <div>
        <b class="b1"></b><b class="b2"></b><b class="b3"></b><b class="b4"></b>
        <div class="boxcontent">
            <asp:DropDownList ID="ddlCourses" runat="server" Width="200px" AutoPostBack="True" Height="22px" OnSelectedIndexChanged="ddlCourses_OnSelectedIndexChanged"></asp:DropDownList>        
            <asp:Button runat="server" Text="重选" ID="clearSets" OnClick="clearSets_OnClick" Visible="False"></asp:Button>
        </div>
        <b class="b4b"></b><b class="b3b"></b><b class="b2b"></b><b class="b1b"></b>
    </div>
    <br/>
    <div>
        <asp:Label ID="error" CssClass="redStar" runat="server" Text=""></asp:Label>
    </div>
   
    <div id="PeriodsList" runat="server" >
        <div>
            <asp:Label ID="Label1" CssClass="redStar" runat="server" Text=""></asp:Label>
        </div>

        <asp:GridView ID="gvPeriod" runat="server" AllowPaging="False" AutoGenerateColumns="False"
            GridLines="None" Height="25px" Width="98%" CssClass="mGrid" border="0" CellPadding="0" CellSpacing="1" PagerStyle-CssClass="pgr"
            AlternatingRowStyle-CssClass="alt">
            <AlternatingRowStyle BackColor="#D9D9D9" />
            <EmptyDataTemplate>
                <table style="border: none">
                    <tr>
                        <td style="font-weight:bold;font-size: 15px;">友情提示:
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5" style="color: red">你当前还没有发布任何竞赛
                        </td>
                    </tr>
                </table>
            </EmptyDataTemplate>
            <%--<RowStyle BackColor="#EAEAEA" />--%>
             <Columns>
                <asp:BoundField DataField="PeriodID" HeaderText="届次ID">
                    <FooterStyle CssClass="hidden"/>
                    <HeaderStyle CssClass="hidden" />
                    <ItemStyle Width="60px" CssClass="hidden"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="CourseName" HeaderText="竞赛" >
                    <HeaderStyle HorizontalAlign="Center" Width="20%" />
                    <FooterStyle HorizontalAlign="Center"/>
                    <ItemStyle  HorizontalAlign="Left"/>
                </asp:BoundField>
                <asp:BoundField DataField="PeriodTitle" HeaderText="届次" >
                    <HeaderStyle HorizontalAlign="Center" Width="40%" />
                    <FooterStyle HorizontalAlign="Center"/>
                    <ItemStyle HorizontalAlign="Left"/>
                </asp:BoundField>
                 <asp:TemplateField HeaderText="当前状态">
                     <ItemTemplate>
                         <div style="width: 100px; align-items: center">
                             <asp:Label ID="lbPeriodState" runat="server" Text=""></asp:Label>
                         </div>
                     </ItemTemplate>
                     <HeaderStyle HorizontalAlign="Center" Width="100px" />
                     <FooterStyle HorizontalAlign="Center"></FooterStyle>
                     <ItemStyle HorizontalAlign="Center" />
                 </asp:TemplateField>
                <asp:BoundField DataField="WorksCount" HeaderText="作品数量" ItemStyle-Width="80px">
                    <HeaderStyle HorizontalAlign="Center" Width="80px"/>
                    <FooterStyle HorizontalAlign="Center"/>
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="样例作品">
                    <ItemTemplate>
                        <asp:Button ID="btnUpload" CausesValidation="false" CommandName="Upload" Text="上传" runat="server"
                            BorderStyle="None"  BackColor="Transparent" CssClass="buttoncssb"/>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="评价指标">
                    <ItemTemplate>
                        <asp:Button ID="btnStandard" CausesValidation="false" CommandName="Standard" Text="设置" runat="server"
                            BorderStyle="None"  BackColor="Transparent" CssClass="buttoncssb"/>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="80px"/>
                    <FooterStyle HorizontalAlign="Center"></FooterStyle>
                    <ItemStyle HorizontalAlign="Center"/>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="互评分组">
                    <ItemTemplate>
                        <asp:Button ID="btnAllot" CausesValidation="false" CommandName="Alloting" Text="分组" runat="server"
                            BorderStyle="None" BackColor="Transparent" CssClass="buttoncssb"/>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="80px"/>
                    <FooterStyle HorizontalAlign="Center"></FooterStyle>
                    <ItemStyle HorizontalAlign="Center"/>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="公布成绩" ItemStyle-Width="80px">
                    <ItemTemplate>
                        <asp:Button ID="btnScore" CausesValidation="false" CommandName="ComputingScore" Text="公布" runat="server"
                            BorderStyle="None" BackColor="Transparent" CssClass="buttoncssb"/>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <FooterStyle HorizontalAlign="Center"></FooterStyle>
                    <ItemStyle HorizontalAlign="Center"/>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="查看详情">
                    <ItemTemplate>
                        <asp:Button ID="btnEdit" CausesValidation="false" CommandName="Down" Text="查看" runat="server"
                            BorderStyle="None"  BackColor="Transparent" CssClass="buttoncssb"/>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="80px"/>
                    <FooterStyle HorizontalAlign="Center"></FooterStyle>
                    <ItemStyle HorizontalAlign="Center"/>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="关闭竞赛">
                    <ItemTemplate>
                        <asp:Button ID="btnDel" CausesValidation="false" CommandName="Del" Text="关闭" runat="server"
                            OnClientClick="javascript:return confirm('确定要关闭本次比赛吗?  关闭竞赛则不再显示,请谨慎操作!');"
                            BorderStyle="None" BackColor="Transparent" CssClass="buttoncssb" ToolTip="竞赛关闭后将不再显示,请谨慎操作" />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <FooterStyle HorizontalAlign="Center"></FooterStyle>
                    <ItemStyle HorizontalAlign="Center"/>
                </asp:TemplateField>
            </Columns>
            <HeaderStyle BackColor="#494B4C" ForeColor="#E6E6E6" HorizontalAlign="Left" />
            <PagerStyle HorizontalAlign="Center" />
        </asp:GridView>
    </div>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
竞赛届次管理
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
我发布的竞赛
</asp:Content>
