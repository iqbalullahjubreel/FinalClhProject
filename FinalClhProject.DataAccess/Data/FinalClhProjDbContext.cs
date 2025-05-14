using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace FinalClhProject.DataAccess.Data
{
    public class FinalClhProjDbContext : DbContext
    {
        public FinalClhProjDbContext(DbContextOptions<FinalClhProjDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }
        public DbSet<User> Users { get; set; }

    }
}
