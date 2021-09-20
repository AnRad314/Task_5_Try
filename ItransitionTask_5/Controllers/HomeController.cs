using ItransitionTask_5.Data;
using ItransitionTask_5.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ItransitionTask_5.Controllers
{
	public class HomeController : Controller
	{
		private readonly UserManager<UserGame> _userManager;
		private readonly SignInManager<UserGame> _signInManager;
		private readonly GameDbContext _context;
		public HomeController(UserManager<UserGame> userManager, SignInManager<UserGame> signInManager, GameDbContext context)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_context = context;
		}

		public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public IActionResult CreateGame()
		{				
			return View();
		}

		[HttpPost]
		public string CreateGame([FromBody] string[] moves)
		{			
			var user = _context.Users.FirstOrDefault(i => i.UserName == User.Identity.Name);
			Game game = new Game();			
			game.IdPlayer1 = user.Id;
			game.CountMoves = moves.Length;
			game.Moves = JsonConvert.SerializeObject(moves);			
			_context.Games.Add(game);
			_context.SaveChanges();
			return Url.Action("Play", "Home", new { id = game.Id });
		}

		public IActionResult Play(int id)
		{
			var movesJson = _context.Games.FirstOrDefault(i => i.Id == id).Moves;
			var moves = JsonConvert.DeserializeObject<String[]>(movesJson);
			ViewBag.IdGame = id;
			return View("Play", moves);
		}

		[HttpPost]
		public string Play([FromBody] string[] data)
		{
			int step = int.Parse(data[0]);
			int id = int.Parse(data[1]);			
			var game = _context.Games.FirstOrDefault(i => i.Id == id);
			if (game.StepUser1 == null)
			{
				game.StepUser1 = step;
			}
			else
			{
				game.StepUser2 = step;
			}
			_context.Games.Update(game);
			_context.SaveChanges();
			return Url.Action("ResultGame", "Home", new { id = game.Id });
		}

		public IActionResult ResultGame (int id)		{


			return View();
		}

		public IActionResult Rule()
		{
			return View();
		}



		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
