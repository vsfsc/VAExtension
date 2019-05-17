using System;
using System.Collections.Generic;

using System.Linq;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using ContestDll;

namespace ContestWeb.Layouts.ContestWeb
{
    public partial class test : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string url = SPContext.Current.Web.Url;
            int i = 0;
 
          
        }
       
    }
    public class Test 
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}
