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
    public class TokenController : Controller
    {
        [HttpGet]
        // GET: Token
        public ActionResult Index()
        {
            if(Session["user"] == null)
            {
                return RedirectToAction("Login", "UserLogin");
            }

          //  List<Token> List_Token1 = new List<Token>();
            ///List<UserLogin> List_UserLogin1 = new List<UserLogin>();

            
            //finalItem.ListToken = List_Token1;
            //finalItem.ListUserLogin = List_UserLogin1;



            DB dbConn = new DB();
            DataTable dtblTokens = new DataTable();
            Token tokenModel = new Token();
            using (MySqlConnection mySqlCon = dbConn.DBConnection())
            {
                mySqlCon.Open();
                string qry = "SELECT tka.TokenAuditID,tk.ProblemName,tk.Location,tk.AttentionLevel,usr.UserName,tkFlow.TokenManagerStatus FROM users usr,tokens tk, token_audit tka,token_flow tkFlow WHERE tk.TokenAuditID = tka.TokenAuditID AND tka.AddedUser = usr.UserEmail AND tk.TokenAuditID = tkFlow.TokenAuditID";  
                MySqlDataAdapter mySqlDA = new MySqlDataAdapter(qry,mySqlCon);
                mySqlDA.Fill(dtblTokens);

                

                tokenModel.ArrTokenAuditID = new int[50];
                tokenModel.ArrProblemName = new string[250];
                tokenModel.ArrLocation = new string[100];
                tokenModel.ArrAttentionLevel = new int[200000];
                tokenModel.ArrFirstImagePath = new string[500];
                tokenModel.ArrUserName = new string[100];
                tokenModel.ArrTokenStatus = new string[50];

                tokenModel.no_of_tokens = Convert.ToInt32(dtblTokens.Rows.Count);

            }

            for (int i = 0; i < dtblTokens.Rows.Count; i++)
            {
                tokenModel.ArrTokenAuditID[i] = Convert.ToInt32(dtblTokens.Rows[i][0]);
                tokenModel.ArrProblemName[i] = dtblTokens.Rows[i][1].ToString();
                tokenModel.ArrLocation[i] = dtblTokens.Rows[i][2].ToString();
                tokenModel.ArrAttentionLevel[i] = Convert.ToInt32(dtblTokens.Rows[i][3]);
                tokenModel.ArrUserName[i] = dtblTokens.Rows[i][4].ToString();
                tokenModel.ArrTokenStatus[i] = dtblTokens.Rows[i][5].ToString();

            }

          

            ViewBag.TokenList = tokenModel;

            return View(tokenModel);
        }

        // GET: Token/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Token/Create
        public ActionResult Add()
        {
            return View();
        }

        // POST: Token/Create
        [HttpPost]
        public ActionResult Add(Token tokenModel)
        {

            //Image 01
            string first_name_of_file = Path.GetFileNameWithoutExtension(tokenModel.FirstImageFile.FileName);
            string extension1 = Path.GetExtension(tokenModel.FirstImageFile.FileName);
            first_name_of_file = first_name_of_file + DateTime.Now.ToString("yymmssfff") +extension1;
            tokenModel.FirstImagePath = "~/Image/" +first_name_of_file;
            first_name_of_file = Path.Combine(Server.MapPath("~/Image/"), first_name_of_file);
            tokenModel.FirstImageFile.SaveAs(first_name_of_file);

            String imgPath1 = tokenModel.FirstImagePath;



            //Image 02
            string second_name_of_file = Path.GetFileNameWithoutExtension(tokenModel.SecondImageFile.FileName);
            string extension2 = Path.GetExtension(tokenModel.SecondImageFile.FileName);
            second_name_of_file = second_name_of_file + DateTime.Now.ToString("yymmssfff") + extension2;
            tokenModel.SecondImagePath = "~/Image/" + second_name_of_file;
            second_name_of_file = Path.Combine(Server.MapPath("~/Image/"), second_name_of_file);
            tokenModel.SecondImageFile.SaveAs(second_name_of_file);

            String imgPath2 = tokenModel.SecondImagePath;




            String AddedUser = Session["user"].ToString();

            DB dbConn = new DB();
            using (MySqlConnection mySqlCon = dbConn.DBConnection())
            {

                mySqlCon.Open();
                // String qry = "INSERT INTO token_audit(AddedUser,Category,AddedDate)VALUES(@AddedUser,@Category,NOW())";

                MySqlCommand mySqlCmd_TokenAudit = new MySqlCommand("Proc_Store_TokenAudit", mySqlCon);
                mySqlCmd_TokenAudit.CommandType = CommandType.StoredProcedure;
                mySqlCmd_TokenAudit.Parameters.AddWithValue("_Category", tokenModel.ProblemCategory);
                mySqlCmd_TokenAudit.Parameters.AddWithValue("_AddedUser", AddedUser);


                mySqlCmd_TokenAudit.ExecuteNonQuery();


                String last_audit_id_qry = "SELECT TokenAuditID FROM token_audit ORDER BY TokenAuditID DESC LIMIT 1";
                MySqlDataAdapter mySqlDA = new MySqlDataAdapter(last_audit_id_qry, mySqlCon);
                DataTable dt = new DataTable();
                mySqlDA.Fill(dt);

                int last_audit_id_num = Convert.ToInt32(dt.Rows[0][0]);

                string qry = "INSERT INTO tokens(TokenAuditID,ProblemName,Location,AttentionLevel,Description) VALUES(@TokenAuditID,@ProblemName,@Location,@AttentionLevel,@Description)";

                MySqlCommand mySqlCmd_tokenInfo = new MySqlCommand(qry, mySqlCon);
                mySqlCmd_tokenInfo.Parameters.AddWithValue("@TokenAuditID", last_audit_id_num);
                mySqlCmd_tokenInfo.Parameters.AddWithValue("@ProblemName", tokenModel.ProblemName);
                mySqlCmd_tokenInfo.Parameters.AddWithValue("@Location", tokenModel.Location);
                mySqlCmd_tokenInfo.Parameters.AddWithValue("@AttentionLevel", tokenModel.AttentionLevel);
                mySqlCmd_tokenInfo.Parameters.AddWithValue("@Description", tokenModel.Description);
                mySqlCmd_tokenInfo.ExecuteNonQuery();


                MySqlCommand mySqlCmd_ImageUpload = new MySqlCommand("Proc_Store_Images", mySqlCon);
                mySqlCmd_ImageUpload.CommandType = CommandType.StoredProcedure;
                mySqlCmd_ImageUpload.Parameters.AddWithValue("_TokenAuditID", last_audit_id_num);
                mySqlCmd_ImageUpload.Parameters.AddWithValue("_ImgPath1", imgPath1);
                mySqlCmd_ImageUpload.Parameters.AddWithValue("_ImgPath2", imgPath2);
                mySqlCmd_ImageUpload.ExecuteNonQuery();



                String qryTokenStatus = "INSERT INTO token_flow(TokenAuditID,TokenManagerStatus,DeptLeaderStatus,FinalVerification) values(@TokenAuditID,'Pending','Pending','Pending')";
                MySqlCommand mySqlCmd_tokenStatus = new MySqlCommand(qryTokenStatus,mySqlCon);
                mySqlCmd_tokenStatus.Parameters.AddWithValue("@TokenAuditID", last_audit_id_num);
                mySqlCmd_tokenStatus.ExecuteNonQuery();


            }
            // TODO: Add insert logic here
            return View();
        }

  

        // GET: Token/Edit/5
        public ActionResult View(int id)
        {
            Token tokenModel = new Token();

            Token tokenModel1 = new Token();
            DataTable dtblToken = new DataTable();
            DataTable dtblSideList = new DataTable();

            DataTable dtblTokenImage = new DataTable();

            DB dbConn = new DB();
            using (MySqlConnection mySqlCon = dbConn.DBConnection())
            {
                mySqlCon.Open();

                String qry = "SELECT tka.TokenAuditID,tk.ProblemName,tka.AddedDate,tk.Location,tk.AttentionLevel,usr.UserName,tki.ImagePath,tk.Description FROM users usr,tokens tk, token_audit tka,token_image tki WHERE tk.TokenAuditID = tka.TokenAuditID AND tka.AddedUser = usr.UserEmail AND tk.TokenAuditID = tki.TokenID AND tk.TokenAuditID = @TokenAuditID ";
                MySqlDataAdapter mySqlDa = new MySqlDataAdapter(qry, mySqlCon);
                mySqlDa.SelectCommand.Parameters.AddWithValue("@TokenAuditID", id);
                mySqlDa.Fill(dtblToken);

             

                String qry_side_token_list = "SELECT tka.TokenAuditID,tk.ProblemName,tk.Location,tk.AttentionLevel,tki.ImagePath FROM tokens tk, token_audit tka,token_image tki WHERE tk.TokenAuditID = tka.TokenAuditID  AND tk.TokenAuditID = tki.TokenID";
                MySqlDataAdapter mySqlDa_sideList = new MySqlDataAdapter(qry_side_token_list, mySqlCon);
                mySqlDa_sideList.Fill(dtblSideList);

                tokenModel.ArrTokenAuditID = new int[50];
                tokenModel.ArrProblemName = new string[250];
                tokenModel.ArrLocation = new string[100];
                tokenModel.ArrAttentionLevel = new int[200000];
                tokenModel.ArrFirstImagePath = new string[500];

                tokenModel.no_of_rows_side_bar = Convert.ToInt32(dtblSideList.Rows.Count);

            }

            for (int i = 0; i < dtblSideList.Rows.Count; i = i + 2)
            {
             
                tokenModel.ArrTokenAuditID[i] = Convert.ToInt32(dtblSideList.Rows[i][0]);
                tokenModel.ArrProblemName[i] = dtblSideList.Rows[i][1].ToString();
                tokenModel.ArrLocation[i] = dtblSideList.Rows[i][2].ToString();
                tokenModel.ArrAttentionLevel[i] = Convert.ToInt32(dtblSideList.Rows[i][3]);
                tokenModel.ArrFirstImagePath[i] = dtblSideList.Rows[i][4].ToString();
              
            }



            if (dtblToken.Rows.Count == 2)
            {
                tokenModel.TokenAuditID = Convert.ToInt32(dtblToken.Rows[0][0]);
                tokenModel.ProblemName = dtblToken.Rows[0][1].ToString();
                tokenModel.AddedDate = dtblToken.Rows[0][2].ToString();
                tokenModel.Location = dtblToken.Rows[0][3].ToString();
                tokenModel.AttentionLevel = Convert.ToInt32(dtblToken.Rows[0][4]);
                tokenModel.UserName = dtblToken.Rows[0][5].ToString();
                tokenModel.FirstImagePath = dtblToken.Rows[0][6].ToString();
                tokenModel.SecondImagePath = dtblToken.Rows[1][6].ToString();
                tokenModel.Description = dtblToken.Rows[0][7].ToString();

                ViewBag.TokenVariable = tokenModel;
                return View(tokenModel);

            }
            else
            {
                return RedirectToAction("index");
            }
         












        }

        // POST: Token/Edit/5
        [HttpPost]
        public ActionResult TokenForward(Token tokenModel)
        {
            DB dbConn = new DB();
            String ForwardUser = Session["user"].ToString();
            using (MySqlConnection mySqlCon = dbConn.DBConnection())
            {

                mySqlCon.Open();
                string qry = "INSERT INTO token_review(TokenAuditID,SpecialActs,RepairDepartment,SentDate,SentUser)VALUES(@TokenAuditID,@SpecialActs,@ReparationDepartment,NOW(),@SentUser)";
                MySqlCommand mySqlCmd_TokenFoward = new MySqlCommand(qry, mySqlCon);
                mySqlCmd_TokenFoward.Parameters.AddWithValue("@TokenAuditID", tokenModel.TokenAuditID);
                mySqlCmd_TokenFoward.Parameters.AddWithValue("@SpecialActs", "Urgent");
                mySqlCmd_TokenFoward.Parameters.AddWithValue("@ReparationDepartment", tokenModel.ReparationDepartment);
                mySqlCmd_TokenFoward.Parameters.AddWithValue("@SentUser",ForwardUser);
                mySqlCmd_TokenFoward.ExecuteNonQuery();

                String update_token_status = "UPDATE token_flow SET TokenManagerStatus = @ReparationDepartment WHERE TokenAuditID = @TokenAuditID";

                MySqlCommand mySqlCommand_update_token_status = new MySqlCommand(update_token_status,mySqlCon);
                mySqlCommand_update_token_status.Parameters.AddWithValue("@TokenAuditID", tokenModel.TokenAuditID);
                mySqlCommand_update_token_status.Parameters.AddWithValue("@ReparationDepartment", tokenModel.ReparationDepartment);
                mySqlCommand_update_token_status.ExecuteNonQuery();

                return RedirectToAction("index");

            }


        }

        // GET: Token/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Token/Delete/5
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



        public ActionResult Update(Token tokenModel)
        {
            DB dbConn = new DB();
            String ForwardUser = Session["user"].ToString();

            using (MySqlConnection mySqlCon = dbConn.DBConnection())
            {
                mySqlCon.Open();
                /*String qry_update_dept_token_flow = "UPDATE token_flow SET TokenManagerStatus = @ReparationDepartment WHERE TokenAuditID = @TokenAuditID";
                MySqlCommand mySqlCmd_update_dept_token_flow = new MySqlCommand(qry_update_dept_token_flow, mySqlCon);
                mySqlCmd_update_dept_token_flow.Parameters.AddWithValue("@ReparationDepartment", tokenModel.TokenAuditID);
                mySqlCmd_update_dept_token_flow.Parameters.AddWithValue("@TokenAuditID", tokenModel.ReparationDepartment);
                mySqlCmd_update_dept_token_flow.ExecuteNonQuery();
                */
                String update_token_status_token_flow = "UPDATE token_flow SET TokenManagerStatus = @ReparationDepartment WHERE TokenAuditID = @TokenAuditID";

                MySqlCommand mySqlCommand_update_token_status_token_flow = new MySqlCommand(update_token_status_token_flow, mySqlCon);
                mySqlCommand_update_token_status_token_flow.Parameters.AddWithValue("@TokenAuditID", tokenModel.TokenAuditID);
                mySqlCommand_update_token_status_token_flow.Parameters.AddWithValue("@ReparationDepartment", tokenModel.ReparationDepartment);
                mySqlCommand_update_token_status_token_flow.ExecuteNonQuery();



                String update_token_status_token_review = "UPDATE token_review SET RepairDepartment = @ReparationDepartment,SentDate = NOW() WHERE TokenAuditID = @TokenAuditID ";
                MySqlCommand mySqlCmd_update_token_status_token_review = new MySqlCommand(update_token_status_token_review,mySqlCon);
                mySqlCmd_update_token_status_token_review.Parameters.AddWithValue("@TokenAuditID", tokenModel.TokenAuditID);
                mySqlCmd_update_token_status_token_review.Parameters.AddWithValue("@ReparationDepartment", tokenModel.ReparationDepartment);
                mySqlCmd_update_token_status_token_review.ExecuteNonQuery();




                return RedirectToAction("Index");
            }



           
        }

   
    }
}
