﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Support_Ticket_System.DataContext;

#nullable disable

namespace Support_Ticket_System.Migrations
{
    [DbContext(typeof(Datacontext))]
    [Migration("20240422140107_9")]
    partial class _9
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Support_Ticket_System.Entites.Priority", b =>
                {
                    b.Property<Guid>("PriorityID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PriorityName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PriorityID");

                    b.ToTable("priorities");
                });

            modelBuilder.Entity("Support_Ticket_System.Entites.ProcessFlow", b =>
                {
                    b.Property<Guid>("ProcessFlowId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ParentProcessFlowId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ProcessFlowName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("TenantID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ProcessFlowId");

                    b.HasIndex("ParentProcessFlowId");

                    b.HasIndex("TenantID");

                    b.ToTable("processFlows");
                });

            modelBuilder.Entity("Support_Ticket_System.Entites.Severity", b =>
                {
                    b.Property<Guid>("SeverityID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("SeverityName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SeverityID");

                    b.ToTable("severities");
                });

            modelBuilder.Entity("Support_Ticket_System.Entites.Status", b =>
                {
                    b.Property<Guid>("StatusID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("StatusName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StatusID");

                    b.ToTable("statuses");
                });

            modelBuilder.Entity("Support_Ticket_System.Entites.StatusHistory", b =>
                {
                    b.Property<Guid>("StatusHistoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("StatusID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("StatusValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("TicketID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("datetime2");

                    b.HasKey("StatusHistoryID");

                    b.HasIndex("StatusID");

                    b.HasIndex("TicketID");

                    b.ToTable("statushistory");
                });

            modelBuilder.Entity("Support_Ticket_System.Entites.Tag", b =>
                {
                    b.Property<Guid>("TagID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("TagName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TagID");

                    b.ToTable("tags");
                });

            modelBuilder.Entity("Support_Ticket_System.Entites.Tenant", b =>
                {
                    b.Property<Guid>("TenantID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TenantID");

                    b.ToTable("tenants");
                });

            modelBuilder.Entity("Support_Ticket_System.Entites.Ticket", b =>
                {
                    b.Property<Guid>("TicketID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AssignTo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("PriorityID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ProcessFlowId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("SeverityID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("TenantID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("UserID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("TicketID");

                    b.HasIndex("PriorityID");

                    b.HasIndex("ProcessFlowId");

                    b.HasIndex("SeverityID");

                    b.HasIndex("TenantID");

                    b.HasIndex("UserID");

                    b.ToTable("tickets");
                });

            modelBuilder.Entity("Support_Ticket_System.Entites.TicketHistory", b =>
                {
                    b.Property<Guid>("TicketHistoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("NewValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OldValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("TicketID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("changeType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TicketHistoryID");

                    b.HasIndex("TicketID");

                    b.HasIndex("UserID");

                    b.ToTable("TicketHistory");
                });

            modelBuilder.Entity("Support_Ticket_System.Entites.User", b =>
                {
                    b.Property<Guid>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("Passwordsalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<Guid>("TenantID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserID");

                    b.HasIndex("TenantID");

                    b.ToTable("users");
                });

            modelBuilder.Entity("Support_Ticket_System.Entites.taggableitem", b =>
                {
                    b.Property<Guid>("TicketID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TagID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("TicketID", "TagID");

                    b.HasIndex("TagID");

                    b.ToTable("taggableitems");
                });

            modelBuilder.Entity("Support_Ticket_System.Entites.ProcessFlow", b =>
                {
                    b.HasOne("Support_Ticket_System.Entites.ProcessFlow", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentProcessFlowId");

                    b.HasOne("Support_Ticket_System.Entites.Tenant", "tenant")
                        .WithMany("processflows")
                        .HasForeignKey("TenantID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Parent");

                    b.Navigation("tenant");
                });

            modelBuilder.Entity("Support_Ticket_System.Entites.StatusHistory", b =>
                {
                    b.HasOne("Support_Ticket_System.Entites.Status", "status")
                        .WithMany()
                        .HasForeignKey("StatusID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Support_Ticket_System.Entites.Ticket", "Ticket")
                        .WithMany()
                        .HasForeignKey("TicketID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ticket");

                    b.Navigation("status");
                });

            modelBuilder.Entity("Support_Ticket_System.Entites.Ticket", b =>
                {
                    b.HasOne("Support_Ticket_System.Entites.Priority", "priority")
                        .WithMany()
                        .HasForeignKey("PriorityID");

                    b.HasOne("Support_Ticket_System.Entites.ProcessFlow", "processFlow")
                        .WithMany("tickets")
                        .HasForeignKey("ProcessFlowId");

                    b.HasOne("Support_Ticket_System.Entites.Severity", "severity")
                        .WithMany()
                        .HasForeignKey("SeverityID");

                    b.HasOne("Support_Ticket_System.Entites.Tenant", "tenant")
                        .WithMany("tickets")
                        .HasForeignKey("TenantID");

                    b.HasOne("Support_Ticket_System.Entites.User", "user")
                        .WithMany("Tickets")
                        .HasForeignKey("UserID");

                    b.Navigation("priority");

                    b.Navigation("processFlow");

                    b.Navigation("severity");

                    b.Navigation("tenant");

                    b.Navigation("user");
                });

            modelBuilder.Entity("Support_Ticket_System.Entites.TicketHistory", b =>
                {
                    b.HasOne("Support_Ticket_System.Entites.Ticket", "Ticket")
                        .WithMany("ticketHistories")
                        .HasForeignKey("TicketID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Support_Ticket_System.Entites.User", "user")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ticket");

                    b.Navigation("user");
                });

            modelBuilder.Entity("Support_Ticket_System.Entites.User", b =>
                {
                    b.HasOne("Support_Ticket_System.Entites.Tenant", "tenant")
                        .WithMany("users")
                        .HasForeignKey("TenantID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("tenant");
                });

            modelBuilder.Entity("Support_Ticket_System.Entites.taggableitem", b =>
                {
                    b.HasOne("Support_Ticket_System.Entites.Tag", "tag")
                        .WithMany("taggableitems")
                        .HasForeignKey("TagID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Support_Ticket_System.Entites.Ticket", "ticket")
                        .WithMany("tags")
                        .HasForeignKey("TicketID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("tag");

                    b.Navigation("ticket");
                });

            modelBuilder.Entity("Support_Ticket_System.Entites.ProcessFlow", b =>
                {
                    b.Navigation("Children");

                    b.Navigation("tickets");
                });

            modelBuilder.Entity("Support_Ticket_System.Entites.Tag", b =>
                {
                    b.Navigation("taggableitems");
                });

            modelBuilder.Entity("Support_Ticket_System.Entites.Tenant", b =>
                {
                    b.Navigation("processflows");

                    b.Navigation("tickets");

                    b.Navigation("users");
                });

            modelBuilder.Entity("Support_Ticket_System.Entites.Ticket", b =>
                {
                    b.Navigation("tags");

                    b.Navigation("ticketHistories");
                });

            modelBuilder.Entity("Support_Ticket_System.Entites.User", b =>
                {
                    b.Navigation("Tickets");
                });
#pragma warning restore 612, 618
        }
    }
}
