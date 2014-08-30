using System.Web.Mvc;

namespace Bowling.Areas.Bowling
{
    public class BowlingAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Bowling";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Bowling_default",
                "Bowling/{*.}",
                new { controller = "Index", action = "Index" }
            );
        }
    }
}