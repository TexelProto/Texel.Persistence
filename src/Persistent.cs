using System;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Texel.Persistence
{
	/// <inheritdoc/>
	public class Persistent<T> : IPersistent<T>
	{
		private IPersistenceFile File { get; set; }
		private readonly IPersistenceSerializer<T> serializer;
		private readonly ILogger logger;

		public T Value { get; set; }

		[ActivatorUtilitiesConstructor]
		public Persistent(IPersistenceFile<T> file, IPersistenceSerializer<T> serializer,
		                  IPersistenceRegistry registry, ILogger logger)
			: this( (IPersistenceFile)file, serializer, registry, logger )
		{}
		
		public Persistent(IPersistenceFile file, IPersistenceSerializer<T> serializer,
		                  IPersistenceRegistry registry, ILogger logger)
		{
			this.File = file;
			this.serializer = serializer;
			this.logger = logger;
			registry.RegisterPersistCallback( this.Serialize );

			using var readStream = file.OpenRead();
			try
			{
				if (readStream.CanSeek && readStream.Length == 0)
					return;
				this.Value = serializer.Deserialize( readStream );
			}
			catch (Exception e)
			{
				logger.Error( e, "Failed to deserialize persistent value" );
				this.Value = default;
			}
		}

		private void Serialize()
		{
			using var readStream = this.File.OpenWrite();
			try
			{
				this.serializer.Serialize( this.Value, readStream );
			}
			catch (Exception e)
			{
				this.logger.Error( e, "Failed to serialize persistent value" );
			}
		}
	}
}