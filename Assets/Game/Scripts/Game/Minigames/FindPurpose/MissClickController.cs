using System;
using UnityEngine;

namespace Game.Minigames.FindPurpose
{
	public class MissClickController : MonoBehaviour
	{
		public event Action OnMissClick;

		private void OnMouseDown() =>
			OnMissClick?.Invoke();
	}
}