namespace BE_Shop.Data
{
	public class HttpException : Exception
	{
		internal string Message { get; }
		internal int StatusCode { get; }
		internal HttpException(string message, int statusCode)
		{
			Message = message;
			StatusCode = statusCode;
		}
	}
}
