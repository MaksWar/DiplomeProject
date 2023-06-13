using System.Collections.Generic;
using Infrastructure.Services.PersistentProgress;

namespace Infrastructure.Factory
{
	public class FactoryProvider : IFactoryProvider
	{
		private List<ISavedProgressReader> _savedProgressReaders;
		private List<ISavedProgress> _savedProgresses;

		public FactoryProvider()
		{
			_savedProgressReaders = new List<ISavedProgressReader>();
			_savedProgresses = new List<ISavedProgress>();
		}

		public IReadOnlyList<ISavedProgressReader> ProgressReaders => _savedProgressReaders;

		public IReadOnlyList<ISavedProgress> ProgressWriters => _savedProgresses;

		public void Register(ISavedProgressReader savedProgressReader)
		{
			_savedProgressReaders.Add(savedProgressReader);
			if(savedProgressReader is ISavedProgress progressWriters)
				_savedProgresses.Add(progressWriters);
		}

		public void Cleanup()
		{
			_savedProgressReaders?.Clear();
			_savedProgresses?.Clear();
		}
	}
}
