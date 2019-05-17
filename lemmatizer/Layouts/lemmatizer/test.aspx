<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="lemmatizer.Layouts.lemmatizer.test" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">

</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <asp:Button ID ="btnOk" runat="server" Text="打印" OnClick="btnOk_Click" />
            <div id="exporting" style="display:none;"><br /><font color="#ff0000" size="4.5px">数据正在打印中，请稍等……</font></div>
    <asp:Button runat="server" Text="DownLoad" ID ="btnDownload"  OnClick="Unnamed_Click" ></asp:Button>
    <asp:FileUpload ID="fileUpload" runat="server" />
<asp:button runat="server"  id="btnRun"  text="导入" OnClick="btnRun_Click"   />
    <asp:Label ID="lblMsg" runat="server"  Text="lblMsg"></asp:Label><br />
    <asp:TextBox ID="txtShow" runat="server" TextMode="MultiLine" Width ="800px" Height ="600px" ></asp:TextBox>
    <asp:CheckBoxList ID="cblist" runat="server" ToolTip="Choose Vocabulary" RepeatDirection="Horizontal" Width="400px" OnSelectedIndexChanged="cblist_SelectedIndexChanged" AutoPostBack="true" >
                            <asp:ListItem Value="1">基本要求</asp:ListItem>
                            <asp:ListItem Value="2">较高要求</asp:ListItem>
                            <asp:ListItem Value="3">更高要求</asp:ListItem>
                        </asp:CheckBoxList>
</asp:Content>


<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
应用程序页
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
我的应用程序页
</asp:Content>
