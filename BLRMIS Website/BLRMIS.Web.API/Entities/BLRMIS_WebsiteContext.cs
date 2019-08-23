using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BLRMIS.Web.API.Entities
{
    public partial class BLRMIS_WebsiteContext : DbContext
    {
        public BLRMIS_WebsiteContext()
        {
        }

        public BLRMIS_WebsiteContext(DbContextOptions<BLRMIS_WebsiteContext> options)
            : base(options)
        {
        }

        public virtual DbSet<LrmisWebApiDownloadedData> LrmisWebApiDownloadedData { get; set; }
        public virtual DbSet<LrmisWebAttachment> LrmisWebAttachment { get; set; }
        public virtual DbSet<LrmisWebCategory> LrmisWebCategory { get; set; }
        public virtual DbSet<LrmisWebComplaint> LrmisWebComplaint { get; set; }
        public virtual DbSet<LrmisWebComplaintLog> LrmisWebComplaintLog { get; set; }
        public virtual DbSet<LrmisWebComplaintStatus> LrmisWebComplaintStatus { get; set; }
        public virtual DbSet<LrmisWebContent> LrmisWebContent { get; set; }
        public virtual DbSet<LrmisWebDepartment> LrmisWebDepartment { get; set; }
        public virtual DbSet<LrmisWebDesignation> LrmisWebDesignation { get; set; }
        public virtual DbSet<LrmisWebDownload> LrmisWebDownload { get; set; }
        public virtual DbSet<LrmisWebFaq> LrmisWebFaq { get; set; }
        public virtual DbSet<LrmisWebFunctionRoleMapping> LrmisWebFunctionRoleMapping { get; set; }
        public virtual DbSet<LrmisWebFunctions> LrmisWebFunctions { get; set; }
        public virtual DbSet<LrmisWebLocation> LrmisWebLocation { get; set; }
        public virtual DbSet<LrmisWebNews> LrmisWebNews { get; set; }
        public virtual DbSet<LrmisWebPage> LrmisWebPage { get; set; }
        public virtual DbSet<LrmisWebRole> LrmisWebRole { get; set; }
        public virtual DbSet<LrmisWebSourcetype> LrmisWebSourcetype { get; set; }
        public virtual DbSet<LrmisWebUser> LrmisWebUser { get; set; }
        public virtual DbSet<LrmisWebVerificationToken> LrmisWebVerificationToken { get; set; }
        public virtual DbSet<LrmisWebVisitorInformation> LrmisWebVisitorInformation { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=10.100.101.26;Database=BLRMIS_Website;User ID=sa;Password=Abcd@1234");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LrmisWebApiDownloadedData>(entity =>
            {
                entity.ToTable("lrmis_web_api_downloaded_data");

                entity.Property(e => e.DownloadedDate)
                    .HasColumnName("Downloaded_Date")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<LrmisWebAttachment>(entity =>
            {
                entity.HasKey(e => e.AttachmentId);

                entity.ToTable("lrmis_web_attachment");

                entity.Property(e => e.AttachmentId).HasColumnName("attachment_id");

                entity.Property(e => e.AttachmentName)
                    .IsRequired()
                    .HasColumnName("attachment_name")
                    .HasMaxLength(50);

                entity.Property(e => e.AttachmentPath)
                    .IsRequired()
                    .HasColumnName("attachment_path")
                    .HasMaxLength(500);

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Filesize)
                    .HasColumnName("filesize")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Mimetype)
                    .HasColumnName("mimetype")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.OriginalFileName)
                    .IsRequired()
                    .HasColumnName("original_fileName")
                    .HasMaxLength(255);

                entity.Property(e => e.SourceId).HasColumnName("source_id");

                entity.Property(e => e.SourceType).HasColumnName("source_type");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.LrmisWebAttachmentCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_lrmis_web_attachment_lrmis_web_user");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.LrmisWebAttachmentModifiedByNavigation)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK_lrmis_web_attachment_lrmis_web_user1");
            });

            modelBuilder.Entity<LrmisWebCategory>(entity =>
            {
                entity.HasKey(e => e.CategoryId);

                entity.ToTable("lrmis_web_category");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.CategoryDescription)
                    .HasColumnName("category_description")
                    .HasMaxLength(500);

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasColumnName("category_name")
                    .HasMaxLength(20);

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.LrmisWebCategoryCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_lrmis_web_category_lrmis_web_user");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.LrmisWebCategoryModifiedByNavigation)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK_lrmis_web_category_lrmis_web_user1");
            });

            modelBuilder.Entity<LrmisWebComplaint>(entity =>
            {
                entity.HasKey(e => e.ComplaintId);

                entity.ToTable("lrmis_web_complaint");

                entity.Property(e => e.ComplaintId).HasColumnName("complaint_id");

                entity.Property(e => e.CitizenCnic)
                    .IsRequired()
                    .HasColumnName("citizen_cnic")
                    .HasMaxLength(15);

                entity.Property(e => e.CitizenEmailAddress)
                    .IsRequired()
                    .HasColumnName("citizen_email_address")
                    .HasMaxLength(30);

                entity.Property(e => e.CitizenName)
                    .IsRequired()
                    .HasColumnName("citizen_name")
                    .HasMaxLength(30);

                entity.Property(e => e.CitizenPhoneNumber)
                    .IsRequired()
                    .HasColumnName("citizen_phone_number")
                    .HasMaxLength(12);

                entity.Property(e => e.ComplaintAccessToken)
                    .HasColumnName("complaint_access_token")
                    .HasMaxLength(50)
                    .HasDefaultValueSql("(replace(newid(),'-',''))");

                entity.Property(e => e.ComplaintAssignTo).HasColumnName("complaint_assign_to");

                entity.Property(e => e.ComplaintCategoryId).HasColumnName("complaint_category_id");

                entity.Property(e => e.ComplaintCode)
                    .HasColumnName("complaint_code")
                    .HasMaxLength(32);

                entity.Property(e => e.ComplaintDescription)
                    .IsRequired()
                    .HasColumnName("complaint_description")
                    .HasMaxLength(500);

                entity.Property(e => e.ComplaintStatusId).HasColumnName("complaint_status_id");

                entity.Property(e => e.ComplaintTitle)
                    .IsRequired()
                    .HasColumnName("complaint_title")
                    .HasMaxLength(100);

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FunctionId).HasColumnName("function_id");

                entity.Property(e => e.IsLocked).HasColumnName("is_locked");

                entity.Property(e => e.LocationId).HasColumnName("location_id");

                entity.Property(e => e.LockedBy).HasColumnName("locked_by");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.ComplaintAssignToNavigation)
                    .WithMany(p => p.LrmisWebComplaintComplaintAssignToNavigation)
                    .HasForeignKey(d => d.ComplaintAssignTo)
                    .HasConstraintName("FK_lrmis_web_complaint_lrmis_web_user1");

                entity.HasOne(d => d.ComplaintCategory)
                    .WithMany(p => p.LrmisWebComplaint)
                    .HasForeignKey(d => d.ComplaintCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_lrmis_web_complaint_lrmis_web_category");

                entity.HasOne(d => d.ComplaintStatus)
                    .WithMany(p => p.LrmisWebComplaint)
                    .HasForeignKey(d => d.ComplaintStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_lrmis_web_complaint_lrmis_web_complaint_status");

                entity.HasOne(d => d.Function)
                    .WithMany(p => p.LrmisWebComplaint)
                    .HasForeignKey(d => d.FunctionId)
                    .HasConstraintName("FK_lrmis_web_complaint_lrmis_web_functions");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.LrmisWebComplaint)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_lrmis_web_complaint_lrmis_web_location");

                entity.HasOne(d => d.LockedByNavigation)
                    .WithMany(p => p.LrmisWebComplaintLockedByNavigation)
                    .HasForeignKey(d => d.LockedBy)
                    .HasConstraintName("FK_lrmis_web_complaint_lrmis_web_user");
            });

            modelBuilder.Entity<LrmisWebComplaintLog>(entity =>
            {
                entity.HasKey(e => e.ComplaintCommentId);

                entity.ToTable("lrmis_web_complaint_log");

                entity.Property(e => e.ComplaintCommentId).HasColumnName("complaint_comment_id");

                entity.Property(e => e.ComplaintAssignBy).HasColumnName("complaint_assign_by");

                entity.Property(e => e.ComplaintAssignTo).HasColumnName("complaint_assign_to");

                entity.Property(e => e.ComplaintComments)
                    .IsRequired()
                    .HasColumnName("complaint_comments")
                    .HasMaxLength(500);

                entity.Property(e => e.ComplaintId).HasColumnName("complaint_id");

                entity.Property(e => e.ComplaintStatusId).HasColumnName("complaint_status_id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.ComplaintAssignByNavigation)
                    .WithMany(p => p.LrmisWebComplaintLogComplaintAssignByNavigation)
                    .HasForeignKey(d => d.ComplaintAssignBy)
                    .HasConstraintName("FK_lrmis_web_complaint_log_lrmis_web_user1");

                entity.HasOne(d => d.ComplaintAssignToNavigation)
                    .WithMany(p => p.LrmisWebComplaintLogComplaintAssignToNavigation)
                    .HasForeignKey(d => d.ComplaintAssignTo)
                    .HasConstraintName("FK_lrmis_web_complaint_log_lrmis_web_user");

                entity.HasOne(d => d.Complaint)
                    .WithMany(p => p.LrmisWebComplaintLog)
                    .HasForeignKey(d => d.ComplaintId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_lrmis_web_complaint_log_lrmis_web_complaint");

                entity.HasOne(d => d.ComplaintStatus)
                    .WithMany(p => p.LrmisWebComplaintLog)
                    .HasForeignKey(d => d.ComplaintStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_lrmis_web_complaint_log_lrmis_web_complaint_status");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.LrmisWebComplaintLogCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_lrmis_web_complaint_log_lrmis_web_user2");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.LrmisWebComplaintLogModifiedByNavigation)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK_lrmis_web_complaint_log_lrmis_web_user3");
            });

            modelBuilder.Entity<LrmisWebComplaintStatus>(entity =>
            {
                entity.HasKey(e => e.ComplaintStatusId);

                entity.ToTable("lrmis_web_complaint_status");

                entity.Property(e => e.ComplaintStatusId)
                    .HasColumnName("complaint_status_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.ComplaintStatus)
                    .IsRequired()
                    .HasColumnName("complaint_status")
                    .HasMaxLength(20);

                entity.Property(e => e.ComplaintStatusCode)
                    .IsRequired()
                    .HasColumnName("complaint_status_code")
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<LrmisWebContent>(entity =>
            {
                entity.HasKey(e => e.ContentId);

                entity.ToTable("lrmis_web_content");

                entity.Property(e => e.ContentId).HasColumnName("content_id");

                entity.Property(e => e.ContentDescription)
                    .IsRequired()
                    .HasColumnName("content_description");

                entity.Property(e => e.ContentPageId).HasColumnName("content_page_id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.ContentPage)
                    .WithMany(p => p.LrmisWebContent)
                    .HasForeignKey(d => d.ContentPageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_lrmis_web_content_lrmis_web_page");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.LrmisWebContentCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_lrmis_web_content_lrmis_web_user");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.LrmisWebContentModifiedByNavigation)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK_lrmis_web_content_lrmis_web_user1");
            });

            modelBuilder.Entity<LrmisWebDepartment>(entity =>
            {
                entity.HasKey(e => e.DepartmentId);

                entity.ToTable("lrmis_web_department");

                entity.Property(e => e.DepartmentId).HasColumnName("department_id");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DepartmentDescription)
                    .HasColumnName("department_description")
                    .HasMaxLength(500);

                entity.Property(e => e.DepartmentName)
                    .IsRequired()
                    .HasColumnName("department_name")
                    .HasMaxLength(20);

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.LrmisWebDepartmentCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_lrmis_web_department_lrmis_web_user");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.LrmisWebDepartmentModifiedByNavigation)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK_lrmis_web_department_lrmis_web_user1");
            });

            modelBuilder.Entity<LrmisWebDesignation>(entity =>
            {
                entity.HasKey(e => e.DesignationId);

                entity.ToTable("lrmis_web_designation");

                entity.Property(e => e.DesignationId).HasColumnName("designation_id");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DesignationDescription)
                    .HasColumnName("designation_description")
                    .HasMaxLength(500);

                entity.Property(e => e.DesignationName)
                    .IsRequired()
                    .HasColumnName("designation_name")
                    .HasMaxLength(30);

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.LrmisWebDesignationCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_lrmis_web_designation_lrmis_web_user1");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.LrmisWebDesignationModifiedByNavigation)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK_lrmis_web_designation_lrmis_web_user");
            });

            modelBuilder.Entity<LrmisWebDownload>(entity =>
            {
                entity.HasKey(e => e.DownloadId);

                entity.ToTable("lrmis_web_download");

                entity.Property(e => e.DownloadId).HasColumnName("download_id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DownloadDescription)
                    .IsRequired()
                    .HasColumnName("download_description");

                entity.Property(e => e.DownloadTitle)
                    .IsRequired()
                    .HasColumnName("download_title")
                    .HasMaxLength(100);

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.LrmisWebDownloadCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_lrmis_web_download_lrmis_web_user1");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.LrmisWebDownloadModifiedByNavigation)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK_lrmis_web_download_lrmis_web_user");
            });

            modelBuilder.Entity<LrmisWebFaq>(entity =>
            {
                entity.HasKey(e => e.FaqId);

                entity.ToTable("lrmis_web_faq");

                entity.Property(e => e.FaqId).HasColumnName("faq_id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FaqDescription)
                    .IsRequired()
                    .HasColumnName("faq_description");

                entity.Property(e => e.FaqTitle)
                    .IsRequired()
                    .HasColumnName("faq_title")
                    .HasMaxLength(200);

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.LrmisWebFaqCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_lrmis_web_faq_lrmis_web_user");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.LrmisWebFaqModifiedByNavigation)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK_lrmis_web_faq_lrmis_web_user1");
            });

            modelBuilder.Entity<LrmisWebFunctionRoleMapping>(entity =>
            {
                entity.HasKey(e => e.MappingId);

                entity.ToTable("lrmis_web_function_role_mapping");

                entity.Property(e => e.MappingId).HasColumnName("mapping_id");

                entity.Property(e => e.FunctionId).HasColumnName("function_id");

                entity.Property(e => e.Include).HasColumnName("include");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.HasOne(d => d.Function)
                    .WithMany(p => p.LrmisWebFunctionRoleMapping)
                    .HasForeignKey(d => d.FunctionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_lrmis_web_function_role_mapping_lrmis_web_functions");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.LrmisWebFunctionRoleMapping)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_lrmis_web_function_role_mapping_lrmis_web_role");
            });

            modelBuilder.Entity<LrmisWebFunctions>(entity =>
            {
                entity.HasKey(e => e.FunctionId);

                entity.ToTable("lrmis_web_functions");

                entity.Property(e => e.FunctionId)
                    .HasColumnName("function_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.FunctionCode)
                    .HasColumnName("function_code")
                    .HasMaxLength(50);

                entity.Property(e => e.FunctionDescription)
                    .HasColumnName("function_description")
                    .HasMaxLength(200);

                entity.Property(e => e.FunctionName)
                    .HasColumnName("function_name")
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<LrmisWebLocation>(entity =>
            {
                entity.HasKey(e => e.LocationId);

                entity.ToTable("lrmis_web_location");

                entity.Property(e => e.LocationId)
                    .HasColumnName("location_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.DigitizationProgressPercentage).HasColumnName("digitization_progress_percentage");

                entity.Property(e => e.LocationName)
                    .IsRequired()
                    .HasColumnName("location_name")
                    .HasMaxLength(200);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<LrmisWebNews>(entity =>
            {
                entity.HasKey(e => e.NewsId);

                entity.ToTable("lrmis_web_news");

                entity.Property(e => e.NewsId).HasColumnName("news_id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.NewsDate)
                    .HasColumnName("news_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.NewsDescription)
                    .IsRequired()
                    .HasColumnName("news_description");

                entity.Property(e => e.NewsTitle)
                    .IsRequired()
                    .HasColumnName("news_title")
                    .HasMaxLength(100);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.LrmisWebNewsCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_lrmis_web_news_lrmis_web_user1");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.LrmisWebNewsModifiedByNavigation)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK_lrmis_web_news_lrmis_web_user");
            });

            modelBuilder.Entity<LrmisWebPage>(entity =>
            {
                entity.HasKey(e => e.PageId);

                entity.ToTable("lrmis_web_page");

                entity.Property(e => e.PageId)
                    .HasColumnName("page_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.PageName)
                    .IsRequired()
                    .HasColumnName("page_name")
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<LrmisWebRole>(entity =>
            {
                entity.HasKey(e => e.RoleId);

                entity.ToTable("lrmis_web_role");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.RoleDescription)
                    .IsRequired()
                    .HasColumnName("role_description")
                    .HasMaxLength(500);

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasColumnName("role_name")
                    .HasMaxLength(20);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.LrmisWebRoleCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_lrmis_web_role_lrmis_web_user");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.LrmisWebRoleModifiedByNavigation)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK_lrmis_web_role_lrmis_web_user1");
            });

            modelBuilder.Entity<LrmisWebSourcetype>(entity =>
            {
                entity.HasKey(e => e.SourceId);

                entity.ToTable("lrmis_web_sourcetype");

                entity.Property(e => e.SourceId)
                    .HasColumnName("source_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.SourceName)
                    .IsRequired()
                    .HasColumnName("source_name")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<LrmisWebUser>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("lrmis_web_user");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.Cnic)
                    .IsRequired()
                    .HasColumnName("cnic")
                    .HasMaxLength(15);

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DepartmentId).HasColumnName("department_id");

                entity.Property(e => e.DesignationId).HasColumnName("designation_id");

                entity.Property(e => e.EmailAddress)
                    .IsRequired()
                    .HasColumnName("email_address")
                    .HasMaxLength(30);

                entity.Property(e => e.FatherName)
                    .IsRequired()
                    .HasColumnName("father_name")
                    .HasMaxLength(30);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasMaxLength(20);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("last_name")
                    .HasMaxLength(20);

                entity.Property(e => e.LocationId).HasColumnName("location_id");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(40);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasColumnName("phone_number")
                    .HasMaxLength(12);

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasColumnName("user_name")
                    .HasMaxLength(20);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.InverseCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_lrmis_web_user_lrmis_web_user1");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.LrmisWebUser)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_lrmis_web_user_lrmis_web_department");

                entity.HasOne(d => d.Designation)
                    .WithMany(p => p.LrmisWebUser)
                    .HasForeignKey(d => d.DesignationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_lrmis_web_user_lrmis_web_designation");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.LrmisWebUser)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_lrmis_web_user_lrmis_web_location");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.InverseModifiedByNavigation)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK_lrmis_web_user_lrmis_web_user");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.LrmisWebUser)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_lrmis_web_user_lrmis_web_role");
            });

            modelBuilder.Entity<LrmisWebVerificationToken>(entity =>
            {
                entity.HasKey(e => e.VerificationId);

                entity.ToTable("lrmis_web_verification_token");

                entity.Property(e => e.VerificationId).HasColumnName("verification_id");

                entity.Property(e => e.Consumed).HasDefaultValueSql("((0))");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EmailAddress)
                    .HasColumnName("email_address")
                    .HasMaxLength(30);

                entity.Property(e => e.Expired).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.PhoneNumber)
                    .HasColumnName("phone_number")
                    .HasMaxLength(20);

                entity.Property(e => e.VerificationCode)
                    .HasColumnName("verification_code")
                    .HasMaxLength(20);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.LrmisWebVerificationTokenCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_lrmis_web_verification_token_lrmis_web_user1");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.LrmisWebVerificationTokenModifiedByNavigation)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK_lrmis_web_verification_token_lrmis_web_user");
            });

            modelBuilder.Entity<LrmisWebVisitorInformation>(entity =>
            {
                entity.HasKey(e => e.VisitorId);

                entity.ToTable("lrmis_web_visitor_information");

                entity.Property(e => e.VisitorId).HasColumnName("visitor_id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.IpAddress)
                    .HasColumnName("ip_address")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.MachineName)
                    .HasColumnName("machine_name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UserAgent)
                    .HasColumnName("user_agent")
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });
        }
    }
}
