﻿using System;
using Microsoft.AspNetCore.Identity;

namespace PustokProject.Models
{
	public class AppUser:IdentityUser
	{
		public string FullName { get; set; }
	}
}

