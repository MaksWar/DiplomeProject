using UnityEngine;
using Zenject;

namespace Infrastructure.AssetManagement
{
	public class AssetProvider : IAssetProvider
	{
		public GameObject Instantiate(string path) =>
			Object.Instantiate(LoadPrefabFromResources(path));

		public GameObject Instantiate(string path, Vector3 at) =>
			Object.Instantiate(LoadPrefabFromResources(path), at, Quaternion.identity, null);

		public GameObject Instantiate(string path, Vector3 at, Quaternion quaternion) =>
			Object.Instantiate(LoadPrefabFromResources(path), at, quaternion, null);

		public GameObject Instantiate(string path, Vector3 at, Quaternion quaternion, Transform parent) =>
			Object.Instantiate(LoadPrefabFromResources(path), at, quaternion, parent);

		private GameObject LoadPrefabFromResources(string path) =>
			Resources.Load<GameObject>(path);
	}
}