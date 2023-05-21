using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MainMenu
{
	public class CategoriesPanel : MonoBehaviour, IPanel
	{
		[SerializeField] private Fader fader;
		[SerializeField] private UIButton closeButton;
		[SerializeField] private GamesCardsPanel gamesCardsPanel;
		[SerializeField] private List<CategoryView> categoryViews;

		private void Awake() =>
			categoryViews.ForEach(x => x.Hide(0));

		[Button]
		public async void Show(float duration = 0.5f, Action onComplete = null)
		{
			closeButton.Show();
			closeButton.Butt.onClick.AddListener(() => FadeHide());

			fader.OnClick += FadeHide;
			fader.Fade();

			gameObject.SetActive(true);
			foreach (var view in categoryViews)
			{
				view.OnClick += SelectedCategory;
				if (view == categoryViews.Last())
				{
					view.Show(duration, onComplete: onComplete);

					break;
				}

				view.Show(duration);
				await Task.Delay(200);
			}
		}

		[Button]
		public async void Hide(float duration = 0.5f, Action onComplete = null, bool unFade = true, bool closeButtonHide = true)
		{
			closeButton.Butt.onClick.RemoveAllListeners();
			if(closeButtonHide)
				closeButton.Hide();

			fader.OnClick -= FadeHide;
			if(unFade)
				fader.UnFade();

			foreach (var view in categoryViews)
			{
				view.OnClick -= SelectedCategory;
				if (view == categoryViews.Last())
				{
					view.Hide(duration, onComplete: delegate
					{
						onComplete?.Invoke();
						gameObject.SetActive(true);
					});

					break;
				}

				view.Hide(duration);
				await Task.Delay(200);
			}
		}

		private void SelectedCategory(Category category) =>
			Hide(onComplete: () => gamesCardsPanel.Show(category), unFade: false, closeButtonHide: false);

		private void FadeHide() => Hide();
	}
}