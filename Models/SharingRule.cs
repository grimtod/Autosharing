using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Autosharing.Models
{
	[Table("SharingRules")]
	public class SharingRule
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid Id { get; set; }

		public Guid GroupId { get; set; }
		public virtual Group Group { get; set; }

		public ActionType ActionType { get; set; }

		public virtual ICollection<SharingFilter> Filters { get; set; }
	}
}
