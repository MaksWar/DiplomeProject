using System;
using System.Collections.Generic;
using Game.Minigames.ChooseNotCorrectPicture;
using UnityEngine;

namespace Infrastructure.Services.DataAnalitycsService
{
	public class AnalyticsService : IAnalyticsService
	{
		private readonly Dictionary<int, GradationOfMark> gradationOfMarks = new Dictionary<int, GradationOfMark>()
		{
			[80] = GradationOfMark.Excellent,
			[60] = GradationOfMark.Good,
			[40] = GradationOfMark.Satisfactory,
			[20] = GradationOfMark.RequiresImprovement,
			[0] = GradationOfMark.Unsatisfactory,
		};

		public int LastMark { get; private set; }

		public GradationOfMark AnaliseData(List<MinigameAnalyticData> data)
		{
			GradationOfMark gradationOfMark = GradationOfMark.RequiresImprovement;
			int resultMark = 0;

			foreach (var analyticData in data)
				resultMark += CalculateGradationOfMark(analyticData);

			resultMark /= data.Count;
			LastMark = resultMark;

			foreach (var mark in gradationOfMarks)
			{
				if(mark.Key > resultMark)
					continue;

				gradationOfMark = mark.Value;
				break;
			}

			return gradationOfMark;
		}

		private int CalculateGradationOfMark(MinigameAnalyticData data)
		{
			//Оцінка = (Витрачений_час / Рекомендований_час) * (1 - (Кількість_помилок / Максимальна_кількість_помилок)) * Коефіцієнт_часу
			ILevelAnalyticData levelAnalyticData = data.LevelAnalyticData;
			float result = Mathf.Clamp01((levelAnalyticData.RecommendationTime / data.CompleteLevelTime) *
			                levelAnalyticData.TimeCoefficient) *
			               (1 - ((float)data.CountOfMisstake / levelAnalyticData.MaxCountMistakes)) * 100;

			return (int) result;
		}
	}

	[Serializable]
	public class MinigameAnalyticData
	{
		public int CountOfMisstake;
		public float CompleteLevelTime;
		public ILevelAnalyticData LevelAnalyticData;
	}

	public enum GradationOfMark
	{
		Excellent = 0,
		Good = 1,
		Satisfactory = 2,
		RequiresImprovement = 3,
		Unsatisfactory = 4,
	}
}