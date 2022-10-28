using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPnet.App_Code
{
    public class Person : Animal
    {

        //field
        string name = "第一名";
        int age;
        bool gender;
        decimal height;
        decimal weight;

        public Person()
        {
        }
        public Person(string Name)
        {
            name = Name;
        }
        public Person(int Age)
        {
            age = Age;
        }
        public Person(string Name, int Age)
        {
            name = Name;
            age = Age;
        }
        public Person(string Name, int Age, bool Gender, decimal Height, decimal Weight)
        {
            name = Name;
            age = Age;
            gender = Gender;
            height = Height;
            weight = Weight;



        }
        //Attibute(屬性)
        public string Name
        {
            get
            {
                if(string.IsNullOrEmpty(name))
                    name = "第一名";

                return name;
            }
            set
            {
                if(!string.IsNullOrEmpty(value))
                    name = value;
            }
        }

        public int Age
        {
            get
            {
               
                return age;
            }
            set
            {
                if (value < 0)
                    age = 0;
                else
                    age = value;
            }
        }

        public decimal Height
        {
            get
            {

                return height;
            }
            set
            {
                if (value < 0)
                    height = 5;
                else
                    height = value;
            }
        }

        public decimal Weight
        {
            get
            {

                return weight;
            }
            set
            {
                if (value < 0)
                    weight = 1;
                else
                    weight = value;
            }
        }

        public bool Gender
        {
            get { return gender; }
            set { gender = value; }
        }


        //方法
        //說話(Speak())方法
        /// <summary>
        /// 請傳入要說的話
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public string Speak(string content)
        {

            return name + "講了「" + content + "」";
        }

        public override string Speak()
        {

            return name + "哈哈哈";
        }

        /// <summary>
        /// 跳了一跳
        /// </summary>
        /// <returns></returns>
        public string Jump()
        {

            return name + "嚇了一跳!!";
        }

        /// <summary>
        /// 回應跳了幾公尺
        /// </summary>
        /// <param name="h"></param>
        /// <returns></returns>
        public string Jump(int h)
        {

            return name + "跳了"+h+"公尺高!!";
        }

        public string Jump(string h)
        {

            return name + "跳了" + h + "公尺高!!";
        }

        public string Jump(int h, int w)
        {

            return name + "跳了" + h + "公尺高,"+w+"公尺遠!!";
        }
    }
}