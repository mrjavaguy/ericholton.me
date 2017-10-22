Title: Functional Programing in C# - Chapter 1 Exercises
Published: 10/22/2017
Tags: 
  - books
  - c#
  - functional
---
# Functional Programming in C# - Chapter 1

<img alt="" src="https://images-na.ssl-images-amazon.com/images/I/31HD12uVDHL._SX396_BO1,204,203,200_.jpg" class="a-dynamic-image" id="imgBlkFront" width="260px" data-a-dynamic-image="{&quot;https://images-na.ssl-images-amazon.com/images/I/31HD12uVDHL._SX396_BO1,204,203,200_.jpg&quot;:[398,499],&quot;https://images-na.ssl-images-amazon.com/images/I/31HD12uVDHL._SX258_BO1,204,203,200_.jpg&quot;:[260,326]}">

I am currently reading the book [*Functional Programming in C#*](https://www.manning.com/books/functional-programming-in-c-sharp) by Enrico Buonanno. Each chapter has a series of exercises. Which I am going to do some of them and put into my blog. I am not going to do all of them necessarily but the ones I find most interesting.

## Exercise 3

Write a method the uses quicksort to sort a List<int> (return a new list, rather than sorting in place)

I learned quicksort back in memory was at a premium, so I did learn to sort in place. In fact instead of using recursion all the way down, we usually just did a bubble or shell sort for n less than some value. Enrico includes a download from his [GitHub](https://github.com/la-yumba/functional-csharp-code) account for all of the exercise (it includes samples). It is interesting to compare some of the difference in the code I wrote (below) and the code he wrote. I biggest difference was where the pivot point was chosen. He did the first element, whereas I did the last element. I do think his is more efficient for C#.

``` csharp
using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Exercises.Chapter1
{
    static class Exercises
    {
        // 1. Write a function that negates a given predicate: whenvever the given predicate
        // evaluates to `true`, the resulting function evaluates to `false`, and vice versa.
        public static Func<bool, bool> Negates => b => !b;

        static Func<T, bool> Negate<T>(this Func<T, bool> pred) => t => !pred(t);

        // 2. Write a method that uses quicksort to sort a `List<int>` (return a new list,
        // rather than sorting it in place).
        static List<int> QuickSort(this List<int> list)
        {
            var count = list.Count - 1;
            if (count < 0) return new List<int>();

            var pivot = list.Last();
            var rest = list.Take(count);

            var smaller = rest.Where(i => pivot >= i);
            var larger = rest.Where(i => pivot < i);

            return smaller.ToList().QuickSort()
               .Append(pivot)
               .Concat(larger.ToList().QuickSort())
               .ToList();
        }

        [Test]
        public static void TestQuickSort()
        {
            var list = new List<int> { -100, 63, 30, 45, 1, 1000, -23, -67, 1, 2, 56, 75, 975, 432, -600, 193, 85, 12 };
            var expected = new List<int> { -600, -100, -67, -23, 1, 1, 2, 12, 30, 45, 56, 63, 75, 85, 193, 432, 975, 1000 };
            var actual = list.QuickSort();
            Assert.AreEqual(expected, actual);
        }
        // 3. Generalize your implementation to take a `List<T>`, and additionally a 
        // `Comparison<T>` delegate.
        static List<T> QuickSort<T>(this List<T> list, Comparison<T> comparer) where T: IComparable<T>
        {
            var count = list.Count - 1;
            if (count < 0) return new List<T>();

            var pivot = list.Last();
            var rest = list.Take(count);

            var smaller = rest.Where(i => comparer(i, pivot) <= 0);
            var larger = rest.Where(i => comparer(i, pivot) > 0);

            return smaller.ToList<T>().QuickSort<T>(comparer)
               .Append(pivot)
               .Concat(larger.ToList<T>().QuickSort<T>(comparer))
               .ToList();
        }


        [Test]
        public static void TestQSort()
        {
            var list = new List<int> { -100, 63, 30, 45, 1, 1000, -23, -67, 1, 2, 56, 75, 975, 432, -600, 193, 85, 12 };
            var expected = new List<int> { -600, -100, -67, -23, 1, 1, 2, 12, 30, 45, 56, 63, 75, 85, 193, 432, 975, 1000 };
            var actual = list.QuickSort();
            Assert.AreEqual(expected, actual);
        }

        // 4. In this chapter, you've seen a `Using` function that takes an `IDisposable`
        // and a function of type `Func<TDisp, R>`. Write an overload of `Using` that
        // takes a `Func<IDisposable>` as first
        // parameter, instead of the `IDisposable`. (This can be used to fix warnings
        // given by some code analysis tools about instantiating an `IDisposable` and
        // not disposing it.)

        static T Using<D,T>(Func<D> createDisposable, Func<D, T> func) where D:IDisposable
        {
            using (var disp = createDisposable()) return func(disp);
        }

    }
}


```