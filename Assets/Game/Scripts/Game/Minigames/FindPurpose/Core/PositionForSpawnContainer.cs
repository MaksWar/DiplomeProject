using System.Collections.Generic;
using System.Linq;
using Additions.Extensions;
using UnityEngine;

namespace Game.Minigames.FindPurpose
{
	public class PositionForSpawnContainer : MonoBehaviour
	{
		[SerializeField] private List<Transform> positions;

		private List<PositionPair> _positions;

		private void Awake()
		{
			_positions ??= new List<PositionPair>();

			foreach (Transform position in positions)
				_positions.Add(new PositionPair() {Position = position, IsEmpty = true});
		}

		public Transform GetRandomPosition()
		{
			List<PositionPair> emptyPositions = _positions.Where(x => x.IsEmpty).ToList();

			PositionPair randomPosPair = emptyPositions.GetRandomElement();
			randomPosPair.IsEmpty = false;

			return randomPosPair.Position;
		}

		public void ResetInfo() =>
			_positions.ForEach(x => x.IsEmpty = true);

		private class PositionPair
		{
			public Transform Position;
			public bool IsEmpty;
		}
	}
}