using System.Collections.Generic;
using System.Linq;
using Additions.Extensions;

namespace Game.Minigames.ChooseNotCorrectPicture
{
	public class ChooserNotCorrectController : IChooserNotCorrectController
	{
		private readonly PictureConditionCollection _conditionCollection;

		private ILevelAnalyticData _levelAnalyticData;
		private List<Condition> _conditions;
		private PictureType _isNotCorrectType;

		private int _currentScoreReward;

		public ILevelAnalyticData LevelAnalyticData => _levelAnalyticData;
		public PictureType IsNotCorrectType => _isNotCorrectType;

		public int RewardForCurrentConditions => _currentScoreReward;

		public List<Condition> Conditions => _conditions;

		public ChooserNotCorrectController(PictureConditionCollection pictureConditionCollection) =>
			_conditionCollection = pictureConditionCollection;

		public void InitConditions()
		{
			List<PictureCondition> conditions = _conditionCollection.PictureConditions;

			PictureCondition pictureCondition = conditions.GetRandomElement();
			_conditions = pictureCondition.Conditions;
			_currentScoreReward = pictureCondition.ScoreReward;

			_levelAnalyticData = pictureCondition;

			_isNotCorrectType = _conditions.First(x => x.IsNotCorrect).PictureType;
		}

		public bool CheckIsRightAnswer(PictureType pictureType) =>
			_isNotCorrectType == pictureType;
	}
}