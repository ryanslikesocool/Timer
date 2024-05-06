// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

namespace ClockKit {
	public struct CKFiniteRepeatingDelayingTimer : ICKFiniteTimer {
		public delegate void CompletionCallback(uint runCount);
		public delegate void SimpleCompletionCallback();

		public float StartTime { get; private set; }
		public float Duration { get; }

		public readonly CompletionCallback onComplete;

		public readonly int targetRunCount;

		private uint runCount;

		public bool IsComplete { get; private set; }

		/// <summary>
		/// Create a new repeating timer.
		/// </summary>
		/// <param name="startTime">The timer's reference time.</param>
		/// <param name="duration">The duration in seconds to delay the complection call for.</param>
		/// <param name="repeatCount">The number of times to run the timer.  Pass <see langword="null"/> to run infinitely.</param>
		/// <param name="onComplete">The code to execute upon timer completion.  The parameter is the number of times the timer has run.</param>
		public CKFiniteRepeatingDelayingTimer(float startTime, float duration, int repeatCount, CompletionCallback onComplete) {
			this.StartTime = startTime;
			this.Duration = duration;
			this.onComplete = onComplete;
			this.IsComplete = false;
			this.targetRunCount = repeatCount;
			runCount = 0;
		}

		/// <summary>
		/// Create a new repeating timer.
		/// </summary>
		/// <param name="startTime">The timer's reference time.</param>
		/// <param name="duration">The duration in seconds to delay the complection call for.</param>
		/// <param name="repeatCount">The number of times to repeat the timer.  Pass <see langword="null"/> to repeat infinitely.</param>
		/// <param name="onComplete">The code to execute upon timer completion.</param>
		public CKFiniteRepeatingDelayingTimer(float startTime, float duration, int repeatCount, SimpleCompletionCallback onComplete) :
			this(startTime, duration, repeatCount, (_) => { onComplete(); }) { }

		public bool OnUpdate(in CKInstant instant) {
			if (IsComplete) {
				return true;
			}

			float localTime = instant.localTime - StartTime;

			IsComplete = localTime >= Duration;
			if (IsComplete) {
				runCount++;
				if (runCount < targetRunCount) {
					StartTime += Duration;
					IsComplete = false;
				}

				onComplete?.Invoke(runCount);
			}
			return IsComplete;
		}
	}
}