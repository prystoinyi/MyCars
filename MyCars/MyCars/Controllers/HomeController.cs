using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MyCars.Models;
using MyCars.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyCars.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private UserManager<ApplicationUser> manager;
        private ApplicationDbContext db;

        public HomeController()
        {
            db = new ApplicationDbContext();
            manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        }

        public ActionResult Index()
        {
            var currentUser = manager.FindById(User.Identity.GetUserId());

            if (currentUser.UserFullRegister == false)
            {
                return RedirectToAction("Create", "UserInfo");
            }

            

            return View(db.UsersInfo.FirstOrDefault());
        }

        public ActionResult GetItems(int id)
        {
            return PartialView(db.Types.Where(c => c.BrandId == id).ToList());
        }

        [HttpGet]
        public ActionResult Edit()
        {
            int selectedIndex = 1;
            SelectList brand = new SelectList(db.Brands, "Id", "Name", selectedIndex);
            ViewBag.Brands = brand;
            //SelectList type = new SelectList(db.Types.Where(c => c.BrandId == selectedIndex), "Id", "Name");
            ViewBag.Types = db.Types.Where(c => c.BrandId == selectedIndex).ToList();

            return View(new DetailsUserCar());
        }

        [HttpPost]
        public ActionResult Edit(DetailsUserCar NewCar)
        {
            if (ModelState.IsValid)
            {
                var userinfo = new UserInfo();

                var userTypeModel = db.Types.FirstOrDefault(tm => tm.Id == NewCar.TypeId);
                userinfo.TypeModels.Add(userTypeModel);

                db.Entry(userinfo).State = EntityState.Added;
                db.UsersInfo.Add(userinfo);

                db.SaveChanges();
            }

            return View(NewCar);
        }
    }
}