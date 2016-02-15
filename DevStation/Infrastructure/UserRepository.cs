﻿using DevStation.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
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

        public void UpdateUser(ApplicationUser user) {

            var userToEdit = UserByUserName(user.UserName);

            userToEdit.FirstName = user.FirstName;
            userToEdit.LastName = user.LastName;
            userToEdit.Email = user.Email;
            userToEdit.PhoneNumber = user.PhoneNumber;
            userToEdit.SkillSet = user.SkillSet;
   
            _db.SaveChanges();
        }

    }
}