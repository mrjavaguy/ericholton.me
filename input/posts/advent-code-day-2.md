Title: Advent Of Code Day Two
Published: 12/02/2017
Tags: 
  - C#
  - advent of code 2017
---

# Advent Of Code

As you walk through the door, a glowing humanoid shape yells in your direction. "You there! Your state appears to be idle. Come help us repair the corruption in this spreadsheet - if we take another millisecond, we'll have to display an hourglass cursor!"

## Corruption Checksum

You're standing in a room with "digitization quarantine" written in LEDs along one wall. The only door is locked, but it includes a small interface. "Restricted Area - Strictly No Digitized Users Allowed."

### Star One

The spreadsheet consists of rows of apparently-random numbers. To make sure the recovery process is on the right track, they need you to calculate the spreadsheet's checksum. For each row, determine the difference between the largest value and the smallest value; the checksum is the sum of all of these differences.

```csharp
        public static IEnumerable<int> Parser(string s) =>
            s.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries).Select(n => int.Parse(n));

        public static int Star1(string[] input) => 
            input
                .Select(s => Parser(s))
                .Select(l => (Max: l.Max(), Min: l.Min()))
                .Select(m => m.Max - m.Min).Sum();
```

### Star Two

"Great work; looks like we're on the right track after all. Here's a star for your effort." However, the program seems a little worried. Can programs be worried?

"Based on what we're seeing, it looks like all the User wanted is some information about the evenly divisible values in the spreadsheet. Unfortunately, none of us are equipped for that kind of calculation - most of us specialize in bitwise operations."

It sounds like the goal is to find the only two numbers in each row where one evenly divides the other - that is, where the result of the division operation is a whole number. They would like you to find those numbers on each line, divide them, and add up each line's result.

In this case we need to get permutations of all the integers in a single line of the spreadsheet.

```csharp
        public static IEnumerable<IEnumerable<T>> Permutations<T>(this IEnumerable<T> elements, int size) =>
            size == 0 ? new[] { new T[0] } :
              elements.SelectMany((e, i) =>
                elements.Skip(i + 1).Permutations(size - 1).Select(c => (new[] { e }).Concat(c)));
```

```csharp
        public static int Star2(string[] input) => 
            input
                .Select(s => Parser(s))
                .Select(l => l.Permutations(2).Where(p => p.First() % p.Last() == 0 || p.Last() % p.First() == 0).Select(p => (Max: p.Max(), Min: p.Min())).Select(m => m.Max / m.Min).First())
                .Sum();
```