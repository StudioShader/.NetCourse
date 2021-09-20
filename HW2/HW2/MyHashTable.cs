using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW2
{
    class MyHashTable<T>: IEnumerable<T> where T: IComparable
    {
        private int Size { get; set; }
        private int Count { get; set; }
        private HashElement<T>[] Storage { get; set; }

        public MyHashTable()
            : this(10)
        {
        }

        public MyHashTable(int size)
        {
            this.Size = size * 2;
            this.Storage = new HashElement<T>[this.Size];
            this.Count = 0;
        }

        public bool Add(T value)
        {
            if (this.Count >= this.Size)
            {
                this.Rebuild();
            }

            var index = this.GetHash(value);
            while (this.Storage[index] != null &&
                (!this.Storage[index].IsDeleted || !this.Storage[index].IsEmpty))
            {
                index = this.GetNextProbingIndex(index);
            }

            if (this.Storage[index] == null)
            {
                this.Storage[index] = new HashElement<T>();
            }

            this.Storage[index].Content = value;
            this.Storage[index].IsDeleted = false;
            this.Storage[index].IsEmpty = false;
            this.Count++;
            return true;
        }

        public bool Contains(T value)
        {
            var index = this.GetHash(value);
            while (true)
            {
                if (this.Storage[index] == null
                    || this.Storage[index].IsDeleted == true
                    || this.Storage[index].IsEmpty == true)
                {
                    return false;
                }
                if (this.Storage[index].Content.CompareTo(value) == 0)
                {
                    return true;
                }

                index = this.GetNextProbingIndex(index);
            }
        }

        public bool Remove(T value)
        {
            var index = this.GetHash(value);
            while (true)
            {
                if (this.Storage[index] == null
                    || this.Storage[index].IsEmpty == true)
                {
                    return false;
                }

                if (this.Storage[index].Content.CompareTo(value) == 0)
                {
                    this.Storage[index].IsDeleted = true;
                    this.Count--;
                    return true;
                }

                index = this.GetNextProbingIndex(index);
            }
        }

        public void Clear()
        {
            foreach (var item in this.Storage)
            {
                if (item != null)
                {
                    item.IsEmpty = true;
                    item.IsDeleted = false;
                }
            }
        }

        private void Rebuild()
        {
            this.Size = Size * 2;
            var newStorageItems = new List<T>();
            foreach (var item in this)
            {
                newStorageItems.Add(item);
            }

            this.Count = 0;
            this.Storage = new HashElement<T>[this.Size];
            foreach (var item in newStorageItems)
            {
                this.Add(item);
            }
        }

        private int GetHash(T value)
        {
            return value.GetHashCode() % this.Size;
        }

        private int GetNextProbingIndex(int index)
        {
            return (index + 1) % this.Size;
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var item in this.Storage)
            {
                if (item != null && !item.IsDeleted && !item.IsEmpty)
                {
                    yield return item.Content;
                }
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
