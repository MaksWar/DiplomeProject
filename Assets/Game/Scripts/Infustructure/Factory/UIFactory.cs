using Infrastructure.AssetManagement;
using Infrastructure.AssetsManagement;
using UnityEngine;
using Zenject;

namespace Infrastructure.Factory
{
	public class UIFactory : FactoryBase, IUIFactory
	{
		private Camera _mainCamera;
		private Camera _uiCamera;

		public Camera MainCamera => _mainCamera;
		public Camera UICamera => _uiCamera;

		public UIFactory(IAssetProvider assetProvider, IFactoryProvider factoryProvider, DiContainer diContainer)
			: base(assetProvider, factoryProvider, diContainer)
		{
		}

		public GameObject CreateMainCamera()
		{
			GameObject prefab = AssetProvider.Instantiate(FactoryAssetPath.MAIN_CAMERA);
			_mainCamera = prefab.GetComponent<Camera>();

			return prefab;
		}

		public GameObject CreateUICamera()
		{
			GameObject prefab = AssetProvider.Instantiate(FactoryAssetPath.UI_Camera);
			_uiCamera = prefab.GetComponent<Camera>();

			return prefab;
		}

		public GameObject CreateHUD() =>
			AssetProvider.Instantiate(FactoryAssetPath.HUD);

		public GameObject CreateResultPanel() =>
			AssetProvider.Instantiate(FactoryAssetPath.ResultPanel);
	}
}