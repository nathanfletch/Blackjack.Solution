using System.Collections.Generic;

namespace ProjectName.Models
{
  public class Player
    {
        public Player()
        {
            this.JoinEntities = new HashSet<CardPlayer>();
        }

        public int PlayerId { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public int Bet { get; set; }
        public int Money { get; set; }
        public bool IsDealer { get; set; }
        public virtual ICollection<CardPlayer> JoinEntities { get; set; }
    }
}