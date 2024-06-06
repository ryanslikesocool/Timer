// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using System;
using System.Linq;
using System.Collections.Generic;
using Foundation;

namespace ClockKit {
	internal sealed class CKUpdateQueue {
		public readonly CKQueue Queue;

		private Dictionary<CKKey, ICKTimer> timers;

		private Dictionary<CKKey, ICKUpdateDelegate> updateDelegates;
		private List<UpdateDelegateOrder> updateDelegateOrder;

		private List<InsertingUpdateDelegate> insertingUpdateDelegates;
		private List<CKKey> removingUpdateDelegates;

		private float localTime;
		private float previousTime;
		private float deltaTime;
		private uint updateCount;

		private CKKey currentTimerKey;
		private CKKey currentUpdateDelegateKey;

		public bool IsEmpty => TimerCount == 0 && UpdateDelegateCount == 0;

		public int TimerCount => timers.Count;
		public int UpdateDelegateCount => updateDelegates.Count;

		// MARK: - Lifecycle

		public CKUpdateQueue(CKQueue queue, float currentTime) {
			this.Queue = queue;

			this.previousTime = currentTime;
			this.deltaTime = 0;
			this.updateCount = 0;

			this.timers = new Dictionary<CKKey, ICKTimer>();
			this.updateDelegates = new Dictionary<CKKey, ICKUpdateDelegate>();
			this.updateDelegateOrder = new List<UpdateDelegateOrder>();

			this.insertingUpdateDelegates = new List<InsertingUpdateDelegate>();
			this.removingUpdateDelegates = new List<CKKey>();

			this.currentTimerKey = new CKKey(queue, CKKeyAssociation.Timer, 0);
			this.currentUpdateDelegateKey = new CKKey(queue, CKKeyAssociation.UpdateDelegate, 0);
		}

		~CKUpdateQueue() {
			this.previousTime = 0;
			this.deltaTime = 0;
			this.updateCount = 0;

			timers.Clear();
			updateDelegates.Clear();
			updateDelegateOrder.Clear();
			insertingUpdateDelegates.Clear();
			removingUpdateDelegates.Clear();

			timers = null;
			updateDelegates = null;
			updateDelegateOrder = null;
			insertingUpdateDelegates = null;
			removingUpdateDelegates = null;
		}

		public void Update(float currentTime) {
			deltaTime = currentTime - previousTime;
			localTime = currentTime;
			previousTime = currentTime;
			updateCount++;

			if (!IsEmpty) {
				CKInstant instant = new CKInstant(
					queue: Queue,
					localTime: localTime,
					deltaTime: deltaTime,
					updateCount: updateCount
				);

				if (updateDelegateOrder.Count > 0) {
					foreach (UpdateDelegateOrder order in updateDelegateOrder) {
						updateDelegates[order.key].OnUpdate(instant);
					}
				}

				if (timers.Count > 0) {
					CKKey[] timerKeys = timers.Keys.ToArray();
					foreach (CKKey key in timerKeys) {
						if (timers.ContainsKey(key)) {
							bool isComplete = timers[key].OnUpdate(instant);
							if (isComplete) {
								StopTimer(key);
							}
						}
					}
				}
			}

			FinalizeUpdateDelegateInsertion();
			FinalizeUpdateDelegateRemoval();
		}

		// MARK: - Utility

		private void FinalizeUpdateDelegateInsertion() {
			if (insertingUpdateDelegates.IsEmpty()) {
				return;
			}

			foreach (InsertingUpdateDelegate inserting in insertingUpdateDelegates) {
				InsertUpdateDelegate(inserting);
			}
			insertingUpdateDelegates.Clear();

			ValidateUpdateDelegateOrder();

			void InsertUpdateDelegate(in InsertingUpdateDelegate inserting) {
				updateDelegates.Add(inserting.key, inserting.updateDelegate);
				updateDelegateOrder.Add(new UpdateDelegateOrder(inserting.priority, inserting.key));
			}
		}

		private void FinalizeUpdateDelegateRemoval() {
			if (removingUpdateDelegates.IsEmpty()) {
				return;
			}

			foreach (CKKey key in removingUpdateDelegates) {
				RemoveUpdateDelegate(key);
			}
			removingUpdateDelegates.Clear();

			ValidateUpdateDelegateOrder();

			void RemoveUpdateDelegate(CKKey key) {
				updateDelegates.Remove(key);
				if (updateDelegateOrder.FirstIndex(pair => pair.key == key).TryGetValue(out int index)) {
					updateDelegateOrder.RemoveAt(index);
				}
			}
		}

