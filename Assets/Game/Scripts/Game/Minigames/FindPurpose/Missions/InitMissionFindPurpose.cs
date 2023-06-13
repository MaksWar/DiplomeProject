using Additions.Missions;
using UnityEngine;
using Zenject;

namespace Game.Minigames.FindPurpose
{
	public class InitMissionFindPurpose : MissionBase
	{
		[SerializeField] private PointViewHandler pointViewHandler;

		private IChooserPointTypes _chooserPointTypes;

		[Inject]
		private void Construct(IChooserPointTypes chooserPointTypes) =>
			_chooserPointTypes = chooserPointTypes;

		protected override void Mission()
		{
			_chooserPointTypes.InitCorrectPoints();
			pointViewHandler.SpawnView(_chooserPointTypes.PointTypes);

			End();
		}
	}
}