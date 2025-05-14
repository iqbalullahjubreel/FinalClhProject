using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using FinalClhProject.DataAccess;

namespace FinalClhProject.DataAccess.Data
{
    public class FinalClhProjDbContextFactory : IDesignTimeDbContextFactory<FinalClhProjDbContext>
    {
        public FinalClhProjDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<FinalClhProjDbContext>();
            optionsBuilder.UseMySql(
                "server=localhost;port=3306;database=FinalClhProjDbContext;user=root;password=07065061918@iqbal",
                ServerVersion.AutoDetect("server=localhost;database=FinalClhProjDbContext;user=root;password=07065061918@iqbal"));

            return new FinalClhProjDbContext(optionsBuilder.Options);
        }
    }

}
