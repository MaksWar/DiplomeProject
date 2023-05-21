using Game.Minigames;
using UnityEngine;
using Zenject;

namespace MainMenu
{
	public class MainMenuInstaller : MonoInstaller
	{
		[SerializeField] private GamesCardDataCollection gamesCardDataCollection;

		public override void InstallBindings()
		{
			IGameCardFabric gameCardFabric = new GameCardFabric(gamesCardDataCollection, Container);

			Container
				.Bind<IGameCardFabric>()
				.FromInstance(gameCardFabric)
				.AsSingle();
		}
	}
}