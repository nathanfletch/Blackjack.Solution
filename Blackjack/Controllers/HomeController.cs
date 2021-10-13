using Microsoft.AspNetCore.Mvc;
using System.Linq;
// .filter: .Where; .map .Select; .sort .OrderBy;
using Blackjack.Models;
using System.Collections.Generic;
// using System.Data.Entity;

namespace Blackjack.Controllers
{
  public class HomeController : Controller
  {
    private readonly BlackjackContext _db;

    public HomeController(BlackjackContext db)
    {
      _db = db;
    }
    
    [HttpGet("/")]
    public ActionResult Index() { 
      

      //reset hands
      _db.CardPlayer.RemoveRange(_db.CardPlayer);
      _db.SaveChanges();

      if(_db.Cards.ToList().Count != 104)
      {
        for(int i = 1; i <= 13; i++)
        {
          string cardName;
          int cardValue;

          switch(i)
          {
            case 1:
              cardName = "A";
              cardValue = 1;
            break;

            case 11:
              cardName = "J";
              cardValue = 10;
            break;

            case 12:
              cardName = "Q";
              cardValue = 10;
            break;

            case 13:
              cardName = "K";
              cardValue = 10;
            break;

            default:
              cardName = i.ToString();
              cardValue = i;
            break;
          }
          for (int j = 0; j < 8; j++)
          {
            Card newCard = new Card(cardValue, cardName);
            _db.Cards.Add(newCard);
          }
        }
        _db.SaveChanges();
      }
      return View(); 
    }
  }
}