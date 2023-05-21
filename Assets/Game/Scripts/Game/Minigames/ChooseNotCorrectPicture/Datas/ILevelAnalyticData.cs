namespace Game.Minigames.ChooseNotCorrectPicture
{
	public interface ILevelAnalyticData
	{
		float RecommendationTime { get; }

		int MaxCountMistakes { get; }

		float TimeCoefficient { get; }
	}
}