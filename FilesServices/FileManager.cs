using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Services.FilesServices.Models;

namespace Services.FilesServices
{
    
    public class Setting
    {
        public FileServiceType FileServiceType { get;set; }
        public string Name { get; set;}
    }
    public class FileManager
    {
        private static FileManager _instance;
        private readonly IFileServices _fileServices;
        private FileManager(Setting setting) {
            Assembly assem = typeof(IFileServices).Assembly;
            _fileServices = (IFileServices)assem.CreateInstance($"Services.FilesServices.{setting.Name}"
                , true, BindingFlags.Default, null, new object[] { setting }, null, null);
        }
        public static FileManager GetInstance(Setting setting)
        {
            if (_instance == null)
            {
                _instance=new FileManager(setting);
               
            }
            return _instance;
        }
        public bool Upload()
        {
            //_fileService
            return true;
        }
        public string Download()
        {
            return "";
        }
    }
}
