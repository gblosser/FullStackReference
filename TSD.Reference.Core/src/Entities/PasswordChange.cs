using System.ComponentModel.DataAnnotations;

namespace TSD.Reference.Core.Entities
{
	/// <summary>
	/// Contains new and old password, along with new password confirmation, to update a User password to new password
	/// </summary>
	public class PasswordChange
	{
		/// <summary>
		/// The old password of the user
		/// </summary>
		[Required]
		public string OldPassword { get; set; }

		/// <summary>
		/// The password the user would like to use
		/// </summary>
		[Required]
		public string NewPassword { get; set; }

		/// <summary>
		/// The new user passoword confirmed
		/// </summary>
		[Required]
		public string ConfirmNewPassword { get; set; }
	}
}
