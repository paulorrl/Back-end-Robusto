using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using SpaUserControl.Domain.Contracts.Repositories;
using SpaUserControl.Domain.Models;
using SpaUserControl.Infraestructure.Data;

namespace SpaUserControl.Infraestructure.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly AppDataContext _context = new AppDataContext();

        public User Get(Guid Id)
        {
            return _context.Users.Where(x => x.Id == Id).FirstOrDefault();
        }

        public List<User> Get(int skip, int take)
        {
            return _context.Users.OrderBy(x => x.Name).Skip(skip).Take(take).ToList();
        }

        public User Get(string Email)
        {
            return _context.Users.Where(x => x.Email.ToLower() == Email.ToLower()).FirstOrDefault();
        }

        public void Create(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void Update(User user)
        {
            _context.Entry<User>(user).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(User user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}