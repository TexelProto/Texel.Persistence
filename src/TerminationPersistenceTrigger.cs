using System;
using Texel.Lifetimes;

namespace Texel.Persistence
{
	/// <inheritdoc/>
	public class TerminationPersistenceTrigger : IPersistenceTrigger
	{
		private Action triggerCallback;

		public TerminationPersistenceTrigger(ILifetimeObserver lifetimeObserver)
		{
			lifetimeObserver.OnTerminate( this.Invoke );
		}

		private void Invoke()
		{
			this.triggerCallback?.Invoke();
		}

		/// <inheritdoc/>
		public void OnTrigger(Action callback)
		{
			this.triggerCallback = callback;
		}
	}
}