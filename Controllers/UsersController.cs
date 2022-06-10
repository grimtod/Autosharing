using Autosharing.Database;
using Autosharing.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Autosharing.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class UsersController : ControllerBase
	{
		[HttpPost]
		public User AddUser(User user)
		{
			var db = new AutosharingDbContext();
			db.Users.Add(user);
			db.SaveChanges();

			return user;
		}

		[HttpDelete("{id}")]
		public void DeleteUser([FromRoute]Guid id)
		{
			var db = new AutosharingDbContext();
			var user = db.Users.Find(id);
			db.Users.Remove(user);
			db.SaveChanges();
		}
	}
}
