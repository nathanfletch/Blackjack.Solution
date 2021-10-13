using Microsoft.EntityFrameworkCore;

namespace Blackjack.Models
{
  public class BlackjackContext : DbContext
  {
    public DbSet<Card> Cards { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<CardPlayer> CardPlayer { get; set; }

    public BlackjackContext(DbContextOptions options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseLazyLoadingProxies();
    }
  }
}