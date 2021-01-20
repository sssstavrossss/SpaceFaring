using Space101.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Space101.Dtos
{
    public class FilePathDto
    {
        public string FileName { get; set; }
        public FileType FileType { get;  set; }
        public string FileExtencion { get; set; }
    }
}