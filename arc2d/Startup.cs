using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace arc2d
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();
			services.AddHttpClient();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			//app.UseDefaultFiles();
			//app.UseStaticFiles();

			app.Use(async (context, next) =>
			{
				await next();

				var path = context.Request.Path.Value;
				bool notApi = !path.StartsWith("/api");
				bool noExt = !Path.HasExtension(path);

				if (notApi && noExt)
				{
					context.Request.Path = "/index.html";
					await next();
				}
			});

			//app.UseDefaultFiles();
			//app.UseStaticFiles();


			var provider = new FileExtensionContentTypeProvider();
			provider.Mappings[".html"] = "text/html";
			provider.Mappings[".webmanifest"] = "application/manifest+json";

			app.UseStaticFiles(new StaticFileOptions()
			{
				ContentTypeProvider = provider
			});


			app.UseRouting();
			//app.UseMvc();

			//app.UseMvc() // suggestion: you can move all the SPA requests to for example /app/<endpoints_for_the_Spa>
			//and let the mvc return 404 in case <endpoints_for_the_Spa> is not recognized by the backend. This way SPA will not receive index.html
			//// at this point the request did not hit the api nor any of the files

			//// return index instead of 404 and let the SPA to take care of displaying the "not found" message
			//app.Use(async (context, next) => {
			//	context.Request.Path = "/index.html";
			//	await next();
			//});
			//			app.UseStaticFiles(); // this will return index.html


			//app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
