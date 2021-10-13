using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Blackjack.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Blackjack.Controllers
{
  public class PlayersController : Controller
  {
    private readonly BlackjackContext _db;

    public PlayersController(BlackjackContext db)
    {
      _db = db;
    }


  //Index
    public ActionResult Index()
    {
      List<Player> players = _db.Players.ToList();
      return View(players);
    }
  //create post
    [HttpPost]
    public ActionResult Create(Player player)
    {
      //Add another player - try after this works
      _db.Players.Add(player);
      Random generator = new Random();
      int newId = generator.Next(104) + 1;
      _db.SaveChanges();
      
      _db.CardPlayer.Add(new CardPlayer() { CardId = newId, PlayerId = player.PlayerId  });
      _db.SaveChanges();

      // var query = from card in _db.Cards
      //       join cardPlayer in _db.CardPlayer
      //           on card.CardId equals cardPlayer.CardId into grouping
      //       from cardPlayer in grouping.DefaultIfEmpty()
      //       select new { card, cardPlayer };
      // Console.WriteLine($"Type: {query.GetType()}");

      
              // public Random a = new Random(); // replace from new Random(DateTime.Now.Ticks.GetHashCode());
              //                                 // Since similar code is done in default constructor internally
              // public List<int> randomList = new List<int>();
              // int MyNumber = 0;
              // private void NewNumber()
              // {
            //     MyNumber = a.Next(0, 10);
            //     while(randomList.Contains(MyNumber))
            //       MyNumber = a.Next(0, 10);
              // }


      

      //no duplicates
      //in the cards table, not in join table  .Where .Contains
      //inner join operation
      //inCards && !inJoin



      
      return RedirectToAction("Index");
    }


  }
}


