using DataAccess.Abstract;
using Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Concrete
{
    public class EfUser : IUser
    {
        public void delete(User user)
        {
            using (var context = new UserContext())
            {
                context.Set<User>().Remove(user);
                context.SaveChanges();
            }
        }

        public User findById(int id)
        {
            using (var context = new UserContext())
            {
                return context.Set<User>().Find(id);

            }
        }

        public List<User> getUsers()
        {
            using (var context = new UserContext())
            {
                return context.Set<User>().ToList();
            }
        }

        public void save(User user)
        {
            using (var context = new UserContext())
            {
                context.Set<User>().Add(user);
                context.SaveChanges();
            }
        }

        public void update(User user)
        {
            using (var context = new UserContext())
            {
                context.Entry(user).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}

