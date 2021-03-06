﻿using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.Entities
{
	public class CityInfoContext : DbContext
    {
		public DbSet<City> Cities { get; set; }
		public DbSet<PointOfInterest> PointsOfInterest { get; set; }

	    public CityInfoContext(DbContextOptions<CityInfoContext> options) : base(options)
	    {
			// will create the db if not already present
			Database.Migrate();

	    }

		// one way to define connection string for DB.
	    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	    //{
		   // optionsBuilder.UseSqlServer("connectionstring");
		   // base.OnConfiguring(optionsBuilder);
	    //}


		
    }
}
