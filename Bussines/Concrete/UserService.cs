using Bussines.Abstract;
using DataAccess.Abstract;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bussines.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUser userRepository;
        public UserService(IUser userRepository)
        {
            this.userRepository = userRepository;
        }
        public void delete(User user)
        {
            userRepository.delete(user);
        }

        public User findById(int id)
        {
            return userRepository.findById(id);
        }

        public List<User> getList()
        {
            return userRepository.getUsers().ToList();
        }

        public void save(User user)
        {
            userRepository.save(user);
        }

        public void update(User user)
        {
            userRepository.update(user);
        }
    }
}
