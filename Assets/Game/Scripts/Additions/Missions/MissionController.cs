using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections;
using System.Linq;
using Infrastructure;
using Infrastructure.Factory;
using Zenject;

namespace Additions.Missions
{
	public class MissionController : MonoBehaviour
	{
		[Header("Сцена после окончания миссий")] 
		[SerializeField] private string nextScene = Scenes.InitialScene;
		[Header("Последовательный список миссий")] 
		[SerializeField] protected List<MissionBase> missionSequence = new List<MissionBase>();

		[SerializeField, HideInInspector] private bool useAdditionalSettings;
		[SerializeField, HideInInspector] private bool useDonePanel = true;
		[SerializeField, HideInInspector] private float delayAfterLastMission;

		protected int Index;

		private IUIFactory _uiFactory;
		private SceneLoader _sceneLoader;

		private DonePanelType _donePanelType = DonePanelType.WellDone;

		public event Action OnMissionEnded;

		[Inject]
		private void Construct(IUIFactory uiFactory, SceneLoader sceneLoader)
		{
			_sceneLoader = sceneLoader;
			_uiFactory = uiFactory;
		}

		private void OnEnable() =>
			missionSequence.ForEach(item => item.OnMissionEnd += OnMissionEnd);

		private void OnDisable() =>
			missionSequence.ForEach(item => item.OnMissionEnd -= OnMissionEnd);

		private void Start()
		{
			missionSequence.ForEach(x => x.gameObject.SetActive(false));
			missionSequence[Index].EnableMission();
		}

		public T GetMission<T>()
		{
			MissionBase mission = missionSequence.FirstOrDefault(item => item.GetType() == typeof(T));
			if (mission is T)
				return (T) Convert.ChangeType(mission, typeof(T));

			return default;
		}

		public Type GetCurrentMissionType() =>
			missionSequence[Index].GetType();

		public void SetDonePanel(DonePanelType donePanelType) => 
			_donePanelType = donePanelType;

		protected virtual void OnMissionEnd(MissionBase obj)
		{
			CallOnMissionEnded();

			if (Index < missionSequence.Count - 1)
				NextMission();
			else
				LastMissionCompleted();
		}

		protected virtual void NextMission()
		{
			Index++;
			missionSequence[Index].EnableMission();
		}

		protected void CallOnMissionEnded() =>
			OnMissionEnded?.Invoke();


		protected void LoadDonePanel() =>
			ShowDonePanelWhichType();

		protected void LastMissionCompleted() => 
			StartCoroutine(DelayAfterLastMission());


		private void ShowDonePanelWhichType()
		{
			/*switch (_donePanelType)
			{
				case DonePanelType.WellDone:
					var wellDonePanel = _uiFactory.CreateWellDone();
					wellDonePanel.SetNextScene(nextScene);
					break;
				case DonePanelType.TryAgain:
					var tryAgainPanel = _uiFactory.CreateTryAgain();
					tryAgainPanel.SetNextScene(nextScene);
					tryAgainPanel.SetAgainScene(SceneLoader.GetCurrentScene());
					break;
			}*/
		}

		private IEnumerator DelayAfterLastMission()
		{
			yield return new WaitForSeconds(delayAfterLastMission);

			if (useDonePanel)
				LoadDonePanel();
			else
				_sceneLoader.Load(nextScene);
		}

		#region Editor

		public void GetMissionInChildrenObjects()
		{
			var missions = GetComponentsInChildren<MissionBase>();

			missionSequence.Clear();
			missionSequence.AddRange(missions);
		}

		#endregion
	}

	public enum DonePanelType
	{
		WellDone = 0,
		TryAgain = 1,
	}
}