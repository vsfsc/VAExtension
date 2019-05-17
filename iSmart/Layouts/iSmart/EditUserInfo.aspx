<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditUserInfo.aspx.cs" Inherits="iSmart.Layouts.iSmart.EditUserInfo" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <script type="text/javascript" src="Validate.js"></script>
    <script type="text/javascript">
        window.onbeforeunload = function()
        {    
            var n = window.event.screenX - window.screenLeft;    
            var b = n > document.documentElement.scrollWidth-20;    
            if(b && window.event.clientY < 0 || window.event.altKey)  // 是关闭而非刷新 
            {    
                document.getElementById('btnClose').click();
            }    
        }  
        function getimgcode() {
            document.getElementById("imag1").src = document.getElementById("imag1").src + '?';

        }
        function getCookie(name) {
            var cookieValue = null;
            var search = name + "=";
            if (document.cookie.length > 0) {
                offset = document.cookie.indexOf(search);
                if (offset != -1) {
                    offset += search.length;
                    end = document.cookie.indexOf(";", offset);
                    if (end == -1) {
                        end = document.cookie.length;
                    }
                    cookieValue = unescape(document.cookie.substring(offset, end))
                }
            }
            return cookieValue;
        }

        function IsValid() {
            var chkValue = getCookie("CheckCode")
            if (chkValue == null) {
                alert("您的浏览器设置已被禁用 Cookies，您必须设置浏览器允许使用 Cookies 选项后才能使用本系统。");
                return false;
            }
            var checkCode = document.getElementById('<%=txtCheckCode.ClientID%>');

            if (checkCode.value.length == 0 || checkCode.value.toLowerCase() != chkValue.toLowerCase()) {
                alert("验证码错误");
                return false;
            } 
                 
       
            var name=document.getElementById('<%=txtName.ClientID%>');
            if (name.value.length==0)
            {
                alert ("姓名不能为空！");
                name.select();
                return false;
            }
            var id=document.getElementById('<%=txtId.ClientID%>');
            if (id.value.length==0 )
            {
            }
            else if (!CheckIdentify(id.value)){
                id.select();
                return false;
            }

            var tel = document.getElementById('<%=txtTelephone.ClientID%>');
            if (tel.value.length == 0) {
                alert("电话不能为空！");
                tel.select();
                return false;
            }
            else if (!CheckTelephone(tel.value)) {
                tel.select();
                return false;
            }
            var email = document.getElementById('<%=txtEmail.ClientID%>');
            if (email.value.length == 0) {
                alert("邮箱不能为空！");
                email.select();
                return false;
            }
            else if (!CheckEmail(email.value)) {
                email.select();
                return false;
            }
            return true;
        }
    </script>
 
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <style type="text/css">
        .labelTD {
            text-align: right;
        }
    </style>
    <style type="text/css">
         .h3css{ font-family:微软雅黑; color:#4141a4; text-decoration:underline}
       .tdone{text-align:right; padding-right:10px; color:#4a4a4a; width:120px}
       .txtcss{ width:200px; border:1px #bebee1 solid; height:25px; vertical-align:middle; line-height:25px; padding:0 10px;
                color:Black;}
        .txtdoenlist{ width:103px; height:25px; line-height:25px; vertical-align:middle; padding:3px;border:1px #bebee1 solid;color:#494b4c; }
        .txtdoenlista{ width:210px; height:25px; line-height:25px; vertical-align:middle; padding:3px;border:1px #bebee1 solid;color:#494b4c; }
        .buttoncss{ width:120px;height:40px; background-color:#3776a9; color:#fff;font-family:微软雅黑; font-size:20px ; border:1px solid #3776a9; cursor:pointer }
     </style>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1">
   
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    
    <%-- 个人信息修改 --%>
    <div id="EditProfile">

        <table style="font-size: 14px; color: #494b4c" cellspacing="0" cellpadding="3">
            <tr style="display: none">
                <td class="tdone">我的帐号：</td>
                <td>
                    <asp:Label ID="txtAccount" runat="server" Text=""></asp:Label>
                    <%--<asp:TextBox ID="txtAccount" runat="server" CssClass="txtcss" onfocus="onftxtAccount()" onblur="onbtxtAccount()"></asp:TextBox>--%>
                </td>
            </tr>
            <asp:HiddenField ID="HiddenField1" Value="ismart Ccc2008neu" runat="server" />
            <tr>
                <td class="tdone">姓名：</td>
                <td>
                    <asp:TextBox ID="txtName" runat="server" CssClass="txtcss" onfocus="onft(this)" onblur="onbt(this)"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tdone">性别：</td>
                <td>
                    <asp:RadioButtonList ID="rblSex" runat="server" RepeatDirection="Horizontal"
                        Width="123px">
                        <asp:ListItem Selected="True" Value="1">男</asp:ListItem>
                        <asp:ListItem Value="0">女</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td class="tdone">身份证号：</td>
                <td>
                    <asp:TextBox ID="txtId" runat="server" CssClass="txtcss" onfocus="onft(this)" onblur="onbt(this)"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tdone">联系电话：</td>
                <td>
                    <asp:TextBox ID="txtTelephone" runat="server" CssClass="txtcss" onfocus="onft(this)"
                        onblur="onbt(this)"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="tdone">电子邮箱：</td>
                <td>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="txtcss" onfocus="onft(this)"
                        onblur="onbt(this)"></asp:TextBox></td>
            </tr>
            <%-- 教育信息 --%>
            <%-- 
            <tr>
                <td colspan="2">
                    教育情况
                </td>
            </tr>
            <tr>
                <td class="tdone">院校驻地：</td>
                <td>
                    <asp:DropDownList ID="ddlProvince" runat="server" AutoPostBack="True" CssClass="txtdoenlist">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlCity" runat="server" AutoPostBack="True" CssClass="txtdoenlist">

                        <asp:ListItem>沈阳</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="tdone">院校名称：</td>
                <td>
                    <asp:DropDownList ID="ddlSchool" runat="server" CssClass="txtdoenlista">
                    </asp:DropDownList>
                </td>
            </tr>
            --%>
            <tr>
                <td class="tdone">输入验证码：</td>
                <td>
                    <asp:TextBox ID="txtCheckCode" Width="120px" runat="server" CssClass="txtcss" onfocus="onft(this)"
                        onblur="onbt(this)"></asp:TextBox>&nbsp;
       <img src="GenerateCheckCode.aspx" alt="点击刷新验证码" height="24px" id="imag1" style="cursor: pointer;
           vertical-align: bottom" onclick="javascript:getimgcode();" title="点击刷新验证码" /></td>
            </tr>
            <tr>
                <td colspan="2" height="20px"></td>
            </tr>
            <tr>
                <td colspan="2"></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td style="text-align: left">
                    <asp:Button ID="btnSave" OnClientClick="return IsValid();" runat="server" Text="提 交" BorderStyle="None" Style="background-color: #8395fc" CssClass="buttoncss"/>
                    <div style="display: none">
                        <asp:Button ID="btnClose" runat="server" Text="退 出" Width="0" CssClass="buttoncss" BorderStyle="None" /></div>
                </td>
            </tr>

        </table>
    </div>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    我的资料
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
    修改个人资料
</asp:Content>
