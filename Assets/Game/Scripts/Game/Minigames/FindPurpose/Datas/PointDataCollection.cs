using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.Minigames.FindPurpose
{
	[CreateAssetMenu(menuName = "Data/PointDataCollection", fileName = "PointDataCollection", order = 0)]
	public class PointDataCollection : ScriptableObjectInstaller<PointDataCollection>
	{
		[SerializeField] private List<LevelPointData> pointData;

		public List<LevelPointData> PointsCollection => pointData;

		public override void InstallBindings()
		{
			Container
				.Bind<PointDataCollection>()
				.FromInstance(this);
		}
	}
}