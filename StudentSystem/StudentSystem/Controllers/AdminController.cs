using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudentSystem.Models;
namespace StudentSystem.Controllers
{
    public class AdminController : Controller
    {
        studentLeaveSystemEntities db = new studentLeaveSystemEntities();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 查询职务
        /// </summary>
        /// <returns></returns>
        public JsonResult SelectRole()
        {
            var a = from Roles in db.Roles
                    select new
                    {
                        RoleId = Roles.RoleId,
                        RoleName = Roles.RoleName
                    };
            return Json(a,JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="admin"></param>
        /// <returns></returns>
        public ActionResult AddUser(Admins admin)
        {
            db.Admins.Add(admin);
            if(db.SaveChanges()>0)
            {
                return Content("1");
            }
            return Content("0");
        }

        public ActionResult UpdatePwd(string  newPwd,string admin)
        {
             var queryAdmins =
     from Admins in db.Admins
     where
       Admins.AdminName == admin
     select Admins;
            foreach (var Admins in queryAdmins)
            {
                Admins.AdminPwd = newPwd;
            }
            if (db.SaveChanges()>0)
            {
                return Content("1");
            }
            else
            {
                return Content("0");
            }
           
        }
    }
}