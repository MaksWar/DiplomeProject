using System.Collections;
using UnityEngine;

namespace Additions.Utils
{
	public interface ICoroutineRunner
	{
		Coroutine StartCoroutine(IEnumerator coroutine);

		void StopCoroutine(Coroutine coroutine);
	}
}