using CRUD.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUD.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        /*public ActionResult Index(int? page, int? pageSize)
        {
            if (page == null)
            {
                page = 1;
            }
            if (pageSize == null)
            {
                pageSize = 2;
            }
            StudentList stuList = new StudentList();
            List<Student> obj = stuList.GetStudent(String.Empty).OrderBy(x=>x.FullName).ToList();
            return View(obj.ToPagedList((int)page,(int)pageSize));
        }*/

        public ActionResult Index(string strSearch, int? page, int? pageSize)
        {
            if (page == null)
            {
                page = 1;
            }
            if (pageSize == null)
            {
                pageSize = 2;
            }

            StudentList stuList = new StudentList();
            List<Student> obj = stuList.GetStudent(String.Empty).OrderBy(x => x.FullName).ToList();
            if (!string.IsNullOrEmpty(strSearch))
            {
                obj = obj.Where(x => x.FullName.Contains(strSearch)).ToList();
            }
            return View(obj.ToPagedList((int)page, (int)pageSize));
        }

        public ActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Create(Student stu)
        {
            if (ModelState.IsValid)
            {
                StudentList stuList = new StudentList();
                stuList.AddStudent(stu);
                return RedirectToAction("Index");
                
            }
            return View();
        }
        public ActionResult Edit(String id = " ")
        {
            StudentList stuList = new StudentList();
            List<Student> obj = stuList.GetStudent(id);
            return View(obj.FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Edit(Student stu)
        {
            StudentList stuList = new StudentList();
            stuList.UpdateStudent(stu);
            return RedirectToAction("Index");
        }
        public ActionResult Delete(string id = " ")
        {
            StudentList stuList = new StudentList();
            List<Student> obj = stuList.GetStudent(id);
            return View(obj.FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Delete(Student stu)
        {
            StudentList stuList = new StudentList();
            stuList.DeleteStudent(stu);
            return RedirectToAction("Index");
        }
    }
}