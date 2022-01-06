using System;
using System.IO;

namespace Texel.Persistence
{
	/// <inheritdoc cref="IPersistenceFile"/>
	/// <summary>
	/// Allows for injection of a file unique for <typeparamref name="T"/>.
	/// </summary>
	/// <typeparam name="T">The type used for the file name</typeparam>
	public interface IPersistenceFile<T> : IPersistenceFile { }

	/// <summary>
	/// Abstraction of a file used for persistence
	/// </summary>
	public interface IPersistenceFile : IDisposable
	{
		/// <summary>
		/// Creates a read stream for this file.
		/// </summary>
		Stream OpenRead();
		/// <summary>
		/// Creates a write stream for this file.
		/// </summary>
		Stream OpenWrite();
	}
}