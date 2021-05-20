using System;
using System.IO;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Configuration;

namespace Services.FilesServices
{
    public class Azure: IFileServices
    {
        private readonly CloudBlobClient _blobClient;
        private static Azure _instance;
        private Azure()
        {
            string connection=ConfigurationManager.AppSettings["AzureConnection"];
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connection);
             _blobClient = storageAccount.CreateCloudBlobClient();
        }
        public static Azure GetInstance()
        {
              if(_instance==null){                 
                 _instance= new Azure();                	
              }
              return _instance;
        }
        public bool UploadBlobFromStream(string containerName, string fileName, MemoryStream stream)
        {
            if (string.IsNullOrEmpty(containerName) && string.IsNullOrEmpty(fileName))
            {
                return false;
            }
            try
            {
                CloudBlockBlob blockBlob = GetBlockBlob(containerName, fileName);
                blockBlob.UploadFromStreamAsync(stream);
                return true;
            }
            catch (Exception)
            {
                throw;
            }           
        }             
        public string GetBlobBase64(string containerName, string fileName)
        {
            if (string.IsNullOrEmpty(containerName) && string.IsNullOrEmpty(fileName))
            {
                return string.Empty;
            }
            try
            {
                CloudBlockBlob blockBlob = GetBlockBlob(containerName, fileName);
                using (MemoryStream stream = new MemoryStream())
                {
                    blockBlob.DownloadToStreamAsync(stream);
                    byte[] buff = stream.ToArray();
                    return Convert.ToBase64String(buff);                   
                }
            }
            catch (Exception)
            {
                throw;
            }            
        }
        private CloudBlockBlob GetBlockBlob(string containerName, string fileName)
        {
            CloudBlobContainer container = _blobClient.GetContainerReference(containerName);
            return container.GetBlockBlobReference(fileName);
        }
    }
}
