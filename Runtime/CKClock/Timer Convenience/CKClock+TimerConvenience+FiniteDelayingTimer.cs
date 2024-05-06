// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using UnityEngine;

namespace ClockKit {
	public static partial class CKClock {
		public static CKKey Delay(
			CKQueue queue,
			float seconds,
			in CKFiniteDelayingTimer.CompletionCallback onComplete
		) {
			ICKTimer timer = new CKFiniteDelayingTimer(
				startTime: Time.time,
				duration: seconds,
				onComplete: onComplete
			);
			return StartTimer(queue, timer);
		}

		public static CKKey Delay(
			float seconds,
			in CKFiniteDelayingTimer.CompletionCallback onComplete
		)
			=> Delay(CKQueue.Default, seconds, onComplete);
	}
}