﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ProjectForIdentity.Models
{
    public class IdentityContext : IdentityDbContext<AppUser,AppRole,string>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
        {

        }

    }
}
