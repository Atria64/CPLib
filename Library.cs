using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Atcoder
{
    class Library
    {
        //自作ライブラリ集
        //使うときは適宜ここからコピペすること

        //いつもの
        readonly static long mod = 1000000000 + 7;

        /// <summary>
        /// Gcd (Gross Common Dividing) 最大公約数
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        static long Gcd(long a,long b)
        {
            var v = new[] { a, b };
            while(v[1] != 0) { v = new[] { v[1], v[0] % v[1] }; }
            return v[0];
        }

        /// <summary>
        /// Lcm (Last Common Multiple) 最小公倍数 
        /// Gcdライブラリの仕様が前提
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        static long Lcm (long a,long b)
        {
            return a / Gcd(a, b) * b;
        }

        /// <summary>
        /// 二分探索を行う関数
        /// 計算量はO(logn)
        /// </summary>
        /// <param name="a">ソート済みの配列</param>
        /// <param name="target">探索対象</param>
        /// <returns></returns>
        static bool BinarySearch(long[] a, long target)
        {
            var left = 0;
            var right = a.Length;
            while (left < right)
            {
                var mid = (left + right) / 2;
                if (a[mid] == target) return true;
                if (a[mid] > target)
                {
                    right = mid;
                }
                else left = mid + 1;
            }
            return false;
        }

        /// <summary>
        /// 各桁の和を求める関数
        /// </summary>
        /// <param name="x">対象となる数</param>
        /// <returns></returns>
        static long EachDigitSum(long x)
        {
            if (x < 10) return x;

            return Cal(x / 10) + x % 10;
        }

        /// <summary>
        /// 階乗を求める再帰関数
        /// </summary>
        /// <param name="x">x!を返す</param>
        /// <returns></returns>
        static long Factorial(long x)
        {
            if (x == 1) return 1;
            return x * Factorial(x - 1);
        }

        /// <summary>
        /// 素数判定を行う関数
        /// 計算量はO(√n)
        /// </summary>
        /// <param name="n">対象変数</param>
        /// <returns></returns>
        static bool Is_Prime(long n)
        {
            if (n == 2)
            {
                return true;
            }
            if (n < 2 || n % 2 == 0)
            {
                return false;
            }
            int i = 3;
            while (i<= Math.Sqrt(n))
            {
                if (n % i == 0)
                {
                    return false;
                }
                i += 2;
            }
            return true;
        }

        /// <summary>
        /// 素因数分解を行う関数
        /// 計算量は多分O(√n)
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        static IEnumerable<long> PrimeFactor(long n)
        {
            int i = 2;
            long tmp = n;

            while (i <= Math.Sqrt(n))
            {
                if (tmp % i == 0)
                {
                    tmp /= i;
                    yield return i;
                }
                else i++;
            }
            if (tmp != 1) yield return tmp;
        }

        /// <summary>
        /// nCrを求める関数
        /// 計算量は多分O(n)
        /// </summary>
        /// <param name="n">nCrのnの部分</param>
        /// <param name="r">nCrのrの部分</param>
        /// <returns></returns>
        static long Combination(long n, long r)
        {
            if (n < r) return -1;
            long ans = 1;
            for (long i = n; i > n - r; i--)
            {
                ans *= i;
            }
            for (long i = 2; i <= r; i++)
            {
                ans /= i;
            }
            return ans;
        }

        /// <summary>
        /// 交換ソートを行う関数
        /// 計算量はO(n^2)で非常に低速、安定な、内部ソート。
        /// Swap関数を用いる
        /// </summary>
        /// <param name="array"></param>
        static void BubbleSort(long[] array)
        {
            bool flug = true;
            while (flug)
            {
                flug = false;
                for (int i = 0; i < array.Length - 1; i++)
                {
                    if (array[i] > array[i + 1])
                    {
                        Swap(ref array[i], ref array[i + 1]);
                        flug = true;
                    }
                }
            }
        }

        /// <summary>
        /// 選択ソートを行う関数
        /// 計算量はO(n^2)で非常に低速、安定な、内部ソート。
        /// Swap関数を用いる
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        static void SelectionSort<T>(T[] array) where T : IComparable<T>
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                int min = i;    // 最小値のインデックス保持用
                                // このループが終われば min には最小値のインデックスが入ることになる
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (array[min].CompareTo(array[j]) > 0)
                    {
                        min = j;
                    }
                }
                // 見つかった最小値の値を交換する
                Swap(ref array[min], ref array[i]);
            }
        }

        /// <summary>
        /// aとbをスワップさせる関数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="a"></param>
        /// <param name="b"></param>
        static void Swap<T>(ref T a, ref T b)
        {
            var tmp = a;
            a = b;
            b = tmp;
        }
    }

    /// <summary>
    /// 優先度付きキュー を表す。
    /// System.Runtime.CompilerServicesが必要
    /// </summary>
    /// <typeparam name="T">キューの型</typeparam>
    public class PriorityQueue<T> where T : IComparable<T>
    {
        public bool Any() => Count > 0;
        public int Count { get; private set; }
        private bool Descendance;
        private T[] data = new T[65536];

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public PriorityQueue(bool descendance = false)
        {
            Descendance = descendance;
        }

        /// <summary>
        /// キューの最小 (<see cref="Descendance"/> が <see cref="true"/> の場合は最大) の要素を取得します。
        /// </summary>
        public T Top
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                ValidateNonEmpty();
                return data[1];
            }
        }

        /// <summary>
        /// キューの最小 (<see cref="Descendance"/> が <see cref="true"/> の場合は最大) の要素を削除して返す。
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Pop()
        {
            var top = Top;
            var elem = data[Count--];
            int index = 1;
            while (true)
            {
                if ((index << 1) >= Count)
                {
                    if (index << 1 > Count) break;
                    if (elem.CompareTo(data[index << 1]) > 0 ^ Descendance) data[index] = data[index <<= 1];
                    else break;
                }
                else
                {
                    var nextIndex = data[index << 1].CompareTo(data[(index << 1) + 1]) <= 0 ^ Descendance
                        ? (index << 1)
                        : (index << 1) + 1;
                    if (elem.CompareTo(data[nextIndex]) > 0 ^ Descendance) data[index] = data[index = nextIndex];
                    else break;
                }
            }

            data[index] = elem;
            return top;
        }

        /// <summary>
        /// キューに要素を追加する。
        /// </summary>
        /// <param name="value">追加する要素</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Push(T value)
        {
            int index = ++Count;
            if (data.Length == Count) Extend(data.Length * 2);
            while ((index >> 1) != 0)
            {
                if (data[index >> 1].CompareTo(value) > 0 ^ Descendance) data[index] = data[index >>= 1];
                else break;
            }

            data[index] = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Extend(int newSize)
        {
            T[] newDatas = new T[newSize];
            data.CopyTo(newDatas, 0);
            data = newDatas;
        }

        private void ValidateNonEmpty()
        {
            if (Count == 0) throw new Exception();
        }
    }

    
}
