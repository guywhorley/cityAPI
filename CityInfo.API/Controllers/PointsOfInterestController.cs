using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityInfo.API.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CityInfo.API.Controllers
{
	[Route("api/cities")]
	public class PointsOfInterestController : Controller
	{	
		public ILogger<PointsOfInterestController> Logger { get; private set;  } 

		// "constructor" injection - the Container will provide the logger.
		public PointsOfInterestController(ILogger<PointsOfInterestController> logger)
		{
			Logger = logger;
		}

		public IActionResult Index()
		{
			return View();
		}

		[HttpGet("{cityId}/pointsofinterest")]
		public IActionResult GetPointsOfInterest(int cityId)
		{
			try
			{
				throw new Exception("Test Exception");
				var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
				if (city == null)
				{
					Logger.LogInformation($"City not found! [id={cityId}]");
					return NotFound();
				}

				return Ok(city.PointsOfInterest);
			}
			catch (Exception e)
			{
				Logger.LogCritical($"Exception while getting points of interest for city with id {cityId}", e);
				return StatusCode(500, "A problem occured while handling your request.");
			}
		}

		[HttpGet("{cityId}/pointsofinterest/{id}", Name = "GetPointOfInterest")]
		public IActionResult GetPointOfInterest(int cityId, int id)
		{
			var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
			if (city == null)
				return NotFound("City not found.");

			var poi = city.PointsOfInterest.FirstOrDefault(p => p.Id == id);
			if (poi == null)
				return NotFound("Point of interest not found.");

			return Ok(poi);
		}

		[HttpPost("{cityId}/pointsofinterest")]
		public IActionResult CreatePointOfInterest(int cityId,
					[FromBody] PointsOfInterestForCreationDTO pointsOfInterest)
		{
			//ERROR CHECKING...
			// null poi
			if (pointsOfInterest == null)
				return BadRequest("Point of Interest not defined.");

			// Adding a customer error and customized validation checking
			if (pointsOfInterest.Description == pointsOfInterest.Name)
			{
				ModelState.AddModelError("Description", "The provided description cannot be the same as the name.");
			}

			// ModelState is a dictionary containing various data
			// If model validation fails, isvalid will be false.
			if (!ModelState.IsValid)
			{
				// will pass along any error messages in modelstate to the response
				return BadRequest(ModelState); 
			}

			// city not found
			var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
			if (city == null)
				return NotFound("City not found.");

			// need to get next poi id...
			var maxPoiId = CitiesDataStore.Current.Cities.SelectMany(c =>
				c.PointsOfInterest).Max(p => p.Id);

			var finalPoi = new PointOfInterestDTO()
			{
				Id = ++maxPoiId,
				Name = pointsOfInterest.Name,
				Description = pointsOfInterest.Description
			};

			city.PointsOfInterest.Add(finalPoi);
			// this is the method to return a '201 created' response with the object in the response body
			return CreatedAtRoute("GetPointOfInterest",
				new { cityId, id = finalPoi.Id }, finalPoi);
		}

		[HttpPut("{cityId}/pointsofinterest/{id}")] // Acc. to HTTP standard, PUT should fully update the resource
		public IActionResult UpdatePointOfInterest(int cityId, int id,
			[FromBody] PointOfInterestForUpdateDTO pointOfInterest)
		{
			// null poi
			if (pointOfInterest == null)
				return BadRequest("Point of Interest not defined.");

			// Adding a customer error and customized validation checking
			if (pointOfInterest.Description == pointOfInterest.Name)
			{
				ModelState.AddModelError("Description", "The provided description cannot be the same as the name.");
			}

			// ModelState is a dictionary containing various data
			// If model validation fails, isvalid will be false.
			if (!ModelState.IsValid)
			{
				// will pass along any error messages in modelstate to the response
				return BadRequest(ModelState);
			}

			// city not found
			var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
			if (city == null)
			{
				return NotFound("City not found.");
			}
			
			// poi not found
			var poiFromStore = city.PointsOfInterest.FirstOrDefault(p => p.Id == id);
			if (poiFromStore == null)
			{
				return NotFound("Point of interest not found.");
			}

			// Update ...
			poiFromStore.Name = pointOfInterest.Name;
			poiFromStore.Description = pointOfInterest.Description;

			// succesful operation but no return content.
			return NoContent();
		}

		[HttpPatch("{cityId}/pointsofinterest/{id}")]
		public IActionResult PartiallyUpdatePointOfInterest(int cityId, int id,
			[FromBody] JsonPatchDocument<PointOfInterestForUpdateDTO> patchDoc)
		{
			// patchDoc is null
			if (patchDoc == null)
			{
				return BadRequest("patchDoc cannot be null.");
			}
			// city not found
			var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
			if (city == null)
			{
				return NotFound("City not found.");
			}
			
			// poi not found
			var poiFromStore = city.PointsOfInterest.FirstOrDefault(p => p.Id == id);
			if (poiFromStore == null)
			{
				return NotFound("Point of interest not found.");
			}

			// using a poi that has an id and just not 'passing it' when instantiating the other DTO type.
			var pointOfInterestToPatch =
				new PointOfInterestForUpdateDTO() 
				{
					Name = poiFromStore.Name,
					Description = poiFromStore.Description
				};

			// if request was not valid, model state will be updated
			patchDoc.ApplyTo(pointOfInterestToPatch, ModelState); 
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			// Adding a customer error and customized validation checking
			if (pointOfInterestToPatch.Description == pointOfInterestToPatch.Name)
			{
				ModelState.AddModelError("Description", "The provided description cannot be the same as the name.");
			}

			// run validation - trigger it manually...
			TryValidateModel(pointOfInterestToPatch);
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			
			// Update ...
			poiFromStore.Name = pointOfInterestToPatch.Name;
			poiFromStore.Description = pointOfInterestToPatch.Description;
			return NoContent();
		}

		[HttpDelete("{cityId}/pointsofinterest/{id}")]
		public IActionResult DeletePointOfInterest(int cityId, int id)
		{
			// city not found
			var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
			if (city == null)
			{
				return NotFound("City not found.");
			}

			// poi not found
			var poiFromStore = city.PointsOfInterest.FirstOrDefault(p => p.Id == id);
			if (poiFromStore == null)
			{
				return NotFound("Point of interest not found.");
			}

			city.PointsOfInterest.Remove(poiFromStore);
			return NoContent();
		}


	}
}