namespace DbLayer
{
	public interface IGwpDb
	{
		public double Avg(string country, string linesOfBusiness, int startYear, int ensYear);
	}
}
