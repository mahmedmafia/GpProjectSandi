using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GpProject.Models
{
    public class JoinedEvents
    {
        private ApplicationDbContext db;
        public JoinedEvents()
        {
            this.db = new ApplicationDbContext();
        }

        public int Id { get; set; }

        public virtual Person JoinedPerson { get; set; }
        public virtual Event Event { get; set; }

        public ICollection<Event> EventsJoined(Person UserToJoin)
        {
           var  joined= db.JoinedEvents.Where(p => p.JoinedPerson.Id == UserToJoin.Id).Select(p=>p.Event).ToList();
            return joined;
        }
        public ICollection<Person> PersonsJoined(Event JoindedEvent)
        {
            var joined = db.JoinedEvents.Where(e => e.Event.Id == JoindedEvent.Id).Select(p => p.JoinedPerson).ToList();
            return joined;
        }
        public bool IsUserJoined( Person UserToJoin, Event JoinedEvent,int id=0)
        {
            var EventToBeJoined = new Event();
            if (id != default(int))
            {
                EventToBeJoined = db.Events.FirstOrDefault(x => x.Id == id);
            }
            if (JoinedEvent!=null)
            {
                EventToBeJoined = JoinedEvent;
            }
            var joined = db.JoinedEvents.Where(p => p.JoinedPerson.Id == UserToJoin.Id).Where(e => e.Event.Id == EventToBeJoined.Id).ToList();
                if (joined.Count() == 0)
                {
                    return true;
                }
                else { return false; }
            
            
        }
    }
}