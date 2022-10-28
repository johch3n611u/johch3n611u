using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ASPnet.App_Code;

namespace ASPnet
{
    public partial class _39OO_Polymorphism : System.Web.UI.Page
    {
        Animal animal;
        IAnimalMove imove;
        IAnimalSpeak ispeak;
        Dog dog = new Dog();
        Cat cat = new Cat();
        Person person = new Person();
        Fish fish = new Fish();

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int flag = 1;

            if (RadioButtonList1.Items[0].Selected)
                animal = person;
            else if (RadioButtonList1.Items[2].Selected)
                animal = cat;


            if (RadioButtonList1.Items[1].Selected)
            {
                imove = dog;
                ispeak = dog;
                flag = 2;
            }
            else if (RadioButtonList1.Items[3].Selected)
            {
                imove = fish;
                flag = 3;
            }


            if (flag==1)
            {
                Label1.Text = animal.Speak();
                Label1.Text += animal.Move(5);
            }
            else if (flag == 2)
            {
                Label1.Text = ispeak.Speak();
                Label1.Text += imove.Move(8);
            }
            else if (flag == 3)
            {
                Label1.Text = imove.Move(8);
            }
        }

        
    }
}