<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LA_Activity.aspx.cs" Inherits="LearningActivity.Layouts.LearningActivity.LA_Activity" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
  
    <style type="text/css">
      
         .subbtn {
             width: 133px; height: 35px; border: 0;
             font-weight: bolder; font-size: 15px; margin: 0; cursor: pointer;
         }
        .black_overlay{
            display: none;
            position: absolute;
            top: 0%;
            left: 0%;
            width: 100%;
            height: 100%;
            background-color: black;
            z-index:1001;
            -moz-opacity: 0.8;
            opacity:.80;
            filter: alpha(opacity=80);
        }
        .white_content {
            display: none;
            position: absolute;
            top: 10%;
            left: 10%;
            width: 80%;
            height: 80%;
            border: 2px solid lightblue;
            background-color: white;
            z-index:9999;
            overflow: auto;
        }
        .white_content_small {
            display: none;
            position: absolute;
            top: 20%;
            left: 30%;
            width: 40%;
            height: 50%;
            border: 2px solid lightblue;
            background-color: white;
            z-index:9999;
            overflow: auto;
        }
        .WorksMain {
            background-color: #F0F0F0;
            margin-left: 10px;
            border-radius: 5px;
            border: 1px solid #CCCCCC;
            -moz-border-radius:5px;
            -webkit-border-radius: 5px;
            width:600px;
        }
         .divDouble {
            background-color:#CAE1FF;
            margin: 5px 0px 5px 10px;
            padding: 5px 0px 5px 5px;
            border-radius: 10px;
            border: 1px solid #CCCCCC;
            -moz-border-radius:5px;
            -webkit-border-radius: 5px;
            width:600px;
        }
        .yuanjiao{
            font-family: 微软雅黑;
            border: 1px solid #379082;
            border-radius:5px 5px 5px 5px;
            background-color: white;
            width:600px;
        }
        .yuanjiaobtn{
            font-family: Arial;
            border: 1px solid #379082;
            border-radius:5px 5px 5px 5px;
            background-color:#999999;
            cursor: pointer;
            font-size: 16px;
            font-weight: bold;
            color: blue;
        }
        input[type=range]:before { content: attr(min); padding-right: 5px; }
        input[type=range]:after { content: attr(max); padding-left: 5px;}
        .datable {background-color: #9FD6FF; color:#333333; font-size:14px;}
        .datable tr {height:20px;padding: 2px 2px 2px 2px; margin: 2px 2px 2px 2px;}
        .datable .lup {background-color: #C8E1FB;font-size: 14px;color: #014F8A;}
        .datable .lup th {border-top: 1px solid #FFFFFF; border-left: 1px solid #FFFFFF;font-weight: bold;}
        .datable .lupbai {background-color: #FFFFFF;}
        .datable .trnei {background-color: #F2F9FF;}
        .datable td {border-top: 1px solid #FFFFFF; border-left: 1px solid #FFFFFF;padding: 2px 2px 2px 2px; margin: 2px 2px 2px 2px;text-align: center;}

    </style>

    <script type="text/javascript" src="JS/jquery.min.js"></script>
   

    <script type="text/javascript">
        ////弹出隐藏层
        //function ShowDiv(show_div, bg_div) {
        //    document.getElementById(show_div).style.display = 'block';
        //    document.getElementById(bg_div).style.display = 'block';
        //    var bgdiv = document.getElementById(bg_div);
        //    bgdiv.style.width = document.body.scrollWidth;
        //    // bgdiv.style.height = $(document).height();
        //    $("#" + bg_div).height($(document).height());
        //};
        ////关闭弹出层
        //function CloseDiv(show_div, bg_div) {
        //    document.getElementById(show_div).style.display = 'none';
        //    document.getElementById(bg_div).style.display = 'none';
        //};

        //function setType(p) {
        //    document.getElementById("hdType").innerText = p;
        //    document.getElementById("ATypeDiv").innerText = p;
        //}
        function CheckUrl() {
            var tburi = document.getElementById("<%=tbUrl.ClientID %>");
            var uriContent = tburi.textContent;
            if (uriContent!=null) {
                document.getElementById("ckUrl").innerHTML = "<a href='" + uriContent + "'>检测Url</a>";
            } else {
                document.getElementById("ckUrl").innerHTML = "";
            }

        }
        function catch_keydown(sel) {
            switch (event.keyCode) {

                case 13: //Enter;

                    sel.options[sel.length] = new Option("", "", false, true);

                    event.returnValue = false;

                    break;

                case 27: //Esc;

                    alert("text:" + sel.options[sel.selectedIndex].text + ", value:" + sel.options[sel.selectedIndex].value + ";");

                    event.returnValue = false;

                    break;

                case 46: //Delete;

                    if (confirm("刪除當前內容!?")) {

                        sel.options[sel.selectedIndex] = null;

                        if (sel.length > 0) { sel.options[0].selected = true; }
                    }

                    event.returnValue = false; break;

                case 8: //Back Space;

                    var s = sel.options[sel.selectedIndex].text;

                    sel.options[sel.selectedIndex].text = s.substr(0, s.length - 1);

                    event.returnValue = false; break;
            }
        }

        function catch_press(sel) {

            sel.options[sel.selectedIndex].text = sel.options[sel.selectedIndex].text + String.fromCharCode

            (event.keyCode); event.returnValue = false;

        }
    </script>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <script type="text/javascript" src="JS/index.js"></script>
    <script src="JS/FaceJScript.js" type="text/javascript"></script>

    <link rel="stylesheet" type="text/css" href="CSS/style.css" />
    <link rel="stylesheet" type="text/css" href="CSS/base.css" />
    <link rel="stylesheet" type="text/css" href="CSS/page.css" />
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div class="divDouble">
        <div align="center" id="divTitle" runat="server" >
            <h2>新建学习活动</h2>
        </div>

        <div>
            <table style="width: 100%;">
                <tr>
                    <td>
                        <div class="wt">
                <span class="redStar">*</span><label>开始时间：</label>
            </div>
            <asp:Label ID="lbStart" runat="server" Text="" Visible="False"></asp:Label>
            <SharePoint:DateTimeControl ID="DTStart" runat="server" AutoPostBack="True" OnDateChanged="DTStart_OnDateChanged" />
                    </td>
                    <td>
                         <asp:Button ID="btnsetStart" runat="server" Text="设定时间" OnClick="btnsetStart_OnClick" CssClass="yuanjiaobtn" ToolTip="设定开始时间,提交保存前必需!"/>
            <asp:Button ID="btnresetStart" runat="server" Text="重设时间" OnClick="btnresetStart_OnClick" CssClass="yuanjiaobtn" Visible="False" ToolTip="重新选择开始时间"/>
                    </td>
                </tr>
               
            </table>
            
           
        </div>
    </div>

    <div class="WorksMain" id="newlaDiv" runat="server" Visible="False"> 
        <div class="onlineEnroll-model">
            <div class="wt">
                <span class="redStar">*</span><label for="thisType">活动类型：</label>
            </div>
          
                <asp:DropDownList ID="ddlType" runat="server" Width="200px" OnSelectedIndexChanged="ddlType_OnSelectedIndexChanged"
                    AutoPostBack="True" onkeydown="catch_keydown(this);" onkeypress="catch_press(this);"></asp:DropDownList>
          
            <asp:Button ID="btnaddType" runat="server" Text="新增活动类型" OnClick="btnaddType_OnClick" CssClass="yuanjiaobtn" />
            <div id="addTypeDiv" runat="server" style="position: relative; z-index: 99; margin-left: 120px;"
                visible="False" class="onlineEnroll-model">
                <table style="background: #CAE1FF; border-radius: 5px; width: 300px;">
                    <tr>
                        <td colspan="2" style="text-align: center">
                            <div>添加新的活动类型</div>
                        </td>
                    </tr>
                    <tr style="width: 60px;">
                        <td><span class="redStar">*</span>类型名称:</td>
                        <td>
                            <asp:TextBox ID="tbTypeName" runat="server" Width="130px" CssClass="yuanjiao"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp; &nbsp;其他人可见:</td>
                        <td><asp:CheckBox ID="cbType" runat="server" AutoPostBack="True" />
                        </td>

                    </tr>
                    <tr>
                        <td>&nbsp; &nbsp;类型描述:</td>
                        <td>
                            <asp:TextBox ID="tbTypeDesc" runat="server" Width="130px" CssClass="yuanjiao"></asp:TextBox></td>

                    </tr>
                    <tr>
                        <td>&nbsp; &nbsp;所属大类:</td>
                        <td>
                            <asp:DropDownList ID="ddlParentType" runat="server"></asp:DropDownList>
                        </td>

                    </tr>
                    <tr>
                        <td colspan="2">
                            <div style="margin-left: 60px;">
                                <asp:Button ID="btnaddTypeCancel" runat="server" Text="取消添加" OnClick="btnaddTypeCancel_OnClick"
                                    CssClass="yuanjiaobtn" />
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnaddTypeEnter" runat="server" Text="确认添加" OnClick="btnaddTypeEnter_OnClick"
                                    CssClass="yuanjiaobtn" />
                            </div>
                        </td>

                    </tr>
                </table>
            </div>
        </div>
        <div class="onlineEnroll-model">
            <div class="wt">
                <span class="redStar">*</span><label>学习对象：</label>
            </div>
            <asp:DropDownList ID="ddlObject" runat="server" Width="200px" OnSelectedIndexChanged="ddlObject_OnSelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
            <asp:Button ID="addObtn" runat="server" Text="新增学习对象" OnClick="addObtn_OnClick" CssClass="yuanjiaobtn" />
            <div id="addObjectDiv" runat="server" style="position: relative; z-index: 99; margin-left: 120px;"
                visible="False" class="onlineEnroll-model">
                <table style="background: #CAE1FF; border-radius: 5px; width: 300px;">
                    <tr>
                        <td colspan="2" style="text-align: center">
                            <div>添加新的学习对象</div>
                        </td>
                    </tr>
                    <tr style="width: 60px;">
                        <td><span class="redStar">*</span>对象名称:</td>
                        <td>

                            <asp:TextBox ID="tboName" runat="server" Width="130px" CssClass="yuanjiao"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                        <td>&nbsp; &nbsp;其他人可见:</td>
                        <td><asp:CheckBox ID="cbObject" runat="server" AutoPostBack="True" />
                        </td>

                    </tr>
                    <tr>
                        <td>&nbsp; &nbsp;对象描述:</td>
                        <td>
                            <asp:TextBox ID="tboDesc" runat="server" Width="130px" CssClass="yuanjiao"></asp:TextBox></td>

                    </tr>

                  
                    <tr>
                        <td colspan="2">
                            <div style="margin-left:60px;">
                                <asp:Button ID="addCancel" runat="server" Text="取消添加" OnClick="addCancel_OnClick"
                                    CssClass="yuanjiaobtn" />
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="addEnter" runat="server" Text="确认添加" OnClick="addEnter_OnClick" CssClass="yuanjiaobtn" />
                            </div>
                        </td>

                    </tr>
                </table>
            </div>
            <div id="ddlObjectDiv" runat="server" style="position: relative; z-index: 99; margin-left: 120px;background: #CAE1FF; border-radius: 5px; width: 300px;">
            </div>
        </div>
       
        <div>
            <div class="onlineEnroll-model">
                <div class="wt">
                    <span class="redStar">*</span><label for="thisLocation">活动地点：</label>
                </div>
                <asp:DropDownList ID="ddlLocation" runat="server" Width="200px" OnSelectedIndexChanged="ddlLocation_OnSelectedIndexChanged"
                    AutoPostBack="True"></asp:DropDownList>
                <asp:Button ID="btnaddLoca" runat="server" Text="新增活动地点" OnClick="btnaddLoca_OnClick"
                    CssClass="yuanjiaobtn" />
                <div id="addLocaDiv" runat="server" style="position: relative; z-index: 99; margin-left: 120px;"
                    visible="False" class="onlineEnroll-model">
                    <table style="background: #CAE1FF; border-radius: 5px; width: 300px;">
                        <tr>
                            <td colspan="2" style="text-align: center">
                                <div>添加新的活动地点</div>
                            </td>
                        </tr>
                        <tr style="width: 60px;">
                            <td><span class="redStar">*</span>地点名称:</td>
                            <td>
                                <asp:TextBox ID="tbLocaName" runat="server" Width="130px" CssClass="yuanjiao"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp; &nbsp;其他人可见:</td>
                            <td><asp:CheckBox ID="cbLocation" runat="server" AutoPostBack="True" />
                            </td>

                        </tr>
                        <tr>
                            <td>&nbsp; &nbsp;地点描述:</td>
                            <td>
                                <asp:TextBox ID="tbLocaDesc" runat="server" Width="130px" CssClass="yuanjiao"></asp:TextBox></td>

                        </tr>
                   
                        <tr>
                            <td colspan="2" >
                                <div style="margin-left: 60px;">
                                    <asp:Button ID="btnAddLocaCancel" runat="server" Text="取消添加" OnClick="btnAddLocaCancel_OnClick"
                                        CssClass="yuanjiaobtn" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnAddLocaEnter" runat="server" Text="确认添加" OnClick="btnAddLocaEnter_OnClick"
                                        CssClass="yuanjiaobtn" />
                                </div>
                            </td>

                        </tr>
                    </table>
                </div>
            </div>
            <div class="onlineEnroll-model">
                <div class="wt">
                    <span class="redStar">*</span><label>持续时长：</label>
                </div>
                <asp:TextBox ID="tbDuring" runat="server" Width="125px" onkeyup="if(isNaN(value))execCommand('undo')" CssClass="yuanjiao" AutoPostBack="True" ToolTip="请输入10-120整数"></asp:TextBox> (单位：分钟.)
                <asp:RangeValidator ID="rvtbDuring" runat="server" ErrorMessage="请输入10~120之间的整数"
                    MinimumValue="10" MaximumValue="120" ControlToValidate="tbDuring" Type="Integer"
                    SetFocusOnError="True" Display="Static" Font-Bold="True" Font-Italic="True" ForeColor="Red"></asp:RangeValidator>
                
            </div>
            <div class="onlineEnroll-model">
                <div class="wt">
                    <label for="tbOthers">活动描述：</label>
                </div>
                <textarea id="tbOthers" cols="20" rows="5" runat="server" placeholder="请输入活动描述，不低于200字，最多500字"
                    style="width: 300px; height:80px; margin: 0"></textarea>
            </div>
            <div class="onlineEnroll-model">
                <div class="wt">
                    作品网址：
                </div>
                <asp:TextBox ID="tbUrl" runat="server" Width="300px" OnTextChanged="tbUrl_OnTextChanged" onkeypress="CheckUrl()" CssClass="yuanjiao"></asp:TextBox>
                <span id="ckUrl"></span>
                <asp:HyperLink ID="checkUrl" runat="server" Visible="False" Target="_blank" ToolTip="检测网址有效性">检测网址有效性</asp:HyperLink>
            </div>
            <div class="onlineEnroll-model" style="margin-left: 120px">
                    <span style="color: red;">注意：你可以直接粘贴作品网络地址，或者从下面上传你的作品文件。</span>
            </div>
            <div class="onlineEnroll-model">
                <div class="wt">
                    活动作品：
                </div>
                <asp:FileUpload ID="fuWorks" runat="server" />
                <asp:Button ID="btnUpFile" runat="server" Text="上传" CausesValidation="False" OnClick="btnUpFile_OnClick"
                    CssClass="yuanjiaobtn" />
                <div class="wt">
                    <asp:GridView ID="gvFiles" runat="server"></asp:GridView>
                </div>
            </div>
            <div class="onlineEnroll-model" style="margin: 0">
                <div class="GetWarnDiv" style="height: 45px">
                    <span style="font-weight: bold;">注意事项：</span>
                    <ol style="color: red">
                        <li>1、文件格式：
                            <ul style="color: #2f4f4f;">
                                <li>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;文档格式：.txt, .docx, .doc, .pdf, .ppt, .pptx, .xls, .xlsx, .accdb,</li>
                                <li>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;图片格式：.png, .jpg, .gif, .bmp, </li>
                                <li>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;音视频格式：.mp3, .mp4</li>
                            </ul>
                        </li>
                        <li>
                            2、文件大小：
                            <ul style="color: #2f4f4f;">
                                <li>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;音、视频≤50M；</li>
                                <li>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;文档≤10M；</li>
                                <li>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;图片≤5M</li>
                            </ul>
                        </li>
                    </ol>
                </div>
            </div>
            <div class="onlineEnroll-model" style="margin-left: 180px;margin-top: 100px;margin-bottom: 10px;">
                <asp:Button ID="btnSubmit" runat="server" Text="提 交" OnClick="btnSubmit_OnClick"
                    ToolTip="提交活动记录！" CssClass="yuanjiaobtn" />
                <asp:Button ID="btnSave" runat="server" Text="保 存" OnClick="btnSave_OnClick" ToolTip="保存修改活动记录！" CssClass="yuanjiaobtn" Visible="False"/>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnBack" runat="server" Text="返 回" OnClick="btnBack_OnClick" ToolTip="返回我的学习记录！"
                    CssClass="yuanjiaobtn" />
            </div>
        </div>
    </div>
    
    <div class="WorksMain" id="chooseDiv" runat="server" Visible="False">
        <div align="center" style="margin-top: 5px; margin-bottom: 10px;">
            <h3>猜你喜欢</h3><h4>推荐历史同期你做过的活动</h4>
        </div>
        <div align="center">
            <asp:GridView ID="gvFavs" runat="server" AutoGenerateColumns="False" CssClass="datable"
                border="0" CellPadding="2" CellSpacing="1" Width="100%">
                <RowStyle CssClass="lupbai" />
                <HeaderStyle CssClass="lup" />
                <AlternatingRowStyle CssClass="trnei" />
                <Columns>
                    <asp:BoundField DataField="开始时间" HeaderText="活动时间"/>
                    <asp:BoundField DataField="活动类型" HeaderText="活动类型" />
                    <asp:BoundField DataField="活动地点" HeaderText="活动地点" />
                    <asp:BoundField DataField="学习对象" HeaderText="学习对象" />
                    <asp:TemplateField HeaderText="选中">
                        <itemtemplate>
                            <input type="radio" name="rbSelectSame" value='<%# Eval("LearningActivityID")%>' />
                            </itemtemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <div style="margin-top: 10px;margin-bottom: 5px;">
                <asp:Button ID="btSelectSame" runat="server" Text="选中&编辑" OnClick="btSelectSame_OnClick" CssClass="yuanjiaobtn" />
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="btSelectNoEdit" runat="server" Text="选中&不编辑" OnClick="btSelectNoEdit_Click" CssClass="yuanjiaobtn" />               
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="btSelectCancel" runat="server" Text="不选&新建" OnClick="btSelectCancel_OnClick" CssClass="yuanjiaobtn" />
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
5Alearning
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
学习活动
</asp:Content>
