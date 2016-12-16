using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DevbridgeSquares.Models
{
    public class PointsListForCreation
    {
        [Required(ErrorMessage = "You should provide a name value.")]
        [MaxLength(50, ErrorMessage = "Name cannot be longer than 50 symbols.")]
        public string Name { get; set; }
        public int NumberOfPoints
        {
            get
            {
                return Points.Count;
            }
        }
        [MaxLength(10000)]
        public List<Point> Points { get; set; } = new List<Point>();
    }
}
