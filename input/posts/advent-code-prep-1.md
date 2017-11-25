Title: Advent Of Code Prep
Published: 11/25/2017
Tags: 
  - C#
  - advent of code 2017
---
# Advent Of Code

This time of year there is a coding challenge called [Advent of Code](https://adventofcode.com/) that some of us at work like to try and do. Last year I did all of the challenges in F#, this year I am planning to do functional C#.

## Preparatory Work

I am doing some prep work before the challenges based on last years Advent of Code.

### Memoize

Since there are computational complex calculation used, and the get repeated, I am going to use the [Memoize](https://en.wikipedia.org/wiki/Memoization) pattern.

```csharp
    public static class Memoize
    {
        public static Func<TReturn> Memo<TReturn>(this Func<TReturn> func)
        {
            object cache = null;
            return () =>
            {
                if (cache == null)
                {
                    cache = func();
                }

                return (TReturn)cache;
            };
        }

        public static Func<TSource, TReturn> Memo<TSource, TReturn>(this Func<TSource, TReturn> func)
        {
            var cache = new ConcurrentDictionary<TSource, TReturn>();

            return argument => cache.GetOrAdd(argument, func);
        }


        public static Func<TSource1, TSource2, TReturn> Memo<TSource1, TSource2, TReturn>(this Func<TSource1, TSource2, TReturn> func)
        {
            var cache = new ConcurrentDictionary<(TSource1, TSource2), TReturn>();

            return (argument1, argument2) => cache.GetOrAdd((argument1, argument2), func(argument1, argument2));
        }
    }

```

#### Tests

```csharp
    public class MemorizeTests
    {
        [Fact]
        public void MemoJustReturn()
        {
            Func<int> f = () => { var x = 0; for (var y = 0; y < 1000; y++) x += y; return x; };

            f.Memo();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var x1 = f();
            stopwatch.Stop();
            var first = stopwatch.Elapsed;
            stopwatch.Reset();
            stopwatch.Start();
            var x2 = f();
            stopwatch.Stop();
            var second = stopwatch.Elapsed;
            Assert.True(second < first);
            Assert.Equal(x1, x2);
        }

        [Fact]
        public void MemoOneValue()
        {
            Func<int, int> f = null;
            f = n => n > 1 ? f(n - 1) + f(n - 2) : n;

            f.Memo();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var x1 = f(10);
            stopwatch.Stop();
            var first = stopwatch.Elapsed;
            stopwatch.Reset();
            stopwatch.Start();
            var x2 = f(10);
            stopwatch.Stop();
            var second = stopwatch.Elapsed;
            Assert.True(second < first);
            Assert.Equal(x1, x2);
        }


        [Fact]
        public void MemoTwoValues()
        {
            Func<int, int, int> f = null;
            f = (x, y) =>
            {
                var z = 0;
                for (var i = 0; i < x; i++)
                    for (var j = 0; j < y; j++)
                        z += i * j; 
                return z;
            };

            f.Memo();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var x1 = f(100, 100);
            stopwatch.Stop();
            var first = stopwatch.Elapsed;
            stopwatch.Reset();
            stopwatch.Start();
            var x2 = f(100, 100);
            stopwatch.Stop();
            var second = stopwatch.Elapsed;
            Assert.True(second < first);
            Assert.Equal(x1, x2);
        }
    }

```

### Binary Tree

Use for shortest path calculations, among other things.

```csharp
    public class Tree<T>
    {

        public Tree(T value, Tree<T> left = default(Tree<T>), Tree<T> right = default(Tree<T>))
        {
            this.Value = value;
            this.Left = left;
            this.Right = right;
        }

        public Tree<T> Left { get; }
        public Tree<T> Right { get; }

        public T Value { get; }

        public static Tree<T> Empty => default(Tree<T>);

        public Tree<T> AddLeftTree(Tree<T> tree)
        {
            return new Tree<T>(this.Value, tree, this.Right);
        }

        public Tree<T> AddRightTree(Tree<T> tree)
        {
            return new Tree<T>(this.Value, this.Left, tree);
        }

        public IEnumerable<T> Preorder()
        {
            var stack = new Stack<Tree<T>>();
            stack.Push(this);

            while (stack.Any())
            {
                var tree = stack.Pop();
                yield return tree.Value;
                if (tree.Right != Empty)
                {
                    stack.Push(tree.Right);
                }
                if (tree.Left != Empty)
                {
                    stack.Push(tree.Left);
                }
            }
        }

        public IEnumerable<T> Inorder()
        {
            var stack = new Stack<Tree<T>>();
            var node = this;

            while (node != Empty)
            {
                stack.Push(node);
                node = node.Left;
            }

            while (stack.Any())
            {

                // visit the top node
                node = stack.Pop();
                yield return node.Value;
                if (node.Right != Empty)
                {
                    node = node.Right;

                    // the next node to be visited is the leftmost
                    while (node != Empty)
                    {
                        stack.Push(node);
                        node = node.Left;
                    }
                }
            }
        }

        public IEnumerable<T> Postorder()
        {
            var stack1 = new Stack<Tree<T>>();
            var stack2 = new Stack<Tree<T>>();

            stack1.Push(this);

            while (stack1.Any())
            {
                var tree = stack1.Pop();
                stack2.Push(tree);

                if (tree.Left != Empty)
                {
                    stack1.Push(tree.Left);
                }
                if (tree.Right != Empty)
                {
                    stack1.Push(tree.Right);
                }
            }

            while (stack2.Any())
            {
                var tree = stack2.Pop();
                yield return tree.Value;
            }
        }

        public IEnumerable<T> LevelOrder()
        {
            var queue = new Queue<Tree<T>>();
            queue.Enqueue(this);
            while (queue.Any())
            {
                var tree = queue.Dequeue();
                yield return tree.Value;
                if (tree.Left != Empty)
                {
                    queue.Enqueue(tree.Left);
                }
                if (tree.Right != Empty)
                {
                    queue.Enqueue(tree.Right);
                }
            }
        }
    }
```

#### Tests

```csharp
    public class TreeTests
    {
        //           1
        //          / \
        //         /   \
        //        /     \
        //       2       3
        //      / \     /
        //     4   5   6
        //   /       / \
        //  7       8   9

        Tree<int> tree = new Tree<int>(1, new Tree<int>(2, new Tree<int>(4, new Tree<int>(7)), new Tree<int>(5)), new Tree<int>(3, new Tree<int>(6, new Tree<int>(8), new Tree<int>(9))));

        [Fact]
        public void PreorderTest()
        {
            var expected = new List<int> { 1, 2, 4, 7, 5, 3, 6, 8, 9 };
            var actual = tree.Preorder();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void InorderTest()
        {
            var expected = new List<int> { 7, 4, 2, 5, 1, 8, 6, 9, 3 };
            var actual = tree.Inorder();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void PostorderTest()
        {
            var expected = new List<int> { 7, 4, 5, 2, 8, 9, 6, 3, 1 };
            var actual = tree.Postorder();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void LevelorderTest()
        {
            var expected = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var actual = tree.LevelOrder();
            Assert.Equal(expected, actual);
        }
    }
```