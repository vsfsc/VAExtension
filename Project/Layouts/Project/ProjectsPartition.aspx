<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectsPartition.aspx.cs" Inherits="Project.Layouts.Project.ProjectsPartition" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
     <style type="text/css">
        .h3css
        {
            font-family: 微软雅黑;
            color: #4141a4;
            text-decoration: underline;
        }
        .tdone
        {
            width:80px;
            text-align:right;
            padding-right: 10px;
            color: #4a4a4a;
        }
        .txtcss
        {
            width: 300px;
            border: 1px #bebee1 solid;
            height: 25px;
            vertical-align: middle;
            line-height: 25px;
            padding: 0 10px;
            color: Black;
        }
        .txtdoenlist
        {
            width: 90px;
            height: 25px;
            line-height: 25px;
            vertical-align: middle;
            padding: 3px;
            border: 1px #bebee1 solid;
            color: #494b4c;
        }
        .txtdoenlista
        {
            width: 300px;
            height: 25px;
            line-height: 25px;
            vertical-align: middle;
            padding: 3px;
            border: 1px #bebee1 solid;
            color: #494b4c;
        }
        .buttoncss
        {
            width: 120px;
            height: 40px;
            background-color: #61a4da;
            color: #fff;
            font-family: 微软雅黑;
            font-size: 20px;
            border: 1px solid #3776a9;
            cursor: pointer;
        }
        .buttoncssa
        {
            width: 120px;
            height: 40px;
            background-color: #81c70e;
            color: #fff;
            font-family: 微软雅黑;
            font-size: 20px;
            border: 1px solid #679c11;
            cursor: pointer;
        }
        .buttoncssb
        {
            color: #4141a4;
            width: 50px;
            height: 25px;
            background: #fff;
            font-size: 14px;
            cursor: pointer;
            text-decoration: underline;
            float: left;
        }
        .buttonc
        {
            color: red;
            width: 50px;
            height: 25px;
            background: #fff;
            font-size: 14px;
            cursor: pointer;
            text-decoration: underline;
            float: left;
        }
        .grouptd
        {
            text-align: left;
            height: 25px;
            padding:0;
            margin:0;
        }
        #showSchoolDiv
        {
            width: 100%;
            height: 180px;
            border: 1px solid #ccc;
            color: #333333;
            line-height: 24px;
            text-align: left;
        }
        .hidden
        {
            display: none;
        }
        .buttoncssv
        {
            width: 100px;
            height: 40px;
            background-color: #666;
            color: #fff;
            font-family: 微软雅黑;
            font-size: 20px;
            border: 1px solid #3776a9;
            cursor: pointer;
        }
    </style>

    <script type="text/javascript" language="javascript">
        function checkboxCount(operate, name) {
            var controlIndex;
            var element;
            var numberofControls;
            numberofControls = document.forms[0].length;
            var checkBoxCount = 0;
            for (controlIndex = 0; controlIndex < numberofControls; controlIndex++) {
                element = document.forms[0][controlIndex];
                if (element.type == "checkbox" && element.name.indexOf(name) == 0) {
                    if (element.checked == true) {
                        checkBoxCount++;
                    }
                }
            }
            if (checkBoxCount == 0) {
                alert("请选中要" + operate + "的记录！");
                return false;
            }
            else {
                if (name == "gvExpert") {
                    if (confirm("确定要" + operate + "选中的记录？") == false)
                        return false;
                    else
                        return true;
                }
                else {
                    return true;
                }
            }
        }
    </script>
        <script type="text/javascript" language="javascript">
            function ShowNo()                        //隐藏两个层
            {
                document.getElementById("doing").style.display = "none";
                document.getElementById("divShow").style.display = "none";
            }
            function ShowYes()                        //隐藏两个层
            {
                document.getElementById("doing").style.display = "";
                document.getElementById("divShow").style.display = "";
            }
            function $(id) {
                return (document.getElementById) ? document.getElementById(id) : document.all[id];
            }
            function showFloat()                    //根据屏幕的大小显示两个层
            {
                var range = getRange();
                $('doing').style.width = range.width + "px";
                $('doing').style.height = range.height + "px";
                $('doing').style.display = "";
                document.getElementById("divShow").style.display = "";
            }
            function getRange()                      //得到屏幕的大小
            {
                var top = document.body.scrollTop;
                var left = document.body.scrollLeft;
                var height = document.body.clientHeight;
                var width = document.body.clientWidth;

                if (top == 0 && left == 0 && height == 0 && width == 0) {
                    top = document.documentElement.scrollTop;
                    left = document.documentElement.scrollLeft;
                    height = document.documentElement.clientHeight;
                    width = document.documentElement.clientWidth;
                }
                return { top: top, left: left, height: height, width: width };
            }
    </script>
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
     <!--加一个半透明层-->
    <div id="doing" style="filter:alpha(opacity=30);-moz-opacity:0.3;opacity:0.3;background-color:#000;width:100%;height:100%;z-index:1000;position: absolute;left:0;top:0;display:none;overflow: hidden;">
    </div>
    <!--加一个显示专家层-->
    <div id="divShow" style="border:solid 1px #898989;background:#fff;padding:10px;width:500px;z-index:1001; position: absolute; display:none;top:60%; left:60%;margin:-200px 0 0 -400px;">
            <div style="padding:3px 15px 3px 15px;text-align:left;vertical-align:middle;" >
                 <div>
                    <asp:GridView ID="gvExpert" style="width:470px"  runat="server" AutoGenerateColumns="False"
                    BorderStyle="None"   CssClass="grouptd"  GridLines="None"  >
                    <Columns>
                        <asp:TemplateField HeaderText="选择" ItemStyle-Width="40px" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate >
                                <asp:CheckBox ID="ckb0" runat="server" />
                            </ItemTemplate>

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<ItemStyle Width="40px"></ItemStyle>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="专家姓名" DataField="Name" >
                            <ItemStyle  Wrap="False"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="专家类别" DataField="WorksType">
                            <ItemStyle   Wrap="False"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="SchoolName" HeaderText="所在学校">
                            <ItemStyle Wrap="False" />
                        </asp:BoundField>
                    </Columns>
                      <HeaderStyle BackColor="#494B4C" ForeColor="#E6E6E6" HorizontalAlign="Left"
                        BorderStyle="None" CssClass="grouptd" />

