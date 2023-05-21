using Additions.Missions;
using Game.Minigames.ChooseNotCorrectPicture.Core;
using UnityEngine;
using Zenject;

namespace Game.Minigames.ChooseNotCorrectPicture.Missions
{
	public class InitMission : MissionBase
	{
		[SerializeField] private PictureViewHandler pictureViewHandler;

		private IChooserNotCorrectController chooserNotCorrectController;

		[Inject]
		private void Construct(IChooserNotCorrectController chooser) =>
			chooserNotCorrectController = chooser;

		protected override void Mission()
		{
			chooserNotCorrectController.InitConditions();
			pictureViewHandler.SpawnView(chooserNotCorrectController.Conditions);

			End();
		}
	}
}