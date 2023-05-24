using TMPro;
using UnityEngine;

namespace MainMenu.ResultsPanels
{
	public class StatisticMinigameTitleView : MonoBehaviour, IResultInfoObject
	{
		[SerializeField] private TextMeshProUGUI TitleText;

		public GameObject GameObject => gameObject;

		public void SetText(string category) =>
			TitleText.text = category;
	}
}