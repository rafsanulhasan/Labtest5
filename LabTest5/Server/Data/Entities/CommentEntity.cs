using System;
using System.Collections.Generic;

namespace LabTest5.Server.Data.Entities
{
	public class CommentEntity
		: EntityBase
	{
		public int Number { get; set; }
		public string Comment { get; set; }
		public string User { get; set; }
		public DateTime Date { get; set; }

		public ICollection<ReactionEntity> Reactions { get; set; }
	}
}