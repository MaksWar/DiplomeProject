using System.Collections.Generic;
using Game.Minigames.ChooseNotCorrectPicture;

namespace Game.Minigames.FindPurpose
{
	public interface IChooserPointTypes
	{
		ILevelAnalyticData LevelAnalyticData { get; }

		List<PointType> PointTypes { get; }

		void InitCorrectPoints();
	}
}