using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;


namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://10.10.3.189");

            HttpResponseMessage resp = client.GetAsync("http://10.10.3.189/api/student").Result;
            IEnumerable<Student> data = null;

            data = resp.Content.ReadAsAsync<IEnumerable<Student>>().Result;
            foreach(var item in data)
            {
                listBox1.Items.Add("學號:"+item.學號+"姓名:"+item.姓名+"電話:"+item.電話+"性別:"+item.性別+"生日:"+item.生日);
            }



        }
        public class Student
        {
            public string 學號 { get; set; }
            public string 姓名 { get; set; }
            public string 電話 { get; set; }
            public string 性別 { get; set; }
            public string 生日 { get; set; }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }
    }
}
