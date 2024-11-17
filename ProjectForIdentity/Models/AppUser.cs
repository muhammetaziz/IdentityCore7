﻿using Microsoft.AspNetCore.Identity;

namespace ProjectForIdentity.Models
{
    public class AppUser:IdentityUser
    {
        public string FullName { get; set; } = string.Empty;

    }
}
