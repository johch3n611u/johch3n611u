using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPnet.App_Code
{
    public class SuperMan:Person
    {
        string cloakColor;

        public string CloakColor
        {
            get
            {
                return cloakColor;
            }
            set
            {
                cloakColor = value;
            }
        }

        public string Fly()
        {
            return "I can fly heigh!!";
        }

        public string Fly(int h)
        {
            return "我飛了" + h + "公尺高";
        }
    }
}