using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using Google.Cloud.Storage.V1;

namespace Services.FilesServices
{
    public class GoogleCloud
    {
        
        private readonly StorageClient _storageClient;
        private static GoogleCloud _instance;
        private GoogleCloud()
        {
            string connection = ConfigurationManager.AppSettings["connection"];
            _storageClient = StorageClient.Create();
        }
        public static GoogleCloud GetInstance()
        {
            if (_instance == null)
            {
                _instance = new GoogleCloud();
            }
            return _instance;
        }

    }
}
