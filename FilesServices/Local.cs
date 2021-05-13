using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Services.FilesServices
{
    public class Local
    {
       
        private static Local _instance;
        private Local()
        {
            string url = ConfigurationManager.AppSettings["dir"];
           
        }
        public static Local GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Local();
            }
            return _instance;
        }
        public bool Save(string path)
        {
            if (!File.Exists(path))
            { 
                
                using (FileStream sw = File.Create(path))
                {
                    /*sw.WriteLine("Hello");
                    sw.WriteLine("And");
                    sw.WriteLine("Welcome");*/
                }
                
            }
            return true;
        }
    }
}
