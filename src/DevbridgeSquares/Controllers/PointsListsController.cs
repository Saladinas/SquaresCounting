using DevbridgeSquares.Models;
using DevbridgeSquares.Repositories;
using DevbridgeSquares.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevbridgeSquares.Controllers
{
    [Route("api/pointsLists")]
    public class PointsListsController : Controller
    {
        private ILogger<PointsController> _logger;
        private static int maxId = 0;

        public PointsListsController(ILogger<PointsController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IActionResult GetPointsLists()
        {
            try
            {
                var pointsLists = PointsDataStore.Current.PointsLists;

                if (pointsLists == null)
                {
                    return NotFound();
                }

                return Ok(pointsLists);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while getting points lists.", ex);
                return StatusCode(500, "A problem happened while handling your request.");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetPointsList(int id)
        {
            try
            {
                var pointsList = PointsDataStore.Current.PointsLists.FirstOrDefault(pl => pl.Id == id);

                if (pointsList == null)
                {
                    return NotFound();
                }

                var pointsListViewModel = new PointsListViewModel
                {
                    Id = pointsList.Id,
                    Name = pointsList.Name,
                    NumberOfPoints = pointsList.Points.Count
                };

                return Ok(pointsListViewModel);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while getting points list with id {id}.", ex);
                return StatusCode(500, "A problem happened while handling your request.");
            }
        }

        [HttpGet("{id}/download")]
        public IActionResult DownloadPointsList(int id)
        {
            try
            {
                string textFile = "";

                var pointsList = PointsDataStore.Current.PointsLists.FirstOrDefault(pl => pl.Id == id);

                if (pointsList == null)
                {
                    return NotFound();
                }

                StringBuilder stringBuilder = new StringBuilder();
                foreach (Point p in pointsList.Points)
                {
                    stringBuilder.AppendLine($"{p.X} {p.Y}");
                }
                textFile = stringBuilder.ToString();

                byte[] bytes = new byte[textFile.Length * sizeof(char)];
                System.Buffer.BlockCopy(textFile.ToCharArray(), 0, bytes, 0, bytes.Length);

                return new FileContentResult(bytes, "text/html");
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while getting points list with id {id}.", ex);
                return StatusCode(500, "A problem happened while handling your request.");
            }
        }

        [HttpPut]
        public IActionResult CreatePointsList(
            [FromBody] PointsListForCreation pointsList)
        {
            try
            {
                if (pointsList == null)
                {
                    return BadRequest();
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var pointsLists = PointsDataStore.Current.PointsLists;

                var listWithSameName = PointsDataStore.Current.PointsLists.FirstOrDefault(pl => pl.Name == pointsList.Name);

                if (listWithSameName != null)
                {
                    pointsLists.Remove(listWithSameName);
                }

                var finalPointsList = new PointsList()
                {
                    Id = ++maxId,
                    Name = pointsList.Name,
                    Points = pointsList.Points
                };

                pointsLists.Add(finalPointsList);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while creating points list in points list.", ex);
                return StatusCode(500, "A problem happened while handling your request.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePointsList(int id)
        {
            try
            {
                var pointsLists = PointsDataStore.Current.PointsLists;
                var pointsList = PointsDataStore.Current.PointsLists.FirstOrDefault(pl => pl.Id == id);

                if (pointsList == null)
                {
                    return NotFound();
                }

                pointsLists.Remove(pointsList);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while deleting points list with id {id}.", ex);
                return StatusCode(500, "A problem happened while handling your request.");
            }
        }

        [HttpPost("clear/{id}")]
        public IActionResult ClearPointsList(int id)
        {
            try
            {
                var pointsList = PointsDataStore.Current.PointsLists.FirstOrDefault(pl => pl.Id == id);

                if (pointsList == null)
                {
                    return NotFound();
                }

                pointsList.Points.Clear();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while clearing points list with id {id}.", ex);
                return StatusCode(500, "A problem happened while handling your request.");
            }
        }

    }
}
