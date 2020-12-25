using HotChocolate.Types;

using LabTest5.Server.Data.Entities;

namespace LabTest5.Server.GraphTypes
{
	public class PostType
		: ObjectType<PostEntity>
	{
		protected override void Configure(IObjectTypeDescriptor<PostEntity> descriptor)
		{
			base.Configure(descriptor);
			descriptor.Name("Post");
			descriptor
				.Field(p => p.Id)
				.ID();
			descriptor
				.Field(p => p.User);
			descriptor
				.Field(p => p.Comments);
		}
	}
}
