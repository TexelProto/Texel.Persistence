using System;

namespace Texel.Persistence
{
	public interface IPersistenceFileProvider : IDisposable
	{
		IPersistenceFile GetFile(string name);
	}
}