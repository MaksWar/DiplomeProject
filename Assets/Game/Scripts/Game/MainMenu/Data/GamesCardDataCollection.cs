using System.Collections.Generic;
using UnityEngine;

namespace MainMenu
{
	[CreateAssetMenu(menuName = "Data/GamesCardDataCollection", fileName = "GamesCardDataCollection", order = 0)]
	public class GamesCardDataCollection : ScriptableObject
	{
		[SerializeField] private List<GamesCardData> gamesCardData;

		public List<GamesCardData> CardData => gamesCardData;
	}
}