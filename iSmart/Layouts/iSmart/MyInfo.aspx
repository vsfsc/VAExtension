<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyInfo.aspx.cs" Inherits="iSmart.Layouts.iSmart.MyInfo" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <script type="text/javascript" src="../JSFiles/jquery-1.10.1/jquery-1.10.1.min.js"></script>
    <script type="text/javascript" src="../JSFiles/jquery.mobile-1.4.2/jquery.mobile-1.4.2.min.js"></script>
    <link href="../JSFiles/jquery.mobile-1.4.2/jquery.mobile-1.4.2.min.css" rel="stylesheet" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <style type="text/css">
        .labelTD {
            text-align: right;
        }
    </style>
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <%-- 个人信息显示 --%>
    <div id="home" data-role="page">
        <div data-role="header" data-add-back-btn="true"
            data-back-btn-text="保存并返回">
            <h1>我的信息</h1>
            <a href="#page2" data-role="button" data-mini="true" data-inline="true">修改信息</a>

        </div>
        <div class="ui-content" role="main">
            <table>
                <tr>
                    <td class="labelTD">姓名：</td>
                    <td>
                        <asp:Label ID="lbName" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <hr/>
                    </td>
                </tr>
                <tr>
                    <td class="labelTD">性别：</td>
                    <td>
                        <asp:Label ID="lbSex" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td class="labelTD">身份证号：</td>
                    <td>
                        <asp:Label ID="lbID" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td>联系电话：</td>
                    <td>
                        <asp:Label ID="lbPhone" runat="server" Text=""></asp:Label>
                     </td>  
                </tr>
                <tr>
                    <td colspan="2">
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td>电子邮箱：</td>
                    <td>
                        <asp:Label ID="lbEmail" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td>所在院校：</td>
                    <td>
                        <asp:Label ID="lbSchool" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <hr />
                    </td>
                </tr>
            </table>
        </div>
        <div data-role="footer">
            <h6></h6>
        </div>
    </div>
    <%-- 修改个人信息 --%>
    <div id="page2" data-role="page">
        <div data-role="header" data-add-back-btn="true"
            data-back-btn-text="保存并返回">
            <h1>修改我的信息</h1>
            <asp:Button ID="btnSave" runat="server" Text="保存并返回"/>
        </div>
        <div class="ui-content" role="main">
            <div>
                <table>
                    <tr>
                        <td>姓名：</td>
                        <td>
                            <asp:TextBox ID="txtName" runat="server" CssClass="txtcss" onfocus="onft(this)" onblur="onbt(this)"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>性别：</td>
                        <td>
                            <asp:RadioButtonList ID="rblSex" runat="server" RepeatDirection="Horizontal" Width="123px">
                                <asp:ListItem Selected="True" Value="1">男</asp:ListItem>
                                <asp:ListItem Value="0">女</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td>身份证号：</td>
                        <td>
                            <asp:TextBox ID="txtId" runat="server" CssClass="txtcss" onfocus="onft(this)" onblur="onbt(this)"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>联系电话：</td>
                        <td>
                            <asp:TextBox ID="txtTelephone" runat="server" CssClass="txtcss" onfocus="onft(this)"
                                onblur="onbt(this)"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>电子邮箱：</td>
                        <td>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="txtcss" onfocus="onft(this)"
                                onblur="onbt(this)"></asp:TextBox></td>
                    </tr>
                    <%-- 教育信息 --%>

                    <tr>
                        <td colspan="2" >
                            <hr /><h6>所在院校</h6>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:DropDownList ID="ddlProvince" runat="server" AutoPostBack="True" Width="50%">
                                <%--<asp:ListItem>辽宁省</asp:ListItem>--%>
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlCity" runat="server" AutoPostBack="True" Width="50%">
                                <asp:ListItem>沈阳</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:DropDownList ID="ddlSchool" runat="server">
                                <%--<asp:ListItem>东北大学</asp:ListItem>--%>
                            </asp:DropDownList>
                        </td>
                    </tr>

                   <%-- <tr>
                        <td colspan="2"><hr />
                        </td>
                    </tr>
                    <tr>
                        <td>输入验证码：</td>
                        <td>
                            <asp:TextBox ID="txtCheckCode" Width="120px" runat="server" CssClass="txtcss" onfocus="onft(this)"
                                onblur="onbt(this)"></asp:TextBox>&nbsp;
       <img src="GenerateCheckCode.aspx" alt="点击刷新验证码" height="24px" id="imag1" style="cursor: pointer; vertical-align: bottom" onclick="javascript:getimgcode();" title="点击刷新验证码" /></td>
                    </tr>--%>
                   
                </table>
            </div>
        </div>
        <div data-role="footer">
            <h6></h6>
        </div>
    </div>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
个人信息维护
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
我的信息
</asp:Content>
