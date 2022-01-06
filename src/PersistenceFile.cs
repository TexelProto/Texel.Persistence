using System.IO;

namespace Texel.Persistence
{
	/// <inheritdoc/>
	public class PersistenceFile<T> : IPersistenceFile<T>
	{
		private readonly IPersistenceFile file;

		public PersistenceFile(IPersistenceFileProvider fileProvider)
		{
			this.file = fileProvider.GetFile( nameof(T) );
		}

		public Stream OpenRead() => this.file.OpenRead();
		public Stream OpenWrite() => this.file.OpenWrite();
		public void Dispose() => this.file.Dispose();
	}
}