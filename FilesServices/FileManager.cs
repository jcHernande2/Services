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
        private static FileManager _instance;
        private IFileServices _fileServices;
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
        public bool Save()
        {
            //_fileService
            return true;
        }
        public string Read()
        {
            return "";
        }
    }
}
