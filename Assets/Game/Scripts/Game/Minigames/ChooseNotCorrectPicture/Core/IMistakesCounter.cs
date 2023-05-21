using System;

namespace Game.Minigames.ChooseNotCorrectPicture.Core
{
	public interface IMistakesCounter
	{
		int CountOfTry { get; }

		event Action<int> OnTryCountChanges;

		void Add(int count);

		void Reset();
	}
}