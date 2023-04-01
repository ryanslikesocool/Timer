#if EASEKIT_3_1
using EaseKit;

namespace ClockKit {
    /// <summary>
    /// An animation that evaluates a <see cref="Spring"/> over time.
    /// <br/>
    /// For advanced Spring evaulation, see <see cref="SpringTimer"/>.
    /// </summary>
    /// <seealso cref="Spring"/>
    /// <seealso cref="SpringTimer"/>
    public struct SpringAnimation<Value> : ICompletableAnimation<Value> {
        public readonly Value start;
        public readonly Value end;
        private readonly IInterpolator<Value> interpolator;
        private readonly Spring.Solver solver;
        private Spring.Solver.State state;

        public bool IsComplete => state.IsComplete;

        public SpringAnimation(
            in Value start,
            in Value end
        ) : this(Spring.Default, start, end) { }

        public SpringAnimation(
            float initialVelocity,
            in Value start,
            in Value end
        ) : this(Spring.Default, initialVelocity, start, end) { }

        public SpringAnimation(
            in Spring spring,
            in Value start,
            in Value end
        ) : this(spring, EasingUtility.CreateInterpolator<Value>(), start, end) { }

        public SpringAnimation(
            in Spring spring,
            float initialVelocity,
            in Value start,
            in Value end
        ) : this(spring.CreateSolver(), initialVelocity, start, end) { }

        public SpringAnimation(
            in Spring spring,
            in IInterpolator<Value> interpolator,
            in Value start,
            in Value end
        ) : this(spring.CreateSolver(), interpolator, start, end) { }

        public SpringAnimation(
            in Spring.Solver solver,
            in IInterpolator<Value> interpolator,
            in Value start,
            in Value end
        ) : this(solver, solver.CreateState(0), interpolator, start, end) { }

        public SpringAnimation(
            in Spring.Solver solver,
            float initialVelocity,
            in Value start,
            in Value end
        ) : this(solver, solver.CreateState(initialVelocity), EasingUtility.CreateInterpolator<Value>(), start, end) { }

        public SpringAnimation(
            in Spring.Solver solver,
            in Spring.Solver.State state,
            in Value start,
            in Value end
        ) : this(solver, state, EasingUtility.CreateInterpolator<Value>(), start, end) { }

        public SpringAnimation(
            in Spring.Solver solver,
            in Spring.Solver.State state,
            in IInterpolator<Value> interpolator,
            in Value start,
            in Value end
        ) {
            this.start = start;
            this.end = end;
            this.interpolator = interpolator;
            this.solver = solver;
            this.state = state;
        }

        public Value Evaluate(float localTime, float time) {
            float springValue = solver.Evaluate(time, ref state);
            return interpolator.Evaluate(start, end, springValue);
        }
    }
}
#endif