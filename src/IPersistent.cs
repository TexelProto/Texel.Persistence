namespace Texel.Persistence
{
	/// <summary>
	/// Container for a persistent object. 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IPersistent<T>
	{
		public T Value { get; set; }
	}
}