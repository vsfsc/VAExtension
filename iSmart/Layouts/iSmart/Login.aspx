<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="iSmart.Layouts.iSmart.Login" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <script type="text/jscript">
        function clearName() {
            document.getElementById('<%=userid.ClientID%>').value = "";
        }
        function autoCheckUser()
        {
            document.getElementById('<%=btnLogin.ClientID%>').click();
        }
        function retUrl(key) {
            var svalue = window.location.search.match(new RegExp("[\?\&]" + key + "=([^\&]*)(\&?)", "i"));
            return svalue ? svalue[1] : svalue;
        }
        function UserValidate(domain,strName, strPWD) {
<%--            document.getElementById('<%=error.ClientID%>').innerHTML = "";--%>
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
                  case 200:
                      window.location.href = '/login/default.aspx'; // 登陆页面
                      var c = retUrl("Resource");
                      if (c == null)
                          window.location.href = '/';
                      else
                          window.location.href = '/'+c;
                      break;
                  case 401:
                      {

                          document.getElementById('error').innerText = "帐号或密码错误！";
                      }
                      break;

                  default: document.getElementById('error').innerText = '抱歉，请再试一次！';

              }
          }
    </script>
       <style type="text/css">
        body
        {
            background: url('/_layouts/15/images/login/bodybg.jpg') center center;
        }
               .ms-areaseparatorleft
        {
            background: none;
            border: none;
        }
        .ms-formareaframe
        {
            background: none;
            border: none;
        }
        .ms-propertysheet
        {
            background: none;
            border: none;
        }
        .ms-descriptiontext
        {
            background: none;
            border: none;
        }
        .ms-areaseparatorright
        {
            background: none;
            border: none;
            display: none;
        }
        .ms-areaseparatorleft
        {
            background: none;
            border: none;
            display: none;
        }
        .ms-pagebottommarginleft
        {
            background: none;
            border: none;
        }
        .ms-pagebottommargin
        {
            background: none;
            border: none;
        }
        .ms-pagebottommarginright
        {
            background: none;
            border: none;
        }
        .ms-input td
        {
            font-weight: bold;
            color: #006700;
        }
        .ms-longone
        {
            height: 18px !important;
                border-style: none;
            border-color: inherit;
             border-width:0px;
            padding-left: 4px;
                padding-top: 2px; padding-bottom:7px;
            height: /**/ 29px !important;
                vertical-align: middle;
            width: 33px;
        }
        .ms-long
        {
            height: 45px !important;
                border-style: none;
                margin-left:18px;
            border-color: inherit;
            border-width: medium;
            padding-left: 8px;
                padding-top: 4px; padding-bottom:7px;
            height: /**/ 29px !important;
                vertical-align: middle;
            width: 220px;
        }
        
        .button_off, .button_on
        {
            border-style: none;
            border-color: inherit;
            border-width: medium;
            width: 93px;
           
        }
        .button_off
        {
            background: url(/_layouts/15/images/login/jiantou.png) left top no-repeat;
            cursor:pointer;
        }
           .button_off2, .button_on2
        {
            border-style: none;
            border-color: inherit;
            border-width: medium;
            width: 93px;
            font-weight: bold;
               font-size: 14px;
           color:#00bfff
        }
        .button_off2
        {
       color:white;
       background-color:transparent;  cursor:pointer;     
        }
      
        #img1
        {
            height: 26px;
        }
        .pages{ 
position: absolute; 
} 
.current{ 
position: absolute; 
z-index: 12 !important; 
left: 0px !important; 
} 

.page4{ 

background: rgba(255, 255, 255, 0.3) !important; /* IE无效，FF有效 */ 
background: url('/_layouts/15/images/login/bgpng.png');
 
filter: alpha(opacity=30);  
z-index: 4; 
width: 50px; 
height:50px; 
top: 50px; 
left: 0px; 
} 
    </style>
    <script type="text/javascript" src="/_layouts/15/images/login/jquery-1.9.1.min.js"></script> 
