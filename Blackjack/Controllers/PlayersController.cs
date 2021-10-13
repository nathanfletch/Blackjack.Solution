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
    public ActionResult Index()
    {
      List<Player> players = _db.Players.ToList();
      return View(players);
    }
    [HttpPost]
    public ActionResult Create(Player player)
    {
      //Add another player - try after this works
      _db.Players.Add(player);
      _db.SaveChanges();

      for (int i = 1; i <=2; i++)
      {
        //get a list of the jointable elements
        List<int> usedCardIds = _db.CardPlayer.Select(cardPlayer => cardPlayer.CardId).ToList();

        //use the random while loop thing to test if it's in that list
        Random generator = new Random();
        int newId = generator.Next(104) + 1;
        while(usedCardIds.Contains(newId))
        {
          Console.WriteLine($"Old crappy id: {newId}");
          newId = generator.Next(104) + 1;
        }
      _db.CardPlayer.Add(new CardPlayer() { CardId = newId, PlayerId = player.PlayerId  });
      _db.SaveChanges();
      }
      // var result = from person in _db.Person
      //        select new
      //        {
      //           id = person.Id,
      //           firstname = person.Firstname,
      //           lastname = person.Lastname,
      //           detailText = person.PersonDetails.Select(d => d.DetailText).SingleOrDefault()
      //       };

      // var query = from card in _db.Cards
      //       join cardPlayer in _db.CardPlayer
      //           on card.CardId equals cardPlayer.CardId into grouping
      //       from cardPlayer in grouping.DefaultIfEmpty()
      //       select new { card, cardPlayer };
      // var queryList = query.ToList();
      // Console.WriteLine($"Count: {queryList.Count}");
      return RedirectToAction("Index");
    }


  }
}


