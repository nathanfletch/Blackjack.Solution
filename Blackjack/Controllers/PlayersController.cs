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
      
      //draw 2
      for (int i = 1; i <=2; i++)
      {
        //get a list of the jointable elements
        List<int> usedCardIds = _db.CardPlayer.Select(cardPlayer => cardPlayer.CardId).ToList();
        int newCardId = Card.GetUniqueRandomId(usedCardIds);
        // += the cardvalue to the score prop, pass in to Entry()
        Card newCard = _db.Cards.FirstOrDefault(c => c.CardId == newCardId);
        player.Score += newCard.Value;
        _db.Entry(player).State = EntityState.Modified;
        _db.CardPlayer.Add(new CardPlayer() { CardId = newCardId, PlayerId = player.PlayerId  });
        _db.SaveChanges();
      }
      
      
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult Hit(int PlayerId)
    {
      Console.WriteLine($"Id: {PlayerId}");
      
      List<int> usedCardIds = _db.CardPlayer.Select(cardPlayer => cardPlayer.CardId).ToList();
      int newCardId = Card.GetUniqueRandomId(usedCardIds);
      // += the cardvalue to the score prop, pass in to Entry()
      Card newCard = _db.Cards.FirstOrDefault(c => c.CardId == newCardId);
      Player newPlayer = _db.Players.FirstOrDefault(p => p.PlayerId == PlayerId);
      //find player by id
      newPlayer.Score += newCard.Value;
      _db.Entry(newPlayer).State = EntityState.Modified;
      _db.CardPlayer.Add(new CardPlayer() { CardId = newCardId, PlayerId = newPlayer.PlayerId  });
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

  }
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
