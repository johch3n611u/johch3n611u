using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.SqlClient;
using System.Configuration;
using System.Net.Mail;

namespace ASPnet
{
    public partial class _33Member_Registeration : System.Web.UI.Page
    {
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MySystemConnectionString1"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MySystemConnectionString1"].ConnectionString);
                SqlCommand Cmd = new SqlCommand("select * from Edu order by EduLevel_Code desc", Conn);

                SqlDataReader rd;
                Conn.Open();
                rd = Cmd.ExecuteReader();
                ListItem item;
                while (rd.Read())
                {
                    item = new ListItem(rd["EduLevel"].ToString(), rd["EduLevel_Code"].ToString());
                    ddlEduLevel.Items.Add(item);
                }

                Conn.Close();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (this.IsValid)
            {
                  try
                  {
                        lblPhoto.Text = "";
                        SqlCommand Cmd = new SqlCommand("insert into members values(@account,hashbytes('sha2_256',@pwd),@name,@birthday,@email,@gender,@edu,@note,@photo,@IsAuth)", Conn);
                        Cmd.Parameters.AddWithValue("@account", txtAccount.Text);
                        Cmd.Parameters.AddWithValue("@pwd", txtPwd.Text);
                        Cmd.Parameters.AddWithValue("@name", txtName.Text);
                        Cmd.Parameters.AddWithValue("@birthday", txtBirthday.Text);
                        Cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                        Cmd.Parameters.AddWithValue("@gender", rblGender.SelectedValue);
                        Cmd.Parameters.AddWithValue("@edu", ddlEduLevel.SelectedValue);
                        Cmd.Parameters.AddWithValue("@note", txtNote.Text);
                        Cmd.Parameters.AddWithValue("@photo", fulPhoto.FileBytes);
                        Cmd.Parameters.AddWithValue("@IsAuth", false);

                    Conn.Open();

                    if (fulPhoto.PostedFile.ContentType != "application/octet-stream")
                    {
                        if (fulPhoto.PostedFile.ContentType == "image/jpeg")
                        {
                            Cmd.ExecuteNonQuery();
                            SendAuthMail(txtEmail.Text,txtAccount.Text);
                        }
                        else
                            lblPhoto.Text = "格式有錯!!";
                    }
                    else
                    {
                        Cmd.ExecuteNonQuery();
                        SendAuthMail(txtEmail.Text, txtAccount.Text);
                    }
                     Conn.Close();

                        

                        //Response.Redirect("17GridView_DataSource.aspx");
                    }
                    catch (Exception ex)
                    {
                        Response.Write(ex.Message);
                    }
                }
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            SqlCommand Cmd = new SqlCommand("select dbo.fnCheckMemberAccount(@account)", Conn);
            Cmd.Parameters.AddWithValue("@account", txtAccount.Text);

            SqlDataReader rd;
            Conn.Open();
            rd = Cmd.ExecuteReader();

            rd.Read();
            if (rd[0].ToString() == "0")
                args.IsValid = true;
            else
                args.IsValid = false;

            Conn.Close();
        }

        protected void SendAuthMail(string mail, string account)
        {
            SmtpClient myMail = new SmtpClient("msa.hinet.net");
            //SmtpClient myMail = new SmtpClient("smtp.gmail.com", 587);
            //myMail.Credentials = new NetworkCredential("YourAccount", "YourPassword");
            //myMail.EnableSsl = true;

            //純文字
            //string from = "jhliao0408@gmail.com";
            //string to = "snlffsnlff94@gmail.com";
            //string subject = "邀請您一同來參加我們的派對!!";
            //string body = "ghhghghgkhgjjhghjrfdhg\nghfgdfdsgdhgjhgr\njhhgrfgfjhgj";
            //myMail.Send(from, to, subject, body);

            MailAddress from = new MailAddress("jhliao0408@gmail.com", "杯具商城");
            MailAddress to = new MailAddress(mail);
            MailMessage Msg = new MailMessage(from, to);
            Msg.Subject = "會員註冊認證信";
            Msg.Body = "請點擊下列超鏈結完成會員註冊認證<br /><br /><a href='http://localhost:64120/back_end_c50108/35Auth_OK.aspx?account=" + account+"'>請點我</a>";
            Msg.IsBodyHtml = true;

            myMail.Send(Msg);

            Response.Write("<script>alert('恭禧您完成會員註冊資料填寫，請至您的信箱收取認證信進行認證，方能啟用會員！') </script>");
        }

    }
}



//create function fnCheckMemberAccount
//    (@account varchar(10))
//	returns int
//as
//begin
//    declare @aa varchar(10)

//    select @aa = account from Members where account = @account
	
//	if @@ROWCOUNT=0
//		return 0

//	return 1
//end