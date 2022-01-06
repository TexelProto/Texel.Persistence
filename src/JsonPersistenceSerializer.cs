using System.IO;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Texel.Persistence
{
	/// <inheritdoc/>
	/// <summary>
	/// Implementation of <see cref="IPersistenceSerializer{T}"/> producing json output.
	/// </summary>
	public class JsonPersistenceSerializer<T> : IPersistenceSerializer<T>
	{
		private readonly JsonSerializer serializer;

		public JsonPersistenceSerializer(IOptions<JsonSerializerOptions> options)
		{
			this.serializer = JsonSerializer.CreateDefault( options.Value.SerializerSettings );
		}

		/// <inheritdoc/>
		public void Serialize(T data, Stream writeStream)
		{
			using var textStream = new StreamWriter( writeStream );
			using var jsonStream = new JsonTextWriter( textStream );

			this.serializer.Serialize( jsonStream, data, typeof(T) );
		}

		/// <inheritdoc/>
		public T Deserialize(Stream readStream)
		{
			using var textStream = new StreamReader( readStream );
			using var jsonStream = new JsonTextReader( textStream );

			return this.serializer.Deserialize<T>( jsonStream );
		}
	}
}