using System.IO.Compression;

namespace Texel.Persistence
{
	/// <summary>
	/// Options for <see cref="ZipPersistenceFileProvider"/>
	/// </summary>
	public class ZipPersistenceOptions
	{
		public string ZipFilepath { get; set; } = "SaveData/savedata.zip";
		public CompressionLevel CompressionLevel { get; set; } = CompressionLevel.Optimal;
	}
}