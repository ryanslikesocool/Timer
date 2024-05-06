// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using UnityEngine;
using System;

namespace ClockKit {
	public static partial class CKClock {
		public static CKKey Sequence(
			CKQueue queue,
			in Func<ICKTimer>[] timerBuilders,
			in CKSequenceTimer.CompletionCallback onComplete = null
		) {
			ICKTimer timer = new CKSequenceTimer(
				startTime: Time.time,
				timerBuilders: timerBuilders,
				onComplete: onComplete
			);
			return StartTimer(queue, timer);
		}

		public static CKKey Sequence(
			in Func<ICKTimer>[] timerBuilders,
			in CKSequenceTimer.CompletionCallback onComplete = null
		)
			=> Sequence(CKQueue.Default, timerBuilders, onComplete);
	}
}