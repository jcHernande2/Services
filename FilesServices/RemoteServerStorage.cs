using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Clients;

namespace Services.FilesServices
{
    public class RemoteServerStorage
    {
        private  HttpClientApi _apiClient;
        private static RemoteServerStorage _instance;
        private RemoteServerStorage()
        {
            string connection = ConfigurationManager.AppSettings["connection"];
            _apiClient = new HttpClientApi("localhost");
        }
        public static RemoteServerStorage GetInstance()
        {
            if (_instance == null)
            {
                _instance = new RemoteServerStorage();
            }
            return _instance;
        }
    }
}
