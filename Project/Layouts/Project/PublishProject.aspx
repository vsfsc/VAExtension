<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PublishProject.aspx.cs" Inherits="Project.Layouts.Project.PublishProject" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="renderer" content="webkit"/>
    <meta name="viewport" content="width=device-width,initial-scale=1,maximum-scale=1"/>
    <link rel="stylesheet" type="text/css" href="Pub/css/onlinebusinesshall/newexpand/css/index-debug.css"/>
    <link rel="stylesheet" type="text/css" href="Pub/css/index.css"/>
    <link rel="stylesheet" type="text/css" href="Pub/static/lib/jquery-step/css/jquery.step.css"/>
    <script src="Pub/static/lib/jquery/1.8/jquery.min.js" type="text/javascript"></script>
    <script src="Pub/static/lib/jquery-step/js/jquery.step.js" type="text/javascript"></script>
    <script type="text/javascript">
	    $(function() {
		    var step= $("#myStep").step({
			    animate:true,
			    initStep:1,
			    speed:1000
		    });
		    $("#preBtn").click(function(event) {
			    var yes=step.preStep();
		    });
		    $("#applyBtn").click(function(event) {
			    var yes=step.nextStep();
		    });
	        $("#<%=btnSave.ClientID%>").click(function (event) {
	            var yes = step.nextStep();
	        });
		    $("#submitBtn").click(function(event) {
			    var yes=step.nextStep();
		    });
		    $("#goBtn").click(function(event) {
			    var yes=step.goStep(3);
		    });
	    });
    </script>
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div>
        <p>
            <asp:GridView ID="gvmyFiles" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle" OnRowDataBound="gvmyFiles_OnRowDataBound">
                <FooterStyle CssClass="GridViewFooterStyle" />
                <RowStyle CssClass="GridViewRowStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" HorizontalAlign="Center" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <HeaderStyle CssClass="GridViewHeaderStyle" HorizontalAlign="Center" />


                <EmptyDataTemplate>
                    <span style="font-family: 微软雅黑; font-size: 14px; color: red;">无文档
                    </span>
                </EmptyDataTemplate>

                <Columns>
                    <asp:TemplateField HeaderText=" ">
                        <ItemTemplate>
                            <asp:Image ID="iconImg" runat="server" Height="25px" Width="25px" />
                        </ItemTemplate>
                    </asp:TemplateField>
                   <asp:HyperLinkField HeaderText="文件名称" DataNavigateUrlFields="fileUrl" DataNavigateUrlFormatString="{0}"
                       DataTextField="Name" />
                    <asp:BoundField DataField="fileExName" HeaderText="fileExName" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="Name" HeaderText="Name" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="fileUrl" HeaderText="fileUrl" ItemStyle-HorizontalAlign="Center" Visible="False"/>
                    <asp:BoundField DataField="fileSize" HeaderText="fileSize" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="fileAuthor" HeaderText="fileAuthor" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="TimeCreated" HeaderText="TimeCreated" ItemStyle-HorizontalAlign="Center" />
                    <asp:TemplateField HeaderText=" 在线预览">
                        <ItemTemplate>
                            <asp:HyperLink ID="viewDoc" runat="server"><%#Eval("Name")%></asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="IconUrl" HeaderText="IconUrl" ItemStyle-HorizontalAlign="Center" Visible="False"/>
                </Columns>
            </asp:GridView>
        </p>
    </div>
    <hr />

    <div class="step-body" id="myStep">

        <div class="step-header" style="width: 80%">
            <ul>
                <li>
                    <p>填写项目信息</p>
                </li>
                <li>
                    <p>上传项目文档</p>
                </li>
                <li>
                    <p>完成发布</p>
                </li>
            </ul>
        </div>
        <div class="step-content">
            <div class="step-list">
                <div class="page-panel-title">
                    <h3 class="page-panel-title-left">填写项目信息</h3>
                    <h3 class="page-panel-title-right">注：橘黄色部分为必填项!</h3>
                </div>
                <div class="intro-flow">
                    <div class="intro-list intro-list-active">
                        <div class="intro-list-left">
                            项目名称:
                        </div>
                        <div class="intro-list-right">
                            <%--<span>1</span>--%>
                            <div class="intro-list-content">
                                <input id="txtName" type="text" style="width: 100%" runat="server"/>
                            </div>
                        </div>
                    </div>


                    <div class="intro-list intro-list-active">
                        <div class="intro-list-left">
                            所属学科:
                        </div>
                        <div class="intro-list-right">
                            <%--<span>2</span>--%>
                            <div class="intro-list-content" >
                                <asp:DropDownList ID="ddlSubjectA" runat="server" CssClass="cssddlSubject" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlSubjectA_OnSelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlSubjectB" runat="server" CssClass="cssddlSubject" AutoPostBack="True"
                                    Visible="False" OnSelectedIndexChanged="ddlSubjectB_OnSelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlSubjectC" runat="server" CssClass="cssddlSubject" Visible="False">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <div class="intro-list">
                        <div class="intro-list-left">
                            项目简介:
                        </div>
                        <div class="intro-list-right">
                            <%--<span>3</span>--%>
                            <div class="intro-list-content">
                                <textarea id="txtIntroduce" cols="100" rows="5" style="width: 100%" runat="server"></textarea>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="footer-btn">
                    <div class="common-btn">
                        <a href="javascript:void(0);" id="applyBtn">
                            <asp:Button ID="btnSave" runat="server" Text="保存" Style="cursor: pointer; border: none;background:none;"
                                OnClientClick="javascript:return IsValidText();return false;" />
                            <asp:Button ID="btnSubmit" runat="server" Text="保存,下一步" Style="cursor: pointer; border: none;
                                background: rgba(0,0,0,0.3);" OnClientClick="return false;" />
                        </a>
                        
                    </div>
                </div>
            </div>
            <div class="step-list">
                <div class="page-panel-title">
                    <h3 class="page-panel-title-left">上传项目文档</h3>
                    <h3 class="page-panel-title-right">注：操作不可逆,请准确填写或完成每一步骤!</h3>
                </div>
                <div class="intro-flow">
                        <div class="intro-list">
                            <div class="intro-list-left">
                                立项报告:
                            </div>
                            <div class="intro-list-right">
                                <%--<span>1</span>--%>
                                <div class="intro-list-content">
                                    <asp:FileUpload ID="fuWorks" runat="server" Width="400px"/>
                                    <asp:Button ID="btnUpFile" runat="server" Text="上传" CausesValidation="False" OnClick="btnUpFile_OnClick" OnClientClick="javascript:return false;" />
                                </div>
                            </div>

                            
                        </div>
                    <div class="intro-list">
                        <div class="intro-list-left">
                            中期检查:
                        </div>
                        <div class="intro-list-right">
                            <%--<span>1</span>--%>
                            <div class="intro-list-content">
                                <asp:FileUpload ID="FileUpload1" runat="server" Width="400px" />
                                <asp:Button ID="Button2" runat="server" Text="上传" CausesValidation="False" OnClick="btnUpFile_OnClick"
                                    OnClientClick="javascript:return false;" />
                            </div>
                        </div>
                    </div>
                    <div class="intro-list">
                        <div class="intro-list-left">
                            结题报告:
                        </div>
                        <div class="intro-list-right">
                            <%--<span>1</span>--%>
                            <div class="intro-list-content">
                                <asp:FileUpload ID="FileUpload2" runat="server" Width="400px" />
                                <asp:Button ID="Button3" runat="server" Text="上传" CausesValidation="False" OnClick="btnUpFile_OnClick"
                                    OnClientClick="javascript:return false;" />
                            </div>
                        </div>
                    </div>
                    <asp:GridView ID="gvFiles" runat="server"></asp:GridView>
                  
                </div>
                <div class="footer-btn">
                    <div class="common-btn">
                        <a href="javascript:void(0);" id="submitBtn">提交</a>
                        <asp:HiddenField ID="prID" runat="server" />
                        
                       
                    </div>
                </div>

            </div>
            <div class="step-list">
                <div class="apply-finish">
                    <div class="apply-finish-header">
                        <img src="Pub/images/success.png" alt="">
                        <div class="apply-finish-msg">恭喜您，发布成功 ! </div>
                    </div>
                    <div class="apply-finish-footer">
                        <p>尊敬的用户，您的项目已发布成功，项目编号为<span id="proNo">P16000001</span>。请耐心等待项目审批.</p>
                        <p><a href="">查看项目信息</a></p>
                    </div>
                </div>
            </div>

        </div>

    </div>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
发布项目
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
发布项目
</asp:Content>
