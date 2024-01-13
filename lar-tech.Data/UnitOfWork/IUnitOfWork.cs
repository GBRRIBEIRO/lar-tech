﻿using lar_tech.Domain.Identity;
using lar_tech.Domain.Models;
using Microsoft.AspNetCore.Identity;
using lar_tech.Data.Repositories;

namespace lar_tech.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        public PersonRelationalRepository<Person> PersonRepository { get; set; }
        public UserManager<ApplicationUser> UserRepository { get; set; }
    }
}