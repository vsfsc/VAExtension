<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberShow.aspx.cs" Inherits="Project.Layouts.Project.MemberShow" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>VA项目平台达人秀</title>
    <link rel="stylesheet" type="text/css" href="css/detail.css" />
    <link rel="stylesheet" type="text/css" href="css/common.css" />
    <link rel="stylesheet" type="text/css" href="css/tianchi.css" />
</head>
<body>
    <form id="form1" runat="server">

        <div data-spm="222">
            <div class="science-box">

                <div class="science-bannner">
                </div>

                <div class="science-info-box">
                    <div class="science-row">
                        <div class="science-info clearfix">
                            <div class="portrait-box">
                                <asp:Image ID="imageUser" runat="server" Width="150" Height="150" CssClass="science-portrait" />
                                <%--<img width="150" height="150" class="science-portrait" src="/science/scientistPic.do?userId=101376415" />--%>
                            </div>
                            <div class="potrait-box-right">

                                <div class="science-honor clearfix">
                                    <ul>
                                    </ul>
                                </div>
                                <div class="science-honor-bottom">
                                    <dl>
                                        <dt class="clearfix">
                                            <span class="fl nick-name" id="userName" runat="server"></span>
                                            <div class="personal-rank">
                                                <p class="rating-txt rating-txt-3" runat="server" id="pSumScore"></p>
                                                <i class="rating-icon rating-3"></i>
                                            </div>
                                        </dt>
                                        <dd class="science-position"><span id="ddSchoolName" runat="server"></span><span
                                            class="science-ranking" id="ddRank" runat="server"></span></dd>
                                    </dl>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>



                <div class="science-competition-list-box">
                    <div class="science-row">
                        <div class="tabs kg-tabs" data-event="click" data-effect="fade">
                            <ul class="tabs-nav clearfix">
                                <li class="selected"><span>项目</span></li>
                            </ul>
                            <div class="science-contents tabs-content">
                                <div class="tabs-pannel">
                                    <div class="science-competition-list" id="divProjectList" runat="server">
                                    </div>
                                </div>
                                <div class="tabs-pannel hidden">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>





            </div>
        </div>

    </form>
</body>
</html>
