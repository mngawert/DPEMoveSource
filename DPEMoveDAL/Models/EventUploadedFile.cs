using System;
using System.Collections.Generic;

namespace DPEMoveDAL.Models
{
    public partial class EventUploadedFile
    {
        public int EventUploadedFileId { get; set; }
        public string EventUploadedFileCode { get; set; }
        public int UploadedFileId { get; set; }
        public int EventId { get; set; }
        public int? Order { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }

        public Event Event { get; set; }
        public UploadedFile UploadedFile { get; set; }
    }
}
