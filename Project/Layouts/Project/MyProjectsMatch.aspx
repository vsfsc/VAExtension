<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyProjectsMatch.aspx.cs" Inherits="Project.Layouts.MyProjectsMatch" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <link rel="stylesheet" type="text/css" href="./css/base.css" />
    <link rel="stylesheet" type="text/css" href="./css/page.css" />

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

    
        <div class="comments-SubmitProfile-Titlebg">
            <div class="comments-SubmitProfile-Title">
                    <h2 style="color:yellow">对接信息</h2>
                </div>
        </div>
        <div class="comments-SubmitProfile-content" style="border: solid 1px #8b4513">
            <asp:GridView ID="gvMyProjMatch" runat="server" AllowPaging="true" PageSize="5" AutoGenerateColumns="False"
            CellPadding="0" GridLines="None" Height="30px" Width="100%" PagerSettings-Mode="NumericFirstLast"
            PagerSettings-FirstPageText="首页" PagerSettings-NextPageText="下一页" PagerSettings-PreviousPageText="上一页"
            PagerSettings-LastPageText="尾页" OnRowDataBound="gvMyProjMatch_RowDataBound">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <RowStyle CssClass="GridViewRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" HorizontalAlign="Center" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" HorizontalAlign="Center" />

            <EmptyDataTemplate>
                <span style="font-family: 微软雅黑; font-size: 14px; color: red;">当前没有任何对接信息
                </span>
            </EmptyDataTemplate>

                <Columns>
                    <asp:BoundField DataField="MatchID" HeaderText="对接ID" ItemStyle-HorizontalAlign="Center" Visible="False" />                         
                     <asp:BoundField DataField="Name" HeaderText="申请人" ItemStyle-HorizontalAlign="Center" />
                     <asp:BoundField DataField="EnterpriseName" HeaderText="企业名称" ItemStyle-HorizontalAlign="Center"/>
                     <asp:BoundField DataField="SendTime" HeaderText="申请时间" ItemStyle-HorizontalAlign="Center"/>
                     <asp:BoundField DataField="IsAccepted" HeaderText="申请状态" ItemStyle-HorizontalAlign="Center" />
                    <%--<asp:CommandField ShowSelectButton="True" SelectText="查看" HeaderText="详情"/>--%>
                    <asp:TemplateField HeaderText="详情" ItemStyle-Width="120px">
                    <ItemTemplate>
                        <asp:Button ID="btnView" CausesValidation="false" CommandName="ViewDetails" Text="查看"
                            runat="server"
                            BorderStyle="None" />
                    </ItemTemplate>
                </asp:TemplateField>
                </Columns>
         </asp:GridView>
    </div>
         <asp:HiddenField ID="hdMatchID" runat="server" />
        <br /><br /><br />
        <div id="divMatchDetail" runat="server" visible="false" class="comments-SubmitProfile-content" style="border: solid 1px #8b4513">           
             <asp:DetailsView ID="matcheDetails" runat="server" Height="30px" Width="100%" AutoGenerateRows="false" HeaderText="对接的详细信息" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" CellSpacing="2" CssClass="auto-style1" ForeColor="Black">

             <headerstyle backcolor="#5D7B9D" forecolor="White"/>
             <RowStyle BackColor="LightGray" ForeColor="Blue" Font-Names="宋体" Font-Size="10" Font-Italic="true"/>
             <AlternatingRowStyle BackColor="White" ForeColor="Blue" Font-Names="宋体" Font-Size="10" Font-Italic="true"/>
             <footerstyle forecolor="Red"  backcolor="LightBlue"  font-names="宋体"  font-size="10" font-bold="true"/>

                 <Fields>
                     <asp:BoundField DataField="Name" HeaderText="姓    名" ItemStyle-Width="90%" />
                     <asp:BoundField DataField="EnterpriseName" HeaderText="企业名称" />
                     <asp:BoundField DataField="Email" HeaderText="电子邮件" />
                     <asp:BoundField DataField="Telephone" HeaderText="联系电话" />
                     <asp:BoundField DataField="SendTime" HeaderText="请求时间" />
                     <asp:BoundField DataField="Description" HeaderText="情况说明" />
                 </Fields>               
             </asp:DetailsView>
            <br /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnAccepted" runat="server" Text="接受" Font-Size="Larger" Height="30" Width="50" OnClick="btnAccepted_Click" BackColor="#5D7B9D" /> &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnRefuse" runat="server" Text="拒绝" Font-Size="Larger" Height="30" Width="50" OnClick="btnRefuse_Click" BackColor="#5D7B9D" />
         </div>
   
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">

</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >

</asp:Content>
