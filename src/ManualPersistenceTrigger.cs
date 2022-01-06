using System;

namespace Texel.Persistence
{
	public class ManualPersistenceTrigger : IPersistenceTrigger
	{
		private Action serializationCallback;

		public void TriggerSerialization()
		{
			if (this.serializationCallback == null)
				throw new InvalidOperationException( $"Attempted to call {nameof(this.TriggerSerialization)} before trigger callback was bound" );
			this.serializationCallback.Invoke();
		}

		void IPersistenceTrigger.OnTrigger(Action callback)
		{
			this.serializationCallback = callback;
		}
	}
}