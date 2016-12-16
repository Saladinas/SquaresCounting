using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevbridgeSquares.Models
{
    public class Square
    {
        public Square(Point pA, Point pB, Point pC, Point pD)
        {
            PointA = pA;
            PointB = pB;
            PointC = pC;
            PointD = pD;
        }

        public Point PointA { get; set; }
        public Point PointB { get; set; }
        public Point PointC { get; set; }
        public Point PointD { get; set; }

        public List<Point> GetAllPoints()
        {
            return new List<Point> { PointA, PointB, PointC, PointD };
        }
    }
}
