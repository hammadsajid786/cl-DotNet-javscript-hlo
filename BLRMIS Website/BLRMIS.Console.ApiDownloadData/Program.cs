using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using BLRMIS.Console.ApiDownloadData.DAL;
using BLRMIS.Console.ApiDownloadData.Model;


namespace ReadAPIutility
{
    class Program
    {
        /// <summary>
        /// this is API's Call Method which accept three paremeters
        /// </summary>
        /// <param name="apiUrl"></param>
        /// <param name="HeaderName"></param>
        /// <param name="apiKey"></param>
        /// 
        List<DeliveryStats> model1 = new List<DeliveryStats>();
        List<DistrictDetail> model2 = new List<DistrictDetail>();

        public void GetAllEventData(string apiUrl, string HeaderName, string apiKey, int apiType)
        {
            try
            {
                using (var client = new WebClient()) //WebClient  
                {
                    client.Headers.Add("Content-Type", "application/json;charset=utf-8");
                    client.Headers.Add("Accept:application/json");
                    client.Headers.Add(HeaderName + ":" + apiKey);
                    var result = client.DownloadString(apiUrl); //URL  

                    if (apiType == 1)
                    {
                        var model = JsonConvert.DeserializeObject<DeliveryStats>(result);
                        model1.Add(model);
                    }
                    else
                    {
                        var model = JsonConvert.DeserializeObject<DistrictList>(result);
                        model2 = model.Data.ToList();
                    }

                    Console.WriteLine(Environment.NewLine + result);
                    Console.WriteLine(Environment.NewLine);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(Environment.NewLine + "API Calling Error");
                Console.WriteLine(Environment.NewLine + ex);
            }

        }

        /// <summary>
        /// this is Main program which execute first
        /// here we call all API's
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            string toDate = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
            string fromDate = string.Format("{0:yyyy-MM-dd}", DateTime.Now.AddDays(-1));

            // Markaze Sahulat API's
            string apiUrlMS = ConfigurationManager.AppSettings["apiUrlMS"].ToString();
            var HeaderNameMS = ConfigurationManager.AppSettings["apiSecurityMS"].ToString();
            var apiKeyMS = ConfigurationManager.AppSettings["apiKeyMS"].ToString();

            //e-Stamping API's
            string apiBaseUrlES = ConfigurationManager.AppSettings["apiUrlES"].ToString();
            var HeaderNameES = ConfigurationManager.AppSettings["apiSecurityES"].ToString();
            var apiKeyES = ConfigurationManager.AppSettings["apiKeyES"].ToString();
            apiBaseUrlES = apiBaseUrlES + "fromDate=" + fromDate + "&toDate" + toDate;
            //apiBaseUrlES = apiBaseUrlES + "fromDate=" + "01-08-2019" + "&toDate=" + "10-08-2019";

            //Insert District API's
            string apiBaseUrlID = ConfigurationManager.AppSettings["apiUrlID"].ToString();
            var HeaderNameID = ConfigurationManager.AppSettings["apiSecurityID"].ToString();
            var apiKeyID = ConfigurationManager.AppSettings["apiKeyID"].ToString();
            //apiBaseUrlES = apiBaseUrlES + "fromDate=" + DateTime.Now.ToShortDateString() + "&toDate" + DateTime.Now.ToShortDateString();
            apiBaseUrlID = apiBaseUrlID + "toDate=" + toDate + "&fromDate=" + fromDate;

            //Call API's
            Program objsync = new Program();
            objsync.GetAllEventData(apiUrlMS, HeaderNameMS, apiKeyMS, 1);
            objsync.GetAllEventData(apiBaseUrlES, HeaderNameES, apiKeyES, 1);
            objsync.GetAllEventData(apiBaseUrlID, HeaderNameID, apiKeyID, 2);

            DataProvider dp = new DataProvider();

            Console.WriteLine(Environment.NewLine + "Inserting Mark-E-Sahulat & eStamping Data");
            string MsgReturn = dp.InsertMarkzSahulatData(objsync.model1);
            Console.WriteLine(Environment.NewLine + MsgReturn);

            Console.WriteLine(Environment.NewLine + "Inserting Grievance Data");
            MsgReturn = dp.InsertGrievanceData();
            Console.WriteLine(Environment.NewLine + MsgReturn);

            Console.WriteLine(Environment.NewLine + "Inserting District Data");
            dp.InsertDistrictData(objsync.model2);
            Console.WriteLine(Environment.NewLine + MsgReturn);

            Console.ReadKey();
        }
    }
}
