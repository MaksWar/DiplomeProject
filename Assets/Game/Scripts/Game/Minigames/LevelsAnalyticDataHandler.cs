using System.Collections.Generic;
using Game.Minigames.ChooseNotCorrectPicture;
using Infrastructure.Services.DataAnalitycsService;

namespace Game.Minigames
{
	public class LevelsAnalyticDataHandler : ILevelsAnalyticDataHandler
	{
		private List<MinigameAnalyticData> _analyticData;

		public List<MinigameAnalyticData> LevelsAnalyticData => _analyticData;

		public void SaveAnalyticData(ILevelAnalyticData levelAnalyticData, float completeTime, int countOfMistake)
		{
			_analyticData ??= new List<MinigameAnalyticData>();

			_analyticData.Add(new MinigameAnalyticData()
			{
				LevelAnalyticData = levelAnalyticData,
				CompleteLevelTime = completeTime,
				CountOfMisstake = countOfMistake
			});
		}
	}
}