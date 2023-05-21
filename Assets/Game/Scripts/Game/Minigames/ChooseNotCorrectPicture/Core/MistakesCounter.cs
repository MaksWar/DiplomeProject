using System;

namespace Game.Minigames.ChooseNotCorrectPicture.Core
{
	public class MistakesCounter : IMistakesCounter
	{
		private int _countOfTry;

		public int CountOfTry => _countOfTry;

		public event Action<int> OnTryCountChanges;

		public void Add(int count)
		{
			if(count <= 0)
				return;

			_countOfTry += count;

			OnTryCountChanges?.Invoke(_countOfTry);
		}

		public void Reset() =>
			_countOfTry = 0;
	}
}