<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true"  Inherits="ContestDll.Public" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
      <script language="javascript" type="text/javascript">
          function onft(e) {
              e.style.border = "1px #61a4da solid";
              e.style.color = "#000";
          }
          function onbt(e) {
              e.style.border = "1px #bebee1 solid";
              e.style.color = "#666";
          }
    </script>

    <style type="text/css">
        .h3css
        {
            font-family: 微软雅黑;
            color: #4141a4;
            text-decoration: underline;
        }
        .tdone
        {
            width: 80px;
            text-align: left;
            padding-left: 10px;
            color: #4a4a4a;
        }
        .txtcss
        {
            width: 200px;
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
            width: 320px;
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
        .grouptd
        {
            text-align: left;
            color: White;
            color: blue;
            height: 25px;
        }
    </style>
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div id="tableDiv">
        <table cellpadding="3" cellspacing="0" border="0" width="850px" style="font-size: 14px;
            color: #494b4c">
            <tr>
                <td class="tdone">
                    作品名称：
                </td>
                <td>
                    <asp:TextBox ID="txtWorksName" runat="server" CssClass="txtcss" onfocus="onft(this)"
                        onblur="onbt(this)" Width="410"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tdone">
                    作品编号：
                </td>
                <td>
                    <asp:TextBox ID="txtWorksCode" runat="server" CssClass="txtcss" onfocus="onft(this)"
                        onblur="onbt(this)" Width="410"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tdone">
                    作品类别：
                </td>
                <td>
                    <div style="float: left">
                    </div>
                    <div style="float: left">
                        <asp:DropDownList ID="ddlWorksType" runat="server" CssClass="txtdoenlista">
                        </asp:DropDownList>
                        &nbsp; &nbsp;
                        <asp:Button ID="btnSearch" runat="server" Text="查询" /></div>
                </td>
            </tr>
            <tr>
                <td colspan="2" height="3px">
                </td>
            </tr>
            <tr>
                <td colspan="2" style="border: 1px solid #999; background-color: #ffffe1">
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <asp:GridView ID="gvWorks" runat="server" AutoGenerateColumns="False"
                                    CellPadding="0" Font-Size="14px" ForeColor="#494B4C" GridLines="None" Width="100%"
                                    BorderStyle="None" CssClass="grouptd">
                                    <PagerSettings Mode="NumericFirstLast" />
                                    <Columns>
                                        <asp:BoundField HeaderText="作品编号" DataField="WorksCode">
                                            <ItemStyle Width="100px" Wrap="false"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="作品名称" DataField="WorksName">
                                            <ItemStyle Width="450px" Wrap="false"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="作品详情" ItemStyle-Width="85px">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnView" runat="server" CommandName="ViewDetail" Text="查看" CssClass="buttoncssb"
                                    BorderStyle="None" />
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Left" />

<ItemStyle Width="40px"></ItemStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle HorizontalAlign="Center" />
                                    <HeaderStyle BackColor="#494B4C" ForeColor="#E6E6E6" HorizontalAlign="Left" BorderStyle="None"
                                        CssClass="grouptd" Font-Size="Small" Wrap="False" />
                                    <AlternatingRowStyle HorizontalAlign="Left"></AlternatingRowStyle>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
作品展示页
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
作品展示页
</asp:Content>
