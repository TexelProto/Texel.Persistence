using System;

namespace Texel.Persistence
{
	/// <summary>
	/// Registry for serialization callbacks.
	/// </summary>
	public interface IPersistenceRegistry
	{
		/// <summary>
		/// Registers a method to be invoked whenever persistence will be triggered.
		/// </summary>
		/// <param name="callback"></param>
		void RegisterPersistCallback(Action callback);
	}
}