namespace ToDoList.Models
{
  public class CardPlayer
    {       
        public int CardPlayerId { get; set; }
        public int CardId { get; set; }
        public int PlayerId { get; set; }
        public virtual Card Card { get; set; }
        public virtual Player Player { get; set; }
    }
}