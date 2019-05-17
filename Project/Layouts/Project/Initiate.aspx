<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Initiate.aspx.cs" Inherits="Project.Layouts.Project.Initiate" DynamicMasterPageFile="~masterurl/default.master" %>

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
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    
    <div id="myProjectsDiv" runat="server" style="margin-left: 30px;">
        <span style="font-family: 微软雅黑; font-size: 16px; font-weight: bold; color: black;padding: 0 0 0 20px;">
            我发起的项目列表
        </span>
        <asp:GridView ID="gvProject" runat="server" AllowPaging="true" PageSize="5" AutoGenerateColumns="False"
            CellPadding="0" GridLines="None" Height="30px" Width="700px" PagerSettings-Mode="NumericFirstLast"
            PagerSettings-FirstPageText="首页" PagerSettings-NextPageText="下一页" PagerSettings-PreviousPageText="上一页"
            PagerSettings-LastPageText="尾页" >
            <FooterStyle CssClass="GridViewFooterStyle" />
            <RowStyle CssClass="GridViewRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" HorizontalAlign="Center" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" HorizontalAlign="Center" />
            <EmptyDataTemplate>
                <span style="font-family: 微软雅黑; font-size: 14px; color: red;">
                    当前你还未发布任何项目
                </span>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="ProjectID" HeaderText="项目ID" ItemStyle-HorizontalAlign="Center"
                    Visible="False"></asp:BoundField>

                <asp:HyperLinkField DataTextField="PName" HeaderText="项目名称" DataNavigateUrlFields="ProjectID"
                    DataNavigateUrlFormatString="PDetails.aspx?ProjectID={0}&pageTypeId=0" ItemStyle-HorizontalAlign="Left"
                    Target="_blank" />
                <asp:BoundField DataField="CreatedDate" HeaderText="发布时间" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="SubjectName" HeaderText="学 科" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="StateName" HeaderText="状 态" ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField HeaderText="编辑" ItemStyle-Width="120px">
                    <ItemTemplate>
                        <asp:Button ID="btnEdit" CausesValidation="false" CommandName="EditProject" Text="编辑"
                            runat="server"
                            BorderStyle="None" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="关闭" ItemStyle-Width="120px">
                    <ItemTemplate>
                        <asp:Button ID="btnClose" CausesValidation="false" OnClientClick="javascript:return confirm('确定要关闭本项目吗?  关闭项目则不再显示,该操作不可逆,请谨慎操作!');"
                            CommandName="ProjectClose" Text="关闭" runat="server" ToolTip="关闭项目则不再显示,该操作不可逆,请谨慎操作!"
                            BorderStyle="None" />
                    </ItemTemplate>
                </asp:TemplateField>
                
            </Columns>

        </asp:GridView>
        <br/>
    </div>
   

    <p>&nbsp;</p>

    <div style="padding-left: 20px; margin-left: 10px;">
        <asp:Label ID="error" CssClass="redStar" runat="server" Text=""></asp:Label>
    </div>
    <div class="WorksMain">
        <div style="font-family: 微软雅黑; font-size: 16px; font-weight: bold; margin-top: 10px;color: black;" runat="server" id="formTitle">
            &nbsp;&nbsp;新建项目
        </div>
        <hr style="height: 1px; width: 100%; margin-top: 4px; filter: alpha(opacity=5,finishopacity=100,style=1);" />
        <asp:Wizard ID="wzdProjectInfo" runat="server" BackColor="#F7F6F3" ActiveStepIndex="0"
            BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px"
            Font-Names="Verdana" Font-Size="0.8em" OnNextButtonClick="OnNextClick">
            <HeaderStyle BackColor="#5D7B9D" BorderStyle="Solid" Font-Bold="True"
                Font-Size="0.9em" ForeColor="White" HorizontalAlign="Left" />
            <NavigationButtonStyle BackColor="#FFFBFF" BorderColor="#CCCCCC"
                BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em"
                ForeColor="#284775" />
            <SideBarButtonStyle BorderWidth="0px" Font-Names="Verdana" ForeColor="White" />
            <SideBarStyle BackColor="#7C6F57" BorderWidth="0px" Font-Size="0.9em"
                VerticalAlign="Top" />
            <StepStyle BorderWidth="0px" ForeColor="#5D7B9D" />
            <WizardSteps>
                <asp:WizardStep ID="stepInfo" runat="server" Title="项目信息" AllowReturn="False" StepType="Start">
                    <table cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <p>
                                    <label class="WorksVerification"><span class="redStar">*</span>项目名称:</label>
                                    <asp:TextBox ID="txtName" runat="server" Width="500px" Height="25px" BorderColor="#CCCCCC"
                                        BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="redStar" runat="server"
                                        ErrorMessage="必填" ControlToValidate="txtName"></asp:RequiredFieldValidator>
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <p>
                                    <label class="WorksVerification"><span class="redStar">*</span>所属学科:</label>
                                    <asp:DropDownList ID="ddlSubjectA" runat="server" CssClass="cssddlSubject" AutoPostBack="True" OnSelectedIndexChanged="ddlSubjectA_OnSelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddlSubjectB" runat="server" CssClass="cssddlSubject" AutoPostBack="True" Visible="False" OnSelectedIndexChanged="ddlSubjectB_OnSelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddlSubjectC" runat="server" CssClass="cssddlSubject" Visible="False" >
                                    </asp:DropDownList>
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <p>
                                    <label class="WorksVerification" style="float: left"><span class="redStar">&nbsp;</span>项目简介:</label>
                                    <span class="fullh" style="width: 500px;">
                                        <SharePoint:InputFormTextBox ID="txtIntroduce" runat="server" RichText="true" Rows="10"
                                            TextMode="MultiLine" RichTextMode="Compatible" Style="width: 500px; height: 200px;"></SharePoint:InputFormTextBox>
                                    </span>
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div align="right">
                                    <p style="clear: both; margin-right: 160px;">
                                        <asp:HiddenField ID="hfID" runat="server" />
                                        <asp:Button ID="btnSubmit" runat="server" Text="发     布" Style="width: 133px; height: 35px;
                                            background: url(images/ButtonBg.gif); border: 0; color: #fff; font-weight: bolder;
                                            font-size: 15px; margin: 0; cursor: pointer"/>
                                        <asp:Button ID="btnSave" runat="server" Text="保     存" Style="width: 133px; height: 35px;
                                            background: url(images/ButtonBg.gif); border: 0; color: #fff; font-weight: bolder;
                                            font-size: 15px; margin: 0; cursor: pointer" />
                                    </p>
                                </div>
                            </td>
                        </tr>
                    </table>
                </asp:WizardStep>
                <asp:WizardStep ID="stepFile" runat="server" Title="项目计划书">
                    <p>
                        <label class="WorksVerification" style="float: left">
                            <span class="redStar">&nbsp;</span>立项报告：
                        </label>
                        <asp:FileUpload ID="fuWorks" runat="server" Width="400px" />
                        <asp:Button ID="btnUpFile" runat="server" Text="上传" CausesValidation="False" OnClick="btnUpFile_OnClick" />
                    </p>
                    <p style="color: red; padding-left: 50px">
                        支持的文档格式：.docx, .doc, .pdf, .ppt, .pptx, .xls, .xlsx
                    </p>
                    <div style="margin-left: 32px;">
                        <asp:GridView ID="gvFiles" runat="server"></asp:GridView>
                    </div>
                </asp:WizardStep>
            </WizardSteps>
        </asp:Wizard>
    </div>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
项目发布
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
项目发布
</asp:Content>
