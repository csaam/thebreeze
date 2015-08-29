using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenTwenty.Common.Utility
{
    public static class MemoizationExtensions
    {

        public static Action Memoize(this Action func)
        {
            object lockObject = new object();
            bool initialized = false;
            return () =>
            {
                if (!initialized)
                {
                    lock (lockObject)
                    {
                        if (!initialized)
                        {
                            func();
                        }
                    }
                }
            };
        }

        public static Action<T1> Memoize<T1>(this Action<T1> func)
        {
            ConcurrentDictionary<T1, object> lookup = new ConcurrentDictionary<T1, object>();

            return (t1) =>
            {
                if (lookup.TryAdd(t1, null))
                {
                    func(t1);
                }
            };
        }

        public static Action<T1,T2> Memoize<T1,T2>(this Action<T1,T2> func)
        {
            Action<Tuple<T1, T2>> temp = (t) => func(t.Item1, t.Item2);
            temp = temp.Memoize();
            return (t1, t2) => temp(Tuple.Create(t1, t2));
        }

        public static Action<T1, T2, T3> Memoize<T1, T2,T3>(this Action<T1, T2,T3> func)
        {
            Action<Tuple<T1, T2, T3>> temp = (t) => func(t.Item1, t.Item2, t.Item3);
            temp = temp.Memoize();
            return (t1, t2, t3) => temp(Tuple.Create(t1, t2, t3));
        }
        public static Action<T1, T2, T3, T4> Memoize<T1, T2, T3, T4>(this Action<T1, T2, T3, T4> func)
        {
            Action<Tuple<T1, T2, T3, T4>> temp = (t) => func(t.Item1, t.Item2, t.Item3, t.Item4);
            temp = temp.Memoize();
            return (t1, t2, t3, t4) => temp(Tuple.Create(t1, t2, t3, t4));
        }

        public static Action<T1, T2, T3, T4, T5> Memoize<T1, T2, T3, T4, T5>(this Action<T1, T2, T3, T4, T5> func)
        {
            Action<Tuple<T1, T2, T3, T4, T5>> temp = (t) => func(t.Item1, t.Item2, t.Item3, t.Item4, t.Item5);
            temp = temp.Memoize();
            return (t1, t2, t3, t4, t5) => temp(Tuple.Create(t1, t2, t3, t4, t5));
        }

        public static Func<R> Memoize<R>(this Func<R> func)
        {
            object lockObject = new object();
            bool initialized = false;
            R result = default(R);
            return () =>
            {
                if (!initialized)
                {
                    lock (lockObject)
                    {
                        if (!initialized)
                        {
                            result = func();
                        }
                    }
                }
                return result;
            };
        }

        public static Func<T1, R> Memoize<T1, R>(this Func<T1, R> func)
        {
            ConcurrentDictionary<T1, R> lookup = new ConcurrentDictionary<T1, R>();
            return (t1) =>
            {
                return lookup.GetOrAdd(t1, func);       
            };
        }
        public static Func<T1, T2, R> Memoize<T1, T2, R>(this Func<T1, T2, R> func)
        {
            Func<Tuple<T1, T2>, R> temp = (t) => func(t.Item1, t.Item2);
            temp = temp.Memoize();
            return (t1, t2) => temp(Tuple.Create(t1, t2));
        }

        public static Func<T1, T2, T3, R> Memoize<T1, T2, T3, R>(this Func<T1, T2, T3, R> func)
        {
            Func<Tuple<T1, T2, T3>, R> temp = (t) => func(t.Item1, t.Item2, t.Item3);
            temp = temp.Memoize();
            return (t1, t2, t3) => temp(Tuple.Create(t1, t2, t3));
        }

        public static Func<T1, T2, T3, T4, R> Memoize<T1, T2, T3, T4, R>(this Func<T1, T2, T3, T4, R> func)
        {
            Func<Tuple<T1, T2, T3, T4>, R> temp = (t) => func(t.Item1, t.Item2, t.Item3, t.Item4);
            temp = temp.Memoize();
            return (t1, t2, t3, t4) => temp(Tuple.Create(t1, t2, t3, t4));
        }

        public static Func<T1, T2, T3, T4, T5, R> Memoize<T1, T2, T3, T4, T5, R>(this Func<T1, T2, T3, T4, T5, R> func)
        {
            Func<Tuple<T1, T2, T3, T4, T5>, R> temp = (t) => func(t.Item1, t.Item2, t.Item3, t.Item4, t.Item5);
            temp = temp.Memoize();
            return (t1, t2, t3, t4, t5) => temp(Tuple.Create(t1, t2, t3, t4, t5));
        }

    }
}
