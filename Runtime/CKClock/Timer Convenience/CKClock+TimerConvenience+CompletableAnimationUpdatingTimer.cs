// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using UnityEngine;

namespace ClockKit {
	public static partial class CKClock {
		public static CKKey Animate<Value, Animation>(
			CKQueue queue,
			in Animation animation,
			in CKCompletableAnimationUpdatingTimer<Value, Animation>.UpdateCallback onUpdate,
			in CKCompletableAnimationUpdatingTimer<Value, Animation>.CompletionCallback onComplete = null
		) where Animation : ICKCompletableAnimation<Value> {
			ICKTimer timer = new CKCompletableAnimationUpdatingTimer<Value, Animation>(
				startTime: Time.time,
				animation: animation,
				onUpdate: onUpdate,
				onComplete: onComplete
			);
			return StartTimer(queue, timer);
		}

		public static CKKey Animate<Value, Animation>(
			CKQueue queue,
			in Animation animation,
			in CKCompletableAnimationUpdatingTimer<Value, Animation>.UpdateCallback onUpdate,
			in CKCompletableAnimationUpdatingTimer<Value, Animation>.SimpleCompletionCallback onComplete = null
		) where Animation : ICKCompletableAnimation<Value> {
			ICKTimer timer = new CKCompletableAnimationUpdatingTimer<Value, Animation>(
				startTime: Time.time,
				animation: animation,
				onUpdate: onUpdate,
				onComplete: onComplete
			);
			return StartTimer(queue, timer);
		}

		public static CKKey Animate<Value, Animation>(
			in Animation animation,
			in CKCompletableAnimationUpdatingTimer<Value, Animation>.UpdateCallback onUpdate,
			in CKCompletableAnimationUpdatingTimer<Value, Animation>.CompletionCallback onComplete = null
		) where Animation : ICKCompletableAnimation<Value>
			=> Animate(CKQueue.Default, animation, onUpdate, onComplete);

		public static CKKey Animate<Value, Animation>(
			in Animation animation,
			in CKCompletableAnimationUpdatingTimer<Value, Animation>.UpdateCallback onUpdate,
			in CKCompletableAnimationUpdatingTimer<Value, Animation>.SimpleCompletionCallback onComplete = null
		) where Animation : ICKCompletableAnimation<Value>
			=> Animate(CKQueue.Default, animation, onUpdate, onComplete);
	}
}