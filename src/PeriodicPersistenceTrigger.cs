using System;
using System.Threading;
using Microsoft.Extensions.Options;

namespace Texel.Persistence
{
	public class PeriodicPersistenceTrigger : IPersistenceTrigger, IDisposable
	{
		private Action triggerCallback;
		private readonly Timer timer;

		public PeriodicPersistenceTrigger(IOptions<PeriodicPersistenceOptions> options)
		{
			var interval = options.Value.Interval;
			this.timer = new Timer( this.Invoke, null, interval, interval );
		}

		private void Invoke(object state)
		{
			this.triggerCallback?.Invoke();
		}

		public void OnTrigger(Action callback)
		{
			this.triggerCallback = callback;
		}

		public void Dispose()
		{
			this.timer?.Dispose();
		}
	}
}