using System.Collections.Generic;
using Infrastructure.Services.DataAnalitycsService;

namespace Game.Minigames
{
	public interface IMinigameAnalyticDataSender
	{
		string GetRecommendation(List<MinigameAnalyticData> levelsAnalyticData);

		GradationOfMark GetMark(List<MinigameAnalyticData> levelsAnalyticData);
	}
}