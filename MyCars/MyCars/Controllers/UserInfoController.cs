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
    public class UserInfoController : Controller
    {
        private UserManager<ApplicationUser> manager;
        private ApplicationDbContext db;

        public UserInfoController()
        {
            db = new ApplicationDbContext();
            manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        }

        // GET: UserInfo
        [HttpGet]
        public ActionResult Create()
        {
            int selectedIndex = 1;
            SelectList brand = new SelectList(db.Brands, "Id", "Name", selectedIndex);
            ViewBag.Brands = brand;
            //SelectList type = new SelectList(db.Types.Where(c => c.BrandId == selectedIndex), "Id", "Name");
            ViewBag.Types = db.Types.Where(c => c.BrandId == selectedIndex).ToList();
            return View(new CreateUserInfoViewModel());
        }

        public ActionResult GetItems(int id)
        {
            return PartialView(db.Types.Where(c => c.BrandId == id).ToList());
        }
        
        [HttpPost]
        public ActionResult Create(CreateUserInfoViewModel selectedModel)
        {

            var currentUser = manager.FindById(User.Identity.GetUserId());
            

            if (ModelState.IsValid)
            {
                var userinfo = new UserInfo();
                userinfo.LastName = selectedModel.UserInfo.LastName;
                userinfo.middleName = selectedModel.UserInfo.middleName;
                userinfo.PhoneNumber = selectedModel.UserInfo.PhoneNumber;
                userinfo.FirstName = selectedModel.UserInfo.FirstName;
                userinfo.CarNumber = selectedModel.UserInfo.CarNumber;
                userinfo.User = currentUser;
                currentUser.UserFullRegister = true;
                manager.Update(currentUser);

                var userTypeModel = db.Types.FirstOrDefault(tm => tm.Id == selectedModel.TypeCarId);
                userinfo.TypeModels.Add(userTypeModel);

                db.Entry(userinfo).State = EntityState.Added;
                db.UsersInfo.Add(userinfo);
                
                db.SaveChanges();
            }
            if (selectedModel != null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(selectedModel);
        }
    }
}