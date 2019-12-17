using System;
using UnityEngine;
using System.Collections;

namespace Showroom.Utility {
	public class CustomCoroutine : MonoBehaviour {

		private static CustomCoroutine instance;
		
		private static void TryCreateInstance() {
			if (instance == null) {
				instance = (new GameObject("CustomCoroutineRunner")).AddComponent<CustomCoroutine>();
			}
		}

		private void Start() {
			DontDestroyOnLoad(this.gameObject);
		}

		public static void Start(IEnumerator coroutine) {
			TryCreateInstance();
			instance.StartCoroutine(coroutine);
		}

		public static void WaitOneFrameThenExecute(Action action) {
			TryCreateInstance();
			instance.StartCoroutine(instance.DoWaitOneFrameThenExecute(action));
		}

		public static void WaitOnConditionThenExecute(Func<bool> condition, Action action) {
			TryCreateInstance();
			instance.StartCoroutine(instance.DoWaitOnConditionThenExecute(condition, action));
		}

		public static void WaitThenExecute(float wait, Action action, bool unscaledTime = false) {
			TryCreateInstance();
			instance.StartCoroutine(instance.DoWaitThenExecute(wait, action, unscaledTime));
		}

		IEnumerator DoWaitOneFrameThenExecute(Action action) {
			yield return 0;
			action();
		}

		IEnumerator DoWaitOnConditionThenExecute(Func<bool> condition, Action action) {
			yield return new WaitUntil(condition);
			action();
		}
		
		IEnumerator DoWaitThenExecute(float wait, Action action, bool unscaledTime = false) {
			if (wait <= 0f) {
				yield return new WaitForEndOfFrame();
			}
			else {
				if (unscaledTime) {
					yield return new WaitForSecondsRealtime(wait);
				}
				else {
					yield return new WaitForSeconds(wait);
				}
			}

			action();
		}
	}
}