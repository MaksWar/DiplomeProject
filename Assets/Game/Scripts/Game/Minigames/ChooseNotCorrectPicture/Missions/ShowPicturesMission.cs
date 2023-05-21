using System.Linq;
using System.Threading.Tasks;
using Additions.Missions;
using Game.Minigames.ChooseNotCorrectPicture.Core;
using UnityEngine;

namespace Game.Minigames.ChooseNotCorrectPicture.Missions
{
	public class ShowPicturesMission : MissionBase
	{
		[SerializeField] private PictureViewHandler pictureViewHandler;

		protected override async void Mission()
		{
			foreach (var x in pictureViewHandler.PictureViews)
			{
				if(x == pictureViewHandler.PictureViews.Last())
					x.Show(onComplete: End);
				else
					x.Show();

				await Task.Delay(200);
			}
		}
	}
}