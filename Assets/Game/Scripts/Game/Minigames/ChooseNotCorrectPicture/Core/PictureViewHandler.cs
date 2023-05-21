using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Game.Minigames.ChooseNotCorrectPicture.Core
{
	public class PictureViewHandler : MonoBehaviour
	{
		[SerializeField] private Transform content;

		private IPictureViewFabric _viewFabric;

		private List<PictureView> _pictureViews;

		public List<PictureView> PictureViews => _pictureViews;

		[Inject]
		private void Construct(IPictureViewFabric pictureViewFabric) =>
			_viewFabric = pictureViewFabric;

		public void SpawnView(List<Condition> conditions)
		{
			List<PictureType> types = conditions.Select(x => x.PictureType).ToList();

			_pictureViews = new List<PictureView>();
			foreach (var type in types)
				PictureViews.Add(_viewFabric.CreatePictureView(type, content, Vector3.zero));
		}
	}
}