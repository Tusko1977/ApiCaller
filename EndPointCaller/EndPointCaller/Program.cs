using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using log4net;
using Newtonsoft.Json;
using System.Configuration;

namespace EndPointCaller
{

    internal class Program
    {
        private static readonly ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        private static void Main(string[] args)
        {

            try
            {

                bool cycledEndPointCall = CycleEndPointCall();
                var jsonMessageReader = new JsonMessageReader();
                var apiRequeste = new ApiRequest(Logger);
                while (true)

                {
                    
                    foreach (var jasonMessage in jsonMessageReader.GetJasonMessagesObjects())
                    {
                        //Console.WriteLine("");
                        Logger.Info("Request start --");

                        Logger.Info(jasonMessage.EndPointURI);

                        var jasonString = JsonConvert.SerializeObject(jasonMessage.EndPointBody);
                        
                        var responce = apiRequeste.PostRequest(jasonString, jasonMessage.EndPointURI).Result;

                        Logger.Info("Request End --");
                        Console.WriteLine("");
                        Console.WriteLine("");
                        Console.WriteLine("");
                        Console.WriteLine("");
                    }

                    if (!(cycledEndPointCall))
                    {
                        break;  
                    }

                }
            }            
            catch (WebException wex)
            {
                if (wex.Response != null)
                {
                    using (var errorResponse = (HttpWebResponse)wex.Response)
                    {
                        using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                        {
                            string error = reader.ReadToEnd();
                            Logger.Error("WebException" + error);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error("Exception occured---" + e.ToString());
            }

            Console.Read();
        }

        private static bool CycleEndPointCall()
        {
            try
            {
                string cycledEndPointCall = ConfigurationManager.AppSettings["CycleEndPointCall"];
                if (cycledEndPointCall.ToUpper() == "N")
                {
                    return false;
                }

                return true;
            }
            catch (Exception)
            {

                return true; 
            }

        }
    }

    public class EndPointCaller
    {
        
        private const string authorisationToken =
            "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ1bmlxdWVfbmFtZSI6IjJjNWU2M2IwNTdkNTRiMzdiNGNjMWMzZmJlZWU0YTljIiwic3ViIjoiMmM1ZTYzYjA1N2Q1NGIzN2I0Y2MxYzNmYmVlZTRhOWMiLCJyb2xlIjpbIjYxYTJhYzRjLTQ2YTQtNGI0My1hNjRjLWM0ZTRhMWQ3NjQwOCIsIjJjNWU2M2IwNTdkNTRiMzdiNGNjMWMzZmJlZWU0YTljIiwiQVNQLk5FVCBJZGVudGl0eSIsIjE2Nzg3ZTMxLWJkOTItNGJkOC1iOTQ4LTVmNmEyMzllNjZlOSIsIkVDVFAiLCJLVlBTIiwiQVBJIl0sImlzcyI6Imh0dHA6Ly9oZG1jbG91ZC1hcHAwMTo4MDI4IiwiYXVkIjoiYWYwM2M1Zjg5MTZlNGQ4ZDhhYmVmMWQwMWU3MDA3YjkiLCJleHAiOjIwOTkwODQzNDAsIm5iZiI6MTQ2Nzg4MTMwNn0.27KX9Za_ndr09k89C7LQuZgLcITNvUtMfXdTaF7Gihs";
        public ApiRespone GetFNO(string messageBody)
        {
            try
            {

                using (HttpClient client = new HttpClient())
                {
                    var url = string.Format("{0}/Contract/{1}/FuturesAndOptions/Trade/", "http://localhost:53376",
                        "qa");
                    using (var req = new HttpRequestMessage(HttpMethod.Post, url))
                    {
                        var tolen = string.Format("bearer {0}", authorisationToken);
                        req.Headers.Add("Authorization", tolen);
                        req.Headers.Accept.Add(
                            new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));                        
                        req.Content = new StringContent(messageBody, Encoding.UTF8, "application/json");
                        var task = client.SendAsync(req).Result;
                        
                        return task.Content.ReadAsAsync<ApiRespone>().Result;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


        }


        public void AddComponent(string messageBody)
        {
            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
            var url = string.Format("{0}/Contract/{1}/FuturesAndOptions/Trade/", "http://localhost:53376",
                        "qa");
            client.BaseAddress = new System.Uri(url);

            var token = string.Format("bearer {0}", authorisationToken);
            client.DefaultRequestHeaders.Add("Authorization", token);
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            System.Net.Http.HttpContent content = new StringContent(messageBody, UTF8Encoding.UTF8, "application/json");
            HttpResponseMessage messge = client.PostAsync(url, content).Result;
            string description = string.Empty;
            if (messge.IsSuccessStatusCode)
            {
                string result = messge.Content.ReadAsStringAsync().Result;                
            }
        }


        public ResponceObject AddComponent1(string messageBody)
        {
            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
            var url = string.Format("{0}/Contract/{1}/FuturesAndOptions/Trade/", "http://localhost:53376",
                        "qa");
            client.BaseAddress = new System.Uri(url);

            var token = string.Format("bearer {0}", authorisationToken);
            client.DefaultRequestHeaders.Add("Authorization", token);
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            System.Net.Http.HttpContent content = new StringContent(messageBody, UTF8Encoding.UTF8, "application/json");
            HttpResponseMessage messge = client.PostAsync(url, content).Result;
            string description = string.Empty;
            if (messge.IsSuccessStatusCode)
            {

                var result = messge.Content.ReadAsAsync<ResponceObject>().Result;
                return result;
            }
            return null;
        }
        
    }
    }




    

