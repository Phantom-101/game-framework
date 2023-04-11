#nullable enable
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Framework.Gameplay.Effects;
using Framework.Persistence;
using Framework.Persistence.Intermediate;
using UnityEngine;

namespace Framework.Gameplay {
    public class GameplayObject : PersistentObject {
        [SerializeReference]
        private List<GameplayEffect> effects = new();

        private void Update() {
            foreach (var effect in effects) {
                effect.OnTick(this, Time.deltaTime);
            }
        }
        
        public void AddEffect(GameplayEffect effect) {
            if (effect.CanApply(this)) {
                effects.Add(effect);
                effect.OnActivate(this);
            }
        }

        public void RemoveEffect(GameplayEffect effect) {
            if (effects.Remove(effect)) {
                effect.OnDeactivate(this);
            }
        }

        public override PersistentData WritePersistentData(PersistentData data, PersistentSerializer serializer) {
            base.WritePersistentData(data, serializer);
            data.Add("effects", effects.ConvertAll(serializer.Save));
            return data;
        }

        public override async UniTask ReadPersistentData(PersistentData data, PersistentSerializer serializer) {
            await base.ReadPersistentData(data, serializer);
            var effectData = data.Get<List<PersistentData>>("effects");
            foreach (var effect in effectData) {
                effects.Add((GameplayEffect)(await serializer.Load(effect))!);
            }
        }
    }
}