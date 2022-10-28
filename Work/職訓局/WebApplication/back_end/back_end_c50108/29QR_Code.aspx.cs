using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MessagingToolkit.QRCode.Codec;
using System.Data.SqlClient;
using System.Configuration;

namespace ASPnet
{
    public partial class _29QR_Code : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            QRCodeEncoder encoder = new QRCodeEncoder();
            encoder.QRCodeVersion = 3;
            encoder.QRCodeScale = 20;

            string ProductID = "ufkjud8979302ikld";

            Bitmap img = encoder.Encode(ProductID);

            img.Save(Server.MapPath("/QR_Code/MyCdoe.jpg"), ImageFormat.Jpeg);

            Image1.ImageUrl = "/QR_Code/MyCdoe.jpg";
        }

        protected void Button1_Click(object sender, EventArgs e)
        {



            QRCodeEncoder encoder = new QRCodeEncoder();
            encoder.QRCodeVersion = 3;
            encoder.QRCodeScale = 10;
            string ProductID = "";

            SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MySystemConnectionString1"].ConnectionString);
            SqlCommand Cmd = new SqlCommand("select product_id from products", Conn);

            SqlDataReader rd;
            Conn.Open();
            rd = Cmd.ExecuteReader();
            while (rd.Read())
            {
                ProductID = rd["product_id"].ToString();
                Bitmap img = encoder.Encode(ProductID);

                img.Save(Server.MapPath("/QR_Code/"+ ProductID + ".jpg"), ImageFormat.Jpeg);

                System.Web.UI.WebControls.Image image = new System.Web.UI.WebControls.Image();
                image.ImageUrl = "/QR_Code/"+ ProductID+".jpg";
                form1.Controls.Add(image);

            }

            Conn.Close();
        }
    }
}