using System;
using System.Collections.Generic;

namespace DPEMoveDAL.Models
{
    public partial class User
    {
        public User()
        {
            MUserPermissiongroup = new HashSet<MUserPermissiongroup>();
        }

        public int UserId { get; set; }
        public string UserCode { get; set; }
        public string TitleCode { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int UserTypeId { get; set; }
        public string FacebookId { get; set; }
        public string Pin { get; set; }
        public string FacebookProfileName { get; set; }
        public int? AddressId { get; set; }
        public string Email { get; set; }
        public decimal? FailedLoginAttempt { get; set; }
        public string ForceChangePassword { get; set; }
        public DateTime? LastChangePasswordDate { get; set; }
        public DateTime? LastLogin { get; set; }
        public string Mobile { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string VerifiedType { get; set; }
        public string CardNo { get; set; }
        public string CardType { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string ApproveComment { get; set; }
        public string IsApprovedByAdmin { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }

        public Address Address { get; set; }
        public MUserType UserType { get; set; }
        public ICollection<MUserPermissiongroup> MUserPermissiongroup { get; set; }
    }
}
