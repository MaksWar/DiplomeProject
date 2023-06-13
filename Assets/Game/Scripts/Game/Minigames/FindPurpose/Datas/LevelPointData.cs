using System;
using System.Collections.Generic;
using System.Linq;
using Additions.Extensions;
using Game.Minigames.ChooseNotCorrectPicture;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Minigames.FindPurpose
{
	[CreateAssetMenu(menuName = "Data/PointData", fileName = "PointData", order = 0)]
	public class LevelPointData : ScriptableObject, ILevelAnalyticData
	{
		[SerializeField] private List<PointType> pointTypes;
		[SerializeField] private float recommendationTime;
		[SerializeField] private int maxCountMistakes;
		[SerializeField] private float timeCoefficient;

		public List<PointType> PointTypes => pointTypes;
		public float RecommendationTime => recommendationTime;
		public int MaxCountMistakes => maxCountMistakes;
		public float TimeCoefficient => timeCoefficient;

#if UNITY_EDITOR
		[Button]
		private void GenerateTypes(int count)
		{
			pointTypes ??= new List<PointType>();
			pointTypes.Clear();

			List<PointType> types = Enum.GetValues(typeof(PointType)).Cast<PointType>().ToList();
			for (int i = 0; i < count; i++)
				pointTypes.Add(types.GetRandomElement());
		}
#endif
	}

	public enum PointType
	{
		Orange = 0,
		Red = 1,
		Blue = 2,
		Green = 3,
	}
}