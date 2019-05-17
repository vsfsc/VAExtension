<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PDetails.aspx.cs" Inherits="Project.Layouts.Project.PDetails" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <script src="MediaPlayer.js" type="text/jscript"></script>
    <script src="Validate.js" type="text/jscript"></script>
    <%--    <script type="text/javascript">
        function radioButChange()
        {
            var radioBtnPerson = document.getElementById('rd1');
            var radioBtnEnter = document.getElementById('rd2');
            var divEnterP=document.getElementById("divMatchEnterP");
            if (radioButPerson.Checked == true)
                divEnterP.style.display='none';
            if (radioButEnter.Checked == true)
                divEnterP.style.display = '';
        }
    </script>--%>
        <script type="text/javascript">
            function txtdiva() {
                document.getElementById("txtDiv").style.display = "";
                document.getElementById('btnSubmitShow').style.display = '';
                document.getElementById('inputbutton').style.display = 'none';

            }
            function YiComments() {
                document.getElementById('inputbutton').style.display = 'none';
            }
            function ValidComments() {
                var txt = document.getElementById('<%=txtComments.ClientID%>');
               if (txt.value.length == 0) {
                   alert("评语不能为空！");
                   txt.select();
                   return false;
               }
               return true
           }
           function rate(obj, oEvent) {

               // 图片地址设置
               var imgSrc = 'images/star_gray.gif'; //没有填色的星星
               var imgSrc2 = 'images/star_red.gif'; //打分后有颜色的星星

               if (obj.rateFlag) return;
               var e = oEvent || window.event;
               var target = e.target || e.srcElement;
               var imgArray = obj.getElementsByTagName("img");
               for (var i = 0; i < imgArray.length; i++) {
                   imgArray[i]._num = i;
                   imgArray[i].onclick = function () {
                       if (obj.rateFlag) return;
                       obj.rateFlag = true;
                       document.getElementById('hdStars').value = this._num + 1;
                   };
               }
               if (target.tagName == "IMG") {
                   for (var j = 0; j < imgArray.length; j++) {
                       if (j <= target._num) {
                           imgArray[j].src = imgSrc2;
                       } else {
                           imgArray[j].src = imgSrc;
                       }
                   }
               } else {
                   for (var k = 0; k < imgArray.length; k++) {
                       imgArray[k].src = imgSrc;
                   }
               }
           }
        </script>
        <style type="text/css">
            #showDiv {
                margin: 0;
                text-align: left;
                background-color: #fff;
                padding: 10px 20px 5px;
            }
            
            .f {
                color: #FFFFFF;
                text-align: center;
            }
            
            .f ul {
                margin: 0;
                padding: 0;
                text-align: left;
                margin-left: 10px;
                margin-top: 15px;
            }
            
            .f ul li {
                padding: 2px 0;
            }
            
            .f ul li a {
                font-size: 13px;
                font-family: '微软雅黑';
                color: #b7b7b7;
                text-decoration: none;
            }
            
            .f ul li a:hover {
                color: #ff6633
            }
            
            .grouptd {
                text-align: left;
                height: 25px;
                padding: 0;
                margin: 0;
            }
            
            .buttoncssb {
                color: #4141a4;
                width: 50px;
                height: 25px;
                background: #fff;
                font-size: 14px;
                cursor: pointer;
                text-decoration: underline;
                float: left;
            }
            
            .liuyan {
                font-weight: bold;
                font-size: 13px;
            }
            
            .fenshu {
                color: #f00
            }
            
            .neirong {
                color: #666
            }
        </style>
        <script type="text/javascript" src="JS/jquery-1.9.1.min.js"></script>

        <script>
            $(document).ready(function () {


                /*隐藏*/
                function ml_close_demo() {
                    $('.float-news').animate({
                        right: '-450px'
                    }, 300, function () {
                        $('.float-open').delay(50).animate({
                            right: '-2px'
                        }, 300);
                    });
                }
                function ml_open_demo() {
                    $('.float-open').animate({
                        right: '-70px'
                    }, 100, function () {
                        $('.float-news').delay(50).animate({
                            right: '0px'
                        }, 300);
                    });
                }

                $('.float-close').click(function () {
                    ml_close_demo();
                    return false;
                });
                $('.open-btn').click(function () {
                    ml_open_demo();
                    return false;
                });

                setTimeout(function () { ml_close_demo() }, 1000);
            })
            /*滑入弹出下拉*/
            function onft1() {
                document.getElementById('HiddenDiv1').style.display = 'inline';

            }
            function onbt1() {
                document.getElementById('HiddenDiv1').style.display = 'none';

            }
            function onft2() {
                document.getElementById('HiddenDiv2').style.display = 'inline';

            }
            function onbt2() {
                document.getElementById('HiddenDiv2').style.display = 'none';

            }
            function onft3() {
                document.getElementById('HiddenDiv3').style.display = 'inline';

            }
            function onbt3() {
                document.getElementById('HiddenDiv3').style.display = 'none';

            }
            function onft4() {
                document.getElementById('HiddenDiv4').style.display = 'inline';

            }
            function onbt4() {
                document.getElementById('HiddenDiv4').style.display = 'none';

            }
            function onft5() {
                document.getElementById('HiddenDiv5').style.display = 'inline';

            }
            function onbt5() {
                document.getElementById('HiddenDiv5').style.display = 'none';

            }
            /* 关闭*/
            function IsValid() {
                var chkValue = getCookie("CheckCode")
                if (chkValue == null) {
                    alert("您的浏览器设置已被禁用 Cookies，您必须设置浏览器允许使用 Cookies 选项后才能使用本系统。");
                    return false;
                }
                var checkCode = document.getElementById('<%=txtAuthCode.ClientID%>');

            if (checkCode.value.length == 0 || checkCode.value.toLowerCase() != chkValue.toLowerCase()) {
                alert("验证码错误");
                return false;
            }


            var name = document.getElementById('<%=txtName.ClientID%>');
            if (name.value.length == 0) {
                alert("姓名不能为空！");
                name.select();
                return false;
            }

            var tel = document.getElementById('<%=txtPhone.ClientID%>');
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
        window.onbeforeunload = function () {
            var n = window.event.screenX - window.screenLeft;
            var b = n > document.documentElement.scrollWidth - 20;
            if (b && window.event.clientY < 0 || window.event.altKey)  // 是关闭而非刷新 
            {
                document.getElementById('btnClose').click();
            }
        }
        document.onkeydown = function () {
            if (event.keyCode == 116)//屏蔽 F5 刷新键
            {
                event.keyCode = 0;
                event.returnValue = false;
            }
        }
        </script>
        <style type="text/css">
            .txtcss {
                width: 50px;
                border: 1px #bebee1 solid;
                height: 25px;
                vertical-align: middle;
                line-height: 25px;
                padding: 0 5px;
                font-size: 16px;
                color: Black;
            }
            
            .spanfen {
                color: #999;
            }
            
            .divpoint img {
                margin: 0px 0 0 5px;
            }
            
            .lblcss {
                padding: 5px;
                margin: 5px;
                line-height: 1.5;
                text-indent: 20px;
                color: #555;
            }
        </style>
        <link rel="stylesheet" type="text/css" href="./css/base.css" />
        <link rel="stylesheet" type="text/css" href="./css/page.css" />

</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    
    <div id="DetailsDiv">
        <%-- 错误提示区 --%>
        <div id="error_txt" runat="server" Visible="False">
            对不起,你所查看的项目不存在或者无权查看此页面!
        </div>

        <%-- 项目内容开始 --%>
        <div id="pDetailsDiv" runat="server" class="comments-Main">
            <div class="comments-Content">
                    <!--项目标题与加入按钮 -->
                <div class="comments-Workstitle">
                    <h1>
                        <asp:Label ID="lbProjectName" runat="server" Text="Android智能终端二维码安全检测系统"></asp:Label>
                        <asp:Button ID="btnJoinIn" runat="server" Text="加入项目" Style="width: 100px; height: 30px; background: url(images/ButtonBg.gif); border: 0; color: #fff; font-weight: bolder;font-size: 15px; margin: 0; cursor: pointer; margin: 0 0 5px 300px;" OnClick="btnJoinIn_Click"/>
                    </h1>

                </div>
                    <%--所属学科--%>
                <div class="comments-WorksType">
                        <h3>学科：<asp:Label ID="lbSubject" runat="server" Text=""></asp:Label></h3>
                    </div>
                
                <%--项目简介 --%>
                <div class="comments-SubmitProfile">
                        <div class="comments-SubmitProfile-Titlebg">
                            <div class="comments-SubmitProfile-Title">
                                <h2>项目简介</h2>
                            </div>
                        </div>
                        <div class="comments-SubmitProfile-content">
                            <p>
                                <asp:Label ID="lblSubmitProfile" runat="server" Text=""></asp:Label>
                            </p>
                            <div id="divIntroduce" runat="server" class="comments-SubmitProfile-divWorksFile">
                                对二维码解码之后的结果进行分类检测，分别检测恶意钓鱼网站、跨站脚本攻击和恶意APP应用下载等攻击方式，结合黑/白名单检测技术、文本相似性匹配技术、机器学习和数据挖掘技术、恶意代码分析技术等，提供全方位的二维码分析检测功能。
                            </div>
                        </div>
                    </div>
                          
                    <%--立项报告--%>        
                <div class="comments-SubmitProfile" id="docLixiang" runat="server" Visible="False">
                            <div class="comments-SubmitProfile-Titlebg">
                                <div class="comments-SubmitProfile-Title">
                                    <h2>项目立项申请书</h2>
                                </div>
                            </div>
                            <div class="comments-SubmitProfile-content">
                                <div runat="server" id="divReport0" class="comments-SubmitProfile-divWorksFile">
                                    <%--立项报告--%>
                                        <iframe src='http://va.neu.edu.cn/Projects/_layouts/15/WopiFrame.aspx?sourcedoc=%2FProjects%2FDocLib1%2F%E7%AB%8B%E9%A1%B9%E7%94%B3%E8%AF%B7%2Edoc&action=embedview&wdStartOn=1'
                                            width='600px' height='400px' frameborder='1'></iframe>
                                </div>
                            </div>
                        </div>
                    <%--中期报告--%>        
                <div class="comments-SubmitProfile" id="docZhongqi" runat="server" visible="False">
                            <div class="comments-SubmitProfile-Titlebg">
                                <div class="comments-SubmitProfile-Title">
                                    <h2>项目中期检查报告</h2>
                                </div>
                            </div>
                            <div class="comments-SubmitProfile-content">
                                <div runat="server" id="divReport1" class="comments-SubmitProfile-divWorksFile">
                                    <%--中期报告--%>
                                        <iframe src='http://va.neu.edu.cn/Projects/_layouts/15/WopiFrame.aspx?sourcedoc=%2FProjects%2FDocLib1%2F%E9%A1%B9%E7%9B%AE%E4%B8%AD%E6%9C%9F%E9%AA%8C%E6%94%B6%E6%8A%A5%E5%91%8A%2Edoc&action=embedview&wdStartOn=1'
                                            width='600px' height='400px' frameborder='1'></iframe>
                                </div>
                            </div>
                        </div>
                    <%--结题报告--%>
                <div class="comments-SubmitProfile" id="docJieti" runat="server" visible="False">
                            <div class="comments-SubmitProfile-Titlebg">
                                <div class="comments-SubmitProfile-Title">
                                    <h2>项目结题验收报告</h2>
                                </div>
                            </div>
                            <div class="comments-SubmitProfile-content">
                                <div runat="server" id="divReport2" class="comments-SubmitProfile-divWorksFile">
                                    <%--结题报告--%>
                                        <iframe src='http://va.neu.edu.cn/Projects/_layouts/15/WopiFrame.aspx?sourcedoc=%2FProjects%2FDocLib1%2F%E7%BB%93%E9%A2%98%E9%AA%8C%E6%94%B6%2Edoc&action=embedview&wdStartOn=1'
                                            width='600px' height='400px' frameborder='1'></iframe>
                                </div>
                            </div>
                            <%--<div class="comments-DocView-title"></div>--%>
                        </div>
                    <%--视频资料--%> 
                <%--<div class="comments-divViewShow">
                    <div class="comments-divViewShow-title">讲解视频</div>
                    <div runat="server" id="divViewShow" class="comments-divViewShow-Show"></div>
                </div>--%>
                    <%-- 项目工作内容 --%>
                <div id="pWorksDiv">
                                
                </div>         
            </div>
        </div>    
        <%--项目内容结束--%>
            <%--项目点评--%>
       <div runat="server" id="divPublicComments" visible="False">
            <table cellpadding="2" cellspacing="0" style="font-size: 14px; width: 800px; text-align: left;margin-left: 100px">
                <tr>
                    <td class="titlebga" style="background-color: #6e5328;color: white" id="a07">
                        <div class="titlebgaa">作品点评</div>
                    </td>
                </tr>
                <tr>
                    <td height="8px"></td>
                </tr>
                <tr>
                    <td style="padding-left: 20px; height: 20px; border-bottom: 1px dotted #555">
                        <div>
                            <span style="color: #777">当前评级：</span>
                            <span runat="server" id="divScore">
                                <img src="images/star_red.gif" alt=""/>
                                <img src="images/star_red.gif" alt="" />
                                <img src="images/star_gray.gif" alt="" />
                                <img src="images/star_gray.gif" alt=""/>
                                <img src="images/star_gray.gif" alt="" />
                            </span>&nbsp;
                            <span style="color: #1a66b3">已有<asp:Label ID="lblPersons" runat="server" Text="1"></asp:Label>人评论</span>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td height="5px"></td>
                </tr>

                <tr>
                    <td style="padding-left: 20px">

                        <asp:GridView ID="gvComments" runat="server" AutoGenerateColumns="False" AllowPaging="True" BorderStyle="None" CssClass="grouptd" GridLines="None" AlternatingRowStyle-HorizontalAlign="Left" HorizontalAlign="Left">
                            <Columns>
                                <asp:BoundField DataField="CreatedBy" HeaderText="点评者">
                                    <ItemStyle Width="100px" Wrap="False" CssClass="liuyan" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Comments" HeaderText="评论内容">
                                    <ItemStyle Width="500px" CssClass="neirong" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Stars" HeaderText="评分">
                                    <ItemStyle Width="60px" Wrap="False" CssClass="fenshu" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Created" DataFormatString="{0:yyyy-MM-dd}" HeaderText="点评时间">
                                    <ItemStyle Width="100px" Wrap="False" />
                                </asp:BoundField>
                            </Columns>
                            <PagerStyle HorizontalAlign="Center" />
                            <HeaderStyle BackColor="#494B4C" ForeColor="#E6E6E6" HorizontalAlign="Left" />

                            <AlternatingRowStyle HorizontalAlign="Left" BackColor="#CCFFFF"></AlternatingRowStyle>
                        </asp:GridView>

                    </td>
                </tr>
                <tr>
                    <td height="5px"></td>
                </tr>
                <tr>
                    <td style="padding-left: 20px; color: #555">
                        <div id="txtDiv" style="display: none">
                            <div style="margin-bottom: 5px">
                                <span style="color: #777">评分：</span>

                                <span onmouseover="rate(this,event)">
                                                    <img src="images/star_gray.gif" /><img src="images/star_gray.gif" /><img src="images/star_gray.gif" /><img
                                                        src="images/star_gray.gif" /><img src="images/star_gray.gif" /></span>
                            </div>
                            <div>
                                评语：<br />
                                <asp:TextBox ID="txtComments" runat="server" Height="150px" Width="500px" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td height="5px"></td>
                </tr>
                <tr>
                    <td style="padding-left: 20px;">
                        <input type="button" value="我要点评" class="buttoncss" onclick="txtdiva()" id="inputbutton" />
                        <asp:Button ID="btnSubmitShow" OnClientClick="return ValidComments()" runat="server" Text="发表" CssClass="buttoncssa" Style="display: none" BorderStyle="None" />
                        <asp:HiddenField ID="hdStars" Value="0" runat="server" />

                    </td>
                </tr>
                <tr>
                    <td height="30px"></td>
                </tr>
            </table>
    </div>
            <%--项目评分--%>
        <div runat="server" id="divWorksScore" visible="False">
                <div class="float-open" id="float-open" style="right: -2px; background-color: #61a4da">
                    <a class="open-btn" href="javascript:void(0);"></a>
                </div>
                <div class="float-news" id="float-news" style="right: -450px;">
                    <a class="float-close" href="javascript:void(0);"></a>
                    <div class="newslist">
                        <div runat="server" id="divContent" style="font-size: 14px; color: #494b4c; margin: 0;padding: 0;">
                            <ol>
                                <li>立项选题(20′):&nbsp;<input id="t1" type="text" value="15" /></li>
                                <li>特色创意(20′):&nbsp;<input id="t2" type="text" value="15" /></li>
                                <li>&nbsp;完 整 性(15′):&nbsp;<input id="t3" type="text" value="15" /></li>
                                <li>&nbsp;合 理 性(20′):&nbsp;<input id="t4" type="text" value="15" /></li>
                                <li>价值意义(25′):&nbsp;<input id="t5" type="text" value="15" /></li>
                            </ol>
                        </div>
                        <div class="comments-scores-ModelPoint">
                            <h6 class="comments-scores-modelTitle">评语（30字以上）</h6>
                        </div>
                        <asp:TextBox ID="txtScorePingYu" TextMode="MultiLine" runat="server" Height="80px" Width="280px" BorderColor="#dbdbdb" BorderStyle="Solid"
                            Style="margin-left: 5px"></asp:TextBox>
                        <!-- 小计 -->
                        <div class="divpoint" style="text-align: left; display: block; margin: 15px 0; margin-left: 5px">
                            <span class="comments-deifena">小计:</span>
                            <asp:Label ID="lblTotalScore" runat="server" class="comments-scoresNamber" Text="90"></asp:Label>
                            <span class="comments-deifena">分(满分100)
                        </span> &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;

                            <input id="btnPingfen" type="button" value="提 交" onclick="alert('评分成功')" />
                        </div>

                        <asp:HiddenField ID="HiddenField1" runat="server" Value="-1" />
                    </div>
                </div>
            </div>
            <%-- 项目审批功能区 --%>
        <div id="divPApprove" runat="server" visible="False" style="margin-left: 100px; ">
                <hr style="border: 1px dotted #6e5328;" />
                    <p>
                        <label class="WorksVerification"><span class="redStar">* &nbsp;</span>审批意见:</label>
                        <%-- <asp:TextBox ID="txtApproveComments" runat="server" Width="450px" TextMode="MultiLine" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>--%>
                            <textarea name="txtApproveComments" id="txtApproveComments" cols="80" rows="5" runat="server"></textarea>
                    </p>

                    <p style="clear: both; margin:10px 160px 10px 200px;">
                        <asp:Button ID="btnPass" runat="server" Text="通过立项" Style="width: 100px; height: 30px; background: url(images/ButtonBg.gif); border: 0; color: #fff; font-weight: bolder; font-size: 15px; margin: 0; cursor: pointer"
                        />
                        <asp:Button ID="btnBack" runat="server" Text="不合格,退回" Style="width: 120px; height: 30px; background: url(images/ButtonBg.gif); border: 0; color: #fff; font-weight: bolder; font-size: 15px; margin-left: 10px; cursor: pointer"
                        />
                    </p>
            </div>
            <%-- 项目对接功能区 --%>
        <div id="divMatchSender" runat="server" style="margin-left: 100px;" visible="False">
                <hr style="border: 1px dotted #6e5328;" />
            
                    <p style="margin: 5px 5px 5px 5px">
                        <label class="WorksVerification" for="txtType"><span class="redStar">&nbsp; * </span>类  别:</label>

                        <%--<input id="rd1" type="radio" name="matchtype" onselect="radioButChange()" onclick="radioButChange()"/><input id="rd2" type="radio" name="matchtype" onselect="radioButChange()" onclick="radioButChange()"/>--%>
                            <asp:RadioButton ID="matchPerson" runat="server" Text="个人" TextAlign="Right" GroupName="Match" AutoPostBack="true" OnCheckedChanged="matchPerson_CheckedChanged"
                            />
                            <asp:RadioButton ID="enterprise" runat="server" Text="企业" TextAlign="Right" GroupName="Match" AutoPostBack="true" OnCheckedChanged="enterprise_CheckedChanged"
                            />
                    </p>

                    <div id="divMatchEnterP" style="margin: 5px 5px 5px 5px" runat="server" visible="false">
                        <label class="WorksVerification" for="txtEnterName"><span class="redStar">&nbsp; * </span>企业名称:</label>
                        <input type="text" name="txtEnterName" id="txtEnterName" runat="server" title="请填写真实名称" />
                    </div>
                    <p style="margin: 5px 5px 5px 5px">
                        <label class="WorksVerification" for="txtMatchIdeas"><span class="redStar">&nbsp; * </span>我对此项目的看法或建议:</label>
                    </p>
                    <p style="margin: 5px 5px 5px 5px">
                        <textarea name="txtMatchIdeas" id="txtMatchIdeas" cols="100" rows="5" runat="server"></textarea>
                    </p>
                    <p style="margin: 5px 5px 5px 5px">
                        <label class="WorksVerification" for="txtName"><span class="redStar">&nbsp; * </span>姓  名:</label>
                        <input type="text" name="txtName" id="txtName" runat="server" title="请填写真实姓名" />
                        <label class="WorksVerification" for="txtEmail"><span class="redStar">&nbsp; * </span>E-Mail:</label>
                        <input type="text" name="txtEmail" id="txtEmail" runat="server" title="请填写正确的Email地址" />
                    </p>
                    <p style="margin: 5px 5px 5px 5px">


                            <label class="WorksVerification" for="txtPhone"><span class="redStar">&nbsp; * </span>电  话:</label>
                            <input type="text" name="txtPhone" id="txtPhone" runat="server" title="请填写正确的联系电话,方便项目负责人联系到您!" />

                            <label class="WorksVerification" for="txtAuthCode"><span class="redStar">&nbsp; * </span>验证码:</label>
                            <input type="text" name="txtAuthCode" id="txtAuthCode" runat="server" />
                            <img src="GenerateCheckCode.aspx" alt="点击刷新验证码" height="24" id="imag1" style="cursor: pointer;vertical-align: bottom" onclick="javascript:getimgcode();"
                                title="点击刷新验证码" />
                            <script type="text/javascript">
                                function getimgcode() {
                                    document.getElementById("imag1").src = document.getElementById("imag1").src + '?';

                                }
                            </script>
                    </p>

                    <p style="clear: both; margin: 10px 160px 10px 200px;">
                        <asp:Button ID="btnMatchSubmit" runat="server" Text="申请对接" Style="width: 100px; height: 30px; background: url(images/ButtonBg.gif); border: 0; color: #fff; font-weight: bolder; font-size: 15px; margin: 0; cursor: pointer"
                            OnClick="btnMatchSubmit_Click" />
                    </p>

            </div>
    </div>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    项目详情
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server">
    <asp:Label ID="lbPNameforPageTitle" runat="server" Text="项目详情"></asp:Label>
</asp:Content>