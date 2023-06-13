using Additions.Missions;
using Additions.Utils;
using Game.Minigames.ChooseNotCorrectPicture.Core;
using Infrastructure;
using Infrastructure.Factory;
using Infrastructure.Services;
using Infrastructure.Services.DataAnalitycsService;
using MainMenu;
using UnityEngine;
using Zenject;

namespace Game.Minigames.FindPurpose.Missions
{
	public class EndMissionFindPurpose : MissionBase
	{
		private IMinigameDataAnalyticsSaver _minigameDataAnalyticsSaver;
		private ILevelsAnalyticDataHandler _levelsAnalyticDataHandler;
		private IMinigameAnalyticDataSender _analyticDataSender;
		private IAnalyticsService _analyticsService;
		private IMistakesCounter _mistakesCounter;
		private ITimeCounter _timeCounter;
		private IUIFactory _uiFactory;
		private ResultPanel _resultPanel;
		private SceneLoader _sceneLoader;

		[SerializeField] private Category MinigameCategory = Category.Focus;

		[Inject]
		private void Construct(
			IMinigameAnalyticDataSender analyticDataSender,
			ILevelsAnalyticDataHandler levelsAnalyticDataHandler,
			IMistakesCounter mistakesCounter,
			ITimeCounter timeCounter,
			IUIFactory uiFactory,
			IMinigameDataAnalyticsSaver minigameDataAnalyticsSaver,
			IAnalyticsService analyticsService,
			SceneLoader sceneLoader
		)
		{
			_minigameDataAnalyticsSaver = minigameDataAnalyticsSaver;
			_levelsAnalyticDataHandler = levelsAnalyticDataHandler;
			_analyticDataSender = analyticDataSender;
			_analyticsService = analyticsService;
			_mistakesCounter = mistakesCounter;
			_timeCounter = timeCounter;
			_sceneLoader = sceneLoader;
			_uiFactory = uiFactory;
		}

		protected override void Mission()
		{
			InitResultPanel();

			string recommendation = _analyticDataSender.GetRecommendation(_levelsAnalyticDataHandler.LevelsAnalyticData);
			GradationOfMark mark = _analyticDataSender.GetMark(_levelsAnalyticDataHandler.LevelsAnalyticData);

			_minigameDataAnalyticsSaver.Save(MinigameCategory, _timeCounter.Seconds, _mistakesCounter.CountOfTry, _analyticsService.LastMark);

			_resultPanel.SetStats(_timeCounter.Seconds, _mistakesCounter.CountOfTry);
			_resultPanel.SetMark(mark);
			_resultPanel.SetRecommendation(recommendation);
			_resultPanel.ContinueButton.onClick.AddListener(End);
			_resultPanel.RetryButton.onClick.AddListener(() => _sceneLoader.ReloadCurrentScene());

			_resultPanel.Show();
		}

		private void InitResultPanel()
		{
			_resultPanel = _uiFactory.CreateResultPanel().GetComponentInChildren<ResultPanel>();
			_resultPanel.Hide(0);
		}
	}
}