using System;
using System.Collections.Generic;

namespace DPEMoveDAL.Models
{
    public partial class MParticipant
    {
        public MParticipant()
        {
            EventParticipant = new HashSet<EventParticipant>();
        }

        public int ParticipantId { get; set; }
        public string ParticipantCode { get; set; }
        public string ParticipantName { get; set; }
        public int? Status { get; set; }

        public ICollection<EventParticipant> EventParticipant { get; set; }
    }
}
