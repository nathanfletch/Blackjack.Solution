using Microsoft.AspNetCore.Mvc;

namespace Blackjack.Controllers
{
  public class HomeController : Controller
  {
    [HttpGet("/")]
    public ActionResult Index() { 
      return View(); 
    }
  }
}