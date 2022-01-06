namespace Texel.Persistence
{
	/// <summary>
	/// Provides a mechanism to create <see cref="IPersistent{T}"/> based on name.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IPersistenceProvider<T>
	{
		IPersistent<T> Create(string name);
	}
}