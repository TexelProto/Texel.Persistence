using System.IO;
using Microsoft.Extensions.Options;

namespace Texel.Persistence
{
	public class PhysicalPersistenceFileProvider : IPersistenceFileProvider
	{
		private readonly string rootDirectory;

		public PhysicalPersistenceFileProvider(IOptions<PhysicalPersistenceOptions> options)
		{
			this.rootDirectory = options.Value.SavedataDirectory;

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