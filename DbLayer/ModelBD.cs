using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbLayer
{
	/// <summary>
	/// Db entity
	/// </summary>
	public class GwpByCountry
	{
		public string Country { get; set; }
		public string VariableId { get; set; }
		public string VariableName { get; set; }
		public string LineOfBusiness { get; set; }
		public Dictionary<int, double> GwpByYears {  get; set; }
	}
}
