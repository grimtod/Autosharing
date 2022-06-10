using Autosharing.Database;
using Autosharing.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Autosharing.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class SharingRulesController : ControllerBase
	{
		[HttpPost]
		public SharingRule AddSharingRule(SharingRule sharingRule)
		{
			var db = new AutosharingDbContext();
			db.SharingRules.Add(sharingRule);
			db.SaveChanges();

			return sharingRule;
		}

		[HttpDelete("{id}")]
		public void DeleteSharingRule([FromRoute]Guid id)
		{
			var db = new AutosharingDbContext();
			var sharingRule = db.SharingRules.Find(id);
			db.SharingRules.Remove(sharingRule);
			db.SaveChanges();
		}
	}
}
