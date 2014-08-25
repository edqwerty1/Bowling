using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bowling.Domain.Entities
{
    /// <summary>
    /// Record of which bowlers attended each event, along with their food option and score
    /// </summary>
    public class Attendee : BaseEntity
    {
        /// <summary>
        /// Bowler who attended event
        /// </summary>
        public virtual Bowler Bowler { get; private set; }

        /// <summary>
        /// Event Attended
        /// </summary>
        public virtual Event Event { get; private set; }

        /// <summary>
        /// Choice of food
        /// </summary>
        public virtual FoodOption FoodOption { get; set; }

        /// <summary>
        /// Has the bowler paid for the event
        /// </summary>
        public bool Paid { get; private set; }

        /// <summary>
        /// Date and Time bowler paid for the event
        /// </summary>
        public DateTime? PaidDateTime { get; private set; }

        /// <summary>
        /// Date and Time Bowler signed up for the event
        /// </summary>
        public DateTime SignUpDateTime { get; private set; }

        /// <summary>
        /// Bowling scores
        /// </summary>
        public virtual ICollection<Result> Results { get; set; }

        /// <summary>
        /// only constructor
        /// </summary>
        /// <param name="bowler"></param>
        /// <param name="bowlingEvent"></param>
        /// <param name="foodOption"></param>
        public Attendee(Bowler bowler, Event bowlingEvent, FoodOption foodOption)
        {
            SignUpDateTime = DateTime.Now;
            Bowler = bowler;
            Event = bowlingEvent;
            FoodOption = foodOption;
        }

        public Attendee()
        { }

        public void Pay()
        {
            Paid = true;
            PaidDateTime = DateTime.Now;

        }
    }
}
