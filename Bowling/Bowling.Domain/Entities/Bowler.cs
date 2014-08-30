using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bowling.Domain.Entities
{
    /// <summary>
    /// A Bowler
    /// </summary>
    public class Bowler : BaseEntity
    {
        /// <summary>
        /// Display name of bowler
        /// </summary>
        public string Name { get; set; }

        public Guid User { get; set; }

        public int TotalGamesAttended
        {
            get
            {
                if (Attendees != null)
                {
                    return Attendees.Count;
                }
                return 0;
            }
        }

        public int TotalEvents
        {
            get
            {
                if (Attendees == null)
                {
                    return 0;
                }
                return Attendees.Count;
            }
        }

        public double AverageScore
        {
            get
            {
                if (Attendees == null)
                {
                    return 0.0;
                }
                double score = 0 ;
                foreach (Attendee attendee in Attendees)
                {
                    if (attendee.Results == null)
                    {
                        return 0.0;
                    }
                    score += attendee.Result;
                    
                }
                return (score / 2.0) / TotalEvents;
            }
        }

        /// <summary>
        /// List of all the times the bowler was an attendee of an event
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public virtual ICollection<Attendee> Attendees { get; set; }
    }
}
