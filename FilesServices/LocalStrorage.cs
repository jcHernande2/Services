using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Services.FilesServices
{
    public class LocalStrorage
    {
       
        private static LocalStrorage _instance;
        private LocalStrorage()
        {
            string url = ConfigurationManager.AppSettings["dir"];
           
        }
        public static LocalStrorage GetInstance()
        {
            if (_instance == null)
            {
                _instance = new LocalStrorage();
            }
            return _instance;
        }
        public bool Save(string path)
        {
            if (!File.Exists(path))
            { 
                
                using (FileStream sw = File.Create(path))
                {
                   
                }
                
            }
            return true;
        }
    }
}
