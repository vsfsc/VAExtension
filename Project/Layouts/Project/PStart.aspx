<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PStart.aspx.cs" Inherits="Project.Layouts.Project.PStart" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
      <link rel="stylesheet" type="text/css" href="./css/base.css" />
    <link rel="stylesheet" type="text/css" href="./css/page.css" />
    <style type="text/css">
        .cssddlType {
            width: 252px;
            height: 25px;
            line-height: 25px;
            vertical-align: middle;
            padding: 3px;
            border: 1px #bebee1 solid;
            color: #494b4c;
        }
        .cssddlSubject {
            width:165px;
            height: 25px;
            line-height: 25px;
            vertical-align: middle;
            padding: 3px;
            border: 1px #bebee1 solid;
            color: #494b4c;
        }

    </style>
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
      <div style="padding-left: 20px; margin-left: 10px;">
        <asp:Label ID="error" CssClass="redStar" runat="server" Text=""></asp:Label>
    </div>
    <div>
        <table>
            <tr>
                <td style="vertical-align: top;text-align:right;font-size:14px;">
                   <span class="redStar"> * </span>项目名称：
                </td>
                <td style="vertical-align: top;text-align:left;font-size:14px;">
                    <asp:Label ID="lbPName" runat="server" Text="" Font-Size="16px"></asp:Label> 
                    <asp:TextBox ID="txtPName" runat="server" Width="500px" Height="25px" BorderColor="#CCCCCC"
                BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rqtxtName" CssClass="redStar" runat="server"
                ErrorMessage="必填" ControlToValidate="txtPName"></asp:RequiredFieldValidator>
                </td>
            </tr>
                 <tr>
                <td style="vertical-align: top;text-align:right;font-size:14px;">
                    <span class="redStar"> * </span>所属学科：
                </td>
                <td style="vertical-align: top;text-align:left;font-size:14px;">
                    <asp:Label ID="lbSubject" runat="server" Text="" Font-Size="16px"></asp:Label>
            <asp:DropDownList ID="ddlSubjectA" runat="server" CssClass="cssddlSubject" AutoPostBack="True"
                OnSelectedIndexChanged="ddlSubjectA_OnSelectedIndexChanged">
            </asp:DropDownList>
            <asp:DropDownList ID="ddlSubjectB" runat="server" CssClass="cssddlSubject" AutoPostBack="True"
                Visible="False" OnSelectedIndexChanged="ddlSubjectB_OnSelectedIndexChanged">
            </asp:DropDownList>
            <asp:DropDownList ID="ddlSubjectC" runat="server" CssClass="cssddlSubject" Visible="False">
            </asp:DropDownList>
            <asp:Button ID="btnSubjectChange" runat="server" Text="修改学科" Visible="False" CausesValidation="False"
                OnClick="btnSubjectChange_OnClick"  CssClass="gray"/>
            <asp:Button ID="btnSubjectCancelChange" runat="server" Text="取消修改" Visible="False" CausesValidation="False"
                OnClick="btnSubjectCancel_OnClick"   CssClass="white" />
            <asp:HiddenField ID="hdfSubject" runat="server" Value="0" />
                </td>
            </tr>
             <tr>
                <td style="vertical-align: top;text-align:right;font-size:14px;">
                     <span class="redStar">&nbsp; &nbsp;</span>项目简介：
                </td>
                <td style="vertical-align: top;text-align:left;font-size:14px;">
                     <textarea id="txtIntro" cols="30" rows="12" style="width: 500px" runat="server" title="项目立项说明(500字以内),简要概述项目的立项可行性与预期目标!"></textarea>
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top;text-align:right;font-size:14px;">
                    <span class="redStar"> * </span>立项报告：
                </td>
                <td style="vertical-align: top;text-align:left;font-size:14px;">
                     <asp:FileUpload ID="lixiangFile" runat="server" Width="420px" />
            <asp:Button ID="btnNewFile" runat="server" Text="上传" CausesValidation="False" OnClick="btnNewFile_OnClick"/>
                    <p style="color: red; padding-top: 5px;">
                        <asp:Label ID="lbDocType" runat="server" Text="支持的文档格式：.docx, .doc, .pdf, .ppt, .pptx, .xls, .xlsx；大小不超过50M"></asp:Label>
                    </p>
                </td>
            </tr>
            <tr>
                <td></td>
                <td style="vertical-align: top;text-align:left;font-size:14px;">
                    <div id="fileDiv" runat="server" style="font-size: 12px;border: 1px solid #808080;background-color:  #ffd700;color: #494b4c;width: 500px;" >你还未上传立项报告,请及时上传!</div>
                    <asp:HiddenField ID="hdfFile" runat="server" Value="0" />
                </td>
            </tr>
            <tr style="margin: 10px">
                <td colspan="2" align="center">
                     <asp:HiddenField ID="hfID" runat="server" />
                <asp:Button ID="btnSubmit" runat="server" Text="发   布" Width="60px" Height="30px"  CssClass="blue" style="color:white;"  OnClick="NewProject_OnClick" />
                <asp:Button ID="btnSave" runat="server" Text="保   存" Width="60px" Height="30px" style="color:white;"  CssClass="green" Visible="False"  OnClick="SaveProject_OnClick" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
项目发布
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
 <asp:Label ID="lbPageTitle" runat="server" Text="发布新项目"></asp:Label>
</asp:Content>
