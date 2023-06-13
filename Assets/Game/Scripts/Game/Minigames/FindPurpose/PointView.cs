using System;
using Additions.Pool;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Minigames.FindPurpose
{
	public class PointView : MonoBehaviourPoolObject
	{
		[SerializeField] private PointType pointType;

		private bool _isSelected;

		public PointType PointType => pointType;

		public bool IsSelected => _isSelected;

		public event Action OnPointSelected;

		public void Show(float duration = 0.5f, Action onComplete = null)
		{
			_isSelected = false;

			transform
				.DOScale(1f, duration)
				.OnComplete(() => onComplete?.Invoke())
				.SetEase(Ease.OutCubic);
		}

		private void Hide(float duration = 0.5f, Action onComplete = null)
		{
			transform
				.DOScale(0f, duration)
				.OnComplete(() => onComplete?.Invoke())
				.SetEase(Ease.InCubic);
		}

		private void OnMouseDown()
		{
			if(_isSelected)
				return;

			Hide();

			_isSelected = true;

			OnPointSelected?.Invoke();
		}

		public override void Push() =>
			PointViewPool.Instance.Push(this);
	}
}