using Bangom.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bangom.Areas.Admin.Controllers
{
    public class AdminHomeController : Controller
    {
        BangomEntities db = new BangomEntities();
        // GET: Admin/AdminHome
        public ActionResult Index()
        {
            return View();
        }

        
    }
}