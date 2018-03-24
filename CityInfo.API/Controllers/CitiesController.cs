using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CityInfo.API.Controllers
{
	//[Produces("application/json")]
	[Route("api/cities")] // attribute-level 'routing' attribute mask that applies to all routes
	public class CitiesController : Controller
	{	
		[HttpGet()]
		public IActionResult GetCities()
		{
			//JsonResult temp = new JsonResult(CitiesDataStore.Current.Cities);
			//temp.StatusCode = 200;
			//return temp;
			return Ok(CitiesDataStore.Current.Cities);
		}

		[HttpGet("{id}")]  // params use {} in the routing template 
		public IActionResult GetCity(int id)
		{
			// using LINQ, you can iterate over a collection and get the matching item
			var cityToReturn = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == id);

			// controller has built-in methods for standard return types
			// by using the built-in return codes of IActionResult, the underlying 
			// presentation can change (Json vs Html vs Text...)

			// 404 - not found
			if (cityToReturn == null)
				return NotFound(); 
			// 200 - ok
			return Ok(cityToReturn);
		}
    }
}