<AlternatingRowStyle HorizontalAlign="Left"></AlternatingRowStyle>
                </asp:GridView>
                    <br />
                    </div>
                <div style="text-align:right; padding-right:20 " >
                <asp:Button ID="btnOk" runat="server" Text="确定"
                        OnClientClick="return checkboxCount('分组','gvExpert')"
                      />   <input type="button"  value ="关闭"  id="btnClose"   onclick="ShowNo()"  name="btnClose"  />
                </div>
            </div>
      </div>

    <table cellpadding="3" cellspacing="0" border="0" width="800px" style="font-size: 14px;
        color: #494b4c">
        <tr>
            <td class="tdone">
                <asp:Label ID="albl" runat="server" Text="项目名称："></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtWorksName" runat="server" CssClass="txtcss"></asp:TextBox>
            </td>
            <td class="tdone">
                <asp:Label ID="slbl" runat="server" Text="项目科目："></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlWorksType" runat="server" CssClass="txtdoenlista">
                </asp:DropDownList>

                </td>
        </tr>
        <tr>
            <td class="tdone">
                <asp:Label ID="Label1" runat="server" Text="参赛学校："></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlSchool" runat="server" CssClass="txtdoenlista">
                </asp:DropDownList>
            </td>
            <td class="tdone">

            </td>
            <td>

                <asp:Button ID="btnSearch" runat="server" Text="查询" />
                </td>
        </tr>
        <tr>
            <td colspan="4" height="5px">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="4" style="border: 1px solid #999; background-color: #ffffe1">
                <asp:GridView ID="gvWorks" runat="server" AutoGenerateColumns="False"
                    BorderStyle="None"   CssClass="grouptd"  GridLines="None"
                    AllowPaging="True" >
                    <PagerSettings Mode="NumericFirstLast" />
                    <Columns>

                        <asp:TemplateField HeaderText="选择" ItemStyle-Width="40px" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate >
                                <asp:CheckBox ID="ckb" runat="server" />
                            </ItemTemplate>

<HeaderStyle HorizontalAlign="Center" Wrap="False"></HeaderStyle>

<ItemStyle Width="40px" Wrap="False"></ItemStyle>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="作品编号" DataField="WorksCode" ItemStyle-Width="125px">
                            <ItemStyle Width="125px" Wrap="false"  ></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="作品类别" DataField="WorksTypeName">
                            <ItemStyle Width="160px" Wrap="false"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="作品名称" DataField="WorksName">
                            <ItemStyle Width="220px" Wrap="false"></ItemStyle>
                        </asp:BoundField>
                         <asp:BoundField HeaderText="参赛学校" DataField="SchoolName">
                            <ItemStyle Width="190px" Wrap="false"></ItemStyle>
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="查看详情" ControlStyle-Width="65px"  HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnView" runat="server" CommandName="ViewDetail" Text="查看详情" CssClass="buttoncssb"
                                    BorderStyle="None" />
                            </ItemTemplate>
                            <ControlStyle Width="60px"></ControlStyle>

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        </asp:TemplateField>
                    </Columns>
                      <PagerStyle HorizontalAlign="Center" />
                      <HeaderStyle BackColor="#494B4C" ForeColor="#E6E6E6" HorizontalAlign="Left"
                        BorderStyle="None" CssClass="grouptd" />

<AlternatingRowStyle HorizontalAlign="Left"></AlternatingRowStyle>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td colspan="4" height="5px">
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Button ID="btnGroup" runat="server" Text="分组" OnClientClick="return checkboxCount('分组','gvWorks')" CssClass="buttoncss"
                    BorderStyle="None" />
            </td>
        </tr>
        <tr>
            <td colspan="4" height="5px">
            </td>
        </tr>

    </table>
    <div></div>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
项目分组
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
项目分给指定的专家
</asp:Content>
