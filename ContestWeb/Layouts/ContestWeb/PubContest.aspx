<%@ Assembly Name="ContestDll, Version=1.0.0.0, Culture=neutral, PublicKeyToken=30c3b5583be889be" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" Inherits="ContestDll.PubContest" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <link rel="stylesheet" type="text/css" href="./css/base.css" />
    <link rel="stylesheet" type="text/css" href="./css/page.css" />
    <script type="text/javascript">
        function IsValidText() {
            var text = RTE_GetRichEditTextOnly("<%=txtRequire.ClientID%>");
            if (text != "") {
                return true;
            }
            else {
                alert('作品要求必填');
                RTE_GiveEditorFocus("<%= txtRequire.ClientID%>");
                return false;
            }
        }
    </script>
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
   
    <!--<div style="height: 25px; margin-left: 30px">
        <span style="font-family:微软雅黑;font-size:16px;font-weight:border;">&nbsp;&nbsp;竞赛届次列表</span>
    </div>
-->

    <div style="margin-left: 30px" class="WorksMain">
        <span style="font-family: 微软雅黑; font-size: 16px; font-weight: border; color: black;">
            竞赛届次列表</span>
        <asp:GridView ID="gvPeriod" runat="server" AllowPaging="true" PageSize="5" AutoGenerateColumns="False"
            CellPadding="0" GridLines="None" Height="30px" Width="700px" PagerSettings-Mode="NumericFirstLast"
            PagerSettings-FirstPageText="首页" PagerSettings-NextPageText="下一页" PagerSettings-PreviousPageText="上一页"
            PagerSettings-LastPageText="尾页">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <RowStyle CssClass="GridViewRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" HorizontalAlign="Center" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" HorizontalAlign="Left" />


            <EmptyDataTemplate>
                <table>
                    <tr>
                        <td>竞赛名称
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">当前没有数据
                        </td>
                    </tr>
                </table>
            </EmptyDataTemplate>

            <Columns>
                <asp:BoundField DataField="PeriodID" HeaderText="期次ID">
                    <FooterStyle CssClass="hidden" />
                    <HeaderStyle CssClass="hidden" />
                    <ItemStyle Width="60px" CssClass="hidden"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="PeriodTitle" HeaderText="竞赛届次" />
                <asp:TemplateField HeaderText="关闭" ItemStyle-Width="120px">
                    <ItemTemplate>
                        <asp:Button ID="btnDel" CausesValidation="false" OnClientClick="javascript:return confirm('确定要关闭本次比赛吗?  关闭竞赛则不再显示,请谨慎操作!');"
                            CommandName="Del" Text="关闭" runat="server" ToolTip="关闭竞赛则不再显示,请谨慎操作!"
                            BorderStyle="None" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="编辑" ItemStyle-Width="120px">
                    <ItemTemplate>
                        <asp:Button ID="btnEdit" CausesValidation="false" CommandName="Down" Text="编辑" runat="server"
                            BorderStyle="None" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="上传样例" ItemStyle-Width="120px">
                    <ItemTemplate>
                        <%-- <asp:Button ID="btnUpload" CausesValidation="false" CommandName="Upload" Text="上传" runat="server" BorderStyle="None" />--%>
                        <asp:HyperLink Target="_blank" runat="server" ID="lnkUpload">上传</asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="评价指标" ItemStyle-Width="120px">
                    <ItemTemplate>
                        <%--<asp:Button ID="btnStandard" CausesValidation="false" CommandName="Standard" Text="添加" runat="server" BorderStyle="None" />--%>
                        <asp:HyperLink Target="_blank" runat="server" ID="lnkStandard">添加</asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>

        </asp:GridView>
        <br>
    </div>
    <p>&nbsp;</p>
     <div style="padding-left: 20px;margin-left: 10px;">
        <asp:Label ID="error" CssClass="redStar" runat="server" Text=""></asp:Label>
    </div>
    <div class="WorksMain">

        <span style="font-family: 微软雅黑; font-size: 16px; font-weight: border; margin-top: 10px;
            color: black;">&nbsp;&nbsp;新增或修改比赛</span>

        <hr style="height: 1px; width: 700px; margin-top: 4px; filter: alpha(opacity=5,finishopacity=100,style=1);" />
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <p>
                        <label class="WorksVerification"><span class="redStar"><i>*</i></span>比赛名称:</label>
                        <asp:TextBox ID="txtName" runat="server" Width="450px" Height="30px" BorderColor="#CCCCCC"
                            BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="redStar" runat="server"
                            ErrorMessage="必填" ControlToValidate="txtName"></asp:RequiredFieldValidator>
                    </p>
                </td>
            </tr>
            <tr>
                <td>
                    <p>
                        <label class="WorksVerification"><span class="redStar"><i>*</i></span>比赛类别:</label>
                        <asp:DropDownList ID="ddlOneWorksType" runat="server" CssClass="txtdoenlista" Height="25px"
                            AutoPostBack="True">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlTwoWorksType" runat="server" CssClass="txtdoenlista" Width="220px"
                            Height="25px">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="redStar" runat="server"
                            ErrorMessage="必填" ControlToValidate="ddlTwoWorksType"></asp:RequiredFieldValidator>
                    </p>
                </td>
            </tr>
            <tr>
                <td>
                    <p>
                        <label class="WorksVerification"><span class="redStar"><i>*</i></span>最多参与人数:</label>
                        <asp:TextBox ID="txtNum" runat="server" Width="20px" Height="20px" BorderColor="#CCCCCC"
                            BorderStyle="Solid" BorderWidth="1px" ToolTip="只能输入1-10的整数,仅单人参赛的请输入1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" CssClass="redStar" runat="server"
                            ErrorMessage="必填" ControlToValidate="txtNum"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ID="RangeValidator1" CssClass="redStar" runat="server" ErrorMessage="只能输入1-10的数字"
                            Type="Integer" ControlToValidate="txtNum" MinimumValue="1" MaximumValue="10"></asp:RangeValidator>
                    </p>
                </td>
            </tr>

            <tr>
                <td>
                    <p>
                        <label class="WorksVerification" style="float: left"><span class="redStar"><i>*</i></span>参赛作品要求:</label>
                        <span class="fullh" style="width: 500px;">
                            <SharePoint:InputFormTextBox ID="txtRequire" runat="server" RichText="true" Rows="10"
                                TextMode="MultiLine" RichTextMode="Compatible" Style="width: 500px; height: 200px;"></SharePoint:InputFormTextBox>
                        </span>
                    </p>
                </td>
            </tr>

        </table>

        <table cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="4">
                    <p></p>
                    <hr style="height: 1px; border: none; border-top: 1px dotted #CCCCCC; width: 700px;
                        margin-top: 4px;" />
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <h3>比赛报名时间</h3>

                </td>
            </tr>
            <tr>
                <td width="150px" align="right">
                    <label class="WorksVerification"><i style="color: red">*</i>开放时间:</label></td>
                <td width="100px" align="left">
                    <SharePoint:DateTimeControl ID="dateTimeStartSubmit" AutoPostBack="true" runat="server"
                        DateOnly="true" />
                </td>
                <td width="150px" align="right">
                    <label class="WorksVerification"><i style="color: red">*</i>截止时间:</label></td>
                <td align="left">
                    <SharePoint:DateTimeControl ID="dateTimeEndSubmit" runat="server" DateOnly="true" />
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <p></p>
                    <hr style="height: 1px; border: none; border-top: 1px dotted #CCCCCC; width: 700px;
                        margin-top: 4px;" />
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <h3>作品评比时间</h3>

                </td>
            </tr>
            <tr>
                <td width="150px" align="right">
                    <label class="WorksVerification"><i style="color: red">*</i>开放时间:</label></td>
                <td width="100px" align="left">
                    <SharePoint:DateTimeControl ID="dateTimeStartScore" runat="server" DateOnly="true" />
                </td>
                <td width="150px" align="right">
                    <label class="WorksVerification"><i style="color: red">*</i>截止时间:</label></td>
                <td align="left">
                    <SharePoint:DateTimeControl ID="dateTimeEndScore" runat="server" DateOnly="true" />
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <p></p>
                    <hr style="height: 1px; border: none; border-top: 1px dotted #CCCCCC; width: 700px;
                        margin-top: 4px;" />
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <h3>作品公示时间</h3>

                </td>
            </tr>
            <tr>
                <td width="150px" align="right">
                    <label class="WorksVerification"><i style="color: red">*</i>开放时间:</label></td>
                <td width="100px" align="left">
                    <SharePoint:DateTimeControl ID="dateTimeStartPublic" runat="server" DateOnly="true" />
                </td>
                <td width="150px" align="right">
                    <label class="WorksVerification"><i style="color: red">*</i>截止时间:</label></td>
                <td align="left">
                    <SharePoint:DateTimeControl ID="dateTimeEndPublic" runat="server" DateOnly="true" />
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <p></p>
                    <hr style="height: 1px; border: none; border-top: 1px dotted #CCCCCC; width: 700px;
                        margin-top: 4px;" />
                </td>
            </tr>
        </table>
        <div align="right">
            <p style="clear: both; margin-right: 160px;">
                <asp:HiddenField ID="hfID" runat="server" />
                <asp:Button ID="btnAdd" runat="server" Text="发     布" Style="width: 133px; height: 35px;
                    background: url(images/ButtonBg.gif); border: 0; color: #fff; font-weight: bolder;
                    font-size: 15px; margin: 0; cursor: pointer"
                    OnClientClick="return IsValidText()" />
                <asp:Button ID="btnSave" runat="server" Text="保     存" Style="width: 133px; height: 35px;
                    background: url(images/ButtonBg.gif); border: 0; color: #fff; font-weight: bolder;
                    font-size: 15px; margin: 0; cursor: pointer"
                    OnClientClick="return IsValidText()" />
            </p>
        </div>
    </div>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
发布新比赛
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
发布比赛
</asp:Content>
