using System;
using System.Collections.Generic;
using System.Linq;
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
        public string Name { get;set; }

        /// <summary>
        /// List of all the times the bowler was an attendee of an event
        /// </summary>
        public virtual ICollection<Attendee> Attendees { get; set; }
    }
}