<script type="text/javascript">
    function increase() {
        var div4 = $(".page4");
        var style = $(".page4").attr("style");
        div4.addClass("current").attr("styleold", style);
        div4.stop();
        div4.animate({
            opacity: 0.9,
            width: "750px",
            height: "400px",
            top: "100px",
            left: "100px"
        }, 600)
        .animate({
            opacity: 1.0
        }, 30);
    }
</script> 
</head>
<body  class="ms-main" onload="increase()">
    <form id="form1" runat="server">
    <div>
        
        <table width="733" border="0" align="center" cellpadding="0" cellspacing="0" style="margin-top:76px; position:relative;">
                <Tr>
                <TD height="426">
                    
                <div style="position: absolute; left:241px; top:177px; "></div>
                 <div class="pages page4">
                     <div style="float: right; z-index: 9999">
                         <asp:ImageButton ID="closeBtn" runat="server" ImageUrl="./images/close.png" Width="36px"
                             Height="36px" OnClick="closeBtn_OnClick" ToolTip="取消登录" />
                     </div>
                <table cellspacing="0" cellpadding="0" class="ms-input" style="top:35%; width: 390px; height: 121px; margin-left: 170px; position: absolute;">
                                <tr><td style="width: 90px;"></td>
                                <td colspan="3">
                                <span id="error" runat="server"  title="ccc" style="color:Red;"></span></td></tr>
                            <tr><td style="width: 90px; color:#333" align="right" rowspan="2">
                                <div style="width:90px;height:90px;">
                                 <img src="/_layouts/15/images/login/header.jpg" width="90px" height="90px" style="padding:0;margin:0px;margin-right:0"/>
                                </div>
                                </td>
                                <td style="height: 45px; width: 220px;" align="left" >
                                   <input runat="server" type="text" id="userid" class="ms-long" name="userid" onkeydown="if(event.keyCode==13)event.keyCode=9"
                                       value="请输入账号" onfocus="clearName()" style="background-color: white" placeholder="请输入账号" /></td>
                                   <td style="width: 80px; height: 45px; "></td>    
                            </tr>  
                            <tr>
                              <td style="height: 45px; width: 220px;" align="left" >
                                    <input runat="server" type="password" class="ms-long" id="password" name="password"
                                        value="" onkeydown="if(event.keyCode==13)autoCheckUser();" style="background-color: white"
                                        placeholder="请输入密码" /> 
                                </td>
                            <td style="width: 80px; height: 45px;  color:#333; position:relative" align="right" >
                                      <input id="btnLogin"    runat="server" class="button_off" onserverclick="btnLogin_ServerClick" type="button"   style=" position: absolute;top:-30px;left:15px;padding:0;margin:0; width:60px;height:60px;"/>
                                </td>
                            </tr>
                            <tr>
                                        <td style="width: 90px;" align="right">
                                         </td>
                                         <td>
                                        <table cellspacing="0" cellpadding="0" class="ms-input" style="width:100%">
                                        <tr>
                                        	<td align="left">
                                                &nbsp;&nbsp;<input type="button" onclick="javascrtpt: window.location.href = 'RetrievePwd.aspx';" value="忘记密码" class="button_off2" title="找回密码" style="color:dodgerblue">
                                                
                                            	<%--&nbsp;&nbsp;<input id="btnforgetPwd"  class="button_off2"  onclick="alert('忘记密码请联系管理员')" type="button" value="忘记密码" />--%>
                                            </td>
                                            <td align="right" >
                                            	 <input type="button" onclick="javascrtpt:window.location.href='Registration.aspx';"
                                                     value="注册新用户" class="button_off2" style="color:mediumseagreen">&nbsp;&nbsp;
                                            </td>
                                        </tr>
                                        </table>
                                        </td>
<td valign="bottom" colspan="2">
                                                      </td>
                            </tr>
                </table>
                </div>
                </TD>
                </Tr>
                <tr>
            <td><font color="#666"><h4> &nbsp;</h4></font></td>
        </tr>
    </table>  
</div>
    </form>
</body>
</html>