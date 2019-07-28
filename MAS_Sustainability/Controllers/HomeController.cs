using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using System.Data;
using MAS_Sustainability.Models;

namespace MAS_Sustainability.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult LayoutBody()
        {

            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "UserLogin");
            }
            else
            {

                //List<Token> List_Token = new List<Token>();
                //List<UserLogin> List_UserLogin = new List<UserLogin>();

                
                DB dbConn = new DB();
                DataTable userDetailsDataTable = new DataTable();
                UserLogin userLogin = new UserLogin();
                Token tokenModel = new Token();
                using (MySqlConnection mySqlCon = dbConn.DBConnection())
                {
                    mySqlCon.Open();
                    MySqlCommand mySqlCmd = mySqlCon.CreateCommand();
                    mySqlCmd.CommandType = System.Data.CommandType.Text;
                    String qry = "SELECT UserName FROM users WHERE UserEmail = '" + Session["user"] + "'";
                    MySqlDataAdapter MySqlDA = new MySqlDataAdapter(qry, mySqlCon);
                    MySqlDA.Fill(userDetailsDataTable);
                }


                    if (userDetailsDataTable.Rows.Count == 1)
                    {
                        
                       // finalItem.ListToken = List_Token;
                       // finalItem.ListUserLogin = List_UserLogin;
                       userLogin.LoggedUserNameSide = userDetailsDataTable.Rows[0][0].ToString(); 

                        // ViewBag.LoginUserVariables = tokenModel;

                        return View(userLogin);
                    }
                    else
                    {
                        return RedirectToAction("index");
                    }


                
            }
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            //ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            //ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}