using System.Linq;
using System.Threading.Tasks;
using Additions.Missions;
using UnityEngine;

namespace Game.Minigames.FindPurpose
{
	public class ShowPointsMission : MissionBase
	{
		[SerializeField] private PointViewHandler pointViewHandler;
		[SerializeField] private PositionForSpawnContainer positionForSpawnContainer;

		protected override async void Mission()
		{
			foreach (PointView view in pointViewHandler.PointViews)
			{
				view.transform.position = positionForSpawnContainer.GetRandomPosition().position;
				if(view == pointViewHandler.PointViews.Last())
					view.Show(onComplete: End);
				else
					view.Show();

				await Task.Delay(200);
			}
		}
	}
}