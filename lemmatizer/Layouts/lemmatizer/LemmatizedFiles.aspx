<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LemmatizedFiles.aspx.cs" Inherits="lemmatizer.Layouts.lemmatizer.LemmatizedFiles" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <script type="text/javascript">
        function SelectAll(aControl) {
            var tempControl = aControl;
            var isChecked = tempControl.checked;

            var elem = tempControl.form.elements;
            for (var i = 0; i < elem.length; i++)
                if (elem[i].type == "checkbox" && elem[i].id != tempControl.id) {
                    if (elem[i].checked != isChecked)
                        elem[i].click();
                }
        }
    </script>
    <script type="text/javascript" >
        function RecordPostion(obj) {
            var diva = obj;
            var sx = document.getElementById('<%=dvscrollX.ClientID %>');
            var sy = document.getElementById('<%=dvscrollY.ClientID %>');
            sy.value = diva.scrollTop;
            sx.value = diva.scrollLeft;
        }

        function GetResultFromServer() {
            var sx = document.getElementById('<%=dvscrollX.ClientID %>');
            var sy = document.getElementById('<%=dvscrollY.ClientID %>');
            //为什么div1就不用ClientID，其它服务器控件就要用？  
            document.getElementById('gvDiv').scrollTop = sy.value;
            document.getElementById('gvDiv').scrollLeft = sx.value;
        }
    </script>
    <style type="text/css">
         .yuanjiaobtn {
             font-family: Arial;
            border: 1px solid #379082;
             -ms-border-radius:5px 5px 5px 5px;
             border-radius:5px 5px 5px 5px;
            background-color:#999999;
            cursor: pointer;
            font-size: 16px;
            font-weight: bold;
            color: blue;
         }
    </style>
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <asp:HiddenField ID="dvscrollX" runat="server" />
    <asp:HiddenField ID="dvscrollY" runat="server" />
    <div style="width: 1000px; height: 500px; background-color:#DCDCDC;">
        <table style="width: 100%">
            <tr>
                <td>
                    <div style="padding: 5px 5px 5px 5px;">
            <asp:Button ID="PackDown" runat="server" Text="PackageDownload" OnClick="PackDown_Click"
                ToolTip="Package Selected and Download" CssClass="yuanjiaobtn" OnClientClick = "this.form.onsubmit = function() {return true;}" /></div>
                </td>
                <td>
                     <div style="background-color: #FFD700; padding-left: 10px; padding-top: 5px; padding-bottom: 5px;">
                         Tips：Click the header row the Collation can be changed; Click on the filename to download the file; Click on the x to delete the file.
                     </div>
                </td>
            </tr>
        </table>
       
        
        <div style="height: 430px; overflow-x: hidden; overflow-y: auto; margin: 10px;" onscroll="javascript:RecordPostion(this);" id="gvDiv">
            <asp:GridView ID="gvFiles" runat="server" Width="960px" OnRowCreated="gvFiles_RowCreated" ForeColor="#333333" GridLines="None" OnSorting="gvFiles_Sorting" AllowSorting="true" >
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField HeaderText="NO." InsertVisible="False">
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <%#Container.DataItemIndex+1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<input type='checkbox' onclick='javascript:SelectAll(this)' title='Click to Select or Deselect  All' />"
                        InsertVisible="False" HeaderStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" />
                        </ItemTemplate>

                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>

                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Delete" >
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnDel" runat="server" ForeColor="red" ToolTip="删除文件" Text=" &nbsp;&nbsp;×&nbsp;&nbsp; "
                                CommandArgument='<%#Eval("FileName") %>'
                                OnCommand="lbtnDel_Command" OnClientClick='return confirm("Are you sure to delete this file ?;");this.form.onsubmit = function() {return true;}'
                                ItemStyle-HorizontalAlign="Center"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="FileName(右击文件名选择“目标另存为”保存文件)" InsertVisible="False" SortExpression="FileName">
                        <ItemTemplate>
                            <div><a href='../db/export/<%#Eval("FileName") %>' target="_blank" title="右击选择“目标另存为”到本地" type="text/plain"><%# Eval("FileName").ToString().Remove(Eval("FileName").ToString().IndexOf('(')) %></a></div>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EditRowStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                    VerticalAlign="Middle" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />
            </asp:GridView>
            <div style="text-align: center;font-weight: bold;font-size: 14px; background-color: #507CD1; width: 960px; color: white;">
                ···The End···</div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
Lemmatization OnLine
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
Lemmatized Files
</asp:Content>
