using System;
using System.Collections.Generic;

namespace Framework.Gameplay.Tags {
    [Serializable]
    public class GameplayTag {
        public string name;
        public List<GameplayTag> children = new();
        
        public GameplayTag(string name) {
            this.name = name;
        }
    }
}