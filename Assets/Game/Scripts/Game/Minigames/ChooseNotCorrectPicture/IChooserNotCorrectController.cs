using System.Collections.Generic;


namespace Game.Minigames.ChooseNotCorrectPicture
{
	public interface IChooserNotCorrectController
	{
		ILevelAnalyticData LevelAnalyticData { get; }

		PictureType IsNotCorrectType { get; }

		int RewardForCurrentConditions { get; }

		List<Condition> Conditions { get; }

		void InitConditions();

		bool CheckIsRightAnswer(PictureType pictureType);
	}
}