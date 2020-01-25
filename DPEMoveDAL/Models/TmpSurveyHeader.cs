using System;
using System.Collections.Generic;

namespace DPEMoveDAL.Models
{
    public partial class TmpSurveyHeader
    {
        public TmpSurveyHeader()
        {
            TmpSurveyDetail = new HashSet<TmpSurveyDetail>();
        }

        public int SurveyHeaderId { get; set; }
        public string SurveyHeaderCode { get; set; }
        public string Qc { get; set; }
        public string Phone { get; set; }
        public string IsInCity { get; set; }
        public int? AddressId { get; set; }
        public string EaCode { get; set; }
        public string HouseholdNo { get; set; }
        public string HouseholdCount { get; set; }
        public int? Morethan15YearsCount { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeePhone { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }

        public Address Address { get; set; }
        public ICollection<TmpSurveyDetail> TmpSurveyDetail { get; set; }
    }
}
