using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using System.Data;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private MyConn db = new MyConn();

        // GET: Phones
        // after login success and all phones will be displayed
        public ActionResult Index()
        {
            if (Session["username"] != null)
            {
                var products = db.phones.Where(x => x.quantity > 0).ToList();

                return View(products);
            }
            else
                {
                    return Redirect("/User");
                }
        }


        //click log out button to sign out account
        [HttpGet]
        public ActionResult Logout()
        {
            Session["username"] = null;
            return Redirect("/User");
        }
    }
}