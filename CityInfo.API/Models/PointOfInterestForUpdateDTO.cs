using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Models
{
    public class PointOfInterestForUpdateDTO
    {
	    [Required(ErrorMessage = "You must provide a name.")]
	    [MaxLength(50, ErrorMessage = "Maximum length of 50 characters is exceeded.")]
	    public string Name { get; set; }

	    [MaxLength(200, ErrorMessage = "Maximum length of 200 characters is exceeded.")]
	    public string Description { get; set; }
	}
}
