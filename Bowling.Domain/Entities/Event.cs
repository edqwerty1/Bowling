using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bowling.Domain.Entities
{
    /// <summary>
    /// Bowling event night
    /// </summary>
    public class Event : BaseEntity
    {
        /// <summary>
        /// Name of event
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Event description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Price
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// List of Bowlers attending event
        /// </summary>
        public virtual ICollection<Attendee> Attendees {get;set;}
    }
}