		private void ValidateUpdateDelegateOrder() {
			updateDelegateOrder.Sort(new Comparison<UpdateDelegateOrder>((i1, i2) => i2.priority.CompareTo(i1.priority)));
		}

		// MARK: - Delegates

		public CKKey AddUpdateDelegate(int priority, in ICKUpdateDelegate updateDelegate) {
			CKKey key = RetrieveNextUpdateDelegateKey();
			insertingUpdateDelegates.Add(new InsertingUpdateDelegate(priority, key, updateDelegate));
			return key;
		}

		public bool HasUpdateDelegate(in CKKey key) {
			if (!IsKeyValid(key, CKKeyAssociation.UpdateDelegate)) {
				return false;
			}
			return updateDelegates.ContainsKey(key);
		}

		public bool RemoveUpdateDelegate(in CKKey key) {
			if (!HasUpdateDelegate(key)) {
				return false;
			}

			removingUpdateDelegates.Add(key);
			return true;
		}

		public void RemoveAllUpdateDelegates() {
			foreach (CKKey key in updateDelegates.Keys) {
				RemoveUpdateDelegate(key);
			}
		}

		// MARK: - Timers

		public CKKey StartTimer(in ICKTimer timer) {
			CKKey key = RetrieveNextTimerKey();
			timers.Add(key, timer);
			return key;
		}

		public bool HasTimer(in CKKey key) {
			if (!IsKeyValid(key, CKKeyAssociation.Timer)) {
				return false;
			}
			return timers.ContainsKey(key);
		}

		public bool StopTimer(in CKKey key) {
			if (!HasTimer(key)) {
				return false;
			}

			timers.Remove(key);
			return true;
		}

		public void StopAllTimers() {
			CKKey[] timerKeys = timers.Keys.ToArray();
			foreach (CKKey key in timerKeys) {
				StopTimer(key);
			}
		}

		// MARK: - Keys

		/// <summary>
		/// Is the given key supported on this queue, and does it match the given association?
		/// </summary>
		private bool IsKeyValid(in CKKey key, CKKeyAssociation association)
			=> key.queue == Queue && key.association == association;

		private CKKey RetrieveNextTimerKey() {
			CKKey resultKey = currentTimerKey;

			do {
				resultKey += 1;

				if (currentTimerKey == resultKey) {
					throw new ArgumentOutOfRangeException($"This Queue ('{Queue}') cannot accommodate any more timers.  All ${((ulong)uint.MaxValue) + 1} timer keys are occupied.");
				}
			} while (timers.ContainsKey(resultKey));

			currentTimerKey = resultKey;
			return resultKey;
		}

		private CKKey RetrieveNextUpdateDelegateKey() {
			CKKey resultKey = currentUpdateDelegateKey;

			do {
				resultKey += 1;

				if (currentUpdateDelegateKey == resultKey) {
					throw new ArgumentOutOfRangeException($"This Queue ('{Queue}') cannot accommodate any more update delegates.  All ${((ulong)uint.MaxValue) + 1} update delegate keys are occupied.");
				}
			} while (updateDelegates.ContainsKey(resultKey));

			currentUpdateDelegateKey = resultKey;
			return resultKey;
		}

		// MARK: - Supporting Data

		private readonly struct UpdateDelegateOrder : IComparable<UpdateDelegateOrder> {
			public readonly int priority;
			public readonly CKKey key;

			public UpdateDelegateOrder(int priority, in CKKey key) {
				this.priority = priority;
				this.key = key;
			}

			public readonly int CompareTo(UpdateDelegateOrder other)
				=> this.priority.CompareTo(other.priority);
		}

		private readonly struct InsertingUpdateDelegate {
			public readonly int priority;
			public readonly CKKey key;
			public readonly ICKUpdateDelegate updateDelegate;

			public InsertingUpdateDelegate(int priority, in CKKey key, in ICKUpdateDelegate updateDelegate) {
				this.priority = priority;
				this.key = key;
				this.updateDelegate = updateDelegate;
			}
		}
	}
}