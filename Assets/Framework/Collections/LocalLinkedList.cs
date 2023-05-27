#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Framework.Collections {
    [Serializable]
    public class LocalLinkedList<T> : IEnumerable<T> {
        [SerializeField]
        private Node[] nodes = new Node[1];

        [SerializeField]
        private int count;

        [SerializeField]
        private int firstIndex = -1;
        
        [SerializeField]
        private int lastIndex = -1;

        [SerializeField]
        private int firstEmptyIndex;

        [SerializeField]
        private uint globalVersion;

        public LocalLinkedList() {
            nodes[0].previousIndex = -1;
            nodes[0].nextIndex = -1;
        }

        public int Count => count;
        
        public NodeHandle? GetFirst() {
            if (firstIndex == -1) return null;
            var first = nodes[firstIndex];
            return new NodeHandle(this, first.value, firstIndex, first.version);
        }

        public NodeHandle? GetLast() {
            if (lastIndex == -1) return null;
            var last = nodes[lastIndex];
            return new NodeHandle(this, last.value, lastIndex, last.version);
        }

        public NodeHandle AddFirst(T value) {
            TryExpand();
            if (firstIndex == -1) {
                nodes[firstEmptyIndex].value = value;
                nodes[firstEmptyIndex].version++;
                firstIndex = firstEmptyIndex;
                lastIndex = firstEmptyIndex;
                firstEmptyIndex = nodes[firstEmptyIndex].nextIndex;
                nodes[firstIndex].nextIndex = -1;
            } else {
                nodes[firstEmptyIndex].value = value;
                nodes[firstEmptyIndex].version++;
                var newFirstEmptyIndex = nodes[firstEmptyIndex].nextIndex;
                nodes[firstEmptyIndex].previousIndex = -1;
                nodes[firstEmptyIndex].nextIndex = firstIndex;
                nodes[firstIndex].previousIndex = firstEmptyIndex;
                firstIndex = firstEmptyIndex;
                firstEmptyIndex = newFirstEmptyIndex;
            }
            if (firstEmptyIndex != -1) {
                nodes[firstEmptyIndex].previousIndex = -1;
            }
            globalVersion++;
            count++;
            return new NodeHandle(this, value, firstIndex, nodes[firstIndex].version);
        }

        public NodeHandle AddLast(T value) {
            TryExpand();
            if (lastIndex == -1) {
                nodes[firstEmptyIndex].value = value;
                nodes[firstEmptyIndex].version++;
                firstIndex = firstEmptyIndex;
                lastIndex = firstEmptyIndex;
                firstEmptyIndex = nodes[firstEmptyIndex].nextIndex;
                nodes[lastIndex].nextIndex = -1;
            } else {
                nodes[firstEmptyIndex].value = value;
                nodes[firstEmptyIndex].version++;
                var newFirstEmptyIndex = nodes[firstEmptyIndex].nextIndex;
                nodes[firstEmptyIndex].previousIndex = lastIndex;
                nodes[firstEmptyIndex].nextIndex = -1;
                nodes[lastIndex].nextIndex = firstEmptyIndex;
                lastIndex = firstEmptyIndex;
                firstEmptyIndex = newFirstEmptyIndex;
            }
            if (firstEmptyIndex != -1) {
                nodes[firstEmptyIndex].previousIndex = -1;
            }
            globalVersion++;
            count++;
            return new NodeHandle(this, value, lastIndex, nodes[lastIndex].version);
        }

        public void Clear() {
            for (var i = 0; i < nodes.Length; i++) {
                nodes[i].previousIndex = i - 1;
                nodes[i].nextIndex = i + 1;
                nodes[i].value = default;
                nodes[i].version++;
            }
            nodes[^1].nextIndex = -1;
            firstIndex = -1;
            lastIndex = -1;
            firstEmptyIndex = 0;
            globalVersion++;
            count = 0;
        }
        
        private void TryExpand() {
            if (firstEmptyIndex != -1) return;
            
            var prevLength = nodes.Length;
            var newArray = new Node[prevLength << 1];
            Array.Copy(nodes, newArray, prevLength);
            nodes = newArray;
            for (var i = prevLength; i < nodes.Length; i++) {
                nodes[i].previousIndex = i - 1;
                nodes[i].nextIndex = i + 1;
            }

            nodes[^1].nextIndex = -1;
            nodes[prevLength].previousIndex = -1;
            firstEmptyIndex = prevLength;
        }

        public override string ToString() {
            var ret = $"FI: {firstIndex} LI: {lastIndex} FEI: {firstEmptyIndex} C: {count}\n";
            for (var i = 0; i < nodes.Length; i++) {
                ret += $"{i} | {nodes[i].ToString()}\n";
            }
            return ret;
        }

        public readonly struct NodeHandle {
            public readonly LocalLinkedList<T> list;
            public readonly T? value;

            private readonly int _index;
            private readonly uint _version;

            internal NodeHandle(LocalLinkedList<T> list, T? value, int index, uint version) {
                this.list = list;
                this.value = value;
                _index = index;
                _version = version;
            }

            public bool IsValid() => _version == list.nodes[_index].version;

            public NodeHandle? GetPrevious() {
                if (!IsValid()) throw new ArgumentException("Handle is invalid");
                var cur = list.nodes[_index];
                if (cur.previousIndex == -1) return null;
                var prev = list.nodes[cur.previousIndex];
                return new NodeHandle(list, prev.value, cur.previousIndex, prev.version);
            }

            public NodeHandle? GetNext() {
                if (!IsValid()) throw new ArgumentException("Handle is invalid");
                var cur = list.nodes[_index];
                if (cur.nextIndex == -1) return null;
                var next = list.nodes[cur.nextIndex];
                return new NodeHandle(list, next.value, cur.nextIndex, next.version);
            }

            public NodeHandle InsertBefore(T newValue) {
                if (!IsValid()) throw new ArgumentException("Handle is invalid");
                list.TryExpand();
                list.nodes[list.firstEmptyIndex].value = newValue;
                list.nodes[list.firstEmptyIndex].version++;
                var newFirstEmptyIndex = list.nodes[list.firstEmptyIndex].nextIndex;
                list.nodes[list.firstEmptyIndex].nextIndex = _index;
                if (list.firstIndex == _index) {
                    list.firstIndex = list.firstEmptyIndex;
                    list.nodes[list.firstEmptyIndex].previousIndex = -1;
                } else {
                    list.nodes[list.nodes[_index].previousIndex].nextIndex = list.firstEmptyIndex;
                    list.nodes[list.firstEmptyIndex].previousIndex = list.nodes[_index].previousIndex;
                }
                list.nodes[_index].previousIndex = list.firstEmptyIndex;
                list.firstEmptyIndex = newFirstEmptyIndex;
                if (list.firstEmptyIndex != -1) {
                    list.nodes[list.firstEmptyIndex].previousIndex = -1;
                }
                list.globalVersion++;
                list.count++;
                return new NodeHandle(list, newValue, list.nodes[_index].previousIndex, list.nodes[list.nodes[_index].previousIndex].version);
            }

            public NodeHandle InsertAfter(T newValue) {
                if (!IsValid()) throw new ArgumentException("Handle is invalid");
                list.TryExpand();
                list.nodes[list.firstEmptyIndex].value = newValue;
                list.nodes[list.firstEmptyIndex].version++;
                var newFirstEmptyIndex = list.nodes[list.firstEmptyIndex].nextIndex;
                list.nodes[list.firstEmptyIndex].previousIndex = _index;
                if (list.lastIndex == _index) {
                    list.lastIndex = list.firstEmptyIndex;
                    list.nodes[list.firstEmptyIndex].nextIndex = -1;
                } else {
                    list.nodes[list.nodes[_index].nextIndex].previousIndex = list.firstEmptyIndex;
                    list.nodes[list.firstEmptyIndex].nextIndex = list.nodes[_index].nextIndex;
                }
                list.nodes[_index].nextIndex = list.firstEmptyIndex;
                list.firstEmptyIndex = newFirstEmptyIndex;
                if (list.firstEmptyIndex != -1) {
                    list.nodes[list.firstEmptyIndex].previousIndex = -1;
                }
                list.globalVersion++;
                list.count++;
                return new NodeHandle(list, newValue, list.nodes[_index].nextIndex, list.nodes[list.nodes[_index].nextIndex].version);
            }

            public void Remove() {
                if (!IsValid()) throw new ArgumentException("Handle is invalid");
                if (list.nodes[_index].previousIndex != -1) {
                    list.nodes[list.nodes[_index].previousIndex].nextIndex = list.nodes[_index].nextIndex;
                }
                if (list.nodes[_index].nextIndex != -1) {
                    list.nodes[list.nodes[_index].nextIndex].previousIndex = list.nodes[_index].previousIndex;
                }
                if (list.firstIndex == _index) {
                    list.firstIndex = list.nodes[_index].nextIndex;
                }
                if (list.lastIndex == _index) {
                    list.lastIndex = list.nodes[_index].previousIndex;
                }
                list.nodes[_index].value = default;
                list.nodes[_index].version++;
                list.nodes[_index].previousIndex = -1;
                list.nodes[_index].nextIndex = list.firstEmptyIndex;
                list.firstEmptyIndex = _index;
                list.globalVersion++;
                list.count--;
            }
        }

        [Serializable]
        private struct Node {
            public T? value;
            public int previousIndex;
            public int nextIndex;
            public uint version;

            public override string ToString() {
                return $"PI: {previousIndex} NI: {nextIndex} V: {version} | {value?.ToString() ?? "null"}";
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerator<T> GetEnumerator() => new Enumerator(this);

        public struct Enumerator : IEnumerator<T> {
            private LocalLinkedList<T>? _list;
            private NodeHandle? _handle;
            private readonly uint _globalVersion;

            internal Enumerator(LocalLinkedList<T> list) {
                _list = list;
                _handle = null;
                _globalVersion = list.globalVersion;
            }

            object IEnumerator.Current => Current!;

            public T Current => _handle!.Value.value!;
            
            public bool MoveNext() {
                if (_list == null) throw new InvalidOperationException("Enumerator has already been disposed");
                if (_globalVersion != _list.globalVersion) throw new InvalidOperationException("Collection was modified during enumeration");
                
                if (_handle == null) {
                    _handle = _list.GetFirst();
                    return _handle != null;
                }

                var next = _handle.Value.GetNext();
                if (next == null) return false;
                _handle = next;
                return true;
            }

            public void Reset() {
                if (_list == null) throw new InvalidOperationException("Enumerator has already been disposed");
                if (_globalVersion != _list.globalVersion) throw new InvalidOperationException("Collection was modified during enumeration");
                
                _handle = null;
            }

            public void Dispose() {
                _list = null;
                _handle = null;
            }
        }
    }
}