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
    public class UserManagementController : Controller
    {
        // GET: UserManagement
        public ActionResult Edit(int id)
        {
            List<UserRegistrationModel> List_UserRegistration = new List<UserRegistrationModel>();
            MainModel mainModel = new MainModel();
            DataTable userDetailsDataTable = new DataTable();

            DB dbConn = new DB();

    
            using (MySqlConnection mySqlCon = dbConn.DBConnection())
            {
                mySqlCon.Open();
                String qry_listOfTokens = "SELECT UserName,UserType,UserID,UserEmail,UserMobile,UserDepartment FROM users WHERE UserEmail = '" + Session["user"] + "'";
                MySqlDataAdapter mySqlDa = new MySqlDataAdapter(qry_listOfTokens, mySqlCon);
                mySqlDa.Fill(userDetailsDataTable);


            }

            for (int i = 0; i < userDetailsDataTable.Rows.Count; i++)
            {

                List_UserRegistration.Add(new UserRegistrationModel
                {
                    UserFullName = userDetailsDataTable.Rows[0][0].ToString(),
                    UserType = userDetailsDataTable.Rows[0][1].ToString(),
                    UserID = Convert.ToInt32(userDetailsDataTable.Rows[0][2]),
                    UserEmail = userDetailsDataTable.Rows[0][3].ToString(),
                    UserMobile = userDetailsDataTable.Rows[0][4].ToString(),
                    UserDepartment = userDetailsDataTable.Rows[0][5].ToString()

                }
                );

            }


            if (userDetailsDataTable.Rows.Count == 1)
            {
                mainModel.LoggedUserName = userDetailsDataTable.Rows[0][0].ToString();
                mainModel.LoggedUserType = userDetailsDataTable.Rows[0][1].ToString();
                mainModel.LoggedUserID = Convert.ToInt32(userDetailsDataTable.Rows[0][2]);

                mainModel.ListUserRegistration = List_UserRegistration;

                return View(mainModel);
            }
            else
            {
                return View("Index");
            }



            
        }



        public ActionResult Index()
        {
            DataTable userDetailsDataTable = new DataTable();
            MainModel mainModel = new MainModel();
            DB dbConn = new DB();

            using (MySqlConnection mySqlCon = dbConn.DBConnection())
            {
                mySqlCon.Open();
                String qry_listOfTokens = "SELECT UserName,UserType,UserID,UserEmail,UserMobile,UserDepartment FROM users WHERE UserEmail = '" + Session["user"] + "'";
                MySqlDataAdapter mySqlDa = new MySqlDataAdapter(qry_listOfTokens, mySqlCon);
                mySqlDa.Fill(userDetailsDataTable);

            }

            if (userDetailsDataTable.Rows[0][1].ToString() != ("Administrator"))
            {
                return RedirectToAction("Index", "Dashbord");
            }



            if (userDetailsDataTable.Rows.Count == 1)
            {
                mainModel.LoggedUserName = userDetailsDataTable.Rows[0][0].ToString();
                mainModel.LoggedUserType = userDetailsDataTable.Rows[0][1].ToString();
                mainModel.LoggedUserID = Convert.ToInt32(userDetailsDataTable.Rows[0][2]);
                return View(mainModel);
            }
         
            else
            {
                return View()
;            }
        }



    }
}