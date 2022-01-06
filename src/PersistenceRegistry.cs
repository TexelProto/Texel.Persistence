using System;
using System.Collections.Generic;
using Serilog;
using Texel.Utilities;

namespace Texel.Persistence
{
	/// <inheritdoc/>
	public class PersistenceRegistry : IPersistenceRegistry
	{
		private readonly ILogger logger;
		private readonly AsyncActionDispatcher actionDispatcher = new();

		public PersistenceRegistry(ILogger logger, IEnumerable<IPersistenceTrigger> triggers)
		{
			this.logger = logger;
			foreach (var trigger in triggers)
				trigger.OnTrigger( this.InvokePersist );
		}

		private void InvokePersist()
		{
			this.logger.Information( "Serialize persistence" );
			this.actionDispatcher.Invoke();
		}

		/// <inheritdoc/>
		public void RegisterPersistCallback(Action callback)
		{
			this.actionDispatcher.Register( callback );
		}
	}
}