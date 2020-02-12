using System;
using System.Collections.Generic;

namespace DPEMoveDAL.Models
{
    public partial class EventParticipant
    {
        public int EventParticipantId { get; set; }
        public int EventId { get; set; }
        public int ParticipantId { get; set; }
        public string EventParticipantName { get; set; }
        public int? EventParticipantAmount { get; set; }
        public string EventParticipantUnit { get; set; }
        public int? Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }

        public Event Event { get; set; }
        public MParticipant Participant { get; set; }
    }
}
