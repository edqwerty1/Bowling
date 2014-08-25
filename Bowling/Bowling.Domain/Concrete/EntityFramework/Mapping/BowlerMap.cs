using Bowling.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Bowling.Domain.Concrete.EntityFramework.Mapping
{
    /// <summary>
    /// Contact database field definition
    /// </summary>
    class BowlerMap : BaseEntityMap<Bowler>//: BaseEntityMap<Bowler>
    {
        /// <summary>
        /// Define the table fields
        /// </summary>
        public BowlerMap()
        {
            this.ToTable("Bowlers");
            this.Property(t => t.Name).IsRequired();
            this.Property(t => t.Name).HasMaxLength(50);
        }
    }
}