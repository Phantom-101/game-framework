#nullable enable
using System.Linq;
using Framework.Variables;
using UnityEngine;

namespace Framework.Tags {
    [CreateAssetMenu(menuName = "Gameplay/TagSet", fileName = "NewGameplayTagSet")]
    public class GameplayTagSet : SetVariable<GameplayTag> {
        public GameplayTag? Get(string path) {
            return Value.FirstOrDefault(e => e.path == path);
        }
        
        public GameplayTag GetOrCreate(string path) {
            var ret = Get(path);
            if (ret == null) {
                ret = new GameplayTag(path);
                Value.Add(ret);
            }
            
            return ret;
        }
    }
}