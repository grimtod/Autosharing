using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Autosharing.Models
{
	[Table("SharingFilters")]
	public class SharingFilter
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid Id { get; set; }

		public string PropertyName { get; set; }

		public PropertyType PropertyType { get; set; }

		public string Value { get; set; }

		public double? BeginValue { get; set; }

		public double? EndValue { get; set; }

		[JsonIgnore]
		public Guid SharingRuleId { get; set; }

		[JsonIgnore]
		public SharingRule SharingRule { get; set; }
	}
}
