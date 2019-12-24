using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DPEMoveDAL.ViewModels
{
    public class EventUploadedFileViewModel
    {
        public int EventUploadedFileId { get; set; }
        public string EventUploadedFileCode { get; set; }
        public int UploadedFileId { get; set; }
        public int EventId { get; set; }
        public int? Order { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }

        [JsonIgnore]
        public EventViewModel Event { get; set; }
        public UploadedFileViewModel UploadedFile { get; set; }
    }
}
