using CityInfo.API.Models;
using System.Collections.Generic;

namespace CityInfo.API
{
	public class CitiesDataStore
    {
		// auto-property initializer syntax
		public static CitiesDataStore Current { get; } = new CitiesDataStore();

		public List<CityDTO> Cities { get; set; }

		public CitiesDataStore()
		{
			// init dummy data
			Cities = new List<CityDTO>()
			{
				new CityDTO()
				{
					Id = 1,
					Name = "New York City",
					Description = "The one with that big park",
					PointsOfInterest = new List<PointOfInterestDTO>()
					{
							new PointOfInterestDTO()
							{
								Id = 4,
								Name = "Carnegie Hall",
								Description = "How Do you get there?"
							},
							new PointOfInterestDTO()
							{
								Id = 5,
								Name = "WTC Memorial",
								Description = "The one that was attacked"
							}
					}							
			},
			new CityDTO()
				{
					Id = 2,
					Name = "Antwerp",
					Description = "The one with that big church that was never finished",
					PointsOfInterest = new List<PointOfInterestDTO>()
					{
							new PointOfInterestDTO()
							{
								Id = 6,
								Name = "Cathedral of our Lady",
								Description = "Gothic Cathedral"
							}							
					}
				},
				new CityDTO()
				{
					Id = 3,
					Name = "Paris",
					Description = "The one with the big tower",
					PointsOfInterest = new List<PointOfInterestDTO>()
					{
							new PointOfInterestDTO()
							{
								Id = 7,
								Name = "Effel Tower",
								Description = "Iconic!"
							},
							new PointOfInterestDTO()
							{
								Id = 8,
								Name = "The Louvre",
								Description = "A really large museum!"
							}
					}
				}
			};

		}

    }
}
