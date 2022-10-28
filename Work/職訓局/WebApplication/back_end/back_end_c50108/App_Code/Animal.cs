using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPnet.App_Code
{
    public abstract class Animal //抽象類別
    {
        public abstract string Speak();  //抽象方法


        public string Move(int m) //一般方法
        {
            return "移動了"+m+"公尺";
        }
    }
}