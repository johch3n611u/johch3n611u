using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Drawing.Imaging;


namespace ASPnet
{
    public partial class _27DataList_Edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void DataList1_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if(e.CommandName=="Edit")
            {
                DataList1.EditItemIndex = e.Item.ItemIndex;
                DataBind();

                int index = e.Item.ItemIndex;
                string status = ((Label)DataList1.Items[index].FindControl("lblProduct_Status")).Text;
                RadioButtonList rbl = (RadioButtonList)DataList1.Items[index].FindControl("rblProduct_Status");
             
                if (status == "True")
                {
                    rbl.Items[0].Selected = true;
                }
                else
                    rbl.Items[1].Selected = true;

            }
            else if (e.CommandName == "Cancel")
            {
                DataList1.EditItemIndex = -1;
                DataBind();

            }
          
        }

        protected void DataList1_UpdateCommand(object source, DataListCommandEventArgs e)
        {
            

            //上傳新的產品圖檔
            FileUpload img=(FileUpload)e.Item.FindControl("fulProductsImg");
            Label proID = (Label)e.Item.FindControl("Product_IDLabel");

            if (img.FileName!="")
            {
                img.SaveAs(Server.MapPath("/tmpImage/temp.jpg"));
                img.Dispose();

                //System.Drawing.Image g = System.Drawing.Image.FromFile(img.PostedFile.FileName);
                System.Drawing.Image g = System.Drawing.Image.FromFile(Server.MapPath("/tmpImage/temp.jpg"));
                ImageFormat imgformat = g.RawFormat;
                Bitmap newImg = new Bitmap(g,360,360);
                Bitmap newSImg = new Bitmap(g, 120, 120);


                if (img.PostedFile.ContentType == "image/jpeg")
                {
                    newImg.Save(Server.MapPath("/ProductsImg/" + proID.Text + ".jpg"));
                    newSImg.Save(Server.MapPath("/ProductsImg/s" + proID.Text + ".jpg"));
                    //img.SaveAs(Server.MapPath("/ProductsImg/" + proID.Text + ".jpg"));
                }
                else if (img.PostedFile.ContentType == "image/png")
                {
                    newImg.Save(Server.MapPath("/ProductsImg/" + proID.Text + ".png"));
                    newSImg.Save(Server.MapPath("/ProductsImg/s" + proID.Text + ".png"));
                    //img.SaveAs(Server.MapPath("/ProductsImg/" + proID.Text + ".png"));
                }

            }

            //資料回寫資料庫
            TextBox name =(TextBox)e.Item.FindControl("txtProduct_Name");
            TextBox price = (TextBox)e.Item.FindControl("txtProduct_Price");
            TextBox price2 = (TextBox)e.Item.FindControl("txtProduct_Price2");
            TextBox intro = (TextBox)e.Item.FindControl("txtProduct_Intro");
            RadioButtonList status = (RadioButtonList)e.Item.FindControl("rblProduct_Status");

            

            SqlDataSource1.UpdateParameters["Product_Name"].DefaultValue = name.Text;
            SqlDataSource1.UpdateParameters["Product_Price"].DefaultValue = price.Text;
            SqlDataSource1.UpdateParameters["Product_Price2"].DefaultValue = price2.Text;
            SqlDataSource1.UpdateParameters["Product_Intro"].DefaultValue = intro.Text;
            SqlDataSource1.UpdateParameters["Product_Status"].DefaultValue = status.SelectedValue;
            SqlDataSource1.UpdateParameters["Product_ID"].DefaultValue = proID.Text;

            SqlDataSource1.Update();

            DataList1.EditItemIndex = -1;
            DataBind();

        }
    }
}