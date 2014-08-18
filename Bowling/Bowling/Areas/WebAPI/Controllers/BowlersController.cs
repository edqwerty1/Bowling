using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Bowling.Domain.Entities;
using Bowling.Models;
using Bowling.Services;
using System.Collections.Generic;

namespace Bowling.Areas.WebAPI.Controllers
{
    public class BowlersController : ApiController
    {
        private readonly IBowlerService bowlerService;

        public BowlersController(IBowlerService bowlerService)
        {
            this.bowlerService = bowlerService;
        }

        // GET: api/Bowlers
        /// <summary>
        /// Returns all bowlers
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Bowler>> GetBowlers()
        {
            return await bowlerService.GetBowlersAsync();
        }

        // GET: api/Bowlers/5
        /// <summary>
        /// finds a bowler
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(Bowler))]
        public async Task<IHttpActionResult> GetBowler(int id)
        {
            Bowler bowler = await bowlerService.GetBowlerAsync(id);
            if (bowler == null)
            {
                return NotFound();
            }

            return Ok(bowler);
        }

        //// PUT: api/Bowlers/5
        //[ResponseType(typeof(void))]
        //public async Task<IHttpActionResult> PutBowler(int id, Bowler bowler)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != bowler.Id)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(bowler).State = EntityState.Modified;

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!BowlerExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        // POST: api/Bowlers
        /// <summary>
        /// Creates a bowler
        /// </summary>
        /// <param name="bowler"></param>
        /// <returns></returns>
        [ResponseType(typeof(Bowler))]
        public async Task<IHttpActionResult> PostBowler(Bowler bowler)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await bowlerService.AddBowler(bowler);

            return CreatedAtRoute("DefaultApi", new { id = bowler.Id }, bowler);
        }

        //    // DELETE: api/Bowlers/5
        //    [ResponseType(typeof(Bowler))]
        //    public async Task<IHttpActionResult> DeleteBowler(int id)
        //    {
        //        Bowler bowler = await db.Bowlers.FindAsync(id);
        //        if (bowler == null)
        //        {
        //            return NotFound();
        //        }

        //        db.Bowlers.Remove(bowler);
        //        await db.SaveChangesAsync();

        //        return Ok(bowler);
        //    }

        //    protected override void Dispose(bool disposing)
        //    {
        //        if (disposing)
        //        {
        //            db.Dispose();
        //        }
        //        base.Dispose(disposing);
        //    }

        //    private bool BowlerExists(int id)
        //    {
        //        return db.Bowlers.Count(e => e.Id == id) > 0;
        //    }
        //}
    }
}