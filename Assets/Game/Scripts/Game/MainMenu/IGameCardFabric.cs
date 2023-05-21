using System.Collections.Generic;
using UnityEngine;

namespace MainMenu
{
	public interface IGameCardFabric
	{
		List<GameCardView> CreateGameCardView(Category category, Transform parent, Vector3 scale);
	}
}