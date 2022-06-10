using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Autosharing.Models
{
	[Table("Users")]
	public class User
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid Id { get; set; }

		public string Name { get; set; }
	}
}
