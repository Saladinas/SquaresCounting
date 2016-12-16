using DevbridgeSquares.Models;
using System.Collections.Generic;

namespace DevbridgeSquares.Services
{
    public interface ISortingService
    {
        List<Point> GetSortedPointsList(List<Point> points, bool sortReverse, string sortingProperty);
    }
}