using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Autosharing.Models
{
	[Table("Entitys")]
	public class Entity
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid Id { get; set; }

		public string Name { get; set; }

		public virtual ICollection<EntityProperty> Properties { get; set; }
	}
}
