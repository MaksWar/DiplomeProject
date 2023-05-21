using System;
using System.Collections.Generic;
using System.Linq;
using MainMenu;

namespace Infrastructure.Data
{
	[Serializable]
	public class PlayerProgress
	{
		public string UniqueID;
		public List<MinigameProgress> MinigamesProgresse;
		public string InitialScene;

		public MinigameProgress GetMinigameProgress(Category category) =>
			MinigamesProgresse.FirstOrDefault(x => x.gameCategory == category);

		public PlayerProgress(string initialLevel, string uniqueID)
		{
			MinigamesProgresse = new List<MinigameProgress>();
			IEnumerable<Category> categories = Enum.GetValues(typeof(Category)).Cast<Category>();
			foreach (Category category in categories)
			{
				MinigamesProgresse.Add(new MinigameProgress()
				{
					gameCategory = category,
					MiniGameStatistic = new MiniGameStatistic()
				});
			}

			UniqueID = uniqueID;
			InitialScene = initialLevel;
		}
	}

	[Serializable]
	public class MinigameProgress
	{
		public Category gameCategory;
		public MiniGameStatistic MiniGameStatistic;
	}

	[Serializable]
	public class MiniGameStatistic
	{
		public int LastMistakesCount;
		public float LastTimeComplete;
		public int LastMark;

		public float AvgMistakesCount;
		public float AvgTimeComplete;
		public float AvgMark;
		public int CountPlays;
	}
}