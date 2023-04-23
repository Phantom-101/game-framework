#nullable enable
using System;
using Newtonsoft.Json;

namespace Framework.Gameplay.Tags {
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class GameplayTag {
        [JsonProperty]
        public string path;

        private const char Separator = '.';
        
        public GameplayTag(string path) {
            this.path = path;
        }

        public GameplayTag(string name, GameplayTag parent) {
            path = parent.path + Separator + name;
        }

        public bool IsAncestorOf(GameplayTag other) {
            return other.path.IndexOf(path, StringComparison.Ordinal) == 0 && other.path.Remove(0, path.Length + 1).StartsWith(Separator);
        }

        public bool IsDescendentOf(GameplayTag other) {
            return other.IsAncestorOf(this);
        }

        public bool IsParentOf(GameplayTag other) {
            return IsAncestorOf(other) && !other.path.Remove(0, path.Length + 1).Contains(Separator);
        }

        public bool IsChildOf(GameplayTag other) {
            return other.IsParentOf(this);
        }
    }
}