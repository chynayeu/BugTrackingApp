namespace BugTrackingApp.service.model
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Project")]
    public partial class Project
    {
        [Required]
        [StringLength(50)]
        public string name { get; set; }

        [Required]
        public string description { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public virtual ICollection<User> Users { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
