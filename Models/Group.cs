using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Autosharing.Models
{
	[Table("Groups")]
	public class Group
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid Id { get; set; }

		public string Name { get; set; }

		public Guid? ParentId { get; set; }
		public virtual Group Parent { get; set; }
	}
}
