using Infrastructure.Services.StaticDataService;
using Zenject;

namespace Infrastructure.States
{
	public class BootstrapState : IState
	{
		private readonly IStaticDataService _staticDataService;
		private readonly GameStateMachine _gameStateMachine;
		private readonly SceneLoader _sceneLoader;

		public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, IStaticDataService staticDataService)
		{
			_staticDataService = staticDataService;
			_gameStateMachine = gameStateMachine;
			_sceneLoader = sceneLoader;
		}

		public void Enter()
		{
			InitServices();

			_sceneLoader.Load(Scenes.InitialScene, onLoaded: EnterLoadLevel);
		}

		public void Exit()
		{
		}

		private void EnterLoadLevel() =>
			_gameStateMachine.Enter<LoadProgressState>();

		private void InitServices() =>
			LoadStaticData();

		private void LoadStaticData() =>
			_staticDataService.Load();

		public class Factory : PlaceholderFactory<IGameStateMachine, BootstrapState>
		{
		}
	}
}