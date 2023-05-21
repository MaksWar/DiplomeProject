using System;
using Additions.Extensions;
using DG.Tweening;
using Infrastructure;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace MainMenu
{
	public class GameCardView : MonoBehaviour, IPointerClickHandler
	{
		[SerializeField] private string sceneToChange;
		[SerializeField] private RectTransform rectTransform;

		private SceneLoader _sceneLoader;

		private Tween _openHideTween;

		[Inject]
		private void Construct(SceneLoader sceneLoader) =>
			_sceneLoader = sceneLoader;

		public void Show(float duration = 0.5f, Action onComplete = null)
		{
			_openHideTween.KillIfValid();
			_openHideTween = rectTransform
				.DOScale(1f, duration)
				.SetEase(Ease.OutBack)
				.OnComplete(() => onComplete?.Invoke());
		}

		public void Hide(float duration = 0.5f, Action onComplete = null)
		{
			_openHideTween.KillIfValid();
			_openHideTween = rectTransform
				.DOScale(0f, duration)
				.SetEase(Ease.InBack)
				.OnComplete(() => onComplete?.Invoke());
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			try
			{
				_sceneLoader.Load(sceneToChange);
			}
			catch (Exception e)
			{
				Debug.Log($"GameCardView : {name}, Error : {e.Message}");
				throw;
			}

		}

		#region Editor

		private void OnValidate() =>
			rectTransform ??= GetComponent<RectTransform>();

		#endregion
	}
}