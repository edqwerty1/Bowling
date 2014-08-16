using Bowling.Domain.Entities;
using System;
using System.Linq;

namespace Bowling.Services
{
   public interface IBowlerService
   {
       IQueryable<Bowler> GetBowlers();
      //IQueryable<Match> GetMatches();
      //IQueryable<Match> GetMatchesForDate(DateTime date);
      //int AddGoal(int matchId, int team, bool penalty = false);
      //int RemoveGoal(int matchId, int team, bool penalty = false);
      //void AddMatch(Match match);
   }
}
