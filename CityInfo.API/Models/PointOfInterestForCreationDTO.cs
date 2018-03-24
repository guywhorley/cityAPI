using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Models
{
	/// <summary>
	/// 
	/// The DTO class is a place to enfore validation rules via annotations. Error 
	/// messages can be defined as well (see below). The errors must be passed into 
	/// the response via modelstate.
	/// 
	/// </summary>

	public class PointsOfInterestForCreationDTO
	{
		// validation via annotations
		[Required(ErrorMessage = "You must provide a name.")]  
		[MaxLength(50, ErrorMessage = "Maximum length of 50 characters is exceeded.")]
		public string Name { get; set; }

		[MaxLength(200, ErrorMessage = "Maximum length of 200 characters is exceeded.")]
		public string Description { get; set; }
	}
}
