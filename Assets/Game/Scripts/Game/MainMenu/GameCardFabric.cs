using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace MainMenu
{
	public class GameCardFabric : IGameCardFabric
	{
		private readonly GamesCardDataCollection _gamesCardDataCollection;
		private readonly DiContainer _diContainer;

		public GameCardFabric(GamesCardDataCollection gamesCardDataCollection, DiContainer diContainer)
		{
			_gamesCardDataCollection = gamesCardDataCollection;
			_diContainer = diContainer;
		}

		public List<GameCardView> CreateGameCardView(Category category, Transform parent, Vector3 scale)
		{
			List<GameCardView> cardViews = new List<GameCardView>();
			List<GameCardView> cardViewsPrefabs =
				_gamesCardDataCollection.CardData.First(x => x.Category == category).CardsView;

			foreach (var cardViewsPrefab in cardViewsPrefabs)
			{
				GameObject cardView = _diContainer.InstantiatePrefab(cardViewsPrefab, parent);
				cardView.transform.localPosition = Vector3.zero;
				cardView.transform.localScale = scale;

				cardViews.Add(cardView.GetComponent<GameCardView>());
			}

			return cardViews;
		}
	}
}