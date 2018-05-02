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

            var result = db.UsersInfo.FirstOrDefault(c => c.User.Id == currentUser.Id);

            if ( result == null)
            {
                return RedirectToAction("Create", "UserInfo");
            }

            if (currentUser.UserFullRegister == false)
            {
                return RedirectToAction("Create", "UserInfo");
            }
            ViewBag.Brand = db.Brands.ToList();
            return View(result);
        }

        public ActionResult GetItems(int id)
        {
            return PartialView(db.Types.Where(c => c.BrandId == id).OrderBy(x => x.Name).ToList());
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

        [HttpGet]
        public ActionResult AddNewCar(int id = 0)
        {
            UserInfo userinfo = db.UsersInfo.Find(id);
            if (userinfo == null)
            {
                return HttpNotFound();
            }

            int selectedIndex = 1;
            SelectList brandd = new SelectList(db.Brands.OrderBy(x => x.Name), "Id", "Name", selectedIndex);
            ViewBag.Brands = brandd;
            SelectList type = new SelectList(db.Types.Where(c => c.BrandId == selectedIndex).OrderBy(x => x.Name), "Id", "Name");
            ViewBag.Types = type;

            return View(userinfo);
        }

        [HttpPost]
        public ActionResult AddNewCar(UserInfo userInfo, int? selectedModel)
        {
            UserInfo newUserInfo = db.UsersInfo.Find(userInfo.Id);

            var types = newUserInfo.TypeModels;

            if (selectedModel.HasValue)
            {
                var modelId = selectedModel.Value;
                foreach (var item in db.Types.Where(co => modelId == co.Id))
                {
                    types.Add(item);
                }
            }

            db.Entry(newUserInfo).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}