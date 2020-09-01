using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Bussines.Abstract;
using Entity;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService userService;

        public HomeController(IUserService userService)
        {
            this.userService = userService;
        }
      
        [HttpGet]
        public IActionResult Index()
        {
            var user = userService.getList();
            return View(user);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                userService.save(user);
                return RedirectToAction("Index");
            }
            return View(user);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id==null)
            {
                return NotFound();
            }
            var user = userService.findById((int)id);
            if (user==null)
            {
                return NotFound();
            }
            return View(user);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var user = userService.findById(id);
            userService.delete(user);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id==null)
            {
                return RedirectToAction("Index");
            }
            var student = userService.findById((int)id);
            if (student==null)
            {
                return NotFound();
            }
            
            return View(student);
        }
        [HttpPost]
        public IActionResult Edit(int id,User user)
        {
            if (id!=user.id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                userService.update(user);
                return RedirectToAction("Index");
            }
            return View(user);
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id==null)
            {
                return RedirectToAction("Index");
            }
            var student = userService.findById((int)id);
            if (student==null)
            {
                return NotFound();
            }
            return View(student);
        }
       

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
