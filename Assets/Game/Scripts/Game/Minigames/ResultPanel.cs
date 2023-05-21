using System;
using DG.Tweening;
using Infrastructure.Services.DataAnalitycsService;
using MainMenu;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Minigames
{
	public class ResultPanel : MonoBehaviour
	{
		[SerializeField] private Fader fader;
		[SerializeField] private TextMeshProUGUI recommendationText;
		[SerializeField] private TextMeshProUGUI timeSpentText;
		[SerializeField] private TextMeshProUGUI mistakesText;
		[SerializeField] private TextMeshProUGUI markText;
		[SerializeField] private Button continueButton;
		[SerializeField] private Button retryButton;
		[SerializeField] private RectTransform rectTransform;

		public Button ContinueButton => continueButton;
		public Button RetryButton => retryButton;

		public void Show(float duration = 0.5f, Action onComplete = null)
		{
			fader.Fade(duration);
			rectTransform
				.DOScale(1f, duration)
				.SetEase(Ease.OutBack)
				.OnComplete(() => onComplete?.Invoke());
		}

		public void Hide(float duration = 0.5f, Action onComplete = null)
		{
			fader.UnFade(duration);
			rectTransform
				.DOScale(0f, duration)
				.SetEase(Ease.InBack)
				.OnComplete(() => onComplete?.Invoke());
		}

		public void SetStats(float timeSpent, float mistakesCount)
		{
			timeSpentText.text = $"Time Spent : {timeSpent:F1}";
			mistakesText.text = $"Mistakes : {mistakesCount}";
		}

		public void SetMark(GradationOfMark gradationOfMark) =>
			markText.text = $"Mark : {gradationOfMark.ToString()}";

		public void SetRecommendation(string recommendation) =>
			recommendationText.text = recommendation;

		#region Editor

		private void OnValidate() =>
			rectTransform ??= GetComponent<RectTransform>();

		#endregion
	}
}