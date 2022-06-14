using Autosharing.Database;
using Autosharing.Dto;
using Autosharing.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Autosharing.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class PermissionsController : ControllerBase
	{
		[HttpGet]
		public IEnumerable<PermissionDto> GetPermissions([FromQuery]Guid userId, [FromQuery]Guid? entityId)
		{
			var db = new AutosharingDbContext();
			var groupIds = new List<Guid>();
			var permissions = new List<PermissionDto>();

			var groups = db.Memberships
				.Include(m => m.Group)
				.Include(m => m.Group.Parent)
				.Where(m => m.UserId == userId)
				.Select(m => m.Group)
				.ToArray();

			foreach (var group in groups)
			{
				FillGroupIdsRecursive(groupIds, group);
			}

			var sharingRules = db.SharingRules
				.Include(sr => sr.Filters)
				.Where(sr => groupIds.Contains(sr.GroupId))
				.ToArray();

			foreach (var sharingRule in sharingRules)
			{
				var entities = GetEntitiesByRule(sharingRule);

				if (entityId.HasValue)
				{
					entities = entities.Where(o => o.Id == entityId.Value);
				}

				permissions.AddRange(entities.Select(o => new PermissionDto
				{
					EntityId = o.Id,
					UserId = userId,
					ActionType = sharingRule.ActionType
				}));
			}

			return permissions;
		}

		private void FillGroupIdsRecursive(List<Guid> groupIds, Group group)
		{
			groupIds.Add(group.Id);

			if (group.ParentId != null)
			{
				FillGroupIdsRecursive(groupIds, group.Parent);
			}
		}

		private IEnumerable<Entity> GetEntitiesByRule(SharingRule sharingRule)
		{
			var db = new AutosharingDbContext();
			IEnumerable<Entity> query = db.Entities.Include(o => o.Properties);

			foreach (var filter in sharingRule.Filters)
			{
				if (filter.PropertyType == PropertyType.String || filter.PropertyType == PropertyType.Bool)
				{
					query = query.Where(o => o.Properties.Any(p => p.Name == filter.PropertyName && p.Value == filter.Value));
				}
				else
				{
					query = query.Where(o => o.Properties.Any(p => p.Name == filter.PropertyName && p.NumberValue >= filter.BeginValue && p.NumberValue <= filter.EndValue));
				}
			}

			return query;
		}
	}
}
