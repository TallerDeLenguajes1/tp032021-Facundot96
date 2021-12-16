using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tp03.Models.Entities
{
    public class MenuOptions
    {
        private string url;
        private string name;
        private string controller;
        private int type;

        public string Url { get => url; set => url = value; }
        public string Name { get => name; set => name = value; }
        public string Controller { get => controller; set => controller = value; }
        public int Type { get => type; set => type = value; }
    }
}
