using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;

namespace APIservice.Models
{
    public class EIPModel : ApiController
    {
        public bool LogIn(string username,string password)
        {
            bool result = false;
            DataSet ds = new DataSet();
            try
            {
                System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection();
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["DadabaseNameConnectionString"].ConnectionString;
                connection.Open();

                
                string strSQL = "SELECT * FROM (tabel Name) WHERE UserName = '"+username+"' AND Password = '"+password+"'";

                System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand();
                command.CommandText = strSQL;
                command.Connection = connection;
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                adapter.Fill(ds, "userExist");
                
                if (ds.Tables[0].Rows.Count > 0)
                    result= true;
                else
                    result= false;

                connection.Close();
                return result;
            }
            catch (System.Data.SqlClient.SqlException oleExp)
            {
                return false;
            }
        }

        public string Register(string username, string password, string Email, string Rights)
        {
            string result = "not Sucsess!";
            try
            {
                System.Data.SqlClient.SqlConnection connection1 = new System.Data.SqlClient.SqlConnection();
                connection1.ConnectionString = ConfigurationManager.ConnectionStrings["DadabaseNameConnectionString"].ConnectionString;
                connection1.Open();

                //here check if the userName or Email already registered Using Select from Tabel 
            
                string strSQL1 = "SELECT * FROM (tabel Name) WHERE UserName = '"+username+"' AND Email = '"+Email+"'";
                System.Data.SqlClient.SqlCommand command1 = new System.Data.SqlClient.SqlCommand();
                command1.CommandText = strSQL1;
                command1.Connection = connection1;
                SqlDataAdapter adapter = new SqlDataAdapter(command1);
                DataSet ds = new DataSet();
                adapter.Fill(ds);


                int rowsaffected1 = ds.Tables[0].Rows.Count;

                if (rowsaffected1 >0)

                    result= "User Name Or Email is already registered!";

                else
                {
                    string strSQL = "INSERT INTO Users(UserName, Password, Email, Rights) " +
                                    "VALUES " +
                                    "('" + username + "','" + password + "','" + Email + "','" + Rights + "') ";

                    command1.CommandText = strSQL;
                    int rowsaffected = command1.ExecuteNonQuery();

                    if (rowsaffected > 0)
                    {
                        result = "Sucsess!";
                    }

                    else
                    {
                        result = "not Sucsess!";
                    }

                }
                connection1.Close();
                return result;
            }
            
            catch (Exception oleExp)
            {
                return oleExp.Message;
            }
        }

        public string GetDataFromExcel(string FileName, string FilePath, string UserName, string Year, string Month,string Org,string UplodTime)
        {
          // OleDbConnection connection1 = new OleDbConnection();

            string result = "not Sucsess!";
            try
            {
                //this commet for Excle file to uploade database 

                // string path = string.Concat("~/Uploaded Folder/");
                //string excelConnectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=No;IMEX=1\";", path);

                 //connection.ConnectionString = excelConnectionString;
                 //connection.Open();

                  System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection();
                  connection.ConnectionString = ConfigurationManager.ConnectionStrings["EnergyInformationConnectionString"].ConnectionString;
                  connection.Open();

                string strSQL = "SELECT* FROM UplodFile WHERE FileName = '" + FileName + "' AND Year ='" + Year + "' AND Month = '" + Month + "' AND Org = '" + Org + "'";

                System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand();
                command.CommandText = strSQL;
                command.Connection = connection ;
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                int rowsaffected1 = ds.Tables[0].Rows.Count;

                if (rowsaffected1 > 0)
                {
                   result = "File already Uploade!";
                }
                else
                {
                    string strSQL1= "insert into (table Name) (FileName, FilePath, UserName, Year,Month, Org, UplodTime) " +
                                        "VALUES " +
                                        "('" + FileName + "','" + FilePath + "','" + UserName + "','" + Year + "' , '" + Month + "', '" + Org + "','" + UplodTime + "'') ";
                    
                    command.CommandText = strSQL1 ;
                    int rowsaffected = command.ExecuteNonQuery();

                    if (rowsaffected > 0)
                    {
                        result = "Files Uploaded Successfully!";
                        UploadFile();
                        //MethodInfo mi = this.GetType().GetMethod("UploadFile");

                    }

                    else
                    {
                        result = "Files Uploaded not Sucsess!";
                    }

                }
            }
            catch
            {

            }

            return result;
        }

