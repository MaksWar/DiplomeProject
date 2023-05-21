using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.Minigames.ChooseNotCorrectPicture
{
	[CreateAssetMenu(menuName = "Data/PictureConditionCollection", fileName = "PictureConditionCollection", order = 0)]
	public class PictureConditionCollection : ScriptableObjectInstaller<PictureConditionCollection>
	{
		[SerializeField] private List<PictureCondition> pictureConditions;

		public List<PictureCondition> PictureConditions => pictureConditions;

		public override void InstallBindings()
		{
			Container
				.Bind<PictureConditionCollection>()
				.FromInstance(this);
		}
	}
}