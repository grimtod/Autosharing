using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Autosharing.Models
{
	[Table("Memberships")]
	public class Membership
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid Id { get; set; }
		
		public Guid GroupId { get; set; }
		public virtual Group Group { get; set; }

		public Guid UserId { get; set; }
		public virtual User User { get; set; }
	}
}
