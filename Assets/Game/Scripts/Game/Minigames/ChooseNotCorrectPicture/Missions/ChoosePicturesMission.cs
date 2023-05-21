using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Additions.Missions;
using Additions.UI.RetractableElements;
using Additions.Utils;
using Game.Minigames.ChooseNotCorrectPicture.Core;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Minigames.ChooseNotCorrectPicture.Missions
{
	public class ChoosePicturesMission : MissionBase, ICoroutineRunner
	{
		[SerializeField] private PictureViewHandler pictureViewHandler;
		[Header("Confirm button"), Space(15)]
		[SerializeField] private Button confirmButton;
		[SerializeField] private RetractableUiElement confirmButtonShowHide;

		private ILevelsAnalyticDataHandler _levelsAnalyticDataHandler;
		private IChooserNotCorrectController _chooser;
		private IMistakesCounter _overallMistakesCounter;
		private IMistakesCounter _levelMistakesCounter;
		private ITimeCounter _overallTimeCounter;
		private ITimeCounter _levelTimeCounter;
		private List<PictureView> viewHandlerPictureViews;

		[Inject]
		private void Construct(
			IChooserNotCorrectController chooserNotCorrectController,
			IMistakesCounter mistakesCounter,
			ILevelsAnalyticDataHandler levelsAnalyticDataHandler,
			ITimeCounter timeCounter
		)
		{
			_overallTimeCounter = timeCounter;
			_levelsAnalyticDataHandler = levelsAnalyticDataHandler;
			_overallMistakesCounter = mistakesCounter;
			_chooser = chooserNotCorrectController;
		}

		protected override void Mission()
		{
			_levelTimeCounter = new TimeCounter(this);
			_levelMistakesCounter = new MistakesCounter();

			_overallTimeCounter.Continue();
			_levelTimeCounter.Start();

			viewHandlerPictureViews = pictureViewHandler.PictureViews;

			viewHandlerPictureViews.ForEach(x => x.OnPictureSelected += PictureSelect);
			confirmButton.onClick.AddListener(CheckAnswer);
		}

		private void PictureSelect(PictureView view)
		{
			viewHandlerPictureViews.Where(x => x == x.IsSelected).ForEach(x => x.UnSelect());
			viewHandlerPictureViews.First(x => x == view).Select();
		}

		private async void CompleteMission()
		{
			viewHandlerPictureViews.ForEach(x => x.OnPictureSelected -= PictureSelect);
			foreach (var view in viewHandlerPictureViews)
			{
				if (view == viewHandlerPictureViews.Last())
				{
					view.Hide(onComplete: EndMission);

					break;
				}

				view.Hide();

				await Task.Delay(200);
			}
		}

		private void EndMission()
		{
			viewHandlerPictureViews.ForEach(x => Destroy(x.gameObject));
			viewHandlerPictureViews.Clear();

			End();
		}

		private void CheckAnswer()
		{
			PictureView answer = viewHandlerPictureViews.FirstOrDefault(x => x.IsSelected);
			if (answer == null)
				return;

			if (answer.Type == _chooser.IsNotCorrectType)
			{
				_overallTimeCounter.Stop();
				_levelTimeCounter.Stop();

				confirmButtonShowHide.Hide();
				confirmButton.onClick.RemoveAllListeners();

				_levelsAnalyticDataHandler.SaveAnalyticData(_chooser.LevelAnalyticData, _levelTimeCounter.Seconds, _levelMistakesCounter.CountOfTry);

				CompleteMission();
			}
			else
			{
				answer.UnSelect();
				answer.Lock();
				answer.ShakeAndHide();

				_overallMistakesCounter.Add(1);
				_levelMistakesCounter.Add(1);
			}
		}
	}
}