using System;
using Additions.Extensions;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MainMenu
{
	public class Fader : MonoBehaviour, IPointerClickHandler
	{
		[SerializeField] private Image fadeImage;

		public event Action OnClick;

		private Tween _tween;

		public void Fade(float duration = 0.5f, Action onComplete = null)
		{
			fadeImage.enabled = true;

			_tween.KillIfValid();
			_tween = fadeImage
				.DOFade(0.5f, duration)
				.OnComplete(() => onComplete?.Invoke());
		}

		public void UnFade(float duration = 0.5f, Action onComplete = null)
		{
			_tween.KillIfValid();
			_tween = fadeImage
				.DOFade(0, duration)
				.OnComplete(() =>
				{
					fadeImage.enabled = false;
					onComplete?.Invoke();
				});
		}

		public void OnPointerClick(PointerEventData eventData) =>
			OnClick?.Invoke();

		#region Editor

		private void OnValidate() =>
			fadeImage ??= GetComponent<Image>();

		#endregion
	}
}