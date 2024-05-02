
using Microsoft.EntityFrameworkCore;
using Support_Ticket_System.Entites;

namespace Support_Ticket_System.DataContext
{
    public class Datacontext : DbContext
    {


        public Datacontext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<User> users { get; set; }
        public DbSet<Ticket> tickets { get; set; }
        public DbSet<Priority> priorities { get; set; }
        public DbSet<Severity> severities { get; set; }
        public DbSet<Tenant> tenants { get; set; }
        public DbSet<Tag> tags { get; set; }
        public DbSet<taggableitem> taggableitems { get; set; }
        public DbSet<ProcessFlow> processFlows { get; set; }
        public DbSet<Status> statuses { get; set; }
        public DbSet<StatusHistory> statushistory { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }
        public DbSet<TicketHistory> ticketHistories { get; set; }
        public DbSet<Comment> comments { get; set; }
        public DbSet<Reason> reasons { get; set; }
        public DbSet<Attachment> attachments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<taggableitem>()
                .HasKey(ti => new { ti.TicketID, ti.TagID });

            modelBuilder.Entity<taggableitem>()
                .HasOne(ti => ti.ticket)
                .WithMany(t => t.tags)
                .HasForeignKey(ti => ti.TicketID);

            modelBuilder.Entity<taggableitem>()
                .HasOne(ti => ti.tag)
                .WithMany(t => t.taggableitems)
                .HasForeignKey(ti => ti.TagID);


        }


    }

}
