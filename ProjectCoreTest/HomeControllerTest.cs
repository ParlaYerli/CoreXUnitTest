using Bussines.Abstract;
using Entity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebUI.Controllers;
using Xunit;

namespace ProjectCoreTest
{
    public class HomeControllerTest
    {
        private readonly Mock<IUserService> _mock;
        private readonly HomeController _controller;
        private List<User> users;
        public HomeControllerTest()
        {
            _mock = new Mock<IUserService>();
            _controller = new HomeController(_mock.Object);
            users = new List<User>()
            {
                new User{ id=1, name="ayse", lastname="gunes"},
                new User{ id=2, name="mert", lastname="gunes"}
            };
        }
        [Fact]
        public void Index_ActionExecute_ReturnView()
        {
            var result = _controller.Index();
            Assert.IsType<ViewResult>(result);
        }
        [Fact]
        public void Index_ActionExecute_ReturnProductList()
        {
            _mock.Setup(service => service.getList()).Returns(users);
            var result = _controller.Index();

            //1.geriye viewresult dönüyor mu onu kontrol ettim.
            var viewResult = Assert.IsType<ViewResult>(result);

            //2.viewresultın modeli bir userlist mi onu kontrol ettim.
            var userList = Assert.IsAssignableFrom<IEnumerable<User>>(viewResult.Model);
            
            //gelen userlistin sayısı iki tane mi onu kontrol ettim.
            Assert.Equal<int>(2, userList.Count());

        }
        [Fact]
        public void Details_IdIsNull_RedirectToIndexAAction()
        {
            var result = _controller.Details(null);
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
        }
        [Fact]
        public void Details_IdInValid_ReturnNotFound()
        {
            User user=null;
            _mock.Setup(x => x.findById(3)).Returns(user);
            var result = _controller.Details(3);
            var redirect = Assert.IsType<NotFoundResult>(result);
            Assert.Equal<int>(404, redirect.StatusCode);
        }
        
        [Theory]
        [InlineData(1)]
        public void Details_ValidId_ReturnUser(int userId)
        {
            User user = users.First(x => x.id == userId);
            _mock.Setup(x => x.findById(userId)).Returns(user);
            var result = _controller.Details(userId);
            var viewResult = Assert.IsType<ViewResult>(result);
            var resultUser = Assert.IsAssignableFrom<User>(viewResult.Model);
            Assert.Equal(user.id,resultUser.id);
            Assert.Equal(user.name, resultUser.name);
        }
        [Fact]
        public void Create_ActionExecutes_ReturnView()
        {//create get metodu için test
            var result = _controller.Create();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Create_InValidModelState_ReturnView()
        {
            _controller.ModelState.AddModelError("Name", "Name alanı gereklidir");
            var result = _controller.Create(users.First());
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<User>(viewResult.Model);
        }
        [Fact]
        public void Create_ValidModelState_ReturnREdirectToIndexAction()
        {
            //ctor parametresi olarak MockBehavior.Loose default olarak seçilidir.(düşük bağlı)
            //o yüzden mock nesnesini kullanmadım. eğer mockbehavior.strict olsaydı mock nesnesini kullanmak zorundaydım çünkğ sıkı bağlıdır.
            var result = _controller.Create(users.First());
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
        }

        [Fact]
        public void Create_ValidModelState_CreateMethodExecute()
        {
            User newUser = null;
            //itisany: herhangi bir user nesnesi alır ve dönen nesne callbackte newuser nesnesine atanır.
            _mock.Setup(x => x.save(It.IsAny<User>())).Callback<User>(x => newUser = x);

            var result = _controller.Create(users.First());

            _mock.Verify(x => x.save(It.IsAny<User>()),Times.Once);

            Assert.Equal(users.First().id, newUser.id);
        }

        [Fact]
        public void Create_InvalidModalState_NeverCreateExecute()
        {
            _controller.ModelState.AddModelError("Name", "Hata");
            var result = _controller.Create(users.First());
            _mock.Verify(x => x.save(It.IsAny<User>()), Times.Never);
        }

    }
}
