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
      
      // test and draw 2?
      for (int i = 0; i < players.Count; i++)
      {
        List<CardPlayer> cardsInAllHands = _db.CardPlayer.ToList();
        List<CardPlayer> cardsInCurrentPlayersHand = cardsInAllHands.Where(cardPlayer => cardPlayer.PlayerId == players[i].PlayerId).ToList();

        if(cardsInCurrentPlayersHand.Count == 0)
        {
          ClearScore(players[i]);
          Draw(players[i]);
          Draw(players[i]);
        }
      }
      return View(players);
    }
    [HttpPost]
    public ActionResult Create(Player player)
    {
      //Add another player - try after this works
      _db.Players.Add(player);
      _db.SaveChanges();

      //draw 2
      Draw(player);
      Draw(player);
     
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult Hit(int PlayerId)
    {
      // += the cardvalue to the score prop, pass in to Entry()
      Player newPlayer = _db.Players.FirstOrDefault(player => player.PlayerId == PlayerId);
      //find player by id
      Draw(newPlayer);
      return RedirectToAction("Index");
    }

    public void Draw(Player player)
    {
      List<Card> cardList = _db.Cards.ToList();

      Random generator = new Random();
      int newCardId = generator.Next(cardList[0].CardId, cardList[12].CardId) + 1;
      // += the cardvalue to the score prop, pass in to Entry()
      Card newCard = _db.Cards.FirstOrDefault(c => c.CardId == newCardId);
      
      player.Score += newCard.Value;
      _db.Entry(player).State = EntityState.Modified;
      _db.CardPlayer.Add(new CardPlayer() { CardId = newCardId, PlayerId = player.PlayerId });
      _db.SaveChanges();
    }

    public void ClearScore(Player player)
    {
      player.Score = 0;
      _db.Entry(player).State = EntityState.Modified;
      _db.SaveChanges();
    }
  }
}

// var result = from person in _db.Person
//              select new
//              {
//                 id = person.Id,
//                 firstname = person.Firstname,
//                 lastname = person.Lastname,
//                 detailText = person.PersonDetails.Select(d => d.DetailText).SingleOrDefault()
//             };

      // var query = from card in _db.Cards
      //       join cardPlayer in _db.CardPlayer
      //           on card.CardId equals cardPlayer.CardId into grouping
      //       from cardPlayer in grouping.DefaultIfEmpty()
      //       select new { card, cardPlayer };
      // var queryList = query.ToList();
      // Console.WriteLine($"Count: {queryList.Count}");
