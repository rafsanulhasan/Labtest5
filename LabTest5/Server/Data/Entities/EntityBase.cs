using System;

namespace LabTest5.Server.Data.Entities
{
	public abstract class EntityBase<T>
		where T : IEquatable<T>
	{
		public T Id { get; init; }
	}

	public abstract class EntityBase
		: EntityBase<Guid>
	{
	}
}
