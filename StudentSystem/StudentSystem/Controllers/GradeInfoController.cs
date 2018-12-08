using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudentSystem.Models;
using System.Data.Entity.SqlServer;
namespace StudentSystem.Controllers
{
    public class GradeInfoController : Controller
    {
        studentLeaveSystemEntities db = new studentLeaveSystemEntities();
        // GET: GradeInfo
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 查询级别
        /// </summary>
        /// <returns></returns>
        public JsonResult selectGrade()
        {
            var b = from grade in db.Grade
                    select new
                    {
                        Gid = grade.Gid,
                        GNum = grade.GNum
                    };
            return Json(b, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 今日请假信息
        /// </summary>
        /// <returns></returns>
        public JsonResult TodayLeave(string grade,  int rows, int pagesize)
        {
            int page = (pagesize - 1) * rows;
            var a = (from LeaveInfoes in db.LeaveInfo
                     where
                       !
                         ((from LeaveInfoes0 in db.LeaveInfo
                           where
         SqlFunctions.DateDiff("dd", LeaveInfoes0.BeginDate, SqlFunctions.GetDate()) == 0 &&
         LeaveInfoes0.GNum == grade 
       
                           select new
                           {
                               LeaveInfoes0.Lid
                           }).Take(page)).Contains(new { Lid = LeaveInfoes.Lid }) &&
                       SqlFunctions.DateDiff("dd", LeaveInfoes.BeginDate, SqlFunctions.GetDate()) == 0 &&
                       LeaveInfoes.GNum == grade 
                   
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
        public JsonResult TodayTotal(string grade)
        {

            var a = from LeaveInfoes in
(from LeaveInfoes in db.LeaveInfo
 where
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
            return Json(a, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 昨日请假信息
        /// </summary>
        /// <returns></returns>
        public JsonResult YesdayLeave(string grade,  int rows, int pagesize)
        {
            int page = (pagesize - 1) * rows;
            var a = (from LeaveInfoes in db.LeaveInfo
                     where
                       !
                         ((from LeaveInfoes0 in db.LeaveInfo
                           where
         SqlFunctions.DateDiff("dd", LeaveInfoes0.BeginDate, SqlFunctions.GetDate()) == 1 &&
         LeaveInfoes0.GNum == grade 
 
                           select new
                           {
                               LeaveInfoes0.Lid
                           }).Take(page)).Contains(new { Lid = LeaveInfoes.Lid }) &&
                       SqlFunctions.DateDiff("dd", LeaveInfoes.BeginDate, SqlFunctions.GetDate()) == 1 &&
                       LeaveInfoes.GNum == grade 
 
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
        public JsonResult YesdayTotal(string grade)
        {

            var a = from LeaveInfoes in
(from LeaveInfoes in db.LeaveInfo
 where

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
        public JsonResult WeekLeave(string grade,int rows, int pagesize)
        {
            int page = (pagesize - 1) * rows;
            var a = (from LeaveInfoes in db.LeaveInfo
                     where
                       !
                         ((from LeaveInfoes0 in db.LeaveInfo
                           where
         SqlFunctions.DateDiff("dd", LeaveInfoes0.BeginDate, SqlFunctions.GetDate()) <= 7 &&
         LeaveInfoes0.GNum == grade
                           select new
                           {
                               LeaveInfoes0.Lid
                           }).Take(page)).Contains(new { Lid = LeaveInfoes.Lid }) &&
                       SqlFunctions.DateDiff("dd", LeaveInfoes.BeginDate, SqlFunctions.GetDate()) <= 7 &&
                       LeaveInfoes.GNum == grade 
     
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
        public JsonResult WeekTotal(string grade)
        {

            var a = from LeaveInfoes in
(from LeaveInfoes in db.LeaveInfo
 where

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
        public JsonResult MonthLeave(string grade, int rows, int pagesize)
        {
            int page = (pagesize - 1) * rows;
            var a = (from LeaveInfoes in db.LeaveInfo
                     where
                       !
                         ((from LeaveInfoes0 in db.LeaveInfo
                           where
         SqlFunctions.DateDiff("mm", LeaveInfoes0.BeginDate, SqlFunctions.GetDate()) == 0 &&
         LeaveInfoes0.GNum == grade 
      
                           select new
                           {
                               LeaveInfoes0.Lid
                           }).Take(page)).Contains(new { Lid = LeaveInfoes.Lid }) &&
                       SqlFunctions.DateDiff("mm", LeaveInfoes.BeginDate, SqlFunctions.GetDate()) == 0 &&
                       LeaveInfoes.GNum == grade 
                   
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
        public JsonResult MonthTotal(string grade)
        {

            var a = from LeaveInfoes in
(from LeaveInfoes in db.LeaveInfo
 where

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