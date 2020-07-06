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
        /// 正剰余
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        static long PositiveMod(long i, long j)
        {
            return (i % j) < 0 ? (i % j) + 0 + (j < 0 ? -j : j) : (i % j + 0);
        }

        /// <summary>
        /// 二項係数(nCk mod. p)
        /// 上のmodが必要
        /// </summary>
        ///////////////////////////////////////////////////////
        static long MAX = 10000000;
        static long[] fac = new long[MAX];
        static long[] finv = new long[MAX];
        static long[] inv = new long[MAX];

        /// <summary>
        /// 初期化してテーブルを作成する
        /// </summary>
        static void COMinit()
        {
            fac[0] = fac[1] = 1;
            finv[0] = finv[1] = 1;
            inv[1] = 1;
            for (int i = 2; i < MAX; i++)
            {
                fac[i] = fac[i - 1] * i % mod;
                inv[i] = mod - inv[mod % i] * (mod / i) % mod;
                finv[i] = finv[i - 1] * inv[i] % mod;
            }
        }
        /// <summary>
        /// テーブルを使用して計算する
        /// </summary>
        /// <param name="n"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        static long Combination(int n, int k)
        {
            if (n < k) return 0;
            if (n < 0 || k < 0) return 0;
            return fac[n] * (finv[k] * finv[n - k] % mod) % mod;
        }
        /////////////////////////////////////////////////////////
        

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
        /// 二分探索を行う関数-雛形
        /// C#の場合System.Collections.Generic.BinarySerch()がある
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
        /// 二分探索を用いて
        /// 指定された要素以上の値が現れる最初のindexを取得する関数
        /// 計算量はO(logn)
        /// </summary>
        /// <param name="a">昇順ソート済みの配列</param>
        /// <param name="target">探索対象</param>
        /// <returns></returns>
        static long LowerBound(long[] a, long target)
        {
            var left = 0;
            var right = a.Length - 1;
            while (left <= right)
            {
                var mid = left + (right - left) / 2;
                if (a[mid] < target) left = mid + 1;
                else right = mid - 1;
            }
            return left;
        }

        /// <summary>
        /// 二分探索を用いて
        /// 指定された要素より大きい値が現れる最小のindexを取得する関数
        /// 計算量はO(logn)
        /// </summary>
        /// <param name="a">昇順ソート済みの配列</param>
        /// <param name="target">探索対象</param>
        /// <returns></returns>
        static long UpperBound(long[] a, long target)
        {
            var left = 0;
            var right = a.Length - 1;
            while (left <= right)
            {
                var mid = left + (right - left) / 2;
                if (a[mid] <= target) left = mid + 1;
                else right = mid - 1;
            }
            return left;
        }

        /// <summary>
        /// 等確率の期待値を求める関数
        /// サイコロとかで頻出
        /// </summary>
        /// <param name="x">1以上の整数</param>
        /// <returns></returns>
        static double EqualExpectedValue(int x)
        {
            if (x <= 0) throw new FormatException("1以上の整数しか受け付けられません");
            double ans = 0;
            for (double i = 1; i <= x; i++)
            {
                ans += i / x;
            }
            return ans;
        }

        /// <summary>
        /// 各桁の和を求める関数
        /// </summary>
        /// <param name="x">対象となる数</param>
        /// <returns></returns>
        static int EachDigitSum(long x)
        {
            int res = 0;
            while (x > 0)
            {
                res += (int)(x % 10);
                x /= 10;
            }

            return res;
        }

        /// <summary>
        /// 階乗を求める再帰関数
        /// </summary>
        /// <param name="x">x!を返す</param>
        /// <returns></returns>
        static long Factorial(int x)
        {
            long res = 1;
            for (int i = 2; i <= x; i++) res *= i;
            return res;
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
        /// modの絡む計算には使えない。
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

        /// <summary>
        /// 桁数を求める関数
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        static int Digits(long a)
        {
            if (a == 0) return 0;
            
            int ret = 0;
            while (a > 0)
            {
                a /= 10;
                ret++;
            }

            return ret;
        }

        /// <summary>
        /// 文字列をソートします
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        static string SortString(string s)
        {
            var tmp = s.ToCharArray();
            Array.Sort(tmp);
            return string.Concat(tmp);
        }

        /// <summary>
        /// 10進数からm進数に変換します
        /// </summary>
        /// <param name="n">変換元</param>
        /// <param name="m">m進数へ変換</param>
        /// <returns></returns>
        static string Func(int n, int m)
        {
            var sb = new StringBuilder();
            while (n > 0)
            {
                sb.Append(n % m);
                n /= m;
            }
            return new string(sb.ToString().Reverse().ToArray());
        }

        /// <summary>
        /// a より b の方が小さかったら、a の値を b の値に置き換える
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        static bool ChMin(ref long a, long b)
        {
            if (a > b)
            {
                a = b;
                return true;
            }
            return false;
        }

        /// <summary>
        /// a より b の方が大きかったら、a の値を b の値に置き換える
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        static bool ChMax(ref long a, long b)
        {
            if (a < b)
            {
                a = b;
                return true;
            }
            return false;
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


    /// <summary>
    /// 順列組み合わせを生成する静的クラス
    /// </summary>
    /// <typeparam name="T"></typeparam>
    static class Permutation<T>
    {
        private static void Search(List<T[]> perms, Stack<T> stack, T[] a)
        {
            int N = a.Length;
            if (N == 0)
            {
                perms.Add(stack.Reverse().ToArray());
            }
            else
            {
                var b = new T[N - 1];
                Array.Copy(a, 1, b, 0, N - 1);
                for (int i = 0; i < a.Length; ++i)
                {
                    stack.Push(a[i]);
                    Search(perms, stack, b);
                    if (i < b.Length) { b[i] = a[i]; }
                    stack.Pop();
                }
            }
        }
        /// <summary>
        /// 配列を与えるとList<T>で順列組み合わせを返す。
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static IEnumerable<T[]> Make(IEnumerable<T> src)
        {
            var perms = new List<T[]>();
            Search(perms, new Stack<T>(), src.ToArray());
            return perms;
        }
    }
}
