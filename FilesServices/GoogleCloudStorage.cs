using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using Google.Cloud.Storage.V1;

namespace Services.FilesServices
{
    public class GoogleCloudStorage
    {
        
        private readonly StorageClient _storageClient;
        private static GoogleCloudStorage _instance;
        private GoogleCloudStorage()
        {
            string connection = ConfigurationManager.AppSettings["connection"];
            _storageClient = StorageClient.Create();
        }
        public static GoogleCloudStorage GetInstance()
        {
            if (_instance == null)
            {
                _instance = new GoogleCloudStorage();
            }
            return _instance;
        }

    }
}
