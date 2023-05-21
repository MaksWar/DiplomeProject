using UnityEngine;

namespace Infrastructure.Factory
{
	public interface IUIFactory
	{
		Camera MainCamera { get; }
		Camera UICamera { get; }

		GameObject CreateMainCamera();
		GameObject CreateUICamera();
		GameObject CreateHUD();
		GameObject CreateResultPanel();
	}
}