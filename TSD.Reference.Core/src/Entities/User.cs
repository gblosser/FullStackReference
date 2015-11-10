using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace TSD.Reference.Core.Entities
{
	public class User
	{

		[IgnoreDataMember]
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }

		[Required]
		public string Email { get; set; }

		[Required]
		public int CustomerId { get; set; }

		[Required]
		public string Password { get; set; }

		public bool IsEmployee { get; set; }

		[Required]
		public string Username { get; set; }
	}
}
