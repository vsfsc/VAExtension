<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="oneshow.aspx.cs" Inherits="Project.Layouts.Project.oneshow" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">

</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div>
        <p>
            <asp:GridView ID="gvmyFiles" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                OnRowDataBound="gvmyFiles_OnRowDataBound">
                <FooterStyle CssClass="GridViewFooterStyle" />
                <RowStyle CssClass="GridViewRowStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" HorizontalAlign="Center" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <HeaderStyle CssClass="GridViewHeaderStyle" HorizontalAlign="Center" />


                <EmptyDataTemplate>
                    <span style="font-family: 微软雅黑; font-size: 14px; color: red;">无文档
                    </span>
                </EmptyDataTemplate>

                <Columns>
                    <asp:TemplateField HeaderText=" ">
                        <ItemTemplate>
                            <asp:Image ID="iconImg" runat="server" Height="25px" Width="25px" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:HyperLinkField HeaderText="文件名称" DataNavigateUrlFields="fileUrl" DataNavigateUrlFormatString="{0}"
                        DataTextField="Name" />
                    <asp:BoundField DataField="fileExName" HeaderText="fileExName" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="Name" HeaderText="Name" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="fileUrl" HeaderText="fileUrl" ItemStyle-HorizontalAlign="Center"
                        Visible="False" />
                    <asp:BoundField DataField="fileSize" HeaderText="fileSize" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="fileAuthor" HeaderText="fileAuthor" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="TimeCreated" HeaderText="TimeCreated" ItemStyle-HorizontalAlign="Center" />
                    <asp:TemplateField HeaderText=" 在线预览">
                        <ItemTemplate>
                            <asp:HyperLink ID="viewDoc" runat="server"><%#Eval("Name")%></asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="IconUrl" HeaderText="IconUrl" ItemStyle-HorizontalAlign="Center"
                        Visible="False" />
                </Columns>
            </asp:GridView>
        </p>
    </div>
    <asp:GridView ID="gvTasks" runat="server" AutoGenerateColumns="True"></asp:GridView>
    <asp:GridView ID="gvDocs" runat="server"></asp:GridView>
    <asp:GridView ID="gvDiscussions" runat="server"></asp:GridView>
    <div id="myChanges" runat="server"></div>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
个人在项目中的表现
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
为他打分
</asp:Content>
