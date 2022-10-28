using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPnet.App_Code
{
    public class Cat:Animal
    {
        public override string Speak()
        {
            return "喵喵喵";
        }
    }
}