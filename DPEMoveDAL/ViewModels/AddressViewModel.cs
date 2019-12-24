using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DPEMoveDAL.ViewModels
{
    public class AddressViewModel
    {
        public int AddressId { get; set; }
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
        public int? Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }

        [JsonIgnore]
        public MStatusViewModel StatusNavigation { get; set; }
        [JsonIgnore]
        public ICollection<EventViewModel> Event { get; set; }
    }
}
