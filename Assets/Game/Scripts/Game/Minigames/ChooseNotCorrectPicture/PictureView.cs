using System;
using Additions.Extensions;
using Additions.Pool;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.Minigames.ChooseNotCorrectPicture
{
	public class PictureView : MonoBehaviourPoolObject, IPointerClickHandler
	{
		[SerializeField] private PictureType pictureType;

		[Header("Components"), Space(15)]
		[SerializeField] private Image pictureImage;

		private Sequence _shakeAndHideSequence;
		private bool _isSelected;
		private bool _isLock;

		public PictureType Type => pictureType;

		public bool IsSelected => _isSelected;

		public event Action<PictureView> OnPictureSelected;

		public void Show(float duration = 0.5f, Action onComplete = null)
		{
			transform
				.DOScale(1f, duration)
				.OnComplete(() => onComplete?.Invoke())
				.SetEase(Ease.OutCubic);
		}

		public void Hide(float duration = 0.5f, Action onComplete = null)
		{
			transform
				.DOScale(0f, duration)
				.OnComplete(() => onComplete?.Invoke())
				.SetEase(Ease.InCubic);
		}

		public void Select()
		{
			if(_isLock)
				return;

			_isSelected = true;

			pictureImage.color = Color.green;
		}

		public void UnSelect()
		{
			if(_isLock)
				return;

			_isSelected = false;

			pictureImage.color = Color.white;
		}

		public void Lock() =>
			_isLock = true;

		public void ShakeAndHide(Action onComplete = null)
		{
			_shakeAndHideSequence.KillIfValid();

			_shakeAndHideSequence = DOTween.Sequence();
			_shakeAndHideSequence.Append(transform.DOShakeScale(0.5f, .5f));
			_shakeAndHideSequence.AppendCallback(() => Hide(onComplete: onComplete));
		}

		public override void Push() =>
			PicturesViewPool.Instance.Push(this);

		public void OnPointerClick(PointerEventData eventData) =>
			OnClick();

		private void OnClick() =>
			OnPictureSelected?.Invoke(this);
	}
}