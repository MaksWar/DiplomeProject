using System.Collections.Generic;
using Infrastructure.Services.DataAnalitycsService;
using MainMenu;

namespace Game.Minigames
{
	public class MinigameAnalyticDataSender : IMinigameAnalyticDataSender
	{
		private readonly IAnalyticsService _analyticsService;

		public MinigameAnalyticDataSender(IAnalyticsService analyticsService) =>
			_analyticsService = analyticsService;

		public string GetRecommendation(List<MinigameAnalyticData> levelsAnalyticData)
		{
			GradationOfMark gradationOfMark = _analyticsService.AnaliseData(levelsAnalyticData);

			string recommendation = AnalyticsHelper.GetRecommendation(gradationOfMark, Category.Logic);

			return recommendation;
		}

		public GradationOfMark GetMark(List<MinigameAnalyticData> levelsAnalyticData) =>
			_analyticsService.AnaliseData(levelsAnalyticData);
	}
}