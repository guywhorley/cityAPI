﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CityInfo.API
{
	public class Startup
	{
		// (added by VS)
		// This method gets called by the runtime. Use this method to ADD services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			// adding MVC middleware
			//services.AddMvc(); // default, json output

			// XML Output Support - to use xml output on API response, ensure that the request 
			// has an "Accept=application/xml" header, otherwise will default to application/json
			services.AddMvc(options =>
			{
				options.RespectBrowserAcceptHeader = true;
				options.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
				//options.InputFormatters.Add(new XmlSerializerInputFormatter());
			});


			// the following code converst the property names to first letter upper case
			//.AddJsonOptions(o => {
			//	if (o.SerializerSettings.ContractResolver != null)
			//	{
			//		var castedResolver = o.SerializerSettings.ContractResolver as DefaultContractResolver;
			//		castedResolver.NamingStrategy = null;
			//	}
			//});
		}

		// (added by VS)
		// This method gets called by the runtime. Use this method to CONFIGURE the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			//loggerFactory.AddConsole();
			//loggerFactory.AddDebug();

			// Configure middleware based on different environments
			// (See project properties debug tab to set the enviornment value ("Development | Staging | Production")
			if (env.IsDevelopment())
			{
				// configures devExpPage middleware (diagnostics middleware)
				app.UseStatusCodePages(); // display error codes when they occur
				app.UseDeveloperExceptionPage();
			}
			else
			{
				// no helpful info on this page
				app.UseExceptionHandler();
			}

			// use the MVC middleware
			app.UseMvc();


			// Example of Convention-based routing... usually for when returning html, not good for just API
			//app.UseMvc(config =>
			//{
			//	config.MapRoute(
			//		name: "Default",
			//		template: "{controller}/{action}/{id?}",
			//		defaults: new { controller="Home", action="Index" }
			//		);

			//});

			// for non-mvc
			/*app.Run(async (context) =>
            {
				//throw new Exception("Example Exception"); // throws a 500 error
                await context.Response.WriteAsync("Hello World!");
            });*/
		}
	}
}
