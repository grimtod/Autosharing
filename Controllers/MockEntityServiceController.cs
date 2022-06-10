using Autosharing.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Autosharing.Controllers
{
	[ApiController]
	[Route("api/entities")]
	public class MockEntityServiceController
	{
		[HttpGet("{id}")]
		public EntityDto GetEntity([FromRoute]Guid id)
		{
			return new EntityDto
			{
				Id = id,
				Name = Guid.NewGuid().ToString(),
				Properties = new Dictionary<string, PropertyValueDto>
				{
					["SomeStringProperty"] = new PropertyValueDto
					{
						Type = Models.PropertyType.String,
						Value = id.ToString()
					},
					["SomeNumberProperty"] = new PropertyValueDto
					{
						Type = Models.PropertyType.Number,
						Value = id.ToByteArray()[0].ToString()
					},
					["SomeBoolProperty"] = new PropertyValueDto
					{
						Type = Models.PropertyType.Bool,
						Value = true.ToString()
					},
					["SomeDateProperty"] = new PropertyValueDto
					{
						Type = Models.PropertyType.DateTime,
						Value = DateTime.Now.ToString()
					}
				}
			};
		}
	}
}
