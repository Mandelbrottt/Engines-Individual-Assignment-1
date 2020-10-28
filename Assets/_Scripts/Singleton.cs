using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour {
	private static T s_instance;

	public static T Instance {
		get {
			if (s_instance != null) {
				return s_instance;
			}

			var instances = FindObjectsOfType<T>();
			switch (instances.Length) {
			case 0:
				var go = new GameObject($"{nameof(T)} Singleton");
				s_instance = go.AddComponent<T>();
				break;
			case 1:
				s_instance = instances[0];
				break;
			default:
				Debug.Assert(false, $"There are more than one \"{nameof(T)}\"!");
				break;
			}

			return s_instance;
		}
	}

	private void Awake() {
		Debug.Assert(s_instance == null);
	}
}
