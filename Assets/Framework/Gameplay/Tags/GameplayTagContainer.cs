using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework.Gameplay.Tags {
    [Serializable]
    public class GameplayTagContainer : IEnumerable<GameplayTag> {
        [SerializeField]
        private List<GameplayTag> tags = new();

        public IEnumerator<GameplayTag> GetEnumerator() {
            return tags.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }
}