using System.Collections.Generic;
using System.Linq;
using Additions.Missions;
using Additions.Utils;
using Game.Minigames.ChooseNotCorrectPicture.Core;
using TMPro;
using UnityEngine;
using Zenject;

namespace Game.Minigames.FindPurpose
{
	public class ChooseTypeOfPointMissionFindPurpose : MissionBase, ICoroutineRunner
	{
		[SerializeField] private MissClickController missClickController;
		[SerializeField] private PositionForSpawnContainer positionForSpawnContainer;
		[SerializeField] private PointViewHandler pointViewHandler;

		private ILevelsAnalyticDataHandler _levelsAnalyticDataHandler;
		private IChooserPointTypes _chooserPointTypes;
		private IMistakesCounter _overallMistakesCounter;
		private IMistakesCounter _levelMistakesCounter;
		private ITimeCounter _overallTimeCounter;
		private ITimeCounter _levelTimeCounter;

		private List<PointView> _pointViews;

		[Inject]
		private void Construct(
			IChooserPointTypes chooserPointTypes,
			IMistakesCounter mistakesCounter,
			ILevelsAnalyticDataHandler levelsAnalyticDataHandler,
			ITimeCounter timeCounter
		)
		{
			_overallTimeCounter = timeCounter;
			_levelsAnalyticDataHandler = levelsAnalyticDataHandler;
			_overallMistakesCounter = mistakesCounter;
			_chooserPointTypes = chooserPointTypes;
		}

		protected override void Mission()
		{
			_levelMistakesCounter = new MistakesCounter();
			_levelTimeCounter = new TimeCounter(this);

			_overallTimeCounter.Continue();
			_levelTimeCounter.Start();

			_pointViews = pointViewHandler.PointViews;

			missClickController.OnMissClick += MissClick;
			_pointViews.ForEach(x => x.OnPointSelected += PointSelect);
		}

		private void MissClick()
		{
			_levelMistakesCounter.Add(1);
			_overallMistakesCounter.Add(1);

			if (IsMaxMistakes())
				EndMission();
		}

		private void PointSelect()
		{
			if (IsComplete())
				EndMission();
		}

		private bool IsMaxMistakes() =>
			_chooserPointTypes.LevelAnalyticData.MaxCountMistakes <= _levelMistakesCounter.CountOfTry;

		private void EndMission()
		{
			missClickController.OnMissClick -= MissClick;
			_pointViews.ForEach(x => x.OnPointSelected -= PointSelect);

			_overallTimeCounter.Stop();
			_levelTimeCounter.Stop();

			_levelsAnalyticDataHandler.SaveAnalyticData(_chooserPointTypes.LevelAnalyticData, _levelTimeCounter.Seconds, _levelMistakesCounter.CountOfTry);

			pointViewHandler.PointViews.ForEach(x => Destroy(x.gameObject));
			pointViewHandler.PointViews.Clear();

			positionForSpawnContainer.ResetInfo();

			End();
		}

		private bool IsComplete() =>
			_pointViews.All(x => x.IsSelected);
	}
}