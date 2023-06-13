using System.Collections.Generic;
using System.Linq;
using Additions.Extensions;
using UnityEngine;

namespace Game.Minigames.FindPurpose
{
	public class PointFabric : IPointFabric
	{
		private readonly List<PointView> _pointViews;

		public PointFabric(List<PointView> pointViews) =>
			_pointViews = pointViews;

		public PointView CreatePointView(PointType pointType, Transform parent, Vector3 startScale)
		{
			PointView prefab = _pointViews.Where(x => x.PointType == pointType).ToList().GetRandomElement();

			PointView pointView = PointViewPool.Instance.Pop(prefab);
			pointView.transform.parent = parent;
			pointView.transform.position = Vector3.zero;
			pointView.transform.localScale = startScale;

			return pointView;
		}
	}
}