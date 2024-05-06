// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using UnityEngine;

namespace ClockKit {
	public static partial class CKClock {
		/// <summary>
		/// Delay a function call for a number of seconds and repeat a set number of times.
		/// </summary>
		/// <param name="queue">The queue to delay on.</param>
		/// <param name="seconds">The duration to delay for.</param>
		/// <param name="repeatCount">The number of times to run the timer.</param>
		/// <param name="onComplete">The function to call when the timer is complete.</param>
		/// <returns>The timer key.</returns>
		public static CKKey Delay(
			CKQueue queue,
			float seconds,
			int repeatCount,
			in CKFiniteRepeatingDelayingTimer.CompletionCallback onComplete
		) {
			ICKTimer timer = new CKFiniteRepeatingDelayingTimer(
				startTime: Time.time,
				duration: seconds,
				repeatCount: repeatCount,
				onComplete: onComplete
			);
			return StartTimer(queue, timer);
		}

		/// <summary>
		/// Delay a function call for a number of seconds and repeat a set number of times.
		/// </summary>
		/// <param name="seconds">The duration to delay for.</param>
		/// <param name="repeatCount">The number of times to run the timer.</param>
		/// <param name="onComplete">The function to call when the timer is complete.</param>
		/// <returns>The timer key.</returns>
		public static CKKey Delay(
			float seconds,
			int repeatCount,
			in CKFiniteRepeatingDelayingTimer.CompletionCallback onComplete
		)
			=> Delay(CKQueue.Default, seconds, repeatCount, onComplete);

		/// <summary>
		/// Delay a function call for a number of seconds and repeat a set number of times.
		/// </summary>
		/// <param name="queue">The queue to delay on.</param>
		/// <param name="seconds">The duration to delay for.</param>
		/// <param name="repeatCount">The number of times to run the timer.</param>
		/// <param name="onComplete">The function to call when the timer is complete.</param>
		/// <returns>The timer key.</returns>
		public static CKKey Delay(
			CKQueue queue,
			float seconds,
			int repeatCount,
			in CKFiniteRepeatingDelayingTimer.SimpleCompletionCallback onComplete
		) {
			ICKTimer timer = new CKFiniteRepeatingDelayingTimer(
				startTime: Time.time,
				duration: seconds,
				repeatCount: repeatCount,
				onComplete: onComplete
			);
			return StartTimer(queue, timer);
		}

		/// <summary>
		/// Delay a function call for a number of seconds and repeat a set number of times.
		/// </summary>
		/// <param name="seconds">The duration to delay for.</param>
		/// <param name="repeatCount">The number of times to run the timer.</param>
		/// <param name="onComplete">The function to call when the timer is complete.</param>
		/// <returns>The timer key.</returns>
		public static CKKey Delay(
			float seconds,
			int repeatCount,
			in CKFiniteRepeatingDelayingTimer.SimpleCompletionCallback onComplete
		)
			=> Delay(CKQueue.Default, seconds, repeatCount, onComplete);
	}
}