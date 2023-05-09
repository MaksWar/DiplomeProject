using System.Collections.Generic;
using Infrastructure.Services.PersistentProgress;

namespace Infrastructure.Factory
{
	public interface IFactoryProvider
	{
		IReadOnlyList<ISavedProgressReader> ProgressReaders { get; }
		IReadOnlyList<ISavedProgress> ProgressWriters { get; }

		void Register(ISavedProgressReader savedProgressReader);
		void Cleanup();
	}
}