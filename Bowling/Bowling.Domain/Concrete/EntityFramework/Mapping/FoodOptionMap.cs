using Bowling.Domain.Entities;

namespace Bowling.Domain.Concrete.EntityFramework.Mapping
{
    /// <summary>
    /// Contact database field definition
    /// </summary>
    class FoodOptionMap : BaseEntityMap<FoodOption>
    {
        /// <summary>
        /// Define the table fields
        /// </summary>
        public FoodOptionMap()
        {
            this.ToTable("FoodOptions");
            this.HasRequired(t => t.Name);
        }
    }
}