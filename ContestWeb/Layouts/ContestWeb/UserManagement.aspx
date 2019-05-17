<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true"   Inherits="ContestDll.UserManagement" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
     <script type ="text/javascript" src ="Validate.js"></script>
        <script language="javascript" type="text/javascript">
            function checkboxCount(operate, strfix) {
                var controlIndex;
                var element;
                var numberofControls;
                numberofControls = document.forms[0].length;
                var checkBoxCount = 0;
                for (controlIndex = 0; controlIndex < numberofControls; controlIndex++) {
                    element = document.forms[0][controlIndex];
                   
                    if (element.type == "checkbox" && element.name.indexOf(strfix) >= 0) {//.substring(0,6)=="gvUser"
                        if (element.checked == true) {
                            checkBoxCount++;
                        }
                    }
                }
                if (checkBoxCount == 0) {
                    alert("请选中要" + operate + "的记录！");
                    return false;
                }
                else {
                    //if (confirm("确定要" + operate + "选中的记录？") == false)
                    //    return false;
                    //else
                        return true;
                }
            }
            function unChecked(strfix) {
                var o = document.getElementsByTagName("INPUT")
                var element;
                for (i = 0; i < o.length; i++) {
                    element = o[i];
                    if (element.type == "checkbox" && element.name.indexOf(strfix) == 0)// && o[i].name==str) 
                    {
                        element.checked = false;
                    }
                }
            }
            function IsValid() {
                var name = document.getElementById('<%=txtAccountNew.ClientID%>');
             if (name.value.length == 0) {
                 alert("用户名不能为空！");
                 name.select();
                 return false;
             }
             var pass = document.getElementById('<%=txtPwd.ClientID%>');
        if (!pass.getAttribute("readonly")) {

            if (pass.value.length < 8) {
                alert("密码长度不能小于8！");
                pass.select();
                return false;
            }
        }
        var name = document.getElementById('<%=txtNameNew.ClientID%>');
        if (name.value.length == 0) {
            alert("姓名不能为空！");
            name.select();
            return false;
        }
        var id = document.getElementById('<%=txtId.ClientID%>');
        if (!id.getAttribute("readonly")) {
            if (id.value.length == 0) {
                alert("身份证号不能为空！");
                id.select();
                return false;

            }
            else if (!CheckIdentify(id.value)) {

                id.select();
                return false;
            }
        }
        var email = document.getElementById("txtEmail");
        if (!CheckEmail(email.value)) {
            email.select();
            return false;
        }

        var tel = document.getElementById("txtTelephone");
        if (!CheckTelephone(tel.value)) {
            tel.select();
            return false;
        }
        return true;
    }
     </script>

   <script type="text/javascript">
       function onfocusjs() {
           var ssd = document.getElementById("pwds");
           ssd.style.display = "";
           var psd = document.getElementById("txtPwd");
           psd.style.border = "1px #61a4da solid";
           psd.style.color = "#000"

       }
       function onblurjs() {
           var ssd = document.getElementById("pwds");
           ssd.style.display = "none";
           var psd = document.getElementById("txtPwd");
           psd.style.border = "1px #bebee1 solid";
           psd.style.color = "#666"
       }
       function onftxtAccount() {
           var sssd = document.getElementById("txtAccount");
           sssd.style.border = "1px #61a4da solid";
           sssd.style.color = "#000";
       }
       function onbtxtAccount() {
           var sssd = document.getElementById("txtAccount");
           sssd.style.border = "1px #bebee1 solid";
           sssd.style.color = "#666";
       }
       function onft(e) {
           e.style.border = "1px #61a4da solid";
           e.style.color = "#000";
       }
       function onbt(e) {
           e.style.border = "1px #bebee1 solid";
           e.style.color = "#666";
       }
     </script>
        <script type="text/javascript" language="javascript">
            function ShowNo()                        //隐藏两个层 
            {
                document.getElementById("doing").style.display = "none";
                document.getElementById("divShow").style.display = "none";
            }
            function ShowYes()                        //隐藏两个层 
            {
                document.getElementById("doing").style.display = "";
                document.getElementById("divShow").style.display = "";
            }
            function ShowNo1()                        //隐藏两个层 
            {
                document.getElementById("doing").style.display = "none";
                document.getElementById("divRole").style.display = "none";
            }
            function ShowYes1()                        //隐藏两个层 
            {
                document.getElementById("doing").style.display = "";
                document.getElementById("divRole").style.display = "";
            }
            function $(id) {
                return (document.getElementById) ? document.getElementById(id) : document.all[id];
            }
            function showFloat()                    //根据屏幕的大小显示两个层 
            {
                var range = getRange();
                $('doing').style.width = range.width + "px";
                $('doing').style.height = range.height + "px";
                $('doing').style.display = "";
                document.getElementById("divShow").style.display = "";
            }
            function getRange()                      //得到屏幕的大小 
            {
                var top = document.body.scrollTop;
                var left = document.body.scrollLeft;
                var height = document.body.clientHeight;
                var width = document.body.clientWidth;

                if (top == 0 && left == 0 && height == 0 && width == 0) {
                    top = document.documentElement.scrollTop;
                    left = document.documentElement.scrollLeft;
                    height = document.documentElement.clientHeight;
                    width = document.documentElement.clientWidth;
                }
                return { top: top, left: left, height: height, width: width };
            }
    </script>
     <script type="text/javascript">
         var prevselitem = null;
         var prebackcolor = "#ffffff";
         function selectx(row) {
             if (prevselitem != null) {
                 prevselitem.style.backgroundColor = prebackcolor;
             }
             prevselitem = row;
             prebackcolor = row.style.backgroundColor;
             row.style.backgroundColor = 'PeachPuff';
         }
