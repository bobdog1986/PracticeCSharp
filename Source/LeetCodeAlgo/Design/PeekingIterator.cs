using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace LeetCodeAlgo.Design
{
    ///284. Peeking Iterator
    ///https://docs.microsoft.com/en-us/dotnet/api/system.collections.ienumerator?view=netframework-4.8
    ///Design an iterator that supports the peek operation on an existing iterator
    ///in addition to the hasNext and the next operations.
    public class PeekingIterator
    {
        private IEnumerator<int> _iterator;
        private bool hasNext;

        // iterators refers to the first element of the array.
        public PeekingIterator(IEnumerator<int> iterator)
        {
            // initialize any member here.
            _iterator = iterator;
            hasNext = true;
        }

        // Returns the next element in the iteration without advancing the iterator.
        public int Peek()
        {
            return _iterator.Current;
        }

        // Returns the next element in the iteration and advances the iterator.
        public int Next()
        {
            var result = _iterator.Current;
            hasNext = _iterator.MoveNext();
            return result;
        }

        // Returns false if the iterator is refering to the end of the array of true otherwise.
        public bool HasNext()
        {
            return hasNext;
        }
    }
}
