<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true"   Inherits="ContestDll.PreliminaryEvaluate" DynamicMasterPageFile="~masterurl/default.master" %>

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
            text-align:left;
            padding-left:10px;
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
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
     <table cellpadding="3" cellspacing="0" border="0" style="font-size: 14px;
        color: #494b4c; width: 861px;">
      <tr><td colspan="2"><h3 class="h3css">专家初评</h3></td></tr>
      <tr><td class="tdone">作品名称：</td>
                <td >
                    <asp:TextBox ID="txtWorksName" runat="server" CssClass="txtcss" Width="300"></asp:TextBox>
                </td></tr>
      <tr><td class="tdone">作品类别：</td><td>
                <asp:DropDownList ID="ddlWorksType" runat="server" CssClass="txtdoenlista" Width="300">
                </asp:DropDownList>
                 &nbsp; &nbsp;<asp:Button ID="btnSearch" runat="server" Text="查询"/></td></tr>  
      <tr><td colspan="2" height="5px"></td></tr>        
      <tr><td colspan="2" style="border: 1px solid #999; background-color: #ffffe1">
      <asp:GridView ID="gvWorks" runat="server" AutoGenerateColumns="False" AllowPaging="True"
            BorderStyle="None"  CssClass="grouptd"  GridLines="None" 
            AlternatingRowStyle-HorizontalAlign="Left" HorizontalAlign="Left" 
              Width="853px">
            <PagerSettings Mode="NumericFirstLast" />
          <EmptyDataTemplate>
              <table>
                    <tr>
                        <td  >
                            没有满足条件的数据
                        </td>
                    </tr>
                </table>
          </EmptyDataTemplate>
            <Columns>
                <asp:BoundField HeaderText="作品名称" DataField="WorksName" ItemStyle-Width="180px" FooterStyle-HorizontalAlign="Left">
<FooterStyle HorizontalAlign="Left"></FooterStyle>

                    <ItemStyle Width="280px"></ItemStyle>
                </asp:BoundField>
               
                <asp:BoundField HeaderText="作品类别" DataField="WorksTypeName" FooterStyle-HorizontalAlign="Left">
<FooterStyle HorizontalAlign="Left"></FooterStyle>

                    <ItemStyle Width="250px"></ItemStyle>
                </asp:BoundField>
                 <asp:BoundField HeaderText="作品编号" DataField="WorksCode" FooterStyle-HorizontalAlign="Left">
<FooterStyle HorizontalAlign="Left"></FooterStyle>

                    <ItemStyle Width="110px"></ItemStyle>
                </asp:BoundField>
                <asp:TemplateField HeaderText="详细信息" ControlStyle-Width="60px" FooterStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnView" runat="server" CommandName="ViewDetail" Text="查看" CssClass="buttoncssb"
                            BorderStyle="None" />
                    </ItemTemplate>
                    <ControlStyle Width="60px"></ControlStyle>

<FooterStyle HorizontalAlign="Left"></FooterStyle>
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="评价" ControlStyle-Width="30px" FooterStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnScore" runat="server" CommandName="WorksScore" Text="评价" CssClass="buttoncssb"
                            BorderStyle="None" />
                    </ItemTemplate>
                    <ControlStyle Width="30px"></ControlStyle>

<FooterStyle HorizontalAlign="Left"></FooterStyle>
                </asp:TemplateField>
                 <asp:BoundField HeaderText="分数" DataField="Score" FooterStyle-HorizontalAlign="Left">
<FooterStyle HorizontalAlign="Left"></FooterStyle>

                    <ItemStyle Width="60px" ForeColor="#cc6f13"></ItemStyle>
                </asp:BoundField>
            </Columns>
            <PagerStyle HorizontalAlign="Center" />
            <HeaderStyle BackColor="#494B4C" ForeColor="#E6E6E6" HorizontalAlign="Left" />

<AlternatingRowStyle HorizontalAlign="Left"></AlternatingRowStyle>
        </asp:GridView>
      </td></tr>
     </table>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
专家初评
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
专家初评
</asp:Content>
