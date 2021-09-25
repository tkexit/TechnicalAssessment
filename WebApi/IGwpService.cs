using System.Collections.Generic;

namespace WebApi
{
	public interface IGwpService
	{
		/// <summary>
		/// Calculate average gross written premium (GWP)
		/// </summary>
		/// <returns>An average gross written premium (GWP)</returns>
		public Dictionary<string, double> Avg(string country, List<string> linesOfBusiness);
	}
}
