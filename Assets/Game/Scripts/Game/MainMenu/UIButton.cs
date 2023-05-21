using System;
using Additions.UI.RetractableElements;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace MainMenu
{
	public class UIButton : RetractableUiElement
	{
		[SerializeField] private Button button;

		public Button Butt => button;

		public override void Show(float time = 0.4f)
		{
			rectTransform
				.DOScale(1f, time);

			base.Show(time);
		}

		public override void Hide(float time = 0.4f)
		{
			rectTransform
				.DOScale(0f, time);

			base.Hide(time);
		}

		#region Editor

		private void OnBecameVisible() =>
			button = Butt ?? GetComponent<Button>();

		#endregion
	}
}