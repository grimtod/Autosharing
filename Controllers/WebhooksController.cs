using Autosharing.Database;
using Autosharing.Dto;
using Autosharing.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Net.Http;

namespace Autosharing.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class WebhooksController : ControllerBase
	{
		private IConfiguration _configuration;

		public WebhooksController(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		[HttpGet("created")]
		public void EntityCreated([FromQuery]Guid entityId)
		{
			var entityDto = GetEntityFromService(entityId);
			var @entity = ConvertToModel(entityDto);
			var db = new AutosharingDbContext();
			db.Entities.Add(@entity);
			db.SaveChanges();
		}

		[HttpGet("changed")]
		public void EntityChanged([FromQuery]Guid entityId)
		{
			var entityDto = GetEntityFromService(entityId);
			var @entity = ConvertToModel(entityDto);
			var db = new AutosharingDbContext();
			@entity = db.Entities.Attach(@entity);
			db.Entry(@entity).State = EntityState.Modified;
			db.SaveChanges();
		}

		[HttpGet("deleted")]
		public void EntityDeleted([FromQuery]Guid entityId)
		{
			var db = new AutosharingDbContext();
			var @entity = db.Entities.Find(entityId);
			db.Entities.Remove(@entity);
			db.SaveChanges();
		}

		private EntityDto GetEntityFromService(Guid entityId)
		{
			var serviceUrl = _configuration.GetSection("Autosharing").GetValue<string>("EntitiesServiceUrl");

			using (var client = new HttpClient())
			{
				var response = client.GetAsync($"{serviceUrl}/api/entities/{entityId}").Result;
				var json = response.Content.ReadAsStringAsync().Result;
				var entityDto = JsonConvert.DeserializeObject<EntityDto>(json);

				return entityDto;
			}
		}

		private Entity ConvertToModel(EntityDto entityDto)
		{
			var result = new Entity
			{
				Id = entityDto.Id,
				Name = entityDto.Name,
				Properties = new List<EntityProperty>()
			};

			foreach (var propertyDto in entityDto.Properties)
			{
				var property = new EntityProperty
				{
					Name = propertyDto.Key,
					Type = propertyDto.Value.Type,
					Value = propertyDto.Value.Value
				};

				if (property.Type == PropertyType.DateTime)
				{
					property.NumberValue = DateTime.Parse(property.Value).Ticks;
				}

				if (property.Type == PropertyType.Number)
				{
					property.NumberValue = double.Parse(property.Value);
				}

				result.Properties.Add(property);
			}

			return result;
		}
	}
}