</script>
 <style type="text/css">
        .h3css
        {
            font-family: 微软雅黑;
            color: #4141a4;
            text-decoration: underline;
        }
        .tdone
        {   width:80px;
            text-align:right;
            padding-right:5px;
            color: #4a4a4a;
            }
        .txtcss
        {
            width: 200px;
            border: 1px #bebee1 solid;
            height: 25px;
            vertical-align: middle;
            line-height: 25px;
            padding: 0 10px;
            color: Black;
        }
        .txtdoenlist
        {
            width: 90px;
            height: 25px;
            line-height: 25px;
            vertical-align: middle;
            padding: 3px;
            border: 1px #bebee1 solid;
            color: #494b4c;
        }
        .txtdoenlista
        {
            width: 300px;
            height: 25px;
            line-height: 25px;
            vertical-align: middle;
            padding: 3px;
            border: 1px #bebee1 solid;
            color: #494b4c;
        }
        .buttoncss
        {
            width: 120px;
            height: 40px;
            background-color: #61a4da;
            color: #fff;
            font-family: 微软雅黑;
            font-size: 20px;
            border: 1px solid #3776a9;
            cursor: pointer;
        }
         .buttoncssa
        {
            width: 120px;
            height: 40px;
            background-color:#61a4da;
            color: #fff;
            font-family: 微软雅黑;
            font-size: 18px;
            border: 1px solid  #679c11;
            cursor: pointer; }
         .buttoncssb
        {   color:#4141a4;
            width: 50px;
            height: 25px;
            background:#fff;
            font-size:14px;
            cursor: pointer; 
            text-decoration:underline;
            float:left;
            }
              .buttoncssv
        {   color:red;
            width: 50px;
            height: 25px;
            background:#fff;
            font-size:14px;
            cursor: pointer; 
            text-decoration:underline;
            float:left;
            }
        .grouptd{ text-align:left; color:White;color:blue;height:25px}
         #showSchoolDiv
        {
            width: 730px;
            height: 100px;
            padding: 4px 10px 10px;
            background-color: #FFFFFF;
            border: 1px solid #05549d;
            color: #333333;
            line-height: 24px;
            text-align: left;
            -webkit-box-shadow: 5px 2px 6px #000;
            -moz-box-shadow: 3px 3px 6px #555;
        }
        .hidden
        {
            display: none;
        }
      .buttoncssc{
        width: 120px;
            height: 40px;
            background-color:#81c70e;
            color: #fff;
            font-family: 微软雅黑;
            font-size: 18px;
            border: 1px solid  #679c11;
            cursor: pointer; 
      }  
    </style>
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
   
    <asp:UpdatePanel ID="up1" runat ="server" >
    <ContentTemplate >
     <!--加一个半透明层--> 
    <div id="doing" style="filter:alpha(opacity=30);-moz-opacity:0.3;opacity:0.3;background-color:#000;width:100%;height:100%;z-index:1000;position: absolute;left:0;top:0;display:none;overflow: hidden;"> 
    </div> 
    <!--加一个显示层--> 
    <div id="divShow" style="border:solid 1px #898989;background:#fff;padding:10px;width:520px;z-index:1001; position: absolute; display:none;top:50%; left:60%;margin:-200px 0 0 -400px;"> 
            <div style="padding:3px 15px 3px 15px;text-align:left;vertical-align:middle;" > 
                 <div> 
        <table width="500px" cellpadding="3" cellspacing="0" style="font-size: 14px; color: #494b4c">
        <tr><td colspan="4">
    </td></tr>
    <tr><td colspan="4" height="5px"></td></tr>
    <tr><td colspan="4">
        <div  id="divManageUser" style="border: 1px solid #bababa; background-color:#ffffe1;font-size: 14px; color: #494b4c">
        <table cellpadding="3" cellspacing="0" style="font-size: 14px; color: #494b4c">
           <tr><td  class="tdone">角色选择：</td>
           <td> 
               <asp:RadioButtonList ID="rblRole" runat="server" RepeatDirection="Horizontal" 
                        Width="201px">
                        <asp:ListItem Selected="True" Value="4">高校管理员</asp:ListItem>
                        <asp:ListItem Value="5">组委会用户</asp:ListItem>
                    </asp:RadioButtonList></td></tr>
           <tr><td class="tdone">用户名：</td>
                <td><asp:TextBox ID="txtAccountNew" runat="server" CssClass="txtcss" 
                        onfocus="onftxtAccount()" onblur="onbtxtAccount()"></asp:TextBox></td></tr>
           <tr><td class="tdone">密码：</td>
               <td><asp:TextBox ID="txtPwd" runat="server" TextMode="Password"  CssClass="txtcss" onfocus="onfocusjs()" onblur="onblurjs()"></asp:TextBox>
                    <span style="font-size:12px; color:red; display:none; margin-left:10px" id="pwds">密码最少8位!</span></td></tr>
           <tr><td class="tdone">姓名：</td><td><asp:TextBox ID="txtNameNew" runat="server"  CssClass="txtcss" 
                                     onfocus="onft(this)" onblur="onbt(this)"></asp:TextBox></td></tr>
           <tr><td class="tdone">性别：</td><td><asp:RadioButtonList ID="rblSex" runat="server" RepeatDirection="Horizontal" 
                        Width="123px">
                        <asp:ListItem Selected="True" Value="1">男</asp:ListItem>
                        <asp:ListItem Value="0">女</asp:ListItem>
                    </asp:RadioButtonList></td></tr>
           <tr><td class="tdone">身份证号：</td><td><asp:TextBox ID="txtId" runat="server"  CssClass="txtcss" onfocus="onft(this)" onblur="onbt(this)"></asp:TextBox></td></tr>
           <tr><td class="tdone">所在学校：</td><td><asp:DropDownList ID="ddlCityNew" runat="server" 
                                     AutoPostBack="True" CssClass="txtdoenlist"
                        >
            <asp:ListItem>沈阳</asp:ListItem>
        </asp:DropDownList>
                    <asp:DropDownList ID="ddlSchollNew" runat="server"  CssClass="txtdoenlista">
                    </asp:DropDownList></td></tr>
           <tr><td class="tdone">电子邮箱：</td>
               <td><asp:TextBox ID="txtEmail" runat="server"  CssClass="txtcss" onfocus="onft(this)" onblur="onbt(this)"></asp:TextBox></td></tr>
           <tr><td class="tdone">联系电话：</td>
               <td><asp:TextBox ID="txtTelephone" runat="server" CssClass="txtcss" onfocus="onft(this)" onblur="onbt(this)"></asp:TextBox></td></tr>
          <tr><td colspan="2" height="5px"></td></tr>
             <tr><td class="tdone"></td><td>
                    <asp:Button ID="btnSave" OnClientClick="return IsValid();" runat="server" Text="保 存"  CssClass="buttoncssc" BorderStyle="None" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input id="btnClose" type="button" 
                        onclick="ShowNo()"  
                        value="关 闭" class="buttoncssc"  style="border-style:none "/>
                    </td></tr>   
                      <tr><td colspan="2" height="5px">
                          </td></tr>
        </table>
    </div>
    
    </td></tr>
     </table>   
                 </div>
            </div>
     </div>
     </ContentTemplate>
    </asp:UpdatePanel>

    <div id="divRole" style="border:solid 1px #898989;background:#fff;padding:10px;width:520px;z-index:1001; position: absolute; display:none;top:50%; left:60%;margin:-200px 0 0 -400px;"> 
            <div style="padding:3px 15px 3px 15px;text-align:left;vertical-align:middle;" >
            <div> 
         <table width="500px" cellpadding="3" cellspacing="0" style="font-size: 14px; color: #494b4c">
        <tr><td>角色选择：</td><td colspan="2"><asp:CheckBoxList ID="cblRole" runat="server" 
            RepeatDirection="Horizontal">
             <asp:ListItem Value="5">竞赛发布者</asp:ListItem>
             <asp:ListItem Value="6">竞赛参与者</asp:ListItem>
             <asp:ListItem Value="7">评分专家</asp:ListItem>
               <asp:ListItem Value="8">竞赛管理员</asp:ListItem>
        </asp:CheckBoxList></td> </tr>
        <tr><td class="tdone"></td><td>
                    <asp:Button ID="btnOk"  runat="server"  OnClientClick="return checkboxCount('保存','cblRole')"
                        Text="保 存"  CssClass="buttoncssc" BorderStyle="None" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input id="btnCancel" type="button" 
                        onclick="ShowNo1()"  
                        value="取 消" class="buttoncssc"  style="border-style:none "/>
                    </td> </tr>
 </table>
