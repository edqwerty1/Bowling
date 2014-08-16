using Bowling.Domain.Entities.Enums;

namespace Bowling.Domain.Entities
{
    public class FoodOption : BaseEntity
    {
        public string Name { get; set; }

        public FoodSide Side { get; set; }
    }
}
