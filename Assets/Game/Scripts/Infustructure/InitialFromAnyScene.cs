using UnityEngine.SceneManagement;
using UnityEngine;

namespace Infrastructure
{
	/// <summary>
	/// Debug script for faster prototyping.
	/// Now you dont need to launch your game from Initial scene, just add this script to your scene
	/// </summary>
	public class InitialFromAnyScene : MonoBehaviour
	{
		public static string DebugScene;

#if UNITY_EDITOR
		private const string SCENE_NAME = "InitScene";

		private void Awake()
		{
			DebugScene = SceneManager.GetActiveScene().name;
			GameBootstrapper bootstrapper = FindObjectOfType<GameBootstrapper>();

			if (bootstrapper == null)
				SceneManager.LoadScene(SCENE_NAME);
		}
	}
#endif
}