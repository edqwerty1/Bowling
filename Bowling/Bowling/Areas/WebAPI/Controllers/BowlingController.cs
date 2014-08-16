using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using Bowling.Domain.Entities;
using Bowling.Models;

namespace Bowling.Areas.WebAPI.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using Bowling.Domain.Entities;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Bowler>("Bowling");
    builder.EntitySet<Attendee>("Attendees"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class BowlingController : ODataController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: odata/Bowling
        [EnableQuery]
        public IQueryable<Bowler> GetBowling()
        {
            return db.Bowlers;
        }

        // GET: odata/Bowling(5)
        [EnableQuery]
        public SingleResult<Bowler> GetBowler([FromODataUri] int key)
        {
            return SingleResult.Create(db.Bowlers.Where(bowler => bowler.Id == key));
        }

        // PUT: odata/Bowling(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<Bowler> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Bowler bowler = await db.Bowlers.FindAsync(key);
            if (bowler == null)
            {
                return NotFound();
            }

            patch.Put(bowler);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BowlerExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(bowler);
        }

        // POST: odata/Bowling
        public async Task<IHttpActionResult> Post(Bowler bowler)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Bowlers.Add(bowler);
            await db.SaveChangesAsync();

            return Created(bowler);
        }

        // PATCH: odata/Bowling(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Bowler> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Bowler bowler = await db.Bowlers.FindAsync(key);
            if (bowler == null)
            {
                return NotFound();
            }

            patch.Patch(bowler);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BowlerExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(bowler);
        }

        // DELETE: odata/Bowling(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Bowler bowler = await db.Bowlers.FindAsync(key);
            if (bowler == null)
            {
                return NotFound();
            }

            db.Bowlers.Remove(bowler);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Bowling(5)/Attendees
        [EnableQuery]
        public IQueryable<Attendee> GetAttendees([FromODataUri] int key)
        {
            return db.Bowlers.Where(m => m.Id == key).SelectMany(m => m.Attendees);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BowlerExists(int key)
        {
            return db.Bowlers.Count(e => e.Id == key) > 0;
        }
    }
}
