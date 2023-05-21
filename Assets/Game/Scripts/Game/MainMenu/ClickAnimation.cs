using Additions.Extensions;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace MainMenu
{
	public class ClickAnimation : MonoBehaviour
	{
		[SerializeField] private RectTransform rectTransform;
		[SerializeField] private Button button;

		private Sequence _clickTween;
		private Vector3 _startScale;

		private void Awake()
		{
			button.onClick.AddListener(OnClick);
			_startScale = rectTransform.localScale;
		}

		private void OnClick()
		{
			_clickTween.KillIfValid();
			_clickTween = DOTween.Sequence();
			_clickTween.Append(rectTransform
				.DOScale(_startScale * 1.1f, 0.25f)
				.SetEase(Ease.InBack));
			_clickTween.Append(rectTransform
				.DOScale(_startScale, 0.25f)
				.SetEase(Ease.OutBack));
		}

		#region Editor

		private void OnValidate()
		{
			rectTransform ??= GetComponent<RectTransform>();
			button ??= GetComponent<Button>();
		}

		#endregion
	}
}
