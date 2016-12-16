using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevbridgeSquares.Models
{
    public class PointsList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumberOfPoints { get
            {
                return Points.Count;
            }
        }
        public List<Point> Points { get; set; } = new List<Point>();
    }
}
