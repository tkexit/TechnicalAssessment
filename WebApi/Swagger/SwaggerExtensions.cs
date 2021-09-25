using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.IO;
using System.Reflection;

namespace WebApi.Swagger
{
	internal static class SwaggerExtensions
	{
		internal static void AddXmlComments(this SwaggerGenOptions options)
		{
			var currentFile = new FileInfo(Assembly.GetExecutingAssembly().Location);
			if (currentFile?.Directory?.FullName != null)
			{
				foreach (var fileInfo in new DirectoryInfo(currentFile.Directory.FullName).GetFiles("*.xml"))
				{
					options.IncludeXmlComments(fileInfo.FullName);
				}
			}
		}
	}
}
