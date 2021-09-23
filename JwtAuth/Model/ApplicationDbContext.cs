using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtAuth.Model
{
    public class ApplicationDbContext: IdentityDbContext
    {
        public DbSet<TodoData> TodoDatas { get; set; }
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
    }
}
