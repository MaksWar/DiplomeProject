using Infrastructure.Data;
using Infrastructure.Factory;
using Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace Infrastructure.Services.SaveLoad
{
	public class SaveLoadService : ISaveLoadService
	{
		private const string progressKey = "Progress";

		private readonly IPersistentProgressService _progressService;
		private readonly IFactoryProvider _factoryProvider;

		public SaveLoadService(IPersistentProgressService progressService, IFactoryProvider factoryProvider)
		{
			_factoryProvider = factoryProvider;
			_progressService = progressService;
		}

		public void SaveProgress()
		{
			foreach (ISavedProgress progressWriter in _factoryProvider.ProgressWriters)
				progressWriter.UpdateProgress(_progressService.Progress);

			SaveLocal();
		}

		public void SaveLocal() =>
			PlayerPrefs.SetString(progressKey, _progressService.Progress.ToJson());

		public PlayerProgress LoadProgress() =>
			PlayerPrefs.GetString(progressKey)?.ToDeserialized<PlayerProgress>();
	}
}