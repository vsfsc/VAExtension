<%@ Assembly Name="ContestDll, Version=1.0.0.0, Culture=neutral, PublicKeyToken=30c3b5583be889be" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true"   Inherits="ContestDll.UserInfo" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">

    <script type ="text/javascript" src="./js/Validate.js" ></script>
    <script  type="text/javascript">
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


        function CheckValue() {
            var name = document.getElementById("<%=txtAccount.ClientID%>").value;
		   
		     if (name == "") {
		         alert("账号不能为空!");
		         return false;
		     }
		    // var chkValue = getCookie("CheckCode")
		    // if (chkValue == null) {
		     //    alert("您的浏览器设置已被禁用 Cookies，您必须设置浏览器允许使用 Cookies 选项后才能使用本系统。");
		     //    return false;
		    // }
		     document.getElementById("HiddenField2").value = chkValue;
		                 if (txtCheckCode  =="" || txtCheckCode.toLowerCase()  !=chkValue.toLowerCase()) {
		                     alert("验证码错误");
		                     return false;
		                  } 
		          

		     return true;
		 }
        function LoginSys(domain, strName, strPWD)
        {
            var location = "/login/default.aspx";
            var auth = null;

            if (window.XMLHttpRequest)// Firefox, Opera 8.0+, Safari
                auth = new XMLHttpRequest();
            else {
                try {
                    auth = new ActiveXObject("Msxml2.XMLHTTP");
                }
                catch (e) {
                    auth = new ActiveXObject("Microsoft.XMLHTTP");
                }
            }
            strName = strName.toLowerCase();
            if (strName.indexOf("\\") < 0) {
                strName = domain + "\\" + strName;
            }
            auth.open('post', location, false, strName, strPWD);
            auth.send();
            switch (auth.status) {
                case 200: window.location.href = '/login/default.aspx'; // 登陆页面
                        window.location.href = '/';
                    break;
                case 401:
                   

            }
        }
        function IsValid() {
           
            var chkValue = getCookie("CheckCode")
            if (chkValue == null) {
                alert("您的浏览器设置已被禁用 Cookies，您必须设置浏览器允许使用 Cookies 选项后才能使用本系统。");
                return false;
            }
            //document.getElementById("HiddenField2").value = chkValue;
            var txtCheckCode = document.getElementById("<%=txtCheckCode.ClientID%>").value;
            if (txtCheckCode  =="" || txtCheckCode.toLowerCase()  !=chkValue.toLowerCase()) {
                alert("验证码错误");
                document.getElementById("<%=txtCheckCode.ClientID%>").select();
             return false;
                         } 
            var name = document.getElementById('<%=txtAccount.ClientID%>');
         if (name.value.length==0)
         {
             alert ("登录帐号不能为空！");
             name.select();
             return false;
         }

         var pass = document.getElementById('<%=txtPwd.ClientID%>');
        if (!pass.getAttribute("readonly") )
        {
            if(pass.value.length<6)
            {
                alert("密码长度不能小于6！");  
                pass.select();
                return false;
            }
            if(pass.value!=document.getElementById('<%=txtPwd1.ClientID%>').value)   
            {   
                alert("密码与确认密码不一致！");  
                document.getElementById('<%=txtPwd1.ClientID%>').select();
               return false;     
           }  
       }
       var name=document.getElementById('<%=txtName.ClientID%>');
         if (name.value.length==0)
         {
             alert ("姓名不能为空！");
             name.select();
             return false;
         }
       /*  else if (!checkChineseCharacter(name.value) )
         {
             alert("姓名只能输入中文汉字！");
             name.select();
             return false;
         }*/
         var id=document.getElementById('<%=txtId.ClientID%>');
        if (id.value.length==0 )
        {
            alert ("身份证号不能为空！");
            id.select();
            return false;

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

     <style type="text/css">
         .h3css{ font-family:微软雅黑; color:#4141a4; text-decoration:underline}
       .tdone{text-align:right; padding-right:10px; color:#4a4a4a; width:120px}
       .txtcss{ width:200px; border:1px #bebee1 solid; height:25px; vertical-align:middle; line-height:25px; padding:0 10px;
                color:Black;}
        .txtdoenlist{ width:103px; height:25px; line-height:25px; vertical-align:middle; padding:3px;border:1px #bebee1 solid;color:#494b4c; }
        .txtdoenlista{ width:210px; height:25px; line-height:25px; vertical-align:middle; padding:3px;border:1px #bebee1 solid;color:#494b4c; }
        .buttoncss{ width:120px;height:40px; background-color:#3776a9; color:#fff;font-family:微软雅黑; font-size:20px ; border:1px solid #3776a9; cursor:pointer }    
        
     </style>

</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <!--<asp:Label ID="Label1" runat="server" Text="账号："></asp:Label>
    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="Label2" runat="server" Text="密  码："></asp:Label>
    -->
      <div>
 
    <table style=" font-size:14px; color:#494b4c" cellspacing="0"  cellpadding="3">
  
    <tr><td class="tdone"><font color="red" font-size="18px"> * </font> 账 &nbsp &nbsp &nbsp  号：</td><td>
        <asp:TextBox ID="txtAccount" runat="server" CssClass="txtcss" onfocus="onftxtAccount()" onblur="onbtxtAccount()"></asp:TextBox>
        <asp:RegularExpressionValidator ID="contxtAccount" runat="server" ErrorMessage="账号必须以字母开头，字母、数字和下划线的任意组合，长度4-16位" ControlToValidate="txtAccount" ForeColor="#ff0000" SetFocusOnError="False" Display="Static" ValidationExpression="^[a-zA-Z][a-zA-Z0-9_]{3,15}$"></asp:RegularExpressionValidator>
        </td></tr>
    <tr><td class="tdone"><font color="red" font-size="18px"> * </font> 密  &nbsp &nbsp &nbsp 码：</td><td>
        <asp:TextBox ID="txtPwd" runat="server" TextMode="Password"  CssClass="txtcss" onfocus="onfocusjs()" onblur="onblurjs()"></asp:TextBox>
                    <span style="font-size:12px; color:red; display:none; margin-left:10px" id="pwds">密码最6位!</span>
        <asp:RegularExpressionValidator ID="contxtPwd" runat="server" ErrorMessage="由字母、数字或特殊字符（除了空格）组成的6-20位密码" ControlToValidate="txtPwd" ValidationExpression="[-\da-zA-Z`=\\\[\];',./~!@#$%^&*()_+|{}:&quot;<>?]{6,20}" ForeColor="#ff0000"></asp:RegularExpressionValidator>
                                    </td></tr>
    <tr>
        <td class="tdone"><font color="red" font-size="18px"> * </font> 确认密码：</td>
        <td>
            <asp:TextBox ID="txtPwd1" runat="server" TextMode="Password"  CssClass="txtcss" onfocus="onft(this)" onblur="onbt(this)"></asp:TextBox>
            <asp:CompareValidator ID="ComtxtPwd1" runat="server" ErrorMessage="密码不一致" ControlToCompare="txtPwd" ControlToValidate="txtPwd1"></asp:CompareValidator>
        </td>
    </tr>
        
        <asp:HiddenField ID="HiddenField1" Value="竞赛注册用户 Ccc2008neu"  runat="server" />
    <tr><td class="tdone"><font color="red" font-size="18px"> * </font> 姓 &nbsp &nbsp &nbsp 名：</td>
        <td><asp:TextBox ID="txtName" runat="server"  CssClass="txtcss" onfocus="onft(this)" onblur="onbt(this)"></asp:TextBox>
            <asp:RegularExpressionValidator ID="contxtName" runat="server" ErrorMessage="姓名只能由汉字(长度2-8)或只能由字母（长度4-20）组成，要符合规定！" ForeColor="#ff0000" ControlToValidate="txtName" ValidationExpression="(^[\u4e00-\u9fa5]{2,8}$)|(^([a-zA-Z]+\s?){4,20}$)"></asp:RegularExpressionValidator>
        </td>
        </tr>
    <tr><td class="tdone"><font color="red" font-size="18px"> * </font> 性 &nbsp &nbsp &nbsp 别：</td><td><asp:RadioButtonList ID="rblSex" runat="server" RepeatDirection="Horizontal" 
                        Width="123px">
                        <asp:ListItem Selected="True" Value="1">男</asp:ListItem>
                        <asp:ListItem Value="0">女</asp:ListItem>
                    </asp:RadioButtonList>
        </td>
    </tr>
    <tr><td class="tdone"><font color="red" font-size="18px"> * </font> 身份证号：</td>
        <td><asp:TextBox ID="txtId" runat="server"  CssClass="txtcss" onfocus="onft(this)" onblur="onbt(this)"></asp:TextBox>
            <asp:RegularExpressionValidator ID="contxtId" runat="server" ErrorMessage="身份证号长度为15位或18位，或者不符合规定！" ValidationExpression="^(^[1-9]\d{7}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}$)|(^[1-9]\d{5}[1-9]\d{3}((0[1-9])|(1[0-2]))((0[1-9]|[1|2]\d)|3[0-1])((\d{4})|\d{3}[Xx])$)$" ForeColor="#ff0000" ControlToValidate="txtId"></asp:RegularExpressionValidator>
        </td>
    </tr>
    
    <tr><td class="tdone"><font color="red" font-size="18px"> * </font> 手 &nbsp &nbsp &nbsp 机：</td><td>
        <asp:TextBox ID="txtTelephone" runat="server" CssClass="txtcss" onfocus="onft(this)" onblur="onbt(this)"></asp:TextBox>
        <asp:RegularExpressionValidator ID="contxtTelephone" runat="server" ErrorMessage="手机号不符合规定！" ValidationExpression="^[1][358][0-9]{9}$" ForeColor="#ff0000" ControlToValidate="txtTelephone"></asp:RegularExpressionValidator>
                                    </td>
        </tr>
    <tr><td class="tdone"><font color="red" font-size="18px"> * </font> 电子邮箱：</td><td>
        <asp:TextBox ID="txtEmail" runat="server"  CssClass="txtcss" onfocus="onft(this)" onblur="onbt(this)"></asp:TextBox>
        <asp:RegularExpressionValidator ID="contxtEmail" runat="server" ErrorMessage="邮箱不符合规定！"  ForeColor="#ff0000" 
            ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtEmail"></asp:RegularExpressionValidator>
                                    </td>
    </tr>
    <tr>
        <td class="tdone"> 职&nbsp &nbsp &nbsp 业：</td>
        <td>
            <asp:DropDownList ID="ddlCareer" runat="server"></asp:DropDownList>
        </td>
    </tr>
    <tr><td class="tdone"> 院校驻地：</td><td>
        <asp:DropDownList ID="ddlProvince" runat="server" AutoPostBack="True" CssClass="txtdoenlist">

        </asp:DropDownList>
        <asp:DropDownList ID="ddlCity" runat="server" AutoPostBack="True" CssClass="txtdoenlist">
                        
        <asp:ListItem>沈阳</asp:ListItem>
        </asp:DropDownList>
    </td></tr>
    <tr><td class="tdone"> 院校名称：</td><td>
        <asp:DropDownList ID="ddlSchool" runat="server"  CssClass="txtdoenlista">
        </asp:DropDownList>
    </td></tr>
    <tr>
        <td class="tdone"><font color="red" font-size="18px"> * </font>输入验证码：</td>
        <td>
            <asp:TextBox ID="txtCheckCode" Width="120px" runat="server" CssClass="txtcss" onfocus="onft(this)"
                onblur="onbt(this)"></asp:TextBox>&nbsp
    <img src="GenerateCheckCode.aspx" alt="点击刷新验证码" height="24px" id="imag1" style="cursor: pointer;
        vertical-align: bottom" onclick="javascript:getimgcode();" title="点击刷新验证码" /></td>
    </tr>       


    <tr><td colspan="2" height="20px"></td></tr>
        <tr><td colspan="2"></td></tr>
        <tr><td>&nbsp;</td><td style="text-align:left">
                    <asp:Button ID="btnSave" OnClientClick="return IsValid();" runat="server" Text="提 交" BorderStyle="None" style="background-color:#8395fc" CssClass="buttoncss" />
                    <div style="display:none" ><asp:Button ID="btnClose"  runat="server" Text="退 出"   Width="0"  CssClass="buttoncss"  BorderStyle="None" /></div></td></tr>

    </table> 
    </div>
</asp:Content>



<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
用户注册
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
用户注册
</asp:Content>
