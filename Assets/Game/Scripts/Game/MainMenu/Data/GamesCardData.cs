using System.Collections.Generic;
using UnityEngine;

namespace MainMenu
{
	[CreateAssetMenu(menuName = "Data/GamesCardData", fileName = "GamesCardData", order = 0)]
	public class GamesCardData : ScriptableObject
	{
		[SerializeField] private Category category;
		[SerializeField] private List<GameCardView> gameCardsView;

		public List<GameCardView> CardsView => gameCardsView;

		public Category Category => category;
	}
}