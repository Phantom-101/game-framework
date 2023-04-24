using System;

namespace Framework.Gameplay.Effects {
    [Serializable]
    public class ValueVersion {
        public int version;
        public float value;

        public ValueVersion(int version, float value) {
            this.version = version;
            this.value = value;
        }
    }
}