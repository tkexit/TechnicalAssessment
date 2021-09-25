using DbLayer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using WebApi;
using WebApi.Exceptions;
using WebApi.Swagger;

namespace TechnicalAssessment
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
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Gwp service", Version = "v1" });
				c.AddXmlComments();
			});
			services.AddSingleton<IGwpDb, GwpCsvDb>();
			services.AddSingleton<IGwpService, GwpService>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseSwagger();
			app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gwp service v1"));
			app.UseRouting();
			app.UseAuthorization();
			app.UseMiddleware<ErrorHandlingMiddleware>();
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}

		
	}
}
