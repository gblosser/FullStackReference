using System;

namespace TSD.Reference.Core.Exceptions
{
	/// <summary>
	/// 
	/// </summary>
	public class PasswordsDoNotMatchException : Exception
	{
		public PasswordsDoNotMatchException(string theMessage) : base(theMessage)
		{
			
		}
	}
}