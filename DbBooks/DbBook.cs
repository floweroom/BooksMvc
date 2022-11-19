using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace DbBooks
{
    public class DbBook :DbContext
    {
        public DbSet<Book>Books { get;set; }
        public DbBook(DbContextOptions<DbBook> opt) : base(opt)
        {

        }
    }
}
