using System.Collections.Generic;
using Additions.Extensions;
using Game.Minigames.ChooseNotCorrectPicture;

namespace Game.Minigames.FindPurpose
{
	public class ChooserPointTypes : IChooserPointTypes
	{
		private readonly PointDataCollection _pointDataCollection;

		private ILevelAnalyticData _levelAnalyticData;
		private List<PointType> _pointTypes;

		public ChooserPointTypes(PointDataCollection pointDataCollection) =>
			_pointDataCollection = pointDataCollection;

		public ILevelAnalyticData LevelAnalyticData => _levelAnalyticData;
		public List<PointType> PointTypes => _pointTypes;

		public void InitCorrectPoints()
		{
			LevelPointData randomLevelData = _pointDataCollection.PointsCollection.GetRandomElement();
			_pointTypes = randomLevelData.PointTypes;

			_levelAnalyticData = randomLevelData;
		}
	}
}