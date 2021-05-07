using System;
using System.Collections.Specialized;
using RestSharp;
using RestSharp.Authenticators;

namespace Services.Clients
{
    public class RestSharpApi
    {
        protected static readonly string DefaultContentType = "application/json";

        private readonly string _token;
        private readonly string _url;
        private readonly RestClient _client;


        public RestSharpApi(string url, string token = null)
        {
            _url = url;
            _token = token;
            _client = new RestClient(url)
            {
                Authenticator = new SimpleAuthenticator("username", "user", "password", "pass"),
            };
        }
        private ResponseClient ExecuteRequest(string resource,Method method, object Params)
        {
            var request = new RestRequest(resource,method);
            var contentType = DefaultContentType;//GetRequestContentType();
            request.AddHeader("content-type", contentType);//"application/json");
            request.AddHeader("Authorization", "Basic " + _token);//this.State.AccessToken);
            request.AddHeader("Referer", _url);
            if (Params != null)
            {
                string requestBody;
                // put params into post body
                if (DefaultContentType == null)
                {
                    //assume it's a string
                    requestBody = Params.ToString();
                }
                else
                {
                    requestBody = Params.ToString();//Translator.Encode(Params);
                }

                //add the requst body to the request stream
                if (!String.IsNullOrEmpty(requestBody))
                {
                    request.AddParameter(contentType, requestBody, ParameterType.RequestBody);
                }

            }
            IRestResponse response = _client.Execute(request);
            
            return new ResponseClient
            {
                Content = response.Content,
                StatusCode = response.StatusCode
            };            
        }
        public ResponseClient Execute(string resource,Method method,NameValueCollection callParams = null)
        {
            return ExecuteRequest(resource,method, callParams);
        }
        public ResponseClient Get(string resource,  NameValueCollection callParams = null)
        {
            var request = new RestRequest(resource);
            IRestResponse response=_client.Get(request);
            return new ResponseClient
            {
                Content = response.Content,
                StatusCode = response.StatusCode
            };
        }
        public ResponseClient Post(string resource,  object data = null)
        {
            var request = new RestRequest(resource);
            IRestResponse response = _client.Post(request);
            return new ResponseClient
            {
                Content = response.Content,
                StatusCode = response.StatusCode
            };
        }

        public ResponseClient Put(string resource,  object data)
        {
            var request = new RestRequest(resource);
            IRestResponse response = _client.Put(request);
            return new ResponseClient
            {
                Content = response.Content,
                StatusCode = response.StatusCode
            };
        }
       
        public ResponseClient Delete(string resource, object data = null)
        {
            var request = new RestRequest(resource);
            IRestResponse response = _client.Delete(request);
            return new ResponseClient
            {
                Content = response.Content,
                StatusCode = response.StatusCode
            };
        }
        
    }


}
