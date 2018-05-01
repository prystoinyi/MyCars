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
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private UserManager<ApplicationUser> manager;
        private ApplicationDbContext db;

        public AdminController()
        {
            db = new ApplicationDbContext();
            manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        }

        public ActionResult Index()
        {
            return View();
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


            if (userinfo.TypeModels.FirstOrDefault() == null)
            {
                int selectedIndex = 1;
                SelectList brandd = new SelectList(db.Brands.OrderBy(x => x.Name), "Id", "Name", selectedIndex);
                ViewBag.Brands = brandd;
                SelectList type = new SelectList(db.Types.Where(c => c.BrandId == selectedIndex).OrderBy(x => x.Name), "Id", "Name");
                ViewBag.Types = type;
            }
            else
            {
                var userModel = userinfo.TypeModels.First();
                var userBrandId = userModel.BrandId.Value;
                var brad = db.Brands.First(b => userBrandId == b.Id);

                SelectList brand = new SelectList(db.Brands.OrderBy(x => x.Name), "Id", "Name", brad.Id);
                ViewBag.Brands = brand;

                var types = db.Types.Where(c => c.BrandId == brad.Id).OrderBy(x => x.Name).ToList();

                SelectList model = new SelectList(types, "Id", "Name", userModel.Id);

                ViewBag.Types = model;
            }

            return View(userinfo);
        }

        public ActionResult GetItems(int id)
        {
            return PartialView(db.Types.Where(c => c.BrandId == id).OrderBy(x => x.Name).ToList());
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

        public ActionResult Delete(int id)
        {
            UserInfo userInfo = db.UsersInfo.Find(id);
            
            if(userInfo == null)
            {
                return HttpNotFound();
            }
            return View(userInfo);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            UserInfo userInfo = db.UsersInfo.Find(id);
            if (userInfo == null)
            {
                return HttpNotFound();
            }
            db.UsersInfo.Remove(userInfo);
            db.SaveChanges();
            return RedirectToAction("UserInfo");
        }


        public ActionResult CarInfo()
        {
            int selectedIndex = 1;
            SelectList brand = new SelectList(db.Brands.OrderBy(x => x.Name), "Id", "Name", selectedIndex);
            ViewBag.Brands = brand;
            SelectList type = new SelectList(db.Types.Where(c => c.BrandId == selectedIndex).OrderBy(x => x.Name), "Id", "Name");
            ViewBag.Types = type;

            return View();
        }

        [HttpGet]
        public ActionResult AddCarBrand()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCarBrand(Brand brand)
        {
            db.Brands.Add(brand);
            db.SaveChanges();

            return RedirectToAction("CarInfo");
        }

        [HttpGet]
        public ActionResult AddCarModel()
        {
            SelectList brand = new SelectList(db.Brands.OrderBy(x => x.Name), "Id", "Name");
            ViewBag.Brands = brand;

            return View();
        }

        [HttpPost]
        public ActionResult AddCarModel(TypeModel type)
        {

            db.Types.Add(type);
            db.SaveChanges();

            return RedirectToAction("CarInfo");
        }

        public ActionResult DelCarModel(int id = 0)
        {
            TypeModel types = db.Types.Find(id);
            ViewBag.Brands = db.Brands.ToList();

            if (types == null)
            {
                return HttpNotFound();
            }
            return View(types);
        }

        [HttpPost, ActionName("DelCarModel")]
        public ActionResult DelConfirmCarModel(int id)
        {
            TypeModel types = db.Types.Find(id);

            if (types == null)
            {
                return HttpNotFound();
            }
            db.Types.Remove(types);
            db.SaveChanges();
            return RedirectToAction("CarInfo");
        }

        public ActionResult DelCarBrand(int id = 0)
        {
            Brand brand = db.Brands.Find(id);

            if (brand == null)
            {
                return HttpNotFound();
            }
            return View(brand);
        }

        [HttpPost, ActionName("DelCarBrand")]
        public ActionResult DelConfirmCarBrand(int id)
        {
            Brand brand = db.Brands.Find(id);

            if (brand == null)
            {
                return HttpNotFound();
            }

            db.Entry(brand).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("CarInfo");
        }
    }
}