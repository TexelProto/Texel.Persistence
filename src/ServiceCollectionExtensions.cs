using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Texel.Persistence
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddPersistence(this IServiceCollection collection)
		{
			return AddPersistence( collection, b => b
			                                       .Trigger.Periodic()
			                                       .Serialization.Json()
			                                       .File.Physical() );
		}

		/// <summary>
		/// Registers core services for the persistence
		/// </summary>
		/// <param name="collection">The collection in which to register the services.</param>
		/// <param name="configure">Configuration delegate to specify further services.</param>
		/// <returns></returns>
		public static IServiceCollection AddPersistence(this IServiceCollection collection, Action<PersistenceBuilder> configure)
		{
			collection.TryAddSingleton( typeof(IPersistent<>), typeof(Persistent<>) );
			collection.TryAddSingleton( typeof(IPersistenceFile<>), typeof(PersistenceFile<>) );
			collection.TryAddSingleton( typeof(IPersistenceProvider<>), typeof(PersistenceProvider<>) );
			collection.TryAddSingleton<IPersistenceRegistry, PersistenceRegistry>();

			configure?.Invoke( new PersistenceBuilder( collection ) );
			return collection;
		}
	}
}