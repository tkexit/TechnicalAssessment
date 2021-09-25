using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApi;
using WebApi.Contracts;
using WebApi.Exceptions;
using System;
using System.Linq;

namespace WebApi.Controllers
{
	[ApiController]
	[Produces("application/json")]
	[Route("/server/api/gwp/")]
	public class GwpController : ControllerBase
	{
		private readonly IGwpService _gwpService;

		public GwpController(IGwpService gwpService)
		{
			_gwpService = gwpService;
		}


		/// <summary>
		/// Calculate average gross written premium (GWP) over 2008-2015 period for the selected lines of business
		/// </summary>
		/// <param name="gwpAvgRequest">The request allows user to choose a country and one or more lines of business (LOB)</param>
		/// <returns> An average gross written premium (GWP) over 2008-2015 period for the selected lines of business</returns>
		[HttpPost("avg")]
		[ProducesResponseType(typeof(Dictionary<string, double>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
		public ActionResult<Dictionary<string, double>> Avg(GwpAvgRequest gwpAvgRequest)
		{
			if (string.IsNullOrEmpty(gwpAvgRequest.Country))
				throw new Exception($"Property Country is null or empty");
			if (gwpAvgRequest.LinesOfBusiness?.Count == 0)
				throw new Exception($"Property LinesOfBusiness is null or empty");
			return _gwpService.Avg(gwpAvgRequest.Country, gwpAvgRequest.LinesOfBusiness);
		}
	}
}
