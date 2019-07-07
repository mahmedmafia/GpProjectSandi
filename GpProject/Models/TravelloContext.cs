//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;
//using System.Web;

//namespace GpProject.Models
//{
//    public class TravelloContext : DbContext
//    {
//        // You can add custom code to this file. Changes will not be overwritten.
//        // 
//        // If you want Entity Framework to drop and regenerate your database
//        // automatically whenever you change your model schema, please use data migrations.
//        // For more information refer to the documentation:
//        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
//        public TravelloContext() : base("name=TravelloContext")
//        {
//        }

//        public DbSet<Person> Persons { get; set; }
//        public DbSet<Event> Events { get; set; }
//        public DbSet<Post> Posts { get; set; }
//        public DbSet<Comment> Comments { get; set; }
//        public DbSet<JoinedEvents> JoinedEvents{ get; set; }

//    }
//}
