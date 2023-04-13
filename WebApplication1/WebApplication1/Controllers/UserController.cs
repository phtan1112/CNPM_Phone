using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }



        //the button submit input username and password and check account agent that has exist in database or not
        [HttpPost]
        public ActionResult Index(LoginClass lc)
        {
            using (SqlConnection DevConn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DevConn"].ConnectionString))
            {
                String querySQL = "select id,agent_name,address,phone_number from agent where username =@username and password = @password";

                DevConn.Open();
                SqlCommand cmd = new SqlCommand(querySQL, DevConn);
                cmd.Parameters.Clear();
                Debug.WriteLine("My debug string here");
                cmd.Parameters.AddWithValue("@username", lc.Username);
                cmd.Parameters.AddWithValue("@password", lc.Password);
                SqlDataReader sdr = cmd.ExecuteReader();

                if (sdr.Read())
                {
                    Session["agentID"] = sdr["id"].ToString();
                    Session["agent_name"] = (String)sdr["agent_name"];
                    Session["address"] = (String)sdr["address"];
                    Session["phoneNumber"] = (String)sdr["phone_number"];
                    Session["username"] = lc.Username.ToString();
                    return Redirect("/Home");
                }
                else
                {
                    ViewData["Message"] = "User Login Detail Failed";
                }

                DevConn.Close();
                return View();
            }

        }
    }
}