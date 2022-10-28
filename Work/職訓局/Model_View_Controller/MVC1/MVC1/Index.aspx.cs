using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MVC1.Models;

namespace MVC1
{
    public partial class Index : System.Web.UI.Page
    {
        dbProductEntities db = new dbProductEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            GridView1.DataSource = db.tProduct.ToList();
            GridView1.DataBind();
        }
    }
}