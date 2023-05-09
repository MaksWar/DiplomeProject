using Infrastructure.Data;
using Infrastructure.Services.PersistentProgress;
using Infrastructure.Services.SaveLoad;
using ModestTree;
using Zenject;

namespace Infrastructure.States
{
	public class LoadProgressState : IState
	{
		private readonly IPersistentProgressService _progressService;
		private readonly ISaveLoadService _savedLoadService;
		private readonly GameStateMachine _stateMachine;

		private const string TestScene = "TestScene";

		public LoadProgressState(GameStateMachine stateMachine, IPersistentProgressService progressService,
			ISaveLoadService savedLoadService)
		{
			_progressService = progressService;
			_savedLoadService = savedLoadService;
			_stateMachine = stateMachine;
		}

		public void Enter()
		{
			LoadProgressOrInitNew();
			_stateMachine.Enter<LoadLevelState, string>(_progressService.Progress.InitialScene);
		}

		public void Exit()
		{
		}

		private void LoadProgressOrInitNew() =>
			_progressService.Progress = _savedLoadService.LoadProgress() ?? NewProgress();

 		private PlayerProgress NewProgress()
		{
			var progress = new PlayerProgress(GetNextScene());

			return progress;
		}

		private string GetNextScene()
		{
			var debugScene = InitialFromAnyScene.DebugScene;
			string scene;
			if (debugScene != null)
			{
				scene = debugScene.IsEmpty()
					? Scenes.TestScene
					: debugScene;
			}
			else
			{
				scene = Scenes.TestScene;
			}

			return scene;
		}

		public class Factory : PlaceholderFactory<IGameStateMachine, LoadProgressState>
		{
		}
	}
}