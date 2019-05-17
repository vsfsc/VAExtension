<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TopUsers.aspx.cs" Inherits="ContestWeb.Layouts.ContestWeb.TopUsers" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <link rel="stylesheet" type="text/css" href="css/detail.css"/>
    <link rel="stylesheet" type="text/css" href="css/common.css"/>
     <link rel="stylesheet" type="text/css" href="css/tianchi.css"/>
<style type="text/css">
    
    .GridViewStyle
{    
    border-right: 2px solid #A7A6AA;
    border-bottom: 2px solid #A7A6AA;
    border-left: 2px solid white;
    border-top: 2px solid white;
    padding: 4px;
}
  .GridViewStyle a
{
    color: #FFFFFF;
}
  .GridViewHeaderStyle th
{
    border-left: 1px solid #EBE9ED;
    border-right: 1px solid #EBE9ED;
}
  .GridViewHeaderStyle
{
    background-color: #5D7B9D;
    font-weight: bold;
    color: White;
}
  .GridViewFooterStyle
{
    background-color: #5D7B9D;
    font-weight: bold;
    color: White;
}
  .GridViewRowStyle
{
    background-color: #F7F6F3;
    color: #333333;
}
  .GridViewAlternatingRowStyle 
{
    background-color: #FFFFFF;
    color: #284775;
}
  .GridViewRowStyle td, .GridViewAlternatingRowStyle td
{
    border: 1px solid #EBE9ED;
}
  .GridViewSelectedRowStyle
{
    background-color: #E2DED6;
    font-weight: bold;
    color: #333333;
}
  .GridViewPagerStyle
{
    background-color: #a9a9a9;
    color: #FFFFFF;
}
  .GridViewPagerStyle table /* to center the paging links*/
{
    margin: 0 auto 0 auto;
}

</style>
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
     <div class="page-container">
        <div data-spm="111">
            <div class="science-list-box">
                <div class="d-row" id="divTopman" runat="server">
                </div>
             </div>
        </div>
         <div id="uInfoDiv" runat="server">
             
         </div>
         <div id="pagerDiv" runat="server">

         </div>
    </div>
       <div style="padding: 20px;">
           <asp:GridView ID="gvUInfo" runat="server" AutoGenerateColumns="False" CellPadding="0" GridLines="None" Height="30px" Width="80%"  ForeColor="#333333" AllowPaging="True" PagerSettings-Mode="NumericFirstLast"
            PagerSettings-FirstPageText="首页" PagerSettings-NextPageText="下一页" PagerSettings-PreviousPageText="上一页"
            PagerSettings-LastPageText="尾页" PageSize="20" OnPageIndexChanging="gvUInfo_OnPageIndexChanging">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <RowStyle CssClass="GridViewRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" HorizontalAlign="Center" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" HorizontalAlign="Center" />

            <EmptyDataTemplate>
                <span style="font-family: 微软雅黑; font-size: 14px; color: red;">成员排行榜没有数据
                </span>
            </EmptyDataTemplate>
        <Columns>
            <asp:BoundField DataField="UserID" HeaderText="UserID" Visible="False" />
            <asp:BoundField DataField="XuHao" HeaderText="排行" />
            <asp:ImageField DataImageUrlField="Account" DataImageUrlFormatString="http://va.neu.edu.cn/my/User%20Photos/Profile%20Pictures/{0}_SThumb.jpg" HeaderText="头像" NullImageUrl="./images/headgif.gif">
                <ControlStyle Height="32px" Width="32px" />
            </asp:ImageField>
            
            <asp:HyperLinkField DataNavigateUrlFields="UserID,SumScore,XuHao" DataNavigateUrlFormatString="MemberShow.aspx?userId={0}&sumScore={1}&Rank={2}" DataTextField="Name" HeaderText="用户名" />
            <asp:BoundField DataField="SchoolName" HeaderText="所在院校" />
            <asp:BoundField DataField="SumScore" HeaderText="个人总积分" />
            
        </Columns>
        <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#E9E7E2" />
        <SortedAscendingHeaderStyle BackColor="#506C8C" />
        <SortedDescendingCellStyle BackColor="#FFFDF8" />
        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
    </asp:GridView>
       </div>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
众创竞赛达人榜
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
众创竞赛达人榜
</asp:Content>
