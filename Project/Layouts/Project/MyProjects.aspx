<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyProjects.aspx.cs" Inherits="Project.Layouts.Project.MyProjects" DynamicMasterPageFile="~masterurl/default.master" %>

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
    <link rel="stylesheet" type="text/css" href="./css/base.css" />
    <link rel="stylesheet" type="text/css" href="./css/page.css" />
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">

    <div class="comments-SubmitProfile"id="MyPubDiv"> <%--我发布的项目--%>
        <div class="comments-SubmitProfile-Titlebg">
            <div class="comments-SubmitProfile-Title">
                    <h2 style="color:yellow">我发布的项目</h2>
                </div>
        </div>
        <div class="comments-SubmitProfile-content" style="border: solid 1px #8b4513">
            <asp:GridView ID="gvMyPub" runat="server" AllowPaging="true" PageSize="7" AutoGenerateColumns="False"
            CellPadding="0" GridLines="None" Height="30px" Width="100%" PagerSettings-Mode="NumericFirstLast"
            PagerSettings-FirstPageText="首页" PagerSettings-NextPageText="下一页" PagerSettings-PreviousPageText="上一页"
            PagerSettings-LastPageText="尾页" OnPageIndexChanging="gvMyPub_PageIndexChanging"  OnRowDataBound="gvMyPub_OnRowDataBound" OnRowCancelingEdit="gvMyPub_OnRowCancelingEdit" OnRowEditing="gvMyPub_OnRowEditing" OnRowUpdating="gvMyPub_OnRowUpdating">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <RowStyle CssClass="GridViewRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" HorizontalAlign="Center" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" HorizontalAlign="Center" />

            <EmptyDataTemplate>
                <span style="font-family: 微软雅黑; font-size: 14px; color: red;">当前没有任何发布的项目
                </span>
            </EmptyDataTemplate>

                <Columns>
                    <asp:BoundField DataField="ProjectID" HeaderText="项目ID" ItemStyle-HorizontalAlign="Center" Visible="False" />
                    <asp:BoundField DataField="IsMatch" Visible="false" />
                    <%--<asp:HyperLinkField DataTextField="PName" HeaderText="项目名称" DataNavigateUrlFields="ProjectID"
                    DataNavigateUrlFormatString="../platform" ItemStyle-HorizontalAlign="Left" Target="_blank"/>--%>

                    <asp:TemplateField HeaderText="项目名称">
                        <ItemTemplate >
                            <asp:HyperLink ID="PNameLink" runat="server" Text='<%#Eval("PName")%>' NavigateUrl="http://va.neu.edu.cn/Projects/platform/">
                            </asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>

                     <asp:BoundField DataField="CreatedDate" HeaderText="发布时间" ItemStyle-HorizontalAlign="Center" ReadOnly="True"/>
                     <asp:BoundField DataField="StateName" HeaderText="项目状态" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ReadOnly="True"/>
                    <asp:BoundField DataField="SubjectName" HeaderText="所属学科" ItemStyle-HorizontalAlign="Center" ReadOnly="True"/>
                    <asp:HyperLinkField DataNavigateUrlFields="ProjectID" HeaderText="详情" Text="查看" NavigateUrl="PDetails.aspx?ProjectID={0}&pageTypeId=0"
                        ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                    <asp:TemplateField HeaderText="文档管理" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" >
                        <ItemTemplate>
                            <asp:HyperLink ID="DocManagement" runat="server">进入文档库</asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="对接">
                        <ItemTemplate>
                            <asp:Label runat="server" Text="Label" ID="lbIsMatch"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlProIsMach" runat="server" Width="100px" >
                                <asp:ListItem Value="0" Selected="True" Text="拒绝对接"></asp:ListItem>
                                <asp:ListItem Value="1" Text="等待对接"></asp:ListItem>
                                <asp:ListItem Value="2" Text="对接完成"></asp:ListItem>
                            </asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField  HeaderText="变更对接状态" ShowEditButton="true" ShowCancelButton="True" EditText="变更状态" UpdateText="确认"/>
                </Columns>
         </asp:GridView>
    </div>

    </div>
    <div class="comments-SubmitProfile" id="MyJoinDiv"> <%--我参与的项目--%>
        <br /><br />
        <div class="comments-SubmitProfile-Titlebg">
            <div class="comments-SubmitProfile-Title">
                    <h2 style="color:yellow">我参与的项目</h2>
            </div>
        </div>
        <div  class="comments-SubmitProfile-content" style="border: solid 1px #8b4513">
            <asp:GridView ID="gvMyJoin" runat="server" AllowPaging="true" PageSize="7" AutoGenerateColumns="False"
            CellPadding="0" GridLines="None" Height="30px" Width="100%" PagerSettings-Mode="NumericFirstLast"
            PagerSettings-FirstPageText="首页" PagerSettings-NextPageText="下一页" PagerSettings-PreviousPageText="上一页"
            PagerSettings-LastPageText="尾页" OnPageIndexChanging="gvMyJoin_PageIndexChanging">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <RowStyle CssClass="GridViewRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" HorizontalAlign="Center" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" HorizontalAlign="Center" />

            <EmptyDataTemplate>
                <span style="font-family: 微软雅黑; font-size: 14px; color: red;">当前你还未参与任何项目
                </span>
            </EmptyDataTemplate>

            <Columns>
                <asp:BoundField DataField="ProjectID" HeaderText="项目ID" Visible="False">
                </asp:BoundField>
               <asp:HyperLinkField DataTextField="PName" HeaderText="项目名称" DataNavigateUrlFields="ProjectID"
                    DataNavigateUrlFormatString="PDetails.aspx?ProjectID={0}&pageTypeId=0" ItemStyle-HorizontalAlign="Left" Target="_blank"/>
                 <asp:BoundField DataField="StateName" HeaderText="项目状态" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="SubjectName" HeaderText="所属学科" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="Score" HeaderText="个人成绩" ItemStyle-HorizontalAlign="Center" />

                <asp:HyperLinkField  DataNavigateUrlFields="ProjectID" HeaderText="项目详情" Text="查看"
                NavigateUrl="PDetails.aspx?ProjectID={0}&pageTypeId=0" ItemStyle-HorizontalAlign="Center" />

            </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
我的项目
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
我的项目
</asp:Content>
