using System.Collections.Generic;
using System.Linq;
using Additions.Extensions;
using UnityEngine;

namespace Game.Minigames.ChooseNotCorrectPicture.Core
{
	public class PictureViewFabric : IPictureViewFabric
	{
		private List<PictureView> _picturesView;

		public PictureViewFabric(List<PictureView> pictureViews) =>
			_picturesView = pictureViews;

		public PictureView CreatePictureView(PictureType pictureType, Transform parent, Vector3 startScale)
		{
			PictureView prefab = _picturesView.Where(x => x.Type == pictureType).ToList().GetRandomElement();

			PictureView pictureView = PicturesViewPool.Instance.Pop(prefab);
			pictureView.transform.parent = parent;
			pictureView.transform.position = Vector3.zero;
			pictureView.transform.localScale = startScale;

			return pictureView;
		}
	}
}