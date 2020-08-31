using Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bussines.Abstract
{
    public interface IUserService
    {
        List<User> getList();
        User findById(int id);
        void save(User user);
        void delete(User user);
        void update(User user);
    }
}
