using System.IO;

namespace Texel.Persistence
{
	/// <summary>
	/// Wrapper for <see cref="FileInfo"/> implementing <see cref="IPersistenceFile"/>
	/// </summary>
	public class PhysicalPersistenceFile : IPersistenceFile
	{
		private readonly FileInfo file;

		public PhysicalPersistenceFile(string filepath)
		{
			this.file = new FileInfo( filepath );
		}

		/// <inheritdoc/>
		public Stream OpenRead()
		{
			if (this.file.Exists)
				return this.file.OpenRead();
			return Stream.Null;
		}

		/// <inheritdoc/>
		public Stream OpenWrite()
		{
			if (this.file.Exists)
				return this.file.Open( FileMode.Truncate, FileAccess.Write );
			return this.file.Create();
		}

		public void Dispose() { }
	}
}