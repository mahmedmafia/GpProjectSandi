using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GpProject.Models
{
    public class Event
    {
        public int Id { get; set; }
        [Required]
        public String Name { get; set; }
        [Required]

        public String Description { get; set; }

        public DateTime? DateCreated { get; set; }
        [Required]

        public DateTime? DayStart { get; set; }
        [Required]

        [DisplayFormat(DataFormatString = "{0:h:mm tt}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Time)]
        public DateTime? TimeStart { get; set; }
        public bool Active { get; set; }
        [Required]
        public String Location { get; set; }
        
        public int OwnerId { get; set; }
        public Person Owner { get; set; }
        [ForeignKey("EventId")]
        public virtual ICollection<EventPost> Posts { get; set; }

    }
}