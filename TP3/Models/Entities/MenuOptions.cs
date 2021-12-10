using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SystemEntities
{
    public class MenuOptions
    {
        private string url;
        private string name;
        private string Control;
        private int userType;

        public string Url { get => url; set => url = value; }
        public string Name { get => name; set => name = value; }
        public int UserType { get => userType; set => userType = value; }
        public string Control1 { get => Control; set => Control = value; }
    }
}
