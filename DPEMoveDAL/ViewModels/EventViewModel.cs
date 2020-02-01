using DPEMoveDAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        public string EventStart { get; set; }
        public string EventFinish { get; set; }
    }

    public class EventDbQuery
    {
        public int EventId { get; set; }
        public string EventCode { get; set; }
        public string EventName { get; set; }
        public string EventShortDescription { get; set; }
        public string EventDescription { get; set; }
        public DateTime EventStartTimestamp { get; set; }
        public string EventStartTH { get; set; }
        public DateTime? EventFinishTimestamp { get; set; }
        public string PublishUrl { get; set; }
        public string FileUrl { get; set; }
        public int ReadCount { get; set; }
        public int CommentCount { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public decimal? Budget { get; set; }
        public decimal? Budgetused { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public string AddressDescription { get; set; }
        public double? Distance { get; set; }
        public string AmphurCode { get; set; }
        public string ProvinceCode { get; set; }
        public string TambonCode { get; set; }
        public double? VoteAvg { get; set; }
    }

    public class EventViewModel2
    {
        public int EventId { get; set; }
        public string EventCode { get; set; }
        public string EventName { get; set; }
        public string EventShortDescription { get; set; }
        public string EventDescription { get; set; }
        public DateTime EventStartTimestamp { get; set; }
        public DateTime? EventFinishTimestamp { get; set; }
        public string EventStartTime { get; set; }
        public string EventFinishTime { get; set; }
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
        public string IsFree { get; set; }

        public List<UploadedFile> UploadedFile { get; set; }

        public List<EventSport> EventSport { get; set; }
        public int[] SportIds { get; set; }
        public string SportEtc { get; set; }

        public List<EventFacilities> EventFacilities { get; set; }
        public List<EventFee> EventFee { get; set; }
        public List<EventNearby> EventNearby { get; set; }

        public List<EventObjectivePerson> EventObjectivePerson { get; set; }
        public int[] ObjectivePersonIds { get; set; }
        public string ObjectivePersonEtc { get; set; }


        public List<EventObjective> EventObjective { get; set; }
        public int[] EventObjectiveIds { get; set; }
        public string EventObjectiveEtc { get; set; }


        /* address */
        public string AmphurCode { get; set; }
        public string ProvinceCode { get; set; }
        public string TambonCode { get; set; }
        public string BuildingName { get; set; }
        public int? AddressTypeId { get; set; }
        public string Moo { get; set; }
        public string HousePropertyName { get; set; }
        public string No { get; set; }
        public string Road { get; set; }
        public string Soi { get; set; }
        public string Floor { get; set; }
        public string Postcode { get; set; }
        public string Lane { get; set; }
        public string RoomNo { get; set; }
        public string Description { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
    }

}
