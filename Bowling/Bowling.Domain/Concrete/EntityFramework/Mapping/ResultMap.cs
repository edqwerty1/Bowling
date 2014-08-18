using Bowling.Domain.Entities;

namespace Bowling.Domain.Concrete.EntityFramework.Mapping
{
    /// <summary>
    /// Contact database field definition
    /// </summary>
    class ResultMap : BaseEntityMap<Result>
    {
        /// <summary>
        /// Define the table fields
        /// </summary>
        public ResultMap()
        {
            this.ToTable("Results");
            this.HasRequired(t => t.Attendee).WithMany();
            this.HasRequired(t => t.Event).WithMany();
            this.HasMany(t => t.Rounds).WithRequired(t => t.Result);
        }
    }
}