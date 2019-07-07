using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GpProject.Models
{
    public class Person
    {
        public int Id { get; set; }
        public String FirstName { get; set; }

        public String LastName { get; set; }
        [ForeignKey("OwnerId")]
        public virtual ICollection<Event> Events { get; set; }
    }
}