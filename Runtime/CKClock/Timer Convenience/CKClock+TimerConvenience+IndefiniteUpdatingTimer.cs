// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using UnityEngine;

namespace ClockKit {
	public static partial class CKClock {
		public static CKKey Update(
			CKQueue queue,
			in CKIndefiniteUpdatingTimer.CompletionPredicate until,
			in CKIndefiniteUpdatingTimer.UpdateCallback onUpdate,
			in CKIndefiniteUpdatingTimer.CompletionCallback onComplete = null
		) {
			ICKTimer timer = new CKIndefiniteUpdatingTimer(
				startTime: Time.time,
				onUpdate: onUpdate,
				onComplete: onComplete,
				completionPredicate: until
			);
			return StartTimer(queue, timer);
		}

		public static CKKey Update(
			CKQueue queue,
			in CKIndefiniteUpdatingTimer.SimpleCompletionPredicate until,
			in CKIndefiniteUpdatingTimer.UpdateCallback onUpdate,
			in CKIndefiniteUpdatingTimer.CompletionCallback onComplete = null
		) {
			ICKTimer timer = new CKIndefiniteUpdatingTimer(
				startTime: Time.time,
				onUpdate: onUpdate,
				onComplete: onComplete,
				completionPredicate: until
			);
			return StartTimer(queue, timer);
		}

		public static CKKey Update(
			CKQueue queue,
			in CKIndefiniteUpdatingTimer.CompletionPredicate until,
			in CKIndefiniteUpdatingTimer.UpdateCallback onUpdate,
			in CKIndefiniteUpdatingTimer.SimpleCompletionCallback onComplete = null
		) {
			ICKTimer timer = new CKIndefiniteUpdatingTimer(
				startTime: Time.time,
				onUpdate: onUpdate,
				onComplete: onComplete,
				completionPredicate: until
			);
			return StartTimer(queue, timer);
		}

		public static CKKey Update(
			CKQueue queue,
			in CKIndefiniteUpdatingTimer.SimpleCompletionPredicate until,
			in CKIndefiniteUpdatingTimer.UpdateCallback onUpdate,
			in CKIndefiniteUpdatingTimer.SimpleCompletionCallback onComplete = null
		) {
			ICKTimer timer = new CKIndefiniteUpdatingTimer(
				startTime: Time.time,
				onUpdate: onUpdate,
				onComplete: onComplete,
				completionPredicate: until
			);
			return StartTimer(queue, timer);
		}

		public static CKKey Update(
			in CKIndefiniteUpdatingTimer.CompletionPredicate until,
			in CKIndefiniteUpdatingTimer.UpdateCallback onUpdate,
			in CKIndefiniteUpdatingTimer.CompletionCallback onComplete = null
		)
			=> Update(CKQueue.Default, until, onUpdate, onComplete);

		public static CKKey Update(
			in CKIndefiniteUpdatingTimer.CompletionPredicate until,
			in CKIndefiniteUpdatingTimer.UpdateCallback onUpdate,
			in CKIndefiniteUpdatingTimer.SimpleCompletionCallback onComplete = null
		)
			=> Update(CKQueue.Default, until, onUpdate, onComplete);

		public static CKKey Update(
			in CKIndefiniteUpdatingTimer.SimpleCompletionPredicate until,
			in CKIndefiniteUpdatingTimer.UpdateCallback onUpdate,
			in CKIndefiniteUpdatingTimer.CompletionCallback onComplete = null
		)
			=> Update(CKQueue.Default, until, onUpdate, onComplete);

		public static CKKey Update(
			in CKIndefiniteUpdatingTimer.SimpleCompletionPredicate until,
			in CKIndefiniteUpdatingTimer.UpdateCallback onUpdate,
			in CKIndefiniteUpdatingTimer.SimpleCompletionCallback onComplete = null
		)
			=> Update(CKQueue.Default, until, onUpdate, onComplete);
	}
}