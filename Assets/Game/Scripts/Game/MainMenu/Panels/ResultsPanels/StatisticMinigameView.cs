using Infrastructure.Data;
using TMPro;
using UnityEngine;

namespace MainMenu.ResultsPanels
{
	public class StatisticMinigameView : MonoBehaviour, IResultInfoObject
	{
		[SerializeField] private TextMeshProUGUI LastMistakesText;
		[SerializeField] private TextMeshProUGUI LastTimeCompleteText;
		[SerializeField] private TextMeshProUGUI LastMarkText;
		[SerializeField] private TextMeshProUGUI AvgMistakesText;
		[SerializeField] private TextMeshProUGUI AvgTimeCompleteText;
		[SerializeField] private TextMeshProUGUI AvgMarkText;
		[SerializeField] private TextMeshProUGUI CountPlays;

		public GameObject GameObject => gameObject;

		public void SetInfo(MiniGameStatistic miniGameStatistic)
		{
			LastMistakesText.text = $"Last Mistakes : {miniGameStatistic.LastMistakesCount}";
			LastTimeCompleteText.text = $"Last Time Complete : {miniGameStatistic.LastTimeComplete:F1}";
			LastMarkText.text = $"Last Mark : {miniGameStatistic.LastMark}";
			AvgMistakesText.text = $"Avg Mistakes : {miniGameStatistic.AvgMistakesCount:F1}";
			AvgTimeCompleteText.text = $"Avg Time Complete : {miniGameStatistic.AvgTimeComplete:F1}";
			AvgMarkText.text = $"Avg Mark : {miniGameStatistic.AvgMark:F1}";
			CountPlays.text = $"Count Plays : {miniGameStatistic.CountPlays}";
		}
	}
}