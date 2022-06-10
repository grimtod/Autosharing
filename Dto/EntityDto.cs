using System;
using System.Collections.Generic;

namespace Autosharing.Dto
{
	public class EntityDto
	{
		public Guid Id { get; set; }

		public string Name { get; set; }

		public Dictionary<string, PropertyValueDto> Properties { get; set; }
	}
}
