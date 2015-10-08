using TSD.Reference.Core.Entities;

namespace TSD.Reference.Data.SQLite.DTO
{
	internal class UserDTO
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public int CustomerId { get; set; }
		public bool IsEmployee { get; set; }
	}

	internal static class UserExtensions
	{
		internal static User ToEntity(this UserDTO d)
		{
			return new User
			{
				Id = d.Id,
				FirstName = d.FirstName,
				LastName = d.LastName,
				Email = d.Email,
				CustomerId = d.CustomerId,
				IsEmployee = d.IsEmployee
			};
		}

		internal static UserDTO ToDTO(this User d)
		{
			return new UserDTO
			{
				Id = d.Id,
				FirstName = d.FirstName,
				LastName = d.LastName,
				Email = d.Email,
				CustomerId = d.CustomerId,
				IsEmployee = d.IsEmployee
			};
		}
	}
}
