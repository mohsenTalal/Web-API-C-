using APIservice.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace APIservice.Controllers
{
    public class EIPController : ApiController
    {
        [HttpGet]
        public dynamic ping()
        {
            return "pong";
        }
        [HttpGet]
        public dynamic getDocs()

        {
            return "Hi from Get DOcs";
        }
        [HttpPost]
        public dynamic LogIn([FromBody] dynamic postParams)
        {

            string myJson = Convert.ToString(postParams);

            var vals = JObject.Parse(myJson);

            string username = vals["UserName"].ToString();
            string password = vals["Password"].ToString();

            EIPModel model1 = new EIPModel();
            //call api
            bool result = model1.LogIn(username, password);
            if (result)

              // return new HttpResponseMessage(HttpStatusCode.OK);
               return "Exist";

            else
                return "Not Exist";

        }

        [HttpPost]
        public string Register([FromBody] string postParams)
        {
            
            string myJson = Convert.ToString(postParams);

            var vals = JObject.Parse(myJson);

            string username = vals["UserName"].ToString();
            string password = vals["password"].ToString();
            string Email = vals["Email"].ToString();
            string Rights = vals["Rights"].ToString();

            EIPModel model1 = new EIPModel();

            string result = model1.Register(username, password, Email, Rights);
            return result;
        }

        [HttpPost]
        public string ReadDataFile([FromBody] string postParams)
        {

            string myJson = Convert.ToString(postParams);

            var vals = JObject.Parse(myJson);

            string FileName = vals["FileName"].ToString();
            string FilePath = vals["FilePath"].ToString();
            string UserName = vals["UserName"].ToString();
            string Year = vals["Year"].ToString();
            string Month = vals["Month"].ToString();
            string Org = vals["Org"].ToString();
            string UplodTime = vals["UplodTime"].ToString();


            EIPModel model1 = new EIPModel();

            string result = model1.GetDataFromExcel(FileName, FilePath, UserName,Year,Month, Org, UplodTime);

           // MethodInfo mi = this.GetType().GetMethod("UploadFile");

            return result;
        }

   
        [HttpPost]
        public string GetReport([FromBody] string postParams)
        {
            return "";
        }
    }
}
