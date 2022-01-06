using Newtonsoft.Json;

namespace Texel.Persistence
{
	/// <summary>
	/// Options for <see cref="JsonPersistenceSerializer{T}"/>
	/// </summary>
	public class JsonSerializerOptions
	{
		public JsonSerializerSettings SerializerSettings { get; set; } = new();
	}
}