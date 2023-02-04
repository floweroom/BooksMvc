using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using BooksMvc.Models;

namespace DbBooks
{
    public class DbBook :IdentityDbContext<User, Role,string>
    {

        public DbSet<Book>Books { get;set; }
        public DbBook(DbContextOptions<DbBook> opt) : base(opt)
        {

        }
    }
}
