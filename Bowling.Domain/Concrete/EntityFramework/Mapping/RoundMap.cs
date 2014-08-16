using Bowling.Domain.Entities;

namespace Bowling.Domain.Concrete.EntityFramework.Mapping
{
    /// <summary>
    /// Contact database field definition
    /// </summary>
    class RoundMap : BaseEntityMap<Round>
    {
        /// <summary>
        /// Define the table fields
        /// </summary>
        public RoundMap()
        {
            this.ToTable("Rounds");
        }
    }
}