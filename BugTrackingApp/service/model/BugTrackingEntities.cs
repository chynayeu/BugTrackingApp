using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace BugTrackingApp.service.model
{
    public partial class BugTrackingEntities : DbContext
    {
        public BugTrackingEntities()
            : base("name=BugTrackingEntities")
        {
        }

        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //One-to-many Ticket -> Comments
            modelBuilder.Entity<Ticket>()
                .HasMany(t => t.Comments).WithRequired(t => t.Ticket)
                .Map(m => m.MapKey("ticketId")).WillCascadeOnDelete(true);

            //One-to-many User -> comments
            modelBuilder.Entity<User>()
                .HasMany(e => e.Comments).WithRequired(e => e.User)
                .Map(m => m.MapKey("userId")).WillCascadeOnDelete(true);

            //One-to-many Project -> Tickets
            modelBuilder.Entity<Project>()
                .HasMany(t => t.Tickets).WithRequired(t => t.Project)
                .Map(m => m.MapKey("projectId")).WillCascadeOnDelete(true);

            // Many-to-many User - Project
            modelBuilder.Entity<User>()
                .HasMany<Project>(t => t.Projects).WithMany(t => t.Users)
                .Map(t => t.MapRightKey("projectId").MapLeftKey("userId").ToTable("UserProject"));
        }
    }
}
