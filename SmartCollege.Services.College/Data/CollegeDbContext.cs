using SmartCollege.Services.College.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SmartCollege.Services.College.Data
{
    public class CollegeDbContext : DbContext
    {
        public CollegeDbContext(DbContextOptions<CollegeDbContext> options)
            : base(options) { }

        public DbSet<AppUser> Users => Set<AppUser>();
        public DbSet<Student> Students => Set<Student>();
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    }
}
