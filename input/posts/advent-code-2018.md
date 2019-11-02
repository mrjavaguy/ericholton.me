Title: Advent of Code 2018
Published: 12/01/2018
Tags: 
  - Go
---

# Advent of Code 2018

This year I decide to try solving the [Advent of Code](https://adventofcode.com/) puzzles/coding challenges using the [Go](https://golang.org/) programming language. This just being an excuse to learn something about Go as a few of the tools that I use at work use Go for their programming lanuage of choice.

The code for my solutions can be found on my [GitHub repository](https://github.com/mrjavaguy/adventofcode2018)

## Day 1: Chronal Calibration

This was a simple [Map/Reduce](https://en.wikipedia.org/wiki/MapReduce) problem. In fact when discussing with my wife what the advent of code is, I just took my input and pasted it into Excel, sum the values to solve part 1.

## Day 2: Inventory Management System

This was again a [Map/Reduce](https://en.wikipedia.org/wiki/MapReduce) problem with a little more calculations need.

## Day 3: No Matter How You Slice It

At first I wrote code using [Axis Aligned Bounding Boxes](https://en.wikipedia.org/wiki/Minimum_bounding_box) using a sweep and union of rectangles. It worked for the sample input but after letting it run for 45 minutes on my real input, I decided to brute force the problem. I need to come back to this problem again to figure out what I did wrong with the sweep and union.

## Day 4: Repose Record

I solve this by creating a [state machine](https://en.wikipedia.org/wiki/Finite-state_machine) for each guard and then for each part just doing the calculations need once the state at each minute was calculated.

## Day 5: Alchemical Reduction

Another [Map/Reduce](https://en.wikipedia.org/wiki/MapReduce) problem even the title hints at that.

## Day 6: Chronal Coordinates

This puzzle, I brute forced it again by creating a bounding box the was 1 unit bigger than need to contain all the coordinates. For part 1, I calculated the [Manhattan distance](https://en.wikipedia.org/wiki/Taxicab_geometry) for each point and each coordinates in the bounding box. Updating the point with coordinate and distance of the closest. If a coordinate hit the edge of the bounding box, it was eliminated from consideration as the right coordinate. The for the remaining coordinate. I just counted how many points it was closest to. Part 2 was solved in a similar manner.

## Day 7: The Sum of Its Parts

Today's problem is a [topological sorting](https://en.wikipedia.org/wiki/Topological_sorting) problem.

## Day 8: Memory Maneuver

Parse the input into a tree and use a [recursive algorithm](https://en.wikipedia.org/wiki/Recursion_(computer_science)) to solve.

## Day 9: Marble Mania

This was fairly easy to solve using a [circular buffer](https://golang.org/pkg/container/ring/) and following the steps outlined by the puzzle.

## Day 10: The Stars Align

For this puzzle, if you examine the start point and the velocity you can get the approximate second that the stars align and then just move forward to almost that time and find the nearest minimum bounding box at that time.

## Day 11: Chronal Charge

I started this puzzle with brute forcing the problem but the second part required me to think more as it was taking forever to run. By using [summed area table](https://en.wikipedia.org/wiki/Summed-area_table) algorithm, it return in under a second.

## Day 12: Subterranean Sustainability

This was a variation of Conway's game of life for part one. Part two required the answer to part one plus some math (in my case first find the # of plant that increase ever 100 generations)

## Day 13: Mine Cart Madness

Train simulation with crashes. Just took time to run.

## Day 14: Chocolate Charts

Grind out the numbers and return the right set to right for part1 and to the left for part 2

## Day 15: Beverage Bandits

A half-ass RPG, help those elves to survive against those goblins. This one kicked my but for a while as I had not correctly defined my `Less` function for finding the best location to move a combatant toward. Before fixing, it seem like I was getting a random output, but go's `heap.Fix` does not guaranty the order of equal items.

## Day 16: Chronal Classification

Figure out how to translate machine code to assembler and then run the assembler code.

## Day 17: Reservoir Research

Falling water, water does not behave this way..., recursion is your friend.

## Day 18: Settlers of The North Pole

Another variation of Conway's game of Life, this time in 2D. Find the repeating pattern.

## Day 19: Go With The Flow

Oh the Elven assembly language does contain gotos. Just grind it out for part 2.

## Day 20: A Regular Map

Using regular expression to generated a maze and then solve the maze. 

## Day 21: Chronal Conversion

More Elven assembly, this one was easier to figure out the program by hand and then hard code the exit criteria.

