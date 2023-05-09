using Infrastructure.AssetManagement;
using Infrastructure.Factory;
using Infrastructure.Services.PersistentProgress;
using Infrastructure.Services.SaveLoad;
using Infrastructure.Services.StaticDataService;
using Infrastructure.States;
using Zenject;

namespace Infrastructure.CompositionRoot
{
	public class GameInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			BindAssetProvider();

			BindFactories();

			BindCoroutineRunner();

			BindSceneLoader();

			BindLoadingCoroutine();

			BindGameStateMachine();

			BindPlayerProgressService();

			BindStaticDataService();

			BindSaveLoadService();
		}

		private void BindAssetProvider()
		{
			Container
				.BindInterfacesAndSelfTo<AssetProvider>()
				.AsSingle();
		}

		private void BindFactories()
		{
			Container
				.Bind<IFactoryProvider>()
				.To<FactoryProvider>()
				.AsSingle();

			Container
				.Bind<IUIFactory>()
				.To<UIFactory>()
				.AsSingle();
		}

		private void BindCoroutineRunner()
		{
			Container
				.Bind<ICoroutineRunner>()
				.To<CoroutineRunner>()
				.FromComponentInNewPrefabResource(InfrastructureAssetPath.CoroutineRunner)
				.AsSingle();
		}

		private void BindSceneLoader()
		{
			Container
				.Bind<SceneLoader>()
				.AsSingle();
		}

		private void BindLoadingCoroutine()
		{
			Container
				.Bind<LoadingCurtain>()
				.FromComponentInNewPrefabResource(InfrastructureAssetPath.LoadingCurtain)
				.AsSingle();
		}

		private void BindGameStateMachine()
		{
			Container
				.Bind<IGameStateMachine>()
				.FromSubContainerResolve()
				.ByInstaller<GameStateMachineInstaller>()
				.AsSingle();
		}

		private void BindPlayerProgressService()
		{
			Container
				.BindInterfacesAndSelfTo<PersistentProgressService>()
				.AsSingle();
		}

		private void BindStaticDataService()
		{
			Container
				.BindInterfacesAndSelfTo<StaticDataService>()
				.AsSingle();
		}

		private void BindSaveLoadService()
		{
			Container
				.BindInterfacesAndSelfTo<SaveLoadService>()
				.AsSingle();
		}
	}
}