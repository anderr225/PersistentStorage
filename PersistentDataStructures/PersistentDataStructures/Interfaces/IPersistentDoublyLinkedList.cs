using System.Collections.Generic;

namespace PersistentDataStructures.Interfaces
{
    public interface IPersistentDoublyLinkedList<T> : IEnumerable<T>
    {
        IPersistentDoublyLinkedList<T> PushBack(T value);
        
        IPersistentDoublyLinkedList<T> PushFront(T value);
        
        IPersistentDoublyLinkedList<T> RemoveFirst();
        
        IPersistentDoublyLinkedList<T> RemoveLast();
        
        T GetFirst();
        
        T GetLast();
    }
}