using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPnet.App_Code
{
    public class Dog:IAnimalSpeak, IAnimalMove
    {
        public string Speak()
        {
            return "汪汪汪";
        }
        public string Move(int m)
        {
            return "移動了" + m + "公尺";
        }
    }
}