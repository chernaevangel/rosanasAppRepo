using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContentApp.Models;
using System.Data.SqlClient;

namespace ContentApp.Controllers
{
    public class AccountController : Controller
    {

        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\source\repos\ContentApp\ContentApp\App_Data\Database1.mdf;Integrated Security=True");
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;

        // GET: Account
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Verify(Account acc)
        {
            conn.Open();
            com.Connection = conn;
            com.CommandText = "select * from Account where username='" +(Request["username"].ToString())+"' and password='" + (Request["password"].ToString()) + "'";
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                conn.Close();
                HttpCookie hc1 = new HttpCookie("username", (Request["username"].ToString()));
                Response.Cookies.Add(hc1);
                return View("Create");
                
            }
            else
            {
                conn.Close();
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult LogOut()
        {
            if (Request.Cookies["username"] != null)
            {
                Response.Cookies["username"].Expires = DateTime.Now.AddDays(-1);
            }
            return View("Login");
        }
    }
}