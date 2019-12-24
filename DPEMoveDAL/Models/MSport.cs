using System;
using System.Collections.Generic;

namespace DPEMoveDAL.Models
{
    public partial class MSport
    {
        public MSport()
        {
            EventSport = new HashSet<EventSport>();
        }

        public int SportId { get; set; }
        public string SportCode { get; set; }
        public string SportName { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }

        public ICollection<EventSport> EventSport { get; set; }
    }
}
