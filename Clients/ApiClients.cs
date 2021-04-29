using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Clients
{  
    public class ApiClient
    {
        private string _url;
        private HttpClient _client;        
        public ApiClient(string url,AuthenticationHeaderValue  authenticationHeaderValue=null)        
        {
            _url = url;
            _client = new HttpClient();
            if (authenticationHeaderValue != null)
                _client.DefaultRequestHeaders.Authorization = authenticationHeaderValue;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
        }
        public void SetCustomHeaders(Dictionary<string, string> headers)
        {
	        foreach (var header in headers)
	        {
		        _client.DefaultRequestHeaders.Add(header.Key, header.Value);
	        }
        }
        public void SetUrl(string url)
        {
            _url=url;
        }
        public void SetAuthenticationHeader(string access_token)
        {
            if (!string.IsNullOrEmpty(access_token))
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(access_token);
        }
        public ResponseClient Post(string source, StringContent content)
        {
            try { 
                var t = Task.Run(() => this.PostAsync(source, content));
                t.Wait();            
                return t.Result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public ResponseClient Get(string source, string urlParams = null)
        {
            try { 
                string pathParams = string.IsNullOrEmpty(urlParams) ? "" : $"?{urlParams}";
                var t = Task.Run(() => this.GetAsync($"{source}{pathParams}"));
                t.Wait();
                return t.Result;
            }
            catch (Exception e)
            {
                throw e;
            }
}
        public string Put(string source, StringContent content)
        {
            return null;
        }
        public ResponseClient Delete(string source, string urlParams = null)
        {
	        try { 
		        string pathParams = string.IsNullOrEmpty(urlParams) ? "" : $"?{urlParams}";
		        var t = Task.Run(() => this.DeleteAsync($"{source}{pathParams}"));
		        t.Wait();
		        return t.Result;
	        }
	        catch (Exception e)
	        {
		        throw e;
	        }
        }


        private async Task<ResponseClient> PostAsync(string resource, StringContent content)
        {
            string responseString = "";
            string url = string.Format("{0}{1}", _url, resource);
            try {                
                HttpResponseMessage response = await _client.PostAsync(url, content);               
                responseString = await response.Content.ReadAsStringAsync();
                return new ResponseClient
                {
                    StatusCode=response.StatusCode,
                    Content = responseString
                };             
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        private async Task<ResponseClient> GetAsync(string resource)
        {
            string responseString = "";
            try
            {
                var response = await _client.GetAsync($"{_url}{resource}");
                responseString = await response.Content.ReadAsStringAsync();
                return  new ResponseClient
                {
                    StatusCode = response.StatusCode,
                    Content = responseString
                };
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        private async Task<ResponseClient> DeleteAsync(string resource)
        {
	        string responseString = "";
	        try
	        {
		        var response = await _client.DeleteAsync($"{_url}{resource}");
		        responseString = await response.Content.ReadAsStringAsync();
		        return  new ResponseClient
		        {
			        StatusCode = response.StatusCode,
			        Content = responseString
		        };
	        }
	        catch (Exception e)
	        {
		        throw e;
	        }
        }


        public string ToQueryString<T>(T obj)
        {
	        var json = JsonConvert.SerializeObject(obj,  Formatting.None, 
		        new JsonSerializerSettings { 
			        NullValueHandling = NullValueHandling.Ignore,
			        DefaultValueHandling = DefaultValueHandling.Ignore,
			        DateFormatHandling = DateFormatHandling.IsoDateFormat,
		        });

	        var jObj = (JObject) JsonConvert.DeserializeObject(json, new JsonSerializerSettings
	        {
		        DateParseHandling = DateParseHandling.None
	        });
	        var query = String.Join("&",
		        jObj.Children().Cast<JProperty>()
			        .Select(jp=>jp.Name + "=" + HttpUtility.UrlEncode(jp.Value.ToString())));
	        return query;
        }

    }


}