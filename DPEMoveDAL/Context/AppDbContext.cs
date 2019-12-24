﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DPEMoveDAL.Models
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ActionLog> ActionLog { get; set; }
        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<Applog> Applog { get; set; }
        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<DepartmentEvent> DepartmentEvent { get; set; }
        public virtual DbSet<DepartmentPerson> DepartmentPerson { get; set; }
        public virtual DbSet<District> District { get; set; }
        public virtual DbSet<Employeeshierarchy> Employeeshierarchy { get; set; }
        public virtual DbSet<Event> Event { get; set; }
        public virtual DbSet<EventFacilities> EventFacilities { get; set; }
        public virtual DbSet<EventGoal> EventGoal { get; set; }
        public virtual DbSet<EventJoinPersonType> EventJoinPersonType { get; set; }
        public virtual DbSet<EventLevel> EventLevel { get; set; }
        public virtual DbSet<EventObjective> EventObjective { get; set; }
        public virtual DbSet<EventSport> EventSport { get; set; }
        public virtual DbSet<EventUploadedFile> EventUploadedFile { get; set; }
        public virtual DbSet<GenerateCode> GenerateCode { get; set; }
        public virtual DbSet<MAccountType> MAccountType { get; set; }
        public virtual DbSet<MAddressType> MAddressType { get; set; }
        public virtual DbSet<MDepartmentEventSubtype> MDepartmentEventSubtype { get; set; }
        public virtual DbSet<MEventFacilitiesTopic> MEventFacilitiesTopic { get; set; }
        public virtual DbSet<MEventFacilitiesXxxxdelete> MEventFacilitiesXxxxdelete { get; set; }
        public virtual DbSet<MEventGoal> MEventGoal { get; set; }
        public virtual DbSet<MEventLevel> MEventLevel { get; set; }
        public virtual DbSet<MEventObjective> MEventObjective { get; set; }
        public virtual DbSet<MEventObjectivePerson> MEventObjectivePerson { get; set; }
        public virtual DbSet<MEventType> MEventType { get; set; }
        public virtual DbSet<MGroup> MGroup { get; set; }
        public virtual DbSet<MGroupRole> MGroupRole { get; set; }
        public virtual DbSet<MIdcardType> MIdcardType { get; set; }
        public virtual DbSet<MJoinPersonType> MJoinPersonType { get; set; }
        public virtual DbSet<MPermissionGroup> MPermissionGroup { get; set; }
        public virtual DbSet<MPermissiongroupProgram> MPermissiongroupProgram { get; set; }
        public virtual DbSet<MPhoneNumberType> MPhoneNumberType { get; set; }
        public virtual DbSet<MProgram> MProgram { get; set; }
        public virtual DbSet<MRole> MRole { get; set; }
        public virtual DbSet<MSport> MSport { get; set; }
        public virtual DbSet<MStatus> MStatus { get; set; }
        public virtual DbSet<MUserPermissiongroup> MUserPermissiongroup { get; set; }
        public virtual DbSet<MUserType> MUserType { get; set; }
        public virtual DbSet<MVoteType> MVoteType { get; set; }
        public virtual DbSet<OtpLog> OtpLog { get; set; }
        public virtual DbSet<PhoneDetail> PhoneDetail { get; set; }
        public virtual DbSet<PrivateMessage> PrivateMessage { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Province> Province { get; set; }
        public virtual DbSet<Rolegroup> Rolegroup { get; set; }
        public virtual DbSet<Rolegrouphasrole> Rolegrouphasrole { get; set; }
        public virtual DbSet<Rolegrouphasuser> Rolegrouphasuser { get; set; }
        public virtual DbSet<TmpAccount> TmpAccount { get; set; }
        public virtual DbSet<UploadedFile> UploadedFile { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Vote> Vote { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseOracle("name=DPEMoveDatabase");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:DefaultSchema", "DPEMOVE");

            modelBuilder.Entity<ActionLog>(entity =>
            {
                entity.ToTable("ACTION_LOG");

                entity.HasIndex(e => e.ActionlogId)
                    .HasName("PK_ACTION_LOG")
                    .IsUnique();

                entity.Property(e => e.ActionlogId).HasColumnName("ACTIONLOG_ID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("CREATED_DATE")
                    .HasColumnType("TIMESTAMP(6)");

                entity.Property(e => e.Ip)
                    .HasColumnName("IP")
                    .HasColumnType("VARCHAR2(255)");

                entity.Property(e => e.LogHeader)
                    .HasColumnName("LOG_HEADER")
                    .HasColumnType("VARCHAR2(255)");

                entity.Property(e => e.LogMessage)
                    .HasColumnName("LOG_MESSAGE")
                    .HasColumnType("VARCHAR2(255)");

                entity.Property(e => e.MacAddress)
                    .HasColumnName("MAC_ADDRESS")
                    .HasColumnType("VARCHAR2(255)");

                entity.Property(e => e.Username)
                    .HasColumnName("USERNAME")
                    .HasColumnType("VARCHAR2(255)");
            });

            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("ADDRESS");

                entity.HasIndex(e => e.AddressId)
                    .HasName("PK_ADDRESS_LOG")
                    .IsUnique();

                entity.Property(e => e.AddressId).HasColumnName("ADDRESS_ID");

                entity.Property(e => e.AddressTypeId).HasColumnName("ADDRESS_TYPE_ID");

                entity.Property(e => e.AmphurCode)
                    .HasColumnName("AMPHUR_CODE")
                    .HasColumnType("VARCHAR2(20)");

                entity.Property(e => e.BuildingName)
                    .HasColumnName("BUILDING_NAME")
                    .HasColumnType("VARCHAR2(255)");

                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("CREATED_DATE")
                    .HasColumnType("TIMESTAMP(6)");

                entity.Property(e => e.Description)
                    .HasColumnName("DESCRIPTION")
                    .HasColumnType("VARCHAR2(255)");

                entity.Property(e => e.Floor)
                    .HasColumnName("FLOOR")
                    .HasColumnType("VARCHAR2(255)");

                entity.Property(e => e.HousePropertyName)
                    .HasColumnName("HOUSE_PROPERTY_NAME")
                    .HasColumnType("VARCHAR2(255)");

                entity.Property(e => e.Lane)
                    .HasColumnName("LANE")
                    .HasColumnType("VARCHAR2(256)");

                entity.Property(e => e.Latitude)
                    .HasColumnName("LATITUDE")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.Longitude)
                    .HasColumnName("LONGITUDE")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.Moo)
                    .HasColumnName("MOO")
                    .HasColumnType("VARCHAR2(255)");

                entity.Property(e => e.No)
                    .HasColumnName("NO")
                    .HasColumnType("VARCHAR2(255)");

                entity.Property(e => e.Postcode)
                    .HasColumnName("POSTCODE")
                    .HasColumnType("VARCHAR2(20)");

                entity.Property(e => e.ProvinceCode)
                    .HasColumnName("PROVINCE_CODE")
                    .HasColumnType("VARCHAR2(20)");

                entity.Property(e => e.Road)
                    .HasColumnName("ROAD")
                    .HasColumnType("VARCHAR2(255)");

                entity.Property(e => e.RoomNo)
                    .HasColumnName("ROOM_NO")
                    .HasColumnType("VARCHAR2(254)");

                entity.Property(e => e.Soi)
                    .HasColumnName("SOI")
                    .HasColumnType("VARCHAR2(255)");

                entity.Property(e => e.Status).HasColumnName("STATUS");

                entity.Property(e => e.TambonCode)
                    .HasColumnName("TAMBON_CODE")
                    .HasColumnType("VARCHAR2(20)");

                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("UPDATED_DATE")
                    .HasColumnType("TIMESTAMP(6)");
            });

            modelBuilder.Entity<Applog>(entity =>
            {
                entity.ToTable("APPLOG");

                entity.HasIndex(e => e.Id)
                    .HasName("APPLOG_PK")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Exception)
                    .HasColumnName("EXCEPTION")
                    .HasColumnType("VARCHAR2(512)");

                entity.Property(e => e.Logged)
                    .HasColumnName("LOGGED")
                    .HasColumnType("VARCHAR2(50)");

                entity.Property(e => e.Logger)
                    .HasColumnName("LOGGER")
                    .HasColumnType("VARCHAR2(250)");

                entity.Property(e => e.Loglevel)
                    .HasColumnName("LOGLEVEL")
                    .HasColumnType("VARCHAR2(50)");

                entity.Property(e => e.Message)
                    .HasColumnName("MESSAGE")
                    .HasColumnType("VARCHAR2(4000)");

                entity.Property(e => e.RequestIp)
                    .HasColumnName("REQUEST_IP")
                    .HasColumnType("VARCHAR2(50)");
            });

            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.Id)
                    .HasName("PK_AspNetRoleClaims")
                    .IsUnique();

                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_AspNetRoleClaims_RoleId");
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.Id)
                    .HasName("PK_AspNetRoles")
                    .IsUnique();

                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.Id)
                    .HasName("PK_AspNetUserClaims")
                    .IsUnique();

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_AspNetUserClaims_UserId");
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.HasIndex(e => new { e.LoginProvider, e.ProviderKey })
                    .HasName("PK_AspNetUserLogins")
                    .IsUnique();

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_AspNetUserLogins_UserId");
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.HasIndex(e => new { e.UserId, e.RoleId })
                    .HasName("PK_AspNetUserRoles")
                    .IsUnique();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_AspNetUserRoles_RoleId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_AspNetUserRoles_UserId");
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.Id)
                    .HasName("PK_AspNetUsers")
                    .IsUnique();

                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AccountType).HasColumnType("VARCHAR2(2)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.IdcardNo).HasColumnType("VARCHAR2(20)");

                entity.Property(e => e.IdcardType).HasColumnType("VARCHAR2(1)");

                entity.Property(e => e.Name).HasColumnType("VARCHAR2(50)");

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.Status).HasColumnType("VARCHAR2(1)");

                entity.Property(e => e.UserName)
                    .HasMaxLength(256)
                    .ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.HasIndex(e => new { e.UserId, e.LoginProvider, e.Name })
                    .HasName("PK_AspNetUserTokens")
                    .IsUnique();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_AspNetUserTokens_UserId");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("COMMENT");

                entity.HasIndex(e => e.CommentId)
                    .HasName("PK_COMMENT")
                    .IsUnique();

                entity.Property(e => e.CommentId).HasColumnName("COMMENT_ID");

                entity.Property(e => e.Comment1)
                    .IsRequired()
                    .HasColumnName("COMMENT")
                    .HasColumnType("VARCHAR2(255)");

                entity.Property(e => e.CommentCode)
                    .IsRequired()
                    .HasColumnName("COMMENT_CODE")
                    .HasColumnType("VARCHAR2(32)");

                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("CREATED_DATE")
                    .HasColumnType("TIMESTAMP(6)");

                entity.Property(e => e.EventCode)
                    .HasColumnName("EVENT_CODE")
                    .HasColumnType("VARCHAR2(32)");

                entity.Property(e => e.StadiumCode)
                    .HasColumnName("STADIUM_CODE")
                    .HasColumnType("VARCHAR2(32)");

                entity.Property(e => e.Status).HasColumnName("STATUS");

                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("UPDATED_DATE")
                    .HasColumnType("TIMESTAMP(6)");

                entity.Property(e => e.UserCode)
                    .IsRequired()
                    .HasColumnName("USER_CODE")
                    .HasColumnType("VARCHAR2(32)");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("DEPARTMENT");

                entity.HasIndex(e => e.DepartmentId)
                    .HasName("PK_DEPARTMENT")
                    .IsUnique();

                entity.Property(e => e.DepartmentId).HasColumnName("DEPARTMENT_ID");

                entity.Property(e => e.AddressId).HasColumnName("ADDRESS_ID");

                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("CREATED_DATE")
                    .HasColumnType("TIMESTAMP(6)");

                entity.Property(e => e.Department1)
                    .IsRequired()
                    .HasColumnName("DEPARTMENT")
                    .HasColumnType("VARCHAR2(255)");

                entity.Property(e => e.DepartmentCode)
                    .IsRequired()
                    .HasColumnName("DEPARTMENT_CODE")
                    .HasColumnType("VARCHAR2(32)");

                entity.Property(e => e.DepartmentType)
                    .IsRequired()
                    .HasColumnName("DEPARTMENT_TYPE")
                    .HasColumnType("CHAR(1)");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("EMAIL")
                    .HasColumnType("VARCHAR2(255)");

                entity.Property(e => e.Mission)
                    .HasColumnName("MISSION")
                    .HasColumnType("VARCHAR2(2048)");

                entity.Property(e => e.Mobile)
                    .HasColumnName("MOBILE")
                    .HasColumnType("VARCHAR2(20)");

                entity.Property(e => e.Status).HasColumnName("STATUS");

                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("UPDATED_DATE")
                    .HasColumnType("TIMESTAMP(6)");

                entity.Property(e => e.Vistion)
                    .HasColumnName("VISTION")
                    .HasColumnType("VARCHAR2(2048)");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Department)
                    .HasForeignKey(d => d.AddressId)
                    .HasConstraintName("FK_ADDRESS");
            });

            modelBuilder.Entity<DepartmentEvent>(entity =>
            {
                entity.ToTable("DEPARTMENT_EVENT");

                entity.HasIndex(e => e.DepartmentEventId)
                    .HasName("PK_DEPARTMENT_EVENT")
                    .IsUnique();

                entity.Property(e => e.DepartmentEventId).HasColumnName("DEPARTMENT_EVENT_ID");

                entity.Property(e => e.AddressId).HasColumnName("ADDRESS_ID");

                entity.Property(e => e.Budget)
                    .HasColumnName("BUDGET")
                    .HasColumnType("NUMBER(18,2)");

                entity.Property(e => e.BudgetUsed)
                    .HasColumnName("BUDGET_USED")
                    .HasColumnType("NUMBER(18,2)");

                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("CREATED_DATE")
                    .HasColumnType("TIMESTAMP(6)");

                entity.Property(e => e.DepartmentEventCode)
                    .IsRequired()
                    .HasColumnName("DEPARTMENT_EVENT_CODE")
                    .HasColumnType("VARCHAR2(32)");

                entity.Property(e => e.DepartmentEventSubtypeId).HasColumnName("DEPARTMENT_EVENT_SUBTYPE_ID");

                entity.Property(e => e.DepartmentEventType)
                    .IsRequired()
                    .HasColumnName("DEPARTMENT_EVENT_TYPE")
                    .HasColumnType("VARCHAR2(1)");

                entity.Property(e => e.DepartmentId).HasColumnName("DEPARTMENT_ID");

                entity.Property(e => e.EventDescription)
                    .IsRequired()
                    .HasColumnName("EVENT_DESCRIPTION")
                    .HasColumnType("VARCHAR2(2048)");

                entity.Property(e => e.EventFinishTimestamp)
                    .HasColumnName("EVENT_FINISH_TIMESTAMP")
                    .HasColumnType("TIMESTAMP(6)");

                entity.Property(e => e.EventName)
                    .IsRequired()
                    .HasColumnName("EVENT_NAME")
                    .HasColumnType("VARCHAR2(255)");

                entity.Property(e => e.EventShortDescription)
                    .IsRequired()
                    .HasColumnName("EVENT_SHORT_DESCRIPTION")
                    .HasColumnType("VARCHAR2(255)");

                entity.Property(e => e.EventStartTimestamp)
                    .HasColumnName("EVENT_START_TIMESTAMP")
                    .HasColumnType("TIMESTAMP(6)");

                entity.Property(e => e.ParentDepartmentEventId).HasColumnName("PARENT_DEPARTMENT_EVENT_ID");

                entity.Property(e => e.Status).HasColumnName("STATUS");

                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("UPDATED_DATE")
                    .HasColumnType("TIMESTAMP(6)");
            });

            modelBuilder.Entity<DepartmentPerson>(entity =>
            {
                entity.ToTable("DEPARTMENT_PERSON");

                entity.HasIndex(e => e.DepartmentPersonId)
                    .HasName("PK_DEPARTMENT_PERSON")
                    .IsUnique();

                entity.Property(e => e.DepartmentPersonId).HasColumnName("DEPARTMENT_PERSON_ID");

                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("CREATED_DATE")
                    .HasColumnType("TIMESTAMP(6)");

                entity.Property(e => e.DepartmentId).HasColumnName("DEPARTMENT_ID");

                entity.Property(e => e.DepartmentPersonCode)
                    .IsRequired()
                    .HasColumnName("DEPARTMENT_PERSON_CODE")
                    .HasColumnType("VARCHAR2(32)");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("EMAIL")
                    .HasColumnType("VARCHAR2(255)");

                entity.Property(e => e.Firstname)
                    .HasColumnName("FIRSTNAME")
                    .HasColumnType("VARCHAR2(255)");

                entity.Property(e => e.Lastname)
                    .HasColumnName("LASTNAME")
                    .HasColumnType("VARCHAR2(255)");

                entity.Property(e => e.Mobile)
                    .HasColumnName("MOBILE")
                    .HasColumnType("VARCHAR2(20)");

                entity.Property(e => e.ParentPersonId).HasColumnName("PARENT_PERSON_ID");

                entity.Property(e => e.PositionName)
                    .HasColumnName("POSITION_NAME")
                    .HasColumnType("VARCHAR2(255)");

                entity.Property(e => e.Status).HasColumnName("STATUS");

                entity.Property(e => e.TitleCode)
                    .HasColumnName("TITLE_CODE")
                    .HasColumnType("VARCHAR2(32)");

                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("UPDATED_DATE")
                    .HasColumnType("TIMESTAMP(6)");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.DepartmentPerson)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DEPARTMENT");
            });

            modelBuilder.Entity<District>(entity =>
            {
                entity.ToTable("DISTRICT");

                entity.HasIndex(e => e.DistrictId)
                    .HasName("PK_DISTRICTID")
                    .IsUnique();

                entity.Property(e => e.DistrictId)
                    .HasColumnName("DISTRICT_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.DistrictCode)
                    .IsRequired()
                    .HasColumnName("DISTRICT_CODE")
                    .HasColumnType("VARCHAR2(50)");

                entity.Property(e => e.DistrictName)
                    .IsRequired()
                    .HasColumnName("DISTRICT_NAME")
                    .HasColumnType("VARCHAR2(50)");

                entity.Property(e => e.ProvinceId).HasColumnName("PROVINCE_ID");

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.District)
                    .HasForeignKey(d => d.ProvinceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PROVINCE");
            });

            modelBuilder.Entity<Employeeshierarchy>(entity =>
            {
                entity.HasKey(e => e.Employeeid);

                entity.ToTable("EMPLOYEESHIERARCHY");

                entity.HasIndex(e => e.Employeeid)
                    .HasName("PK_EMPLOYEESHIERARCHY")
                    .IsUnique();

                entity.Property(e => e.Employeeid)
                    .HasColumnName("EMPLOYEEID")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.Designation)
                    .IsRequired()
                    .HasColumnName("DESIGNATION")
                    .HasColumnType("VARCHAR2(30)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("NAME")
                    .HasColumnType("VARCHAR2(100)");

                entity.Property(e => e.Photopath)
                    .HasColumnName("PHOTOPATH")
                    .HasColumnType("VARCHAR2(255)");

                entity.Property(e => e.Reportingmanager)
                    .HasColumnName("REPORTINGMANAGER")
                    .HasColumnType("NUMBER");
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.ToTable("EVENT");

                entity.HasIndex(e => e.EventId)
                    .HasName("PK_EVENT")
                    .IsUnique();

                entity.Property(e => e.EventId).HasColumnName("EVENT_ID");

                entity.Property(e => e.AddressId).HasColumnName("ADDRESS_ID");

                entity.Property(e => e.Budget)
                    .HasColumnName("BUDGET")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.Budgetused)
                    .HasColumnName("BUDGETUSED")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.ContactPersonEmail)
                    .HasColumnName("CONTACT_PERSON_EMAIL")
                    .HasColumnType("VARCHAR2(256)");

                entity.Property(e => e.ContactPersonFax)
                    .HasColumnName("CONTACT_PERSON_FAX")
                    .HasColumnType("VARCHAR2(128)");

                entity.Property(e => e.ContactPersonLineid)
                    .HasColumnName("CONTACT_PERSON_LINEID")
                    .HasColumnType("VARCHAR2(128)");

                entity.Property(e => e.ContactPersonMobile)
                    .HasColumnName("CONTACT_PERSON_MOBILE")
                    .HasColumnType("VARCHAR2(128)");

                entity.Property(e => e.ContactPersonName)
                    .HasColumnName("CONTACT_PERSON_NAME")
                    .HasColumnType("VARCHAR2(256)");

                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("CREATED_DATE")
                    .HasColumnType("TIMESTAMP(6)");

                entity.Property(e => e.EventCode)
                    .IsRequired()
                    .HasColumnName("EVENT_CODE")
                    .HasColumnType("VARCHAR2(32)");

                entity.Property(e => e.EventDescription)
                    .IsRequired()
                    .HasColumnName("EVENT_DESCRIPTION")
                    .HasColumnType("VARCHAR2(2048)");

                entity.Property(e => e.EventFinishTimestamp)
                    .HasColumnName("EVENT_FINISH_TIMESTAMP")
                    .HasColumnType("TIMESTAMP(6)");

                entity.Property(e => e.EventLevelEtc)
                    .HasColumnName("EVENT_LEVEL_ETC")
                    .HasColumnType("VARCHAR2(255)");

                entity.Property(e => e.EventLevelId).HasColumnName("EVENT_LEVEL_ID");

                entity.Property(e => e.EventName)
                    .IsRequired()
                    .HasColumnName("EVENT_NAME")
                    .HasColumnType("VARCHAR2(255)");

                entity.Property(e => e.EventObjectivePersonId).HasColumnName("EVENT_OBJECTIVE_PERSON_ID");

                entity.Property(e => e.EventShortDescription)
                    .IsRequired()
                    .HasColumnName("EVENT_SHORT_DESCRIPTION")
                    .HasColumnType("VARCHAR2(255)");

                entity.Property(e => e.EventStartTimestamp)
                    .HasColumnName("EVENT_START_TIMESTAMP")
                    .HasColumnType("TIMESTAMP(6)");

                entity.Property(e => e.EventTypeId).HasColumnName("EVENT_TYPE_ID");

                entity.Property(e => e.ProjectCode)
                    .HasColumnName("PROJECT_CODE")
                    .HasColumnType("VARCHAR2(32)");

                entity.Property(e => e.ProjectSelect)
                    .HasColumnName("PROJECT_SELECT")
                    .HasColumnType("CHAR(1)");

                entity.Property(e => e.PublishUrl)
                    .IsRequired()
                    .HasColumnName("PUBLISH_URL")
                    .HasColumnType("VARCHAR2(255)");

                entity.Property(e => e.ReadCount).HasColumnName("READ_COUNT");

                entity.Property(e => e.ResponsiblePerson)
                    .HasColumnName("RESPONSIBLE_PERSON")
                    .HasColumnType("VARCHAR2(255)");

                entity.Property(e => e.ResponsiblePersonCode)
                    .HasColumnName("RESPONSIBLE_PERSON_CODE")
                    .HasColumnType("VARCHAR2(32)");

                entity.Property(e => e.ResponsiblePersonType)
                    .HasColumnName("RESPONSIBLE_PERSON_TYPE")
                    .HasColumnType("CHAR(1)");

                entity.Property(e => e.StadiumCode)
                    .HasColumnName("STADIUM_CODE")
                    .HasColumnType("VARCHAR2(32)");

                entity.Property(e => e.Status).HasColumnName("STATUS");

                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("UPDATED_DATE")
                    .HasColumnType("TIMESTAMP(6)");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Event)
                    .HasForeignKey(d => d.AddressId)
                    .HasConstraintName("EVENT_R02");

                entity.HasOne(d => d.EventLevel)
                    .WithMany(p => p.Event)
                    .HasForeignKey(d => d.EventLevelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("EVENT_R01");

                entity.HasOne(d => d.EventType)
                    .WithMany(p => p.Event)
                    .HasForeignKey(d => d.EventTypeId)
                    .HasConstraintName("FK_M_EVENT_TYPE");
            });

            modelBuilder.Entity<EventFacilities>(entity =>
            {
                entity.ToTable("EVENT_FACILITIES");

                entity.HasIndex(e => e.EventFacilitiesId)
                    .HasName("PK_EVENT_FACILITIES")
                    .IsUnique();

                entity.Property(e => e.EventFacilitiesId)
                    .HasColumnName("EVENT_FACILITIES_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("CREATED_DATE")
                    .HasColumnType("TIMESTAMP(6)");

                entity.Property(e => e.EventFacilitiesName)
                    .HasColumnName("EVENT_FACILITIES_NAME")
                    .HasColumnType("VARCHAR2(255)");

                entity.Property(e => e.EventId).HasColumnName("EVENT_ID");

                entity.Property(e => e.FacilitiesAmount).HasColumnName("FACILITIES_AMOUNT");

                entity.Property(e => e.FacilitiesUnit)
                    .HasColumnName("FACILITIES_UNIT")
                    .HasColumnType("VARCHAR2(255)");

                entity.Property(e => e.MEventFacilitiesTopicId).HasColumnName("M_EVENT_FACILITIES_TOPIC_ID");

                entity.Property(e => e.Status).HasColumnName("STATUS");

                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("UPDATED_DATE")
                    .HasColumnType("TIMESTAMP(6)");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.EventFacilities)
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SYS_C00111083");

                entity.HasOne(d => d.MEventFacilitiesTopic)
                    .WithMany(p => p.EventFacilities)
                    .HasForeignKey(d => d.MEventFacilitiesTopicId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SYS_C00111084");
            });

            modelBuilder.Entity<EventGoal>(entity =>
            {
                entity.ToTable("EVENT_GOAL");

                entity.HasIndex(e => e.EventGoalId)
                    .HasName("PK_EVENT_GOAL")
                    .IsUnique();

                entity.Property(e => e.EventGoalId)
                    .HasColumnName("EVENT_GOAL_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("CREATED_DATE")
                    .HasColumnType("TIMESTAMP(6)");

                entity.Property(e => e.EventId).HasColumnName("EVENT_ID");

                entity.Property(e => e.MEventGoalId).HasColumnName("M_EVENT_GOAL_ID");

                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("UPDATED_DATE")
                    .HasColumnType("TIMESTAMP(6)");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.EventGoal)
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SYS_C00111024");

                entity.HasOne(d => d.MEventGoal)
                    .WithMany(p => p.EventGoal)
                    .HasForeignKey(d => d.MEventGoalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SYS_C00111025");
            });

            modelBuilder.Entity<EventJoinPersonType>(entity =>
            {
                entity.ToTable("EVENT_JOIN_PERSON_TYPE");

                entity.HasIndex(e => e.EventJoinPersonTypeId)
                    .HasName("PK_EVENT_JOIN_PERSON_TYPE")
                    .IsUnique();

                entity.Property(e => e.EventJoinPersonTypeId).HasColumnName("EVENT_JOIN_PERSON_TYPE_ID");

                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("CREATED_DATE")
                    .HasColumnType("TIMESTAMP(6)");

                entity.Property(e => e.EventId).HasColumnName("EVENT_ID");

                entity.Property(e => e.EventJoinPersonTypeCode)
                    .IsRequired()
                    .HasColumnName("EVENT_JOIN_PERSON_TYPE_CODE")
                    .HasColumnType("VARCHAR2(32)");

                entity.Property(e => e.JoinPersonTypeCount).HasColumnName("JOIN_PERSON_TYPE_COUNT");

                entity.Property(e => e.JoinPersonTypeId).HasColumnName("JOIN_PERSON_TYPE_ID");

                entity.Property(e => e.Status).HasColumnName("STATUS");

                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("UPDATED_DATE")
                    .HasColumnType("TIMESTAMP(6)");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.EventJoinPersonType)
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("EVENT_JOIN_PERSON_TYPE_R01");

                entity.HasOne(d => d.JoinPersonType)
                    .WithMany(p => p.EventJoinPersonType)
                    .HasForeignKey(d => d.JoinPersonTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("EVENT_JOIN_PERSON_TYPE_R02");
            });

            modelBuilder.Entity<EventLevel>(entity =>
            {
                entity.ToTable("EVENT_LEVEL");

                entity.HasIndex(e => e.EventLevelId)
                    .HasName("PK_EVENT_LEVEL")
                    .IsUnique();

                entity.Property(e => e.EventLevelId)
                    .HasColumnName("EVENT_LEVEL_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("CREATED_DATE")
                    .HasColumnType("TIMESTAMP(6)");

                entity.Property(e => e.EventId).HasColumnName("EVENT_ID");

                entity.Property(e => e.MEventLevelId).HasColumnName("M_EVENT_LEVEL_ID");

                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("UPDATED_DATE")
                    .HasColumnType("TIMESTAMP(6)");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.EventLevelNavigation)
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SYS_C00111026");

                entity.HasOne(d => d.MEventLevel)
                    .WithMany(p => p.EventLevel)
                    .HasForeignKey(d => d.MEventLevelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SYS_C00111027");
            });

            modelBuilder.Entity<EventObjective>(entity =>
            {
                entity.ToTable("EVENT_OBJECTIVE");

                entity.HasIndex(e => e.EventObjectiveId)
                    .HasName("PK_EVENT_OBJECTIVE")
                    .IsUnique();

                entity.Property(e => e.EventObjectiveId)
                    .HasColumnName("EVENT_OBJECTIVE_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("CREATED_DATE")
                    .HasColumnType("TIMESTAMP(6)");

                entity.Property(e => e.EventId).HasColumnName("EVENT_ID");

                entity.Property(e => e.MEventObjectiveId).HasColumnName("M_EVENT_OBJECTIVE_ID");

                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("UPDATED_DATE")
                    .HasColumnType("TIMESTAMP(6)");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.EventObjective)
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SYS_C00111022");

                entity.HasOne(d => d.MEventObjective)
                    .WithMany(p => p.EventObjective)
                    .HasForeignKey(d => d.MEventObjectiveId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SYS_C00111023");
            });

            modelBuilder.Entity<EventSport>(entity =>
            {
                entity.ToTable("EVENT_SPORT");

                entity.HasIndex(e => e.EventSportId)
                    .HasName("PK_EVENT_SPORT")
                    .IsUnique();

                entity.Property(e => e.EventSportId).HasColumnName("EVENT_SPORT_ID");

                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("CREATED_DATE")
                    .HasColumnType("TIMESTAMP(6)");

                entity.Property(e => e.EventId).HasColumnName("EVENT_ID");

                entity.Property(e => e.EventSportCode)
                    .IsRequired()
                    .HasColumnName("EVENT_SPORT_CODE")
                    .HasColumnType("VARCHAR2(32)");

                entity.Property(e => e.SportEtc)
                    .HasColumnName("SPORT_ETC")
                    .HasColumnType("VARCHAR2(255)");

                entity.Property(e => e.SportId).HasColumnName("SPORT_ID");

                entity.Property(e => e.Status).HasColumnName("STATUS");

                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("UPDATED_DATE")
                    .HasColumnType("TIMESTAMP(6)");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.EventSport)
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SYS_C00111078");

                entity.HasOne(d => d.Sport)
                    .WithMany(p => p.EventSport)
                    .HasForeignKey(d => d.SportId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SYS_C00111082");
            });

            modelBuilder.Entity<EventUploadedFile>(entity =>
            {
                entity.ToTable("EVENT_UPLOADED_FILE");

                entity.HasIndex(e => e.EventUploadedFileId)
                    .HasName("PK_EVENT_UPLOADED_FILE")
                    .IsUnique();

                entity.Property(e => e.EventUploadedFileId).HasColumnName("EVENT_UPLOADED_FILE_ID");

                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("CREATED_DATE")
                    .HasColumnType("TIMESTAMP(6)");

                entity.Property(e => e.EventId).HasColumnName("EVENT_ID");

                entity.Property(e => e.EventUploadedFileCode)
                    .IsRequired()
                    .HasColumnName("EVENT_UPLOADED_FILE_CODE")
                    .HasColumnType("VARCHAR2(32)");

                entity.Property(e => e.Order).HasColumnName("ORDER");

                entity.Property(e => e.Status).HasColumnName("STATUS");

                entity.Property(e => e.UploadedFileId).HasColumnName("UPLOADED_FILE_ID");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.EventUploadedFile)
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("EVENT_UPLOADED_FILE_R01");

                entity.HasOne(d => d.UploadedFile)
                    .WithMany(p => p.EventUploadedFile)
                    .HasForeignKey(d => d.UploadedFileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("EVENT_UPLOADED_FILE_R02");
            });

            modelBuilder.Entity<GenerateCode>(entity =>
            {
                entity.HasKey(e => e.GenerateId);

                entity.ToTable("GENERATE_CODE");

                entity.HasIndex(e => e.GenerateId)
                    .HasName("PK_GENERATE_CODE")
                    .IsUnique();

                entity.Property(e => e.GenerateId).HasColumnName("GENERATE_ID");

                entity.Property(e => e.CreateBy)
                    .IsRequired()
                    .HasColumnName("CREATE_BY")
                    .HasColumnType("VARCHAR2(50)");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("CREATE_DATE")
                    .HasColumnType("TIMESTAMP(6)");

                entity.Property(e => e.GenerateKey)
                    .IsRequired()
                    .HasColumnName("GENERATE_KEY")
                    .HasColumnType("VARCHAR2(25)");

                entity.Property(e => e.GenerateValue)
                    .HasColumnName("GENERATE_VALUE")
                    .HasColumnType("NUMBER(18)");

                entity.Property(e => e.IsDelete)
                    .HasColumnName("IS_DELETE")
                    .HasColumnType("CHAR(1)");

                entity.Property(e => e.UpdateBy)
                    .HasColumnName("UPDATE_BY")
                    .HasColumnType("VARCHAR2(50)");

                entity.Property(e => e.UpdateDate)
                    .HasColumnName("UPDATE_DATE")
                    .HasColumnType("TIMESTAMP(6)");

                entity.Property(e => e.Years)
                    .HasColumnName("YEARS")
                    .HasColumnType("VARCHAR2(8)");
            });

            modelBuilder.Entity<MAccountType>(entity =>
            {
                entity.HasKey(e => e.AccountTypeId);

                entity.ToTable("M_ACCOUNT_TYPE");

                entity.HasIndex(e => e.AccountTypeId)
                    .HasName("ACCOUNT_TYPE_ID")
                    .IsUnique();

                entity.Property(e => e.AccountTypeId)
                    .HasColumnName("ACCOUNT_TYPE_ID")
                    .HasColumnType("VARCHAR2(2)")
                    .ValueGeneratedNever();

                entity.Property(e => e.AccountTypeName)
                    .IsRequired()
                    .HasColumnName("ACCOUNT_TYPE_NAME")
                    .HasColumnType("VARCHAR2(100)");

                entity.Property(e => e.DefaultGroupId).HasColumnName("DEFAULT_GROUP_ID");

                entity.Property(e => e.RequireProfileBoo)
                    .HasColumnName("REQUIRE_PROFILE_BOO")
                    .HasColumnType("VARCHAR2(1)");
            });

            modelBuilder.Entity<MAddressType>(entity =>
            {
                entity.HasKey(e => e.AddressTypeId);

                entity.ToTable("M_ADDRESS_TYPE");

                entity.HasIndex(e => e.AddressTypeId)
                    .HasName("PK_M_ADDRESS_TYPE")
                    .IsUnique();

                entity.Property(e => e.AddressTypeId).HasColumnName("ADDRESS_TYPE_ID");

                entity.Property(e => e.AddressType)
                    .IsRequired()
                    .HasColumnName("ADDRESS_TYPE")
                    .HasColumnType("VARCHAR2(255)");

                entity.Property(e => e.AddressTypeCode)
                    .IsRequired()
                    .HasColumnName("ADDRESS_TYPE_CODE")
                    .HasColumnType("VARCHAR2(32)");

                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("CREATED_DATE")
                    .HasColumnType("TIMESTAMP(6)");

                entity.Property(e => e.Status).HasColumnName("STATUS");

                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("UPDATED_DATE")
                    .HasColumnType("TIMESTAMP(6)");
            });

            modelBuilder.Entity<MDepartmentEventSubtype>(entity =>
            {
                entity.HasKey(e => e.DepartmentEventSubtypeId);

                entity.ToTable("M_DEPARTMENT_EVENT_SUBTYPE");

                entity.HasIndex(e => e.DepartmentEventSubtypeId)
                    .HasName("PK_M_DEPARTMENT_EVENT_SUBTYPE")
                    .IsUnique();

                entity.Property(e => e.DepartmentEventSubtypeId).HasColumnName("DEPARTMENT_EVENT_SUBTYPE_ID");

                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("CREATED_DATE")
                    .HasColumnType("TIMESTAMP(6)");

                entity.Property(e => e.DepartmentEventSubtypeCode)
                    .IsRequired()
                    .HasColumnName("DEPARTMENT_EVENT_SUBTYPE_CODE")
                    .HasColumnType("VARCHAR2(32)");

                entity.Property(e => e.DepartmentEventSubtypeName)
                    .IsRequired()
                    .HasColumnName("DEPARTMENT_EVENT_SUBTYPE_NAME")
                    .HasColumnType("VARCHAR2(255)");

                entity.Property(e => e.Status).HasColumnName("STATUS");

                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("UPDATED_DATE")
                    .HasColumnType("TIMESTAMP(6)");
            });

            modelBuilder.Entity<MEventFacilitiesTopic>(entity =>
            {
                entity.HasKey(e => e.EventFacilitiesTopicId);

                entity.ToTable("M_EVENT_FACILITIES_TOPIC");

                entity.HasIndex(e => e.EventFacilitiesTopicId)
                    .HasName("PK_M_EVENT_FAC_TOPIC")
                    .IsUnique();

                entity.Property(e => e.EventFacilitiesTopicId)
                    .HasColumnName("EVENT_FACILITIES_TOPIC_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("CREATED_DATE")
                    .HasColumnType("TIMESTAMP(6)");

                entity.Property(e => e.EventFacilitiesTopicCode)
                    .IsRequired()
                    .HasColumnName("EVENT_FACILITIES_TOPIC_CODE")
                    .HasColumnType("VARCHAR2(32)");

                entity.Property(e => e.EventFacilitiesTopicName)
                    .HasColumnName("EVENT_FACILITIES_TOPIC_NAME")
                    .HasColumnType("VARCHAR2(255)");

                entity.Property(e => e.Status).HasColumnName("STATUS");

                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("UPDATED_DATE")
                    .HasColumnType("TIMESTAMP(6)");
            });

            modelBuilder.Entity<MEventFacilitiesXxxxdelete>(entity =>
            {
                entity.HasKey(e => e.EventFacilitiesId);

                entity.ToTable("M_EVENT_FACILITIES_XXXXDELETE");

                entity.HasIndex(e => e.EventFacilitiesId)
                    .HasName("PK_M_EVENT_FACILITIES")
                    .IsUnique();

                entity.Property(e => e.EventFacilitiesId)
                    .HasColumnName("EVENT_FACILITIES_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("CREATED_DATE")
                    .HasColumnType("TIMESTAMP(6)");

                entity.Property(e => e.EventFacilitiesCode)
                    .IsRequired()
                    .HasColumnName("EVENT_FACILITIES_CODE")
                    .HasColumnType("VARCHAR2(32)");

                entity.Property(e => e.EventFacilitiesName)
                    .HasColumnName("EVENT_FACILITIES_NAME")
                    .HasColumnType("VARCHAR2(255)");

                entity.Property(e => e.EventFacilitiesTopicId).HasColumnName("EVENT_FACILITIES_TOPIC_ID");

                entity.Property(e => e.Status).HasColumnName("STATUS");

                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("UPDATED_DATE")
                    .HasColumnType("TIMESTAMP(6)");
            });

            modelBuilder.Entity<MEventGoal>(entity =>
            {
                entity.HasKey(e => e.EventGoalId);

                entity.ToTable("M_EVENT_GOAL");

                entity.HasIndex(e => e.EventGoalId)
                    .HasName("PK_M_EVENT_GOAL")
                    .IsUnique();

                entity.Property(e => e.EventGoalId)
                    .HasColumnName("EVENT_GOAL_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("CREATED_DATE")
                    .HasColumnType("TIMESTAMP(6)");

                entity.Property(e => e.EventGoalCode)
                    .IsRequired()
                    .HasColumnName("EVENT_GOAL_CODE")
                    .HasColumnType("VARCHAR2(32)");

                entity.Property(e => e.EventGoalName)
                    .HasColumnName("EVENT_GOAL_NAME")
                    .HasColumnType("VARCHAR2(255)");

                entity.Property(e => e.Status).HasColumnName("STATUS");

                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("UPDATED_DATE")
                    .HasColumnType("TIMESTAMP(6)");
            });

            modelBuilder.Entity<MEventLevel>(entity =>
            {
                entity.HasKey(e => e.EventLevelId);

                entity.ToTable("M_EVENT_LEVEL");

                entity.HasIndex(e => e.EventLevelId)
                    .HasName("PK_M_EVENT_LEVEL")
                    .IsUnique();

                entity.Property(e => e.EventLevelId).HasColumnName("EVENT_LEVEL_ID");

                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("CREATED_DATE")
                    .HasColumnType("TIMESTAMP(6)");

                entity.Property(e => e.EventLevelCode)
                    .IsRequired()
                    .HasColumnName("EVENT_LEVEL_CODE")
                    .HasColumnType("VARCHAR2(32)");

                entity.Property(e => e.EventLevelName)
                    .HasColumnName("EVENT_LEVEL_NAME")
                    .HasColumnType("VARCHAR2(255)");

                entity.Property(e => e.Status).HasColumnName("STATUS");

                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("UPDATED_DATE")
                    .HasColumnType("TIMESTAMP(6)");
            });

            modelBuilder.Entity<MEventObjective>(entity =>
            {
                entity.HasKey(e => e.EventObjectiveId);

                entity.ToTable("M_EVENT_OBJECTIVE");

                entity.HasIndex(e => e.EventObjectiveId)
                    .HasName("PK_M_EVENT_OBJECTIVE")
                    .IsUnique();

                entity.Property(e => e.EventObjectiveId)
                    .HasColumnName("EVENT_OBJECTIVE_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("CREATED_DATE")
                    .HasColumnType("TIMESTAMP(6)");

                entity.Property(e => e.EventObjectiveCode)
                    .IsRequired()
                    .HasColumnName("EVENT_OBJECTIVE_CODE")
                    .HasColumnType("VARCHAR2(32)");

                entity.Property(e => e.EventObjectiveName)
                    .HasColumnName("EVENT_OBJECTIVE_NAME")
                    .HasColumnType("VARCHAR2(255)");

                entity.Property(e => e.Status).HasColumnName("STATUS");

                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("UPDATED_DATE")
                    .HasColumnType("TIMESTAMP(6)");
            });

            modelBuilder.Entity<MEventObjectivePerson>(entity =>
            {
                entity.HasKey(e => e.EventObjectivePersonId);

                entity.ToTable("M_EVENT_OBJECTIVE_PERSON");

                entity.HasIndex(e => e.EventObjectivePersonId)
                    .HasName("PK_M_EVENT_OBJECTIVE_PERSON")
                    .IsUnique();

                entity.Property(e => e.EventObjectivePersonId).HasColumnName("EVENT_OBJECTIVE_PERSON_ID");

                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("CREATED_DATE")
                    .HasColumnType("TIMESTAMP(6)");

                entity.Property(e => e.EventObjectivePersonCode)
                    .IsRequired()
                    .HasColumnName("EVENT_OBJECTIVE_PERSON_CODE")
                    .HasColumnType("VARCHAR2(32)");

                entity.Property(e => e.EventObjectivePersonName)
                    .HasColumnName("EVENT_OBJECTIVE_PERSON_NAME")
                    .HasColumnType("VARCHAR2(255)");

                entity.Property(e => e.Status).HasColumnName("STATUS");

                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("UPDATED_DATE")
                    .HasColumnType("TIMESTAMP(6)");
            });

            modelBuilder.Entity<MEventType>(entity =>
            {
                entity.HasKey(e => e.EventTypeId);

                entity.ToTable("M_EVENT_TYPE");

                entity.HasIndex(e => e.EventTypeId)
                    .HasName("PK_M_EVENT_TYPE")
                    .IsUnique();

                entity.Property(e => e.EventTypeId)
                    .HasColumnName("EVENT_TYPE_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("CREATED_DATE")
                    .HasColumnType("TIMESTAMP(6)");

                entity.Property(e => e.EventTypeCode)
                    .IsRequired()
                    .HasColumnName("EVENT_TYPE_CODE")
                    .HasColumnType("VARCHAR2(32)");

                entity.Property(e => e.EventTypeName)
                    .HasColumnName("EVENT_TYPE_NAME")
                    .HasColumnType("VARCHAR2(255)");

                entity.Property(e => e.Status).HasColumnName("STATUS");

                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("UPDATED_DATE")
                    .HasColumnType("TIMESTAMP(6)");
            });

            modelBuilder.Entity<MGroup>(entity =>
            {
                entity.HasKey(e => e.GroupId);

                entity.ToTable("M_GROUP");

                entity.HasIndex(e => e.GroupId)
                    .HasName("PK_M_GROUP")
                    .IsUnique();

                entity.Property(e => e.GroupId).HasColumnName("GROUP_ID");

                entity.Property(e => e.CreateBy).HasColumnName("CREATE_BY");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("CREATE_DATE")
                    .HasColumnType("TIMESTAMP(6)");

                entity.Property(e => e.Description)
                    .HasColumnName("DESCRIPTION")
                    .HasColumnType("VARCHAR2(1000)");

                entity.Property(e => e.GroupName)
                    .IsRequired()
                    .HasColumnName("GROUP_NAME")
                    .HasColumnType("VARCHAR2(200)");

                entity.Property(e => e.Status).HasColumnName("STATUS");

                entity.Property(e => e.UpdateBy).HasColumnName("UPDATE_BY");

                entity.Property(e => e.UpdateDate)
                    .HasColumnName("UPDATE_DATE")
                    .HasColumnType("TIMESTAMP(6)");
            });

            modelBuilder.Entity<MGroupRole>(entity =>
            {
                entity.HasKey(e => new { e.GroupId, e.RoleId });

                entity.ToTable("M_GROUP_ROLE");

                entity.HasIndex(e => new { e.GroupId, e.RoleId })
                    .HasName("M_GROUP_ROLE_PK")
                    .IsUnique();

                entity.Property(e => e.GroupId).HasColumnName("GROUP_ID");

                entity.Property(e => e.RoleId).HasColumnName("ROLE_ID");
            });

            modelBuilder.Entity<MIdcardType>(entity =>
            {
                entity.HasKey(e => e.IdcardType);

                entity.ToTable("M_IDCARD_TYPE");

                entity.HasIndex(e => e.IdcardType)
                    .HasName("PK_M_IDCARD_TYPE")
                    .IsUnique();

                entity.Property(e => e.IdcardType)
                    .HasColumnName("IDCARD_TYPE")
                    .HasColumnType("VARCHAR2(1)")
                    .ValueGeneratedNever();

                entity.Property(e => e.IdcardName)
                    .IsRequired()
                    .HasColumnName("IDCARD_NAME")
                    .HasColumnType("VARCHAR2(100)");
            });

            modelBuilder.Entity<MJoinPersonType>(entity =>
            {
                entity.HasKey(e => e.JoinPersonTypeId);

                entity.ToTable("M_JOIN_PERSON_TYPE");

                entity.HasIndex(e => e.JoinPersonTypeId)
                    .HasName("PK_M_JOIN_PERSON_TYPE")
                    .IsUnique();

                entity.Property(e => e.JoinPersonTypeId).HasColumnName("JOIN_PERSON_TYPE_ID");

                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("CREATED_DATE")
                    .HasColumnType("TIMESTAMP(6)");

                entity.Property(e => e.JoinPersonName)
                    .HasColumnName("JOIN_PERSON_NAME")
                    .HasColumnType("VARCHAR2(255)");

                entity.Property(e => e.JoinPersonTypeCode)
                    .HasColumnName("JOIN_PERSON_TYPE_CODE")
                    .HasColumnType("VARCHAR2(32)");

                entity.Property(e => e.Status).HasColumnName("STATUS");

                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("UPDATED_DATE")
                    .HasColumnType("TIMESTAMP(6)");
            });

            modelBuilder.Entity<MPermissionGroup>(entity =>
            {
                entity.HasKey(e => e.PermissionGroupId);

                entity.ToTable("M_PERMISSION_GROUP");

                entity.HasIndex(e => e.PermissionGroupId)
                    .HasName("PK_M_PERMISSION_GROUP")
                    .IsUnique();

                entity.Property(e => e.PermissionGroupId).HasColumnName("PERMISSION_GROUP_ID");

                entity.Property(e => e.CreateBy).HasColumnName("CREATE_BY");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("CREATE_DATE")
                    .HasColumnType("TIMESTAMP(6)");

                entity.Property(e => e.Description)
                    .HasColumnName("DESCRIPTION")
                    .HasColumnType("VARCHAR2(1000)");

                entity.Property(e => e.MPermissionGroupCode)
                    .HasColumnName("M_PERMISSION_GROUP_CODE")
                    .HasColumnType("VARCHAR2(50)");

                entity.Property(e => e.MPermissionGroupName)
                    .IsRequired()
                    .HasColumnName("M_PERMISSION_GROUP_NAME")
                    .HasColumnType("VARCHAR2(200)");

                entity.Property(e => e.Status).HasColumnName("STATUS");

                entity.Property(e => e.UpdateBy).HasColumnName("UPDATE_BY");

                entity.Property(e => e.UpdateDate)
                    .HasColumnName("UPDATE_DATE")
                    .HasColumnType("TIMESTAMP(6)");
            });

            modelBuilder.Entity<MPermissiongroupProgram>(entity =>
            {
                entity.HasKey(e => e.PermissiongroupProgramId);

                entity.ToTable("M_PERMISSIONGROUP_PROGRAM");

                entity.HasIndex(e => e.PermissiongroupProgramId)
                    .HasName("PK_PERMISSIONGROUP_PROGRAM")
                    .IsUnique();

                entity.Property(e => e.PermissiongroupProgramId).HasColumnName("PERMISSIONGROUP_PROGRAM_ID");

                entity.Property(e => e.Canedit)
                    .HasColumnName("CANEDIT")
                    .HasColumnType("CHAR(1)");

                entity.Property(e => e.Canexcel)
                    .HasColumnName("CANEXCEL")
                    .HasColumnType("CHAR(1)");

                entity.Property(e => e.Canpdf)
                    .HasColumnName("CANPDF")
                    .HasColumnType("CHAR(1)");

                entity.Property(e => e.Canview)
                    .HasColumnName("CANVIEW")
                    .HasColumnType("CHAR(1)");

                entity.Property(e => e.CreateBy).HasColumnName("CREATE_BY");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("CREATE_DATE")
                    .HasColumnType("TIMESTAMP(6)");

                entity.Property(e => e.MPermissionGroupId).HasColumnName("M_PERMISSION_GROUP_ID");

                entity.Property(e => e.ProgramId).HasColumnName("PROGRAM_ID");

                entity.Property(e => e.Status).HasColumnName("STATUS");

                entity.Property(e => e.UpdateBy).HasColumnName("UPDATE_BY");

                entity.Property(e => e.UpdateDate)
                    .HasColumnName("UPDATE_DATE")
                    .HasColumnType("TIMESTAMP(6)");

                entity.HasOne(d => d.MPermissionGroup)
                    .WithMany(p => p.MPermissiongroupProgram)
                    .HasForeignKey(d => d.MPermissionGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_M_PERMISSION_GROUP");

                entity.HasOne(d => d.Program)
                    .WithMany(p => p.MPermissiongroupProgram)
                    .HasForeignKey(d => d.ProgramId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_M_PROGRAM");
            });

            modelBuilder.Entity<MPhoneNumberType>(entity =>
            {
                entity.HasKey(e => e.PhoneNumberTypeId);

                entity.ToTable("M_PHONE_NUMBER_TYPE");

                entity.HasIndex(e => e.PhoneNumberTypeId)
                    .HasName("PK_M_PHONE_NUMBER_TYPE")
                    .IsUnique();

                entity.Property(e => e.PhoneNumberTypeId).HasColumnName("PHONE_NUMBER_TYPE_ID");

                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("CREATED_DATE")
                    .HasColumnType("TIMESTAMP(6)");

                entity.Property(e => e.PhoneNumberType)
                    .IsRequired()
                    .HasColumnName("PHONE_NUMBER_TYPE")
                    .HasColumnType("VARCHAR2(255)");

                entity.Property(e => e.PhoneNumberTypeCode)
                    .IsRequired()
                    .HasColumnName("PHONE_NUMBER_TYPE_CODE")
                    .HasColumnType("VARCHAR2(32)");

                entity.Property(e => e.Status).HasColumnName("STATUS");

                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("UPDATED_DATE")
                    .HasColumnType("TIMESTAMP(6)");
            });

            modelBuilder.Entity<MProgram>(entity =>
            {
                entity.HasKey(e => e.ProgramId);

                entity.ToTable("M_PROGRAM");

                entity.HasIndex(e => e.ProgramId)
                    .HasName("PK_M_PROGRAM")
                    .IsUnique();

                entity.Property(e => e.ProgramId).HasColumnName("PROGRAM_ID");

                entity.Property(e => e.CreateBy).HasColumnName("CREATE_BY");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("CREATE_DATE")
                    .HasColumnType("TIMESTAMP(6)");

                entity.Property(e => e.Description)
                    .HasColumnName("DESCRIPTION")
                    .HasColumnType("VARCHAR2(1000)");

                entity.Property(e => e.ProgramCode)
                    .HasColumnName("PROGRAM_CODE")
                    .HasColumnType("VARCHAR2(50)");

                entity.Property(e => e.ProgramName)
                    .IsRequired()
                    .HasColumnName("PROGRAM_NAME")
                    .HasColumnType("VARCHAR2(200)");

                entity.Property(e => e.Status).HasColumnName("STATUS");

                entity.Property(e => e.UpdateBy).HasColumnName("UPDATE_BY");

                entity.Property(e => e.UpdateDate)
                    .HasColumnName("UPDATE_DATE")
                    .HasColumnType("TIMESTAMP(6)");
            });

            modelBuilder.Entity<MRole>(entity =>
            {
                entity.HasKey(e => e.RoleId);

                entity.ToTable("M_ROLE");

                entity.HasIndex(e => e.RoleId)
                    .HasName("M_ROLE_PK")
                    .IsUnique();

                entity.Property(e => e.RoleId)
                    .HasColumnName("ROLE_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreateBy).HasColumnName("CREATE_BY");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("CREATE_DATE")
                    .HasColumnType("TIMESTAMP(6)");

                entity.Property(e => e.Description)
                    .HasColumnName("DESCRIPTION")
                    .HasColumnType("VARCHAR2(1000)");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasColumnName("ROLE_NAME")
                    .HasColumnType("VARCHAR2(200)");

                entity.Property(e => e.Status).HasColumnName("STATUS");

                entity.Property(e => e.UpdateBy).HasColumnName("UPDATE_BY");

                entity.Property(e => e.UpdateDate)
                    .HasColumnName("UPDATE_DATE")
                    .HasColumnType("TIMESTAMP(6)");
            });

            modelBuilder.Entity<MSport>(entity =>
            {
                entity.HasKey(e => e.SportId);

                entity.ToTable("M_SPORT");

                entity.HasIndex(e => e.SportId)
                    .HasName("PK_M_SPORT")
                    .IsUnique();

                entity.Property(e => e.SportId).HasColumnName("SPORT_ID");

                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("CREATED_DATE")
                    .HasColumnType("TIMESTAMP(6)");

                entity.Property(e => e.SportCode)
                    .HasColumnName("SPORT_CODE")
                    .HasColumnType("VARCHAR2(5)");

                entity.Property(e => e.SportName)
                    .HasColumnName("SPORT_NAME")
                    .HasColumnType("VARCHAR2(255)");

                entity.Property(e => e.Status).HasColumnName("STATUS");

                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("UPDATED_DATE")
                    .HasColumnType("TIMESTAMP(6)");
            });

            modelBuilder.Entity<MStatus>(entity =>
            {
                entity.HasKey(e => e.StatusId);

                entity.ToTable("M_STATUS");

                entity.HasIndex(e => e.StatusId)
                    .HasName("PK_M_STATUS")
                    .IsUnique();

                entity.Property(e => e.StatusId).HasColumnName("STATUS_ID");

                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("CREATED_DATE")
                    .HasColumnType("TIMESTAMP(6)");

                entity.Property(e => e.StatusCode)
                    .IsRequired()
                    .HasColumnName("STATUS_CODE")
                    .HasColumnType("VARCHAR2(32)");

                entity.Property(e => e.StatusName)
                    .IsRequired()
                    .HasColumnName("STATUS_NAME")
                    .HasColumnType("VARCHAR2(50)");

                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("UPDATED_DATE")
                    .HasColumnType("TIMESTAMP(6)");
            });

            modelBuilder.Entity<MUserPermissiongroup>(entity =>
            {
                entity.HasKey(e => e.UserPermissiongroupId);

                entity.ToTable("M_USER_PERMISSIONGROUP");

                entity.HasIndex(e => e.UserPermissiongroupId)
                    .HasName("PK_M_USER_PERMISSIONGROUP")
                    .IsUnique();

                entity.Property(e => e.UserPermissiongroupId).HasColumnName("USER_PERMISSIONGROUP_ID");

                entity.Property(e => e.Comment)
                    .HasColumnName("COMMENT")
                    .HasColumnType("VARCHAR2(512)");

                entity.Property(e => e.CreateBy).HasColumnName("CREATE_BY");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("CREATE_DATE")
                    .HasColumnType("TIMESTAMP(6)");

                entity.Property(e => e.EndDate)
                    .HasColumnName("END_DATE")
                    .HasColumnType("TIMESTAMP(6)");

                entity.Property(e => e.MPermissionGroupId).HasColumnName("M_PERMISSION_GROUP_ID");

                entity.Property(e => e.StartDate)
                    .HasColumnName("START_DATE")
                    .HasColumnType("TIMESTAMP(6)");

                entity.Property(e => e.Status).HasColumnName("STATUS");

                entity.Property(e => e.UpdateBy).HasColumnName("UPDATE_BY");

                entity.Property(e => e.UpdateDate)
                    .HasColumnName("UPDATE_DATE")
                    .HasColumnType("TIMESTAMP(6)");

                entity.Property(e => e.UserId).HasColumnName("USER_ID");

                entity.HasOne(d => d.MPermissionGroup)
                    .WithMany(p => p.MUserPermissiongroup)
                    .HasForeignKey(d => d.MPermissionGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_M_U_PERM_M_PERM_GROUP");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.MUserPermissiongroup)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_M_USER_PERM_USER");
            });

            modelBuilder.Entity<MUserType>(entity =>
            {
                entity.HasKey(e => e.UserTypeId);

                entity.ToTable("M_USER_TYPE");

                entity.HasIndex(e => e.UserTypeId)
                    .HasName("PK_M_USER_TYPE")
                    .IsUnique();

                entity.Property(e => e.UserTypeId).HasColumnName("USER_TYPE_ID");

                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("CREATED_DATE")
                    .HasColumnType("TIMESTAMP(6)");

                entity.Property(e => e.Status).HasColumnName("STATUS");

                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("UPDATED_DATE")
                    .HasColumnType("TIMESTAMP(6)");

                entity.Property(e => e.UserTypeCode)
                    .IsRequired()
                    .HasColumnName("USER_TYPE_CODE")
                    .HasColumnType("VARCHAR2(32)");

                entity.Property(e => e.UserTypeName)
                    .HasColumnName("USER_TYPE_NAME")
                    .HasColumnType("VARCHAR2(100)");
            });

            modelBuilder.Entity<MVoteType>(entity =>
            {
                entity.HasKey(e => e.VoteTypeId);

                entity.ToTable("M_VOTE_TYPE");

                entity.HasIndex(e => e.VoteTypeId)
                    .HasName("PK_M_VOTE_TYPE")
                    .IsUnique();

                entity.Property(e => e.VoteTypeId).HasColumnName("VOTE_TYPE_ID");

                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("CREATED_DATE")
                    .HasColumnType("TIMESTAMP(6)");

                entity.Property(e => e.Status).HasColumnName("STATUS");

                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("UPDATED_DATE")
                    .HasColumnType("TIMESTAMP(6)");

                entity.Property(e => e.VoteFor)
                    .IsRequired()
                    .HasColumnName("VOTE_FOR")
                    .HasColumnType("VARCHAR2(1)");

                entity.Property(e => e.VoteType)
                    .IsRequired()
                    .HasColumnName("VOTE_TYPE")
                    .HasColumnType("VARCHAR2(255)");

                entity.Property(e => e.VoteTypeCode)
                    .IsRequired()
                    .HasColumnName("VOTE_TYPE_CODE")
                    .HasColumnType("VARCHAR2(32)");
            });

            modelBuilder.Entity<OtpLog>(entity =>
            {
                entity.ToTable("OTP_LOG");

                entity.HasIndex(e => e.OtpLogId)
                    .HasName("PK_OTP_LOG")
                    .IsUnique();

                entity.Property(e => e.OtpLogId).HasColumnName("OTP_LOG_ID");

                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("CREATED_DATE")
                    .HasColumnType("TIMESTAMP(6)");

                entity.Property(e => e.Email)
                    .HasColumnName("EMAIL")
                    .HasColumnType("VARCHAR2(255)");

                entity.Property(e => e.ExpireDate)
                    .HasColumnName("EXPIRE_DATE")
                    .HasColumnType("TIMESTAMP(6)");

                entity.Property(e => e.Otp)
                    .IsRequired()
                    .HasColumnName("OTP")
                    .HasColumnType("VARCHAR2(32)");

                entity.Property(e => e.OtpLogCode)
                    .IsRequired()
                    .HasColumnName("OTP_LOG_CODE")
                    .HasColumnType("VARCHAR2(32)");

                entity.Property(e => e.Phone)
                    .HasColumnName("PHONE")
                    .HasColumnType("VARCHAR2(32)");

                entity.Property(e => e.Reference)
                    .IsRequired()
                    .HasColumnName("REFERENCE")
                    .HasColumnType("VARCHAR2(32)");

                entity.Property(e => e.Status).HasColumnName("STATUS");

                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("UPDATED_DATE")
                    .HasColumnType("TIMESTAMP(6)");
            });

            modelBuilder.Entity<PhoneDetail>(entity =>
            {
                entity.ToTable("PHONE_DETAIL");

                entity.HasIndex(e => e.PhoneDetailId)
                    .HasName("PK_PHONE_DETAIL")
                    .IsUnique();

                entity.Property(e => e.PhoneDetailId).HasColumnName("PHONE_DETAIL_ID");

                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("CREATED_DATE")
                    .HasColumnType("TIMESTAMP(6)");

                entity.Property(e => e.PhoneDetail1)
                    .IsRequired()
                    .HasColumnName("PHONE_DETAIL")
                    .HasColumnType("VARCHAR2(255)");

                entity.Property(e => e.PhoneNumberTypeId).HasColumnName("PHONE_NUMBER_TYPE_ID");

                entity.Property(e => e.Status).HasColumnName("STATUS");

                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("UPDATED_DATE")
                    .HasColumnType("TIMESTAMP(6)");
            });

            modelBuilder.Entity<PrivateMessage>(entity =>
            {
                entity.ToTable("PRIVATE_MESSAGE");

                entity.HasIndex(e => e.PrivateMessageId)
                    .HasName("PK_PRIVATE_MESSAGE")
                    .IsUnique();

                entity.Property(e => e.PrivateMessageId).HasColumnName("PRIVATE_MESSAGE_ID");

                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("CREATED_DATE")
                    .HasColumnType("TIMESTAMP(6)");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("EMAIL")
                    .HasColumnType("VARCHAR2(255)");

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasColumnName("FIRSTNAME")
                    .HasColumnType("VARCHAR2(255)");

                entity.Property(e => e.Ip)
                    .IsRequired()
                    .HasColumnName("IP")
                    .HasColumnType("VARCHAR2(255)");

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasColumnName("LASTNAME")
                    .HasColumnType("VARCHAR2(255)");

                entity.Property(e => e.Phone)
                    .HasColumnName("PHONE")
                    .HasColumnType("VARCHAR2(255)");

                entity.Property(e => e.PrivateMessageCode)
                    .IsRequired()
                    .HasColumnName("PRIVATE_MESSAGE_CODE")
                    .HasColumnType("VARCHAR2(32)");

                entity.Property(e => e.Status).HasColumnName("STATUS");

                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("UPDATED_DATE")
                    .HasColumnType("TIMESTAMP(6)");

                entity.Property(e => e.UserCode)
                    .HasColumnName("USER_CODE")
                    .HasColumnType("VARCHAR2(32)");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("PRODUCT");

                entity.HasIndex(e => e.ProductId)
                    .HasName("PRODUCT_PK")
                    .IsUnique();

                entity.Property(e => e.ProductId)
                    .HasColumnName("PRODUCT_ID")
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.ProductDesc)
                    .HasColumnName("PRODUCT_DESC")
                    .HasColumnType("VARCHAR2(100)");

                entity.Property(e => e.ProductName)
                    .HasColumnName("PRODUCT_NAME")
                    .HasColumnType("VARCHAR2(100)");
            });

            modelBuilder.Entity<Province>(entity =>
            {
                entity.ToTable("PROVINCE");

                entity.HasIndex(e => e.ProvinceId)
                    .HasName("PK_PROVINCEID")
                    .IsUnique();

                entity.Property(e => e.ProvinceId)
                    .HasColumnName("PROVINCE_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.ProvinceCode)
                    .IsRequired()
                    .HasColumnName("PROVINCE_CODE")
                    .HasColumnType("VARCHAR2(50)");

                entity.Property(e => e.ProvinceName)
                    .IsRequired()
                    .HasColumnName("PROVINCE_NAME")
                    .HasColumnType("VARCHAR2(50)");
            });

            modelBuilder.Entity<Rolegroup>(entity =>
            {
                entity.ToTable("ROLEGROUP");

                entity.HasIndex(e => e.RoleGroupId)
                    .HasName("ROLEGROUP_PK")
                    .IsUnique();

                entity.Property(e => e.RoleGroupId)
                    .HasColumnName("ROLE_GROUP_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.RoleGroupName)
                    .HasColumnName("ROLE_GROUP_NAME")
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<Rolegrouphasrole>(entity =>
            {
                entity.HasKey(e => new { e.RoleGroupId, e.RoleId });

                entity.ToTable("ROLEGROUPHASROLE");

                entity.HasIndex(e => new { e.RoleGroupId, e.RoleId })
                    .HasName("ROLEGROUPHASROLE_PK")
                    .IsUnique();

                entity.Property(e => e.RoleGroupId).HasColumnName("ROLE_GROUP_ID");

                entity.Property(e => e.RoleId).HasColumnName("ROLE_ID");

                entity.HasOne(d => d.RoleGroup)
                    .WithMany(p => p.Rolegrouphasrole)
                    .HasForeignKey(d => d.RoleGroupId)
                    .HasConstraintName("FK_ROLEGROUPHASROLE_ROLEGROUP");
            });

            modelBuilder.Entity<Rolegrouphasuser>(entity =>
            {
                entity.HasKey(e => new { e.RoleGroupId, e.UserId });

                entity.ToTable("ROLEGROUPHASUSER");

                entity.HasIndex(e => new { e.RoleGroupId, e.UserId })
                    .HasName("ROLEGROUPHASUSER_PK")
                    .IsUnique();

                entity.Property(e => e.RoleGroupId).HasColumnName("ROLE_GROUP_ID");

                entity.Property(e => e.UserId).HasColumnName("USER_ID");
            });

            modelBuilder.Entity<TmpAccount>(entity =>
            {
                entity.HasKey(e => e.AccountId);

                entity.ToTable("TMP_ACCOUNT");

                entity.HasIndex(e => e.AccountId)
                    .HasName("TMP_ACCOUNT_PK")
                    .IsUnique();

                entity.Property(e => e.AccountId)
                    .HasColumnName("ACCOUNT_ID")
                    .HasColumnType("VARCHAR2(100)");

                entity.Property(e => e.AccountName)
                    .HasColumnName("ACCOUNT_NAME")
                    .HasColumnType("VARCHAR2(100)");
            });

            modelBuilder.Entity<UploadedFile>(entity =>
            {
                entity.ToTable("UPLOADED_FILE");

                entity.HasIndex(e => e.UploadedFileId)
                    .HasName("PK_UPLOADED_FILE")
                    .IsUnique();

                entity.Property(e => e.UploadedFileId).HasColumnName("UPLOADED_FILE_ID");

                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("CREATED_DATE")
                    .HasColumnType("TIMESTAMP(6)");

                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasColumnName("FILE_NAME")
                    .HasColumnType("VARCHAR2(32)");

                entity.Property(e => e.FileType)
                    .IsRequired()
                    .HasColumnName("FILE_TYPE")
                    .HasColumnType("VARCHAR2(20)");

                entity.Property(e => e.FileUrl)
                    .IsRequired()
                    .HasColumnName("FILE_URL")
                    .HasColumnType("VARCHAR2(512)");

                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("UPDATED_DATE")
                    .HasColumnType("TIMESTAMP(6)");

                entity.Property(e => e.UploadedFileCode)
                    .IsRequired()
                    .HasColumnName("UPLOADED_FILE_CODE")
                    .HasColumnType("VARCHAR2(32)");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("USER");

                entity.HasIndex(e => e.UserId)
                    .HasName("PK_USER")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("USER_ID");

                entity.Property(e => e.AddressId).HasColumnName("ADDRESS_ID");

                entity.Property(e => e.ApproveComment)
                    .HasColumnName("APPROVE_COMMENT")
                    .HasColumnType("VARCHAR2(512)");

                entity.Property(e => e.CardNo)
                    .HasColumnName("CARD_NO")
                    .HasColumnType("VARCHAR2(255)");

                entity.Property(e => e.CardType)
                    .HasColumnName("CARD_TYPE")
                    .HasColumnType("CHAR(1)");

                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("CREATED_DATE")
                    .HasColumnType("TIMESTAMP(6)");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("EMAIL")
                    .HasColumnType("VARCHAR2(255)");

                entity.Property(e => e.EndDate)
                    .HasColumnName("END_DATE")
                    .HasColumnType("TIMESTAMP(6)");

                entity.Property(e => e.FacebookId)
                    .HasColumnName("FACEBOOK_ID")
                    .HasColumnType("VARCHAR2(255)");

                entity.Property(e => e.FacebookProfileName)
                    .HasColumnName("FACEBOOK_PROFILE_NAME")
                    .HasColumnType("VARCHAR2(255)");

                entity.Property(e => e.FailedLoginAttempt)
                    .HasColumnName("FAILED_LOGIN_ATTEMPT")
                    .HasColumnType("NUMBER(38)");

                entity.Property(e => e.Firstname)
                    .HasColumnName("FIRSTNAME")
                    .HasColumnType("VARCHAR2(255)");

                entity.Property(e => e.ForceChangePassword)
                    .HasColumnName("FORCE_CHANGE_PASSWORD")
                    .HasColumnType("VARCHAR2(1)");

                entity.Property(e => e.IsApprovedByAdmin)
                    .HasColumnName("IS_APPROVED_BY_ADMIN")
                    .HasColumnType("CHAR(1)");

                entity.Property(e => e.LastChangePasswordDate)
                    .HasColumnName("LAST_CHANGE_PASSWORD_DATE")
                    .HasColumnType("TIMESTAMP(6)");

                entity.Property(e => e.LastLogin)
                    .HasColumnName("LAST_LOGIN")
                    .HasColumnType("TIMESTAMP(6)");

                entity.Property(e => e.Lastname)
                    .HasColumnName("LASTNAME")
                    .HasColumnType("VARCHAR2(255)");

                entity.Property(e => e.Mobile)
                    .HasColumnName("MOBILE")
                    .HasColumnType("VARCHAR2(20)");

                entity.Property(e => e.Password)
                    .HasColumnName("PASSWORD")
                    .HasColumnType("VARCHAR2(100)");

                entity.Property(e => e.Pin)
                    .HasColumnName("PIN")
                    .HasColumnType("VARCHAR2(32)");

                entity.Property(e => e.StartDate)
                    .HasColumnName("START_DATE")
                    .HasColumnType("TIMESTAMP(6)");

                entity.Property(e => e.Status).HasColumnName("STATUS");

                entity.Property(e => e.TitleCode)
                    .HasColumnName("TITLE_CODE")
                    .HasColumnType("VARCHAR2(32)");

                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("UPDATED_DATE")
                    .HasColumnType("TIMESTAMP(6)");

                entity.Property(e => e.UserCode)
                    .IsRequired()
                    .HasColumnName("USER_CODE")
                    .HasColumnType("VARCHAR2(32)");

                entity.Property(e => e.UserName)
                    .HasColumnName("USER_NAME")
                    .HasColumnType("VARCHAR2(20)");

                entity.Property(e => e.UserTypeId).HasColumnName("USER_TYPE_ID");

                entity.Property(e => e.VerifiedType)
                    .HasColumnName("VERIFIED_TYPE")
                    .HasColumnType("CHAR(1)");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.AddressId)
                    .HasConstraintName("FK_USER_ADDRESS");

                entity.HasOne(d => d.UserType)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.UserTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_USER_M_USER_TYPE");
            });

            modelBuilder.Entity<Vote>(entity =>
            {
                entity.ToTable("VOTE");

                entity.HasIndex(e => e.VoteId)
                    .HasName("PK_VOTE")
                    .IsUnique();

                entity.Property(e => e.VoteId).HasColumnName("VOTE_ID");

                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("CREATED_DATE")
                    .HasColumnType("TIMESTAMP(6)");

                entity.Property(e => e.EventCode)
                    .HasColumnName("EVENT_CODE")
                    .HasColumnType("VARCHAR2(32)");

                entity.Property(e => e.StadiumCode)
                    .HasColumnName("STADIUM_CODE")
                    .HasColumnType("VARCHAR2(32)");

                entity.Property(e => e.Status).HasColumnName("STATUS");

                entity.Property(e => e.UpdatedBy).HasColumnName("UPDATED_BY");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("UPDATED_DATE")
                    .HasColumnType("TIMESTAMP(6)");

                entity.Property(e => e.VoteMaxValue).HasColumnName("VOTE_MAX_VALUE");

                entity.Property(e => e.VoteTypeId).HasColumnName("VOTE_TYPE_ID");

                entity.Property(e => e.VoteValue).HasColumnName("VOTE_VALUE");
            });

            modelBuilder.HasSequence("SQ_ACTION_LOG");

            modelBuilder.HasSequence("SQ_ADDRESS");

            modelBuilder.HasSequence("SQ_APPLOG");

            modelBuilder.HasSequence("SQ_AspNetRoleClaims");

            modelBuilder.HasSequence("SQ_AspNetUserClaims");

            modelBuilder.HasSequence("SQ_COMMENT");

            modelBuilder.HasSequence("SQ_DEPARTMENT");

            modelBuilder.HasSequence("SQ_DEPARTMENT_EVENT");

            modelBuilder.HasSequence("SQ_DEPARTMENT_PERSON");

            modelBuilder.HasSequence("SQ_EVENT");

            modelBuilder.HasSequence("SQ_EVENT_JOIN_PERSON_TYPE");

            modelBuilder.HasSequence("SQ_EVENT_SPORT");

            modelBuilder.HasSequence("SQ_EVENT_UPLOADED_FILE");

            modelBuilder.HasSequence("SQ_GENERATE_CODE");

            modelBuilder.HasSequence("SQ_M_ADDRESS_TYPE");

            modelBuilder.HasSequence("SQ_M_DEPARTMENT_EVENT_SUBTYPE");

            modelBuilder.HasSequence("SQ_M_EVENT_LEVEL");

            modelBuilder.HasSequence("SQ_M_EVENT_OBJECTIVE_PERSON");

            modelBuilder.HasSequence("SQ_M_GROUP");

            modelBuilder.HasSequence("SQ_M_JOIN_PERSON_TYPE");

            modelBuilder.HasSequence("SQ_M_PERMISSION_GROUP");

            modelBuilder.HasSequence("SQ_M_PERMISSIONGROUP_PROGRAM");

            modelBuilder.HasSequence("SQ_M_PHONE_NUMBER_TYPE");

            modelBuilder.HasSequence("SQ_M_PROGRAM");

            modelBuilder.HasSequence("SQ_M_SPORT");

            modelBuilder.HasSequence("SQ_M_STATUS");

            modelBuilder.HasSequence("SQ_M_USER_PERMISSIONGROUP");

            modelBuilder.HasSequence("SQ_M_USER_TYPE");

            modelBuilder.HasSequence("SQ_M_VOTE_TYPE");

            modelBuilder.HasSequence("SQ_OTP_LOG");

            modelBuilder.HasSequence("SQ_PHONE_DETAIL");

            modelBuilder.HasSequence("SQ_PRIVATE_MESSAGE");

            modelBuilder.HasSequence("SQ_PRODUCT");

            modelBuilder.HasSequence("SQ_TMP_ACCOUNT");

            modelBuilder.HasSequence("SQ_UPLOADED_FILE");

            modelBuilder.HasSequence("SQ_USER");

            modelBuilder.HasSequence("SQ_VOTE");
        }
    }
}
