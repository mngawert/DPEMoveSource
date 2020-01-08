using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DPEMoveDAL.ViewModels
{
    public class UploadedFileViewModel
    {
        public int UploadedFileId { get; set; }
        public string UploadedFileCode { get; set; }
        public string FileType { get; set; }
        public string FileName { get; set; }
        public string FileUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public int EventId { get; set; }

        [JsonIgnore]
        public ICollection<EventUploadedFileViewModel> EventUploadedFile { get; set; }
    }
}
