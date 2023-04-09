#nullable enable
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Framework.Persistence {
    public class PersistentObject : MonoBehaviour, IPersistent {
        public virtual PersistentData WritePersistentData(PersistentData data, PersistentSerializer serializer) {
            data.Add("name", gameObject.name);
            var t = transform;
            data.Add("position", Vector3ToFloatArray(t.localPosition));
            data.Add("rotation", Vector3ToFloatArray(t.localEulerAngles));
            return data;
        }

        public virtual async UniTask ReadPersistentData(PersistentData data, PersistentSerializer serializer) {
            gameObject.name = data.Get<string>("name");
            var t = transform;
            t.localPosition = FloatArrayToVector3(data.Get<float[]>("position"));
            t.localEulerAngles = FloatArrayToVector3(data.Get<float[]>("rotation"));
        }

        private static float[] Vector3ToFloatArray(Vector3 v) {
            return new[] { v.x, v.y, v.z };
        }

        private static Vector3 FloatArrayToVector3(IReadOnlyList<float> a) {
            return new Vector3(a[0], a[1], a[2]);
        }
    }
}