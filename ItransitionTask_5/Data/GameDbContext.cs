using ItransitionTask_5.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItransitionTask_5.Data
{
	public class GameDbContext : IdentityDbContext<UserGame>
	{
		public GameDbContext(DbContextOptions<GameDbContext> options)
			: base(options)
		{
			Database.EnsureCreated();
		}
		public DbSet<Game> Games { get; set; }	

	}
}

	

