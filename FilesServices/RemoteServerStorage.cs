using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Clients;

namespace Services.FilesServices
{
    public class Server
    {
        private  HttpClientApi _apiClient;
        private static Server _instance;
        private Server()
        {
            string connection = ConfigurationManager.AppSettings["connection"];
            _apiClient = new HttpClientApi("localhost");
        }
        public static Server GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Server();
            }
            return _instance;
        }
    }
}
