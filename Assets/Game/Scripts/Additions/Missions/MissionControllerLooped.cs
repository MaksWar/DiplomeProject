using UnityEngine;

namespace Additions.Missions
{
	public class MissionControllerLooped : MissionController
	{
		[Header("Индекс входа в цикл"), Space]
		[SerializeField] private int loopEnterIndex;
		[Header("Индекс выхода из цикла")]
		[SerializeField] private int loopExitIndex;
		[Header("Кол-во циклов")]
		[SerializeField] private int loopsCount;

		private int _completedLoopsCount;
		private bool _exitFromLoop;
		private bool _hardExitFromLoop;

		public int LoopsCount => loopsCount;

		public int CompletedLoopsCount => _completedLoopsCount;

		[ContextMenu("SoftExitFromLoop")]
		public void SoftExitFromLoop() =>
			_completedLoopsCount = loopsCount + 1;

		[ContextMenu("ExitFromLoop")]
		public void ExitFromLoop()
		{
			_exitFromLoop = true;
			_completedLoopsCount = loopsCount + 1;
			Index = loopExitIndex;
		}

		[ContextMenu("HardExitFromLoop")]
		public void HardExitFromLoop()
		{
			var index = Index;
			ExitFromLoop();
			missionSequence[index].DisableMission();
			_hardExitFromLoop = true;
		}

		protected override void OnMissionEnd(MissionBase obj)
		{
			CallOnMissionEnded();

			if (Index < missionSequence.Count - 1 || NeedLoop())
				NextMission();
			else
				LastMissionCompleted();
		}

		protected override void NextMission()
		{
			if (_exitFromLoop)
			{
				_exitFromLoop = false;
				missionSequence[++Index].EnableMission();
				return;
			}

			if (_hardExitFromLoop)
			{
				_hardExitFromLoop = false;
				return;
			}

			if (NeedEnterLoop(Index))
				_completedLoopsCount++;

			if (NeedRestartLoop(Index))
			{
				Index = loopEnterIndex;
				missionSequence[Index].EnableMission();

				return;
			}

			if (loopExitIndex > missionSequence.Count - 1)
				Debug.Log($"<b><color=red>ОШИБКА: </color> Loop Exit Index выходит за Mission Sequence</b> ");

			if (loopEnterIndex > missionSequence.Count - 1)
				Debug.Log($"<b><color=red>ОШИБКА: </color> Loop Enter Index выходит за Mission Sequence</b> ");

			missionSequence[++Index].EnableMission();
		}

		private bool NeedEnterLoop(int currentIndex) =>
			NeedLoop() && currentIndex == loopEnterIndex;

		private bool NeedRestartLoop(int currentIndex) =>
			NeedLoop() && currentIndex == loopExitIndex;

		private bool NeedLoop() =>
			_completedLoopsCount < loopsCount;
	}
}