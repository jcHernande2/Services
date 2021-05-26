using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.FilesServices.Models
{
    public enum FileServiceType
    {
        Aws,
        Azure,
        Box,
        DropBox,
        Google,
        GoogleDive,
        Local,
        OneDrive,
        RemoteServer,
    }
   
}
