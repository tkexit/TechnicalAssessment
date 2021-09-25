using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using WebApi.Contracts;
using System.Linq;
using WebApi.Exceptions;

namespace WebApi.Tests
{
	public class CountryGwpTest
	{
		RestClient restClient;
		[SetUp]
		public void Setup()
		{
			restClient = new RestClient("http://localhost:9091/server/api/gwp");
		}
		private static readonly object[] positiveTestDate =
		{
			new List<Tuple<string, double>>
				{
				new Tuple<string, double> ("transport", 285137382.5),
				new Tuple<string, double> ("liability", 2165823562.3)
				}
		};

		[Test(Description = "PositiveTest")]
		[TestCaseSource(nameof(positiveTestDate))]
		public void PositiveTest(List<Tuple<string, double>> data)
		{
			var request = new RestRequest("/avg", Method.POST);
			request.AddJsonBody(new GwpAvgRequest()
			{
				Country = "ae",
				LinesOfBusiness = data.Select(p => p.Item1).ToList()
			});
			var result = restClient.Post<Dictionary<string, double>>(request);
			Assert.IsNotNull(result);
			Assert.IsTrue(result.IsSuccessful);
			Assert.IsNotNull(result.Data);
			Assert.AreEqual(data[0].Item2, result.Data[data[0].Item1]);
			Assert.AreEqual(data[1].Item2, result.Data[data[1].Item1]);
		}

		[Test(Description = "NegativeTest")]
		public void NegativeTest()
		{
			var request = new RestRequest("/avg", Method.POST);
			request.AddJsonBody(new GwpAvgRequest()
			{
				Country = "RusRus",
				LinesOfBusiness = new List<string>() { "test" }
			});
			var result = restClient.Post<Error>(request);
			Assert.IsNotNull(result);
			Assert.IsFalse(result.IsSuccessful);
			Assert.IsNotNull(result.Data);
			Assert.AreEqual(result.Data.Code, "Unexpected");
		}
	}
}