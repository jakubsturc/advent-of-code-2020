using System;
using System.Linq;
using Xunit;

namespace JakubSturc.AdventOfCode2020
{
    public static class Day12
    {
        public static int Part1() => Part1(Input.ReadLines(day: 12));

        public static int Part1(string[] lines)
        {         
            var dest = lines
                .Select(Command.Parse)
                .Aggregate(Ship.Init, (ship, command) => ship.Apply(command));
            return Math.Abs(dest.Pos.North) + Math.Abs(dest.Pos.East);
        }

        public static int Part2()
        {
            throw new NotImplementedException();
        }

        public record Command(char C, int Val)
        {
            public static Command Parse(string str) => new Command(str[0], int.Parse(str[1..]));
        }

        public record Ship(Position Pos, Direction Dir)
        {
            public static Ship Init { get => new Ship(new Position(North: 0, East: 0), new Direction(Deg: 0)); }

            public Ship Apply(Command command) => command switch 
            {
                ('R', var val) => new Ship(Pos, Dir.TurnRight(val)),
                ('L', var val) => new Ship(Pos, Dir.TurnLeft(val)),
                ('N', var val) => new Ship(Pos.MoveNorth(val), Dir),                
                ('S', var val) => new Ship(Pos.MoveSouth(val), Dir),
                ('E', var val) => new Ship(Pos.MoveEast(val), Dir),
                ('W', var val) => new Ship(Pos.MoveWest(val), Dir),
                ('F', var val) => new Ship(Pos.Move(Dir, val), Dir),
                _ => throw new NotSupportedException()
            };
        }


        public record Position(int North, int East)
        {
            public Position MoveNorth(int dst) => new Position(North + dst, East);
            
            public Position MoveSouth(int dst) => new Position(North - dst, East);

            public Position MoveEast(int dst) => new Position(North, East + dst);

            public Position MoveWest(int dst) => new Position(North, East - dst);

            internal Position Move(Direction dir, int dst) => dir.Deg switch
            { 
                0   => MoveEast(dst),
                90  => MoveSouth(dst),
                180 => MoveWest(dst),
                270 => MoveNorth(dst),
                _ => throw new NotSupportedException()
            };
        }

        /// <summary>
        /// E: 0, S: 90, W:180, N: 270
        /// </summary>
        public record Direction(int Deg)
        {
            public Direction TurnRight(int deg) => new Direction((Deg + deg) % 360);
            
            public Direction TurnLeft(int deg) => new Direction((Deg - deg + 360) % 360);
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
