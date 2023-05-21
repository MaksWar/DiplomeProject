using UnityEditor;
using UnityEngine;

namespace Additions.Missions.Editor
{
	[CustomEditor(typeof(MissionController),true)]
	public class MissionControllerEditor : UnityEditor.Editor
	{
		private SerializedProperty useAdditionalSettings;
		private SerializedProperty useDonePanel;
		private SerializedProperty delayAfterLastMission;

		private void OnEnable()
		{
			useAdditionalSettings = serializedObject.FindProperty("useAdditionalSettings");
			useDonePanel = serializedObject.FindProperty("useDonePanel");
			delayAfterLastMission = serializedObject.FindProperty("delayAfterLastMission");
		}


		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			var controller = (MissionController) target;

			EditorGUILayout.Space();

			if (GUILayout.Button("Take missions in children`s objects"))
				controller.GetMissionInChildrenObjects();

			EditorGUILayout.Space();
			EditorGUILayout.PropertyField(useAdditionalSettings);

			if (useAdditionalSettings.boolValue)
			{
				EditorGUILayout.PropertyField(useDonePanel);
				EditorGUILayout.PropertyField(delayAfterLastMission);
			}

			serializedObject.ApplyModifiedProperties();
		}
	}
}