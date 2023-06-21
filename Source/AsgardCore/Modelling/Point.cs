using System;
using System.Collections.Generic;
using System.IO;

namespace AsgardCore.Modelling
{
    /// <summary>
    /// Represent a 2D integer point.
    /// </summary>
    public readonly struct Point : ISerializable, IEquatable<Point>
    {
        /// <summary>
        /// The (0, 0) point.
        /// </summary>
        public static readonly Point Origin = new Point(0, 0);

        /// <summary>
        /// The vector for North direction (0, 1).
        /// </summary>
        public static readonly Point North = new Point(0, 1);

        /// <summary>
        /// The vector for North-west direction (-1, 1).
        /// </summary>
        public static readonly Point NorthWest = new Point(-1, 1);

        /// <summary>
        /// The vector for West direction (-1, 0).
        /// </summary>
        public static readonly Point West = new Point(-1, 0);

        /// <summary>
        /// The vector for South-west direction (-1, -1).
        /// </summary>
        public static readonly Point SouthWest = new Point(-1, -1);

        /// <summary>
        /// The vector for South direction (0, -1).
        /// </summary>
        public static readonly Point South = new Point(0, -1);

        /// <summary>
        /// The vector for South-east direction (1, -1).
        /// </summary>
        public static readonly Point SouthEast = new Point(1, -1);

        /// <summary>
        /// The vector for East direction (1, 0).
        /// </summary>
        public static readonly Point East = new Point(1, 0);

        /// <summary>
        /// The vector for North-east direction (1, 1).
        /// </summary>
        public static readonly Point NorthEast = new Point(1, 1);

        /// <summary>
        /// The neighboring fields on a chess board (8 fields).
        /// </summary>
        public static readonly Point[] Directions = new Point[]
        {
            North,
            NorthWest,
            West,
            SouthWest,
            South,
            SouthEast,
            East,
            NorthEast
        };

        /// <summary>
        /// The X coordinate of the <see cref="Point"/>.
        /// </summary>
        public readonly int X;

        /// <summary>
        /// The Y coordinate of the <see cref="Point"/>.
        /// </summary>
        public readonly int Y;

        /// <summary>
        /// Creates a new <see cref="Point"/> with the given coordinates.
        /// </summary>
        /// <param name="x">The x (width) coordinate.</param>
        /// <param name="y">The y (height) coordinate.</param>
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Deserializes a <see cref="Point"/> instance.
        /// </summary>
        public Point(BinaryReader br)
        {
            X = br.ReadInt32();
            Y = br.ReadInt32();
        }

        /// <summary>
        /// A "selector" function used for deserializing <see cref="Point"/>s.
        /// </summary>
        /// <param name="br">The binary-stream reader.</param>
        /// <returns>The deserialized <see cref="Point"/> instance.</returns>
        public static Point Restore(BinaryReader br)
        {
            return new Point(br);
        }

        /// <summary>
        /// Produces the sum of two <see cref="Point"/>s.
        /// </summary>
        public static Point operator +(Point a, Point b)
        {
            return new Point(a.X + b.X, a.Y + b.Y);
        }

        /// <summary>
        /// Produces the difference of two <see cref="Point"/>s.
        /// </summary>
        public static Point operator -(Point a, Point b)
        {
            return new Point(a.X - b.X, a.Y - b.Y);
        }

        /// <summary>
        /// Determines the equality of two <see cref="Point"/>s.
        /// Faster than the <see cref="Equals(object)"/> method.
        /// </summary>
        public static bool operator ==(Point a, Point b)
        {
            return a.X == b.X && a.Y == b.Y;
        }

        /// <summary>
        /// Determines the inequality of two <see cref="Point"/>s.
        /// Faster than the <see cref="Equals(object)"/> method.
        /// </summary>
        public static bool operator !=(Point a, Point b)
        {
            return a.X != b.X || a.Y != b.Y;
        }

        /// <summary>
        /// Calculates the 8 neighbors of the current <see cref="Point"/>.
        /// </summary>
        public List<Point> CalculateNeighbors()
        {
            List<Point> result = new List<Point>(8);
            for (int i = 0; i < 8; i++)
                result.Add(this + Directions[i]);
            return result;
        }

        /// <summary>
        /// Gets the square distance from the other <see cref="Point"/>.
        /// </summary>
        public int SqrDistance(Point p)
        {
            return (X - p.X) * (X - p.X) + (Y - p.Y) * (Y - p.Y);
        }

        /// <summary>
        /// Gets a clone of this <see cref="Point"/>.
        /// </summary>
        public Point Clone()
        {
            return new Point(X, Y);
        }

        public bool Equals(Point other)
        {
            return
                X == other.X &&
                Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Point))
                return false;

            Point other = (Point)obj;
            return
                X == other.X &&
                Y == other.Y;
        }

        public override int GetHashCode()
        {
            // Shift greatly increases distribution.
            // Adding is almost 2x faster than XOR.
            return (X << 16) + Y;
        }

        public override string ToString()
        {
            return "(" + X + ", " + Y + ")";
        }

        public void Serialize(BinaryWriter bw)
        {
            bw.Write(X);
            bw.Write(Y);
        }
    }
}
