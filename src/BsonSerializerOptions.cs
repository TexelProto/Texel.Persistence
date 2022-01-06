using Newtonsoft.Json;

namespace Texel.Persistence
{
	/// <summary>
	/// Options for <see cref="BsonPersistenceSerializer{T}"/>
	/// </summary>
	public class BsonSerializerOptions
	{
		public JsonSerializerSettings SerializerSettings { get; set; } = new();
	}
}