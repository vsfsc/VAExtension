<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="Project.Layouts.Project.Test"
    DynamicMasterPageFile="~masterurl/default.master" EnableEventValidation="false" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <style type="text/css">
.btnRole {
	-moz-box-shadow:inset 0px 1px 0px 0px #cf866c;
	-webkit-box-shadow:inset 0px 1px 0px 0px #cf866c;
	box-shadow:inset 0px 1px 0px 0px #cf866c;
	background:-webkit-gradient(linear, left top, left bottom, color-stop(0.05, #d0451b), color-stop(1, #bc3315));
	background:-moz-linear-gradient(top, #d0451b 5%, #bc3315 100%);
	background:-webkit-linear-gradient(top, #d0451b 5%, #bc3315 100%);
	background:-o-linear-gradient(top, #d0451b 5%, #bc3315 100%);
	background:-ms-linear-gradient(top, #d0451b 5%, #bc3315 100%);
	background:linear-gradient(to bottom, #d0451b 5%, #bc3315 100%);
	filter:progid:DXImageTransform.Microsoft.gradient(startColorstr='#d0451b', endColorstr='#bc3315',GradientType=0);
	background-color:#d0451b;
	-moz-border-radius:3px;
	-webkit-border-radius:3px;
	border-radius:3px;
	border:1px solid #942911;
	display:inline-block;
	cursor:pointer;
	color:#ffffff;
	font-family:Arial;
	font-size:13px;
	padding:6px 24px;
	text-decoration:none;
	text-shadow:0px 1px 0px #854629;
}
.btnRole:hover {
	background:-webkit-gradient(linear, left top, left bottom, color-stop(0.05, #bc3315), color-stop(1, #d0451b));
	background:-moz-linear-gradient(top, #bc3315 5%, #d0451b 100%);
	background:-webkit-linear-gradient(top, #bc3315 5%, #d0451b 100%);
	background:-o-linear-gradient(top, #bc3315 5%, #d0451b 100%);
	background:-ms-linear-gradient(top, #bc3315 5%, #d0451b 100%);
	background:linear-gradient(to bottom, #bc3315 5%, #d0451b 100%);
	filter:progid:DXImageTransform.Microsoft.gradient(startColorstr='#bc3315', endColorstr='#d0451b',GradientType=0);
	background-color:#bc3315;
}
.btnRole:active {
	position:relative;
	top:1px;
}


.myspan {
	-moz-box-shadow:inset 0px 39px 0px -24px #9acc85;
	-webkit-box-shadow:inset 0px 39px 0px -24px #9acc85;
	box-shadow:inset 0px 39px 0px -24px #9acc85;
	background-color:#74ad5a;
	-moz-border-radius:4px;
	-webkit-border-radius:4px;
	border-radius:4px;
	border:1px solid #3b6e22;
	display:inline-block;
	color:#ffffff;
	font-family:Arial;
	font-size:15px;
	padding:6px 15px;
	text-decoration:none;
	text-shadow:0px 1px 0px #92b879;
}
.myspan:hover {
	background-color:#68a54b;
}
.myspan:active {
	position:relative;
	top:1px;
}

.applyspan {
	-moz-box-shadow:inset 0px 0px 14px -3px #f2fadc;
	-webkit-box-shadow:inset 0px 0px 14px -3px #f2fadc;
	box-shadow:inset 0px 0px 14px -3px #f2fadc;
	background:-webkit-gradient(linear, left top, left bottom, color-stop(0.05, #dbe6c4), color-stop(1, #9ba892));
	background:-moz-linear-gradient(top, #dbe6c4 5%, #9ba892 100%);
	background:-webkit-linear-gradient(top, #dbe6c4 5%, #9ba892 100%);
	background:-o-linear-gradient(top, #dbe6c4 5%, #9ba892 100%);
	background:-ms-linear-gradient(top, #dbe6c4 5%, #9ba892 100%);
	background:linear-gradient(to bottom, #dbe6c4 5%, #9ba892 100%);
	filter:progid:DXImageTransform.Microsoft.gradient(startColorstr='#dbe6c4', endColorstr='#9ba892',GradientType=0);
	background-color:#dbe6c4;
	-moz-border-radius:6px;
	-webkit-border-radius:6px;
	border-radius:6px;
	border:1px solid #b2b8ad;
	display:inline-block;
	color:#757d6f;
	font-family:Arial;
	font-size:15px;
	font-weight:bold;
	padding:6px 24px;
	text-decoration:none;
	text-shadow:0px 1px 0px #ced9bf;
}
.applyspan:hover {
	background:-webkit-gradient(linear, left top, left bottom, color-stop(0.05, #9ba892), color-stop(1, #dbe6c4));
	background:-moz-linear-gradient(top, #9ba892 5%, #dbe6c4 100%);
	background:-webkit-linear-gradient(top, #9ba892 5%, #dbe6c4 100%);
	background:-o-linear-gradient(top, #9ba892 5%, #dbe6c4 100%);
	background:-ms-linear-gradient(top, #9ba892 5%, #dbe6c4 100%);
	background:linear-gradient(to bottom, #9ba892 5%, #dbe6c4 100%);
	filter:progid:DXImageTransform.Microsoft.gradient(startColorstr='#9ba892', endColorstr='#dbe6c4',GradientType=0);
	background-color:#9ba892;
}
.applyspan:active {
	position:relative;
	top:1px;
}


.yuanjiaoDiv{
 font-family: Arial;
 border: 1px solid #63b8ee;
 border-radius: 20px;
 padding: 30px 30px;
 width: 330px;
 }
    </style>
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <%--<p>
        <asp:Label ID="lbGvTitle" runat="server" Text=""></asp:Label>
        <asp:GridView ID="gvMyRoles" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField HeaderText="角色ID" DataField="RoleID" Visible="false" />
                <asp:BoundField HeaderText="拥有角色" DataField="RoleName" />
                <asp:BoundField HeaderText="获得日期" DataField="Approved" />
            </Columns>
        </asp:GridView>
    </p>
    <asp:GridView ID="gvApprovingRoles" runat="server" AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField HeaderText="角色ID" DataField="RoleID" Visible="false" />
            <asp:BoundField HeaderText="待审批角色" DataField="RoleName" />
            <asp:BoundField HeaderText="申请日期" DataField="Created" />
        </Columns>
    </asp:GridView>
    <p>
        <asp:GridView ID="gvNewRoles" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField HeaderText="角色ID" DataField="RoleID" Visible="false" />
                <asp:BoundField HeaderText="可申请角色" DataField="RoleName" />
                <asp:TemplateField HeaderText="申 请" ItemStyle-Width="120px">
                    <ItemTemplate>
                        <asp:Button ID="btnApply" CausesValidation="false" CommandName="cmdApply" Text="申请" runat="server" BorderStyle="None" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        
    </p>
        --%>
    <fieldset class="yuanjiaoDiv">
        <legend style="font-size:13px;color:#2c9d11;">已拥有角色</legend>
        <div id="MyRolesDiv" runat="server"></div>
    </fieldset>
	 <fieldset class="yuanjiaoDiv">
        <legend style="font-size:13px;color:#b5b10e;">待审批角色</legend>
        <div id="ApprovingRolesDiv" runat="server"></div>
    </fieldset>
	 <fieldset class="yuanjiaoDiv">
        <legend style="font-size:13px;color:#f24537;">可申请角色</legend>
        <div id="NewRolesDiv" runat="server"></div>
    </fieldset>
    <div>
        <asp:TextBox ID="tbTest" runat="server"></asp:TextBox>
        <asp:Button ID="btnTest" runat="server" Text="测试自动创建博客类别" OnClick="btnTest_OnClick"/>
        <asp:GridView ID="gvTypes" runat="server"></asp:GridView>
    </div>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
测试页
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
测试页
</asp:Content>
