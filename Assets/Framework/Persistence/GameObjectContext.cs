#nullable enable
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Framework.Persistence {
    public class GameObjectContext {
        private readonly Stack<GameObject> _context = new();
        private readonly HashSet<Component> _loaded = new();

        public void Push(GameObject context) {
            _context.Push(context);
        }

        public void Pop() {
            _context.Pop();
        }

        public Component GetComponent(Type type) {
            foreach (var target in _context.Peek().GetComponents(type)) {
                if (target.GetType() == type && !_loaded.Contains(target)) {
                    _loaded.Add(target);
                    return target;
                }
            }

            var ret = _context.Peek().AddComponent(type);
            _loaded.Add(ret);
            return ret;
        }
    }
}