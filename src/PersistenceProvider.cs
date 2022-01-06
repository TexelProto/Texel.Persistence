using Serilog;

namespace Texel.Persistence
{
	/// <inheritdoc/>
	public class PersistenceProvider<T> : IPersistenceProvider<T>
	{
		private readonly IPersistenceFileProvider fileProvider;
		private readonly IPersistenceSerializer<T> serializer;
		private readonly IPersistenceRegistry registry;
		private readonly ILogger logger;

		public PersistenceProvider(IPersistenceFileProvider fileProvider, IPersistenceSerializer<T> serializer,
		                           IPersistenceRegistry registry, ILogger logger)
		{
			this.fileProvider = fileProvider;
			this.serializer = serializer;
			this.registry = registry;
			this.logger = logger;
		}

		/// <inheritdoc/>
		public IPersistent<T> Create(string name)
		{
			var file = this.fileProvider.GetFile( name );
			return new Persistent<T>( file, this.serializer, this.registry, this.logger );
		}
	}
}