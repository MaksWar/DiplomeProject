using Infrastructure.Factory;
using Infrastructure.Services.PersistentProgress;
using Zenject;

namespace Infrastructure.States
{
	public class LoadLevelState : IPayLoadedState<string>
	{
		private readonly IPersistentProgressService _progressService;
		private readonly IFactoryProvider _factoryProvider;
		private readonly IUIFactory _uiFactory;
		private readonly GameStateMachine _stateMachine;
		private readonly LoadingCurtain _loadingCurtain;
		private readonly SceneLoader _sceneLoader;

		public LoadLevelState(
			GameStateMachine stateMachine,
			LoadingCurtain loadingCurtain,
			SceneLoader sceneLoader,
			IUIFactory uiFactory,
			IPersistentProgressService progressService,
			IFactoryProvider factoryProvider)
		{
			_loadingCurtain = loadingCurtain;
			_uiFactory = uiFactory;
			_sceneLoader = sceneLoader;
			_stateMachine = stateMachine;
			_progressService = progressService;
			_factoryProvider = factoryProvider;
		}

		public void Enter(string sceneName)
		{
			_loadingCurtain.Show();
			_sceneLoader.Load(sceneName, onLoaded: OnLoaded);
		}

		public void Exit() =>
			_loadingCurtain.Hide();

		private void OnLoaded()
		{
			//InitHud();
			//InitCameras();
			InformProgressReaders();

			_loadingCurtain.Hide();
		}

		private void InitCameras()
		{
			_uiFactory.CreateMainCamera();
			_uiFactory.CreateUICamera();
		}

		private void InitHud() =>
			_uiFactory.CreateHUD();

		private void InformProgressReaders()
		{
			foreach (ISavedProgressReader progressReader in _factoryProvider.ProgressReaders)
				progressReader.LoadProgress(_progressService.Progress);
		}

		public class Factory : PlaceholderFactory<IGameStateMachine, LoadLevelState>
		{
		}
	}
}