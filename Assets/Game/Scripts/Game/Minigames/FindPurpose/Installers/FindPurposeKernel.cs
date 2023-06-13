using System.Collections.Generic;
using Additions.Utils;
using Game.Minigames.ChooseNotCorrectPicture.Core;
using UnityEngine;
using Zenject;

namespace Game.Minigames.FindPurpose.Installers
{
	public class FindPurposeKernel : MonoInstaller, ICoroutineRunner
	{
		[SerializeField] private List<PointView> pointViews;

		public override void InstallBindings()
		{
			IPointFabric pointFabric = new PointFabric(pointViews);

			Container
				.Bind<ICoroutineRunner>()
				.FromInstance(this)
				.AsSingle();

			Container
				.Bind<IChooserPointTypes>()
				.To<ChooserPointTypes>()
				.AsSingle();

			Container
				.Bind<IMistakesCounter>()
				.To<MistakesCounter>()
				.AsSingle();

			Container
				.Bind<IPointFabric>()
				.FromInstance(pointFabric)
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