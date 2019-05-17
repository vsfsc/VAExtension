<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RoleApprove.aspx.cs" Inherits="Project.Layouts.Project.RoleApprove" DynamicMasterPageFile="~masterurl/default.master" %>

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
          .GridViewPagerStyle table
        {
            margin: 0 auto 0 auto;
        }

</style>
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div>        
        <asp:GridView ID="gvRolesApproving" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowCommand="gvRolesApproving_RowCommand">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="UserRoleID" HeaderText="用户角色ID" Visible="false"/>
                <asp:BoundField DataField="Name" HeaderText="申请人" />
                <asp:BoundField DataField="RoleName" HeaderText="角   色" />
                <asp:BoundField DataField="Created" HeaderText="申请时间" />
                <asp:TemplateField HeaderText="审批处理">
                    <ItemTemplate>
                        <asp:Button ID="btnPass" runat="server" Text="通 过" CommandName="RolePass" CommandArgument='<%# Bind("UserRoleID") %>' CausesValidation="false"/>
                        <asp:Button ID="btnNoPass" runat="server" Text="驳 回"  CommandName="RoleNoPass" CommandArgument='<%# Bind("UserRoleID") %>'  />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
        <asp:GridView ID="gvRolesApproved" runat="server" AutoGenerateColumns="False"
            CellPadding="3" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None"
            BorderWidth="1px">
            <Columns>
                <asp:BoundField DataField="UserRoleID" HeaderText="用户角色ID" />
                <asp:BoundField DataField="Name" HeaderText="申请人" />
                <asp:BoundField DataField="RoleName" HeaderText="角   色" />
                <asp:BoundField DataField="Created" HeaderText="申请时间" />
                <asp:BoundField DataField="Approved" HeaderText="处理时间" />
                <asp:TemplateField HeaderText="处理结果"></asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
            <RowStyle ForeColor="#000066" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#007DBB" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#00547E" />

        </asp:GridView>
        <asp:GridView ID="gvAdminUsers" runat="server" AutoGenerateColumns="False"
            CellPadding="3" BackColor="#DEBA84" BorderColor="#DEBA84"
            BorderStyle="None" BorderWidth="1px" CellSpacing="2">
            <Columns>
                <asp:BoundField DataField="UserRoleID" HeaderText="用户角色ID" />
                <asp:BoundField DataField="Name" HeaderText="分配对象" />
                <asp:BoundField DataField="Created" HeaderText="分配时间" />
            </Columns>
            <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
            <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
            <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#FFF1D4" />
            <SortedAscendingHeaderStyle BackColor="#B95C30" />
            <SortedDescendingCellStyle BackColor="#F1E5CE" />
            <SortedDescendingHeaderStyle BackColor="#93451F" />

        </asp:GridView>
    </div>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
权限管理
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
用户角色审批
</asp:Content>
