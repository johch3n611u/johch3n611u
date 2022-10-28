using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASPnet
{
    public partial class _01FirstWebPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            for(int i=0;i<100;i++)
            {
                Calendar myC = new Calendar();
                form1.Controls.Add(myC);
            }
        }
    }
}