using System;
using TSD.Reference.Core.Entities;

namespace TSD.Reference.Core.Exceptions
{
	public class InvalidRentalAgreementException : Exception
	{
		public InvalidRentalAgreementException(string theMessage, RentalAgreement theRentalAgreement) : base(theMessage)
		{
			RentalAgreement = theRentalAgreement;
		}

		public RentalAgreement RentalAgreement { get; private set; }
	}
}
