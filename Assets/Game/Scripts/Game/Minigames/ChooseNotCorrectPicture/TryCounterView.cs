using Game.Minigames.ChooseNotCorrectPicture.Core;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Minigames.ChooseNotCorrectPicture
{
	public class TryCounterView : MonoBehaviour
	{
		[SerializeField] private Text tryCountText;

		private IMistakesCounter mistakesCounter;

		[Inject]
		private void Construct(IMistakesCounter mistakesCounter) =>
			this.mistakesCounter = mistakesCounter;

		private void OnDestroy() =>
			mistakesCounter.OnTryCountChanges -= UpdateView;

		private void Awake()
		{
			mistakesCounter.OnTryCountChanges += UpdateView;

			UpdateView(0);
		}

		private void UpdateView(int count) =>
			tryCountText.text = $"Спроба : {count}";
	}
}