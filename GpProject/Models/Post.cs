using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GpProject.Models
{
    public class Post
    {
        [Key]
        [Column("Post_Id")]
        public int Id { get; set; }
        public String Content { get; set; }

        public DateTime? DatePosted { get; set; }
        public int PersonId { get; set; }

        public Person Person { get; set; }
        public int EventId { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }

    }
}