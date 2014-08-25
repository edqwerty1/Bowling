using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bowling.Domain.Abstract;
using Bowling.Domain.Entities;

namespace Bowling.Services.Implementation
{
   public class BowlerService : IBowlerService
   {
      private readonly IRepository<Bowler> bowlers;
      //private readonly ITeamService teamService;
      public BowlerService(IRepository<Bowler> bowlers)
      {
         this.bowlers = bowlers;
      //   this.teamService = teamService;
      }

      public Task< IEnumerable<Bowler>> GetBowlersAsync()
      { 
         return bowlers.GetAllAsync();
      }

      public Task<Bowler> GetBowlerAsync(int id)
      {
          return bowlers.GetSingleAsync(b => b.Id == id);
      }

       public Task AddBowler(Bowler bowler)
      {

           bowlers.Add(bowler);
           return UnitOfWork.CommitAsync(); 
      }

      //public IQueryable<Match> GetMatchesForDate(DateTime date)
      //{
      //   return matches.GetAll().Where(s => s.KickOff.Date == date.Date);
      //}

      //public int AddGoal(int matchId, int team, bool penalty = false)
      //{
      //   Match match = matches.Single(m => m.Id == matchId);
      //   Team teamEntity = teamService.GetTeam(team);
      //   match.AddGoal(teamEntity, penalty);
      //   UnitOfWork.Commit();

      //   return match.Goals.Where(t => t.Team.Id == team && t.Penalty == penalty).Count();
      //}

      //public int RemoveGoal(int matchId, int team, bool penalty = false)
      //{
      //   Match match = matches.Single(m => m.Id == matchId);
      //   Goal goal = match.Goals.First(g => g.Team.Id == team && g.Penalty == penalty);
         
      //   match.Goals.Remove(goal);
      //   UnitOfWork.Commit();

      //   return match.Goals.Where(t => t.Team.Id == team && t.Penalty == penalty).Count();
      //}

      //public void AddMatch(Match match)
      //{
      //   matches.Add(match);

      //   UnitOfWork.Commit();
      //}
   }
}
