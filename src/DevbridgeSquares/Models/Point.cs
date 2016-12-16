using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DevbridgeSquares.Models
{
    public class Point
    {
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        [Required(ErrorMessage = "Coordinate X value is required.")]
        [Range(-5000, 5000)]
        public int X { get; set; }
        [Required(ErrorMessage = "Coordinate Y value is required.")]
        [Range(-5000, 5000)]
        public int Y { get; set; }

        public override bool Equals(System.Object obj)
        {
            if (obj == null)
            {
                return false;
            }

            Point p = obj as Point;
            if ((System.Object)p == null)
            {
                return false;
            }

            return (X == p.X) && (Y == p.Y);
        }

        public bool Equals(Point p)
        {
            if ((object)p == null)
            {
                return false;
            }

            return (X == p.X) && (Y == p.Y);
        }

        public override int GetHashCode()
        {
            return X ^ Y;
        }

        public static bool operator ==(Point a, Point b)
        {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(a, b))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }

            // Return true if the fields match:
            return a.X == b.X && a.Y == b.Y;
        }

        public static bool operator !=(Point a, Point b)
        {
            return !(a == b);
        }
    }
}

