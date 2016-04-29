using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using TAMVC.Models;

namespace TAMVC.DAL
{
    public class SQLHelper
    {
        string connectionstring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();

        public bool Login(string email, string password)
        {
           
            bool res = false;
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                using (SqlCommand cmd = new SqlCommand("usp_User_checklogin", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = email;
                    cmd.Parameters.Add("@UPassword", SqlDbType.VarChar).Value = password;

                    con.Open();
                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    if (dt.Rows.Count > 0)
                    {
                        User u = new User();
                        u.UserId = dt.Rows[0]["UserId"].ToString();
                        u.Email = dt.Rows[0]["Email"].ToString();
                        u.First_Name = dt.Rows[0]["First Name"].ToString();
                        u.Last_Name = dt.Rows[0]["Last Name"].ToString();
                        u.Gender = dt.Rows[0]["Gender"].ToString();
                        u.DOB = dt.Rows[0]["DOB"].ToString();
                        u.Address1 = dt.Rows[0]["Address1"].ToString();
                        u.Address2 = dt.Rows[0]["Address2"].ToString();
                        u.City = dt.Rows[0]["City"].ToString();
                        u.State = dt.Rows[0]["State"].ToString();
                        u.Country = dt.Rows[0]["Country"].ToString();
                        u.Phone = dt.Rows[0]["Phone"].ToString();
                        u.IsVerified = dt.Rows[0]["IsVerified"].ToString();
                        u.IsLocked = dt.Rows[0]["IsLocked"].ToString();
                        u.IsActive = dt.Rows[0]["IsActive"].ToString();
                        u.CreatedDate = dt.Rows[0]["CreatedDate"].ToString();
                        HttpContext.Current.Session["user"] = u;
                        return true;
                    }
                }

               
            }
            return res;
        }
        public bool registerUser(string email,string password)
        {
            bool res = false;
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                using (SqlCommand cmd = new SqlCommand("usp_User_new_registration", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = email;
                    cmd.Parameters.Add("@UPassword", SqlDbType.VarChar).Value = password;

                    con.Open();
                   int i= cmd.ExecuteNonQuery();
                    if(i==1)
                    {
                        res= true;
                    }

                   
                }
            }
            return res;
        }

        public string  addClassified(Classified c)
        {
            string res = "Added Successfully";
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                using (SqlCommand cmd = new SqlCommand("usp_Classified_Add", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ClassifiedTitle", SqlDbType.VarChar).Value = c.ClassifiedTitle;
                    cmd.Parameters.Add("@Summary", SqlDbType.VarChar).Value = c.Summary;
                    cmd.Parameters.Add("@Description", SqlDbType.VarChar).Value = c.Description;
                    cmd.Parameters.Add("@ClassifiedImage", SqlDbType.VarChar).Value = c.ClassifiedImage;
                    cmd.Parameters.Add("@ClassifiedPrice", SqlDbType.Int).Value = Convert.ToInt32(c.ClassifiedPrice);
                    cmd.Parameters.Add("@PostedDate", SqlDbType.DateTime).Value = Convert.ToDateTime( c.PostedDate);
                    cmd.Parameters.Add("@CreatedBy", SqlDbType.UniqueIdentifier).Value = new Guid (c.CreatedBy);
                    cmd.Parameters.Add("@CategoryName", SqlDbType.VarChar).Value = c.CategoryName;
                    
                    con.Open();
                    try
                    {
                        cmd.ExecuteNonQuery();

                    }
                    catch(Exception e)
                    {
                        res = e.Message;
                    }
                   
                }
            }
            return res;
        }


       
       

        
    }
}