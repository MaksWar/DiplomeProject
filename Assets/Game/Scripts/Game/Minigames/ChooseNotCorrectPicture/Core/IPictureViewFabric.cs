using UnityEngine;

namespace Game.Minigames.ChooseNotCorrectPicture.Core
{
	public interface IPictureViewFabric
	{
		PictureView CreatePictureView(PictureType pictureType, Transform parent, Vector3 startScale);
	}
}