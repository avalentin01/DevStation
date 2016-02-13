﻿using DevStation.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DevStation.Infrastructure
{
    public class UserRepository
    {
        private ApplicationDbContext _db;

        public UserRepository(DbContext db)
        {
            _db = (ApplicationDbContext)db;
        }

        public IQueryable<ApplicationUser> ListUsers()
        {
            return from u in _db.Users
                   where u.Active
                   select u;
        }

        public IQueryable<ApplicationUser> SearchUsers(string searchTerm)
        {
            return from u in _db.Users
                   where u.Active &&
                   (u.FirstName.Contains(searchTerm) ||
                   u.SkillSet.Contains(searchTerm))
                   select u;                   
        }

        public ApplicationUser UserByUserName(string userName)
        {
            return (from u in _db.Users
                    where u.Active && u.UserName == userName
                    select u)
                    .Include(u => u.CurrentJob)
                    .Include(u => u.CompletedJobs)
                    .FirstOrDefault();
        }

    }
}