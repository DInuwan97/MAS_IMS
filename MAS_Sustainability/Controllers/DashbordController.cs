using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using System.Data;
using System.IO;

namespace MAS_Sustainability.Controllers
{
    public class DashbordController : Controller
    {
        public String SessionEmail = null;
        // GET: Dashbord
        public ActionResult Index()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "UserLogin");
            }
            else
            {
                DB dbConn = new DB();
                DataTable userDetailsDataTable = new DataTable();
                UserLogin userLogin = new UserLogin();
                using (MySqlConnection mySqlCon = dbConn.DBConnection())
                {
                    mySqlCon.Open();               
                    String qry_listOfTokens = "SELECT UserName FROM users WHERE UserEmail = '" + Session["user"] + "'";
                    MySqlDataAdapter mySqlDa = new MySqlDataAdapter(qry_listOfTokens, mySqlCon);
                    mySqlDa.Fill(userDetailsDataTable);

                    if (userDetailsDataTable.Rows.Count == 1) {
                        userLogin.LoggedUserNameSide = userDetailsDataTable.Rows[0][0].ToString();
                        ViewBag.LoginUserVariables = userLogin;
                        return View();
                    }
                    else
                    {
                        return RedirectToAction("index");
                    }


                }
            }
           
        }

        public String getSessionEmail()
        {
            return SessionEmail;
        }
    }
}