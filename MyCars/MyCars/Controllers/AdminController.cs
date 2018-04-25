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
    public class AdminController : Controller
    {
        private UserManager<ApplicationUser> manager;
        private ApplicationDbContext db;

        public AdminController()
        {
            db = new ApplicationDbContext();
            manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        }

        // GET: Admin
        public ActionResult UserInfo()
        {
            ViewBag.Brands = db.Brands.ToList();
            return View(db.UsersInfo.ToList());
        }

        [HttpGet]
        public ActionResult Edit(int id = 0)
        {
            UserInfo userinfo = db.UsersInfo.Find(id);
            if (userinfo == null)
            {
                return HttpNotFound();
            }
            var userModel = userinfo.TypeModels.First();
            var userBrandId = userModel.BrandId.Value;
            var brad = db.Brands.First(b => userBrandId == b.Id);

            SelectList brand = new SelectList(db.Brands, "Id", "Name", brad.Id);
            ViewBag.Brands = brand;

            var types = db.Types.Where(c => c.BrandId == brad.Id).ToList();

            SelectList model = new SelectList(types, "Id", "Name", userModel.Id);

            ViewBag.Types = model;


            return View(userinfo);
        }

        public ActionResult GetItems(int id)
        {
            return PartialView(db.Types.Where(c => c.BrandId == id).ToList());
        }

        [HttpPost]
        public ActionResult Edit(UserInfo userinfo, int? selectedModel)
        {
            UserInfo newUserInfo = db.UsersInfo.Find(userinfo.Id);
            newUserInfo.LastName = userinfo.LastName;
            newUserInfo.FirstName = userinfo.FirstName;
            newUserInfo.middleName = userinfo.middleName;
            newUserInfo.PhoneNumber = userinfo.PhoneNumber;
            newUserInfo.CarNumber = userinfo.CarNumber;

            newUserInfo.TypeModels.Clear();

            if (selectedModel.HasValue)
            {
                var modelId = selectedModel.Value;
                foreach (var item in db.Types.Where(co => modelId == co.Id ))
                {
                    newUserInfo.TypeModels.Add(item);
                }
            }

            db.Entry(newUserInfo).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("UserInfo");
        }
    }
}