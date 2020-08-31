using Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IUser
    {
        User findById(int id);
        List<User> getUsers();
        void save(User user);
        void delete(User user);
        void update(User user);
    }
}
