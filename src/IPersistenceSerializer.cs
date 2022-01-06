using System.IO;

namespace Texel.Persistence
{
	/// <summary>
	/// Serializes/Deserializes data from streams. 
	/// </summary>
	/// <typeparam name="T">The type of the data to be serialized with this.</typeparam>
	public interface IPersistenceSerializer<T>
	{
		/// <summary>
		/// Serializes <paramref name="data"/> and stores the data in <paramref name="writeStream"/>.
		/// </summary>
		/// <param name="data">The object to be serialized.</param>
		/// <param name="writeStream">A stream to a persistent data store.</param>
		void Serialize(T data, Stream writeStream);
		/// <summary>
		/// Deserializes the data from <paramref name="readStream"/> into an instance of <typeparamref name="T"/>
		/// </summary>
		/// <param name="readStream">A stream to provide the raw data.</param>
		/// <returns></returns>
		T Deserialize(Stream readStream);
	}
}