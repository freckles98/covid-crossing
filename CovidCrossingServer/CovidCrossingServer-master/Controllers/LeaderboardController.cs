using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CovidCrossingServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LeaderboardController : ControllerBase
    {
        /// <summary>
        /// The Post method handles player requests to add there score to the leaderboard. The request takes the form
        /// of a POST to the leaderboard controller URI. If the client already has an entry on the leaderboard (as
        /// identified by the record GUID), their record will be updated with the new nickname and score (if changed).
        /// </summary>
        /// <param name="record">The LeaderboardRecord to add to (or update) the leaderboard</param>
        /// <param name="db">Database context obtained from services</param>
        /// <returns>An ActionResult indicating whether the request was successful or invalid</returns>
        [HttpPost]
        public ActionResult Post([FromBody] Record record, [FromServices] LeaderboardContext db)
        {
            if (record.Nickname == null || record.Nickname == "") { return BadRequest("Invalid nickname"); }
            if (record.Guid == null) { return BadRequest("Invalid GUID"); }
            var found = db.Records.SingleOrDefault(r => r.Guid == record.Guid);
            if (found != default)
            {
                found.Nickname = record.Nickname;
                found.HighScore = record.HighScore;
            }
            else
            {
                db.Records.Add(record);
            }
            db.SaveChanges();
            return Ok();
        }

        /// <summary>
        /// The Get method fetches the leaderboard records and sends it as a response to the client. Recordds are sorted
        /// in descending order according to high score.
        /// </summary>
        /// <param name="db">Database context obtained from services</param>
        /// <returns>The leaderboard contents as an enumerable of records</returns>
        [HttpGet]
        public IEnumerable<Record> Get([FromServices] LeaderboardContext db)
        {
            return db.Records.OrderByDescending(r => r.HighScore);
        }
    }
}
