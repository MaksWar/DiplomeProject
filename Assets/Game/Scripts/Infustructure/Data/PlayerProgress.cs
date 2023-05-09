using System;

namespace Infrastructure.Data
{
	[Serializable]
	public class PlayerProgress
	{
		public string InitialScene;

		public PlayerProgress(string initialLevel)
		{
			InitialScene = initialLevel;
		}
	}
}