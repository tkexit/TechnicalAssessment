using System.Collections.Generic;
using WebApi.Contracts;
using DbLayer;
using System;

namespace WebApi
{
	/// <summary>
	///  Business logic service
	/// </summary>
	public class GwpService : IGwpService
	{
		private readonly IGwpDb _gwpDb;

		public GwpService(IGwpDb gwpDb)
		{
			_gwpDb = gwpDb;
		}
		
		private const int startYears = 2008;
		private const int endYears = 2015;

		/// <summary>
		/// Calculate average gross written premium (GWP)
		/// </summary>
		/// <returns>An average gross written premium (GWP)</returns>
		public Dictionary<string, double> Avg(string country, List<string> linesOfBusiness)
		{
			var result = new Dictionary<string, double>();
			foreach (var line in linesOfBusiness)
			{
				var avg = _gwpDb.Avg(country, line, startYears, endYears);
				result.Add(line, Math.Round(avg, 1));
			}
			return result;
		}
	}
}
