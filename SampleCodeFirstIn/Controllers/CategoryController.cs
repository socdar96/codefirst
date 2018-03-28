using SampleCodeFirstIn.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SampleCodeFirstIn.Controllers
{
    public class CategoryController : Controller
    {
        InviContext db = new InviContext();
        // GET: Category
        public ActionResult Index()
        {
            return View();
        }
    }
}