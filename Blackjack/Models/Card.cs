using System.Collections.Generic;

namespace Blackjack.Models
{
    public class Card
    {
        public Card(int value, string name)
        {
          Value = value;
          Name = name;
          this.JoinEntities = new HashSet<CardPlayer>();
        }

        public int CardId { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }

        public virtual ICollection<CardPlayer> JoinEntities { get;}
    }
}