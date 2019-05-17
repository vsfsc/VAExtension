<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LA_Details.aspx.cs" Inherits="LearningActivity.Layouts.LA_Details" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <%--<meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" />--%>
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
            .lista {
                font-size: 14px;
                height: 30px;
                color: #fffff;

            }
    </style>

    <script type="text/javascript" src="JS/jquery.min.js"></script>

    <script type="text/javascript">
        //弹出隐藏层
        function ShowDiv(show_div, bg_div) {
            document.getElementById(show_div).style.display = 'block';
            document.getElementById(bg_div).style.display = 'block';
            var bgdiv = document.getElementById(bg_div);
            bgdiv.style.width = document.body.scrollWidth;
            // bgdiv.style.height = $(document).height();
            $("#" + bg_div).height($(document).height());
        };
        //关闭弹出层
        function CloseDiv(show_div, bg_div) {
            document.getElementById(show_div).style.display = 'none';
            document.getElementById(bg_div).style.display = 'none';
        };

        function setType(a) {
            alert(a.innerText);
            var lbtype = document.getElementById('<%=lbtype.ClientID %>');
            lbtype.value = this.innerText;
            var tt = document.getElementById('<%=thisType.ClientID %>');
            tt.innerText = this.innerText;
            var tt = document.getElementById('<%=thisType.ClientID %>');
            document.getElementById('thisTypeID').innerText = this.innerText;
        }
        function showadd(a, b) {
            document.getElementById(a).style.display = '';
            document.getElementById(b).style.display = 'none';
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
    <div style="width: 600px;">
        <div id="fade" class="black_overlay"></div>
        <div id="TypeDiv" class="white_content">
            <div style="text-align: right; cursor: default; height: 40px;">
                <a style="font-size: 16px;cursor: pointer" onclick="CloseDiv('TypeDiv','fade')">关闭</a>
            </div>
            <h2>活动类型选择</h2>
            <input id="BtnaddTypes" type="button" value="新增活动类型" onclick="showadd('AddTypeDiv', 'cType')" />
            <asp:Button ID="typebtn" runat="server" Text="新增活动类型" />
            <%-- 新增活动类型时,默认将其选择为当前选择活动类型,并关闭类型选择浮动窗口 --%>
            <hr />
            <div id="cType">
            <div id="TypeContent" runat="server">
               
            </div>
            </div>
            <div id="AddTypeDiv" style="display: none">
                <div class="onlineEnroll-model">
                    <div class="wt">
                        <span class="redStar">*</span><label>类型分组：</label>
                    </div>
                    <asp:DropDownList ID="ddlParentType" runat="server">
                        <asp:ListItem>请选择类别分组</asp:ListItem>
                    </asp:DropDownList>
                    <label>若创建新的类别分组，此项不用选</label>
                </div>
                <div class="onlineEnroll-model">
                    <div class="wt">
                        <span class="redStar">*</span><label for="tbAction">类型名称：</label>
                    </div>
                    <input id="tbAction" type="text"  runat="server"/>
                </div>
                <div class="onlineEnroll-model">
                    <div class="wt">
                        <span class="redStar">*</span><label for="tbTypeDesc">类型描述：</label>
                    </div>
                    <textarea id="tbTypeDesc" cols="20" rows="3" runat="server" style="width: 400px;
                        height: 50px; margin: 0"></textarea>
                </div>
                <div class="onlineEnroll-model" style="margin-left: 200px; margin-top: 30px;">
                    <asp:Button ID="addTypeBtn" runat="server" Text="添加新类别" OnClick="addTypeBtn_OnClick" ToolTip="新增活动类别！" />&nbsp;&nbsp;&nbsp;&nbsp;
                    <input id="Button1" type="button" value="取 消" onclick="showadd('cType', 'AddTypeDiv')" />
                </div>
            </div>
        </div>
        <div id="ObjectDiv" class="white_content">
            <div style="text-align: right; cursor: default; height: 40px;">
                <a style="font-size: 16px; cursor: pointer" onclick="CloseDiv('ObjectDiv','fade')">关闭</a>
            </div>
            <h2> 学习对象选择 </h2>
            <input id="btnAddObj" type="button" value="新增学习对象" onclick="showadd('AddObjectDiv','ObjectContent')" />
            <%-- 新增活动类型时,默认将其选择为当前选择活动类型,并关闭类型选择浮动窗口 --%>
            <hr />
            <div id="ObjectContent" runat="server">
                
            </div>
            <div id="AddObjectDiv" style="display: none">
                <div class="onlineEnroll-model">
                    <div class="wt">
                        <span class="redStar">*</span><label for="tbObjTitle">对象名称：</label>
                    </div>
                    <input id="tbObjTitle" type="text" runat="server"/>
                </div>
                <div class="onlineEnroll-model">
                    <div class="wt">
                        <span class="redStar">*</span><label for="tbObjDesc">对象描述：</label>
                    </div>
                    <textarea id="tbObjDesc" cols="20" rows="3" runat="server" style="width: 400px; height: 50px;
                        margin: 0"></textarea>
                </div>
                <div class="onlineEnroll-model">
                    <div class="wt">
                        <span class="redStar">*</span><label for="tbObjContent">学习内容：</label>
                    </div>
                    <textarea id="tbObjContent" cols="20" rows="5" runat="server" style="width: 400px;
                        height: 100px; margin: 0"></textarea>
                </div>
                <div class="onlineEnroll-model" style="margin-left: 200px; margin-top: 30px;">
                    <asp:Button ID="addObjBtn" runat="server" Text="添 加" OnClick="addObjBtn_OnClick" ToolTip="新增学习对象" />
                </div>
            </div>
        </div>
        <div id="LocationDiv" class="white_content">
            <div style="text-align: right; cursor: default; height: 40px;">
                <a style="font-size: 16px; cursor: pointer" onclick="CloseDiv('LocationDiv','fade')">关闭</a>
            </div>
            <h2> 学习地点选择 </h2>
            <input id="btnAddLocation" type="button" value="新增学习地点" onclick="showadd('AddLocationDiv', 'LocationContent')" />
            <hr/>
            <div id="LocationContent" runat="server">
                
            </div>
            <div id="AddLocationDiv" style="display: none">
                <div class="onlineEnroll-model">
                    <div class="wt">
                        <span class="redStar">*</span><label>地点分组：</label>
                    </div>
                    <asp:DropDownList ID="ddlPLocation" runat="server">
                        <asp:ListItem>请选择地点分组</asp:ListItem>
                    </asp:DropDownList>
                    <label>若创建新的地点分组，此项不用选</label>
                </div>
                <div class="onlineEnroll-model">
                    <div class="wt">
                        <span class="redStar">*</span><label for="tbLocation">地点名称：</label>
                    </div>
                    <input id="tbLocation" type="text" />
                </div>
                <div class="onlineEnroll-model">
                    <div class="wt">
                        <span class="redStar">*</span><label for="tbLocationDesc">地点描述：</label>
                    </div>
                    <textarea id="tbLocationDesc" cols="20" rows="3" runat="server" style="width: 400px;"></textarea>
                </div>
                <div class="onlineEnroll-model" style="margin-left: 200px; margin-top: 30px;">
                    <asp:Button ID="addLocatonBtn" runat="server" Text="添加新地点" OnClick="addLocatonBtn_OnClick" ToolTip="新增活动地点！" />&nbsp;&nbsp;&nbsp;&nbsp;
                </div>
            </div>
        </div>

        <div class="onlineEnroll-model">
            <div class="wt">
                <span class="redStar">*</span><label for="thisType">活动类型：</label>
            </div>
            <asp:DropDownList ID="ddlType" runat="server"></asp:DropDownList>
            <input type="text" name="thisType" id="thisType" placeholder="点击选择活动类型" runat="server"
                onclick="ShowDiv('TypeDiv', 'fade')" style="width: 400px;"/>
            <asp:TextBox ID="lbtype" runat="server"></asp:TextBox>
            <input id="thisTypeID" type="hidden" runat="server" />
            
        </div>
        <div class="onlineEnroll-model">
            <div class="wt">
                <span class="redStar">*</span><label for="thisObj">学习对象：</label>
            </div>
            <input type="text" name="thisObj" id="thisObj" placeholder="点击选择学习对象" runat="server"
                onclick="ShowDiv('ObjectDiv', 'fade')" style="width: 400px;" />
            <input id="thisObjID" type="hidden" runat="server" />
        </div>
        <div>
            <div class="onlineEnroll-model">
                <div class="wt">
                    <span class="redStar">*</span><label for="thisLocation">活动地点：</label>
                </div>
                <input type="text" name="thisLocation" id="thisLocation" placeholder="点击选择学习地点" runat="server"
                    onclick="ShowDiv('LocationDiv', 'fade')" style="width: 400px;" />
                <input id="thisLocaID" type="hidden" runat="server" />
            </div>
            <div class="onlineEnroll-model">
                <div class="wt">
                    <span class="redStar">*</span><label>开始时间：</label>
                </div>
                <SharePoint:DateTimeControl ID="DTStart" runat="server" HoursMode24="True"/>
            </div>
            <div class="onlineEnroll-model">
                <div class="wt">
                    <span class="redStar">*</span><label for="tbDuring">持续时长：</label>
                </div>
                <input id="tbDuring" type="text" style="width: 400px;" />
            </div>
            <div class="onlineEnroll-model">
                <div class="wt">
                    <span class="redStar">*</span><label for="tbOthers">活动描述：</label>
                </div>
                <textarea id="tbOthers" cols="20" rows="5" runat="server" placeholder="请输入活动描述，不低于200字，最多500字" style="width: 400px;height: 100px;margin:0"></textarea>
            </div>
            <div class="onlineEnroll-model">
                <div class="wt">
                    <span class="redStar">*</span>活动作品：
                </div>
                <asp:FileUpload ID="fuWorks" runat="server" Width="280px" />
                <asp:Button ID="btnUpFile" runat="server" Text="上传" CausesValidation="False" Width="80px"/>
                <div class="wt">
                    <asp:Label ID="error" runat="server" Text=""></asp:Label>
                </div>
            </div>
            <div class="onlineEnroll-model">
                <div class="GetWarnDiv" style="height: 45px">
                    <span style="font-weight: bord;">注意事项：</span>
                    <ul style="color: red">
                        <li style="border-bottom: 1px solid #666 dotted;">1、文件格式：.doc .ppt .pdf .xls .mp3 .mp4 .jpg .png；</li>
                        <li style="border-bottom: 1px solid #666 dotted;">2、文件大小：音、视频≤50M；文档≤10M；图片≤5M</li>
                    </ul>
                </div>
            </div>
            <div class="onlineEnroll-model" style="margin-left:200px;margin-top: 30px;">
                <asp:Button ID="btnSubmit" runat="server" Text="提 交" OnClick="btnSubmit_OnClick" ToolTip="提交活动记录！" />
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
