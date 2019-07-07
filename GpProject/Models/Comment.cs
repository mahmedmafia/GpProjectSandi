using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace GpProject.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }

        public int PersonId { get; set; }
        public virtual Person Person { get; set; }
        public DateTime? DateCommented { get; set; }

        public int PostId { get; set; }

    }
}