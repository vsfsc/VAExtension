using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Data;

namespace SP2013.SPOrderWebPart
{
    [ToolboxItemAttribute(false)]
    public class SPOrderWebPart : WebPart
    {

        // 更改可视 Web 部件项目项后，Visual Studio 可能会自动更新此路径。
        private const string _ascxPath = @"~/_CONTROLTEMPLATES/15/SP2013/SPOrderWebPart/SPOrderWebPartUserControl.ascx";

        #region 事件
        protected override void CreateChildControls()
        {
            Control control = Page.LoadControl(_ascxPath);
            Controls.Add(control);

        }
        #endregion



    }
}
