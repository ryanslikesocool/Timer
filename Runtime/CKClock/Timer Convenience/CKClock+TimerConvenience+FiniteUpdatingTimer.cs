// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using UnityEngine;

namespace ClockKit {
	public static partial class CKClock {
		public static CKKey Update(
			CKQueue queue,
			float seconds,
			in CKFiniteUpdatingTimer.UpdateCallback onUpdate,
			in CKFiniteUpdatingTimer.CompletionCallback onComplete = null
		) {
			ICKTimer timer = new CKFiniteUpdatingTimer(
				startTime: Time.time,
				seconds: seconds,
				onUpdate: onUpdate,
				onComplete: onComplete
			);
			return StartTimer(queue, timer);
		}

		public static CKKey Update(
			float seconds,
			in CKFiniteUpdatingTimer.UpdateCallback onUpdate,
			in CKFiniteUpdatingTimer.CompletionCallback onComplete = null
		)
			=> Update(CKQueue.Default, seconds: seconds, onUpdate, onComplete);
	}
}