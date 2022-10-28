using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASPnet
{
    public partial class _26DataList_SelectedItemTemplate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void DataList1_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if(e.CommandName=="Select")
            {
                DataList1.SelectedIndex = e.Item.ItemIndex;
                DataBind();
            }
        }
    }
}