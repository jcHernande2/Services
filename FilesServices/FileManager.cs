using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Services.FilesServices
{
    public enum FileServiceType
    {
        Azure,
        Google,
        Server
    }
    public class Setting
    {
        public FileServiceType FileServiceType { get;set; }
        public string Name { get; set;}
    }
    public class FileManager
    {
        private static IFileServices _instance;
        public static IFileServices GetInstance(Setting setting)
        {
            if (_instance == null)
            {
                Assembly assem = typeof(IFileServices).Assembly;
                _instance = (IFileServices)assem.CreateInstance($"Services.FilesServices.{setting.Name}"
                    , true, BindingFlags.Default, null, new object[] { setting }, null, null);               
            }
            return _instance;
        }
    }
}
