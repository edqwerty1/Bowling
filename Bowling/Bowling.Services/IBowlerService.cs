using Bowling.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bowling.Services
{
   public interface IBowlerService
   {
       Task<IEnumerable<Bowler>> GetBowlersAsync();
       Task<Bowler> GetBowlerAsync(int id);

       Task<Bowler> GetBowlerByUserAsync(Guid id);

       Task AddBowler(Bowler bowler);
      //IQueryable<Match> GetMatches();
      //IQueryable<Match> GetMatchesForDate(DateTime date);
      //int AddGoal(int matchId, int team, bool penalty = false);
      //int RemoveGoal(int matchId, int team, bool penalty = false);
      //void AddMatch(Match match);
   }
}
