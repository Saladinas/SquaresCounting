using DevbridgeSquares.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevbridgeSquares.Services
{
    public class SortingService : ISortingService
    {
        public List<Point> GetSortedPointsList(List<Point> points, bool sortReverse, string sortingProperty)
        {
            switch (sortingProperty)
            {
                case "x":
                    if (sortReverse == true)
                    {
                        points = points.OrderByDescending(p => p.X).ToList();
                        break;
                    }
                    points = points.OrderBy(p => p.X).ToList();
                    break;
                case "y":
                    if (sortReverse == true)
                    {
                        points = points.OrderByDescending(p => p.Y).ToList();
                        break;
                    }
                    points = points.OrderBy(p => p.Y).ToList();
                    break;
                default:
                    break;
            }

            return points;
        }
    }
}
