using System;

namespace ClockKit {
    public struct CKFinitePausableDelayingTimer : ICKFiniteTimer {
        public delegate bool PauseCheck();
        public delegate void CompletionCallback();

        public float StartTime { get; private set; }
        public float Duration { get; }

        public readonly PauseCheck isPaused;
        public readonly CompletionCallback onComplete;

        public bool IsComplete { get; private set; }

        public CKFinitePausableDelayingTimer(float startTime, float duration, PauseCheck isPaused, CompletionCallback onComplete) {
            this.StartTime = startTime;
            this.Duration = duration;
            this.isPaused = isPaused;
            this.onComplete = onComplete;
            this.IsComplete = false;
        }

        public bool OnUpdate(in CKClockInformation information) {
            if (IsComplete) {
                return true;
            }

            if (isPaused()) {
                StartTime += information.deltaTime;
                return false;
            }

            float localTime = information.time - StartTime;

            IsComplete = localTime >= Duration;
            if (IsComplete) {
                onComplete?.Invoke();
            }
            return IsComplete;
        }
    }
}