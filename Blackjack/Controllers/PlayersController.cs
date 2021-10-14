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
      
      Random selectP1 = new Random();
      int playerTurn = selectP1.Next(2);
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
          if (i == playerTurn)
          {
            players[i].IsPlaying = true;
            players[(i-1)*-1].IsPlaying = false;
            _db.Entry(players[i]).State = EntityState.Modified;
            _db.Entry(players[(i-1)*-1]).State = EntityState.Modified;
            _db.SaveChanges();
          }
          //set turn 
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
    
    public ActionResult Hold()
    {
      Console.WriteLine($"Hi from hold!");
      
      //get our players
      List<Player> players = _db.Players.ToList();
      //toggle their isplaying booleans
      foreach (Player player in players)
      {
        Console.WriteLine($"{player.Name} - {player.IsPlaying}");
        player.IsPlaying = !player.IsPlaying;
        _db.Entry(player).State = EntityState.Modified;
      }
      //saveChanges
      _db.SaveChanges();
      //redirect to index
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult Hit(int PlayerId)
    {
      // += the cardvalue to the score prop, pass in to Entry()
      Player newPlayer = _db.Players.FirstOrDefault(player => player.PlayerId == PlayerId);
      //find player by id
      Draw(newPlayer);

      if(newPlayer.Score > 21)
      {
        List<Player> players = _db.Players.ToList();

        for (int i = 0; i < players.Count; i++)
        {
          if (players[i].IsPlaying)
          {
            int otherPlayersIndex = (i - 1) * -1;
            players[i].Money -= players[i].Bet;
            // players[i].Bet = 0;
            players[otherPlayersIndex].Money += players[i].Bet;
            // players[otherPlayersIndex].Bet = 0;
            _db.Entry(players[i]).State = EntityState.Modified;
            _db.Entry(players[otherPlayersIndex]).State = EntityState.Modified;
            _db.SaveChanges();
          }
        }
        return RedirectToAction("Result");
      }
      else if (newPlayer.Score == 21)
      {
        List<Player> players = _db.Players.ToList();

        for (int i = 0; i < players.Count; i++)
        {
          if (players[i].IsPlaying)
          {
            int otherPlayersIndex = (i - 1) * -1;
            players[i].Money += players[i].Bet;
            // players[i].Bet = 0;
            players[otherPlayersIndex].Money -= players[i].Bet;
            // players[otherPlayersIndex].Bet = 0;
            _db.Entry(players[i]).State = EntityState.Modified;
            _db.Entry(players[otherPlayersIndex]).State = EntityState.Modified;
            _db.SaveChanges();
          }
        }
        return RedirectToAction("Result");
      }
      else{
        return RedirectToAction("Index");
      }
    }

    public ActionResult Result()
    {
      //update money - subtract the bet value, add to other player
      List<Player> players = _db.Players.ToList();
      
      return View(players);
      //show scores for both players - pass the list of players
      //play again
    }
    public ActionResult Win()
    {
      return View();
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

/*
  wincheck
  hold/pass turn route
  check with an if in the index view - display buttons only to the current player




  currentTurnPlayer: lots of updates
  bet
  winner/loser/in progress - null?


  players - join table

  multiple games? a row in the table would represent the game 

  index of all games would be the history display

  cols are the properties
  
  just one?

*/


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
