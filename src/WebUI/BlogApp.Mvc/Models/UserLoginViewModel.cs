using System;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Mvc.Models
{
	public class UserLoginViewModel
	{
		[Required]
		public string Email { get; set; }

        [Required]
		[DataType(DataType.Password)]
        public string Password { get; set; }
	}
}

