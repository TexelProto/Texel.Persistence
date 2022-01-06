using System;

namespace Texel.Persistence
{
	/// <summary>
	/// Options for <see cref="PeriodicPersistenceTrigger"/>
	/// </summary>
	public class PeriodicPersistenceOptions
	{
		/// <summary>
		/// The default location 
		/// </summary>
		public const string Location = "Persistence:Periodic";
		
		public TimeSpan Interval { get; set; }
	}
}