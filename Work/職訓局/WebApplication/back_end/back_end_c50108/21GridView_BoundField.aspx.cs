using MessagingToolkit.QRCode.Codec;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASPnet
{
    public partial class _21GridView_BoundField : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowIndex != -1)
            {
                QRCodeEncoder encoder = new QRCodeEncoder();


                string ProductID = e.Row.Cells[0].Text;

                Bitmap img = encoder.Encode(ProductID);

                img.Save(Server.MapPath("/QR_Code/"+ ProductID+".jpg"), ImageFormat.Jpeg);

                ((System.Web.UI.WebControls.Image)e.Row.Cells[7].FindControl("Image1")).ImageUrl = "/QR_Code/" + ProductID + ".jpg";
            }
        }
    }
}