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
        public int TypeId { get; set; }
        public PostType Type { get; set; }

        public Person Person { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }

    }
    public class  HomePost: Post
    {
        public HomePost()
        {
            this.TypeId = PostType.Profile;
        }
    }
    public class EventPost:Post
    {
        public EventPost()
        {
            this.TypeId = PostType.Event;
        }
        public int EventId { get; set; }

    }
    public class Question : Post
    {
        public Question()
        {
            this.TypeId = PostType.Question;
        }
    }
}