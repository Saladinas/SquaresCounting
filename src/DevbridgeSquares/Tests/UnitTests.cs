using DevbridgeSquares.Models;
using DevbridgeSquares.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DevbridgeSquares.Tests
{
    public class UnitTests
    {
        [Fact]
        public void PointsShoudBeEquals()
        {
            Point p1 = new Point(0, 1);
            Point p2 = new Point(0, 1);

            Assert.Equal(p1, p2);
        }

        [Fact]
        public void PointsShoudNotBeEquals()
        {
            Point p1 = new Point(0, 1);
            Point p2 = new Point(0, -1);

            Assert.NotEqual(p1, p2);
        }

        [Fact]
        public void OneSquareShouldBeFound()
        {
            var _squaresService = new SquaresService();

            Point p1 = new Point(0, 0);
            Point p2 = new Point(1, 0);
            Point p3 = new Point(0, 1);
            Point p4 = new Point(1, 1);
            List<Point> pointsList = new List<Point> { p1, p2, p3, p4 };

            Assert.Equal(_squaresService.GetSquaresList(pointsList).Count, 1);
        }

        [Fact]
        public void ZeroSquaresShouldBeFound()
        {
            var _squaresService = new SquaresService();

            Point p1 = new Point(0, 0);
            Point p2 = new Point(1, 0);
            Point p3 = new Point(0, 5);
            Point p4 = new Point(1, 1);
            List<Point> pointsList = new List<Point> { p1, p2, p3, p4 };

            Assert.Equal(_squaresService.GetSquaresList(pointsList).Count, 0);
        }

        [Fact]
        public void TwoSquaresShouldBeFound()
        {
            var _squaresService = new SquaresService();

            Point p1 = new Point(0, 0);
            Point p2 = new Point(1, 0);
            Point p3 = new Point(0, 1);
            Point p4 = new Point(1, 1);
            Point p5 = new Point(2, 2);
            Point p6 = new Point(2, 0);
            Point p7 = new Point(0, 2);
            List<Point> pointsList = new List<Point> { p1, p2, p3, p4 , p5, p6, p7};

            Assert.Equal(_squaresService.GetSquaresList(pointsList).Count, 2);
        }

        [Fact]
        public void PointsShouldBeSortedByX()
        {
            var _sortingService = new SortingService();

            Point p1 = new Point(0, 0);
            Point p2 = new Point(-2, 2);

            List<Point> pointsList = new List<Point> { p1, p2 };
            pointsList = _sortingService.GetSortedPointsList(pointsList, false, "x");

            Assert.Equal(pointsList[0].X, -2);
        }

        [Fact]
        public void PointsShouldBeSortedReverseByX()
        {
            var _sortingService = new SortingService();

            Point p1 = new Point(0, 0);
            Point p2 = new Point(2, 2);

            List<Point> pointsList = new List<Point> { p1, p2 };
            pointsList = _sortingService.GetSortedPointsList(pointsList, true, "x");

            Assert.Equal(pointsList[0].X, 2);
        }

        [Fact]
        public void PointsShouldBeSortedByY()
        {
            var _sortingService = new SortingService();

            Point p1 = new Point(0, 4);
            Point p2 = new Point(-2, 2);

            List<Point> pointsList = new List<Point> { p1, p2 };
            pointsList = _sortingService.GetSortedPointsList(pointsList, false, "y");

            Assert.Equal(pointsList[0].Y, 2);
        }

        [Fact]
        public void PointsShouldBeSortedReverseByY()
        {
            var _sortingService = new SortingService();

            Point p1 = new Point(0, 4);
            Point p2 = new Point(2, 8);

            List<Point> pointsList = new List<Point> { p1, p2 };
            pointsList = _sortingService.GetSortedPointsList(pointsList, true, "y");

            Assert.Equal(pointsList[0].Y, 8);
        }
    }
}
