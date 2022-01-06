using System.IO;

namespace Texel.Persistence
{
	public class PhysicalPersistenceFileProvider : IPersistenceFileProvider
	{
		private readonly string rootDirectory;

		public PhysicalPersistenceFileProvider(string rootDirectory)
		{
			this.rootDirectory = rootDirectory;

			if (Directory.Exists( this.rootDirectory ) == false)
				Directory.CreateDirectory( this.rootDirectory );
		}

		public IPersistenceFile GetFile(string name)
		{
			var filepath = Path.Combine( this.rootDirectory, name + ".save" );
			return new PhysicalPersistenceFile( filepath );
		}

		public void Dispose() { }
	}
}