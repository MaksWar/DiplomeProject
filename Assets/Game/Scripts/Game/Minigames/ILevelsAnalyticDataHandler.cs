using System.Collections.Generic;
using Game.Minigames.ChooseNotCorrectPicture;
using Infrastructure.Services.DataAnalitycsService;

namespace Game.Minigames
{
	public interface ILevelsAnalyticDataHandler
	{
		List<MinigameAnalyticData> LevelsAnalyticData { get; }

		void SaveAnalyticData(ILevelAnalyticData levelAnalyticData, float completeTime, int countOfMistake);
	}
}