<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PManage.aspx.cs" Inherits="Project.Layouts.Project.PManage" DynamicMasterPageFile="~masterurl/default.master" %>

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
            width:168px;
            height: 25px;
            line-height: 25px;
            vertical-align: middle;
            padding: 3px;
            border: 1px #bebee1 solid;
            color: #494b4c;
        }
    </style>
  <%--  <script type="text/javascript">
        function checkEmpty() {
            var sptxtIntroduce = RTE_GetRichEditTextOnly("<%=txtIntroduce.ClientID%>");
            if (sptxtIntroduce == "") {
                alert("项目简介不能为空!");
                RTE_GiveEditorFocus(<%=txtIntroduce.ClientID%>);
                return false;
            } else {
                return true;
            }
        }
    </script>--%>
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    
    <div style="padding-left: 20px; margin-left: 10px;">
        <asp:Label ID="error" CssClass="redStar" runat="server" Text=""></asp:Label>
    </div>
    <div>

        <p style="line-height: 100%; padding-top: 10px;">
            <label class="WorksVerification"><span class="redStar"> * </span>项目名称：</label>
            <asp:Label ID="lbPName" runat="server" Text="" Font-Size="16px"></asp:Label>
            <asp:TextBox ID="txtPName" runat="server" Width="500px" Height="25px" BorderColor="#CCCCCC"
                BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rqtxtName" CssClass="redStar" runat="server"
                ErrorMessage="必填" ControlToValidate="txtPName"></asp:RequiredFieldValidator>
        </p>

        <p style="line-height: 100%; padding-top: 10px;">
            <label class="WorksVerification"><span class="redStar"> * </span>所属学科：</label>
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
                OnClick="btnSubjectChange_OnClick" />
            <asp:Button ID="btnSubjectCancelChange" runat="server" Text="取消修改" Visible="False" CausesValidation="False"
                OnClick="btnSubjectCancel_OnClick" />
            <asp:HiddenField ID="hdfSubject" runat="server" Value="0" />
        </p>

        <p style="padding-top: 10px;">
            <label class="WorksVerification" style="height: 30px;"><span class="redStar">&nbsp; &nbsp;</span>项目简介：</label>
            <textarea id="txtIntro" cols="30" rows="5" style="width: 500px" runat="server" title="项目立项说明(500字以内),简要概述项目的立项可行性与预期目标!"></textarea>
            <SharePoint:InputFormTextBox ID="txtIntroduce" runat="server" RichText="true" Rows="8"
                Columns="1" Width="500px" TextMode="MultiLine" RichTextMode="Compatible" IsValid="True"
                CausesValidation="False" AllowHyperlink="True" Direction="NotSet" Visible="False"></SharePoint:InputFormTextBox>
        </p>

        <p style="line-height: 100%; padding-top: 10px;">
            <label class="WorksVerification">
                <span class="redStar"> * </span>立项报告：
            </label>
            <asp:FileUpload ID="lixiangFile" runat="server" Width="400px" />
            <span style="color: red" id="fileInfo" runat="server"></span>
            <asp:GridView ID="gvmyFile" runat="server" AutoGenerateColumns="False" CellPadding="5" ForeColor="#333333" GridLines="None">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField DataField="FileID" HeaderText="文件ID" Visible="False" ShowHeader="False" />
                    <asp:BoundField DataField="FileNameInDocLib" HeaderText="文档库文件名" Visible="False" />
                    <asp:BoundField DataField="FileName" HeaderText="文件名称" ShowHeader="False" />
                    <asp:BoundField DataField="FileSize" HeaderText="文件大小" ShowHeader="False" />
                    <asp:CommandField DeleteText="删除重传" ShowDeleteButton="True" ShowHeader="False" />
                </Columns>
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
            </asp:GridView>
            <asp:Button ID="btnNewFile" runat="server" Text="上传新版本" CausesValidation="False" OnClick="btnNewFile_OnClick"/>
            <asp:HiddenField ID="hdfFile" runat="server" Value="0" />
            <asp:Button ID="btnFileCancel" runat="server" Text="取消上传新版本" OnClick="btnFileCancel_OnClick" Visible="False" CausesValidation="False"/>
        </p>
        <p style="color: red; padding-left: 100px; padding-top: 5px;">
            <asp:Label ID="lbDocType" runat="server" Text="支持的文档格式：.docx, .doc, .pdf, .ppt, .pptx, .xls, .xlsx"></asp:Label>
        </p>
        
        <p style="line-height: 100%; padding-top: 10px; padding-left: 200px;">
            <asp:HiddenField ID="hfID" runat="server" />
            <asp:Button ID="btnSubmit" runat="server" Text="发   布" Style="width: 133px; height: 35px;
                background: url(images/ButtonBg.gif); border: 0; color: #fff; font-weight: bolder;
                font-size: 15px; margin: 0; cursor: pointer"
                OnClick="NewProject_OnClick" />
            <asp:Button ID="btnSave" runat="server" Text="保   存" Style="width: 133px; height: 35px;
                background: url(images/ButtonBg.gif); border: 0; color: #fff; font-weight: bolder;
                font-size: 15px; margin: 0; cursor: pointer"
                OnClick="SaveProject_OnClick" />
        </p>
    </div>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
项目管理
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
    <asp:Label ID="lbPageTitle" runat="server" Text="发布新项目"></asp:Label>
</asp:Content>
