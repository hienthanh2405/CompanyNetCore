using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CompanyNetCore.Entities
{
    public class PersonDatabaseContext : DbContext
    {
        public PersonDatabaseContext(DbContextOptions<PersonDatabaseContext> options) : base(options)
        {
                
        }

        public DbSet<Person> Persons { get; set; }
    }
}
