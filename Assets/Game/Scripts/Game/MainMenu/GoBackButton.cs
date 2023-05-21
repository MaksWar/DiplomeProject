using Infrastructure;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace MainMenu
{
	public class GoBackButton : MonoBehaviour, IPointerClickHandler
	{
		[SerializeField] private string sceneToChange;

		private SceneLoader _sceneLoader;

		[Inject]
		private void Construct(SceneLoader sceneLoader) =>
			_sceneLoader = sceneLoader;

		public void OnPointerClick(PointerEventData eventData) =>
			_sceneLoader.Load(sceneToChange);
	}
}