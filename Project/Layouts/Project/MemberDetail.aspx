<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberDetail.aspx.cs" Inherits="Project.Layouts.Project.MemberDetail" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <link rel="stylesheet" type="text/css" href="css/detail.css"/>
    <link rel="stylesheet" type="text/css" href="css/common.css"/>
    <link rel="stylesheet" type="text/css" href="css/tianchi.css"/>
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">

    <div data-spm="222">
        <div class="science-box">

	        <div class="science-bannner">
                </div>
            
            <div class="science-info-box">
		        <div class="science-row">
			        <div class="science-info clearfix">
				        <div class="portrait-box">
                            <asp:Image ID="imageUser" runat="server" Width="150" Height="150" CssClass="science-portrait"/>
                            <%--<img width="150" height="150" class="science-portrait" src="/science/scientistPic.do?userId=101376415" />--%>
				        </div>
				        <div class="potrait-box-right">
					
					        <div class="science-honor clearfix">
						        <ul>
                                    <li class="honor-first">1</li>
                                    <li class="honor-second">3</li>
                                    <li class="honor-third">0</li>
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
                                    <dd class="science-position"><span  id="ddSchoolName" runat="server"></span><span class="science-ranking" id="ddRank" runat="server"></span></dd>
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
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    VA众创项目达人秀
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
    VA众创项目达人秀
</asp:Content>
