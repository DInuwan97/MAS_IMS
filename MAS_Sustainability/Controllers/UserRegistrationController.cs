using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;


namespace MAS_Sustainability.Controllers
{
    public class UserRegistrationController : Controller
    {
        // GET: UserRegistration
        public ActionResult Index()
        {
            return View();
        }

        // GET: UserRegistration/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserRegistration/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserRegistration/Create
        [HttpPost]
        public ActionResult Create(UserRegistrationModel userRegistrationModel)
        {
            DB dbConn = new DB();

            // String connectionString = @"server=localhost;port=3306;user id=root;database=mas_isscs;password=ThirtyFirst9731@;";

            using (MySqlConnection mySqlCon = dbConn.DBConnection())
            {
                mySqlCon.Open();
                String qry = "INSERT INTO users(UserName,UserEmail,UserMobile,Password,ConfirmPassword,UserDepartment,UserType) VALUES(@UserName,@UserEmail,@UserMobile,@Password,@ConfirmPassword,@UserDepartment,@UserType)";
                MySqlCommand mySqlcmd = new MySqlCommand(qry, mySqlCon);

                mySqlcmd.Parameters.AddWithValue("@UserName", userRegistrationModel.UserFullName);
                mySqlcmd.Parameters.AddWithValue("@UserEmail", userRegistrationModel.UserEmail);
                mySqlcmd.Parameters.AddWithValue("@UserMobile", userRegistrationModel.UserMobile);
                mySqlcmd.Parameters.AddWithValue("@Password", userRegistrationModel.Password);
                mySqlcmd.Parameters.AddWithValue("@ConfirmPassword", userRegistrationModel.ConfirmPassword);
                mySqlcmd.Parameters.AddWithValue("@UserDepartment", userRegistrationModel.UserDepartment);
                mySqlcmd.Parameters.AddWithValue("@UserType", "Admin");

                mySqlcmd.ExecuteNonQuery();


            }

            return RedirectToAction("Create");
        }

        // GET: UserRegistration/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserRegistration/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: UserRegistration/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserRegistration/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
