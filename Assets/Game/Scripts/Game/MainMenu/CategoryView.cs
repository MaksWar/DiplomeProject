using System;
using Additions.Extensions;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MainMenu
{
	public class CategoryView : MonoBehaviour, IPointerClickHandler
	{
		[SerializeField] private Category category;
		[SerializeField] private RectTransform rectTransform;

		private Tween _openHideTween;

		public Category CategoryType => category;

		public event Action<Category> OnClick;

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

		public void OnPointerClick(PointerEventData eventData) =>
			OnClick?.Invoke(category);

		#region Editor

		private void OnValidate() =>
			rectTransform ??= GetComponent<RectTransform>();

		#endregion
	}

	public enum Category
	{
		Logic = 0,
		Focus = 1,
		Math = 2,
	}
}