using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItransitionTask_5.Models
{
	public class UserGame : IdentityUser
	{
		public string Login { get; set; }
	}
}
