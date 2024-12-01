// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using UnityEngine;

namespace ClockKit {
	internal sealed partial class CKClockController {
		private static CKClockController _shared = default;

		/// <summary>
		/// The shared instance of the Component.
		/// </summary>
		public static CKClockController Shared {
			get {
				if (_shared == null) {
					CKClockController newShared
#if UNITY_2023_1_OR_NEWER
						= GameObject.FindAnyObjectByType<CKClockController>();
#else
						= GameObject.FindObjectOfType<CKClockController>();
#endif
					if (newShared != null) {
						newShared.InitializeSingleton();
					}

					if (_shared == null) {
						GameObject singletonObject = new GameObject {
							hideFlags = HideFlags.HideAndDontSave,
							name = "Singleton<CKClockController>"
						};

						_shared = singletonObject.AddComponent<CKClockController>();
					}
				}

				return _shared;
			}
		}

		private void InitializeSingleton() {
			if (_shared != null && _shared != this) {
				GameObject.Destroy(this);
				return;
			}

			_shared = this;
			GameObject.DontDestroyOnLoad(this.gameObject);
		}

		/// <summary>
		/// Perform any necessary cleanup.
		/// </summary>
		public void DeinitializeSingleton() {
			_shared = default;
			GameObject.Destroy(this.gameObject);
		}
	}
}