<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SPOrderWebPartUserControl.ascx.cs" Inherits="SP2013.SPOrderWebPart.SPOrderWebPartUserControl" %>
<link href="../../../../_layouts/15/css/bootstrap.min.css" rel="stylesheet" />
<script src="../../../../_layouts/15/js/bootstrap.min.js"></script>
<script src="../../../../_layouts/15/js/jquery-1.8.2.min.js"></script>
<style type="text/css">
    .hidden {
        display: none;
    }

    .GridViewStyle {
        border-right: 2px solid #A7A6AA;
        border-bottom: 2px solid #A7A6AA;
        border-left: 2px solid white;
        border-top: 2px solid white;
        padding: 4px;
    }

        .GridViewStyle a {
            color: #FFFFFF;
        }

    .GridViewHeaderStyle th {
        border-left: 1px solid #EBE9ED;
        border-right: 1px solid #EBE9ED;
    }

    .GridViewHeaderStyle {
        background-color: #5D7B9D;
        font-weight: bold;
        color: White;
    }

    .GridViewFooterStyle {
        background-color: #5D7B9D;
        font-weight: bold;
        color: White;
    }

    .GridViewRowStyle {
        background-color: #F7F6F3;
        color: #333333;
    }

    .GridViewAlternatingRowStyle {
    }

        .GridViewRowStyle td, .GridViewAlternatingRowStyle td {
            border: 1px solid #EBE9ED;
        }

    .GridViewSelectedRowStyle {
        background-color: #E2DED6;
        font-weight: bold;
        color: #333333;
    }

    .GridViewPagerStyle {
        background-color: #284775;
        color: #FFFFFF;
    }

        .GridViewPagerStyle table /* to center the paging links*/ {
            margin: 0 auto 0 auto;
        }

    .Textcss {
        font-size: 16px;
        color: blue;
    }

    .Textcss1 {
        font-size: 16px;
        color: red;
    }

    .Info {
        font-size: 16px;
        color: red;
    }
</style>
<div></div>
<table>

    <tr>
        <td>
            <asp:GridView ID="gvOrder" runat="server" AllowPaging="true" AutoGenerateColumns="False"
                CellPadding="0" GridLines="None" Height="30px" OnRowCommand="gvOrder_RowCommand"
                Width="750px" CssClass="GridViewStyle" OnPageIndexChanging="gvOrder_PageIndexChanging" AlternatingRowStyle-CssClass="GridViewAlternatingRowStyle" OnRowDataBound="gvOrder_RowDataBound">
                <EmptyDataTemplate>
                    <table>
                        <tr>
                            <td>选择
                            </td>
                            <td>教师名称
                            </td>
                            <td>面试地点
                            </td>
                            <td>开始时间
                            </td>
                            <td>结束时间
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5">当前没有可预约时间
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <RowStyle CssClass="GridViewRowStyle" />
                <Columns>                  
                    <asp:BoundField DataField="ID" HeaderText="ID">
                        <FooterStyle CssClass="hidden" />
                        <HeaderStyle CssClass="hidden" />
                        <ItemStyle CssClass="hidden"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="teacher" HeaderText="面试教师">
                        <ItemStyle Width="10%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="address" HeaderText="地点">
                        <ItemStyle Width="20%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField  DataField="start" HeaderText="开始时间" >
                        <ItemStyle Width="20%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField  DataField="end" HeaderText="结束时间" >
                        <ItemStyle Width="20%"></ItemStyle>
                    </asp:BoundField>
                      <asp:BoundField  DataField="num" HeaderText="面试人数" >
                        <ItemStyle Width="10%"></ItemStyle>
                    </asp:BoundField> 
                     <asp:BoundField  DataField="IsOrder" HeaderText="已满" >
                        <FooterStyle CssClass="hidden" />
                        <HeaderStyle CssClass="hidden" />
                        <ItemStyle CssClass="hidden"></ItemStyle>
                    </asp:BoundField>                              
                    <asp:TemplateField HeaderText="操作" ControlStyle-Width="90px" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Button ID="btnOrder" Style="border: 0; margin-left: 0px; padding-left: 0px; background-color: #32acd1; width: 60px; height: 25px; color: #fff; align-content: center" CommandName="Order" Text="预约" runat="server"
                                BorderStyle="Solid" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="GridViewHeaderStyle" HorizontalAlign="Left" />
                <PagerStyle HorizontalAlign="Center" />
                <FooterStyle CssClass="GridViewFooterStyle" HorizontalAlign="Left" />
            </asp:GridView>
        </td>
    </tr>
</table>
<div style="color:red"><asp:Label ID="lblMsg" runat="server" Text=""></asp:Label></div>




