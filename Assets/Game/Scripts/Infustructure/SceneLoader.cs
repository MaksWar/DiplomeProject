using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure
{
	public class SceneLoader
	{
		private readonly ICoroutineRunner _coroutineRunner;

		public SceneLoader(ICoroutineRunner coroutineRunner) =>
			_coroutineRunner = coroutineRunner;

		public void Load(string name, Action onLoaded = null) =>
			_coroutineRunner.StartCoroutine(LoadScene(name, onLoaded));

		public void ReloadCurrentScene(Action onLoaded = null) =>
			_coroutineRunner.StartCoroutine(ReloadScene(SceneManager.GetActiveScene().name, onLoaded));

		private static IEnumerator LoadScene(string sceneName, Action onLoaded = null)
		{
			if (SceneManager.GetActiveScene().name == sceneName)
			{
				onLoaded?.Invoke();
				yield break;
			}

			AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(sceneName);

			while (waitNextScene.isDone == false)
				yield return null;

			onLoaded?.Invoke();
		}

		private IEnumerator ReloadScene(string name, Action onLoaded = null)
		{
			AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(name);

			while (waitNextScene.isDone == false)
				yield return null;

			onLoaded?.Invoke();
		}
	}
}