using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApotekConsoleApp.DbContexts
{
    public class ApotekDbContextFactory : IDesignTimeDbContextFactory<ApotekDbContext>
    {
        public ApotekDbContext CreateDbContext(string[] args = null)
        {
            var options = new DbContextOptionsBuilder<ApotekDbContext>();
            options.UseMySql("server=localhost; port=3306; database=db_apotek; user=root; password=5u7ud100%");

            return new ApotekDbContext(options.Options);
        }
    }
}
