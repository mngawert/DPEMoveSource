using System;
using System.Collections.Generic;

namespace DPEMoveDAL.Models
{
    public partial class UploadedFile
    {
        public UploadedFile()
        {
            EventUploadedFile = new HashSet<EventUploadedFile>();
        }

        public int UploadedFileId { get; set; }
        public string UploadedFileCode { get; set; }
        public string FileType { get; set; }
        public string FileName { get; set; }
        public string FileUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }

        public ICollection<EventUploadedFile> EventUploadedFile { get; set; }
    }
}
