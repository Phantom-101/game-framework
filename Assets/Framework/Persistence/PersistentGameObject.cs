#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using UnityEngine;

namespace Framework.Persistence {
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class PersistentGameObject : MonoBehaviour {
        [SerializeField]
        [JsonProperty]
        private new string name = string.Empty;

        [SerializeField]
        [JsonProperty]
        private float[] position = Array.Empty<float>();

        [SerializeField]
        [JsonProperty]
        private float[] rotation = Array.Empty<float>();

        [SerializeField]
        [JsonProperty]
        private List<Component> components = new();
        
        private GameObjectContext? _context;

        [OnSerializing]
        private void OnSerializing(StreamingContext context) {
            name = gameObject.name;
            position = Vector3ToFloatArray(transform.localPosition);
            rotation = Vector3ToFloatArray(transform.localEulerAngles);
            components = GetComponents<Component>().Where(e => e != this && e.GetType().GetCustomAttributes(true).Any(attr => attr is JsonObjectAttribute)).ToList();
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context) {
            _context?.Pop();
            gameObject.name = name;
            transform.localPosition = FloatArrayToVector3(position);
            transform.localEulerAngles = FloatArrayToVector3(rotation);
        }
        
        private static float[] Vector3ToFloatArray(Vector3 v) {
            return new[] { v.x, v.y, v.z };
        }

        private static Vector3 FloatArrayToVector3(IReadOnlyList<float> a) {
            return new Vector3(a[0], a[1], a[2]);
        }
    }
}