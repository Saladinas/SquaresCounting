using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevbridgeSquares.Models;

namespace DevbridgeSquares.Services
{
    public class SquaresService : ISquaresService
    {
        public List<Square> GetSquaresList(List<Point> points)
        {
            HashSet<Point> pointSet = new HashSet<Point>(points); //A hashset of points for quicker lookup
            List<Square> squares = new List<Square>();

            for (int i = 0; i < (pointSet.Count - 1); i++)
            {
                Point pointA = points[i];
                for (int j = i + 1; j < points.Count; j++)
                {
                    Point pointB = points[j];

                    int vectorX = pointB.Y - pointA.Y; // We calculate the vector
                    int vectorY = pointA.X - pointB.X;

                    // One pair of points
                    Point possiblePointC1 = new Point(pointA.X + vectorX, pointA.Y + vectorY);
                    Point possiblePointD1 = new Point(pointB.X + vectorX, pointB.Y + vectorY);

                    // Seconds pair
                    Point possiblePointC2 = new Point(pointA.X - vectorX, pointA.Y - vectorY);
                    Point possiblePointD2 = new Point(pointB.X - vectorX, pointB.Y - vectorY);

                    // if pair exists in a hashset - we found a square
                    if (pointSet.Contains(possiblePointC1) && pointSet.Contains(possiblePointD1))
                    {
                        Square newSquare = new Square(pointA, pointB, possiblePointC1, possiblePointD1);
                        if (squareNotAlreadyExist(newSquare, squares))
                        {
                            squares.Add(newSquare);
                        }
                    }
                    // if pair exists in a hashset - we found a square
                    if (pointSet.Contains(possiblePointC2) && pointSet.Contains(possiblePointD2))
                    {
                        Square newSquare = new Square(pointA, pointB, possiblePointC2, possiblePointD2);
                        if (squareNotAlreadyExist(newSquare, squares))
                        {
                            squares.Add(newSquare);
                        }
                    }
                }
            }
            return squares;
        }

        private bool squareNotAlreadyExist(Square newSquare, List<Square> squares)
        {
            var newSquarePoints = newSquare.GetAllPoints();
            foreach (Square s in squares)
            {
                var squarePoints = s.GetAllPoints();
                //  checking if first list have all same points as second
                var allOfList1IsInList2 = squarePoints.Intersect(newSquarePoints).Count() == squarePoints.Count();
                if (allOfList1IsInList2)
                {
                    return false;
                }
            }
            return true;
        }
    }
}