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