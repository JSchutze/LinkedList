using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp16
{
    public class SLinkedList<T> : IList<T>, IEnumerator<T>
    {
        private class Cell
        {
            public T Value { get; set; }
            public Cell Next { get; set; }
            public Cell Prev { get; set; }
        }
        private Cell head;
        private Cell tail;
        private int position = -1;
        public SLinkedList()
        {
            Count = 0;
            head = default;
            tail = default;
        }

        public T this[int index]
        {
            get
            {
                if (index > Count - 1)
                    throw new IndexOutOfRangeException();
                return GetCellByIndex(index).Value;

            }
            set
            {
                if (index > Count - 1)
                    throw new IndexOutOfRangeException();
                GetCellByIndex(index).Value = value;

            }
        }

        public int Count { get; private set; }

        public bool IsReadOnly { get { return false; } }

        T IEnumerator<T>.Current
        {
            get
            {
                if (position >= Count || position == -1)
                    throw new InvalidOperationException();
                return this[position];
            }
        }

        object IEnumerator.Current => throw new NotImplementedException();

        public void Add(T item)
        {
            Cell current = new Cell();
            current.Value = item;
            if (Count == 0)
            {
                head = current;
                tail = current;
            }
            else
            {
                tail.Next = current;
                current.Prev = tail;
                tail = current;
            }
            Count++;
        }

        public void Clear()
        {
            Count = 0;
            head = default;
            tail = default;
        }

        public bool Contains(T item)
        {
            return IndexOf(item) != -1;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            Cell current = head;
            while (current != null)
            {
                array[arrayIndex] = current.Value;
                current = current.Next;
                arrayIndex++;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this;
        }

        public int IndexOf(T item)
        {
            Cell current = head;
            int index = 0;
            while (current != null)
            {
                if (current.Value.Equals(item))
                    return index;
                current = current.Next;
                index++;
            }
            return -1;

        }

        public void Insert(int index, T item)
        {
            if (Count == 0 || index > Count-1)
                throw new InvalidOperationException ();
            if (index > 0 && index < Count-1)
            {
                Cell current = GetCellByIndex(index);
                Cell insertion = new Cell();
                current.Prev.Next = insertion;
                insertion.Value = item;
                insertion.Next = current;
                insertion.Prev = current.Prev;
                current.Prev = insertion;
                
            }
            if (index == 0)
            {
                Cell insertion = new Cell();
                insertion.Value = item;
                head.Prev = insertion;
                insertion.Prev = null;
                insertion.Next = head;
                head = insertion;
            }
            if (index == Count-1)
            {
                Cell insertion = new Cell();
                insertion.Value = item;
                insertion.Next = null;
                insertion.Prev = tail;
                tail.Next = insertion;
                tail = insertion;
            }
            Count++;
        }

        public bool Remove(T item)
        {
            if (IndexOf(item) > -1)
            {
                RemoveAt(IndexOf(item));
                return true;
            }
            return false;
        
        }

        public void RemoveAt(int index)
        {
            if (Count == 0 || index > Count - 1)
                throw new InvalidOperationException();
            if (index > 0 && index < Count - 1)
            {
                Cell current = GetCellByIndex(index);
                current.Prev.Next = current.Next;
                current.Next.Prev = current.Prev;
            }
            if (index == 0)
            {
                head = head.Next;
                head.Prev = null;
            }
            if (index == Count - 1)
            {
                tail = tail.Prev;
                tail.Next = null;
            }
            Count--;
        }

        private Cell GetCellByIndex(int index)
        {
            Cell current = new Cell();
            current = head;
            int counter = 0;
            while (index != counter)
            {
                current = current.Next;
                counter++;
            }
            return current;
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        void IDisposable.Dispose()
        {
        }

        bool IEnumerator.MoveNext()
        {
            if (Count != 0 && position < Count - 1)
            {
                position++;
                return true;
            }
            else
                return false;
        }

        void IEnumerator.Reset()
        {
            position = -1;
        }
    }
}
