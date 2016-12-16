using DevbridgeSquares.Models;
using DevbridgeSquares.Repositories;
using static Microsoft.AspNetCore.Http.StatusCodes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevbridgeSquares.ViewModels;
using DevbridgeSquares.Services;
using System.Text.RegularExpressions;
using System.Text;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace DevbridgeSquares.Controllers
{
    [Route("api/pointsLists")]
    public class PointsController : Controller
    {
        private const int maxPointsInList = 10000;
        private ILogger<PointsController> _logger;
        private ISortingService _sortingService;
        private ISquaresService _squaresService;

        public PointsController(ILogger<PointsController> logger, ISortingService sortingService, ISquaresService squaresService)
        {
            _logger = logger;
            _sortingService = sortingService;
            _squaresService = squaresService;
        }

        [HttpGet("{listId}/points")]
        public IActionResult GetPoints(int listId)
        {
            try
            {
                var pageSize = Convert.ToInt32(Request.Query["pageSize"]);
                var page = Convert.ToInt32(Request.Query["page"]);
                bool sortReverse = Convert.ToBoolean(Request.Query["sortReverse"]);
                string sortingProperty = Request.Query["sortType"];

                var pointsList = PointsDataStore.Current.PointsLists.FirstOrDefault(pl => pl.Id == listId);

                if (pointsList == null)
                {
                    return NotFound();
                }

                var points = pointsList.Points;
                points = _sortingService.GetSortedPointsList(points, sortReverse, sortingProperty);

                var pager = new Pager(points.Count(), page, pageSize);
                var pointsViewModel = new PagingViewModel<Point>
                {
                    Items = points.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize),
                    Pager = pager
                };

                return Ok(pointsViewModel);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while getting points from points list with id {listId}.", ex);
                return StatusCode(500, "A problem happened while handling your request.");
            }
        }

        [HttpGet("{listId}/squares")]
        public IActionResult GetSquares(int listId)
        {
            try
            {
                var pageSize = Convert.ToInt32(Request.Query["pageSize"]);
                var page = Convert.ToInt32(Request.Query["page"]);
                bool sortReverse = Convert.ToBoolean(Request.Query["sortReverse"]);
                string sortingProperty = Request.Query["sortType"];

                var pointsList = PointsDataStore.Current.PointsLists.FirstOrDefault(pl => pl.Id == listId);

                if (pointsList == null)
                {
                    return NotFound();
                }

                var squares = _squaresService.GetSquaresList(pointsList.Points);

                var pager = new Pager(squares.Count(), page, pageSize);
                var pointsViewModel = new PagingViewModel<Square>
                {
                    Items = squares.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize),
                    Pager = pager
                };

                return Ok(pointsViewModel);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while getting squares from points list with id {listId}.", ex);
                return StatusCode(500, "A problem happened while handling your request.");
            }
        }

        [HttpPost("{listId}/points")]
        public IActionResult CreatePoint(int listId,
            [FromBody] Point point)
        {
            try
            {
                if (point == null)
                {
                    return BadRequest();
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var pointsList = PointsDataStore.Current.PointsLists.FirstOrDefault(pl => pl.Id == listId);

                if (pointsList == null)
                {
                    return NotFound();
                }

                if (pointsList.Points.Any(p => p == point))
                {
                    ModelState.AddModelError("coordinates", "There are point with same coordinates.");
                    Response.StatusCode = Status400BadRequest;
                    return BadRequest(ModelState);
                }

                if (pointsList.Points.Count >= maxPointsInList)
                {
                    ModelState.AddModelError("points", String.Format("You cannot add more points than {0}.", maxPointsInList));
                    Response.StatusCode = Status400BadRequest;
                    return BadRequest(ModelState);
                }

                pointsList.Points.Add(point);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while creating point in points list with id {listId}.", ex);
                return StatusCode(500, "A problem happened while handling your request.");
            }
        }

        [HttpPut("{listId}/points")]
        public IActionResult DeletePoint(int listId,
            [FromBody] Point point)
        {
            try
            {
                var pointsList = PointsDataStore.Current.PointsLists.FirstOrDefault(pl => pl.Id == listId);

                if (pointsList == null)
                {
                    return NotFound();
                }

                var pointFromStore = pointsList.Points.FirstOrDefault(p => p == point);

                if (pointFromStore == null)
                {
                    return NotFound();
                }

                pointsList.Points.Remove(pointFromStore);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while deleting point from points list with id {listId} or point with coordinates ({point.X};{point.Y}).", ex);
                return StatusCode(500, "A problem happened while handling your request.");
            }
        }

        [HttpPost("{id}/upload")]
        public IActionResult UploadPoints(int id,
       [FromBody] FileModel base64file)
        {
            try
            {
                var pointsList = PointsDataStore.Current.PointsLists.FirstOrDefault(pl => pl.Id == id);
                List<string> dublicateLinesNumbers = new List<string>();
                List<string> wrongFormatLinesNumbers = new List<string>();

                if (pointsList == null)
                {
                    return NotFound();
                }

                base64file.Base64File = Regex.Replace(base64file.Base64File, @"^data:text\/[a-zA-Z]+;base64,", string.Empty);
                var pointsLines = Encoding.ASCII.GetString(Convert.FromBase64String(base64file.Base64File))
                            .Split(new string[] { "\r\n" }, StringSplitOptions.None).ToList();

                for (var i = 0; i < pointsLines.Count; i++)
                {
                    string[] splitedPoints = pointsLines[i].Split(new char[] { ' ' }, 2);
                    int firstCoordinate, secondCoordinate;
                    if ((Int32.TryParse(splitedPoints[0], out firstCoordinate)) && (Int32.TryParse(splitedPoints[1], out secondCoordinate)))
                    {
                        if (pointsList.Points.Any(p => p.X == firstCoordinate && p.Y == secondCoordinate))
                        {
                            dublicateLinesNumbers.Add((i + 1).ToString());
                            continue;
                        }

                        if (pointsList.Points.Count >= maxPointsInList)
                        {
                            CheckDublicateLinesNumbers(dublicateLinesNumbers);
                            CheckWrongFormatLinesNumbers(wrongFormatLinesNumbers);
                            ModelState.AddModelError("points", String.Format("You cannot add more points than {0}.", maxPointsInList));
                            Response.StatusCode = Status400BadRequest;
                            return BadRequest(ModelState);
                        }

                        var point = new Point(firstCoordinate, secondCoordinate);
                        if (PointsValidation(point))
                        {
                            pointsList.Points.Add(point);
                            continue;
                        }
                        wrongFormatLinesNumbers.Add((i + 1).ToString());
                        continue;
                    }
                    else
                    {
                        wrongFormatLinesNumbers.Add((i + 1).ToString());
                        continue;
                    }
                }
                if (dublicateLinesNumbers.Count > 0 || wrongFormatLinesNumbers.Count > 0)
                {
                    CheckDublicateLinesNumbers(dublicateLinesNumbers);
                    CheckWrongFormatLinesNumbers(wrongFormatLinesNumbers);
                    return BadRequest(ModelState);
                }
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while uploading points from file to list with id {id}.", ex);
                return StatusCode(500, "A problem happened while handling your request.");
            }
        }

        private bool PointsValidation(Point point)
        {
            var context = new ValidationContext(point, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            return Validator.TryValidateObject(point, context, results, true);
        }

        private ModelStateDictionary CheckDublicateLinesNumbers(List<string> dublicateLinesNumbers)
        {
            if (dublicateLinesNumbers.Count > 0)
            {
                ModelState.AddModelError("dublicateLines", String.Format("There are already points with same coordinates as in file at lines {0}.", String.Join(", ", dublicateLinesNumbers.ToArray())));
            }
            return ModelState;
        }

        private ModelStateDictionary CheckWrongFormatLinesNumbers(List<string> wrongFormatLinesNumbers)
        {
            if (wrongFormatLinesNumbers.Count > 0)
            {
                ModelState.AddModelError("wrontFormatLines", String.Format("There are bad format coordinates in file at lines {0}.", String.Join(", ", wrongFormatLinesNumbers.ToArray())));
            }
            return ModelState;
        }
    }
}
