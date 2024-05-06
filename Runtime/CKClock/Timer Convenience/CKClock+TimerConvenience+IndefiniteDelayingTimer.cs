// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using UnityEngine;

namespace ClockKit {
	public static partial class CKClock {
		public static CKKey Delay(
			CKQueue queue,
			in CKIndefiniteDelayingTimer.CompletionPredicate until,
			in CKIndefiniteDelayingTimer.CompletionCallback onComplete = null
		) {
			ICKTimer timer = new CKIndefiniteDelayingTimer(
				startTime: Time.time,
				onComplete: onComplete,
				completionPredicate: until
			);
			return StartTimer(queue, timer);
		}

		public static CKKey Delay(
			CKQueue queue,
			in CKIndefiniteDelayingTimer.CompletionPredicate until,
			in CKIndefiniteDelayingTimer.SimpleCompletionCallback onComplete = null
		) {
			ICKTimer timer = new CKIndefiniteDelayingTimer(
				startTime: Time.time,
				onComplete: onComplete,
				completionPredicate: until
			);
			return StartTimer(queue, timer);
		}

		public static CKKey Delay(
			CKQueue queue,
			in CKIndefiniteDelayingTimer.SimpleCompletionPredicate until,
			in CKIndefiniteDelayingTimer.CompletionCallback onComplete = null
		) {
			ICKTimer timer = new CKIndefiniteDelayingTimer(
				startTime: Time.time,
				onComplete: onComplete,
				completionPredicate: until
			);
			return StartTimer(queue, timer);
		}

		public static CKKey Delay(
			CKQueue queue,
			in CKIndefiniteDelayingTimer.SimpleCompletionPredicate until,
			in CKIndefiniteDelayingTimer.SimpleCompletionCallback onComplete = null
		) {
			ICKTimer timer = new CKIndefiniteDelayingTimer(
				startTime: Time.time,
				onComplete: onComplete,
				completionPredicate: until
			);
			return StartTimer(queue, timer);
		}

		public static CKKey Delay(
			in CKIndefiniteDelayingTimer.CompletionPredicate until,
			in CKIndefiniteDelayingTimer.CompletionCallback onComplete = null
		)
			=> Delay(CKQueue.Default, until, onComplete);
	}
}