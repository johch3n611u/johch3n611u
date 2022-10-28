using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASPnet
{
    public partial class _15WebForm_Validation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            ranForBirthday.MaximumValue = DateTime.Now.ToString("d");
        }

        protected void btnAll_Click(object sender, EventArgs e)
        {
            for(int i=0;i<ltbInterest.Items.Count;i++)
            {
                ltbInterest2.Items.Add(ltbInterest.Items[i].Text);
            }
            ltbInterest.Items.Clear();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ltbInterest2.Items.Count; i++)
            {
                ltbInterest.Items.Add(ltbInterest2.Items[i].Text);
            }
            ltbInterest2.Items.Clear();
        }

        protected void btnYes_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ltbInterest.Items.Count; i++)
            {
                if (ltbInterest.Items[i].Selected)
                {
                    ltbInterest2.Items.Add(ltbInterest.Items[i].Text);
                    ltbInterest.Items.RemoveAt(i);
                }
            }
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ltbInterest2.Items.Count; i++)
            {
                if (ltbInterest2.Items[i].Selected)
                {
                    ltbInterest.Items.Add(ltbInterest2.Items[i].Text);
                    ltbInterest2.Items.RemoveAt(i);
                }
            }
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            string id = txtID.Text;

            string[] eng = {"A", "B", "C", "D", "E", "F", "G", "H", "J", "K",
                "L", "M", "N", "P", "Q", "R", "S", "T", "U", "V", "X", "Y", "W",
                "Z", "I", "O" };

            int intEng = 0;

            for(int i=0;i<eng.Length;i++)
            {
                if(eng[i]==id.Substring(0,1).ToUpper())
                {
                    intEng = i + 10;
                    break;
                }

            }

            //假設n=17
            int n1 = intEng / 10;  //n1=1
            int n2 = intEng % 10;  //n2=7



            int[] a = new int[9];
            for (int i = 0; i < a.Length; i++)
            {
                a[i] = Convert.ToInt16(id.Substring(i + 1, 1));

            }

            int sum = 0;
            for (int i = 0; i < 8; i++)
            {

                sum += a[i] * (8 - i);
            }

            int n = 0;
            n=n1 + n2 * 9 + sum + a[8];

            //n1 +n2*9+a[0]*8+a[1]*7*a[2]*6+a[3]*5

            
            if (n % 10 == 0)
                args.IsValid = true;
            else
                args.IsValid = false;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (this.IsValid)
            {
                Response.Write("表單已成功送到伺服器");
            }
        }

        protected void CustomValidator2_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (ltbInterest2.Items.Count < 3)
                args.IsValid = false;
            else
                args.IsValid = true;
        }
    }
}