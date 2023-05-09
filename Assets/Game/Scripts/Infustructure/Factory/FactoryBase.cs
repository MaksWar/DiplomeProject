using Infrastructure.AssetManagement;
using Infrastructure.Services.PersistentProgress;
using UnityEngine;
using Zenject;

namespace Infrastructure.Factory
{
	public abstract class FactoryBase
	{
		protected readonly IAssetProvider AssetProvider;
		protected readonly DiContainer DiContainer;
		protected readonly IFactoryProvider FactoryProvider;

		protected FactoryBase(IAssetProvider assetProvider, IFactoryProvider factoryProvider, DiContainer diContainer)
		{
			FactoryProvider = factoryProvider;
			AssetProvider = assetProvider;
			DiContainer = diContainer;
		}

		protected GameObject InstantiateRegisteredWithInjection(string pathPrefab, Vector3 pos, Transform parent)
		{
			GameObject obj = DiContainer.InstantiatePrefabResource(pathPrefab, pos, Quaternion.identity, parent);
			RegisterProgressWatchers(obj);

			return obj;
		}

		protected T InstantiateRegisteredWithInjection<T>(T prefab, Vector3 pos, Transform parent)
			where T : MonoBehaviour
		{
			var obj = DiContainer.InstantiatePrefab(prefab, pos, Quaternion.identity, parent);
			RegisterProgressWatchers(obj);

			return obj.GetComponent<T>();
		}

		protected GameObject InstantiateRegistered(string pathPrefab, Vector3 pos)
		{
			GameObject obj = AssetProvider.Instantiate(pathPrefab, pos);
			RegisterProgressWatchers(obj);

			return obj;
		}

		protected GameObject InstantiateRegistered(string pathPrefab)
		{
			GameObject obj = AssetProvider.Instantiate(pathPrefab);
			RegisterProgressWatchers(obj);

			return obj;
		}

		private void RegisterProgressWatchers(GameObject gameObject)
		{
			foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
				FactoryProvider.Register(progressReader);
		}
	}
}