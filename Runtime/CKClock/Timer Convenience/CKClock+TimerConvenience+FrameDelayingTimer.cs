// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using UnityEngine;

namespace ClockKit {
	public static partial class CKClock {
		public static CKKey Delay(
			CKQueue queue,
			int frames,
			in CKFrameDelayingTimer.CompletionCallback onComplete
		) {
			ICKTimer timer = new CKFrameDelayingTimer(
				startTime: Time.time,
				frames: frames,
				onComplete: onComplete
			);
			return StartTimer(queue, timer);
		}

		public static CKKey Delay(
			CKQueue queue,
			int frames,
			in CKFrameDelayingTimer.SimpleCompletionCallback onComplete
		) {
			ICKTimer timer = new CKFrameDelayingTimer(
				startTime: Time.time,
				frames: frames,
				onComplete: onComplete
			);
			return StartTimer(queue, timer);
		}

		public static CKKey Delay(
			int frames,
			in CKFrameDelayingTimer.CompletionCallback onComplete
		)
			=> Delay(CKQueue.Default, frames, onComplete);

		public static CKKey Delay(
			int frames,
			in CKFrameDelayingTimer.SimpleCompletionCallback onComplete
		)
			=> Delay(CKQueue.Default, frames, onComplete);

		// MARK: Single Frame

		/// <summary>
		/// Delays a function call for a single update loop (normally a single frame).
		/// </summary>
		/// <param name="queue">The queue to delay on.</param>
		/// <param name="onComplete">The function to call when the timer is complete.</param>
		/// <returns>The timer key.</returns>
		public static CKKey Delay(
			CKQueue queue,
			in CKFrameDelayingTimer.CompletionCallback onComplete
		)
			=> Delay(queue, frames: 1, onComplete);

		/// <summary>
		/// Delays a function call for a single update loop (normally a single frame).
		/// </summary>
		/// <param name="queue">The queue to delay on.</param>
		/// <param name="onComplete">The function to call when the timer is complete.</param>
		/// <returns>The timer key.</returns>
		public static CKKey Delay(
			CKQueue queue,
			in CKFrameDelayingTimer.SimpleCompletionCallback onComplete
		)
			=> Delay(queue, frames: 1, onComplete);

		/// <summary>
		/// Delays a function call for a single update loop (normally a single frame).
		/// </summary>
		/// <param name="onComplete">The function to call when the timer is complete.</param>
		/// <returns>The timer key.</returns>
		public static CKKey Delay(
			in CKFrameDelayingTimer.CompletionCallback onComplete
		)
			=> Delay(CKQueue.Default, frames: 1, onComplete);

		/// <summary>
		/// Delays a function call for a single update loop (normally a single frame).
		/// </summary>
		/// <param name="onComplete">The function to call when the timer is complete.</param>
		/// <returns>The timer key.</returns>
		public static CKKey Delay(
			in CKFrameDelayingTimer.SimpleCompletionCallback onComplete
		)
			=> Delay(CKQueue.Default, frames: 1, onComplete);
	}
}