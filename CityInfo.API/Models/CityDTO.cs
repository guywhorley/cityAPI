using System.Collections.Generic;

namespace CityInfo.API.Models
{
	/// <summary>
	/// City Data-Transport-Object.
	/// </summary>
	public class CityDTO
    {
		public int Id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public int NumberOfPointsOfInterest
		{
			get
			{
				return PointsOfInterest.Count;
			}
		}
		public ICollection<PointOfInterestDTO> PointsOfInterest { get; set; } = new List<PointOfInterestDTO>(); 
		// is a good idea to intialize the list as empty using auto-property init syntax;

	}
}
