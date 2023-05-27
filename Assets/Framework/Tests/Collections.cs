#nullable enable
using System.Collections.Generic;
using Framework.Collections;
using NUnit.Framework;

namespace Framework.Tests {
    public class Collections {
        [Test]
        public void LocalLinkedList() {
            // Create empty lists
            var expected = new LinkedList<int>();
            var actual = new LocalLinkedList<int>();
            Assert.AreEqual(expected.Count, actual.Count);
            CollectionAssert.AreEqual(expected, actual);

            // Add first when length is 0
            expected.AddFirst(0);
            actual.AddFirst(0);
            Assert.AreEqual(expected.Count, actual.Count); // 1
            CollectionAssert.AreEqual(expected, actual); // 0

            // Remove first when length is 1
            expected.RemoveFirst();
            actual.GetFirst()!.Value.Remove();
            Assert.AreEqual(expected.Count, actual.Count); // 0
            CollectionAssert.AreEqual(expected, actual); // Empty

            // Add last when length is 0
            expected.AddLast(1);
            actual.AddLast(1);
            Assert.AreEqual(expected.Count, actual.Count); // 1
            CollectionAssert.AreEqual(expected, actual); // 1
            
            // Remove last when length is 1
            expected.RemoveLast();
            actual.GetLast()!.Value.Remove();
            Assert.AreEqual(expected.Count, actual.Count); // 0
            CollectionAssert.AreEqual(expected, actual); // Empty
            
            // Add first when length is not 0
            expected.AddFirst(2);
            expected.AddFirst(3);
            actual.AddFirst(2);
            actual.AddFirst(3);
            Assert.AreEqual(expected.Count, actual.Count); // 2
            CollectionAssert.AreEqual(expected, actual); // 3 2
            
            // Add last when length is not 0
            expected.AddLast(4);
            actual.AddLast(4);
            Assert.AreEqual(expected.Count, actual.Count); // 3
            CollectionAssert.AreEqual(expected, actual); // 3 2 4
            
            // Insert before first
            expected.AddBefore(expected.First, 5);
            actual.GetFirst()!.Value.InsertBefore(5);
            Assert.AreEqual(expected.Count, actual.Count); // 4
            CollectionAssert.AreEqual(expected, actual); // 5 3 2 4
            
            // Insert after last
            expected.AddAfter(expected.Last, 6);
            actual.GetLast()!.Value.InsertAfter(6);
            Assert.AreEqual(expected.Count, actual.Count); // 5
            CollectionAssert.AreEqual(expected, actual); // 5 3 2 4 6
            
            // Insert before middle
            expected.AddBefore(expected.First.Next!, 7);
            actual.GetFirst()!.Value.GetNext()!.Value.InsertBefore(7);
            Assert.AreEqual(expected.Count, actual.Count); // 6
            CollectionAssert.AreEqual(expected, actual); // 5 7 3 2 4 6
            
            // Insert after middle
            expected.AddAfter(expected.First.Next!, 8);
            actual.GetFirst()!.Value.GetNext()!.Value.InsertAfter(8);
            Assert.AreEqual(expected.Count, actual.Count); // 7
            CollectionAssert.AreEqual(expected, actual); // 5 7 8 3 2 4 6
            
            // Remove first when length is greater than 1
            expected.RemoveFirst();
            actual.GetFirst()!.Value.Remove();
            Assert.AreEqual(expected.Count, actual.Count); // 6
            CollectionAssert.AreEqual(expected, actual); // 7 8 3 2 4 6
            
            // Remove last when length is greater than 1
            expected.RemoveLast();
            actual.GetLast()!.Value.Remove();
            Assert.AreEqual(expected.Count, actual.Count); // 5
            CollectionAssert.AreEqual(expected, actual); // 7 8 3 2 4
            
            // Remove middle
            expected.Remove(expected.First.Next!);
            actual.GetFirst()!.Value.GetNext()!.Value.Remove();
            Assert.AreEqual(expected.Count, actual.Count); // 4
            CollectionAssert.AreEqual(expected, actual); // 7 3 2 4
            
            // Clear
            expected.Clear();
            actual.Clear();
            Assert.AreEqual(expected.Count, actual.Count); // 0
            CollectionAssert.AreEqual(expected, actual); // Empty
            
            // Everything works after clearing
            
            // Add first when length is 0
            expected.AddFirst(0);
            actual.AddFirst(0);
            Assert.AreEqual(expected.Count, actual.Count); // 1
            CollectionAssert.AreEqual(expected, actual); // 0

            // Remove first when length is 1
            expected.RemoveFirst();
            actual.GetFirst()!.Value.Remove();
            Assert.AreEqual(expected.Count, actual.Count); // 0
            CollectionAssert.AreEqual(expected, actual); // Empty

            // Add last when length is 0
            expected.AddLast(1);
            actual.AddLast(1);
            Assert.AreEqual(expected.Count, actual.Count); // 1
            CollectionAssert.AreEqual(expected, actual); // 1
            
            // Remove last when length is 1
            expected.RemoveLast();
            actual.GetLast()!.Value.Remove();
            Assert.AreEqual(expected.Count, actual.Count); // 0
            CollectionAssert.AreEqual(expected, actual); // Empty
            
            // Add first when length is not 0
            expected.AddFirst(2);
            expected.AddFirst(3);
            actual.AddFirst(2);
            actual.AddFirst(3);
            Assert.AreEqual(expected.Count, actual.Count); // 2
            CollectionAssert.AreEqual(expected, actual); // 3 2
            
            // Add last when length is not 0
            expected.AddLast(4);
            actual.AddLast(4);
            Assert.AreEqual(expected.Count, actual.Count); // 3
            CollectionAssert.AreEqual(expected, actual); // 3 2 4
            
            // Insert before first
            expected.AddBefore(expected.First, 5);
            actual.GetFirst()!.Value.InsertBefore(5);
            Assert.AreEqual(expected.Count, actual.Count); // 4
            CollectionAssert.AreEqual(expected, actual); // 5 3 2 4
            
            // Insert after last
            expected.AddAfter(expected.Last, 6);
            actual.GetLast()!.Value.InsertAfter(6);
            Assert.AreEqual(expected.Count, actual.Count); // 5
            CollectionAssert.AreEqual(expected, actual); // 5 3 2 4 6
            
            // Insert before middle
            expected.AddBefore(expected.First.Next!, 7);
            actual.GetFirst()!.Value.GetNext()!.Value.InsertBefore(7);
            Assert.AreEqual(expected.Count, actual.Count); // 6
            CollectionAssert.AreEqual(expected, actual); // 5 7 3 2 4 6
            
            // Insert after middle
            expected.AddAfter(expected.First.Next!, 8);
            actual.GetFirst()!.Value.GetNext()!.Value.InsertAfter(8);
            Assert.AreEqual(expected.Count, actual.Count); // 7
            CollectionAssert.AreEqual(expected, actual); // 5 7 8 3 2 4 6
            
            // Remove first when length is greater than 1
            expected.RemoveFirst();
            actual.GetFirst()!.Value.Remove();
            Assert.AreEqual(expected.Count, actual.Count); // 6
            CollectionAssert.AreEqual(expected, actual); // 7 8 3 2 4 6
            
            // Remove last when length is greater than 1
            expected.RemoveLast();
            actual.GetLast()!.Value.Remove();
            Assert.AreEqual(expected.Count, actual.Count); // 5
            CollectionAssert.AreEqual(expected, actual); // 7 8 3 2 4
            
            // Remove middle
            expected.Remove(expected.First.Next!);
            actual.GetFirst()!.Value.GetNext()!.Value.Remove();
            Assert.AreEqual(expected.Count, actual.Count); // 4
            CollectionAssert.AreEqual(expected, actual); // 7 3 2 4
            
            // Clear
            expected.Clear();
            actual.Clear();
            Assert.AreEqual(expected.Count, actual.Count); // 0
            CollectionAssert.AreEqual(expected, actual); // Empty
        }
    }
}