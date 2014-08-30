using System.Collections.Generic;

namespace Bowling.Domain.Entities
{
    public class Result : BaseEntity
    {
        public virtual Event Event { get; set; }

        public virtual Attendee Attendee { get; set; }

        public virtual ICollection<Round> Rounds { get; set; }

        public int TotalScore
        {
            get
            {
                if (Rounds == null)
                {
                    return 0;
                }
                int result = 0;
                foreach(Round round in Rounds)
                {
                    result += round.Score;
                }
                return result;
            }
        }
    }
}
