using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ItransitionTask_5.Models
{
	public class Game
	{
		public int Id { get; set; }

		[ForeignKey("UserGame")]
		[Display(Name = "Id creator")]
		public string IdPlayer1 { get; set; }

		[ForeignKey("UserGame")]
		public string IdPlayer2 { get; set; }
		public string Moves { get; set; }
		public int CountMoves { get; set; }
		public int StepUser1 { get; set; }
		public int StepUser2 { get; set; }
		
	}
}
