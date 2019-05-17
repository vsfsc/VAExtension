<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CourseChoose.aspx.cs" Inherits="CourseChooseList.Layouts.CourseChooseList.CourseChoose" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <script type="text/javascript">
        function checkboxCount(operate) {
            var controlIndex;
            var element;
            var numberofControls;
            numberofControls = document.forms[0].length;
            var checkBoxCount = 0;
            for (controlIndex = 0; controlIndex < numberofControls; controlIndex++) {
                element = document.forms[0][controlIndex];
                if (element.type == "checkbox") {
                    if (element.checked == true) {
                        checkBoxCount++;
                    }
                }
            }
            if (checkBoxCount == 0) {
                alert("请选中要" + operate + "的记录！");
                return false;
            }
            else {
                if (confirm("确定要" + operate + "选中的记录？") == false)
                    return false;
                else
                    return true;
            }
        }


    </script>
    <style type="text/css">
        .hidden {
            display: none;
        }

        .GridViewStyle {
            border-right: 2px solid #A7A6AA;
            border-bottom: 2px solid #A7A6AA;
            border-left: 2px solid white;
            border-top: 2px solid white;
            padding: 4px;
        }

            .GridViewStyle a {
                color: #FFFFFF;
            }

        .GridViewHeaderStyle th {
            border-left: 1px solid #EBE9ED;
            border-right: 1px solid #EBE9ED;
        }

        .GridViewHeaderStyle {
            background-color: #5D7B9D;
            font-weight: bold;
            color: White;
        }

        .GridViewFooterStyle {
            background-color: #5D7B9D;
            font-weight: bold;
            color: White;
        }

        .GridViewRowStyle {
            background-color: #F7F6F3;
            color: #333333;
        }

        .GridViewAlternatingRowStyle {
            background-color: #FFFFFF;
            color: #284775;
        }

            .GridViewRowStyle td, .GridViewAlternatingRowStyle td {
                border: 1px solid #EBE9ED;
            }

        .GridViewSelectedRowStyle {
            background-color: #E2DED6;
            font-weight: bold;
            color: #333333;
        }

        .GridViewPagerStyle {
            background-color: #284775;
            color: #FFFFFF;
        }

            .GridViewPagerStyle table /* to center the paging links*/ {
                margin: 0 auto 0 auto;
            }
    </style>
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">

    <asp:GridView ID="gvCourse" runat="server" AllowPaging="False" AutoGenerateColumns="False"
        CellPadding="0" GridLines="None" Height="30px"
        Width="100%" CssClass="GridViewStyle">
        <EmptyDataTemplate>
            <table>
                <tr>
                    <td>选择
                    </td>
                    <td>课程名称
                    </td>
                    <td>教师
                    </td>
                    <td>开课时间
                    </td>
                    <td>结课时间
                    </td>
                    <td>学分
                    </td>
                </tr>
                <tr>
                    <td colspan="5">当前没有可选课程
                    </td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <RowStyle CssClass="GridViewRowStyle" />
        <Columns>
            <asp:TemplateField HeaderText="选择" ItemStyle-Width="40px">
                <ItemTemplate>
                    <asp:CheckBox ID="cbChoose" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="ID" HeaderText="课程ID">
                <FooterStyle CssClass="hidden" />
                <HeaderStyle CssClass="hidden" />
                <ItemStyle Width="40px" CssClass="hidden"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="ModuleName" HeaderText="课程名称">
            </asp:BoundField>
            <asp:BoundField DataField="TeacherAccount" HeaderText="教师" CssClass="hidden">
                <FooterStyle CssClass="hidden" />
                <HeaderStyle CssClass="hidden" />
            </asp:BoundField>
            <asp:BoundField DataField="StartDate" HeaderText="开课时间">
            </asp:BoundField>
            <asp:BoundField DataField="EndDate" HeaderText="结课时间">
            </asp:BoundField>
            <asp:BoundField DataField="ClassHours" HeaderText="学时">
            </asp:BoundField>
            <asp:BoundField DataField="IsRequired" HeaderText="必选">
                <FooterStyle CssClass="hidden" />
                <HeaderStyle CssClass="hidden" />
                <ItemStyle Width="40px" CssClass="hidden"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="IsFinish" HeaderText="已选">
                <FooterStyle CssClass="hidden" />
                <HeaderStyle CssClass="hidden" />
                <ItemStyle Width="40px" CssClass="hidden"></ItemStyle>
            </asp:BoundField>
           <%-- <asp:TemplateField HeaderText="取消课程" ControlStyle-Width="90px" ItemStyle-Width="100px">
                <ItemTemplate>
                    <asp:Button ID="btnCancelCourse" CommandName="CancelCourse" Text="取消" runat="server"
                        BorderStyle="None" />
                </ItemTemplate>
            </asp:TemplateField>--%>
            <asp:BoundField DataField="SCID" HeaderText="选课ID">
                <FooterStyle CssClass="hidden" />
                <HeaderStyle CssClass="hidden" />
                <ItemStyle Width="40px" CssClass="hidden"></ItemStyle>
            </asp:BoundField>


        </Columns>
        <HeaderStyle CssClass="GridViewHeaderStyle" HorizontalAlign="Left" />
        <PagerStyle HorizontalAlign="Center" />
    </asp:GridView>
    <br />
    <asp:Button Style="border: 0; background-color: #32acd1; width: 90px; height: 30px; color: #fff"
        ID="btnOk" runat="server" Text="选课" OnClientClick="return checkboxCount('选课')" OnClick="btnOk_Click" />
    <asp:Button Style="border: 0; background-color: #32acd1; width: 90px; height: 30px; color: #fff"
        ID="btnCancel" runat="server" Text="去选" OnClientClick="return checkboxCount('取消选课')" OnClick="btnCancel_Click" />
    <asp:Label ID="Des" runat="server" Text=""></asp:Label>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    学生选课
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server">
    大学计算机
</asp:Content>
