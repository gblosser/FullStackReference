using System;

namespace TSD.Reference.Core.Exceptions
{
	/// <summary>
	/// 
	/// </summary>
	public class IncorrectCredentialsException : Exception
	{
		public IncorrectCredentialsException(string theMessage) : base(theMessage)
		{
			
		}
	}
}