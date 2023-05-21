using System;
using DG.Tweening;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


namespace Additions.UI.RetractableElements
{
	[RequireComponent(typeof(RectTransform))]
	public class RetractableUiElement : MonoBehaviour
	{
		[SerializeField] protected RectTransform rectTransform;
		[SerializeField] private Ease ease = Ease.Linear;
		[SerializeField] private Vector2 showPosition;
		[SerializeField] private Vector2 hiddenPosition;

		private bool _isShown;

		public bool IsShown => _isShown;

		public event Action OnPanelOpened;

		public event Action OnPanelClosed;

		public virtual void Show(float time = 0.4f)
		{
			_isShown = true;
			rectTransform.DOAnchorPos(showPosition, time)
				.SetEase(ease)
				.OnComplete(() => OnPanelOpened?.Invoke());
		}

		public virtual void Hide(float time = 0.4f)
		{
			_isShown = false;
			rectTransform.DOAnchorPos(hiddenPosition, time)
				.SetEase(ease)
				.OnComplete(() => OnPanelClosed?.Invoke());
		}

#if UNITY_EDITOR
		[ContextMenu("CopyShowPosition")]
		public void CopyShowPosition()
		{
			showPosition = rectTransform.anchoredPosition;

			EditorUtility.SetDirty(this);
		}

		[ContextMenu("CopyHiddenPosition")]
		public void CopyHiddenPosition()
		{
			hiddenPosition = rectTransform.anchoredPosition;

			EditorUtility.SetDirty(this);
		}
#endif

		protected virtual void OnValidate()
		{
			if (rectTransform == null)
				rectTransform = GetComponent<RectTransform>();
		}
	}
}