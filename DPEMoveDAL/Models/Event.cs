using System;
using System.Collections.Generic;

namespace DPEMoveDAL.Models
{
    public partial class Event
    {
        public Event()
        {
            EventFacilities = new HashSet<EventFacilities>();
            EventFee = new HashSet<EventFee>();
            EventGoal = new HashSet<EventGoal>();
            EventJoinPersonType = new HashSet<EventJoinPersonType>();
            EventLevelNavigation = new HashSet<EventLevel>();
            EventNearby = new HashSet<EventNearby>();
            EventObjective = new HashSet<EventObjective>();
            EventParticipant = new HashSet<EventParticipant>();
            EventSport = new HashSet<EventSport>();
            EventUploadedFile = new HashSet<EventUploadedFile>();
        }

        public int EventId { get; set; }
        public string EventCode { get; set; }
        public string EventName { get; set; }
        public string EventShortDescription { get; set; }
        public DateTime EventStartTimestamp { get; set; }
        public DateTime? EventFinishTimestamp { get; set; }
        public int? AddressId { get; set; }
        public string StadiumCode { get; set; }
        public string PublishUrl { get; set; }
        public string ResponsiblePerson { get; set; }
        public string ResponsiblePersonCode { get; set; }
        public int ReadCount { get; set; }
        public int EventLevelId { get; set; }
        public string EventLevelEtc { get; set; }
        public int? EventObjectivePersonId { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public decimal? Budget { get; set; }
        public decimal? Budgetused { get; set; }
        public int? EventTypeId { get; set; }
        public string ProjectSelect { get; set; }
        public string ProjectCode { get; set; }
        public string ResponsiblePersonType { get; set; }
        public string ContactPersonName { get; set; }
        public string ContactPersonEmail { get; set; }
        public string ContactPersonMobile { get; set; }
        public string ContactPersonFax { get; set; }
        public string ContactPersonLineid { get; set; }
        public string EventDescription { get; set; }
        public string IsFree { get; set; }
        public string IsCancel { get; set; }
        public string CancelReason { get; set; }

        public Address Address { get; set; }
        public MEventLevel EventLevel { get; set; }
        public MEventType EventType { get; set; }
        public ICollection<EventFacilities> EventFacilities { get; set; }
        public ICollection<EventFee> EventFee { get; set; }
        public ICollection<EventGoal> EventGoal { get; set; }
        public ICollection<EventJoinPersonType> EventJoinPersonType { get; set; }
        public ICollection<EventLevel> EventLevelNavigation { get; set; }
        public ICollection<EventNearby> EventNearby { get; set; }
        public ICollection<EventObjective> EventObjective { get; set; }
        public ICollection<EventParticipant> EventParticipant { get; set; }
        public ICollection<EventSport> EventSport { get; set; }
        public ICollection<EventUploadedFile> EventUploadedFile { get; set; }
    }
}
