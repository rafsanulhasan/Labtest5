namespace LabTest5.Server.Data.Entities
{
	public class ReactionEntity
		: EntityBase
	{
		public ReactionTypes Reaction { get; set; }
		public string User { get; set; }
	}
}