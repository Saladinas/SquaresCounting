using DevbridgeSquares.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevbridgeSquares.Repositories
{
    public class PointsDataStore
    {
        public static PointsDataStore Current { get; } = new PointsDataStore();
        public List<PointsList> PointsLists { get; } = new List<PointsList>();
    }
}
