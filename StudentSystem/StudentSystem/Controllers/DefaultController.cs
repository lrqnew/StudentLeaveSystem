using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudentSystem.Models;
namespace StudentSystem.Controllers
{
    public class DefaultController : Controller
    {
        studentLeaveSystemEntities db = new studentLeaveSystemEntities();
        // GET: Default
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 今日销假人数
        /// </summary>
        /// <returns></returns>
        public JsonResult TodayXj()
        {
            var a = from LeaveInfoes in
(from LeaveInfoes in db.LeaveInfo
 where
   SqlFunctions.DateDiff("dd", LeaveInfoes.EndDate, SqlFunctions.GetDate()) == 0 &&
   LeaveInfoes.Statu == 1
 select new
 {
     Dummy = "x"
 })
                    group LeaveInfoes by new { LeaveInfoes.Dummy } into g
                    select new
                    {
                        xjTotal = g.Count()
                    };
            return Json(a,JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 昨日销假人数
        /// </summary>
        /// <returns></returns>
        public JsonResult YesXj()
        {
            var a = from LeaveInfoes in
(from LeaveInfoes in db.LeaveInfo
 where
   SqlFunctions.DateDiff("dd", LeaveInfoes.EndDate, SqlFunctions.GetDate()) == 1 &&
   LeaveInfoes.Statu == 1
 select new
 {
     Dummy = "x"
 })
                    group LeaveInfoes by new { LeaveInfoes.Dummy } into g
                    select new
                    {
                        xjTotal = g.Count()
                    };
            return Json(a, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 本周销假人数
        /// </summary>
        /// <returns></returns>
        public JsonResult WeekXj()
        {
            var a = from LeaveInfoes in
(from LeaveInfoes in db.LeaveInfo
 where
   SqlFunctions.DateDiff("dd", LeaveInfoes.EndDate, SqlFunctions.GetDate()) <= 7 &&
   LeaveInfoes.Statu == 1
 select new
 {
     Dummy = "x"
 })
                    group LeaveInfoes by new { LeaveInfoes.Dummy } into g
                    select new
                    {
                        xjTotal = g.Count()
                    };
            return Json(a, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 今日请假人数
        /// </summary>
        /// <returns></returns>
        public JsonResult TodayTotal()
        {
            var a = from LeaveInfoes in
                    (from LeaveInfoes in db.LeaveInfo
                     where
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
        /// 今天请假数据
        /// </summary>
        /// <returns></returns>
        public JsonResult Today( int rows, int pagesize)
        {
           int page = (pagesize - 1) * rows;
            var a = (from LeaveInfoes in db.LeaveInfo
                     where
                       !
                         ((from LeaveInfoes0 in db.LeaveInfo
                           where
         SqlFunctions.DateDiff("dd", LeaveInfoes0.BeginDate, SqlFunctions.GetDate()) == 0 
                           select new
                           {
                               LeaveInfoes0.Lid
                           }).Take(page)).Contains(new { Lid = LeaveInfoes.Lid }) &&
                       SqlFunctions.DateDiff("dd", LeaveInfoes.BeginDate, SqlFunctions.GetDate()) == 0 
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
        /// 昨天请假人数
        /// </summary>
        /// <returns></returns>
        public JsonResult YesterdayTotal()
        {
            var a= from LeaveInfoes in
                     (from LeaveInfoes in db.LeaveInfo
                      where
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
            return Json(a,JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 昨天请假数据
        /// </summary>
        /// <returns></returns>
        public JsonResult Yesterday(int rows, int pagesize)
        {
            int page = (pagesize - 1) * rows;
            var a = (from LeaveInfoes in db.LeaveInfo
                     where
                       !
                         ((from LeaveInfoes0 in db.LeaveInfo
                           where
         SqlFunctions.DateDiff("dd", LeaveInfoes0.BeginDate, SqlFunctions.GetDate()) == 1 
                           select new
                           {
                               LeaveInfoes0.Lid
                           }).Take(page)).Contains(new { Lid = LeaveInfoes.Lid }) &&
                       SqlFunctions.DateDiff("dd", LeaveInfoes.BeginDate, SqlFunctions.GetDate()) == 1 
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
        /// 本周请假人数
        /// </summary>
        /// <returns></returns>
        public JsonResult WeekTotal()
        {
            var a = from LeaveInfoes in
                      (from LeaveInfoes in db.LeaveInfo
                       where
                       SqlFunctions.DateDiff("dd", LeaveInfoes.BeginDate, SqlFunctions.GetDate()) <=7
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
        /// 本周请假数据
        /// </summary>
        /// <returns></returns>
        public JsonResult Week(int rows, int pagesize)
        {
            int page = (pagesize - 1) * rows;
            var a = (from LeaveInfoes in db.LeaveInfo
                     where
                       !
                         ((from LeaveInfoes0 in db.LeaveInfo
                           where
         SqlFunctions.DateDiff("dd", LeaveInfoes0.BeginDate, SqlFunctions.GetDate()) <= 7
                           select new
                           {
                               LeaveInfoes0.Lid
                           }).Take(page)).Contains(new { Lid = LeaveInfoes.Lid }) &&
                       SqlFunctions.DateDiff("dd", LeaveInfoes.BeginDate, SqlFunctions.GetDate()) <= 7
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
        /// 本月请假人数
        /// </summary>
        /// <returns></returns>
        public JsonResult MonthTotal()
        {
            var a = from LeaveInfoes in
                      (from LeaveInfoes in db.LeaveInfo
                       where
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
        /// <summary>
        /// 本月请假数据
        /// </summary>
        /// <returns></returns>
        public JsonResult Month(int rows, int pagesize)
        {
            int page = (pagesize - 1) * rows;
            var a = (from LeaveInfoes in db.LeaveInfo
                     where
                       !
                         ((from LeaveInfoes0 in db.LeaveInfo
                           where
         SqlFunctions.DateDiff("mm", LeaveInfoes0.BeginDate, SqlFunctions.GetDate()) == 0 
                           select new
                           {
                               LeaveInfoes0.Lid
                           }).Take(page)).Contains(new { Lid = LeaveInfoes.Lid }) &&
                       SqlFunctions.DateDiff("mm", LeaveInfoes.BeginDate, SqlFunctions.GetDate()) == 0 

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
    }
}