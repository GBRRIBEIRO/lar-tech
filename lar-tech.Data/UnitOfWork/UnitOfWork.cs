using lar_tech.Data.Database;
using lar_tech.Domain.Identity;
using lar_tech.Domain.Interfaces;
using lar_tech.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lar_tech.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(UserManager<ApplicationUser> userManager, ApplicationDbContext appDb)
        {
            PersonRepository = new PersonRelationalRepository<Person>(appDb);
            UserRepository = userManager;
        }

        public PersonRelationalRepository<Person> PersonRepository { get; set; }
        public UserManager<ApplicationUser> UserRepository { get; set; }

    }
}
