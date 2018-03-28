﻿using SampleCodeFirstIn.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SampleCodeFirstIn.Context
{
    public class InviContext : DbContext
    {
        public  DbSet<Category> Categories { get; set; }
        public  DbSet<Product> Product { get; set; }
        public DbSet<User> Users { get; set; }
    }
}