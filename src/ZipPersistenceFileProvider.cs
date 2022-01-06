using System;
using System.IO;
using System.IO.Compression;
using System.Threading;
using Microsoft.Extensions.Options;

namespace Texel.Persistence
{
	/// <inheritdoc/>
	public class ZipPersistenceFileProvider : IPersistenceFileProvider
	{
		private readonly ZipArchive writeArchive;
		private readonly ZipArchive readArchive;
		private readonly CompressionLevel compressionLevel;

		public ZipPersistenceFileProvider(IOptions<ZipPersistenceOptions> options)
		{
			if (options?.Value == null)
				throw new ArgumentNullException( nameof(options), "Neither options nor options.Value may be null" );

			var zipFilepath = options.Value.ZipFilepath;

			if (string.IsNullOrWhiteSpace( zipFilepath ))
				throw new ArgumentException( "The provided zip filepath must not be null or whitespace" );

			this.compressionLevel = options.Value.CompressionLevel;

			var directory = Path.GetDirectoryName( zipFilepath );
			Directory.CreateDirectory( directory );

			if (File.Exists( zipFilepath ))
			{
				//there exists an old archive, copy it to a temp file to not override it later
				var writeZipStream = File.Open( zipFilepath, FileMode.Open, FileAccess.ReadWrite );
				var readZipStream = File.Open( Path.GetTempFileName(), FileMode.Open, FileAccess.ReadWrite );
				writeZipStream.CopyTo( readZipStream );
				writeZipStream.Position = 0;
				readZipStream.Position = 0;

				this.writeArchive = new ZipArchive( writeZipStream, ZipArchiveMode.Update );
				this.readArchive = new ZipArchive( readZipStream, ZipArchiveMode.Update );
			}
			else
			{
				var writeZipStream = File.Create( zipFilepath );
				this.writeArchive = new ZipArchive( writeZipStream, ZipArchiveMode.Update );
				this.readArchive = null;
			}
		}

		public Stream OpenRead(string name)
		{
			var readEntry = this.readArchive?.GetEntry( name );
			if (readEntry != null)
				return readEntry.Open();
			return Stream.Null;
		}

		public Stream OpenWrite(string name)
		{
			var writeEntry = this.writeArchive.GetEntry( name )
			                 ?? this.writeArchive.CreateEntry( name, this.compressionLevel );
			return writeEntry.Open();
		}

		public IPersistenceFile GetFile(string name)
		{
			return new ZipPersistenceFile( this, name );
		}

		public void Dispose()
		{
			this.writeArchive?.Dispose();
			this.readArchive?.Dispose();
		}
	}
}