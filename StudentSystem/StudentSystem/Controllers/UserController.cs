using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudentSystem.Models;
using System.Data.Entity.SqlServer;

namespace StudentSystem.Controllers
{
    public class UserController : Controller
    {
        studentLeaveSystemEntities db = new studentLeaveSystemEntities();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        //登录
        public JsonResult Login(Admins admin)
        {
            // int num = db.Admins.Where(a => a.AdminName == admin.AdminName && a.AdminPwd == admin.AdminPwd).Count();
            var a = from adn in db.Admins
                         join roles in db.Roles on adn.RoleId equals roles.RoleId
                         join qxb in db.Qxb on roles.RoleId equals qxb.RoleId
                         join gnb in db.Gnb on qxb.GnId equals gnb.GnId
                         where adn.AdminName == admin.AdminName && adn.AdminPwd == admin.AdminPwd
                         select new
                         {
                             adminName = adn.AdminName,
                             pwd = adn.AdminPwd,
                             role = roles.RoleName,
                             gnName = gnb.GnName
                         };
            return Json(a,JsonRequestBehavior.AllowGet);
           
        }
        /// <summary>
        /// 添加级别
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        public ActionResult AddGrade(Grade grade)
        {
            db.Grade.Add(grade);
            if(db.SaveChanges()> 0)
            {
                return Content("1");
            }
            return Content("0");
        }
        /// <summary>
        /// 查询级别
        /// </summary>
        /// <returns></returns>
        public JsonResult selectGrade()
        {
            var b = from grade in db.Grade select new {
                Gid=grade.Gid,
                GNum=grade.GNum
            };
            return Json(b, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 查询级别,根据级别id
        /// </summary>
        /// <returns></returns>
        public JsonResult selectGradeBy(int gid)
        {
            var b = from grade in db.Grade where grade.Gid==gid
                    select new
                    {
                        Gid = grade.Gid,
                        GNum = grade.GNum
                    };
            return Json(b, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 查询班级
        /// </summary>
        /// <returns></returns>

        public JsonResult selectClass(int gid)
        {
            var c = from Classes in db.Class
                    where
                      Classes.Grade.Gid == gid
                    select new
                    {
                        Classes.Cid,
                        Classes.CName
                    };
            return Json(c, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 添加班级
        /// </summary>
        /// <returns></returns>
        public ActionResult AddClass(Class clas)
        {
            db.Class.Add(clas);
            if(db.SaveChanges()>0)
            {
                return Content("1");
            }
            return Content("0");
        }
        public ActionResult AddStudent(Student stu)
        {
            db.Student.Add(stu);
            if(db.SaveChanges()>0)
            {
                return Content("1");
            }
            return Content("0");
        }
        /// <summary>
        /// 根据学号查询学生
        /// </summary>
        /// <param name="stuNum"></param>
        /// <returns></returns>
        public JsonResult SelectStudent(string stuNum)
        {
            var a = from Students in db.Student
                    from Grades in db.Grade
                    where Grades.Gid == Students.Class.Cid && Students.StuNum == stuNum
                    select new
                    {
                        StuNum = Students.StuNum,
                        StuName = Students.StuName,
                        Phone = Students.Phone,
                        GNum = Grades.GNum,
                        CName = Students.Class.CName
                    };
            return Json(a,JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 查询所有学生信息
        /// </summary>
        /// <returns></returns>
        public JsonResult SelectStu(int rows , int pagesize )
        {
            int page = (pagesize - 1) * rows;
           var a= (from Students in db.Student
             from Classes in db.Class
             where
               !
                 ((from Students0 in db.Student
                   select new
                   {
                       Students0.StuId
                   }).Take(page)).Contains(new { StuId = Students.StuId }) &&
               Students.Grade.Gid == Classes.Cid
             select new
             {
                 Students.StuNum,
                 Students.StuName,
                 Students.Phone,
                 Students.Grade.GNum,
                 Classes.CName
             }).Take(rows);
            return Json(a,JsonRequestBehavior.AllowGet);


        }

        /// <summary>
        /// 添加请假信息
        /// </summary>
        /// <param name="leave"></param>
        /// <returns></returns>
        public ActionResult AddLeave(LeaveInfo leave)
        {
            db.LeaveInfo.Add(leave);
            if(db.SaveChanges()>0)
            {
                return Content("1");
            }
            return Content("0");
        }
        /// <summary>
        /// 查询假条信息
        /// </summary>
        /// <returns></returns>
        public JsonResult SelectLeave(int rows = 0,int pagesize=0)
        {
            using (var a=new studentLeaveSystemEntities())
            {
                var leave = db.LeaveInfo.SqlQuery("select top " + rows + "* from LeaveInfo where Lid not in(select top ((" + (pagesize - 1) + "*" + rows + ")) Lid from LeaveInfo) and Statu=0");
                return Json(leave, JsonRequestBehavior.AllowGet);
            }
            
        }
        public JsonResult SelectLeaveBy(int rows, int pagesize,string stuNum)
        {
            using (var a = new studentLeaveSystemEntities())
            {
                var leave = db.LeaveInfo.SqlQuery("select top " + rows + "* from LeaveInfo where Lid not in(select top ((" + (pagesize - 1) + "*" + rows + ")) Lid from LeaveInfo) and Statu=0 and StuNum="+stuNum);
                return Json(leave, JsonRequestBehavior.AllowGet);
            }

        }
        /// <summary>
        /// 查询假条数据条数
        /// </summary>
        /// <returns></returns>
        public ActionResult Total()
        {
            int total = db.LeaveInfo.Where(a => a.Statu == 0).Count();
             
            return Content(total.ToString());
        }
        /// <summary>
        /// 查询学生数据条数
        /// </summary>
        /// <returns></returns>
        public ActionResult StuTotal()
        {
            int total = db.Student.Count();

            return Content(total.ToString());
        }
        /// <summary>
        /// 根据学号更改假条状态
        /// </summary>
        /// <param name="stuNum"></param>
        /// <returns></returns>
        public ActionResult UpdataByStuNum(string stuNum)
        {
            var queryLeaveInfoes = from LeaveInfoes in db.LeaveInfo
                                   where LeaveInfoes.StuNum == stuNum
                                   select LeaveInfoes;
                                   foreach (var LeaveInfoes in queryLeaveInfoes)
                                    {
                                       LeaveInfoes.Statu = 1;
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
        /// <summary>
        /// 查询所有班级和年级
        /// </summary>
        /// <returns></returns>
        public JsonResult SelectClassAnGrade()
        {
            var a = from Classes in db.Class
                    select new
                    {
                        Classes.Grade.GNum,
                        Classes.CName
                    };
            return Json(a,JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 根据年级和班级查询
        /// </summary>
        /// <returns></returns>
        public JsonResult SelectClassAndGradeBy(int gid,int cid)
        {
            var a = from Classes in db.Class
                    where
                      Classes.Gid == gid &&
                      Classes.Cid == cid
                    select new
                    {
                        Classes.Grade.GNum,
                        Classes.CName
                    };
            return Json(a,JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 今日请假信息
        /// </summary>
        /// <returns></returns>
        public JsonResult TodayLeave(string grade, string cla,int rows,int pagesize)
        {
            int page = (pagesize - 1) * rows;
            var a = (from LeaveInfoes in db.LeaveInfo
                     where
                       !
                         ((from LeaveInfoes0 in db.LeaveInfo
                           where
         SqlFunctions.DateDiff("dd", LeaveInfoes0.BeginDate, SqlFunctions.GetDate()) == 0 &&
         LeaveInfoes0.GNum == grade &&
         LeaveInfoes0.CName == cla
                           select new
                           {
                               LeaveInfoes0.Lid
                           }).Take(page)).Contains(new { Lid = LeaveInfoes.Lid }) &&
                       SqlFunctions.DateDiff("dd", LeaveInfoes.BeginDate, SqlFunctions.GetDate()) == 0 &&
                       LeaveInfoes.GNum == grade &&
                       LeaveInfoes.CName == cla
                     select new
                     {
                         Lid = LeaveInfoes.Lid,
                         StuNum = LeaveInfoes.StuNum,
                         StuName = LeaveInfoes.StuName,
                         Phone = LeaveInfoes.Phone,
                         GNum = LeaveInfoes.GNum,
                         CName = LeaveInfoes.CName,
                         BeginDate = LeaveInfoes.BeginDate,
                         EndDate = LeaveInfoes.EndDate,
                         Addresss = LeaveInfoes.Addresss,
                         Reason = LeaveInfoes.Reason,
                         Principal = LeaveInfoes.Principal,
                         Statu = LeaveInfoes.Statu
                     }).Take(rows);
            return Json(a, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 今天请假数量
        /// </summary>
        /// <returns></returns>
        public JsonResult TodayTotal(string grade,string cla)
        {
            
            var a = from LeaveInfoes in
(from LeaveInfoes in db.LeaveInfo
 where
   LeaveInfoes.CName == cla &&
   LeaveInfoes.GNum == grade &&
   SqlFunctions.DateDiff("dd", LeaveInfoes.BeginDate, SqlFunctions.GetDate()) == 0
 select new
 {
     Dummy = "x"
 })
                    group LeaveInfoes by new { LeaveInfoes.Dummy } into g
                    select new
                    {
                        total = g.Count()
                    };
            return Json(a,JsonRequestBehavior.AllowGet);
        }
         /// <summary>
         /// 昨日请假信息
         /// </summary>
         /// <returns></returns>
        public JsonResult YesdayLeave(string grade, string cla, int rows, int pagesize)
        {
            int page = (pagesize - 1) * rows;
            var a = (from LeaveInfoes in db.LeaveInfo
                     where
                       !
                         ((from LeaveInfoes0 in db.LeaveInfo
                           where
         SqlFunctions.DateDiff("dd", LeaveInfoes0.BeginDate, SqlFunctions.GetDate()) == 1 &&
         LeaveInfoes0.GNum == grade &&
         LeaveInfoes0.CName == cla
                           select new
                           {
                               LeaveInfoes0.Lid
                           }).Take(page)).Contains(new { Lid = LeaveInfoes.Lid }) &&
                       SqlFunctions.DateDiff("dd", LeaveInfoes.BeginDate, SqlFunctions.GetDate()) == 1 &&
                       LeaveInfoes.GNum == grade &&
                       LeaveInfoes.CName == cla
                     select new
                     {
                         Lid = LeaveInfoes.Lid,
                         StuNum = LeaveInfoes.StuNum,
                         StuName = LeaveInfoes.StuName,
                         Phone = LeaveInfoes.Phone,
                         GNum = LeaveInfoes.GNum,
                         CName = LeaveInfoes.CName,
                         BeginDate = LeaveInfoes.BeginDate,
                         EndDate = LeaveInfoes.EndDate,
                         Addresss = LeaveInfoes.Addresss,
                         Reason = LeaveInfoes.Reason,
                         Principal = LeaveInfoes.Principal,
                         Statu = LeaveInfoes.Statu
                     }).Take(rows);
            return Json(a, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 昨天请假数量
        /// </summary>
        /// <returns></returns>
        public JsonResult YesdayTotal(string grade, string cla)
        {

            var a = from LeaveInfoes in
(from LeaveInfoes in db.LeaveInfo
 where
   LeaveInfoes.CName == cla &&
   LeaveInfoes.GNum == grade &&
   SqlFunctions.DateDiff("dd", LeaveInfoes.BeginDate, SqlFunctions.GetDate()) == 1
 select new
 {
     Dummy = "x"
 })
                    group LeaveInfoes by new { LeaveInfoes.Dummy } into g
                    select new
                    {
                        total = g.Count()
                    };
            return Json(a, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 本周请假信息
        /// </summary>
        /// <returns></returns>
        public JsonResult WeekLeave(string grade, string cla, int rows, int pagesize)
        {
            int page = (pagesize - 1) * rows;
            var a = (from LeaveInfoes in db.LeaveInfo
                     where
                       !
                         ((from LeaveInfoes0 in db.LeaveInfo
                           where
         SqlFunctions.DateDiff("dd", LeaveInfoes0.BeginDate, SqlFunctions.GetDate()) <= 7 &&
         LeaveInfoes0.GNum == grade &&
         LeaveInfoes0.CName == cla
                           select new
                           {
                               LeaveInfoes0.Lid
                           }).Take(page)).Contains(new { Lid = LeaveInfoes.Lid }) &&
                       SqlFunctions.DateDiff("dd", LeaveInfoes.BeginDate, SqlFunctions.GetDate()) <= 7 &&
                       LeaveInfoes.GNum == grade &&
                       LeaveInfoes.CName == cla
                     select new
                     {
                         Lid = LeaveInfoes.Lid,
                         StuNum = LeaveInfoes.StuNum,
                         StuName = LeaveInfoes.StuName,
                         Phone = LeaveInfoes.Phone,
                         GNum = LeaveInfoes.GNum,
                         CName = LeaveInfoes.CName,
                         BeginDate = LeaveInfoes.BeginDate,
                         EndDate = LeaveInfoes.EndDate,
                         Addresss = LeaveInfoes.Addresss,
                         Reason = LeaveInfoes.Reason,
                         Principal = LeaveInfoes.Principal,
                         Statu = LeaveInfoes.Statu
                     }).Take(rows);
            return Json(a, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 本周请假数量
        /// </summary>
        /// <returns></returns>
        public JsonResult WeekTotal(string grade, string cla)
        {

            var a = from LeaveInfoes in
(from LeaveInfoes in db.LeaveInfo
 where
   LeaveInfoes.CName == cla &&
   LeaveInfoes.GNum == grade &&
   SqlFunctions.DateDiff("dd", LeaveInfoes.BeginDate, SqlFunctions.GetDate()) <= 7
 select new
 {
     Dummy = "x"
 })
                    group LeaveInfoes by new { LeaveInfoes.Dummy } into g
                    select new
                    {
                        total = g.Count()
                    };
            return Json(a, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 本月请假信息
        /// </summary>
        /// <returns></returns>
        public JsonResult MonthLeave(string grade, string cla, int rows, int pagesize)
        {
            int page = (pagesize - 1) * rows;
            var a = (from LeaveInfoes in db.LeaveInfo
                     where
                       !
                         ((from LeaveInfoes0 in db.LeaveInfo
                           where
         SqlFunctions.DateDiff("mm", LeaveInfoes0.BeginDate, SqlFunctions.GetDate()) == 0 &&
         LeaveInfoes0.GNum == grade &&
         LeaveInfoes0.CName == cla
                           select new
                           {
                               LeaveInfoes0.Lid
                           }).Take(page)).Contains(new { Lid = LeaveInfoes.Lid }) &&
                       SqlFunctions.DateDiff("mm", LeaveInfoes.BeginDate, SqlFunctions.GetDate()) == 0 &&
                       LeaveInfoes.GNum == grade &&
                       LeaveInfoes.CName == cla
                     select new
                     {
                         Lid = LeaveInfoes.Lid,
                         StuNum = LeaveInfoes.StuNum,
                         StuName = LeaveInfoes.StuName,
                         Phone = LeaveInfoes.Phone,
                         GNum = LeaveInfoes.GNum,
                         CName = LeaveInfoes.CName,
                         BeginDate = LeaveInfoes.BeginDate,
                         EndDate = LeaveInfoes.EndDate,
                         Addresss = LeaveInfoes.Addresss,
                         Reason = LeaveInfoes.Reason,
                         Principal = LeaveInfoes.Principal,
                         Statu = LeaveInfoes.Statu
                     }).Take(rows);
            return Json(a, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 本月请假数量
        /// </summary>
        /// <returns></returns>
        public JsonResult MonthTotal(string grade, string cla)
        {

            var a = from LeaveInfoes in
(from LeaveInfoes in db.LeaveInfo
 where
   LeaveInfoes.CName == cla &&
   LeaveInfoes.GNum == grade &&
   SqlFunctions.DateDiff("mm", LeaveInfoes.BeginDate, SqlFunctions.GetDate()) == 0
 select new
 {
     Dummy = "x"
 })
                    group LeaveInfoes by new { LeaveInfoes.Dummy } into g
                    select new
                    {
                        total = g.Count()
                    };
            return Json(a, JsonRequestBehavior.AllowGet);
        }
    }
}