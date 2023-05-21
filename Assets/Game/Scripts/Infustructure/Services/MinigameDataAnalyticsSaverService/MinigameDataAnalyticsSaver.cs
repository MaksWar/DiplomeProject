using System.Linq;
using Infrastructure.Data;
using Infrastructure.Services.DBSyncService;
using Infrastructure.Services.PersistentProgress;
using Infrastructure.Services.SaveLoad;
using MainMenu;
using UnityEngine;

namespace Infrastructure.Services
{
	public class MinigameDataAnalyticsSaver : IMinigameDataAnalyticsSaver, ISavedProgress
	{
		private IDatabaseSyncService _databaseSyncService;
		private ISaveLoadService _saveLoadService;

		private MinigameProgress _lastMinigameProgress;
		private string _uniqueID;
		private Category _category;
		private float _timeComplete;
		private int _mistakesCount;
		private int _mark;

		public void Construct(IPersistentProgressService progressService, ISaveLoadService saveLoadService, IDatabaseSyncService databaseSyncService)
		{
			_databaseSyncService = databaseSyncService;
			_saveLoadService = saveLoadService;

			_uniqueID = progressService.Progress.UniqueID;
		}

		public void Save(Category category, float timeComplete, int mistakesCount, int mark)
		{
			_category = category;
			_timeComplete = timeComplete;
			_mistakesCount = mistakesCount;
			_mark = mark;

			_saveLoadService.SaveProgress();
			_databaseSyncService.SendData(_lastMinigameProgress);
		}

		public void LoadProgress(PlayerProgress progress)
		{
		}

		public void UpdateProgress(PlayerProgress progress)
		{
			_lastMinigameProgress = progress.GetMinigameProgress(_category);
			MiniGameStatistic statistic = _lastMinigameProgress?.MiniGameStatistic;
			if (statistic == null)
			{
				Debug.LogError($"{GetType().Name}. MiniGameStatistic : NullReference");

				return;
			}

			statistic.LastMark = _mark;
			statistic.LastMistakesCount = _mistakesCount;
			statistic.LastTimeComplete = _timeComplete;

			if (statistic.CountPlays <= 0)
			{
				statistic.AvgMark = _mark;
				statistic.AvgTimeComplete = _timeComplete;
				statistic.AvgMistakesCount = _mistakesCount;
			}
			else
			{
				statistic.AvgMark = (statistic.AvgMark * statistic.CountPlays + _mark) /
				                    (statistic.CountPlays + 1);
				statistic.AvgMistakesCount = (statistic.AvgMistakesCount * statistic.CountPlays + _mistakesCount) /
				                             (statistic.CountPlays + 1);
				statistic.AvgTimeComplete = (statistic.AvgTimeComplete * statistic.CountPlays + _timeComplete) /
				                            (statistic.CountPlays + 1);
			}

			statistic.CountPlays++;
		}
	}
}