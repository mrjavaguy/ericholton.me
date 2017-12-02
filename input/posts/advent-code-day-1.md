Title: Advent Of Code Day One
Published: 12/01/2017
Tags: 
  - C#
  - advent of code 2017
---

# Advent Of Code

Code Available on [GitHub](https://github.com/mrjavaguy/adventofcode2017)

The night before Christmas, one of Santa's Elves calls you in a panic. "The printer's broken! We can't print the Naughty or Nice List!" By the time you make it to sub-basement 17, there are only a few minutes until midnight. "We have a big problem," she says; "there must be almost fifty bugs in this system, but nothing else can print The List. Stand in this square, quick! There's no time to explain; if you can convince them to pay you in stars, you'll be able to--" She pulls a lever and the world goes blurry.

When your eyes can focus again, everything seems a lot more pixelated than before. She must have sent you inside the computer! You check the system clock: 25 milliseconds until midnight. With that much time, you should be able to collect all fifty stars by December 25th.

Collect stars by solving puzzles. Two puzzles will be made available on each day millisecond in the advent calendar; the second puzzle is unlocked when you complete the first. Each puzzle grants one star. Good luck!

## Inverse Captcha

You're standing in a room with "digitization quarantine" written in LEDs along one wall. The only door is locked, but it includes a small interface. "Restricted Area - Strictly No Digitized Users Allowed."

It goes on to explain that you may only leave by solving a captcha to prove you're not a human. Apparently, you only get one millisecond to solve the captcha: too fast for a normal human, but it feels like hours to you.

### Star One

The captcha requires you to review a sequence of digits (your puzzle input) and find the sum of all digits that match the next digit in the list. The list is circular, so the digit after the last digit is the first digit in the list.

> Note to self, actually read the problem completely and setup unit tests. I made this problem harder by not doing this.

The easiest way to do this would be with a loop, however I decided to use linq for this. To start with I need to pair each character with the characted after it with the added problem of the loop around for the last character to the first character. Normally, pairing is done with calling `Zip` function in linq like `list.Zip(list.Skip(1), (f,s) => (f,s))` but this does not work for a wrap around. Instead this list needs to be rotated one character and then zipped together with the unrotated list. Rotation is done like:

```csharp
        public static IEnumerable<TSource> Rotate<TSource>(this IEnumerable<TSource> enumerable, int amountToRotate) =>
            enumerable.Skip(amountToRotate).Concat(enumerable.Take(amountToRotate));
```

So zipping would be done like:

```csharp
        public static int CalculateInverseCaptcha(string input)
        {
            var rotated = input.Rotate(1);
            return input.Zip(rotated, (first, second) => (first, second)).Select(pair => IsMatching(pair.first, pair.second)).Sum();
        }

        private static int IsMatching(char first, char second) => (first == second) ? first.ToInteger().Match(() => 0, (value) => value) : 0;
```

The `ToInteger()` is a helper function that returns an `Option<int>`.

### Star Two

You notice a progress bar that jumps to 50% completion. Apparently, the door isn't yet satisfied, but it did emit a star as encouragement. The instructions change:

Now, instead of considering the next digit, it wants you to consider the digit halfway around the circular list. That is, if your list contains 10 items, only include a digit in your sum if the digit 10/2 = 5 steps forward matches it. Fortunately, your list has an even number of elements.

This is basically the same problem but rotating the list by one half of the length of the list. So I created a common function for calculating the captcha.

```
        public static int CalculateInverseCaptcha(string input, int pairDistance)
        {
            var rotated = input.Rotate(pairDistance);
            return input.Zip(rotated, (first, second) => (first, second)).Select(pair => IsMatching(pair.first, pair.second)).Sum();
        }
```