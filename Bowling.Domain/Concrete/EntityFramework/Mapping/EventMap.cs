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
            this.HasRequired(t => t.Name);
            this.HasOptional(t => t.Description);
        }
    }
}