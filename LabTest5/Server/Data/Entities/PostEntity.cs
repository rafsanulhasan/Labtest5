using System.Collections.Generic;

namespace LabTest5.Server.Data.Entities
{
	public class PostEntity
		: EntityBase
	{
		public string User { get; set; }

		public ICollection<CommentEntity> Comments { get; set; }
	}
}
