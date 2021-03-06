﻿using System.Web.Mvc;

namespace Bowling.Areas.WebAPI
{
    public class WebAPIAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "WebAPI";
            }
        }

        //public override void RegisterArea(AreaRegistrationContext context) 
        //{
        //    context.MapRoute(
        //        "WebAPI_default",
        //        "WebAPI/{controller}/{id}",
        //        new { action = "Index", id = UrlParameter.Optional }
        //    );


        //}

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "WebAPI_default",
                "WebAPI/{action}/{apiId}",
                new { controller = "Bowlers", action = "Index", apiId = UrlParameter.Optional });

        }
    }
}