</div>
          </div></div>
    <table width="800px" cellpadding="3" cellspacing="0" style="font-size: 14px; color: #494b4c">
       <tr><td colspan="2"><h3 class="h3css">用户管理</h3></td></tr>
        <tr>
           <td class="tdone">用户角色：</td>
           <td> <asp:DropDownList ID="ddlRole" runat="server"  
                   CssClass="txtdoenlist">
             <asp:ListItem Value="0">全部</asp:ListItem>
             <asp:ListItem Value="5">竞赛发布者</asp:ListItem>
             <asp:ListItem Value="6">竞赛参与者</asp:ListItem>
             <asp:ListItem Value="7">评分专家</asp:ListItem>
               <asp:ListItem Value="8">竞赛管理员</asp:ListItem>
             </asp:DropDownList>  &nbsp;&nbsp;       
               </td></tr>
      
       <tr><td class="tdone">用户名：</td>
           <td><asp:TextBox ID="txtAccount" runat="server"  CssClass="txtcss" onfocus="onft(this)" onblur="onbt(this)"></asp:TextBox></td>
           </tr>
       <tr>
           <td class="tdone">姓名：</td>
           <td><asp:TextBox ID="txtName" runat="server"  CssClass="txtcss" onfocus="onft(this)" onblur="onbt(this)"></asp:TextBox></td></tr>
       <tr><td  class="tdone">所在学校：</td>
           <td><asp:DropDownList ID="ddlCity" AutoPostBack="true" runat="server" CssClass="txtdoenlist">
        </asp:DropDownList>
        <asp:DropDownList ID="ddlSchool" runat="server" CssClass="txtdoenlista">
                    </asp:DropDownList> &nbsp;      
               <asp:Button ID="btnSearch" runat="server" Text="查询" height="25px" />          
              </td></tr>
          <tr><td class="tdone"></td><td>&nbsp;</td></tr>  
          <tr><td colspan="2" style="border: 1px solid #999; background-color:#ffffe1"> 
          <div>
             <asp:GridView ID="gvUser" runat="server" AutoGenerateColumns="False" 
                  AllowPaging="True" Width="100%" BorderStyle="None" CssClass="grouptd"
              Font-Size="14px" ForeColor="#494B4C"  GridLines="None">
                 <PagerSettings Mode="NumericFirstLast" />
              <HeaderStyle BackColor="#494B4C" ForeColor="#E6E6E6" HorizontalAlign="Left" />

                 <Columns>
                    <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:CheckBox ID="ckb" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                     <asp:BoundField DataField="Account" HeaderText="用户名">
                         <ItemStyle Width="100px" Wrap="False" />
                     </asp:BoundField>
                     <asp:BoundField DataField="Name" HeaderText="姓名">
                         <ItemStyle Width="100px" Wrap="False" />
                     </asp:BoundField>
                     <asp:BoundField DataField="Sex" HeaderText="性别">
                         <ItemStyle Width="50px" Wrap="False" />
                     </asp:BoundField>
                     <asp:BoundField DataField="IDCard" HeaderText="身份证号">
                         <ItemStyle Width="170px" Wrap="False" />
                     </asp:BoundField>
                     <asp:BoundField DataField="RoleName" HeaderText="角色">
                         <ItemStyle Width="80px" Wrap="False" />
                     </asp:BoundField>
                     <asp:BoundField DataField="SchoolName" HeaderText="所在学校">
                         <ItemStyle Width="180px" Wrap="False" />
                     </asp:BoundField>
                     <asp:TemplateField HeaderText="查看" ControlStyle-Width="60px">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnView" runat="server" CommandName="ViewDetail" Text="查看"  CssClass="buttoncssb" BorderStyle="None"/>
                    </ItemTemplate>
                    <ControlStyle Width="60px"></ControlStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="编辑" ControlStyle-Width="60px">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnDel" runat="server" CommandName="ViewDelete" Text="编辑" CssClass="buttoncssv" BorderStyle="None" />
                    </ItemTemplate>
                    <ControlStyle Width="60px"></ControlStyle>
                </asp:TemplateField>
                 </Columns>

                 <PagerStyle HorizontalAlign="Center" />
        </asp:GridView></div></td></tr>  
       <tr><td colspan="2" height="5px"></td></tr>
       </table>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
      <ContentTemplate>
    <div>
        <asp:Button ID="btnAddExpert" runat="server" Text="添加管理用户" 
            CssClass="buttoncssa" BorderStyle="none" />&nbsp;
        <asp:Button ID="btnAddUser" runat="server" Text="添加普通用户" CssClass="buttoncssa" 
            BorderStyle="none"/>&nbsp;
        <asp:Button ID="btnAddRole" runat="server" Text="添加角色" CssClass="buttoncssa" OnClientClick="unChecked('cblRole');return checkboxCount('添加角色','gvUser')"
            BorderStyle="none"/>&nbsp;
        <asp:Button ID="btnCancelRole" runat="server" Text="取消角色" CssClass="buttoncssa" OnClientClick="unChecked('cblRole');return checkboxCount('取消角色','gvUser')"
            BorderStyle="none"/>
    </div>
      </ContentTemplate>
  </asp:UpdatePanel>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
用户管理
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
用户管理
</asp:Content>
