using System.Collections.Generic;

namespace WebApi.Contracts
{
	/// <summary>
	/// The request allows user to choose a country and one or more lines of business (LOB)
	/// </summary>
	public class GwpAvgRequest
	{
		/// <summary>
		/// To choose a country
		/// </summary>
		public string Country { get; set; }

		/// <summary>
		/// To choose one or more lines of business (LOB)
		/// </summary>
		public List<string> LinesOfBusiness { get; set; }
	}
}
