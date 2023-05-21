using System;

namespace Additions.Utils
{
	public interface ITimeCounter
	{
		public event Action OnTimeChanged;

		float Seconds { get; }

		void Start();

		void Continue();

		void Stop();
	}
}