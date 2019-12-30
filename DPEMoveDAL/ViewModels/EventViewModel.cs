using System;
using System.Collections.Generic;

namespace DPEMoveDAL.ViewModels
{
    public class EventViewModel
    {
        public int EventId { get; set; }
        public string EventCode { get; set; }
        public string EventName { get; set; }
        public string EventShortDescription { get; set; }
        public string EventDescription { get; set; }
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



        /* Custom */
        public int? LimitStart { get; set; }
        public int? LimitSize { get; set; }
        public string OrderBy { get; set; }
        public string OrderDesc { get; set; }
        public double? Distance { get; set; }

        public AddressViewModel Address { get; set; }
        public MEventLevelViewModel EventLevel { get; set; }
        public MEventTypeViewModel EventType { get; set; }
        public MStatusViewModel StatusNavigation { get; set; }
        public ICollection<EventJoinPersonTypeViewModel> EventJoinPersonType { get; set; }
        public ICollection<EventUploadedFileViewModel> EventUploadedFile { get; set; }
        //public ICollection<UploadedFileViewModel> UploadedFile { get; set; }

    }



    public class EventRequestViewModel
    {
        public string EventName { get; set; }
        public string EventCode { get; set; }
        public int Status { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public string AmphurCode { get; set; }
        public string ProvinceCode { get; set; }
        public string TambonCode { get; set; }
        public double? Distance { get; set; }
        public int? LimitStart { get; set; }
        public int? LimitSize { get; set; }
        public string OrderBy { get; set; }
        public string OrderDesc { get; set; }
    }

    public class EventDbQuery
    {
        public int EventId { get; set; }
        public string EventCode { get; set; }
        public string EventName { get; set; }
        public string EventShortDescription { get; set; }
        public string EventDescription { get; set; }
        public DateTime EventStartTimestamp { get; set; }
        public DateTime? EventFinishTimestamp { get; set; }
        public string PublishUrl { get; set; }
        public string FileUrl { get; set; }
        public int ReadCount { get; set; }
        public int CommentCount { get; set; }
        public int Status { get; set; }
        public decimal? Budget { get; set; }
        public decimal? Budgetused { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public string AddressDescription { get; set; }
        public double? Distance { get; set; }
    }


}
