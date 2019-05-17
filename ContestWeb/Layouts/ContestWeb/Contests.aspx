<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" Inherits="ContestDll.Contests" DynamicMasterPageFile="~masterurl/default.master" %>

<%--所有比赛项目页面前台--%>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
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
      padding: 10px;
      font-size: 15px;
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
      padding: 5px;
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
    <asp:GridView ID="gvContests" runat="server"  PageSize="10" AutoGenerateColumns="False"
                CellPadding="0" GridLines="None" Height="30px" Width="100%" AllowPaging="true" PagerSettings-Mode="NumericFirstLast"
                PagerSettings-FirstPageText="首页" PagerSettings-NextPageText="下一页" PagerSettings-PreviousPageText="上一页"
                PagerSettings-LastPageText="尾页" OnPageIndexChanging="gvPeriods_PageIndexChanging" OnRowDataBound="gvPeriods_RowDataBound">
                <FooterStyle CssClass="GridViewFooterStyle" />
                <RowStyle CssClass="GridViewRowStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" HorizontalAlign="Center" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <HeaderStyle CssClass="GridViewHeaderStyle" HorizontalAlign="Center" />

                <EmptyDataTemplate>
                    <span style="font-family: 微软雅黑; font-size: 14px; color: red;">当前无任何竞赛开赛
                    </span>
                </EmptyDataTemplate>

                <Columns>
                    <asp:BoundField DataField="PeriodID" HeaderText="竞赛ID" ItemStyle-HorizontalAlign="Center" Visible="false"/>
                    <%--<asp:BoundField DataField="PName" HeaderText="PName" ItemStyle-HorizontalAlign="Center" />--%>
                    <asp:BoundField DataField="CourseName" HeaderText="竞赛名称" ItemStyle-HorizontalAlign="Center" />
                    <asp:TemplateField HeaderText="竞赛届次">
                        <ItemTemplate >
                            <asp:HyperLink ID="PNameLink" runat="server" Text='<%#Eval("PeriodTitle")%>' NavigateUrl='<%# "PDetails.aspx?PeriodID="+DataBinder.Eval(Container.DataItem,"PeriodID").ToString()%>' Target="_blank">                                
                            </asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="UserName" HeaderText="竞赛发布人" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="Created" HeaderText="发布时间" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="WorksTypeName" HeaderText="竞赛类别" ItemStyle-HorizontalAlign="Center" />
                    <asp:TemplateField HeaderText="竞赛届次">
                        <ItemTemplate >
                           <div style="text-align: center">
                               <asp:Label ID="lbState" runat="server" Text=""></asp:Label>
                           </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:HyperLinkField DataNavigateUrlFields="PeriodID" HeaderText="竞赛预览" Text="查 看" 
                       DataNavigateUrlFormatString="PDetails.aspx?PeriodID={0}" ItemStyle-HorizontalAlign="Center" Target="_blank"/>                  
                </Columns>
    </asp:GridView>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
竞赛列表
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
所有比赛项目
</asp:Content>
