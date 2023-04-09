#nullable enable
using Framework.Variables;
using UnityEngine;

namespace Framework.Gameplay.Tags {
    [CreateAssetMenu(menuName = "Gameplay/Tag", fileName = "NewGameplayTag")]
    public class GameplayTagVariable : ScriptableVariable<GameplayTag> {
        public GameplayTagSet tagSet = null!;
        public string path = string.Empty;
        
        public override GameplayTag Value {
            get => _value ??= tagSet.GetOrCreate(path);
            set => _value = value;
        }

        private GameplayTag? _value;
    }
}