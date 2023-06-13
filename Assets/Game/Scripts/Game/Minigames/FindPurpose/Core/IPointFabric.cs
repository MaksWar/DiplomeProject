using UnityEngine;

namespace Game.Minigames.FindPurpose
{
	public interface IPointFabric
	{
		PointView CreatePointView(PointType pointType, Transform parent, Vector3 startScale);
	}
}