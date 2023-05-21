using System;
using System.Collections;
using System.Globalization;
using System.Linq;
using Infrastructure.Data;
using Infrastructure.Services.PersistentProgress;
using Infrastructure.Services.SaveLoad;
using MainMenu;
using UnityEngine;

namespace Infrastructure.Services.DBSyncService
{
	public class DatabaseSyncService : IDatabaseSyncService
	{
		private readonly IPersistentProgressService _progressService;
		private readonly ICoroutineRunner _coroutineRunner;
		private readonly ISaveLoadService _saveLoadService;

		public DatabaseSyncService(
			IPersistentProgressService progressService,
			ISaveLoadService saveLoadService,
			ICoroutineRunner coroutineRunner
		)
		{
			_saveLoadService = saveLoadService;
			_coroutineRunner = coroutineRunner;
			_progressService = progressService;
		}

		public void SendData(MinigameProgress progress) =>
			_coroutineRunner.StartCoroutine(Send(progress));

		private IEnumerator Send(MinigameProgress progress)
		{
			MiniGameStatistic statistic = progress.MiniGameStatistic;

			WWWForm form = new WWWForm();
			form.AddField("ID", _progressService.Progress.UniqueID);
			form.AddField("gameCategory", (int) progress.gameCategory);
			form.AddField("LastMistakesCount", statistic.LastMistakesCount);
			form.AddField("LastTimeComplete", statistic.LastTimeComplete.ToString());
			form.AddField("LastMark", statistic.LastMark);
			form.AddField("AvgMistakesCount", statistic.AvgMistakesCount.ToString());
			form.AddField("AvgTimeComplete", statistic.AvgTimeComplete.ToString());
			form.AddField("AvgMark", statistic.AvgMark.ToString());
			form.AddField("CountPlays", statistic.CountPlays);
			
			Debug.Log(statistic.LastTimeComplete.ToString());
			
			WWW www = new WWW("http://localhost/SqlConnect/SendData.php", form);
			yield return www;
			if (www.text == "0")
			{
				Debug.Log("123");
			}
			else
			{
				Debug.Log("Errooor" + www.text);
			}
		}

		public void SyncData() =>
			_coroutineRunner.StartCoroutine(Get());

		private IEnumerator Get()
		{
			foreach (Category category in Enum.GetValues(typeof(Category)).Cast<Category>())
			{
				WWWForm form = new WWWForm();
				PlayerProgress playerProgress = _progressService.Progress;
				form.AddField("ID", playerProgress.UniqueID);
				form.AddField("gameCategory", (int) category);
				WWW www = new WWW("http://localhost/SqlConnect/GetterData.php", form);

				yield return www;

				if (www.text[0] == '0')
				{
					string[] splitedValues = www.text.Split('\t');
					MinigameProgress progress =
						playerProgress.GetMinigameProgress((Category) int.Parse(splitedValues[1]));

					MiniGameStatistic miniGameStatistic = progress.MiniGameStatistic;
					miniGameStatistic.LastMistakesCount = int.Parse(splitedValues[2]);
					miniGameStatistic.LastTimeComplete = float.Parse(splitedValues[3], CultureInfo.InvariantCulture);
					miniGameStatistic.LastMark = int.Parse(splitedValues[4]);
					miniGameStatistic.AvgMistakesCount = float.Parse(splitedValues[5], CultureInfo.InvariantCulture);
					miniGameStatistic.AvgTimeComplete = float.Parse(splitedValues[6], CultureInfo.InvariantCulture);
					miniGameStatistic.AvgMark = float.Parse(splitedValues[7], CultureInfo.InvariantCulture);
					miniGameStatistic.CountPlays = int.Parse(splitedValues[8]);

					_saveLoadService.SaveLocal();
				}
				else
				{
					Debug.Log($"GetData Error : {www.text}");
				}
			}
		}

		public void RegisterPlayer(string uniqueId) =>
			_coroutineRunner.StartCoroutine(Register(uniqueId));

		public IEnumerator Register(string uniqueId)
		{
			WWWForm form = new WWWForm();
			form.AddField("ID", uniqueId);
			WWW www = new WWW("http://localhost/SqlConnect/RegisterUniqueID.php", form);
			yield return www;
			if (www.text == "0")
				Debug.Log($"Success Register : {uniqueId}");
			else if(www.text == "2")
				Debug.Log($"{www.text} : User Exist!");
			else
				Debug.Log("Errooor" + www.text);
		}
	}
}