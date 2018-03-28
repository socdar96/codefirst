namespace SampleCodeFirstIn.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Product
    {
        public int ProductID { get; set; }

        public string ProdName { get; set; }

        public int CategID { get; set; }

        public Category Category { get; set; }
    }
}
