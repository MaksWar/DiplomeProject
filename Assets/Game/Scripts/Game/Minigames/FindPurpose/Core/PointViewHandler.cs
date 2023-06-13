using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Game.Minigames.FindPurpose
{
	public class PointViewHandler : MonoBehaviour
	{
		[SerializeField] private Transform content;

		private IPointFabric _pointFabric;

		private List<PointView> _pointViews;

		public List<PointView> PointViews => _pointViews;

		[Inject]
		private void Construct(IPointFabric pointFabric) =>
			_pointFabric = pointFabric;

		public void SpawnView(List<PointType> pointTypes)
		{
			_pointViews = new List<PointView>();
			foreach (PointType type in pointTypes)
				_pointViews.Add(_pointFabric.CreatePointView(type, content, Vector3.zero));
		}
	}
}