using Autosharing.Models;
using System;

namespace Autosharing.Dto
{
	public class PermissionDto
	{
		public Guid EntityId { get; set; }

		public Guid UserId { get; set; }

		public ActionType ActionType { get; set; }
	}
}
