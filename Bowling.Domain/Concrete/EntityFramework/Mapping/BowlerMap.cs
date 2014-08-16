using Bowling.Domain.Entities;

namespace Bowling.Domain.Concrete.EntityFramework.Mapping
{
    /// <summary>
    /// Contact database field definition
    /// </summary>
    class BowlerMap : BaseEntityMap<Bowler>
    {
        /// <summary>
        /// Define the table fields
        /// </summary>
        public BowlerMap()
        {
            this.ToTable("Bowlers");
            this.HasRequired(t => t.Name);
        }
    }
}