using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Net;

namespace ASPnet
{
    public partial class _34Smtp_Client : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //SmtpClient myMail = new SmtpClient("msa.hinet.net");
            SmtpClient myMail = new SmtpClient("smtp.gmail.com",587);
            myMail.Credentials = new NetworkCredential("YourAccount","YourPassword");
            myMail.EnableSsl = true;

            //純文字
            //string from = "jhliao0408@gmail.com";
            //string to = "snlffsnlff94@gmail.com";
            //string subject = "邀請您一同來參加我們的派對!!";
            //string body = "ghhghghgkhgjjhghjrfdhg\nghfgdfdsgdhgjhgr\njhhgrfgfjhgj";
            //myMail.Send(from, to, subject, body);

            MailAddress from = new MailAddress("jhliao0408@gmail.com", "億載金城武");
            MailAddress to = new MailAddress("snlffsnlff94@gmail.com");
            MailMessage Msg = new MailMessage(from, to);
            Msg.Subject = "邀請您一同來參加我們的派對!!";
            Msg.Body = "ghhghghgkhgjjhghjrfdhg\nghfgdfdsgdhgjhgr\njhhgrfgfjhgj<img src='' />";
            Msg.IsBodyHtml = true;

            myMail.Send(Msg);
        }
    }
}