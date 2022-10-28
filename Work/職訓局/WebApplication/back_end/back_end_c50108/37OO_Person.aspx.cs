using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ASPnet.App_Code;

namespace ASPnet
{
    public partial class _37OO_Person : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Random r = new Random();

            TextBox tb = new TextBox();
            ListItem item = new ListItem();
            item.Text = "kkkk";
            item.Value = "ttt";

            ListItem item2 = new ListItem("aaaa");
  
            //TextBox tb2 = new TextBox();
            //TextBox tb3 = new TextBox();
            //TextBox tb4 = new TextBox();
            //TextBox tb5 = new TextBox();

            Person Mary = new Person();
            //Response.Write(Mary.Name);
            Response.Write(Mary.Jump());
            Response.Write(Mary.Jump(5));
            Response.Write(Mary.Jump("5"));
            Response.Write(Mary.Jump(5,2));


            Person Jason = new Person();
            Jason.Name = "Jason Lee";
            Jason.Age = -18;

            string speak= Jason.Speak("我是一個大帥哥!!");

            Response.Write(speak);

            ////Jason.gender = true;
            ////Jason.height = 172;
            //Jason.weight = 68.5M;
            ////Jason.name = "Jason Lee";


            Person Josh = new Person("Josh Lai",26);

            Person May = new Person("May Chen", 20, false, 180, 70);

            May.Age = 25;
            Response.Write(May.Age);
        }
    }
}