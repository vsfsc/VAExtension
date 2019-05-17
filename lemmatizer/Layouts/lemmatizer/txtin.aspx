<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="txtin.aspx.cs" Inherits="lemmatizer.Layouts.lemmatizer.txtin" DynamicMasterPageFile="~masterurl/default.master" EnableEventValidation="false" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <!-- #region 全局样式表 -->
    <style type="text/css">/*全局样式表*/
         .yuanjiao{
             font-family: Arial;
             border: 1px solid #379082;
             border-radius:5px 5px 5px 5px;
             background-color: white;
             margin:10px;
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
        .hoverTable{
            width:900px;
            border-collapse:collapse;
        }
        /* rm css */
        .rm_mainContent{
            width:1000px;
            background-color:#e7e9e8;
            padding-left:5px;
        }
        .hoverTable td{
            padding:5px; border:none;
        }
        /* Define the default color for all the table rows */
        .hoverTable tr{
            background: #e9e7e7;
        }
        /* Define the hover highlight color for the table row */
        /*.hoverTable tr:hover {
              background-color: #ffff99;
        }*/
        .resultDiv {
            width: 800px;
            height: 200px;
            overflow-x: hidden;
            padding: 10px;
            -moz-border-radius: 15px;
            -webkit-border-radius: 15px;
            border-radius: 15px;
        }
          .ta{
    	    -moz-box-shadow:1px 1px 0 #E7E7E7;
            -moz-box-sizing:border-box;
            border-color:#CCCCCC #999999 #999999 #CCCCCC;
            border-style:solid;
            border-width:1px;
            font-family:arial,sans-serif;
            font-size:13px;
            height:280px;
            margin:10px auto;
            outline-color:-moz-use-text-color;
            outline-style:none;
            outline-width:medium;
            padding:2px;
            width:980px;
            border-radius:5px 5px 5px 5px;
              margin: 4px 4px 4px 4px;
          }
        .FileUpload
        {

        border: solid 1px #999999;
        background-color: #ffffff;
        background-image: none;
        margin-top:4px;
        width:250px;
        margin-right:16px;
        }
        .fakeContainer {
            float: left;
            margin: 20px;
            border: none;
            width: 640px;
            height: 320px;
	        background-color: #ffffff;
            overflow: hidden;
        }
    </style>


    <!-- #endregion -->


    <style type="text/css">
   /******输出结果三个tab样式表******/
        .content {padding:5px;}
        .tabs {
            width: 600px;
            float: none;
            list-style: none;
            position: relative;
            text-align: left;
        }
        .tabs li {
            float: left;
            display: block;
        }
        .tabs input[type="radio"] {
            position: absolute;
            top: -9999px;
            left: -9999px;
        }
        .tabs label {
            display: block;
            padding: 5px 20px;
            border-radius: 15px 15px 0 0;
            font-size: 20px;
            font-weight: bold;
            background:#424242;
            color: #ffffff;
            cursor: pointer;
            position: relative;
            top: 10px;
            -webkit-transition: all 0.2s ease-in-out;
            -moz-transition: all 0.2s ease-in-out;
            -o-transition: all 0.2s ease-in-out;
            transition: all 0.2s ease-in-out;
        }
        .tabs label:hover {
            background:#cccccc;
            color:royalblue;
        }
        .tabs .tab-content {
            z-index: 2;
            display: none;
            overflow: hidden;
            width: 1000px;
            height: 500px;
            font-size: 15px;
            line-height:20px;
            padding: 5px;
            position: absolute;
            top: 45px;
            left: 0;
            background: #cccccc;
        }
        .tabs [id^="tab"]:checked + label {
            top: 0;
            padding-top: 15px;
            background:#cccccc;
            color:blue ;
        }
        .tabs [id^="tab"]:checked ~ [id^="tab-content"] {
            display: block;
        }

        /**Title选择样式**/
        .demo{width:400px;margin:5px;float:left;}
        /*.demo div{height:30px;width:400px;}*/
    </style>

    <script type="text/javascript">//Table导入到Excel
        var idTmr;
        function SaveTabletoExcel(tableid) {//整个表格拷贝到EXCEL中
            var curTbl = document.getElementById(tableid);
            var oXL = new ActiveXObject("Excel.Application");
            //创建AX对象excel
            var oWB = oXL.Workbooks.Add();
            //获取workbook对象
            var xlsheet = oWB.Worksheets(1);
            //激活当前sheet
            var sel = document.body.createTextRange();
            sel.moveToElementText(curTbl);
            //把表格中的内容移到TextRange中
            sel.select();
            //全选TextRange中内容
            sel.execCommand("Copy");
            //复制TextRange中内容
            xlsheet.Paste();
            //粘贴到活动的EXCEL中
            oXL.Visible = true;
            //设置excel可见属性

            try {
                var fname = oXL.Application.GetSaveAsFilename("将Table导出到Excel.xls", "Excel Spreadsheets (*.xls), *.xls");
            } catch (e) {
                print("Nested catch caught " + e);
            } finally {
                oWB.SaveAs(fname);

                oWB.Close(savechanges = false);
                //xls.visible = false;
                oXL.Quit();
                oXL = null;
                //结束excel进程，退出完成
                //window.setInterval("Cleanup();",1);
                idTmr = window.setInterval("Cleanup();", 1);

            }
        }
        function Cleanup() {
            window.clearInterval(idTmr);
            CollectGarbage();
        }

        function drawCircle(canvasId, dataArr, colorArr, textArr) {
            var c = document.getElementById(canvasId);
            var ctx = c.getContext("2d");

            var radius = c.height / 2 - 20; //半径
            var ox = radius + 20, oy = radius + 20; //圆心

            var width = 30, height = 10; //图例宽和高
            var posX = ox * 2 + 20, posY = 30;   //
            var textX = posX + width + 5, textY = posY + 10;

            var startAngle = 0; //起始弧度
            var endAngle = 0;   //结束弧度
            for (var i = 0; i < dataArr.length; i++) {
                //绘制饼图
                endAngle = endAngle + dataArr[i] * Math.PI * 2; //结束弧度
                ctx.fillStyle = colorArr[i];
                ctx.beginPath();
                ctx.moveTo(ox, oy); //移动到到圆心
                ctx.arc(ox, oy, radius, startAngle, endAngle, false);
                ctx.closePath();
                ctx.fill();
                startAngle = endAngle; //设置起始弧度

                //绘制比例图及文字
                ctx.fillStyle = colorArr[i];
                ctx.fillRect(posX, posY + 20 * i, width, height);
                ctx.moveTo(posX, posY + 20 * i);
                ctx.font = 'bold 12px 微软雅黑';    //斜体 30像素 微软雅黑字体
                ctx.fillStyle = colorArr[i]; //"#000000";
                var percent = textArr[i] + "：" + 100 * dataArr[i] + "%";
                ctx.fillText(percent, textX, textY + 20 * i);
            }
        }

    </script>

    <script type="text/javascript">//绘制饼图
        function drawCircle(canvasId, data_arr, color_arr, text_arr) {
            var c = document.getElementById(canvasId);
            var ctx = c.getContext("2d");

            var radius = c.height / 2 - 20; //半径
            var ox = radius + 20, oy = radius + 20; //圆心

            var width = 30, height = 10; //图例宽和高
            var posX = ox * 2 + 20, posY = 30;   //
            var textX = posX + width + 5, textY = posY + 10;

            var startAngle = 0; //起始弧度
            var endAngle = 0;   //结束弧度
            for (var i = 0; i < data_arr.length; i++) {
                //绘制饼图
                endAngle = endAngle + data_arr[i] * Math.PI * 2; //结束弧度
                ctx.fillStyle = color_arr[i];
                ctx.beginPath();
                ctx.moveTo(ox, oy); //移动到到圆心
                ctx.arc(ox, oy, radius, startAngle, endAngle, false);
                ctx.closePath();
                ctx.fill();
                startAngle = endAngle; //设置起始弧度

                //绘制比例图及文字
                ctx.fillStyle = color_arr[i];
                ctx.fillRect(posX, posY + 20 * i, width, height);
                ctx.moveTo(posX, posY + 20 * i);
                ctx.font = 'bold 12px 微软雅黑';    //斜体 30像素 微软雅黑字体
                ctx.fillStyle = color_arr[i]; //"#000000";
                var percent = text_arr[i] + "：" + 100 * data_arr[i] + "%";
                ctx.fillText(percent, textX, textY + 20 * i);
            }
        }

    </script>

    <script type="text/javascript">
        //兼容火狐、IE8
        //显示遮罩层
        function showMask() {
            $("#mask").css("height", $(document).height());
            $("#mask").css("width", $(document).width());
            $("#mask").show();
        }
        //隐藏遮罩层
        function hideMask() {

            $("#mask").hide();
        }

</script>
    <style type="text/css">
        .mask {
            position: absolute;
            top: 0px;
            filter: alpha(opacity=60);
            background-color: #777;
            z-index: 1002;
            left: 0px;
            opacity:0.5; -moz-opacity:0.5;

        }
</style>
    <link rel="stylesheet" href="./CSS/style.css" media="screen" type="text/css" />

</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
   <%-- 遮罩层--%>
    <div id="mask" class="mask" style="display: none;">
        <div style="text-align: center; vertical-align: middle;height: 100%;width: 100%">
            <img src="./images/loading.gif"/>
        </div>
    </div>

    <%-- 输入文档 --%>
    <div id="inputDiv" class="rm_mainContent" runat="server">
            <div class="demo">
	            <div style="margin-left:4px;">
                    <table>
                        <tr>
                            <td>Title：</td>
                            <td>
                                <input type="text" value="" class="yuanjiao" style="width: 600px; height: 25px" id="homecity_name"
                                    name="homecity_name" mod="address|notice" mod_address_source="hotel" mod_address_suggest=""
                                    mod_address_reference="cityid" mod_notice_tip="Type the title or click to choose it"
                                    runat="server" title="Type the title or click to choose it" />
                                <input id="cityid" name="cityid" type="hidden" value="{$cityid}" />
                            </td>
                            <td>    User:</td>
                            <td>
                                <input id="username" type="text" class="yuanjiao" style="width: 240px; height: 25px;" title="Type Your Name" placeholder="Type Your Name" runat="server"/>
                                <%--<asp:TextBox ID="usertb" runat="server" Width="250px" Height="25px" CssClass="yuanjiao" ToolTip=""></asp:TextBox>--%>
                            </td>
                        </tr>
                    </table>
		        </div>
            </div>
            <div id="jsContainer" class="jsContainer" style="height:0">
                <div id="tuna_alert" style="display:none;position:absolute;z-index:999;overflow:hidden;"></div>
                <div id="tuna_jmpinfo" style="visibility:hidden;position:absolute;z-index:120;"></div>
            </div>
            <script type="text/javascript" src="./JS/fixdiv.js"></script>
            <script type="text/javascript" src="./JS/address.js"></script>
            <asp:TextBox ID="titletb" runat="server" Width="400px" Height="25px" CssClass="yuanjiao" Visible="false"></asp:TextBox>
             <asp:RequiredFieldValidator ID="rftitle" runat="server" ErrorMessage="*(必填)" ControlToValidate="titletb" ForeColor="#FF3300" Visible="false"></asp:RequiredFieldValidator>
        <div >
            <table width="100%" style="margin-left:10px;">
                <tr>
                    <td style="width:40%;text-align:left">
                        <span style="font-weight: bold;font-size: 18px;">Texts:   &nbsp;&nbsp;</span>
                        <asp:FileUpload ID="txtUpload" runat="server" Width="200px" CssClass="yuanjiaobtn" Style="width: 200px; border: 0; height: 30px;line-height: 30px;" />
                        <asp:Button ID="txtImport" runat="server" Text="Import" OnClick="txtImport_Click"
                            Width="68px" CssClass="yuanjiaobtn" />

                    </td>
                    <td style="width:20%;text-align:right">Choose Vocabulary：

                    </td>
                    <td style="width:40%;text-align:left">
                        <asp:CheckBoxList ID="cblist" runat="server" ToolTip="Choose Vocabulary" RepeatDirection="Horizontal" Width="400px" OnSelectedIndexChanged="cblist_SelectedIndexChanged" AutoPostBack="true" >
                            <asp:ListItem Value="0">高中大纲</asp:ListItem>
                            <asp:ListItem Value="1">基本要求</asp:ListItem>
                            <asp:ListItem Value="2">较高要求</asp:ListItem>
                            <asp:ListItem Value="3">更高要求</asp:ListItem>
                        </asp:CheckBoxList>

                    </td>
                </tr>
                <%--<tr style="text-align:right;"><asp:CheckBox ID="ckEurope" Text="是否排查欧框词汇" runat="server" /></tr>--%>
            </table>

            <%--<input type="file" id="btnFile" onpropertychange="txtFoo.value=this.value;" style="visibility:hidden;" />
            <input type="text" id="txtFoo" style="visibility:hidden" />
            <input type="button" onclick="btnFile.click()" value="选择文件" class="yuanjiaobtn"/>--%>

               <%-- <input type="button" name="button" title="粘贴到文本框" value="来自粘贴板" onclick="javascript: mypast();" class="yuanjiaobtn" />--%>

            <%--<asp:TextBox ID="txtsBox" runat="server" Height="280px" TextMode="MultiLine" Width="960px" CssClass="yuanjiao" ToolTip="Please Type or paste your text here and click the submit button below!"
                ></asp:TextBox>   --%>
            <textarea id="txtcontent" onkeyup="wordStatic(this);" onchange="wordStatic(this);"
                runat="server" rows="100" cols="80" class="ta" placeholder="Please Type or paste your text here and click the submit button below!"></textarea>
        </div>
        <table width="100%">
            <tr>
                <td style="text-align: left">
                    <div>(<span style="color: blue">Limit: 100,000 Words</span>)<span id="mywords" style="display: none;">&nbsp;&nbsp;<span
                        id="wcount" style="color: red">Entered：0 Words;</span>  <span id="lcount" style="color: green">
                            Remaining: 30,000 Words</span></span></div>
                    <script type="text/javascript">
                        function wordStatic(input) {
                            // 获取文本框对象
                            var el = document.getElementById('wcount');
                            var ll = document.getElementById('lcount');
                            var mw = document.getElementById('mywords');
                            if (el && input) {
                                // 获取输入内容长度并更新到界面
                                var value = input.value;
                                // 替换中文字符为空格
                                value = value.replace(/[\u4e00-\u9fa5]+/g, " ");
                                // 将换行符，前后空格不计算为单词数
                                value = value.replace(/\n|\r|^\s+|\s+$/gi, "");
                                // 多个空格替换成一个空格
                                value = value.replace(/\s+/gi, " ");
                                // 更新计数
                                var length = 0;
                                var match = value.match(/\s/g);
                                if (match) {
                                    length = match.length + 1;
                                } else if (value) {
                                    length = 1;
                                }
                                mw.style.display = "";
                                el.innerText = "Entered：" + length + " Words;";
                                if (length <= 100000) {
                                    ll.innerHTML = "Remaining: " + (100000 - length) + " Words;";
                                } else {
                                    ll.innerHTML = "exceeded: " + (length-100000) + " Words;";
                                }

                            }
                        }
                    </script>
                </td>
                <td style="text-align: right;margin-right: 40px;padding-right: 20px;">
                    <asp:Button ID="clearBtn" runat="server" Text="Clear" OnClick="clearBtn_OnClick" CssClass="yuanjiaobtn" ToolTip="Clear the Texts"/>
                    <asp:Button ID="lemmanew" runat="server" Text="Submit" OnClick="lemmanew_Click" CssClass="yuanjiaobtn" ToolTip="Submit to Process"/>
                </td>
            </tr>
        </table>

    </div>

    <%-- 输出结果 --%>
    <div id="outputDiv" runat="server"  visible ="false" style="padding-left:5px;width:1000px;">
        <div style="float:right;margin-right:20px;">
            <asp:Button ID="backBtn" runat="server" Text="返回" OnClick="backBtn_OnClickBtn_OnClick" CssClass="yuanjiaobtn" ToolTip="返回上次操作"/>
            <asp:Button ID="closeBtn" runat="server" Text="关闭" OnClick="closeBtn_OnClick" CssClass="yuanjiaobtn" ToolTip="关闭页面"/></div>
        <div class="content">
        <ul class="tabs">

            <li>
                <%--词汇列表--%>
                <input type="radio" name="tabs" id="tab1" checked />
                <label for="tab1">WordList</label>
                <div id="tab-content1" class="tab-content">
                    <div style="overflow-y: auto;overflow-x:hidden; height: 450px;width: 980px; padding: 5px 5px 5px 10px;">
                    <asp:GridView ID="wordgv" runat="server" OnRowCreated="wordgv_OnRowCreated" OnRowDataBound="wordgv_OnRowDataBound"
                        Width="100%" AllowSorting="True" OnSorting="wordgv_OnSorting">
                        <AlternatingRowStyle BackColor="White" />
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
                        <Columns>
                        <asp:TemplateField HeaderText="NO." InsertVisible="False">
                            <itemstyle horizontalalign="Center" />
                            <headerstyle horizontalalign="Center" width="5%" />
                            <itemtemplate>

                        </itemtemplate>
                        </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    </div>
                    <div style="float:right;margin-right:20px;padding: 10px 5px 5px">
                        <asp:Label ID="totalW" runat="server" Text="Total: 0 Words" Visible="False"></asp:Label>
                        <asp:Button ID="exExcel" runat="server" Text="导出EXCEL" CssClass="yuanjiaobtn" OnClick="exExcel_OnClick"/>
                    </div>
                </div>
            </li>
            <li><%--彩色文本--%>
                <input type="radio" name="tabs" id="tab2" />
                <label for="tab2">Profiled Text</label>
                <div id="tab-content2" class="tab-content">
                    <%--<span style="color: blue;font-size:large;padding-left: 10px;">Title: </span> --%>
                    <asp:Label ID="outlb" runat="server" Text="" ForeColor="#000" Font-Size="Large" Font-Italic="True"></asp:Label>
                    <div style="float: right; padding: 5px 5px 5px 5px; background-color: #000000;margin-right: 18px;"
                        id="tuliDiv" runat="server"></div>
                    <div id="outDiv" runat="server" style="width: 990px; padding: 5px 5px 5px 5px; background-color: #000000;
                        height: 460px; overflow-y: auto; white-space: normal; word-wrap: break-word;
                        word-break: break-all;"></div>
                </div>
            </li>

            <li><%--数据统计饼状图--%>
                <input type="radio" name="tabs" id="tab3" />
                <label for="tab3">Word Profile</label>
                <div id="tab-content3" class="tab-content" style="text-align:center;vertical-align:middle">
                    <asp:Chart ID="Chart1" runat="server" Height="500px" Width="800px">
                        <Series>
                            <asp:Series Name="Series1"></asp:Series>
                        </Series>
                        <ChartAreas>
                            <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                        </ChartAreas>
                    </asp:Chart>
                    <table style="display: none">
                        <tr>
                            <td>
                                <canvas id="canvas_lv" width="400" height="280" style="border: 1px solid #0026ff;">
                        浏览器不支持绘制饼图
                    </canvas>
                            </td>
                            <td>
                                <canvas id="canvas_seq" width="400" height="280" style="border: 1px solid #0026ff;">
                        浏览器不支持绘制饼图
                    </canvas>
                            </td>
                        </tr>
                    </table>
                </div>
            </li>
        </ul>
        </div>
    </div>
</asp:Content>
 <%-- PlaceHolderPageTitle显示在浏览器标题处,PageTitleInTitleArea显示在网页标题栏 --%>
<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    Lemmatization OnLine
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
   <asp:Label runat="server" ID="Titlelb">Input</asp:Label>
</asp:Content>
