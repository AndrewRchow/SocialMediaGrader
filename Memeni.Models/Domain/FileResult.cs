using System;
using System.Collections.Generic;

namespace Memeni.Models.Domain
{
    public class FileResult
    {
        public int FileId { get; set; }
        public string FileName { get; set; }
        public int Size { get; set; }
        public string ContentType { get; set; }
        public string ServerFileName { get; set; }
    }
}
