using Autosharing.Database;
using Autosharing.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Autosharing.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class MembershipsController : ControllerBase
	{
		[HttpPost]
		public Membership AddMebership(Membership membership)
		{
			var db = new AutosharingDbContext();
			db.Memberships.Add(membership);
			db.SaveChanges();

			return membership;
		}

		[HttpDelete]
		public void DeleteMembership([FromQuery]Guid groupId, [FromQuery]Guid userId)
		{
			var db = new AutosharingDbContext();
			var membership = db.Memberships.First(m => m.UserId == userId && m.GroupId == groupId);
			db.Memberships.Remove(membership);
			db.SaveChanges();
		}
	}
}
