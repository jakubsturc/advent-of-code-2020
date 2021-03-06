﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Xunit;

namespace JakubSturc.AdventOfCode2020
{
    public static class Day17
    {
        public static int Part1()
        {
            var input = Input.ReadLines(day: 17);
            var cycle0 = PocketDimension3d.Parse(input);
            var cycle1 = cycle0.Next();
            var cycle2 = cycle1.Next();
            var cycle3 = cycle2.Next();
            var cycle4 = cycle3.Next();
            var cycle5 = cycle4.Next();
            var cycle6 = cycle5.Next();
            return cycle6.ActiveCells.Count;
        }

        public static int Part2()
        {
            var input = Input.ReadLines(day: 17);
            var cycle0 = PocketDimension4d.Parse(input);
            var cycle1 = cycle0.Next();
            var cycle2 = cycle1.Next();
            var cycle3 = cycle2.Next();
            var cycle4 = cycle3.Next();
            var cycle5 = cycle4.Next();
            var cycle6 = cycle5.Next();
            return cycle6.ActiveCells.Count;
        }

        public record PocketDimension3d(ImmutableHashSet<Cell3d> ActiveCells)
        {
            public static PocketDimension3d Parse(string[] lines)
            {
                var activeCellsBuilder = ImmutableHashSet<Cell3d>.Empty.ToBuilder();

                for (int x = 0; x < lines.Length; x++)
                {
                    for (int y = 0; y < lines[x].Length; y++)
                    {
                        if (lines[x][y] == '#')
                        {
                            activeCellsBuilder.Add(new Cell3d(x, y, 0));
                        }
                    }
                }

                return new PocketDimension3d(activeCellsBuilder.ToImmutable());
            }

            public PocketDimension3d Next()
            {
                var minX = ActiveCells.Min(cell => cell.X);
                var minY = ActiveCells.Min(cell => cell.Y);
                var minZ = ActiveCells.Min(cell => cell.Z);
                var maxX = ActiveCells.Max(cell => cell.X);
                var maxY = ActiveCells.Max(cell => cell.Y);
                var maxZ = ActiveCells.Max(cell => cell.Z);

                var nextGenBuilder = ImmutableHashSet<Cell3d>.Empty.ToBuilder();

                for (int x = minX - 1; x <= maxX + 1; x++)
                {
                    for (int y = minY - 1; y <= maxY + 1; y++)
                    {
                        for (int z = minZ - 1; z <= maxZ + 1; z++)
                        {
                            var cell = new Cell3d(x, y, z);
                            var activeNeighboursCount = cell.GetNeighbours()
                                .Where(neighbour => ActiveCells.Contains(neighbour))
                                .Count();
                            var has2Neighbors = activeNeighboursCount == 2;
                            var has3Neighbors = activeNeighboursCount == 3;
                            var isActive = ActiveCells.Contains(cell);

                            if (isActive && (has2Neighbors || has3Neighbors))
                            {
                                // If a cube is active and exactly 2 or 3 of its neighbors are also active,
                                // the cube remains active. Otherwise, the cube becomes inactive.
                                nextGenBuilder.Add(cell);
                            }
                            else if (!isActive && has3Neighbors)
                            {
                                // If a cube is inactive but exactly 3 of its neighbors are active,
                                // the cube becomes active. Otherwise, the cube remains inactive.
                                nextGenBuilder.Add(cell);
                            }
                        }
                    }
                }

                return new PocketDimension3d(nextGenBuilder.ToImmutable());
            }
        }

        public record PocketDimension4d(ImmutableHashSet<Cell4d> ActiveCells)
        {
            public static PocketDimension4d Parse(string[] lines)
            {
                var activeCellsBuilder = ImmutableHashSet<Cell4d>.Empty.ToBuilder();

                for (int x = 0; x < lines.Length; x++)
                {
                    for (int y = 0; y < lines[x].Length; y++)
                    {
                        if (lines[x][y] == '#')
                        {
                            activeCellsBuilder.Add(new Cell4d(x, y, 0, 0));
                        }
                    }
                }

                return new PocketDimension4d(activeCellsBuilder.ToImmutable());
            }

