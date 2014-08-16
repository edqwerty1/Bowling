using Bowling.Domain.Entities;

namespace Bowling.Domain.Concrete.EntityFramework.Mapping
{
    /// <summary>
    /// Contact database field definition
    /// </summary>
    class AttendeeMap : BaseEntityMap<Attendee>
    {
        /// <summary>
        /// Define the table fields
        /// </summary>
        public AttendeeMap()
        {
            this.ToTable("Attendees");
            this.HasRequired(t => t.Bowler).WithMany(t => t.Attendees);
            this.HasRequired(t => t.Event).WithMany(t => t.Attendees);
            this.HasRequired(t => t.FoodOption).WithMany();
            this.HasMany(t => t.Results).WithRequired(t => t.Attendee);
        }
    }
}