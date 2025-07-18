﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DrugPrevention.Repositories.NganVHH.Models;

public partial class SU25_PRN222_SE1709_G2_DrugPreventionSystemContext : DbContext
{
    public SU25_PRN222_SE1709_G2_DrugPreventionSystemContext()
    {
    }

    public SU25_PRN222_SE1709_G2_DrugPreventionSystemContext(DbContextOptions<SU25_PRN222_SE1709_G2_DrugPreventionSystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AppointmentsNganVHH> AppointmentsNganVHHs { get; set; }

    public virtual DbSet<CommunityProgramsToanN> CommunityProgramsToanNs { get; set; }

    public virtual DbSet<ConsultantScheduleTrongLH> ConsultantScheduleTrongLHs { get; set; }

    public virtual DbSet<ConsultantsTrongLH> ConsultantsTrongLHs { get; set; }

    public virtual DbSet<CoursesQuangTNV> CoursesQuangTNVs { get; set; }

    public virtual DbSet<ProgramParticipantsToanN> ProgramParticipantsToanNs { get; set; }

    public virtual DbSet<SurveyQuestionsQuangTNV> SurveyQuestionsQuangTNVs { get; set; }

    public virtual DbSet<SurveysNamND> SurveysNamNDs { get; set; }

    public virtual DbSet<System_UserAccount> System_UserAccounts { get; set; }

    public virtual DbSet<UserCoursesTuyenTM> UserCoursesTuyenTMs { get; set; }

    public virtual DbSet<UserSurveysNamND> UserSurveysNamNDs { get; set; }

    public virtual DbSet<UsersTuyenTM> UsersTuyenTMs { get; set; }

    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
    //        => optionsBuilder.UseSqlServer("Data Source=NGANNGANCHIMTE\\NGANCUTE;Initial Catalog=SU25_PRN222_SE1709_G2_DrugPreventionSystem;Persist Security Info=True;User ID=sa;Password=12345;Encrypt=False");

    public static string GetConnectionString(string connectionStringName)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        string connectionString = config.GetConnectionString(connectionStringName);
        return connectionString;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(GetConnectionString("DefaultConnection")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AppointmentsNganVHH>(entity =>
        {
            entity.ToTable("AppointmentsNganVHH", tb => tb.HasComment("Bảng lưu trữ thông tin các cuộc hẹn tư vấn về phòng chống ma túy"));

            entity.Property(e => e.ConsultantID).HasComment("Liên kết đến bảng ConsultantsTrongLH để xác định chuyên gia tư vấn phụ trách");
            entity.Property(e => e.ConsultationType).HasDefaultValue("Online");
            entity.Property(e => e.Duration).HasDefaultValue(60);
            entity.Property(e => e.IsInterpreterNeeded).HasComment("Cuộc hẹn có cần người thông dịch không? (0: Không cần, 1: Cần thông dịch viên)");
            entity.Property(e => e.Status).HasDefaultValue("Scheduled");
            entity.Property(e => e.UserID).HasComment("Liên kết đến bảng UsersTuyenTM để xác định người dùng đặt lịch hẹn");

            entity.HasOne(d => d.Consultant).WithMany(p => p.AppointmentsNganVHHs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AppointmentsNganVHH_ConsultantsTrongLH");

            entity.HasOne(d => d.User).WithMany(p => p.AppointmentsNganVHHs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AppointmentsNganVHH_UsersTuyenTM");
        });

        modelBuilder.Entity<CommunityProgramsToanN>(entity =>
        {
            entity.HasKey(e => e.ProgramToanNSID).HasName("PK__Communit__CB59144E12A4196E");

            entity.Property(e => e.CurrentParticipants).HasDefaultValue(0);
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.Organizer).WithMany(p => p.CommunityProgramsToanNs).HasConstraintName("FK__Community__Organ__6E01572D");
        });

        modelBuilder.Entity<ConsultantScheduleTrongLH>(entity =>
        {
            entity.HasKey(e => e.ScheduleTrongLHID).HasName("PK__Consulta__33BE882E9B4EB4B6");

            entity.Property(e => e.BufferMinutesBetweenMeetings).HasDefaultValue(15);
            entity.Property(e => e.EffectiveFrom).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsAvailable).HasDefaultValue(true);
            entity.Property(e => e.MaxAppointmentsPerSlot).HasDefaultValue(1);
            entity.Property(e => e.RecurringPattern).HasDefaultValue("Weekly");

            entity.HasOne(d => d.Consultant).WithMany(p => p.ConsultantScheduleTrongLHs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Consultan__Consu__6EF57B66");
        });

        modelBuilder.Entity<ConsultantsTrongLH>(entity =>
        {
            entity.HasKey(e => e.ConsultantTrongLHID).HasName("PK__Consulta__0D38FD8F3AC5B645");

            entity.Property(e => e.AverageRating).HasDefaultValue(0m);
            entity.Property(e => e.IsAvailable).HasDefaultValue(true);
            entity.Property(e => e.ProfileVerified).HasDefaultValue(false);
            entity.Property(e => e.TotalConsultations).HasDefaultValue(0);
            entity.Property(e => e.UserID).HasComment("Liên kết đến bảng UsersTuyenTM để xác định tài khoản user của chuyên gia tư vấn");

            entity.HasOne(d => d.User).WithMany(p => p.ConsultantsTrongLHs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ConsultantsTrongLH_UsersTuyenTM");
        });

        modelBuilder.Entity<CoursesQuangTNV>(entity =>
        {
            entity.HasKey(e => e.CourseQuangTNVID).HasName("PK__CoursesQ__122D26204369C42B");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Rating).HasDefaultValue(0m);
        });

        modelBuilder.Entity<ProgramParticipantsToanN>(entity =>
        {
            entity.HasKey(e => e.ParticipantToanNSID).HasName("PK__ProgramP__C3891D6C60918DA9");

            entity.Property(e => e.CertificateIssued).HasDefaultValue(false);
            entity.Property(e => e.FeedbackProvided).HasDefaultValue(false);
            entity.Property(e => e.RegistrationDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.ProgramToanNS).WithMany(p => p.ProgramParticipantsToanNs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProgramPa__Progr__6FE99F9F");

            entity.HasOne(d => d.User).WithMany(p => p.ProgramParticipantsToanNs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProgramPa__UserI__70DDC3D8");
        });

        modelBuilder.Entity<SurveyQuestionsQuangTNV>(entity =>
        {
            entity.HasKey(e => e.QuestionQuangTNVID).HasName("PK__SurveyQu__8B30E7C0D3C5C7DC");

            entity.Property(e => e.IsRequired).HasDefaultValue(true);
            entity.Property(e => e.RiskWeight).HasDefaultValue(1);

            entity.HasOne(d => d.Survey).WithMany(p => p.SurveyQuestionsQuangTNVs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SurveyQue__Surve__71D1E811");
        });

        modelBuilder.Entity<SurveysNamND>(entity =>
        {
            entity.HasKey(e => e.SurveyNamNDID).HasName("PK__SurveysN__A48BA58E75BA3E48");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.IsAnonymous).HasDefaultValue(false);
        });

        modelBuilder.Entity<UserCoursesTuyenTM>(entity =>
        {
            entity.HasKey(e => e.UserCourseTuyenTMID).HasName("PK__UserCour__ACC294198875EA2B");

            entity.Property(e => e.CertificateIssued).HasDefaultValue(false);
            entity.Property(e => e.EnrollmentDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Progress).HasDefaultValue(0m);

            entity.HasOne(d => d.Course).WithMany(p => p.UserCoursesTuyenTMs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserCours__Cours__72C60C4A");

            entity.HasOne(d => d.User).WithMany(p => p.UserCoursesTuyenTMs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserCours__UserI__73BA3083");
        });

        modelBuilder.Entity<UserSurveysNamND>(entity =>
        {
            entity.HasKey(e => e.UserSurveyNamNDID).HasName("PK__UserSurv__7AAF6C20FDB6DFF4");

            entity.Property(e => e.IsAnonymous).HasDefaultValue(false);
            entity.Property(e => e.SubmissionDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Survey).WithMany(p => p.UserSurveysNamNDs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserSurve__Surve__74AE54BC");

            entity.HasOne(d => d.User).WithMany(p => p.UserSurveysNamNDs).HasConstraintName("FK__UserSurve__UserI__75A278F5");
        });

        modelBuilder.Entity<UsersTuyenTM>(entity =>
        {
            entity.HasKey(e => e.UserTuyenTMID).HasName("PK__UsersTuy__1042E1E09A3482BB");

            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.RegistrationDate).HasDefaultValueSql("(getdate())");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}