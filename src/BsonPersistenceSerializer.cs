using System.IO;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;

namespace Texel.Persistence
{
	/// <inheritdoc/>
	/// <summary>
	/// Implementation of <see cref="IPersistenceSerializer{T}"/> producing bson output.
	/// </summary>
	public class BsonPersistenceSerializer<T> : IPersistenceSerializer<T>
	{
		private readonly JsonSerializer serializer;

		public BsonPersistenceSerializer(IOptions<BsonSerializerOptions> options)
		{
			this.serializer = JsonSerializer.CreateDefault(options.Value.SerializerSettings);
		}

		/// <inheritdoc/>
		public void Serialize(T data, Stream writeStream)
		{
			using var jsonStream = new BsonDataWriter( writeStream );

			this.serializer.Serialize( jsonStream, data, typeof(T) );
		}

		/// <inheritdoc/>
		public T Deserialize(Stream readStream)
		{
			using var jsonStream = new BsonDataReader( readStream );

			return this.serializer.Deserialize<T>( jsonStream );
		}
	}
}