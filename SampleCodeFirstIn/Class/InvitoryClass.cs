using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SampleCodeFirstIn.Class
{

    public class Categories
    {
        public int CategID { get; set; }
        public string CategName { get; set; }
    }
    public class Product
    {
        public int ProductID { get; set; }
        public string ProdName { get; set; }
        public int CategID { get; set; }
    }
}