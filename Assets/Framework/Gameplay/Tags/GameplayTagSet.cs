#nullable enable
using System.Linq;
using Framework.Variables;
using UnityEngine;

namespace Framework.Gameplay.Tags {
    [CreateAssetMenu(menuName = "Gameplay/TagSet", fileName = "NewGameplayTagSet")]
    public class GameplayTagSet : SetVariable<GameplayTag> {
        public GameplayTag? Get(string path) {
            var tags = path.Split(".");
            GameplayTag? current = null;
            foreach (var tag in tags) {
                if (current == null) {
                    var next = Value.FirstOrDefault(e => e.name == tag);
                    if (next == null) {
                        return null;
                    }
                    current = next;
                } else {
                    var next = current.children.FirstOrDefault(e => e.name == tag);
                    if (next == null) {
                        return null;
                    }
                    current = next;
                }
            }
            return current!;
        }
        
        public GameplayTag GetOrCreate(string path) {
            var tags = path.Split(".");
            GameplayTag? current = null;
            foreach (var tag in tags) {
                if (current == null) {
                    var next = Value.FirstOrDefault(e => e.name == tag);
                    if (next == null) {
                        next = new GameplayTag(tag);
                        Value.Add(next);
                    }
                    current = next;
                } else {
                    var next = current.children.FirstOrDefault(e => e.name == tag);
                    if (next == null) {
                        next = new GameplayTag(tag);
                        current.children.Add(next);
                    }
                    current = next;
                }
            }
            return current!;
        }
    }
}