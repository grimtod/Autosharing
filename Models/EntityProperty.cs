using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Autosharing.Models
{
	[Table("EntityProperties")]
	public class EntityProperty
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid Id { get; set; }

		public string Name { get; set; }

		public PropertyType Type { get; set; }

		public string Value { get; set; }

		public double NumberValue { get; set; }

		[JsonIgnore]
		public Guid EntityId { get; set; }

		[JsonIgnore]
		public Entity Entity { get; set; }
	}
}
