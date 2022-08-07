using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///Greatest Common Divisor, #GCD
        private int getGCD(int x, int y)
        {
            return y == 0 ? x : getGCD(y, x % y);
        }
        private long getGCDLong(long x, long y)
        {
            return y == 0 ? x : getGCDLong(y, x % y);
        }
        ///Least Common Multiple, #LCM
        private int getLCM(int x, int y)
        {
            return x / getGCD(x, y) * y;
        }
        private long getLCMLong(long x, long y)
        {
            return x / getGCDLong(x, y) * y;
        }

        private long getFactorial(long n)
        {
            if (n == 0 || n == 1) return 1;
            long r = 1;
            while (n >= 1)
            {
                r *= n;
                n--;
            }
            return r;
        }

        private long getFactorial(long n, long count)
        {
            long r = 1;
            while (count > 0 && n > 0)
            {
                r *= n;
                n--;
                count--;
            }
            return r;
        }

        private long getCombines(long n, long count)
        {
            return getFactorial(n, count) / getFactorial(count);
        }

        private bool[][] initVisitMatrix(int m,int n)
        {
            var visit=new bool[m][];
            for (int i = 0; i < m; i++)
                visit[i] = new bool[n];
            return visit;
        }

        private List<int>[] initListGraph(int n)
        {
            List<int>[] graph = new List<int>[n];
            for (int i = 0; i < n; i++)
                graph[i] = new List<int>();
            return graph;
        }

        private int[][] init2DMatrix(int m, int n, int seed=0)
        {
            var dp = new int[m][];
            for (int i = 0; i < m; i++)
            {
                dp[i] = new int[n];
                if(seed!=0)
                    Array.Fill(dp[i], seed);
            }
            return dp;
        }

        private int[][][] init3DMatrix(int m, int n,int k, int seed=0)
        {
            var dp = new int[m][][];
            for (int i = 0; i < m; i++)
            {
                dp[i] = new int[n][];
                for(int j = 0; j < n; j++)
                {
                    dp[i][j] = new int[k];
                    if(seed!=0)
                        Array.Fill(dp[i][j], seed);
                }
            }
            return dp;
        }

        private long[][] init2DLongMatrix(int m, int n,  long seed = 0)
        {
            var dp = new long[m][];
            for (int i = 0; i < m; i++)
            {
                dp[i] = new long[n];
                if (seed != 0)
                    Array.Fill(dp[i], seed);
            }
            return dp;
        }


    }

    /// <summary>
    /// using binary search to auto sort a List<T>, default compare
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AutoSortList<T>
    {
        private readonly List<T> _list;
        private readonly IComparer<T>? _comparer;

        public AutoSortList() : this(null) { }

        public AutoSortList(IComparer<T>? comparer)
        {
            _comparer = InitializeComparer(comparer);
            _list = new List<T>();
        }

        public AutoSortList(IEnumerable<T> elements, IComparer<T>? comparer)
        {
            _comparer = InitializeComparer(comparer);
            _list = elements.OrderBy(x => x, _comparer).ToList();
        }

        private static IComparer<T>? InitializeComparer(IComparer<T>? comparer)
        {
            if (comparer == null) return Comparer<T>.Default;
            else return comparer;
        }

        public int Count => _list.Count;

        public List<T> List => _list;

        public int Insert(T val)
        {
            if (_list.Count == 0)
            {
                _list.Add(val);
                return 0;
            }
            else
            {
                if (_comparer.Compare(val, _list[0]) <= 0)
                {
                    _list.Insert(0, val);
                    return 0;
                }
                else if (_comparer.Compare(_list.Last(), val) < 0)
                {
                    _list.Add(val);
                    return _list.Count - 1;
                }
                else
                {
                    int left = 0;
                    int right = _list.Count - 1;
                    while (left < right)
                    {
                        int mid = (left + right) / 2;
                        if (_comparer.Compare(_list[mid], val) < 0)
                        {
                            left = mid + 1;
                        }
                        else
                        {
                            right = mid;
                        }
                    }
                    _list.Insert(left, val);
                    return left;
                }
            }
        }

        public int IndexIfInsert(T val)
        {
            if (_list.Count == 0)
            {
                return 0;
            }
            else
            {
                if (_comparer.Compare(val, _list[0]) <= 0)
                {
                    return 0;
                }
                else if (_comparer.Compare(_list.Last(), val) < 0)
                {
                    return _list.Count;
                }
                else
                {
                    int left = 0;
                    int right = _list.Count - 1;
                    while (left < right)
                    {
                        int mid = (left + right) / 2;
                        if (_comparer.Compare(_list[mid], val) < 0)
                        {
                            left = mid + 1;
                        }
                        else
                        {
                            right = mid;
                        }
                    }
                    return left;
                }
            }
        }

        public T this[int index]
        {
            get => _list[index];
        }
    }

}