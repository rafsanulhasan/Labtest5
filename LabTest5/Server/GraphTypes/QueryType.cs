using HotChocolate.Types;

using LabTest5.Server.Data.Entities;

using System;
using System.Collections.Generic;

namespace LabTest5.Server.GraphTypes
{
	public class QueryType
		: ObjectType
	{
		protected override void Configure(IObjectTypeDescriptor descriptor)
		{
			descriptor.Name("Query");
			_ = descriptor
				.Field("getPosts")
				.Resolve(ctx =>
				{
					var result = new List<PostEntity>();

					for (var i = 0; i < 50; i++)
					{
						var comments = new List<CommentEntity>();
						for (var j = 0; j < 50; j++)
						{
							var reactions = new List<ReactionEntity>();
							for (var k = 0; k < 50; k++)
							{
								reactions.Add(new ReactionEntity()
								{
									Reaction = (ReactionTypes)((k + 2) % 2),
									User = $"Reaction {k + 1}"
								});
							}
							comments.Add(
								new CommentEntity
								{
									Date = DateTime.Now,
									Number = j + 1,
									User = $"Commenter {j + 1}",
									Comment = $"Comment Description {j + 1}",
									Reactions = reactions
								}
							);

						}
						var post = new PostEntity()
						{
							User = $"Author {i + 1}",
							Comments = comments
						};
						result.Add(post);
					}

					return result;
				})
				.UseOffsetPaging<PostType>()
				.UseSorting<PostType>()
				.UseFiltering<PostType>();
		}
	}
}
