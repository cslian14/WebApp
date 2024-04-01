using CasulMyAppCRUD.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CasulMyAppCRUD.Controllers
{
    public class BaseController : Controller
    {
        public CasulEntities _db;
        public BaseRepository<User> _userRepo;
        public BaseController()
        {
            _db = new CasulEntities();
            _userRepo = new BaseRepository<User>();
        }

    }
}