using System;
using System.Collections.Generic;

namespace Framework.Gameplay.Effects {
    [Serializable]
    public class VersionedValue {
        /// <summary>
        /// Record of all value versions. Versions are ordered by increasing version/priority.
        /// </summary>
        public LinkedList<ValueVersion> values = new();

        public VersionedValue(float baseValue) {
            values.AddFirst(new ValueVersion(int.MinValue, baseValue));
        }
        
        /// <summary>
        /// Get the first version of the value.
        /// </summary>
        /// <returns></returns>
        public float GetBaseValue() {
            return values.First.Value.value;
        }

        /// <summary>
        /// Get the latest version of the value.
        /// </summary>
        /// <returns></returns>
        public float GetLatestValue() {
            return values.Last.Value.value;
        }

        /// <summary>
        /// Get the value at the provided version. If no such version exists, the next lowest version will be used.
        /// </summary>
        /// <param name="version">The version at which the value should be retrieved</param>
        /// <returns></returns>
        public float GetValueAtVersion(int version) {
            var current = values.Last;
            while (current!.Value.version > version) {
                current = current.Previous;
            }

            return current.Value.value;
        }

        /// <summary>
        /// Record a new version of the value. If the version already exists, then the value of that version will be overridden.
        /// </summary>
        /// <param name="version">Version of the provided value</param>
        /// <param name="target">Target that the value has changed to</param>
        public void ModifyValue(int version, float target) {
            var current = values.Last;
            while (current!.Value.version > version) {
                current = current.Previous;
            }

            if (current.Value.version == version) {
                current.Value.value = target;
            } else {
                values.AddAfter(current, new ValueVersion(version, target));
            }
        }
    }
}