using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ModestTree;
using UnityEngine;
using Zenject;

namespace MainMenu
{
	public class GamesCardsPanel : MonoBehaviour, IPanel
	{
		[SerializeField] private UIButton closeButton;
		[SerializeField] private CategoriesPanel categoriesPanel;
		[SerializeField] private Fader fader;
		[SerializeField] private Transform content;

		private IGameCardFabric _gameCardFabric;

		private List<GameCardView> _gameCardViews;

		[Inject]
		private void Construct(IGameCardFabric gameCardFabric) =>
			_gameCardFabric = gameCardFabric;

		public async void Show(Category category, Action onComplete = null)
		{
			if (_gameCardViews?.IsEmpty() == false)
				ClearViews();

			_gameCardViews = _gameCardFabric.CreateGameCardView(category, content, Vector3.zero);

			gameObject.SetActive(true);

			closeButton.Butt.onClick.AddListener(HideAndShowCategories);
			fader.OnClick += HideAndShowCategories;

			foreach (var gameCardView in _gameCardViews)
			{
				if (gameCardView == _gameCardViews.Last())
				{
					gameCardView.Show(onComplete: onComplete);

					break;
				}

				gameCardView.Show();
				await Task.Delay(200);
			}
		}

		public async void Hide(Action onComplete = null)
		{
			if (_gameCardViews.IsEmpty())
				return;

			foreach (var gameCard in _gameCardViews)
			{
				if (gameCard == _gameCardViews.Last())
				{
					gameCard.Hide(onComplete: delegate
					{
						onComplete?.Invoke();

						ClearViews();
						gameObject.SetActive(false);
					});

					break;
				}

				gameCard.Hide();
				await Task.Delay(200);
			}

			fader.OnClick -= HideAndShowCategories;
		}

		private void HideAndShowCategories()
		{
			closeButton.Butt.onClick.RemoveAllListeners();
			Hide(() => categoriesPanel.Show());
		}

		private void ClearViews()
		{
			_gameCardViews.ForEach(x => Destroy(x.gameObject));
			_gameCardViews.Clear();
		}
	}
}