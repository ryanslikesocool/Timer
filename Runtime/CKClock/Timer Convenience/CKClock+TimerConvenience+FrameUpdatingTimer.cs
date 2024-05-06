// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using UnityEngine;

namespace ClockKit {
	public static partial class CKClock {
		public static CKKey Update(
			CKQueue queue,
			int frames,
			in CKFrameUpdatingTimer.UpdateCallback onUpdate,
			in CKFrameUpdatingTimer.CompletionCallback onComplete = null
		) {
			ICKTimer timer = new CKFrameUpdatingTimer(
				startTime: Time.time,
				frames: frames,
				onUpdate: onUpdate,
				onComplete: onComplete
			);
			return StartTimer(queue, timer);
		}

		public static CKKey Update(
			CKQueue queue,
			int frames,
			in CKFrameUpdatingTimer.UpdateCallback onUpdate,
			in CKFrameUpdatingTimer.SimpleCompletionCallback onComplete
		) {
			ICKTimer timer = new CKFrameUpdatingTimer(
				startTime: Time.time,
				frames: frames,
				onUpdate: onUpdate,
				onComplete: onComplete
			);
			return StartTimer(queue, timer);
		}

		public static CKKey Update(
			int frames,
			in CKFrameUpdatingTimer.UpdateCallback onUpdate,
			in CKFrameUpdatingTimer.CompletionCallback onComplete = null
		)
			=> Update(CKQueue.Default, frames: frames, onUpdate, onComplete);

		public static CKKey Update(
			int frames,
			in CKFrameUpdatingTimer.UpdateCallback onUpdate,
			in CKFrameUpdatingTimer.SimpleCompletionCallback onComplete
		)
			=> Update(CKQueue.Default, frames: frames, onUpdate, onComplete);
	}
}