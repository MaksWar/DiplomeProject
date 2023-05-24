using System;
using System.Collections.Generic;
using Infrastructure.Data;
using Infrastructure.Services.PersistentProgress;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace MainMenu.ResultsPanels
{
	public class ResultPanel : MonoBehaviour
	{
		[SerializeField] private Fader fader;
		[SerializeField] private UIButton closeButton;
		[SerializeField] private GameObject notItemsText;
		[Header("Elements For Spawn"), Space(15)]
		[SerializeField] private StatisticMinigameView statisticMinigameView;
		[SerializeField] private StatisticMinigameTitleView statisticMinigameTitleView;
		[SerializeField] private Transform content;

		private IPersistentProgressService _progressService;

		private List<IResultInfoObject> _resultInfoObjects = new List<IResultInfoObject>();

		[Inject]
		private void Construct(IPersistentProgressService progressService) =>
			_progressService = progressService;

		[Button]
		public void Show(float duration = 0.5f, Action onComplete = null)
		{
			closeButton.Show();
			closeButton.Butt.onClick.AddListener(() => FadeHide());

			fader.OnClick += FadeHide;
			fader.Fade();

			gameObject.SetActive(true);
			InitPanel();
		}

		[Button]
		public void Hide(float duration = 0.5f, Action onComplete = null, bool unFade = true, bool closeButtonHide = true)
		{
			closeButton.Butt.onClick.RemoveAllListeners();
			if(closeButtonHide)
				closeButton.Hide();

			fader.OnClick -= FadeHide;
			if(unFade)
				fader.UnFade();

			DeinitPanel();
		}

		private void InitPanel()
		{
			List<Category> categories = new List<Category>();
			foreach (MinigameProgress progress in _progressService.Progress.MinigamesProgresse)
			{
				if (progress.MiniGameStatistic.CountPlays > 0)
					categories.Add(progress.gameCategory);
			}

			foreach (Category category in categories)
			{
				SpawnTitle(category);
				SpawnStatisticInfo(category);
			}

			if(categories.Count <= 0)
				notItemsText.SetActive(true);
		}

		private void DeinitPanel()
		{
			_resultInfoObjects.ForEach(x => Destroy(x.GameObject));
			_resultInfoObjects.Clear();

			notItemsText.SetActive(false);
		}

		private void SpawnTitle(Category category)
		{
			StatisticMinigameTitleView titleView = Instantiate(statisticMinigameTitleView, content);
			titleView.SetText(category.ToString());

			_resultInfoObjects.Add(titleView);
		}

		private void SpawnStatisticInfo(Category category)
		{
			MinigameProgress progress = _progressService.Progress.GetMinigameProgress(category);

			StatisticMinigameView infoView = Instantiate(statisticMinigameView, content);
			infoView.SetInfo(progress.MiniGameStatistic);

			_resultInfoObjects.Add(infoView);
		}

		private void FadeHide() =>
			Hide();
	}
}