            public PocketDimension4d Next()
            {
                var minX = ActiveCells.Min(cell => cell.X);
                var minY = ActiveCells.Min(cell => cell.Y);
                var minZ = ActiveCells.Min(cell => cell.Z);
                var minW = ActiveCells.Min(cell => cell.W);
                var maxX = ActiveCells.Max(cell => cell.X);
                var maxY = ActiveCells.Max(cell => cell.Y);
                var maxZ = ActiveCells.Max(cell => cell.Z);
                var maxW = ActiveCells.Max(cell => cell.W);

                var nextGenBuilder = ImmutableHashSet<Cell4d>.Empty.ToBuilder();

                for (int x = minX - 1; x <= maxX + 1; x++)
                {
                    for (int y = minY - 1; y <= maxY + 1; y++)
                    {
                        for (int z = minZ - 1; z <= maxZ + 1; z++)
                        {
                            for (int w = minW - 1; w <= maxW + 1; w++)
                            {
                                var cell = new Cell4d(x, y, z, w);
                                var activeNeighboursCount = cell.GetNeighbours()
                                    .Where(neighbour => ActiveCells.Contains(neighbour))
                                    .Count();
                                var has2Neighbors = activeNeighboursCount == 2;
                                var has3Neighbors = activeNeighboursCount == 3;
                                var isActive = ActiveCells.Contains(cell);

                                if (isActive && (has2Neighbors || has3Neighbors))
                                {
                                    // If a cube is active and exactly 2 or 3 of its neighbors are also active,
                                    // the cube remains active. Otherwise, the cube becomes inactive.
                                    nextGenBuilder.Add(cell);
                                }
                                else if (!isActive && has3Neighbors)
                                {
                                    // If a cube is inactive but exactly 3 of its neighbors are active,
                                    // the cube becomes active. Otherwise, the cube remains inactive.
                                    nextGenBuilder.Add(cell);
                                }
                            }
                        }
                    }
                }

                return new PocketDimension4d(nextGenBuilder.ToImmutable());
            }
        }

        public record Cell3d(int X, int Y, int Z)
        {
            public IEnumerable<Cell3d> GetNeighbours()
            {
                for (int x = -1; x <= 1; x++)
                {
                    for (int y = -1; y <= 1; y++)
                    {
                        for (int z = -1; z <= 1; z++)
                        {
                            if (x == 0 && y == 0 && z == 0) continue;
                            else yield return this with
                            {
                                X = X + x,
                                Y = Y + y,
                                Z = Z + z
                            };
                        }
                    }
                }
            }
        }

        public record Cell4d(int X, int Y, int Z, int W)
        {
            public IEnumerable<Cell4d> GetNeighbours()
            {
                for (int x = -1; x <= 1; x++)
                {
                    for (int y = -1; y <= 1; y++)
                    {
                        for (int z = -1; z <= 1; z++)
                        {
                            for (int w = -1; w <= 1; w++)
                            {
                                if (x == 0 && y == 0 && z == 0 && w == 0) continue;
                                else yield return this with
                                {
                                    X = X + x,
                                    Y = Y + y,
                                    Z = Z + z,
                                    W = W + w
                                };
                            }
                        }
                    }
                }
            }
        }

        public class Tests
        { 
            [Fact]
            public void Part1_Sample1()
            {
                var sample1 = new string[]
                {
                    ".#.",
                    "..#",
                    "###"
                };

                var cycle0 = PocketDimension3d.Parse(sample1);
                var cycle1 = cycle0.Next();
                var cycle2 = cycle1.Next();
                var cycle3 = cycle2.Next();
                var cycle4 = cycle3.Next();
                var cycle5 = cycle4.Next();
                var cycle6 = cycle5.Next();

                Assert.Equal(112, cycle6.ActiveCells.Count);
            }

            [Fact]
            public void Part2_Sample1()
            {
                var sample1 = new string[]
                {
                    ".#.",
                    "..#",
                    "###"
                };

                var cycle0 = PocketDimension4d.Parse(sample1);
                var cycle1 = cycle0.Next();
                var cycle2 = cycle1.Next();
                var cycle3 = cycle2.Next();
                var cycle4 = cycle3.Next();
                var cycle5 = cycle4.Next();
                var cycle6 = cycle5.Next();

                Assert.Equal(848, cycle6.ActiveCells.Count);
            }
        }
    }
}
