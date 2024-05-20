using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace CoreBotCLU.Models
{
    public partial class SupportTicketSystemContext : DbContext
    {
        

        public SupportTicketSystemContext(DbContextOptions<SupportTicketSystemContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Attachments> Attachments { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Comments> Comments { get; set; }
        public virtual DbSet<Priorities> Priorities { get; set; }
        public virtual DbSet<ProcessFlows> ProcessFlows { get; set; }
        public virtual DbSet<Reasons> Reasons { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Severities> Severities { get; set; }
        public virtual DbSet<Statuses> Statuses { get; set; }
        public virtual DbSet<Statushistory> Statushistory { get; set; }
        public virtual DbSet<Taggableitems> Taggableitems { get; set; }
        public virtual DbSet<Tags> Tags { get; set; }
        public virtual DbSet<Tenants> Tenants { get; set; }
        public virtual DbSet<TicketCategory> TicketCategory { get; set; }
        public virtual DbSet<TicketHistories> TicketHistories { get; set; }
        public virtual DbSet<TicketType> TicketType { get; set; }
        public virtual DbSet<Tickets> Tickets { get; set; }
        public virtual DbSet<UserRoles> UserRoles { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        ////{
        ////    var builder = new ConfigurationBuilder()
        ////                      .SetBasePath(Directory.GetCurrentDirectory())
        ////                      .AddJsonFile("appsettings.json");
        ////    var config = builder.Build();
        ////    var connectionString = config.GetConnectionString("DBConnectionString");

        ////    if (!optionsBuilder.IsConfigured)
        ////    {
        ////        optionsBuilder.UseSqlServer(connectionString);
        ////    }
        ////}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attachments>(entity =>
            {
                entity.ToTable("attachments");

                entity.HasIndex(e => e.TicketId);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ContentType).IsRequired();

                entity.Property(e => e.FileData).IsRequired();

                entity.Property(e => e.FileName).IsRequired();

                entity.Property(e => e.TicketId).HasColumnName("TicketID");

                entity.HasOne(d => d.Ticket)
                    .WithMany(p => p.Attachments)
                    .HasForeignKey(d => d.TicketId);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CategoryId)
                    .HasColumnName("CategoryID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Comments>(entity =>
            {
                entity.HasKey(e => e.CommentId);

                entity.ToTable("comments");

                entity.HasIndex(e => e.TicketId);

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.CommentId)
                    .HasColumnName("CommentID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasColumnName("text");

                entity.Property(e => e.TicketId).HasColumnName("TicketID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Ticket)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.TicketId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Priorities>(entity =>
            {
                entity.HasKey(e => e.PriorityId);

                entity.ToTable("priorities");

                entity.Property(e => e.PriorityId)
                    .HasColumnName("PriorityID")
                    .ValueGeneratedNever();

                entity.Property(e => e.PriorityName).IsRequired();
            });

            modelBuilder.Entity<ProcessFlows>(entity =>
            {
                entity.HasKey(e => e.ProcessFlowId);

                entity.ToTable("processFlows");

                entity.HasIndex(e => e.ParentProcessFlowId);

                entity.HasIndex(e => e.TenantId);

                entity.Property(e => e.ProcessFlowId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.ProcessFlowName).IsRequired();

                entity.Property(e => e.TenantId).HasColumnName("TenantID");

                entity.HasOne(d => d.ParentProcessFlow)
                    .WithMany(p => p.InverseParentProcessFlow)
                    .HasForeignKey(d => d.ParentProcessFlowId);

                entity.HasOne(d => d.Tenant)
                    .WithMany(p => p.ProcessFlows)
                    .HasForeignKey(d => d.TenantId);
            });

            modelBuilder.Entity<Reasons>(entity =>
            {
                entity.HasKey(e => e.ReasonId);

                entity.ToTable("reasons");

                entity.HasIndex(e => e.TicketId);

                entity.Property(e => e.ReasonId)
                    .HasColumnName("ReasonID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Content).IsRequired();

                entity.Property(e => e.TicketId).HasColumnName("TicketID");

                entity.HasOne(d => d.Ticket)
                    .WithMany(p => p.Reasons)
                    .HasForeignKey(d => d.TicketId);
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.HasKey(e => e.RoleId);

                entity.Property(e => e.RoleId)
                    .HasColumnName("RoleID")
                    .ValueGeneratedNever();

                entity.Property(e => e.RoleName).IsRequired();
            });

            modelBuilder.Entity<Severities>(entity =>
            {
                entity.HasKey(e => e.SeverityId);

                entity.ToTable("severities");

                entity.Property(e => e.SeverityId)
                    .HasColumnName("SeverityID")
                    .ValueGeneratedNever();

                entity.Property(e => e.SeverityName).IsRequired();
            });

            modelBuilder.Entity<Statuses>(entity =>
            {
                entity.HasKey(e => e.StatusId);

                entity.ToTable("statuses");

                entity.Property(e => e.StatusId)
                    .HasColumnName("StatusID")
                    .ValueGeneratedNever();

                entity.Property(e => e.StatusName).IsRequired();
            });

            modelBuilder.Entity<Statushistory>(entity =>
            {
                entity.ToTable("statushistory");

                entity.HasIndex(e => e.StatusId);

                entity.HasIndex(e => e.TicketId);

                entity.Property(e => e.StatusHistoryId)
                    .HasColumnName("StatusHistoryID")
                    .ValueGeneratedNever();

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.TicketId).HasColumnName("TicketID");

                entity.Property(e => e.TimeStamp).HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Statushistory)
                    .HasForeignKey(d => d.StatusId);

                entity.HasOne(d => d.Ticket)
                    .WithMany(p => p.Statushistory)
                    .HasForeignKey(d => d.TicketId);
            });

            modelBuilder.Entity<Taggableitems>(entity =>
            {
                entity.HasKey(e => new { e.TicketId, e.TagId });

                entity.ToTable("taggableitems");

                entity.HasIndex(e => e.TagId);

                entity.Property(e => e.TicketId).HasColumnName("TicketID");

                entity.Property(e => e.TagId).HasColumnName("TagID");

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.Taggableitems)
                    .HasForeignKey(d => d.TagId);

                entity.HasOne(d => d.Ticket)
                    .WithMany(p => p.Taggableitems)
                    .HasForeignKey(d => d.TicketId);
            });

            modelBuilder.Entity<Tags>(entity =>
            {
                entity.HasKey(e => e.TagId);

                entity.ToTable("tags");

                entity.Property(e => e.TagId)
                    .HasColumnName("TagID")
                    .ValueGeneratedNever();

                entity.Property(e => e.TagName).IsRequired();
            });

            modelBuilder.Entity<Tenants>(entity =>
            {
                entity.HasKey(e => e.TenantId);

                entity.ToTable("tenants");

                entity.Property(e => e.TenantId)
                    .HasColumnName("TenantID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<TicketCategory>(entity =>
            {
                entity.HasKey(e => new { e.TicketId, e.CategoryId });

                entity.HasIndex(e => e.CategoryId);

                entity.Property(e => e.TicketId).HasColumnName("TicketID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.TicketCategory)
                    .HasForeignKey(d => d.CategoryId);

                entity.HasOne(d => d.Ticket)
                    .WithMany(p => p.TicketCategory)
                    .HasForeignKey(d => d.TicketId);
            });

            modelBuilder.Entity<TicketHistories>(entity =>
            {
                entity.HasKey(e => e.TicketHistoryId);

                entity.ToTable("ticketHistories");

                entity.HasIndex(e => e.TicketId);

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.TicketHistoryId)
                    .HasColumnName("TicketHistoryID")
                    .ValueGeneratedNever();

                entity.Property(e => e.ChangeType)
                    .IsRequired()
                    .HasColumnName("changeType");

                entity.Property(e => e.TicketId).HasColumnName("TicketID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Ticket)
                    .WithMany(p => p.TicketHistories)
                    .HasForeignKey(d => d.TicketId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TicketHistories)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<TicketType>(entity =>
            {
                entity.Property(e => e.TicketTypeId)
                    .HasColumnName("TicketTypeID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Tickets>(entity =>
            {
                entity.HasKey(e => e.TicketId);

                entity.ToTable("tickets");

                entity.HasIndex(e => e.PriorityId);

                entity.HasIndex(e => e.ProcessFlowId);

                entity.HasIndex(e => e.SeverityId);

                entity.HasIndex(e => e.TenantId);

                entity.HasIndex(e => e.TicketTypeId);

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.TicketId)
                    .HasColumnName("TicketID")
                    .ValueGeneratedNever();

                entity.Property(e => e.PriorityId).HasColumnName("PriorityID");

                entity.Property(e => e.SeverityId).HasColumnName("SeverityID");

                entity.Property(e => e.TenantId).HasColumnName("TenantID");

                entity.Property(e => e.TicketTypeId).HasColumnName("TicketTypeID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Priority)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.PriorityId);

                entity.HasOne(d => d.ProcessFlow)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.ProcessFlowId);

                entity.HasOne(d => d.Severity)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.SeverityId);

                entity.HasOne(d => d.Tenant)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.TenantId);

                entity.HasOne(d => d.TicketType)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.TicketTypeId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<UserRoles>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserRolesId)
                    .HasColumnName("UserRolesID")
                    .ValueGeneratedNever();

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.RoleValue).IsRequired();

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("users");

                entity.HasIndex(e => e.TenantId);

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Email).IsRequired();

                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasDefaultValueSql("(0x)");

                entity.Property(e => e.Passwordsalt)
                    .IsRequired()
                    .HasDefaultValueSql("(0x)");

                entity.Property(e => e.TenantId).HasColumnName("TenantID");

                entity.Property(e => e.Username).IsRequired();

                entity.HasOne(d => d.Tenant)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.TenantId);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
