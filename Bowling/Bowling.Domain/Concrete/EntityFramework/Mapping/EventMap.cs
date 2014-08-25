using Bowling.Domain.Entities;

namespace Bowling.Domain.Concrete.EntityFramework.Mapping
{
    /// <summary>
    /// Contact database field definition
    /// </summary>
    class EventMap : BaseEntityMap<Event>
    {
        /// <summary>
        /// Define the table fields
        /// </summary>
        public EventMap()
        {
            this.ToTable("Events");
            this.Property(t => t.Name).IsRequired().HasMaxLength(100);
            this.Property(t => t.Description).IsOptional().HasMaxLength(500);
        }
    }
}