        public void UploadFile()
        {
            //the excel file has same the column name you can castmation the Method
            string Year = "";
            string Month = "";
            string Product_Name = "";
            string Product_type = "";
            string Product_Category = "";
            string Source = "";
            string Sale_Flow = "";
            string Unit = "";
            string create_by = "";
            string create_time = "";
            string Update_by = "";
            string Update_tiem = "";
            string Product_code = "";
            string Volume_Per_Day = "";

            OleDbConnection connection = new OleDbConnection();
            try

            {

                string path = string.Concat(("~/Uploaded Folder/"));

                string excelConnectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=No;IMEX=1\";", path);



                connection.ConnectionString = excelConnectionString;
                connection.Open();
                //DataTable dt = null;

                using (OleDbCommand cmd = new OleDbCommand("select * from [LS$]", connection))
                {
                    using (OleDbDataReader rdr = cmd.ExecuteReader())
                    {
                        int i = 0;
                        while (rdr.Read())

                        {
                            if (i > 0)
                            {
                                Year = "";
                                Month = "";
                                Product_Name = "";
                                Product_type = "";
                                Product_Category = "";
                                Source = "";
                                Sale_Flow = "";
                                Unit = "";
                                create_by = "";
                                create_time = "";
                                Update_by = "";
                                Update_tiem = "";


                                Product_code = "";
                                Volume_Per_Day = "";
                                if (!rdr.IsDBNull(0))
                                    Year = rdr.GetString(0);
                                if (!rdr.IsDBNull(1))
                                    Month = rdr.GetString(1);
                                if (!rdr.IsDBNull(2))
                                    Product_Name = rdr.GetString(2);
                                if (!rdr.IsDBNull(3))
                                    Product_type = rdr.GetString(3);
                                if (!rdr.IsDBNull(4))
                                    Product_Category = rdr.GetString(4);
                                if (!rdr.IsDBNull(5))
                                    Source = rdr.GetString(5);
                                if (!rdr.IsDBNull(6))
                                    Sale_Flow = rdr.GetString(6);
                                if (!rdr.IsDBNull(7))
                                    Unit = rdr.GetString(7);
                                if (!rdr.IsDBNull(8))
                                    Product_code = rdr.GetString(8);
                                if (!rdr.IsDBNull(9))
                                    Volume_Per_Day = rdr.GetString(9);
                                InsertToSql(Year, Month, Product_Name, Product_type, Product_Category, Source, Sale_Flow, Unit, Product_code, Volume_Per_Day, create_by, create_time, Update_by, Update_tiem);
                            }
                            i += 1;
                        }

                    }
                }

            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
        }

        private void InsertToSql(string year, string month, string product_Name, string product_type, string product_Category, string source, string sale_Flow, string unit, string product_code, string volume_Per_Day, string create_by, string create_time, string Update_by, string Update_tiem)
        {

            try
            {
                using (SqlConnection openCon = new SqlConnection("Initial Catalog=DBname;Data Source=***********;user id=enazi;password=123"))
                {
                    openCon.Open();
                    string saveStaff = "INSERT INTO [dbo].[table Name] (year,month,product_Name,product_type,product_Category,source,sale_Flow,unit,product_code,volume_Per_Day,create_by,create_time, Update_by,Update_tiem)" +
                                       " VALUES " +
                                       "('" + year + "','" + month + "','" + product_Name + "','" + product_type + "','" + product_Category + "','" + source + "','" + sale_Flow + "','" + unit + "','" + product_code + "','" + volume_Per_Day + "',@create_by,@create_time,@Update_by,@Update_tiem)";

                    using (SqlCommand querySaveStaff = new SqlCommand(saveStaff))
                    {

                        querySaveStaff.Connection = openCon;
                        querySaveStaff.CommandType = CommandType.Text;
                        querySaveStaff.Parameters.AddWithValue("@create_by", "mohsen");
                        querySaveStaff.Parameters.AddWithValue("@create_time", DateTime.Now);
                        querySaveStaff.Parameters.AddWithValue("@Update_by", "mohsen");
                        querySaveStaff.Parameters.AddWithValue("@Update_tiem", DateTime.Now);

                        int rowsAffected = querySaveStaff.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {

            }
            finally
            {

            }
        }

        public string GetGetReport()
        {
            return "";
        }
    }
}