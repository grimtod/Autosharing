using Autosharing.Database;
using Autosharing.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Autosharing.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class GroupsController : ControllerBase
	{
		[HttpPost]
		public Group AddGroup(Group group)
		{
			var db = new AutosharingDbContext();
			db.Groups.Add(group);
			db.SaveChanges();

			return group;
		}

		[HttpDelete("{id}")]
		public void DeleteGroup([FromRoute]Guid id)
		{
			var db = new AutosharingDbContext();
			var group = db.Groups.Find(id);
			db.Groups.Remove(group);
			db.SaveChanges();
		}
	}
}
