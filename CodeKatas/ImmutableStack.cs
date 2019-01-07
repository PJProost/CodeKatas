using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeKatas
{
    public class ImmutableStack<T>
    {
        public ImmutableStack()
        {
            Count = 0;
            Head = default(T);
            Tail = null;
        }

        public ImmutableStack<T> Add(T item)
        {
            return new ImmutableStack<T>()
            {
                Count = this.Count + 1,
                Head = item,
                Tail = this
            };
        }

        public ImmutableStack<T> Take()
        {
            if (Count == 0)
            {
                return null;
            }
            else
            {
                return new ImmutableStack<T>()
                {
                    Count = this.Count - 1,
                    Head = this.Tail.Head,
                    Tail = this.Tail.Tail
                };
            }
        }

        public T Peek()
        {
            return this.Head;
        }

        public int Count { get; set; }
        public T Head { get; set; }
        public ImmutableStack<T> Tail { get; set; }
    }
}
