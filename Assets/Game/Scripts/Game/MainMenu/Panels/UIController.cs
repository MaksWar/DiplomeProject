using System.Collections.Generic;
using MainMenu.ResultsPanels;
using UnityEngine;

namespace MainMenu
{
	public class UIController : MonoBehaviour
	{
		[SerializeField] private UIButton PlayButton;
		[SerializeField] private UIButton ResultsButton;
		[SerializeField] private UIButton ExitButton;
		[SerializeField] private CategoriesPanel categoriesPanel;
		[SerializeField] private ResultPanel resultPanel;

		private List<IPanel> _panels;

		private void Awake()
		{
			PlayButton.Butt.onClick.AddListener(() => categoriesPanel.Show());
			ResultsButton.Butt.onClick.AddListener(() => resultPanel.Show());
			ExitButton.Butt.onClick.AddListener(Exit);

			categoriesPanel.gameObject.SetActive(false);
			categoriesPanel.Hide(0);
		}


		private void Exit() =>
			Application.Quit();
	}
}