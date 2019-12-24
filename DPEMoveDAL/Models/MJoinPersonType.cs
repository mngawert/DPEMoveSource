using System;
using System.Collections.Generic;

namespace DPEMoveDAL.Models
{
    public partial class MJoinPersonType
    {
        public MJoinPersonType()
        {
            EventJoinPersonType = new HashSet<EventJoinPersonType>();
        }

        public int JoinPersonTypeId { get; set; }
        public string JoinPersonTypeCode { get; set; }
        public string JoinPersonName { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }

        public ICollection<EventJoinPersonType> EventJoinPersonType { get; set; }
    }
}
