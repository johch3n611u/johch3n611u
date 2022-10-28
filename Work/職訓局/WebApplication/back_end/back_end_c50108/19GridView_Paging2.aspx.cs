using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASPnet
{
    public partial class _19GridView_Paging2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //TextBox tb = new TextBox();
            //tb.Text = "abcd";
            //form1.Controls.Add(tb);
        }

        //protected void lkbNext_Click(object sender, EventArgs e)
        //{
        //    GridView1.PageIndex++;
        //}

        //protected void lkbPrev_Click(object sender, EventArgs e)
        //{
        //    if(GridView1.PageIndex>0)
        //        GridView1.PageIndex--;
        //}

        protected void PageChange_Click(object sender, EventArgs e)
        {
            if (((LinkButton)sender).ID == "lkbPrev")
            {
                if (GridView1.PageIndex > 0)
                    GridView1.PageIndex--;
            }
            else
            {
                GridView1.PageIndex++;
            }
        }

        protected void GridView1_DataBound(object sender, EventArgs e)
        {
            TableCell tc = GridView1.BottomPagerRow.Cells[0];

            string page = "Page " + (GridView1.PageIndex + 1) + " of " + GridView1.PageCount;
            Label lblInfo = (Label)tc.FindControl("lblInfo");
            lblInfo.Text = page;

            DropDownList ddlPager = (DropDownList)tc.FindControl("ddlPager");

            ListItem item;
            for (int i = 1; i <= GridView1.PageCount; i++)
            {
                item = new ListItem(i.ToString());
                //item.Text = i.ToString();

                if(GridView1.PageIndex==i-1)
                    item.Selected = true;
                ddlPager.Items.Add(item);
            }

            
        }

        protected void ddlPager_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlPager= (DropDownList)GridView1.BottomPagerRow.Cells[0].FindControl("ddlPager");

            GridView1.PageIndex = ddlPager.SelectedIndex;
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            Response.Write(TextBox1.Text);
        }
    }
}