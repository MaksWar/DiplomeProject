using System.Collections.Generic;
using Additions.Utils;
using Game.Minigames.ChooseNotCorrectPicture.Core;
using UnityEngine;
using Zenject;

namespace Game.Minigames.ChooseNotCorrectPicture
{
	public class ChooseNotCorrectPictureKernel : MonoInstaller, ICoroutineRunner
	{
		[SerializeField] private List<PictureView> pictureViews;

		public override void InstallBindings()
		{
			IPictureViewFabric pictureViewFabric = new PictureViewFabric(pictureViews);

			Container
				.Bind<ICoroutineRunner>()
				.FromInstance(this)
				.AsSingle();

			Container
				.Bind<IChooserNotCorrectController>()
				.To<ChooserNotCorrectController>()
				.AsSingle();

			Container
				.Bind<IMistakesCounter>()
				.To<MistakesCounter>()
				.AsSingle();

			Container
				.Bind<IPictureViewFabric>()
				.FromInstance(pictureViewFabric)
				.AsSingle();

			Container
				.Bind<ILevelsAnalyticDataHandler>()
				.To<LevelsAnalyticDataHandler>()
				.AsSingle();

			Container
				.Bind<IMinigameAnalyticDataSender>()
				.To<MinigameAnalyticDataSender>()
				.AsSingle();

			Container
				.Bind<ITimeCounter>()
				.To<TimeCounter>()
				.AsSingle();
		}
	}
}