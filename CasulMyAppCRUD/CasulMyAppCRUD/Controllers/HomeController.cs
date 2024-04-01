using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CasulMyAppCRUD.Controllers
{
    //[Authorize(Roles = "User, Manager")]
    [Authorize]
    public class HomeController : BaseController
    {
        // GET: Home
        public ActionResult Index()
        {
            List<User> userList = _userRepo.GetAll();
            return View(_userRepo.GetAll());
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index");
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(User u)
        {
            var user = _userRepo.Table.Where(m => m.username == u.username).FirstOrDefault();
            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(u.username, false);
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "User not exist or Incorrect Password");

            return View(u);

        }

        //CREATE
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(User u)
        {
            _userRepo.Create(u);
            TempData["Msg"] = $"User {u.username} added!";
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
        //EDIT
        //Authorize(Roles = "Manager")]
        public ActionResult Edit(int id)
        {
            return View(_userRepo.Get(id));
        }
        //DETAILS
        public ActionResult Details(int id)
        {
            return View(_userRepo.Get(id));
        }
        [HttpPost]
        public ActionResult Edit(User u)
        {
            _userRepo.Update(u.id, u);
            TempData["Msg"] = $"User {u.username} updated!";

            return RedirectToAction("Index");
        }
        //DELETE
        //[Authorize(Roles = "Manager")]
        public ActionResult Delete(int id)
        {
            _userRepo.Delete(id);
            TempData["Msg"] = $"User deleted!";

            return RedirectToAction("Index");
        }
    }
}