// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

#if UNITY_MATHEMATICS
using Unity.Mathematics;
#else
using UnityEngine;
#endif

namespace ClockKit {
	public struct CKFiniteAnimationUpdatingTimer<Value, Animation> : ICKFiniteTimer where Animation : ICKFiniteAnimation<Value> {
		public delegate void UpdateCallback(Value value);
		public delegate void CompletionCallback(Value value);
		public delegate void SimpleCompletionCallback();

		public float StartTime { get; }
		public readonly float Duration => animation.Duration;

		public readonly Animation animation;
		public readonly UpdateCallback onUpdate;
		public readonly CompletionCallback onComplete;

		public bool IsComplete { get; private set; }

		public CKFiniteAnimationUpdatingTimer(float startTime, Animation animation, UpdateCallback onUpdate, CompletionCallback onComplete) {
			this.StartTime = startTime;
			this.animation = animation;
			this.onUpdate = onUpdate;
			this.onComplete = onComplete;
			IsComplete = false;
		}

		public CKFiniteAnimationUpdatingTimer(float startTime, Animation animation, UpdateCallback onUpdate, SimpleCompletionCallback onComplete) : this(
			startTime: startTime,
			animation: animation,
			onUpdate: onUpdate,
			onComplete: (Value _) => onComplete?.Invoke()
		) { }

		public bool OnUpdate(in CKInstant instant) {
			if (IsComplete) {
				return true;
			}

			float localTime = instant.localTime - StartTime;
			float percent = localTime / Duration;
			percent =
#if UNITY_MATHEMATICS
				math.saturate(percent);
#else
            	Mathf.Clamp(percent, 0.0f, 1.0f);
#endif
			Value value = animation.Evaluate(localTime, percent);

			onUpdate(value);

			IsComplete = percent >= 1;
			if (IsComplete) {
				onComplete?.Invoke(value);
			}
			return IsComplete;
		}
	}
}