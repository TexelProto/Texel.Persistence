using System.IO;

namespace Texel.Persistence
{
	/// <inheritdoc/>
	public class ZipPersistenceFile : IPersistenceFile
	{
		private readonly ZipPersistenceFileProvider provider;
		private readonly string name;

		public ZipPersistenceFile(ZipPersistenceFileProvider provider, string name)
		{
			this.provider = provider;
			this.name = name;
		}

		public void Dispose() { }

		/// <inheritdoc/>
		public Stream OpenRead()
		{
			return this.provider.OpenRead( this.name );
		}

		/// <inheritdoc/>
		public Stream OpenWrite()
		{
			return this.provider.OpenWrite( this.name );
		}
	}
}