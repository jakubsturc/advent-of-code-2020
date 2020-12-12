using System;
using System.Linq;
using Xunit;

namespace JakubSturc.AdventOfCode2020
{
    public static class Day12
    {
        public static int Part1() => Part1(Input.ReadLines(day: 12));

        public static int Part2() => Part2(Input.ReadLines(day: 12));

        public static int Part1(string[] lines)
        {         
            var dest = lines
                .Select(Command.Parse)
                .Aggregate(DirectShip.Init, (ship, command) => ship.Apply(command));
            return Math.Abs(dest.Pos.North) + Math.Abs(dest.Pos.East);
        }

        public static int Part2(string[] lines)
        {
            var dest = lines
                .Select(Command.Parse)
                .Aggregate(IndirectShip.Init, (ship, command) => ship.Apply(command));
            return Math.Abs(dest.Sp.North) + Math.Abs(dest.Sp.East);
        }

        public record Command(char C, int Val)
        {
            public static Command Parse(string str) => new Command(str[0], int.Parse(str[1..]));
        }

        public record DirectShip(Position Pos, Direction Dir)
        {
            public static DirectShip Init { get => new DirectShip(new Position(North: 0, East: 0), new Direction(Deg: 0)); }

            public DirectShip Apply(Command command) => command switch 
            {
                ('R', var val) => new DirectShip(Pos, Dir.Rotate(val)),
                ('L', var val) => new DirectShip(Pos, Dir.Rotate(-val)),
                ('N', var val) => new DirectShip(Pos.MoveNorth(val), Dir),
                ('S', var val) => new DirectShip(Pos.MoveSouth(val), Dir),
                ('E', var val) => new DirectShip(Pos.MoveEast(val), Dir),
                ('W', var val) => new DirectShip(Pos.MoveWest(val), Dir),
                ('F', var val) => new DirectShip(Pos.Move(Dir, val), Dir),
                _ => throw new NotSupportedException()
            };
        }

        public record IndirectShip(Position Sp, Position Wp) 
        {
            public static IndirectShip Init { get => new IndirectShip(new Position(North: 0, East: 0), new Position(North: 1, East: 10)); }

            public IndirectShip Apply(Command command) => command switch
            {
                ('R', var val) => this with { Wp = Wp.Rotate(val) },
                ('L', var val) => this with { Wp = Wp.Rotate(-val) },
                ('N', var val) => this with { Wp = Wp.MoveNorth(val)},
                ('S', var val) => this with { Wp = Wp.MoveSouth(val)},
                ('E', var val) => this with { Wp = Wp.MoveEast(val) },
                ('W', var val) => this with { Wp = Wp.MoveWest(val) },
                ('F', var val) => this with { Sp = Sp.Move(Wp, val) },
                _ => throw new NotSupportedException()
            };
        }

        public record Position(int North, int East)
        {
            public Position MoveNorth(int dst) => this with { North = North + dst };
            
            public Position MoveSouth(int dst) => this with { North = North - dst };

            public Position MoveEast(int dst) => this with { East = East + dst };

            public Position MoveWest(int dst) => this with { East = East - dst };

            public Position Move(Direction dir, int dst) => dir.Deg switch
            { 
                0   => MoveEast(dst),
                90  => MoveSouth(dst),
                180 => MoveWest(dst),
                270 => MoveNorth(dst),
                _ => throw new NotSupportedException()
            };

            public Position Move(Position waypoint, int dst) => this with
            {
                North = North + dst * waypoint.North,
                East = East + dst * waypoint.East
            };

            public Position Rotate(int deg) => ((deg + 360) % 360) switch
            {
                0   => this,
                90  => this with { North = -East, East = North },
                180 => this with { North = -North, East = -East },
                270 => this with { North = East, East = -North },
                _ => throw new NotSupportedException()
            };
        }

        /// <summary>
        /// E: 0, S: 90, W:180, N: 270
        /// </summary>
        public record Direction(int Deg)
        {
            public Direction Rotate(int deg) => new Direction((Deg + deg + 360) % 360);
        }

        public class Tests
        { 
            [Fact]
            public void Part1_Sample1()
            {
                var lines = new string[] { "F10", "N3", "F7", "R90", "F11" };
                Assert.Equal(25, Part1(lines));
            }
        }

    }
}
