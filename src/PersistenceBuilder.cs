using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Texel.Lifetimes;

namespace Texel.Persistence
{
	public class PersistenceBuilder
	{
		public class FileBuilder
		{
			public PersistenceBuilder Builder { get; }
			public IServiceCollection Services => this.Builder.Services;

			internal FileBuilder(PersistenceBuilder persistenceBuilder)
			{
				this.Builder = persistenceBuilder;
			}

			/// <summary>
			/// Registers the services to store the persistence in physical files.  
			/// </summary>
			public PersistenceBuilder Physical()
				=> this.Physical( (Action<PhysicalPersistenceOptions>)null );

			/// <inheritdoc cref="Physical()"/>
			/// <param name="directory">
			/// Specifies in which directory the files should be stored.
			/// If it does not exist it will be created.
			/// </param>
			public PersistenceBuilder Physical(string directory)
				=> this.Physical( op => op.SavedataDirectory = directory );

			/// <inheritdoc cref="Physical()"/>
			/// <param name="configure">Configuration delegate for the service options.</param>
			public PersistenceBuilder Physical(Action<PhysicalPersistenceOptions> configure)
			{
				this.Services.TryAddSingleton<IPersistenceFileProvider, PhysicalPersistenceFileProvider>();
				var optionsBuilder = this.Services.AddOptions<PhysicalPersistenceOptions>();
				if (configure != null)
					optionsBuilder.Configure( configure );
				return this.Builder;
			}

			/// <summary>
			/// Registers the services to store the persistence in a zip archive.  
			/// </summary>
			public PersistenceBuilder Zip()
				=> this.Zip( (Action<ZipPersistenceOptions>)null );

			/// <inheritdoc cref="Zip()"/>
			/// <param name="filepath">
			/// Specifies the path to the archive in which the files should be stored.
			/// If part of the path does not exist it will be created.
			/// </param>
			public PersistenceBuilder Zip(string filepath)
				=> this.Zip( op => op.ZipFilepath = filepath );

			/// <inheritdoc cref="Zip()"/>
			/// <param name="configure">Configuration delegate for the service options.</param>
			public PersistenceBuilder Zip(Action<ZipPersistenceOptions> configure)
			{
				this.Services.TryAddSingleton<IPersistenceFileProvider, ZipPersistenceFileProvider>();
				var optionsBuilder = this.Services.AddOptions<ZipPersistenceOptions>();
				if (configure != null)
					optionsBuilder.Configure( configure );
				return this.Builder;
			}
		}

		public class SerializationBuilder
		{
			public PersistenceBuilder Builder { get; }
			public IServiceCollection Services => this.Builder.Services;

			internal SerializationBuilder(PersistenceBuilder persistenceBuilder)
			{
				this.Builder = persistenceBuilder;
			}

			/// <summary>
			/// Registers the services to serialize data in the JSON format.  
			/// </summary>
			public PersistenceBuilder Json()
				=> this.Json( null );

			/// <inheritdoc cref="Json()"/>
			/// <param name="configure">Configuration delegate for the service options.</param>
			public PersistenceBuilder Json(Action<JsonSerializerOptions> configure)
			{
				this.Services.TryAddSingleton( typeof(IPersistenceSerializer<>), typeof(JsonPersistenceSerializer<>) );
				var optionsBuilder = this.Services.AddOptions<JsonSerializerOptions>();
				if (configure != null)
					optionsBuilder.Configure( configure );
				return this.Builder;
			}

			/// <summary>
			/// Registers the services to serialize data in the BSON format.  
			/// </summary>
			public PersistenceBuilder Bson()
				=> this.Bson( null );

			/// <inheritdoc cref="Bson()"/>
			/// <param name="configure">Configuration delegate for the service options.</param>
			public PersistenceBuilder Bson(Action<BsonSerializerOptions> configure)
			{
				this.Services.TryAddSingleton( typeof(IPersistenceSerializer<>), typeof(BsonPersistenceSerializer<>) );
				var optionsBuilder = this.Services.AddOptions<BsonSerializerOptions>();
				if (configure != null)
					optionsBuilder.Configure( configure );
				return this.Builder;
			}
		}

		public class TriggerBuilder
		{
			public PersistenceBuilder Builder { get; }
			public IServiceCollection Services => this.Builder.Services;

			internal TriggerBuilder(PersistenceBuilder persistenceBuilder)
			{
				this.Builder = persistenceBuilder;
			}

			/// <summary>
			/// Registers the services to trigger serialization on termination of an <see cref="ILifetimeObserver"/>.
			/// </summary>
			public PersistenceBuilder OnTermination()
			{
				this.Services.TryAddEnumerable( ServiceDescriptor.Singleton<IPersistenceTrigger, TerminationPersistenceTrigger>() );
				return this.Builder;
			}

			/// <summary>
			/// Registers the services to trigger serialization periodically.   
			/// </summary>
			public PersistenceBuilder Periodic()
				=> this.Periodic( null );

			/// <inheritdoc cref="Periodic()"/>
			/// <param name="interval">The interval at which serialization should be triggered</param>
			public PersistenceBuilder Periodic(TimeSpan interval)
				=> this.Periodic( op => op.Interval = interval );

			/// <inheritdoc cref="Periodic()"/>
			/// <param name="configure">Configuration delegate for the service options.</param>
			public PersistenceBuilder Periodic(Action<PeriodicPersistenceOptions> configure)
			{
				this.Services.TryAddEnumerable(
					ServiceDescriptor.Singleton<IPersistenceTrigger, PeriodicPersistenceTrigger>()
					);

				var optionsBuilder = this.Services.AddOptions<PeriodicPersistenceOptions>();
				if (configure != null)
					optionsBuilder.Configure( configure );

				return this.Builder;
			}

			/// <summary>
			/// Registers the services to manually trigger serialization.   
			/// </summary>
			public PersistenceBuilder Manually()
			{
				this.Services.TryAddEnumerable(
					ServiceDescriptor.Singleton<IPersistenceTrigger, ManualPersistenceTrigger>()
					);

				return this.Builder;
			}
		}

		public IServiceCollection Services { get; }
		public FileBuilder File { get; }
		public SerializationBuilder Serialization { get; }
		public TriggerBuilder Trigger { get; }

		public PersistenceBuilder(IServiceCollection services)
		{
			this.Services = services;
			this.File = new FileBuilder( this );
			this.Serialization = new SerializationBuilder( this );
			this.Trigger = new TriggerBuilder( this );
		}
	}
}