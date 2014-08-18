
namespace Bowling.Domain.Entities
{
    public class Round : BaseEntity
    {
        public int Score { get; set; }

        public virtual Result Result {get;set;}
    }
}
