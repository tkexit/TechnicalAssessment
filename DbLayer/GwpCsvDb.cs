using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace DbLayer
{
	public class GwpCsvDb : IGwpDb
	{
		private readonly List<GwpByCountry> _countries;
		public GwpCsvDb()
		{
			_countries = ReadByCsv(pathToCsv);
		}


		public double Avg(string country, string linesOfBusiness, int startYear, int ensYear)
		{
			var gwpByCountryAndLOB = _countries.SingleOrDefault(p => p.Country == country && p.LineOfBusiness == linesOfBusiness);
			if (gwpByCountryAndLOB == null)
				throw new Exception($"No data by country = {country} and linesOfBusiness = {linesOfBusiness}.");
			return gwpByCountryAndLOB.GwpByYears.Where(p => p.Key >= startYear && p.Key <= ensYear).Average(p => p.Value);
		}

		const string FirstYearColumnName = "Y2000";
		const string pathToCsv = "\\gwpByCountry.csv";
		private List<GwpByCountry> ReadByCsv(string path)
		{
			var location = Assembly.GetEntryAssembly().Location;
			var directory = Path.GetDirectoryName(location);
			var fullPath = directory + path;

			if (!File.Exists(fullPath))
				throw new Exception($"File db {fullPath} does not exists.");
			
			bool isFirstLine = true;
			var yearsList = new List<int>();
			var result = new List<GwpByCountry>();
			int firstYearColumnIndex = 4;
			using (StreamReader fs = new StreamReader(fullPath))
			{
				do
				{
					var line = fs.ReadLine();
					if (line != null)
					{
						if (isFirstLine)
						{
							var data = line.Split(",");
							firstYearColumnIndex = Array.IndexOf(data, FirstYearColumnName);
							foreach (var item in data.Skip(firstYearColumnIndex))
							{
								yearsList.Add(int.Parse(item.Trim().Remove(0, 1)));
							}
							isFirstLine = false;
						}
						else
						{
							var data = line.Split(",");
							var gwpByCountry = new GwpByCountry
							{
								Country = data[0],
								VariableId = data[1],
								VariableName = data[2],
								LineOfBusiness = data[3],
								GwpByYears = new Dictionary<int, double>()
							};
							
							int yearIndex = 0;
							foreach (var item in data.Skip(firstYearColumnIndex))
							{
								if (double.TryParse(item, out var res))
									gwpByCountry.GwpByYears.Add(yearsList[yearIndex], res);
								yearIndex++;
							}
							result.Add(gwpByCountry);
						}
					}
				} while (!fs.EndOfStream);
			}
			return result;
		}
	}
}
