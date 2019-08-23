using System;
using System.Collections.Generic;
using System.Text;

namespace BLRMIS.Web.Common
{
    public class FileModel
    {
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public string OriginalFileName { get; set; }
        public string Mimetype { get; set; }
        public string Filesize { get; set; }
    }
}
