using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Additions.UI.RetractableElements
{
	public class OpenRetractableElement : MonoBehaviour, IPointerClickHandler
	{
		[SerializeField] private RetractableUiElement elementToOpen;

		public event Action OnClick;

		public void SetElementToOpen(RetractableUiElement element)
		{
			elementToOpen = element;
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			OnClick?.Invoke();
			elementToOpen.Show();
		}
	}
}