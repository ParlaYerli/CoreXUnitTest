using Bussines.Abstract;
using Entity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebAPI.Controllers;
using Xunit;

namespace WebAPITest
{
    public class ApiUserControllerTest
    {
        private readonly Mock<IUserService> _mock;
        private readonly UserController _controller;
        private List<User> users;
        public ApiUserControllerTest()
        {
            _mock = new Mock<IUserService>();
            _controller = new UserController(_mock.Object);
            users = new List<User>()
            {
                new User{ id=1, name="ayse", lastname="gunes"},
                new User{ id=2, name="mert", lastname="gunes"}
            };
        }
        [Fact]
        public void GetAll_ActionExecutesReturnOkResultWithUser()
        {
            _mock.Setup(x => x.getList()).Returns(users);
            var result = _controller.GetAll();
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnUser = Assert.IsAssignableFrom<List<User>>(okResult.Value);
            Assert.Equal(2, returnUser.ToList().Count);
        }

        [Theory]
        [InlineData(0)]
        public void GetUser_IdInValid_ReturnNotFound(int userId)
        {
            User user = null;
            _mock.Setup(x => x.findById(userId)).Returns(user);
            var result = _controller.GetUser(userId);
            Assert.IsType<NotFoundResult>(result);
        }

        [Theory]
        [InlineData(1)]
        public void GetUser_IdValid_ReturnOkResult(int userId)
        {
            User user = users.First(x => x.id == userId);
            _mock.Setup(x => x.findById(userId)).Returns(user);
            var result = _controller.GetUser(userId);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnUser = Assert.IsAssignableFrom<User>(okResult.Value);
            Assert.Equal(userId, returnUser.id);
            Assert.Equal(user.name, returnUser.name);
        }

        [Theory]
        [InlineData(1)]
        public void Update_IdIsNotEqualUser_ReturnBadRequest(int userId)
        {
            User user = users.First(x => x.id == userId);
            var result = _controller.Update(2, user);
            Assert.IsType<BadRequestResult>(result);
        }
        [Theory]
        [InlineData(1)]
        public void Update_ActionExecute_ReturnNoContent(int userId)
        {
            User user = users.First(x => x.id == userId);
            _mock.Setup(x => x.update(user));
            var result = _controller.Update(userId, user);
            _mock.Verify(x => x.update(user), Times.Once);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void Add_ActionExecutes_ReturnCreatedAtAction()
        {
            User user = users.First();
            _mock.Setup(x => x.save(user));
            var result = _controller.Add(user);
            var CreatedAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            _mock.Verify(x => x.save(user),Times.Once);
            Assert.Equal("GetUserById", CreatedAtActionResult.ActionName);
        }

        [Theory]
        [InlineData(0)]
        public void Delete_IdInValid_ReturnNotFound(int userId)
        {
            User user = null;
            _mock.Setup(x => x.findById(userId)).Returns(user);
            var result = _controller.Delete(userId);
            Assert.IsType<NotFoundResult>(result);
        }

        [Theory]
        [InlineData(1)]
        public void Delete_ActionExecutes_ReturnNoContent(int userId)
        {
            User user = users.First(x=>x.id==userId);
            _mock.Setup(x => x.findById(userId)).Returns(user);
            _mock.Setup(x => x.delete(user));
            var result = _controller.Delete(userId);
            _mock.Verify(x => x.delete(user), Times.Once);
            Assert.IsType<NoContentResult>(result);
        }

    }
}
