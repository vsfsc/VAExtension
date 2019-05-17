using System;
using System.Web;
using System.Web.UI;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Publishing;
using Microsoft.SharePoint.Publishing.Navigation;

namespace SmoothNavigationMenu.ControlTemplates.SmoothNavigationMenu
{
    public partial class SmoothNavMenu : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SiteMapNode rootnode = GetSiteMapRootNodeofCurrentWeb();
            string menuContent = BuildMenuContent(rootnode);
            menuContenLiteral.Text = menuContent;
        }

        private string BuildMenuContent(SiteMapNode rootnode)
        {
            throw new NotImplementedException();
        }

        private SiteMapNode GetSiteMapRootNodeofCurrentWeb()
        {
            SiteMapProvider siteMapProvider = PortalSiteMapProvider.CombinedNavSiteMapProvider;
            SiteMapNode rootNode = siteMapProvider.RootNode;
            return rootNode;
        }
    }
}
