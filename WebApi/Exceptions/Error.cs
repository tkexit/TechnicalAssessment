namespace WebApi.Exceptions
{
	/// <summary>
	/// Base error type
	/// </summary>
	public class Error
	{
		/// <summary>
		///  Error code
		/// </summary>
		public string Code { get; set; }

		/// <summary>
		///  Error message
		/// </summary>
		public string Message { get; set; }

		public Error() { }
		public Error(string errorCodes, string message)
		{
			Code = errorCodes;
			Message = message;
		}
	}
}