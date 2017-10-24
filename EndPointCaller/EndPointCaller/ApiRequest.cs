using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Newtonsoft.Json;
using System.Net;

namespace EndPointCaller
{
    public class ApiRequest
    {

        private string _url;
        private string _authorizationToken;
        private string _verb;
        private ILog _logger;
        public ApiRequest(ILog logger)

        {

            _url = ConfigurationManager.AppSettings["url"];
            _authorizationToken = ConfigurationManager.AppSettings["authorizationToken"];
            _logger = logger;
            _verb = ConfigurationManager.AppSettings["Verb"];

        }


        public async Task<IResponse> PostRequest(string messageBody, string customeUrl)
        {
            try

            {
                var client = new HttpClient { BaseAddress = new System.Uri(customeUrl) };

                client.DefaultRequestHeaders.Add("Authorization", _authorizationToken);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpContent content = new StringContent(messageBody, UTF8Encoding.UTF8, "application/json");

                //POST Endpoints
                if (_verb.Contains("POST"))
                {
                    HttpResponseMessage messge = await client.PostAsync(customeUrl, content);

                    string description = string.Empty;


                    if (!messge.IsSuccessStatusCode)
                    {
                        _logger.Error("Error Response");

                        _logger.Error(messge.ReasonPhrase);
                        _logger.Error((int)messge.StatusCode);
                        var response = messge.Content.ReadAsAsync<ErrorObject>().Result;
                        var jasonErrString = JsonConvert.SerializeObject(response);
                        _logger.Error(jasonErrString);

                        return await Task.FromResult<IResponse>(response); ;

                    }

                    _logger.Info("Sucess Response");
                    var result = messge.Content.ReadAsAsync<Object>().Result;
                    var jasonSucessString = JsonConvert.SerializeObject(result);
                    _logger.Info(jasonSucessString);
                    return null;
                }
                if (_verb.Contains("GET"))
                {
                    HttpResponseMessage messge = await client.GetAsync(customeUrl);

                    string description = string.Empty;


                    if (!messge.IsSuccessStatusCode)
                    {
                        _logger.Error("Error Response");

                        _logger.Error(messge.ReasonPhrase);
                        _logger.Error((int)messge.StatusCode);
                        var response = messge.Content.ReadAsAsync<ErrorObject>().Result;
                        var jasonErrString = JsonConvert.SerializeObject(response);
                        _logger.Error(jasonErrString);

                        return await Task.FromResult<IResponse>(response); ;

                    }

                    _logger.Info("Sucess Response");
                    var result = messge.Content.ReadAsAsync<Object>().Result;
                    var jasonSucessString = JsonConvert.SerializeObject(result);
                    _logger.Info(jasonSucessString);
                    return null;
                }
                if (_verb.Contains("PUT"))
                {
                    HttpResponseMessage messge = await client.PutAsync(customeUrl, content);
                    
                    string description = string.Empty;


                    if (!messge.IsSuccessStatusCode)
                    {
                        _logger.Error("Error Response");

                        _logger.Error(messge.ReasonPhrase);
                        _logger.Error((int)messge.StatusCode);
                        var response = messge.Content.ReadAsAsync<ErrorObject>().Result;
                        var jasonErrString = JsonConvert.SerializeObject(response);
                        _logger.Error(jasonErrString);

                        return await Task.FromResult<IResponse>(response); ;

                    }

                    _logger.Info("Sucess Response");
                    var result = messge.Content.ReadAsAsync<Object>().Result;
                    var jasonSucessString = JsonConvert.SerializeObject(result);
                    _logger.Info(jasonSucessString);
                    return null;
                }
                if (_verb.Contains("DELETE"))
                {
                    HttpResponseMessage messge = await client.DeleteAsync(customeUrl);

                    string description = string.Empty;


                    if (!messge.IsSuccessStatusCode)
                    {
                        _logger.Error("Error Response");

                        _logger.Error(messge.ReasonPhrase);
                        _logger.Error((int)messge.StatusCode);
                        var response = messge.Content.ReadAsAsync<ErrorObject>().Result;
                        var jasonErrString = JsonConvert.SerializeObject(response);
                        _logger.Error(jasonErrString);

                        return await Task.FromResult<IResponse>(response); ;

                    }

                    _logger.Info("Sucess Response");
                    var result = messge.Content.ReadAsAsync<Object>().Result;
                    var jasonSucessString = JsonConvert.SerializeObject(result);
                    _logger.Info(jasonSucessString);
                    return null;
                }

                else
                {
                    HttpResponseMessage messge = await client.GetAsync(customeUrl);

                    string description = string.Empty;


                    if (!messge.IsSuccessStatusCode)
                    {
                        _logger.Error("Error Response");

                        _logger.Error(messge.ReasonPhrase);
                        _logger.Error((int)messge.StatusCode);
                        var response = messge.Content.ReadAsAsync<ErrorObject>().Result;
                        var jasonErrString = JsonConvert.SerializeObject(response);
                        _logger.Error(jasonErrString);

                        return await Task.FromResult<IResponse>(response); ;

                    }

                    _logger.Info("Sucess Response");
                    var result = messge.Content.ReadAsAsync<Object>().Result;
                    var jasonSucessString = JsonConvert.SerializeObject(result);
                    _logger.Info(jasonSucessString);
                    return null;
                }
            }
            catch (Exception e)
            {
                if (e != null)
                {

                }
                return null;
            }
        

        }

       


    }
}
