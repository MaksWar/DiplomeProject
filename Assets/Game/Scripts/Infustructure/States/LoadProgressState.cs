using System;
using Infrastructure.Data;
using Infrastructure.Factory;
using Infrastructure.Services;
using Infrastructure.Services.DBSyncService;
using Infrastructure.Services.PersistentProgress;
using Infrastructure.Services.SaveLoad;
using ModestTree;
using Zenject;

namespace Infrastructure.States
{
	public class LoadProgressState : IState
	{
		private readonly IMinigameDataAnalyticsSaver _minigameDataAnalyticsSaver;
		private readonly IPersistentProgressService _progressService;
		private readonly IDatabaseSyncService _databaseSyncService;
		private readonly ISaveLoadService _savedLoadService;
		private readonly IFactoryProvider _factoryProvider;
		private readonly GameStateMachine _stateMachine;

		private const string TestScene = "TestScene";

		public LoadProgressState(GameStateMachine stateMachine, IPersistentProgressService progressService,
			ISaveLoadService savedLoadService, IMinigameDataAnalyticsSaver minigameDataAnalyticsSaver,
			IFactoryProvider factoryProvider, IDatabaseSyncService databaseSyncService)
		{
			_databaseSyncService = databaseSyncService;
			_factoryProvider = factoryProvider;
			_minigameDataAnalyticsSaver = minigameDataAnalyticsSaver;
			_progressService = progressService;
			_savedLoadService = savedLoadService;
			_stateMachine = stateMachine;
		}

		public void Enter()
		{
			LoadProgressOrInitNew();
			InitMinigameStatistic();

			_databaseSyncService.SyncData();
			_savedLoadService.SaveLocal();

			_stateMachine.Enter<LoadLevelState, string>(_progressService.Progress.InitialScene);
		}

		private void InitMinigameStatistic()
		{
			_minigameDataAnalyticsSaver.Construct(_progressService, _savedLoadService, _databaseSyncService);

			_factoryProvider.Register(_minigameDataAnalyticsSaver as ISavedProgress);
		}

		public void Exit()
		{
		}

		private void LoadProgressOrInitNew() =>
			_progressService.Progress = _savedLoadService.LoadProgress() ?? NewProgress();

 		private PlayerProgress NewProgress()
		{
			var progress = new PlayerProgress(GetNextScene(), Guid.NewGuid().ToString());

			_databaseSyncService.RegisterPlayer(progress.UniqueID);

			return progress;
		}

		private string GetNextScene()
		{
			var debugScene = InitialFromAnyScene.DebugScene;
			string scene;
			if (debugScene != null)
			{
				scene = debugScene.IsEmpty()
					? Scenes.MainMenu
					: debugScene;
			}
			else
			{
				scene = Scenes.MainMenu;
			}

			return scene;
		}

		public class Factory : PlaceholderFactory<IGameStateMachine, LoadProgressState>
		{
		}
	}
}