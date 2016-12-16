using DevbridgeSquares.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevbridgeSquares.Services
{
    public interface ISquaresService
    {
        List<Square> GetSquaresList(List<Point> points);
    }
}
