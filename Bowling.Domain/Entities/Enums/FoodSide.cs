
using System.ComponentModel;
using System.Linq;
namespace Bowling.Domain.Entities.Enums
{
    public enum FoodSide
    {
        None = 0,
        [Description("Chips")]
        Chips = 1,
        [Description("Curly Fries")]
        CurlyFries = 2
    }

    public static class MatchTypeExtensions
    {

        public static string Description(this FoodSide self)
        {
            return (self
                  .GetType()
                  .GetField(self.ToString())
                  .GetCustomAttributes(typeof(DescriptionAttribute), false)
                  .SingleOrDefault() as DescriptionAttribute)
                  .Description;
        }
    }

}
