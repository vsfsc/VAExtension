<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="vblist.aspx.cs" Inherits="lemmatizer.Layouts.lemmatizer.vblist" DynamicMasterPageFile="~masterurl/default.master" %>


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
            font-size:15px;
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
        .FileUpload{
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

    <link rel="stylesheet" href="CSS/style.css" media="screen" type="text/css" />
    <!-- #endregion -->
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
	            <div style="margin-left:4px;padding-top:5px;">
                    <div id="selectbut">
                        册:
                        <asp:DropDownList ID="ddlBook" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlBook_SelectedIndexChanged" CssClass="yuanjiao" >
                            <asp:ListItem Value="0">请选择册</asp:ListItem>
                            <asp:ListItem Value="1">Book 1</asp:ListItem>
                            <asp:ListItem Value="2">Book 2</asp:ListItem>
                            <asp:ListItem Value="3">Book 3</asp:ListItem>
                            <asp:ListItem Value="4">Book 4</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;&nbsp;单元:
                        <asp:DropDownList ID="ddlUnit" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlUnit_SelectedIndexChanged" CssClass="yuanjiao" >
                            <asp:ListItem Value="0">请选择单元</asp:ListItem>
                            <asp:ListItem Value="1">Unit 1</asp:ListItem>
                            <asp:ListItem Value="2">Unit 2</asp:ListItem>
                            <asp:ListItem Value="3">Unit 3</asp:ListItem>
                            <asp:ListItem Value="4">Unit 4</asp:ListItem>
                            <asp:ListItem Value="5">Unit 5</asp:ListItem>
                            <asp:ListItem Value="6">Unit 6</asp:ListItem>
                            <asp:ListItem Value="7">Unit 7</asp:ListItem>
                            <asp:ListItem Value="8">Unit 8</asp:ListItem>
                            <asp:ListItem Value="9">Unit 9</asp:ListItem>
                            <asp:ListItem Value="10">Unit 10</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;&nbsp;篇目:
                        <asp:DropDownList ID="ddlText" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlText_SelectedIndexChanged" CssClass="yuanjiao" >
                            <asp:ListItem Value="0">请选择篇目</asp:ListItem>
                            <asp:ListItem Value="1">Lesson 1 Listening</asp:ListItem>
                            <asp:ListItem Value="2">Text A</asp:ListItem>
                            <asp:ListItem Value="3">Text B</asp:ListItem>
                            <asp:ListItem Value="4">Lesson 2 Listening</asp:ListItem>
                            <asp:ListItem Value="5">Text C</asp:ListItem>
                        </asp:DropDownList>
                        导入正文：
                        <asp:FileUpload ID="txtUpload" runat="server" Width="200px" CssClass="yuanjiao" />
                        <asp:Button ID="txtImport" runat="server" Text="导 入" OnClick="txtImport_Click"
                            Width="68px" CssClass="yuanjiaobtn" />
                    </div>
                    <div>
                        <span style="font-weight: bold;font-size: 18px;">正文:   &nbsp;&nbsp;</span>
                        <asp:Label ID="lbTextTitle" runat="server" Text="" Font-Bold="true" Font-Size="Medium" ForeColor="#003399"></asp:Label>
                    </div>
                    <div>
                         <textarea id="txtcontent" onkeyup="wordStatic(this);" onchange="wordStatic(this);"
                runat="server" rows="100" cols="80" class="ta" placeholder="请输入或者导入课文正文!" style="line-height:25px;font-size:12px;"></textarea>
                         <div>
                             (<span style="color: blue">建议单次处理文本最多词数: 10,000 </span>)
                             <span id="mywords" style="display: none;">&nbsp;&nbsp;
                                 <span id="wcount" style="color: red">已输入：0 ;</span>
                                 <span id="lcount" style="color: green">还可输入: 10,000 Words</span>
                             </span>
                         </div>
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
                                el.innerText = "已输入：" + length + " 词;";
                                if (length <= 10000) {
                                    ll.innerHTML = "还可输入: " + (10000 - length) + " 词;";
                                } else {
                                    ll.innerHTML = "超出: " + (length-10000) + " 词;";
                                }

                            }
                        }
                    </script>
                    </div>
                    <div style="text-align:right;">
                        <asp:Button ID="clearBtn" runat="server" Text="清 空" ForeColor="#ff3300" OnClick="clearBtn_OnClick" CssClass="yuanjiaobtn" ToolTip="Clear the Texts"/>
                        <asp:Button ID="lemmanew" runat="server" Text="提 交" ForeColor="#003300" OnClick="lemmanew_Click" CssClass="yuanjiaobtn" ToolTip="Submit to Process"/>
                    </div>

		        </div>
            </div>
    </div>
    <div id="outputDiv" runat="server"  visible ="false" style="padding-left:5px;width:1000px;">
        <div style="float:right;margin-right:20px;">
            <asp:Label ID="totalW" runat="server" Text="Total: 0 Words"></asp:Label>
            <asp:Button ID="exExcel" runat="server" Text="导出EXCEL" CssClass="yuanjiaobtn" OnClick="exExcel_OnClick"/>
            <asp:Button ID="backBtn" runat="server" Text="返回" OnClick="backBtn_OnClick" CssClass="yuanjiaobtn" ToolTip="返回上次操作"/>
            <asp:Button ID="closeBtn" runat="server" Text="关闭" OnClick="closeBtn_OnClick" CssClass="yuanjiaobtn" ToolTip="关闭页面"/>
        </div>
        <div class="content">
             <%--词汇列表--%>
            <div style="overflow-y: auto;overflow-x:hidden; height: 450px;width: 980px; padding: 5px 5px 5px 10px;">
                <asp:GridView ID="wordgv" runat="server" Width="100%" AutoGenerateColumns="true" >
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
                    <EmptyDataTemplate>
                        你所筛查的文本不包含生词，如有疑问，请确认所选择“Book”、“Unit”、“Text”及录入的课文文本是否准确，然后再试！
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
课文生词排查
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
<asp:Label runat="server" ID="Titlelb">生词排查</asp:Label>
</asp:Content>
