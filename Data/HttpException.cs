namespace BE_Shop.Data
{
	public class HttpException : Exception
	{
        public override string Message { get; }
		internal int StatusCode { get; }
		internal HttpException(string message, int statusCode = 400)
		{
			Message = message;
			StatusCode = statusCode;
		}
	}
}
