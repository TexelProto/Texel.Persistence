using System;

namespace Texel.Persistence
{
	public interface IPersistenceTrigger
	{
		/// <summary>
		/// Provides a callback to trigger the serialization.
		/// </summary>
		/// <param name="callback">The callback that will trigger the serialization.</param>
		void OnTrigger(Action callback);
	}
}