using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using AsgardCore.Modeling;
using AsgardCore.Test;
using NUnit.Framework;

namespace Test.AsgardCore.Modelling
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal sealed class Point_Tests
    {
        private Point _Point;

        [Test]
        public void Type_Constants_HappyPath()
        {
            Assert.AreEqual(new Point(), Point.Origin);
            Assert.AreEqual(new Point(0, 1), Point.North);
            Assert.AreEqual(new Point(1, 1), Point.NorthEast);
            Assert.AreEqual(new Point(1, 0), Point.East);
            Assert.AreEqual(new Point(1, -1), Point.SouthEast);
            Assert.AreEqual(new Point(0, -1), Point.South);
            Assert.AreEqual(new Point(-1, -1), Point.SouthWest);
            Assert.AreEqual(new Point(-1, 0), Point.West);
            Assert.AreEqual(new Point(-1, 1), Point.NorthWest);
        }

        [Test]
        public void Constructor_AllCases_HappyPath()
        {
            _Point = new Point();
            VerifyPoint(0, 0);

            _Point = new Point(42, 28);
            VerifyPoint(42, 28);
        }

        [Test]
        public void Operator_Plus_HappyPath()
        {
            Point p1 = new Point(1, 2);
            Point p2 = new Point(3, 4);

            _Point = p1 + p2;
            VerifyPoint(4, 6);

            _Point = p2 + p1;
            VerifyPoint(4, 6);
        }

        [Test]
        public void Operator_Minus_HappyPath()
        {
            Point p1 = new Point(2, 1);
            Point p2 = new Point(3, 4);

            _Point = p1 - p2;
            VerifyPoint(-1, -3);

            _Point = p2 - p1;
            VerifyPoint(1, 3);
        }

        [Test]
        public void Operator_Equals_HappyPath()
        {
            Assert.IsTrue(new Point(1, 2) == new Point(1, 2));
            Assert.IsFalse(new Point(1, 1) == new Point(1, 2));
            Assert.IsFalse(new Point(2, 2) == new Point(1, 2));
        }

        [Test]
        public void Operator_NotEquals_HappyPath()
        {
            Assert.IsFalse(new Point(1, 2) != new Point(1, 2));
            Assert.IsTrue(new Point(1, 1) != new Point(1, 2));
            Assert.IsTrue(new Point(2, 2) != new Point(1, 2));
        }

        [Test]
        public void Equals_Object_HappyPath()
        {
            _Point = new Point(1, 2);

            Assert.IsFalse(_Point.Equals(null));
            Assert.IsFalse(_Point.Equals(new object()));
#pragma warning disable IDE0004 // Remove Unnecessary Cast
            Assert.IsFalse(_Point.Equals((object)new Point()));
            Assert.IsFalse(_Point.Equals((object)new Point(2, 2)));
            Assert.IsFalse(_Point.Equals((object)new Point(1, 1)));
            Assert.IsTrue(_Point.Equals((object)_Point.Clone()));
#pragma warning restore IDE0004 // Remove Unnecessary Cast
        }

        [Test]
        public void Equals_Point_HappyPath()
        {
            _Point = new Point(1, 2);

            Assert.IsFalse(_Point.Equals(new Point()));
            Assert.IsFalse(_Point.Equals(new Point(2, 2)));
            Assert.IsFalse(_Point.Equals(new Point(1, 1)));
            Assert.IsTrue(_Point.Equals(_Point.Clone()));
        }

        [Test]
        public void GetHashCode_AroundOrigin_ValuesAreUnique()
        {
            HashSet<int> set = new HashSet<int>(10201);
            for (int i = -50; i < 51; i++)
                for (int j = -50; j < 51; j++)
                {
                    Point p = new Point(i, j);
                    int hash = p.GetHashCode();
                    Assert.IsTrue(set.Add(hash), "Hash is not unique for:" + p);
                }
        }

        [Test]
        public void GetHashCode_ExtremeValues_DoesNotThrowException()
        {
            _ = new Point(int.MaxValue, int.MaxValue).GetHashCode();
            _ = new Point(int.MaxValue, int.MinValue).GetHashCode();
            _ = new Point(int.MinValue, int.MaxValue).GetHashCode();
            _ = new Point(int.MinValue, int.MinValue).GetHashCode();

            _ = new Point(32767, int.MaxValue).GetHashCode();
            _ = new Point(32768, int.MaxValue).GetHashCode();
        }

        [Test]
        public void Clone_AllCases_ReturnsExactCopy()
        {
            _Point = new Point(1, 2);
            Point clone = _Point.Clone();

            Assert.AreEqual(_Point, clone);
        }

        [Test]
        public void ToString_AllCases_ReturnsStringRepresentation()
        {
            _Point = new Point(1, 2);

            Assert.AreEqual("(1, 2)", _Point.ToString());
        }

        [Test]
        public void GetNeighbors_Origin_ReturnsPointList()
        {
            List<Point> neighbors = Point.Origin.GetNeighbors();
            CollectionAssert.AreEqual(Point.Directions, neighbors);
        }

        [Test]
        public void GetNeighbors_NotOrigin_ReturnsPointList()
        {
            List<Point> neighbors = new Point(3, 4).GetNeighbors();
            Point[] expectedNeighbors = new[]
            {
                new Point(3, 5),
                new Point(3, 3),
                new Point(4, 5),
                new Point(4, 3),
                new Point(2, 5),
                new Point(2, 3),
                new Point(4, 4),
                new Point(2, 4)
            };
            CollectionAssert.AreEquivalent(expectedNeighbors, neighbors);
        }

        [Test]
        public void SqrDistance_DifferentPoints_ReturnsSquareDistance()
        {
            Point p1 = new Point(5, 1);
            Point p2 = new Point(2, 5);

            Assert.AreEqual(25, p1.SqrDistance(p2));
        }

        [Test]
        public void Serialization_AllCases_HappyPath()
        {
            _Point = new Point(1, 2);

            TestCommon.SerializationCore(ref _Point, Point.Restore);

            VerifyPoint(1, 2);
        }

        private void VerifyPoint(int x, int y)
        {
            Assert.AreEqual(x, _Point.X);
            Assert.AreEqual(y, _Point.Y);
        }
    }
}
