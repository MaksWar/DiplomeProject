using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Minigames.ChooseNotCorrectPicture
{
	[CreateAssetMenu(menuName = "Data/PictureData", fileName = "PictureData", order = 0)]
	public class PictureCondition : ScriptableObject, ILevelAnalyticData
	{
		[SerializeField] private int scoreReward;
		[SerializeField] private List<Condition> conditions;
		[Header("LevelAnalyticData"), Space(15)]
		[SerializeField] private float recommendationTime;
		[SerializeField] private float timeCoefficient;

		public List<Condition> Conditions => conditions;

		public int ScoreReward => scoreReward;

		public float RecommendationTime => recommendationTime;

		public int MaxCountMistakes => conditions.Count - 1;

		public float TimeCoefficient => timeCoefficient;
	}

	[Serializable]
	public class Condition
	{
		[SerializeField] private PictureType type;
		[SerializeField] private bool isNotCorrect;

		public PictureType PictureType => type;

		public bool IsNotCorrect => isNotCorrect;
	}
}